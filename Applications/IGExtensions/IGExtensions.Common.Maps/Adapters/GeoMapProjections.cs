using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace Infragistics.Controls.Maps
{
    public class GeoMapProjections
    {
        
    }
    public class MapCartesianProjection
    {
        public MapCartesianProjection()
        {
            UseSquareAspectRatio = true;
        }
        #region Properties
        public bool UseSquareAspectRatio { get; set; }

        public double CartesianMinX { get; private set; }
        public double CartesianMaxX { get; private set; }
        public double CartesianRangeX { get { return this.CartesianMaxX - this.CartesianMinX; } }
        public ScalerParams CartesianScalerX { get; private set; }

        public double CartesianMinY { get; private set; }
        public double CartesianMaxY { get; private set; }
        public double CartesianRangeY { get { return this.CartesianMaxY - this.CartesianMinY; } }
        public ScalerParams CartesianScalerY { get; private set; }

        public Rect CartesianMaxBounds { get; private set; }
        public XamGeographicMap GeographicMap { get; private set; }

         
        #endregion

        public void InitializeMap(XamGeographicMap map)
        {
            if (map == null)
                throw new MapCartesianException("GeographicMap cannot be null when initializing MapCartesianProjection");
          
            GeographicMap = map;
        }

        public void InitializeMapBounds(Rect cartesianMaxBounds)
        {
            if (GeographicMap == null)
                throw new MapCartesianException("GeographicMap cannot be null before initializing map bounds for MapCartesianProjection");
            //GeographicMap = map;
            
            var map = this.GeographicMap;

            CartesianScalerX = new ScalerParams(map.WindowRect, map.ViewportRect, false);
            CartesianScalerX.EffectiveViewportRect = map.EffectiveViewport;
            CartesianScalerY = new ScalerParams(map.WindowRect, map.ViewportRect, false);
            CartesianScalerY.EffectiveViewportRect = map.EffectiveViewport;

            CartesianMinX = map.XAxis.GetScaledValue(map.XAxis.ActualMinimumValue, CartesianScalerX);
            CartesianMaxX = map.XAxis.GetScaledValue(map.XAxis.ActualMaximumValue, CartesianScalerX);

            CartesianMinY = map.YAxis.GetScaledValue(map.YAxis.ActualMaximumValue, CartesianScalerY);
            CartesianMaxY = map.YAxis.GetScaledValue(map.YAxis.ActualMinimumValue, CartesianScalerY);

            CartesianMaxBounds = GetCartesianBounds(cartesianMaxBounds);

            //var bounds = shapefileConverter.WorldRect;

            ////if (CartesianBounds.Height <= bounds.Height &&
            ////    CartesianBounds.Width <= bounds.Width)
            ////{
            ////    CartesianBounds = bounds.WorldRect;
            ////}
            //var aspectRatio = bounds.Width / bounds.Height;
            //if (aspectRatio != 1.0)
            //{
            //    if (bounds.Width < bounds.Height)
            //    {
            //        var diff = bounds.Height - bounds.Width;
            //        var halfDiff = diff / 2.0;

            //        bounds = new Rect(bounds.Left - halfDiff, bounds.Top, bounds.Width + diff, bounds.Height);
            //    }
            //    else
            //    {
            //        var diff = bounds.Width - bounds.Height;
            //        var halfDiff = diff / 2.0;

            //        bounds = new Rect(bounds.Left, bounds.Top - halfDiff, bounds.Width, bounds.Height + diff);
            //    }
            //}
            //CartesianMaxBounds = bounds;
        }

        public Rect GetCartesianBounds(Rect shapeBounds)
        {
            var bounds = shapeBounds;
            var aspectRatio = bounds.Width / bounds.Height;
            if (aspectRatio != 1.0 && this.UseSquareAspectRatio)
            {
                if (bounds.Width < bounds.Height)
                {
                    var diff = bounds.Height - bounds.Width;
                    var halfDiff = diff / 2.0;

                    bounds = new Rect(bounds.Left - halfDiff, bounds.Top, bounds.Width + diff, bounds.Height);
                }
                else
                {
                    var diff = bounds.Width - bounds.Height;
                    var halfDiff = diff / 2.0;

                    bounds = new Rect(bounds.Left, bounds.Top - halfDiff, bounds.Width, bounds.Height + diff);
                }
            }
            return bounds;
        }

        public Rect GetCartesianBounds(ShapefileConverter shapefileConverter)
        {
            if (shapefileConverter == null)
                throw new MapCartesianException("ShapefileConverter cannot be null before using  MapCartesianProjection");
          
            return GetCartesianBounds(shapefileConverter.WorldRect);
        }

        #region Projection Methods
        public Point ProjectToGeographic(Point point)
        {
            if (GeographicMap == null)
                throw new MapCartesianException("GeographicMap cannot be null before using MapCartesianProjection");

            var scaledX = (point.X - CartesianMaxBounds.Left) / CartesianMaxBounds.Width;
            var scaledY = (point.Y - CartesianMaxBounds.Top) / CartesianMaxBounds.Height;

            var xCartesian = CartesianMinX + CartesianRangeX * scaledX;
            var yCartesian = CartesianMaxY - (CartesianRangeY * scaledY);

            var unscaledX = GeographicMap.XAxis.GetUnscaledValue(xCartesian, CartesianScalerX);
            var unscaledY = GeographicMap.YAxis.GetUnscaledValue(yCartesian, CartesianScalerY);

            return new Point(unscaledX, unscaledY);
        }
        public List<Point> ProjectToGeographic(List<Point> points)
        {
            //return pointList.Select(ProjectToGeographic).ToList();
            var geoPoints = new List<Point>();
            foreach (var point in points)
            {
                geoPoints.Add(ProjectToGeographic(point));
            }
            return geoPoints;
        }
        public List<List<Point>> ProjectToGeographic(List<List<Point>> points)
        {
            var geoShapes = new List<List<Point>>();
            for (var i = 0; i < points.Count; i++)
            {
                var geoPoints = ProjectToGeographic(points[i]);
                geoShapes.Add(geoPoints);
            }
            return geoShapes;
        }
        public ShapefileConverter ProjectToGeographic(ShapefileConverter shapefileConverter)
        {
            var shapeRecords = new ShapefileConverter();
            foreach (var record in shapefileConverter)
            {
                if (record.Points.Count > 0)
                    shapeRecords.Add(record);
            }
            foreach (var record in shapeRecords)
            {
                record.Points = ProjectToGeographic(record.Points);
            }
            return shapeRecords;
        }
        #endregion
    }

    public class MapCartesianException : Exception
    {
        public MapCartesianException() : base() { }
        public MapCartesianException(string message) : base(message) { }
        public MapCartesianException(string message, Exception inner) : base(message, inner) { }

    }
}