using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    /// <summary>
    /// Represents an adapter for the <see cref="XamGeographicMap"/>
    /// </summary>
    public static class GeoMapAdapter
    {
        public const double ZoomScaleFactor = 0.05;
        public const double PanScaleFactor = 0.1;
        
        public const double MapWindowMinPosition = 0.0;    
        public const double MapWindowMaxZoom = 1.0;        
        /// <summary>
        /// Minimum zoom allowed by Geographic Imagery Map
        /// </summary>
        public const double MapWindowMinZoom = 0.00010;  
        public static readonly Rect MapWindowMaxView = new Rect(MapWindowMinPosition, MapWindowMinPosition, MapWindowMaxZoom, MapWindowMaxZoom);

        public static void EnableMouseWheelZoomOnMapHover(this XamGeographicMap geoMap)
        {
            geoMap.MouseEnter += (o, e) =>
            {
                geoMap.Focus();
            };
        }

        #region Methods - Zooming
        /// <summary>
        /// Zooms XamGeographicMap to a given window Rect
        /// </summary>
        /// <param name="geoMap"></param>
        /// <param name="windowRect"></param>
        public static void ZoomMapToWindow(XamGeographicMap geoMap, Rect windowRect)
        {
            geoMap.WindowRect = windowRect;
        }
        /// <summary>
        /// Zooms XamGeographicMap to a given geographic location
        /// </summary>
        /// <param name="geoMap"></param>
        /// <param name="geoLocation"></param>
        /// <param name="geoMargin"></param>
        public static void ZoomMapToLocation(XamGeographicMap geoMap, GeoLocation geoLocation, double geoMargin = 3.0)
        {
            //validate geoMargin
            if (double.IsInfinity(geoMargin) || double.IsNaN(geoMargin))
                geoMargin = 3.0;

            geoMargin = System.Math.Abs(geoMargin);
            // navigate to a geographic location 
            var geoRect = new Rect(geoLocation.Longitude - geoMargin, geoLocation.Latitude - geoMargin, 2 * geoMargin, 2 * geoMargin);
            Rect windowRect = geoMap.GetZoomFromGeographic(geoRect);
            geoMap.WindowRect = windowRect;
        }
        /// <summary>
        /// Zoom in the <see cref="XamGeographicMap"/> by specified zoom factor - percentage of the current window view rectangle
        /// </summary>
        public static void ZoomIn(this XamGeographicMap geoMap, double zoomScaleFactor = ZoomScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double zoomScaleH = window.Height * zoomScaleFactor;
            double zoomScaleW = window.Width * zoomScaleFactor;
            //const double zoomWindowMin = 0.00005;

            window.Width = System.Math.Max(GeoMapAdapter.MapWindowMinZoom, window.Width - (2 * zoomScaleW));
            window.Height = System.Math.Max(GeoMapAdapter.MapWindowMinZoom, window.Height - (2 * zoomScaleH));

            //if (window.Width > GeoMapAdapter.MapWindowMinZoom) 
                window.X = System.Math.Min(1.0, window.X + zoomScaleW);
            //if (window.Height > GeoMapAdapter.MapWindowMinZoom) 
                window.Y = System.Math.Min(1.0, window.Y + zoomScaleH);

            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Zoom out the <see cref="XamGeographicMap"/> by specified zoom factor - percentage of the current window view rectangle
        /// </summary>
        public static void ZoomOut(this XamGeographicMap geoMap, double zoomScaleFactor = ZoomScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double zoomScaleH = window.Height * zoomScaleFactor;
            double zoomScaleW = window.Width * zoomScaleFactor;
            //const double zoomWindowMax = 1.0;

            window.Width = System.Math.Min(GeoMapAdapter.MapWindowMaxZoom, window.Width + (2 * zoomScaleW));
            window.Height = System.Math.Min(GeoMapAdapter.MapWindowMaxZoom, window.Height + (2 * zoomScaleH));

            //if (window.Width < GeoMapAdapter.MapWindowMaxZoo) 
                window.X = System.Math.Max(0.0, window.X - zoomScaleW);
            //if (window.Height < GeoMapAdapter.MapWindowMaxZoo) 
                window.Y = System.Math.Max(0.0, window.Y - zoomScaleH);

            geoMap.NavigateTo(window);
        } 
        #endregion
        #region Methods - Panning
        
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> to specified geographic region of the world
        /// </summary>
        /// <param name="geoMap">an instance of XamGeographicMap</param>
        /// <param name="geoRegion">a geographic region of the world</param>
        public static void NavigateTo(this XamGeographicMap geoMap, GeoRegion geoRegion)
        {
            var windowRect = geoMap.GetZoomFromGeographic(geoRegion.NorthWest.ToPoint(), geoRegion.SouthEast.ToPoint());

            // conversion of geographic region to window rect works when XamGeographicMap view was initialized 
            if (windowRect == new Rect(0, 0, 1, 1))
            {
                geoMap.GridAreaRectChanged += (o, e) =>
                {
                    windowRect = geoMap.GetZoomFromGeographic(geoRegion.NorthWest.ToPoint(), geoRegion.SouthEast.ToPoint());
                    geoMap.WindowRect = windowRect;
                };
            }
            else
            {
                geoMap.WindowRect = windowRect;
            }
         
        }

        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> to specified window view rectangle
        /// </summary>
        public static void NavigateTo(this XamGeographicMap geoMap, Rect winRect)
        {
            if (geoMap == null) return;

            geoMap.WindowRect = winRect;
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> to max window view rectangle
        /// </summary>
        public static void NavigateToFullView(this XamGeographicMap geoMap)
        {
            geoMap.NavigateTo(GeoMapAdapter.MapWindowMaxView);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in West direction by specified pan factor - percentage of the current window's width
        /// </summary>
        public static void NavigateWest(this XamGeographicMap geoMap, double panScaleFactor = PanScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double panScale = window.Width * panScaleFactor;

            window.X = System.Math.Max(0.0, window.X - panScale);
            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in East direction by specified pan factor - percentage of the current window's width
        /// </summary>
        public static void NavigateEast(this XamGeographicMap geoMap, double panScaleFactor = PanScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double panScale = window.Width * panScaleFactor;

            window.X = System.Math.Min(1.0 - window.Width, window.X + panScale);
            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in North direction by specified pan factor - percentage of the current window's height
        /// </summary>
        public static void NavigateNorth(this XamGeographicMap geoMap, double panScaleFactor = PanScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double panScale = window.Width * panScaleFactor;

            window.Y = System.Math.Max(0.0, window.Y - panScale);
            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in South direction by specified pan factor - percentage of the current window's height
        /// </summary>
        public static void NavigateSouth(this XamGeographicMap geoMap, double panScaleFactor = PanScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double panScale = window.Width * panScaleFactor;

            window.Y = System.Math.Min(1.0 - window.Height, window.Y + panScale);
            geoMap.NavigateTo(window);
        }
       

        #endregion

        #region Methods - Calculation
        public static Point GetWindowCenterPoint(this XamGeographicMap geoMap)
        {
            var rect = geoMap.WindowRect;
            double x = rect.X + (rect.Width / 2);
            double y = rect.Y + (rect.Height / 2);
            return new Point(x, y);
        }
        public static Point GetWindowNorthWestPoint(this XamGeographicMap geoMap)
        {
            var rect = geoMap.WindowRect;
            return new Point(rect.X, rect.Y);
        }
        public static Point GetWindowNorthEastPoint(this XamGeographicMap geoMap)
        {
            var rect = geoMap.WindowRect;
            return new Point(rect.X + rect.Width, rect.Y);
        }
        public static Point GetWindowSouthEastPoint(this XamGeographicMap geoMap)
        {
            var rect = geoMap.WindowRect;
            return new Point(rect.X + rect.Width, rect.Y + rect.Height);
        }
        public static Point GetWindowSouthWestPoint(this XamGeographicMap geoMap)
        {
            var rect = geoMap.WindowRect;
            return new Point(rect.X, rect.Y + rect.Height);
        } 

        /// <summary>
        /// Calculates Rect that wraps all shape points 
        /// </summary>
        /// <param name="shapePoints"></param>
        /// <returns></returns>
        public static Rect GetShapeRectFromPoints(IEnumerable<List<Point>> shapePoints)
        {
            double xMin = double.PositiveInfinity;
            double yMin = double.PositiveInfinity;
            double xMax = double.NegativeInfinity;
            double yMax = double.NegativeInfinity;

            foreach (List<Point> points in shapePoints)
            {
                foreach (Point point in points)
                {
                    xMin = System.Math.Min(xMin, point.X);
                    xMax = System.Math.Max(xMax, point.X);

                    yMin = System.Math.Min(yMin, point.Y);
                    yMax = System.Math.Max(yMax, point.Y);
                }
            }
            double width = xMax - xMin;
            double height = yMax - yMin;
            var shapeRect = new Rect(xMin, yMin, width, height);
            return shapeRect;
        }

        /// <summary>
        /// Calculates geographic location of mouse over a given XamGeographicMap
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <param name="geoMap"></param>
        /// <returns></returns>
        public static GeoLocation GetGeoLocation(Point mousePosition, XamGeographicMap geoMap)
        {
            var xAxis = geoMap.XAxis;
            var yAxis = geoMap.YAxis;

            var viewport = new Rect(0, 0, geoMap.ActualWidth, geoMap.ActualHeight);
            var window = geoMap.WindowRect;

            bool isInverted = xAxis.IsInverted;
            var param = new ScalerParams(window, viewport, isInverted);
            param.EffectiveViewportRect = geoMap.EffectiveViewport;
            var longitude = xAxis.GetUnscaledValue(mousePosition.X, param);

            isInverted = yAxis.IsInverted;
            param = new ScalerParams(window, viewport, isInverted);
            var latitude = yAxis.GetUnscaledValue(mousePosition.Y, param);

            return new GeoLocation(longitude, latitude);
        }
        #endregion

       
    }
}