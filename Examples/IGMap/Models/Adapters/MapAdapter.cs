using System;
using System.Windows;
using System.Windows.Threading;
using Infragistics.Controls.Maps;           // XamMap, MapLayer
using Infragistics.Samples.Shared.Models;

namespace IGMap.Models
{
    public class MapAdapter
    {
        private XamMap _map;
        protected bool ShowCoordinateLines;
        //private Rect NextWindowRect { get; set; }
        public XamMap Map { get { return _map; } set { _map = value; } }

        public MapAdapter(XamMap map)
            : this(map, false)
        {
        }

        public MapAdapter(XamMap map, bool showCoordinateLines)
        {
            _map = map;
            this.ShowCoordinateLines = showCoordinateLines;
        }
        public MapAdapter(XamMap map, GeoRegion region)
        {
            _map = map;
            SetMapBoundary(region);
        }

        #region  Boundry Methods
        public void SetMapBoundary()
        {
            SetMapBoundary(GeoRegions.WorldNonPolarRegion);
        }

        public void SetMapBoundary(GeoRegion region)
        {
            SetMapBoundary(_map, region);
        }
        public static void SetMapBoundary(XamMap map, GeoRegion region)
        {
            var mode = map.WindowAnimationMode;
            map.IsAutoWorldRect = false;
            map.WindowAnimationMode = MapWindowAnimationMode.None;
            // Change the map control's WindowRect and WorldRect
            map.WindowRect = map.WorldRect = ProjectMapRegion(region, map).ToRect();
            foreach (MapLayer layer in map.Layers)
            {
                layer.WorldRect = map.WorldRect;
            }
            map.WindowCenter = ProjectMapLocation(0, 0, map);
            map.WindowAnimationMode = mode;
        } 
        #endregion

        #region Pan Methods
        public void PanMapToLocation(double longitude, double latitude)
        {
            this.PanMapToLocation(new Point(longitude, latitude));
        }
        public void PanMapToLocation(Point location)
        {
            this.PanMapToLocation(GeoLocation.FromPoint(location));
        }
        public void PanMapToLocation(GeoLocation location)
        {
            _map.WindowCenter = this.ProjectMapLocation(location.ToPoint());
        }
       
        #endregion

        #region Zoom Methods
        public void ZoomMapToLocation(double longitude, double latitude)
        {
            this.ZoomMapToLocation(new Point(longitude, latitude));
        }
        public void ZoomMapToLocation(double longitude, double latitude, double zoom)
        {
            this.ZoomMapToLocation(new Point(longitude, latitude), zoom);
        }
        public void ZoomMapToLocation(Point location)
        {
            this.ZoomMapToLocation(GeoLocation.FromPoint(location));
        }
        public void ZoomMapToLocation(Point location, double zoom)
        {
            this.ZoomMapToLocation(GeoLocation.FromPoint(location), zoom);
        }
        public void ZoomMapToLocation(GeoLocation location)
        {
            this.ZoomMapToLocation(location, 45);
        }
        public void ZoomMapToLocation(GeoLocation location, double zoom)
        {
            this.ZoomMapToRegion(new GeoRegion(location, 180 / zoom, 90 / zoom));
        }
        public void ZoomMapToRegion(GeoRegion region)
        {
            this.ZoomMapToRegion(region, TimeSpan.FromSeconds(0.0));
        }

        public void ZoomMapToRegion(GeoRegion region, TimeSpan zoomDuration)
        {
            ZoomMapToRegion(_map, region, zoomDuration);
        }
        public static void ZoomMapToRegion(XamMap map, GeoRegion region)
        {
            ZoomMapToRegion(map, region, TimeSpan.FromSeconds(0.0));
        }
        private static Rect MapWindowRect { get; set; }
        public static void ZoomMapToRegion(XamMap map, GeoRegion region, TimeSpan zoomDuration)
        {
            var currentWindowRect = map.WindowRect;
            var geodeticGeoRegion = region;
            var cartesianGeoRegion = ProjectMapRegion(geodeticGeoRegion, map);
            MapWindowRect = cartesianGeoRegion.ToRect();

            if (zoomDuration.TotalSeconds < 1.0)
            {
                map.WindowRect = MapWindowRect;
                return;
            }
            
            currentWindowRect.Union(MapWindowRect);
            map.WindowRect = currentWindowRect;
            var timer = new DispatcherTimer { Interval = zoomDuration };
            timer.Tick += (o, e) =>
            {
                map.WindowRect = MapWindowRect;
                ((DispatcherTimer)o).Stop();
            };
            timer.Start();
        }
      
        #endregion

        #region Projection Methods

        /// <summary>
        ///  Project Geodetic Coordinates to Cartesian Point 
        /// </summary>
        /// <param name="longitude"></param>
        /// <param name="latitude"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Point ProjectMapLocation(double longitude, double latitude, XamMap map)
        {
            return ProjectMapLocation(new Point(longitude, latitude), map);
        }
        public Point ProjectMapLocation(double longitude, double latitude)
        {
            return this.ProjectMapLocation(new Point(longitude, latitude));
        }
        /// <summary>
        /// Project Geodetic GeoLocation to Cartesian GeoLocation
        /// </summary>
        /// <param name="location"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static GeoLocation ProjectMapLocation(GeoLocation location, XamMap map)
        {
            Point cartesianMapPoint = ProjectMapLocation(location.ToPoint(), map);
            return GeoLocation.FromPoint(cartesianMapPoint);
        }
        public GeoLocation ProjectMapLocation(GeoLocation location)
        {
            Point cartesianMapPoint = this.ProjectMapLocation(location.ToPoint());
            return GeoLocation.FromPoint(cartesianMapPoint);
        }
        /// <summary>
        /// Project Geodetic Point to Cartesian Point
        /// </summary>
        /// <param name="mapPoint"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static Point ProjectMapLocation(Point mapPoint, XamMap map)
        {
            return map.MapProjection.ProjectToMap(mapPoint);
        }
        private Point ProjectMapLocation(Point mapPoint)
        {
            return _map.MapProjection.ProjectToMap(mapPoint);
        }

        /// <summary>
        /// Project Geodetic GeoRegion to Cartesian GeoRegion
        /// </summary>
        /// <param name="region"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public static GeoRegion ProjectMapRegion(GeoRegion region, XamMap map)
        {
            GeoLocation cartesianMapOrigin = ProjectMapLocation(region.Origin, map);
            GeoLocation cartesianMapEnding = ProjectMapLocation(region.Ending, map);
            return new GeoRegion(cartesianMapOrigin, cartesianMapEnding);
        }
        public GeoRegion ProjectMapRegion(GeoRegion region)
        {
            GeoLocation cartesianMapOrigin = this.ProjectMapLocation(region.Origin);
            GeoLocation cartesianMapEnding = this.ProjectMapLocation(region.Ending);
            return new GeoRegion(cartesianMapOrigin, cartesianMapEnding);
        }
        #endregion

      


        /// <summary>
        /// Convert Cartesian to Geodetic coordinates
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public Point UnProjectMapLocation(Point point)
        {
            return _map.MapProjection.UnprojectFromMap(point);
        }

        public string ConvertDegreestoString(double value, string negativeDirection, string postiveDirection)
        {
            string result = String.Format("{0:0.00}", value);
            if (value < 0)
                result += String.Format(" ({0:0.00} {1})", value, negativeDirection);
            else if (value > 0)
                result += String.Format(" ({0:0.00} {1})", value, postiveDirection);

            return result;
        }

    }
}
