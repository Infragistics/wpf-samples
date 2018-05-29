//using Infragistics.Controls.Maps;

using System.Collections.Generic;
//using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using IGExtensions.Common.Maps;
using IGExtensions.Common.Models;
using Infragistics.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps //Infragistics.Controls.Maps
{
    public enum TileImageryDisplayMode
    {
        MouseHover,
        MouseRightClick,
        MouseLeftClick,
        Always,
    }
    public enum TileImageryInteractionMode
    {
        MouseMove,
        MouseRightClick,
        MouseLeftClick,
        None,
    }

    public class GeoImageryPreviewSeries : GeographicTileSeries
    {
        public GeoImageryPreviewSeries()
        {
            this.PropertyChanged += OnSeriesPropertyChanged;
            //this.TileImageryCenterLocation.PropertyChanged += OnTileImageryCenterLocationChanged;
            //this.TileCartesianRegion.PropertyChanged += OnTileCartesianRegionChanged;
            this.TileImageryRegion.PropertyChanged += OnTileImageryRegionChanged;
        }

      

        private static readonly GeoRegion TileRegion = new GeoRegion(new Point(0, 0), 90, 60);
        private static readonly CartesianRegion CartesianRegion = new CartesianRegion(400, 200, 300, 200);
        private static readonly Size TileSize = new Size(90, 90);
        // SeriesViewer is GeographicMap
        protected XamGeographicMap GeographicMap { get { return this.SeriesViewer as XamGeographicMap; } }
        #region Properties

        public const string TileImageryUsesGeoProjectionPropertyName = "TileImageryUsesGeoProjection";
        public static readonly DependencyProperty TileImageryUsesGeoProjectionProperty = DependencyProperty.Register(
            TileImageryUsesGeoProjectionPropertyName, typeof(bool), typeof(GeoImageryPreviewSeries),
            new PropertyMetadata(true, (sender, e) =>
                ((GeoImageryPreviewSeries)sender).RaisePropertyChanged(TileImageryUsesGeoProjectionPropertyName, e.OldValue, e.NewValue)));
        /// <summary>
        /// Gets or sets the TileImageryUsesGeoProjection property 
        /// </summary>
        public bool TileImageryUsesGeoProjection
        {
            get { return (bool)GetValue(TileImageryUsesGeoProjectionProperty); }
            set { SetValue(TileImageryUsesGeoProjectionProperty, value); }
        }

        public const string TileImageryRectPropertyName = "TileImageryRect";
        public static readonly DependencyProperty TileImageryRectProperty = DependencyProperty.Register(
            TileImageryRectPropertyName, typeof(CartesianRegion), typeof(GeoImageryPreviewSeries),
            new PropertyMetadata(CartesianRegion, (sender, e) =>
                ((GeoImageryPreviewSeries)sender).RaisePropertyChanged(TileImageryRectPropertyName, e.OldValue, e.NewValue)));
        /// <summary>
        /// Gets or sets the TileImageryRect property 
        /// </summary>
        public CartesianRegion TileImageryRect
        {
            get { return (CartesianRegion)GetValue(TileImageryRectProperty); }
            set { SetValue(TileImageryRectProperty, value); }
        }


        public const string TileImageryRegionPropertyName = "TileImageryRegion";
        public static readonly DependencyProperty TileImageryRegionProperty = DependencyProperty.Register(
            TileImageryRegionPropertyName, typeof(GeoRegion), typeof(GeoImageryPreviewSeries),
            new PropertyMetadata(TileRegion, (sender, e) =>
                ((GeoImageryPreviewSeries)sender).RaisePropertyChanged(TileImageryRegionPropertyName, e.OldValue, e.NewValue)));
        /// <summary>
        /// Gets or sets the TileImageryRegion property 
        /// </summary>
        public GeoRegion TileImageryRegion
        {
            get { return (GeoRegion)GetValue(TileImageryRegionProperty); }
            set { SetValue(TileImageryRegionProperty, value); }
        }

       
         
        public const string TileImageryInteractionModePropertyName = "TileImageryInteractionMode";
        public static readonly DependencyProperty TileImageryInteractionModeProperty = DependencyProperty.Register(
            TileImageryInteractionModePropertyName, typeof(TileImageryInteractionMode), typeof(GeoImageryPreviewSeries),
            new PropertyMetadata(TileImageryInteractionMode.MouseRightClick, (sender, e) =>
                ((GeoImageryPreviewSeries)sender).RaisePropertyChanged(TileImageryInteractionModePropertyName, e.OldValue, e.NewValue)));
        /// <summary>
        /// Gets or sets the TileImageryInteractionMode property 
        /// </summary>
        public TileImageryInteractionMode TileImageryInteractionMode
        {
            get { return (TileImageryInteractionMode)GetValue(TileImageryInteractionModeProperty); }
            set { SetValue(TileImageryInteractionModeProperty, value); }
        }

        public const string TileImageryDisplayModePropertyName = "TileImageryDisplayMode";
        public static readonly DependencyProperty TileImageryDisplayModeProperty = DependencyProperty.Register(
            TileImageryDisplayModePropertyName, typeof(TileImageryDisplayMode), typeof(GeoImageryPreviewSeries),
            new PropertyMetadata(TileImageryDisplayMode.Always, (sender, e) =>
                ((GeoImageryPreviewSeries)sender).RaisePropertyChanged(TileImageryDisplayModePropertyName, e.OldValue, e.NewValue)));
        /// <summary>
        /// Gets or sets the TileImageryDisplayMode property 
        /// </summary>
        public TileImageryDisplayMode TileImageryDisplayMode
        {
            get { return (TileImageryDisplayMode)GetValue(TileImageryDisplayModeProperty); }
            set { SetValue(TileImageryDisplayModeProperty, value); }
        }
        public const string TileImageryExtentWidthPropertyName = "TileImageryExtentWidth";
        public static readonly DependencyProperty TileImageryExtentWidthProperty = DependencyProperty.Register(
            TileImageryExtentWidthPropertyName, typeof(double), typeof(GeoImageryPreviewSeries),
            new PropertyMetadata(160.0, (sender, e) =>
                ((GeoImageryPreviewSeries)sender).RaisePropertyChanged(TileImageryExtentWidthPropertyName, e.OldValue, e.NewValue)));
        /// <summary>
        /// Gets or sets the TileImageryExtentWidth property 
        /// </summary>
        public double TileImageryExtentWidth
        {
            get { return (double)GetValue(TileImageryExtentWidthProperty); }
            set { SetValue(TileImageryExtentWidthProperty, value); }
        }
        public const string TileImageryExtentHeightPropertyName = "TileImageryExtentHeight";
        public static readonly DependencyProperty TileImageryExtentHeightProperty = DependencyProperty.Register(
            TileImageryExtentHeightPropertyName, typeof(double), typeof(GeoImageryPreviewSeries),
            new PropertyMetadata(60.0, (sender, e) =>
                ((GeoImageryPreviewSeries)sender).RaisePropertyChanged(TileImageryExtentHeightPropertyName, e.OldValue, e.NewValue)));
        /// <summary>
        /// Gets or sets the TileImageryExtentHeight property 
        /// </summary>
        public double TileImageryExtentHeight
        {
            get { return (double)GetValue(TileImageryExtentHeightProperty); }
            set { SetValue(TileImageryExtentHeightProperty, value); }
        }
        #endregion
      

        private void OnSeriesPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case SeriesViewerPropertyName:
                    {
                        this.GeographicMap.MouseMove += OnGeographicMapMouseMove;
                        this.GeographicMap.MouseLeave += OnGeographicMapMouseLeave;
                        this.GeographicMap.MouseEnter += OnGeographicMapMouseEnter;
                        this.GeographicMap.MouseRightButtonDown += OnGeographicMapMouseRightDown;
                        this.GeographicMap.MouseRightButtonUp += OnGeographicMapMouseRightUp;
                        this.GeographicMap.MouseLeftButtonDown += OnGeographicMapMouseLeftButtonDown;
                        this.GeographicMap.MouseLeftButtonUp += OnGeographicMapMouseLeftButtonUp;
                        this.GeographicMap.Loaded += OnGeographicMapLoaded;
                        break;
                    }
                case TileImageryDisplayModePropertyName:
                    {
                        UpdateVisibility();
                        break;
                    }
                case TileImageryExtentHeightPropertyName:
                case TileImageryExtentWidthPropertyName:
                    {
                        if (_isMapLoaded && !_isSeriesUpdating)
                            UpdateImageryView(this.TileImageryRegion, true);

                        break;
                    }
                case TileImageryRegionPropertyName:
                    {
                        if (_isMapLoaded && !_isSeriesUpdating)
                            UpdateImageryView(this.TileImageryRegion, true);
                        break;
                    }
                case TileImageryUsesGeoProjectionPropertyName:
                    {
                        if (_isMapLoaded && !_isSeriesUpdating)
                        {
                            UpdateImageryView(this.TileImageryRegion, true);

                            if (this.TileImageryUsesGeoProjection)
                            {
                                UpdateImageryView(this.TileImageryRegion, true);
                            }
                            else
                            {
                                var southEast = this.TileImageryRegion.SouthEast;
                                var northEast = this.TileImageryRegion.NorthEast;
                                var centerPoint = this.GeographicMap.GetMapPosition(this.TileImageryRegion.Center);
                                var southEastPoint = this.GeographicMap.GetMapPosition(southEast);
                                var northEastPoint = this.GeographicMap.GetMapPosition(northEast);
                                var midPoint = southEastPoint.MidPoint(northEastPoint);

                                var mousePosition = new Point(centerPoint.X, midPoint.Y);

                                //TODO 
                                centerPoint = new Point(southEastPoint.X + (this.TileImageryExtentWidth / 2),
                                                        southEastPoint.Y + (this.TileImageryExtentHeight / 2));

                                //var deltaX = System.Math.Abs(southEastPoint.X - northEastPoint.X);
                                //var deltaY = System.Math.Abs(southEastPoint.Y - northEastPoint.Y);
                                //midPoint = new Point(southEastPoint.X + (deltaX / 2), southEastPoint.Y + (deltaY / 2));
      
                                UpdateImageryView(mousePosition, true);
                                //UpdateImageryView(this.TileImageryRegion, true);
                            }
                        }
                            
                        break;
                    }
            }
        }

        private void OnTileImageryRegionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (_isMapLoaded && !_isSeriesUpdating)
            {
                //System.Diagnostics.Debug.WriteLine("RegionChanged " + "  |   " + e.PropertyName + " " + this.TileImageryRegion.Center.ToString());
                UpdateImageryView(this.TileImageryRegion, true);
            }
        }
        private void OnGeographicMapLoaded(object sender, RoutedEventArgs e)
        {
            if (MousePosition.IsEmpty())
                MousePosition = this.GeographicMap.ViewportRect.Center();

            UpdateImageryView(MousePosition, true);
            //var seriesPosition = this.GeographicMap.GetMapPosition(new Point(0, 0));
            //UpdateImageryView(MousePosition, true);
            UpdateVisibility();
            _isMapLoaded = true;
           
        }
        #region Event Handlers
    
        private void OnGeographicMapMouseEnter(object sender, MouseEventArgs e)
        {
            _isMapMouseHover = true;
            UpdateVisibility();
        }
        private void OnGeographicMapMouseLeave(object sender, MouseEventArgs e)
        {
            _isMapMouseHover = false;
            _isMapMouseLeftClick = false;
            _isMapMouseRightClick = false;

            UpdateVisibility();
        }
        private void OnGeographicMapMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMapMouseLeftClick = true;

            MousePosition = e.GetPosition(this.GeographicMap);
            UpdateImageryView(MousePosition);

            //UpdateImageryCenterLocation(seriesPosition);

            //this.TileImageryCenterPosition = e.GetPosition(this.GeographicMap);
            //var seriesPosition = e.GetPosition(this.GeographicMap);
            //UpdateImageryView(seriesPosition);

            UpdateVisibility();
        }
        private void OnGeographicMapMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMapMouseLeftClick = false;
            MousePosition = e.GetPosition(this.GeographicMap);
            UpdateVisibility();
        }
        private void OnGeographicMapMouseRightDown(object sender, MouseButtonEventArgs e)
        {
            _isMapMouseRightClick = true;

            MousePosition = e.GetPosition(this.GeographicMap);
            UpdateImageryView(MousePosition);
            //this.TileImageryCenterPosition = e.GetPosition(this.GeographicMap);
            //UpdateImageryCenterLocation(seriesPosition);

            UpdateVisibility();
            //e.Handled = true;
        }
        private void OnGeographicMapMouseRightUp(object sender, MouseButtonEventArgs e)
        {
            _isMapMouseRightClick = false;

            MousePosition = e.GetPosition(this.GeographicMap);
            UpdateVisibility();
            //var series = this.GeographicMap.Series[GeographicImagerySeriesName] as GeographicTileSeries;
            //this.Visibility = _isImageryPreviewEnabled ? Visibility.Visible : Visibility.Collapsed;

            e.Handled = true;
        }
        private void OnGeographicMapMouseMove(object sender, MouseEventArgs e)
        {
            var mapPositio = e.GetPosition(this.GeographicMap);
            UpdateImageryView(mapPositio);

            //UpdateImageryCenterLocation(seriesPosition);

            //this.TileImageryCenterPosition = e.GetPosition(this.GeographicMap);
            //UpdateImageryView(seriesPosition);
        }

        #endregion

        private bool _isSeriesUpdating;
        private bool _isMapLoaded;
        private bool _isMapMouseHover;
        private bool _isMapMouseRightClick;
        private bool _isMapMouseLeftClick;
        protected Point MousePosition = new Point(double.NaN, double.NaN);
        private Point _tileImageryCenterPosition = new Point(0,0);
       
        #region Methods
        private bool IsUpdateRequired(bool forceUpdate = false)
        {
            if (this.TileImageryInteractionMode == TileImageryInteractionMode.MouseMove || forceUpdate ||
               (this.TileImageryInteractionMode == TileImageryInteractionMode.MouseRightClick && _isMapMouseRightClick) ||
               (this.TileImageryInteractionMode == TileImageryInteractionMode.MouseLeftClick && _isMapMouseLeftClick))
            {
                return true;
            }
            return false;
        }
     
        private void UpdateVisibility()
        {
            if (this.TileImageryDisplayMode == TileImageryDisplayMode.Always)
                this.Visibility = Visibility.Visible;
            else if (this.TileImageryDisplayMode == TileImageryDisplayMode.MouseHover && _isMapMouseHover)
                this.Visibility = _isMapMouseHover ? Visibility.Visible : Visibility.Collapsed;
            else if (this.TileImageryDisplayMode == TileImageryDisplayMode.MouseRightClick && _isMapMouseRightClick)
                this.Visibility = _isMapMouseRightClick ? Visibility.Visible : Visibility.Collapsed;
            else if (this.TileImageryDisplayMode == TileImageryDisplayMode.MouseLeftClick && _isMapMouseLeftClick)
                this.Visibility = _isMapMouseLeftClick ? Visibility.Visible : Visibility.Collapsed;
            else
                this.Visibility = Visibility.Collapsed;
        }
        //private void UpdateImageryCenterLocation(Point mousePosition, bool forceUpdate = false)
        //{
        //    if (this.TileImageryInteractionMode == TileImageryInteractionMode.MouseMove || forceUpdate ||
        //       (this.TileImageryInteractionMode == TileImageryInteractionMode.MouseRightClick && _isMapMouseRightClick) ||
        //       (this.TileImageryInteractionMode == TileImageryInteractionMode.MouseLeftClick && _isMapMouseLeftClick))
        //    {
        //        var geoLocation = this.GeographicMap.GetGeoLocation(mousePosition);
        //        if(!geoLocation.IsWithinSphericalMercatorWorld()) return;
                
        //        //if (!this.TileImageryCenterLocation.IsSameLocation(geoLocation))
        //        //{
        //        //    this.TileImageryCenterLocation.Longitude = geoLocation.Longitude;
        //        //    this.TileImageryCenterLocation.Latitude = geoLocation.Latitude;
        //        //}
        //    }
        //}
        private void UpdateImageryView(GeoRegion geoRegion, bool forceUpdate = false)
        {
            var mousePosition = this.GeographicMap.GetMapPosition(geoRegion.Center);
            //System.Diagnostics.Debug.WriteLine("Updating TileImageryRegion " + this.TileImageryRegion.CenterLongitude);
            UpdateImageryView(mousePosition, forceUpdate);
        }
       

        private void UpdateImageryView(Point mousePosition, bool forceUpdate = false)
        {
            if (IsUpdateRequired(forceUpdate))
            {
                var geoPoints = new List<Point>();
                
                double offsetX, offsetY;
                #region  Geo-Projection
                if (this.TileImageryUsesGeoProjection)
                {
                    //offsetX = this.GeographicMap.GetMapDistanceX(this.TileImageryRegion.East, this.TileImageryRegion.West);
                    //offsetY = this.GeographicMap.GetMapDistanceX(this.TileImageryRegion.North, this.TileImageryRegion.South);
                    var geoCenter = this.GeographicMap.GetGeoLocation(mousePosition);
                    //if (!geoLocation.IsWithinSphericalMercatorWorld()) return;

                    _isSeriesUpdating = true;

                    this.TileImageryRegion.UpdateLocation(geoCenter);

                    var southWestPoint = this.GeographicMap.GetMapPosition(this.TileImageryRegion.SouthWest);
                    var northEastPoint = this.GeographicMap.GetMapPosition(this.TileImageryRegion.NorthEast);

                    var x = southWestPoint.X;
                    var y = northEastPoint.Y;
                    var w = System.Math.Abs(northEastPoint.X - southWestPoint.X);
                    var h = System.Math.Abs(northEastPoint.Y - southWestPoint.Y);
                    this.TileImageryRect.Update(x, y, w, h);
                   
                    this.TileImageryExtentHeight = h;
                    this.TileImageryExtentWidth = w;

                    // System.Diagnostics.Debug.WriteLine("Updating TileImageryCenter " + this.TileImageryRegion.CenterLongitude);
                    _isSeriesUpdating = false;

                    geoPoints = this.TileImageryRegion.ToPoints();
                } 
                #endregion
                else
                {
                    _isSeriesUpdating = true;
                
                    var geoLocation = this.GeographicMap.GetGeoLocation(mousePosition);
                    //this.TileImageryRegion.Center = geoLocation;
                  
                    var southWestPoint = new Point(mousePosition.X - (this.TileImageryExtentWidth / 2),
                                                   mousePosition.Y - (this.TileImageryExtentHeight / 2));

                    var northEastPoint = new Point(mousePosition.X + (this.TileImageryExtentWidth / 2),
                                                   mousePosition.Y + (this.TileImageryExtentHeight / 2));

                    var southWest = this.GeographicMap.GetGeoLocation(southWestPoint);
                    var northEast = this.GeographicMap.GetGeoLocation(northEastPoint);

                    this.TileImageryRegion.Height = System.Math.Abs(northEast.Latitude - southWest.Latitude);
                    this.TileImageryRegion.Width = System.Math.Abs(northEast.Longitude - southWest.Longitude);
                    this.TileImageryRegion.UpdateLocation(geoLocation);
                   
                    _isSeriesUpdating = false;
                    
                    //offsetX = this.GeographicMap.GetMapDistanceX(this.TileImageryRegion.East / 2, this.TileImageryRegion.West / 2);
                    //offsetY = this.GeographicMap.GetMapDistanceX(this.TileImageryRegion.South / 2, this.TileImageryRegion.North / 2);
                    
                    ////offsetY = this.GeographicMap.GetMapDistanceX(this.TileImageryRegion.North, this.TileImageryRegion.South);
                    //var deltaS = System.Math.Abs(this.TileImageryRegion.CenterLatitude - this.TileImageryRegion.South);
                    //var deltaN = System.Math.Abs(this.TileImageryRegion.North - this.TileImageryRegion.CenterLatitude);
                    //var delta = System.Math.Max(deltaS, deltaN);
                    ////offsetY = this.GeographicMap.GetMapDistanceX(delta, this.TileImageryRegion.CenterLatitude);
                    ////delta = System.Math.Max(this.TileImageryRegion.South, this.TileImageryRegion.North);
                    //var mapPoint = this.GeographicMap.GetMapPosition(new Point(0, delta));

                    //delta = (this.TileImageryRegion.Height / 2);
                    //mapPoint = this.GeographicMap.GetMapPosition(new Point(0, geoLocation.Latitude + delta));
                    //// offsetY = mapPoint.Y;

                    // use TileImageryExtent values
                    offsetX = this.TileImageryExtentWidth / 2;
                    offsetY = this.TileImageryExtentHeight / 2;
                    
                    var cartesianPoints = new List<Point>();
                    cartesianPoints.Add(mousePosition.Shift(new Point(offsetX, offsetY)));
                    cartesianPoints.Add(mousePosition.Shift(new Point(offsetX, -offsetY)));
                    cartesianPoints.Add(mousePosition.Shift(new Point(-offsetX, -offsetY)));
                    cartesianPoints.Add(mousePosition.Shift(new Point(-offsetX, offsetY)));
                    //cartesianPoints.Add(mousePosition.Shift(new Point(offsetX, offsetY)));

                    foreach (var point in cartesianPoints)
                    {
                        geoPoints.Add(this.GeographicMap.GetGeographicPoint(point));
                    }
                    //var geoRegion = new GeoRegion(geoPoints);

                    //System.Diagnostics.Debug.WriteLine(geoLocation.ToString() + " - " + geoRegion.ToString() + " - " + TileImageryRegion.ToString());
                }
              
                var shapePoints = new List<List<Point>> { geoPoints };
                this.ShapeMemberPath = "";
                this.ItemsSource = shapePoints;
                //this.TileImageryRegion = geoRegion;
            }
        }
        #endregion
    }
}