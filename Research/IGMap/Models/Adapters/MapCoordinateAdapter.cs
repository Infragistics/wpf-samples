using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Maps;           // XamMap, MapLayer
using Infragistics.Samples.Shared.Models;   // GeoCoordinateGrid, GeoCoordinateLine

namespace IGMap.Models
{
    public class MapCoordinateAdapter  
    {
        public MapCoordinateAdapter( )
        { }
        
        /// <summary>
        /// Add Map Coordinate Grid to the first MapLayer found in passed XamMap control
        /// </summary>
        /// <param name="xamMap"></param>
        public static void AddCoordinateGrid(XamMap xamMap)
        {
            if (xamMap.Layers.Count != 0)
            {
                AddCoordinateGrid(xamMap.Layers[0]);
            }
        }
        /// <summary>
        /// Add Map Coordinate Grid to the MapLayer object 
        /// </summary>
        /// <param name="mapLayer"></param>
        public static void AddCoordinateGrid(MapLayer mapLayer)
        {
            var grid = new GeoCoordinateGrid();
            foreach (GeoLatitudeLine coordinateLine in grid.LatitudeLines)
            {
                AddCoordinateLine(mapLayer, coordinateLine);
            }
            foreach (GeoLongitudeLine coordinateLine in grid.LongitudeLines)
            {
                AddCoordinateLine(mapLayer, coordinateLine);
            }
        }
        /// <summary>
        /// Add Map Coordinate Line to the MapLayer object  
        /// </summary>
        /// <param name="mapLayer"></param>
        /// <param name="coordinateLine"></param>
        public static void AddCoordinateLine(MapLayer mapLayer, GeoCoordinateLine coordinateLine)
        {
            List<Point> coordPoints = new List<Point>();
            coordPoints.Add(coordinateLine.Origin.ToPoint());
            coordPoints.Add(coordinateLine.Ending.ToPoint());

            // poly-line collection for end-points of line
            MapPolylineCollection mapLine = new MapPolylineCollection();
            // Convert Geodetic to Cartesian coordinates
            mapLine.Add(mapLayer.Map.MapProjection.ProjectToMap(coordPoints));
            // Create path element and set points using poly-lines
            PathElement lineElement = new PathElement { Polylines = mapLine };

            lineElement.Fill = mapLayer.Stroke;
            lineElement.StrokeThickness = mapLayer.StrokeThickness;
            lineElement.ShadowFill = mapLayer.ShadowFill;

            mapLayer.Elements.Add(lineElement);

            Rect worldRect = mapLayer.WorldRect;
            worldRect.Union(lineElement.WorldRect);
            mapLayer.WorldRect = worldRect;
        }
    }

   
}