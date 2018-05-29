using System;
using System.Collections.Generic;
using System.Windows;
//using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using IGExtensions.Common.Models;
using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Framework;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace Infragistics.Controls.Maps //IGExtensions.Common.Maps
{
    /// <summary>
    /// Provides constants for the world in the context of the <see cref="XamGeographicMap"/> control
    /// </summary>
    public static class GeoMapGlobals
    {
        //public const double MapWorldLatitudeMin = -85.051128780;
        //public const double MapWorldLatitudeMax = 85.051128780;
        //public const double MapWorldLongitudeMin = -180.0;
        //public const double MapWorldLongitudeMax = 180.0;

        //public const double MapWorldLatitudeMin = -85;
        //public const double MapWorldLatitudeMax = 85;
        //public const double MapWorldLongitudeMin = -180.0;
        //public const double MapWorldLongitudeMax = 180.0;
        
        //public const double MapWorldWidthMax = MapWorldLongitudeMax - MapWorldLongitudeMin;
        //public const double MapWorldHeightMax = MapWorldLatitudeMax - MapWorldLatitudeMin;
        //public static Point MapWorldOriginPoint = new Point(MapWorldLongitudeMin, MapWorldLatitudeMin);
        //public static GeoLocation MapWorldOriginLocation = new GeoLocation(MapWorldLongitudeMin, MapWorldLatitudeMin);

        //public static Rect MapWorldRect = new Rect(MapWorldOriginPoint, new Point(MapWorldLongitudeMax, MapWorldLatitudeMax));
        //public static GeoRegion MapWorldRegion = new GeoRegion(MapWorldOriginPoint, MapWorldWidthMax, MapWorldHeightMax);

        /// <summary>
        /// Minimum zoom allowed by Geographic Imagery in the <see cref="XamGeographicMap"/> control
        /// </summary>
        public const double MapWindowMinZoom = 0.00010;
        public const double MapWindowMaxZoom = 1.0;
        public const double MapWindowMinPosition = 0.0;
        public static readonly Rect MapWindowMaxView = new Rect(MapWindowMinPosition, MapWindowMinPosition, MapWindowMaxZoom, MapWindowMaxZoom);

        public const double MapZoomScaleFactor = 0.05;
        public const double MapPanScaleFactor = 0.1;
    }

    /// <summary>
    /// Represents an adapter with extension methods for the <see cref="XamGeographicMap"/> control
    /// </summary>
    public static class GeoMapAdapter
    {
        //public static double ZoomScaleFactor { get; set; } 
        //public static double PanScaleFactor { get; set; }

        //public const double MapWindowMinPosition = 0.0;    
        //public const double MapWindowMaxZoom = 1.0;        

        //public static GeoMapGlobals MapGlobals = new GeoMapGlobals();        
        //public static GeoMapGlobals MapGlobals { get; set; }

        static GeoMapAdapter()
        {
            //MapGlobals = new GeoMapGlobals();
            SeriesMouseDoubleClickTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(2000) };
            SeriesMouseDoubleClickTimer.Tick += OnSeriesMouseDoubleClickTimerTick;
        }

        #region Events
        public static event DataChartMouseButtonEventHandler SeriesMouseDoubleClick;
        public static void OnMapMouseDoubleClick(object sender, DataChartMouseButtonEventArgs e)
        {
            if (SeriesMouseDoubleClick != null)
                SeriesMouseDoubleClick(sender, e);
        }

        private static bool IsSeriesMouseDoubleClickTracking = false;
        private static int SeriesMouseDoubleClicks = 0;
        private static readonly DispatcherTimer SeriesMouseDoubleClickTimer;

        public static void EnableSeriesMouseDoubleClick(this XamGeographicMap geoMap, bool isEnabled = true)
        {
            IsSeriesMouseDoubleClickTracking = isEnabled;

            if (IsSeriesMouseDoubleClickTracking)
            {
                if (geoMap != null) geoMap.SeriesMouseLeftButtonDown += OnSeriesMouseLeftButtomDown;
                if (geoMap != null) geoMap.SeriesMouseLeftButtonUp += OnSeriesMouseLeftButtonUp;
                //if (geoMap != null) geoMap.SeriesMouseMove += OnSeriesMouseMove;
                SeriesMouseDoubleClickTimer.Start();
            }
            else
            {
                SeriesMouseDoubleClickTimer.Stop();
                if (geoMap != null) geoMap.SeriesMouseLeftButtonDown -= OnSeriesMouseLeftButtomDown;
                if (geoMap != null) geoMap.SeriesMouseLeftButtonUp -= OnSeriesMouseLeftButtonUp;
                //if (geoMap != null) geoMap.SeriesMouseMove -= OnSeriesMouseMove;
            }
        }

        static void OnSeriesMouseMove(object sender, ChartMouseEventArgs e)
        {

        }

        static void OnSeriesMouseDoubleClickTimerTick(object sender, EventArgs e)
        {
            SeriesMouseDoubleClicks = 0;
        }

        static void OnSeriesMouseLeftButtonUp(object sender, DataChartMouseButtonEventArgs e)
        {
            SeriesMouseDoubleClicks++;
            if (SeriesMouseDoubleClicks >= 2)
            {
                OnMapMouseDoubleClick(sender, e);
            }
        }

        static void OnSeriesMouseLeftButtomDown(object sender, DataChartMouseButtonEventArgs e)
        {
            //IsSeriesMouseDoubleClickOn = true;
        }


        public static void EnableMouseWheelZoomOnMapHover(this XamGeographicMap geoMap)
        {
            geoMap.MouseEnter += (o, e) =>
            {
                geoMap.Focus();
            };
        }

        #endregion
        //public static MapProjector MapProjector2; // = new MapProjector();

        //public static MapProjector MapProjector(this XamGeographicMap geoMap)
        //{
        //    if (MapProjector2 != null && MapProjector2.GeoMap.Name != geoMap.Name)
        //    {
        //        MapProjector2 = new MapProjector(geoMap);
           
        //    }
        //    MapProjector2 = new MapProjector(geoMap);
        //    return MapProjector2;
        //}

        #region MapProjector

        private static Rect _unitRect = new Rect(0, 0, 1, 1);
        //public static double GetUnscaledLongitude(this XamGeographicMap geoMap, double cartLongitude)
        //{
        //    var xaxis = geoMap.XAxis;
        //    var yaxis = geoMap.YAxis;

        //    // init appropriate axis scaler parameters
        //    ScalerParams xParams = new ScalerParams(windowRect, ViewportRect, xaxis.IsInverted);
        //    xParams.EffectiveViewportRect = this.EffectiveViewport;
        //    ScalerParams yParams = new ScalerParams(windowRect, ViewportRect, yaxis.IsInverted);
        //    yParams.EffectiveViewportRect = EffectiveViewport;


        //    //var xParams = geoMap.GetLongitudeScaler();
        //    var scaledValue = geoMap.XAxis.GetUnscaledValue(cartLongitude, xParams);
        //    return scaledValue;
        //}

        public static double GetScaledLongitude(this XamGeographicMap geoMap, double geoLongitude)
        {
            var xParams = geoMap.GetLongitudeScaler();
            var scaledValue = geoMap.XAxis.GetScaledValue(geoLongitude, xParams);
            return scaledValue;
        }
        public static double GetScaledLatitude(this XamGeographicMap geoMap, double geoLatitude)
        {
            var yParams = geoMap.GetLatitudeScaler();
            var scaledValue = geoMap.YAxis.GetScaledValue(geoLatitude, yParams);
            return scaledValue;
        }
        public static Point GetScaledLocation(this XamGeographicMap geoMap, Point geoLocation)
        {
            // init appropriate axis scaler parameters
            var x = geoMap.GetScaledLongitude(geoLocation.X);
            var y = geoMap.GetScaledLongitude(geoLocation.Y);
            var scaledLocation = new Point(x, y);
            return scaledLocation;
        }
        public static Point GetScaledLocation(this XamGeographicMap geoMap, GeoLocation geoLocation)
        {
            // init appropriate axis scaler parameters
            var x = geoMap.GetScaledLongitude(geoLocation.Longitude);
            var y = geoMap.GetScaledLongitude(geoLocation.Latitude);
            var scaledLocation = new Point(x, y);
            return scaledLocation;
        }

        public static ScalerParams GetLongitudeScaler(this XamGeographicMap geoMap)
        {
            var xaxis = geoMap.XAxis;
            var xParams = new ScalerParams(_unitRect, geoMap.ViewportRect, xaxis.IsInverted);
            xParams.EffectiveViewportRect = geoMap.EffectiveViewport;
            return xParams;
        }
        public static ScalerParams GetLatitudeScaler(this XamGeographicMap geoMap)
        {
            var yaxis = geoMap.YAxis;
            var yParams = new ScalerParams(_unitRect, geoMap.ViewportRect, yaxis.IsInverted);
            yParams.EffectiveViewportRect = geoMap.EffectiveViewport;
            return yParams;
        } 
        #endregion

        
        #region Methods - Zooming
        /// <summary>
        /// Zooms XamGeographicMap to a given window Rect
        /// </summary>
        /// <param name="geoMap"></param>
        /// <param name="windowRect"></param>
        public static void ZoomMapToWindow(this XamGeographicMap geoMap, Rect windowRect)
        {
            geoMap.WindowRect = windowRect;
        }
        public static void ZoomMapToRegion(this XamGeographicMap geoMap, GeoRect region)
        {
            var windowRect = geoMap.GetZoomFromGeographic(region.ToRect());
            geoMap.ZoomMapToWindow(windowRect);
        }
        public static void ZoomMapToRegion(this XamGeographicMap geoMap, GeoRegion region)
        {
            var windowRect = geoMap.GetZoomFromGeographic(region.ToRect());
            geoMap.ZoomMapToWindow(windowRect);
        }
        public static void ZoomMapToRegion(this XamGeographicMap geoMap, GeoRect region, TimeSpan zoomDuration)
        {
            Rect currentWindowRect = geoMap.WindowRect;
            //GeoRegion geodeticGeoRegion = region;
            var winRect = geoMap.GetZoomFromGeographic(region.ToRect());

            //GeoRegion cartesianGeoRegion = ProjectMapRegion(geodeticGeoRegion, map);
            //MapWindowRect = cartesianGeoRegion.ToRect();
            var dTL = geoMap.WindowRect.TopLeft().DistanceVector(winRect.TopLeft());
            var dTR = geoMap.WindowRect.TopRight().DistanceVector(winRect.TopRight());
            var dBR = geoMap.WindowRect.BottomRight().DistanceVector(winRect.BottomRight());
            var dBL = geoMap.WindowRect.BottomLeft().DistanceVector(winRect.BottomLeft());


            var StartTime = DateTime.MinValue;

            //var zoomInterval = zoomDuration.TotalMilliseconds / 2000.0;
            var zoomInterval = zoomDuration.TotalMilliseconds / 2000.0;
             
            var counter = 0;
            var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(10) };
            timer.Tick += (o, e) =>
            {
              
                if (StartTime == DateTime.MinValue)
                {
                    StartTime = DateTime.Now;
                }
                double tick = (DateTime.Now - StartTime).Ticks / TimeSpan.TicksPerMillisecond;
                double duration = zoomDuration.TotalMilliseconds;
                var factor = 50.0;
                //TODO add logic for smooth transition between map's window Rect
                //double lin = duration > 1.0 ? MathUtil.Clamp(tick / duration, 0.0, 1.0) : 1.0;
                //var tLog = Math.Log(1.0 + factor * Math.E / (Math.E - 1.0));
                //var trig = 0.5 - 0.5 * Math.Cos(Math.PI * factor);

            
                //System.Diagnostics.Debug.WriteLine("  log " + tLog + "  tri " + trig);

                var top = geoMap.WindowRect.Top + (dTL.Y / factor);
                var left = geoMap.WindowRect.Left + (dTL.X / factor);
                var bottom = geoMap.WindowRect.Bottom + (dBR.Y / factor);
                var right = geoMap.WindowRect.Right + (dBR.X / factor);

                counter++;
                var rect = new Rect(new Point(left, top), new Point(right, bottom));
                if (counter > factor || winRect.Contains(rect))
                {
                    ((DispatcherTimer)o).Stop(); 
                }
                 
                geoMap.WindowRect = rect;
                
            };
            timer.Start();
         
        }

        public class WindowAnimator
        {
            ///// <summary>
            ///// Initialises a new WindowAnimator object for the specified viewport
            ///// </summary>
            ///// <param name="viewport"></param>
            //public WindowAnimator(MapViewport viewport)
            //{
            //    Viewport = viewport;
            //}
        }
        public static double GetMapZoomScale(this XamGeographicMap geoMap)
        {
            //var mapZoomScale = System.Math.Min(geoMap.ActualWindowScaleVertical, geoMap.ActualWindowScaleHorizontal);
            var mapZoomScale = geoMap.ActualWindowScale;
            return mapZoomScale; 
        }
        public static double GetMapViewportScale(this XamGeographicMap geoMap)
        {
            //var mapSizeScale = System.Math.Min(geoMap.ViewportRect.Width, geoMap.ViewportRect.Height);
            var mapSizeScale = System.Math.Max(geoMap.ViewportRect.Width, geoMap.ViewportRect.Height);
            return mapSizeScale;
        }

        /// <summary>
        /// Zooms XamGeographicMap to a given geographic location based on source map
        /// </summary>
        /// <param name="geoMap"></param>
        /// <param name="sourceMap"></param>
        /// <param name="mouseLocation">in Cartesian coordinates</param>
        /// <param name="mapZoomMagnification">magnification of source map</param>
        public static void ZoomMapToLocation(this XamGeographicMap geoMap, XamGeographicMap sourceMap, Point mouseLocation, double mapZoomMagnification = 4.0)
        {
            if (mapZoomMagnification < 0.001) mapZoomMagnification = 0.001;
            //if (mapZoomMagnification < 1) mapZoomMagnification = 1;
            //var offset = (sourceMap.GetMapZoomScale() * (geoMap.GetMapViewportScale() / 2)) / mapZoomMagnification;
            //var pixcelNorthWest = new Point(mouseLocation.X - offset, mouseLocation.Y - offset);
            //var pixcelSouthEast = new Point(mouseLocation.X + offset, mouseLocation.Y + offset);

            var offsetX = (sourceMap.ActualWindowScale * (geoMap.ViewportRect.Width / 2)) / mapZoomMagnification;
            var offsetY = (sourceMap.ActualWindowScale * (geoMap.ViewportRect.Height / 2)) / mapZoomMagnification;

            var pixcelNorthWest = new Point(mouseLocation.X - offsetX, mouseLocation.Y - offsetY);
            var pixcelSouthEast = new Point(mouseLocation.X + offsetX, mouseLocation.Y + offsetY);
 
            var w = pixcelSouthEast.X - pixcelNorthWest.X;
            var h = pixcelSouthEast.Y - pixcelNorthWest.Y;

            var pixRect = new Rect(pixcelNorthWest.X, pixcelNorthWest.Y, w, h);
 
            // init appropriate axis scaler parameters
            var xaxis = sourceMap.XAxis;
            var yaxis = sourceMap.YAxis;
            var xParams = new ScalerParams(sourceMap.ActualWindowRect, sourceMap.ViewportRect, xaxis.IsInverted);
            xParams.EffectiveViewportRect = sourceMap.EffectiveViewport;
            var yParams = new ScalerParams(sourceMap.ActualWindowRect, sourceMap.ViewportRect, yaxis.IsInverted);
            yParams.EffectiveViewportRect = sourceMap.EffectiveViewport;

            var gL = xaxis.GetUnscaledValue(pixRect.Left, xParams);
            var gT = yaxis.GetUnscaledValue(pixRect.Bottom, yParams);
            var gR = xaxis.GetUnscaledValue(pixRect.Right, xParams);
            var gB = yaxis.GetUnscaledValue(pixRect.Top, yParams);
             
            var gH = gB - gT;
            var gW = gR - gL;
            //DebugManager.LogWarning("gT " + gT + " gB " + gB + " gL " + gL + " gR " + gR + " | gH " + gH + " gW " + gW);


            //if (gT <= GeoGlobal.MapSphericalMercator.LatitudeMin) return;
            //if (gB >= GeoGlobal.MapSphericalMercator.LatitudeMax) return;

            //if (gR >= GeoGlobal.MapSphericalMercator.LongitudeMax) return;
            //if (gL <= GeoGlobal.MapSphericalMercator.LongitudeMin) return;



            if (gT <= GeoGlobal.WorldsMercator.LatitudeMin && gR >= GeoGlobal.WorldsMercator.LongitudeMax) return;
            if (gT <= GeoGlobal.WorldsMercator.LatitudeMin && gL <= GeoGlobal.WorldsMercator.LongitudeMin) return;

            if (gB >= GeoGlobal.WorldsMercator.LatitudeMax && gR >= GeoGlobal.WorldsMercator.LongitudeMax) return;
            if (gB >= GeoGlobal.WorldsMercator.LatitudeMax && gL <= GeoGlobal.WorldsMercator.LongitudeMin) return;

            if (gT < GeoGlobal.WorldsMercator.LatitudeMin) gT = GeoGlobal.WorldsMercator.LatitudeMin;
            if (gB > GeoGlobal.WorldsMercator.LatitudeMax) gB = GeoGlobal.WorldsMercator.LatitudeMax;

            if (gR > GeoGlobal.WorldsMercator.LongitudeMax) gR = GeoGlobal.WorldsMercator.LongitudeMax;
            if (gL < GeoGlobal.WorldsMercator.LongitudeMin) gL = GeoGlobal.WorldsMercator.LongitudeMin;

            gH = System.Math.Abs(gB - gT);
            gW = System.Math.Abs(gR - gL);
          
            //if (gT + gH > GeoGlobal.MapSphericalMercator.LatitudeMax)
            //{
            //    gB = GeoGlobal.MapSphericalMercator.LatitudeMax;
            //    gT = gB - gH;
            //    //gT = gT - (gH);

            //}
            //if (gT < GeoGlobal.MapSphericalMercator.LatitudeMin)
            //{
            //    gT = GeoGlobal.MapSphericalMercator.LatitudeMin; // +(gH);
            //    gB = gT + gH; // +(gH);
            //    //return;
            //}

            
            var geoRect = new Rect(gL, gT, gW, gH);
            var winRect = geoMap.GetZoomFromGeographic(geoRect);

            //DebugManager.LogWarning("geoRect    " + geoRect.ToString());
            //DebugManager.LogWarning("winRect    " + winRect.ToString());
            //DebugManager.LogWarning("     "  );
          
            geoMap.WindowRect = winRect;
        }

        /// <summary>
        /// Zooms XamGeographicMap to a given geographic location
        /// </summary>
        /// <param name="geoMap"></param>
        /// <param name="geoLocation"></param>
        /// <param name="mapZoomScale"></param>
        public static void ZoomMapToLocation(this XamGeographicMap geoMap, GeoLocation geoLocation, double mapZoomScale = 0.1)
        {
            //validate geoMargin
            if (double.IsInfinity(mapZoomScale) || double.IsNaN(mapZoomScale))
                mapZoomScale = 0.1;

            mapZoomScale = System.Math.Abs(mapZoomScale);
            //// navigate to a geographic location 
            //var geoRect = new Rect(geoLocation.Longitude - geoMargin, geoLocation.Latitude - geoMargin, 2 * geoMargin, 2 * geoMargin);
            ////Rect windowRect = geoMap.GetZoomFromGeographic(geoRect);
            //var geoRegion = new GeoRegion(geoLocation, geoMargin);
            //var southEast = new Point(geoLocation.Longitude - geoMargin, geoLocation.Latitude + geoMargin);
            //var northWest = new Point(geoLocation.Longitude + geoMargin, geoLocation.Latitude - geoMargin);

            //var windowRect2 = geoMap.GetZoomFromGeographic(geoRegion.NorthWest.ToPoint(), geoRegion.SouthEast.ToPoint());
            //var windowRect = geoMap.GetZoomFromGeographic(northWest, southEast);

          


            var longitudeOffset = (180 * 2 * mapZoomScale) / 2;
            var latitudeOffset = (85 * 2 * mapZoomScale) / 2;


            var cartLocation = geoMap.GetWindowPoint(geoLocation.ToPoint());
            var cartNorthWest = new Point(cartLocation.X - 50, cartLocation.Y - 50);
            var cartSouthEast = new Point(cartLocation.X + 50, cartLocation.Y + 50);

            var w = cartSouthEast.X - cartNorthWest.X;
            var h = cartSouthEast.Y - cartNorthWest.Y;

            var cartRect = new Rect(cartNorthWest.X, cartNorthWest.Y, w, h);
            DebugManager.Log("cartRect " + cartRect);
            var geoRect = geoMap.GetGeographicFromZoom(cartRect);
            DebugManager.Log("geoRect  " + geoRect);
         

         
            var northWest = new Point(geoLocation.Longitude - longitudeOffset, geoLocation.Latitude + latitudeOffset);
            var southEast = new Point(geoLocation.Longitude + longitudeOffset, geoLocation.Latitude - latitudeOffset);

             

            var geoRegion44 = new GeoRegion(northWest, southEast);

            northWest = geoMap.GetGeographicPoint(cartNorthWest);
            southEast = geoMap.GetGeographicPoint(cartSouthEast);
            DebugManager.Log("cartNorthWest " + cartNorthWest + " cartSouthEast " + cartSouthEast);
            DebugManager.Log("geoNorthWest  " + northWest     + " geoSouthEast  " + southEast);
       

            var windowRect44 = geoMap.GetZoomFromGeographic(northWest, southEast);

            windowRect44 = geoMap.GetZoomFromGeographic(geoRect);

         
            DebugManager.Log("geoLocation " + geoLocation.ToPoint());
            DebugManager.Log("geoRegion44 " + geoRegion44.ToRect());
            //DebugManager.Log("geoRect4    " + geoRect4);
          //DebugManager.Log("windowRect4 " + windowRect4);
            DebugManager.Log("windowRect4 " + windowRect44);

            geoMap.WindowRect = windowRect44;
        }
        /// <summary>
        /// Zoom in the <see cref="XamGeographicMap"/> by specified zoom factor - percentage of the current window view rectangle
        /// </summary>
        public static void ZoomIn(this XamGeographicMap geoMap, double zoomScaleFactor = GeoMapGlobals.MapZoomScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double zoomScaleH = window.Height * zoomScaleFactor;
            double zoomScaleW = window.Width * zoomScaleFactor;
            //const double zoomWindowMin = 0.00005;

            window.Width = System.Math.Max(GeoMapGlobals.MapWindowMinZoom, window.Width - (2 * zoomScaleW));
            window.Height = System.Math.Max(GeoMapGlobals.MapWindowMinZoom, window.Height - (2 * zoomScaleH));

            //if (window.Width > GeoMapAdapter.MapWindowMinZoom) 
                window.X = System.Math.Min(1.0, window.X + zoomScaleW);
            //if (window.Height > GeoMapAdapter.MapWindowMinZoom) 
                window.Y = System.Math.Min(1.0, window.Y + zoomScaleH);

            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Zoom out the <see cref="XamGeographicMap"/> by specified zoom factor - percentage of the current window view rectangle
        /// </summary>
        public static void ZoomOut(this XamGeographicMap geoMap, double zoomScaleFactor = GeoMapGlobals.MapZoomScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double zoomScaleH = window.Height * zoomScaleFactor;
            double zoomScaleW = window.Width * zoomScaleFactor;
            //const double zoomWindowMax = 1.0;

            window.Width = System.Math.Min(GeoMapGlobals.MapWindowMaxZoom, window.Width + (2 * zoomScaleW));
            window.Height = System.Math.Min(GeoMapGlobals.MapWindowMaxZoom, window.Height + (2 * zoomScaleH));

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
            geoMap.NavigateTo(GeoMapGlobals.MapWindowMaxView);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in West direction by specified pan factor - percentage of the current window's width
        /// </summary>
        public static void NavigateWest(this XamGeographicMap geoMap, double panScaleFactor = GeoMapGlobals.MapPanScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double panScale = window.Width * panScaleFactor;

            window.X = System.Math.Max(0.0, window.X - panScale);
            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in East direction by specified pan factor - percentage of the current window's width
        /// </summary>
        public static void NavigateEast(this XamGeographicMap geoMap, double panScaleFactor = GeoMapGlobals.MapPanScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double panScale = window.Width * panScaleFactor;

            window.X = System.Math.Min(1.0 - window.Width, window.X + panScale);
            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in North direction by specified pan factor - percentage of the current window's height
        /// </summary>
        public static void NavigateNorth(this XamGeographicMap geoMap, double panScaleFactor = GeoMapGlobals.MapPanScaleFactor)
        {
            Rect window = geoMap.WindowRect;
            double panScale = window.Width * panScaleFactor;

            window.Y = System.Math.Max(0.0, window.Y - panScale);
            geoMap.NavigateTo(window);
        }
        /// <summary>
        /// Navigates the <see cref="XamGeographicMap"/> in South direction by specified pan factor - percentage of the current window's height
        /// </summary>
        public static void NavigateSouth(this XamGeographicMap geoMap, double panScaleFactor = GeoMapGlobals.MapPanScaleFactor)
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
            return rect.Center(); // new Point(x, y);
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
        public static Rect GetGeoRectFromShapePoints(IEnumerable<List<Point>> shapePoints)
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
        public static GeoRegion GetGeoRegionFromShapePoints(IEnumerable<List<Point>> shapePoints)
        {
            var geoRect = GetGeoRectFromShapePoints(shapePoints);
            return GeoRegion.FromRect(geoRect);
        }

        #region Calculation Methods for Geo Locations
        /// <summary>
        /// Calculates geographic location of mouse cursor over the XamGeographicMap
        /// </summary>
        public static GeoLocation GetGeoLocation(this XamGeographicMap geoMap, Point mousePosition)
        {
            var geoPoint = geoMap.GetGeographicPoint(mousePosition);
            return new GeoLocation(geoPoint);

            //var xAxis = geoMap.XAxis;
            //var yAxis = geoMap.YAxis;

            //var viewport = new Rect(0, 0, geoMap.ActualWidth, geoMap.ActualHeight);
            //var window = geoMap.WindowRect;

            //bool isInverted = xAxis.IsInverted;
            //var param = new ScalerParams(window, viewport, isInverted);
            //param.EffectiveViewportRect = geoMap.EffectiveViewport;
            //var longitude = xAxis.GetUnscaledValue(mousePosition.X, param);

            //isInverted = yAxis.IsInverted;
            //param = new ScalerParams(window, viewport, isInverted);
            //var latitude = yAxis.GetUnscaledValue(mousePosition.Y, param);

            //return new GeoLocation(longitude, latitude);
        }
        public static GeoPoint GetGeoPoint(this XamGeographicMap geoMap, Point mousePosition)
        {
            var point = geoMap.GetGeographicPoint(mousePosition);
            return new GeoPoint(point); //geoMap.GetGeoLocation(mousePosition).ToPoint();
        }
        public static double GetGeoLatitude(this XamGeographicMap geoMap, double mousePositionY)
        {
            return geoMap.GetGeoPoint(new Point(0, mousePositionY)).Longitude;
        }
        public static double GetGeoLongitude(this XamGeographicMap geoMap, double mousePositionX)
        {
            return geoMap.GetGeoPoint(new Point(mousePositionX, 0)).Longitude;
        }
        //TODO use GeoCalc
        //public static Vector GetGeoDistance(this XamGeographicMap geoMap, Point mousePoint1, Point mousePoint2)
        //{
        //    var geoPoint1 = geoMap.GetGeoPoint(mousePoint1);
        //    var geoPoint2 = geoMap.GetGeoPoint(mousePoint2);

        //    var distanceX = System.Math.Abs(geoPoint1.X - geoPoint2.X);
        //    var distanceY = System.Math.Abs(geoPoint1.Y - geoPoint2.Y);

        //    return new Vector(distanceX, distanceY);
        //}
        //TODO cleanup
        //public static double GetGeoDistanceX(this XamGeographicMap geoMap, Point mousePoint1, Point mousePoint2)
        //{
        //    return geoMap.GetGeoDistance(mousePoint1, mousePoint2).X;
        //}
        //public static double GetGeoDistanceY(this XamGeographicMap geoMap, Point mousePoint1, Point mousePoint2)
        //{
        //    return geoMap.GetGeoDistance(mousePoint1, mousePoint2).Y;
        //}
        //public static double GetGeoDistanceX(this XamGeographicMap geoMap, double mouseX1, double mouseX2)
        //{
        //    return geoMap.GetGeoDistanceX(new Point(mouseX1, 0), new Point(mouseX2, 0));
        //}
        //public static double GetGeoDistanceY(this XamGeographicMap geoMap, double mouseY1, double mouseY2)
        //{
        //    return geoMap.GetGeoDistanceY(new Point(0, mouseY1), new Point(0, mouseY2));
        //} 
        #endregion

        #region Calculation Methods for Map Postions
        public static Point GetMapPosition(this XamGeographicMap geoMap, Point geoPoint)
        {
            return geoMap.GetWindowPoint(geoPoint);

            //var xAxis = geoMap.XAxis;
            //var yAxis = geoMap.YAxis;

            //var viewport = new Rect(0, 0, geoMap.ActualWidth, geoMap.ActualHeight);
            //var window = geoMap.WindowRect;

            //bool isInverted = xAxis.IsInverted;
            //var param = new ScalerParams(window, viewport, isInverted);
            //param.EffectiveViewportRect = geoMap.EffectiveViewport;
            //var longitude = xAxis.GetScaledValue(geoPoint.X, param);

            //isInverted = yAxis.IsInverted;
            //param = new ScalerParams(window, viewport, isInverted);
            //param.EffectiveViewportRect = geoMap.EffectiveViewport; //added
            //var latitude = yAxis.GetScaledValue(geoPoint.Y, param);

            //return new Point(longitude, latitude);
        }
        public static Point GetMapPosition(this XamGeographicMap geoMap, GeoLocation geoLocation)
        {
            return geoMap.GetMapPosition(geoLocation.ToPoint());
        }
        public static double GetMapPositionX(this XamGeographicMap geoMap, double longitude)
        {
            return geoMap.GetMapPosition(new Point(longitude, 0)).X;
        }
        public static double GetMapPositionY(this XamGeographicMap geoMap, double latitude)
        {
            return geoMap.GetMapPosition(new Point(0, latitude)).Y;
        }

        public static Vector GetMapDistance(this XamGeographicMap geoMap, Point geoPoint1, Point geoPoint2)
        {
            var mapPoint1 = geoMap.GetMapPosition(geoPoint1);
            var mapPoint2 = geoMap.GetMapPosition(geoPoint2);

            var distanceX = System.Math.Abs(mapPoint1.X - mapPoint2.X);
            var distanceY = System.Math.Abs(mapPoint1.Y - mapPoint2.Y);

            return new Vector(distanceX, distanceY);
        }
       
        public static Vector GetMapDistance(this XamGeographicMap geoMap, GeoLocation geoPoint1, GeoLocation geoPoint2)
        {
            return geoMap.GetMapDistance(geoPoint1.ToPoint(), geoPoint2.ToPoint());
        }
        public static double GetMapDistanceX(this XamGeographicMap geoMap, GeoRegion geoRegion)
        {
            return geoMap.GetMapDistanceX(geoRegion.West, geoRegion.East);
        }
        public static double GetMapDistanceY(this XamGeographicMap geoMap, GeoRegion geoRegion)
        {
            return geoMap.GetMapDistanceX(geoRegion.South, geoRegion.North);
        }
        public static double GetMapDistanceX(this XamGeographicMap geoMap, double longitude1, double longitude2)
        {
            return geoMap.GetMapDistanceX(new Point(longitude1, 0), new Point(longitude2, 0));
        }
        public static double GetMapDistanceY(this XamGeographicMap geoMap, double latitude1, double latitude2)
        {
            return geoMap.GetMapDistanceY(new Point(0, latitude1), new Point(0, latitude2));
        }
        public static double GetMapDistanceX(this XamGeographicMap geoMap, Point geoPoint1, Point geoPoint2)
        {
            return geoMap.GetMapDistance(geoPoint1, geoPoint2).X;
        }
        public static double GetMapDistanceY(this XamGeographicMap geoMap, Point geoPoint1, Point geoPoint2)
        {
            return geoMap.GetMapDistance(geoPoint1, geoPoint2).Y;
        }
     

        #endregion
        #endregion

     }

    public class MapProjector
    {

        public MapProjector(XamGeographicMap geographicMap)
        {
            GeoMap = geographicMap;
        }
        public XamGeographicMap GeoMap;
       
        private static Rect _unitRect = new Rect(0, 0, 1, 1);
        public double GetScaledLongitude(double geoLongitude)
        {
            // init appropriate axis scaler parameters
            var xParams = GeoMap.GetLongitudeScaler();
            var scaledValue = GeoMap.XAxis.GetScaledValue(geoLongitude, xParams);
            return scaledValue;
        }
        public double GetScaledLatitude(double geoLatitude)
        {
            // init appropriate axis scaler parameters
            var yParams = GeoMap.GetLatitudeScaler();
            var scaledValue = GeoMap.YAxis.GetScaledValue(geoLatitude, yParams);
            return scaledValue;
        }
        public Point GetScaledLocation(Point geoLocation)
        {
            // init appropriate axis scaler parameters
            var x = GeoMap.GetScaledLongitude(geoLocation.X);
            var y = GeoMap.GetScaledLongitude(geoLocation.Y);
            var scaledLocation = new Point(x, y);
            return scaledLocation;
        }
        public Point GetScaledLocation(GeoLocation geoLocation)
        {
            // init appropriate axis scaler parameters
            var x = GeoMap.GetScaledLongitude(geoLocation.Longitude);
            var y = GeoMap.GetScaledLongitude(geoLocation.Latitude);
            var scaledLocation = new Point(x, y);
            return scaledLocation;
        }

        public ScalerParams GetLongitudeScaler()
        {
            var xaxis = GeoMap.XAxis;
            var xParams = new ScalerParams(_unitRect, GeoMap.ViewportRect, xaxis.IsInverted);
            xParams.EffectiveViewportRect = GeoMap.EffectiveViewport;
            return xParams;
        }
        public ScalerParams GetLatitudeScaler()
        {
            var yaxis = GeoMap.YAxis;
            var yParams = new ScalerParams(_unitRect, GeoMap.ViewportRect, yaxis.IsInverted);
            yParams.EffectiveViewportRect = GeoMap.EffectiveViewport;
            return yParams;
        }
    }
}