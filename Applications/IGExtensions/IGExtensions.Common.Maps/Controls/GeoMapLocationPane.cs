using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IGExtensions.Common.Models;
using IGExtensions.Common.Maps.Assets.Resources;
using IGExtensions.Framework;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.Controls
{
    /// <summary>
    /// Represents map location pane that provides a set of information about location of a mouse cursor in the <see cref="XamGeographicMap"/> control.  
    /// <para>Location can be displayed in different coordinate systems and formats such as:  </para>
    /// <para>Geographic Degree-Minute-Seconds, Geographic Decimal Degrees, Map Window Coordinates, or Map Mouse Coordinates </para> 
    /// </summary>
    public class GeoMapLocationPane : GeoMapPane 
    {
        public GeoMapLocationPane()
        {
            this.IsMapTrackingRestricted = false;
            this.MapRestrictionRegion = GeoRegions.WorldFullRegion;

            this.MapLatitude.SymbolPositive = MapStrings.GeoMapLocationPane_Symbol_N;
            this.MapLatitude.SymbolNegative = MapStrings.GeoMapLocationPane_Symbol_S;
            this.MapLongitude.SymbolPositive = MapStrings.GeoMapLocationPane_Symbol_E;
            this.MapLongitude.SymbolNegative = MapStrings.GeoMapLocationPane_Symbol_W;

            this.MapCoordinateDisplayMode = MapCoordinateDisplayMode.GeoDegreeMinuteSeconds;
            this.MapPaneOrientation = Orientation.Horizontal;
          
            DefaultStyleKey = typeof(GeoMapLocationPane);

            this.IsMovable = true;
        }
        
        #region Event Handlers 
        private void OnGeographicMapMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var mousePosition = e.GetPosition(this.MapControl);
            //var geoLocation = GetGeoLocation(this.MapControl, mousePosition);
            var geoLocation = this.MapControl.GetGeoLocation(mousePosition);

            DebugManager.LogData("GeoMapLocationPane.OnGeoMapMouseMoved: " + geoLocation);
            
            if (this.IsMapTrackingRestricted)
            {
                //this.Location = GeoLocations.Invalid;
                //this.Position = new Point(double.NaN, double.NaN);
                //this.CrosshairPoint = new Point(double.NaN, double.NaN);
                if (this.MapRestrictionRegion.Contains(geoLocation))
                {
                    var crosshairPoint = this.MapControl.CrosshairPoint;
                    this.MapLocation = geoLocation;
                    this.MapPosition = mousePosition;
                    this.MapCrosshairPoint = crosshairPoint; 
                }
            }
            else
            {
                var crosshairPoint = this.MapControl.CrosshairPoint;
                this.MapLocation = geoLocation;
                this.MapPosition = mousePosition;
                this.MapCrosshairPoint = crosshairPoint;    
            }
            
        }
       
        #endregion
      
        #region Methods
        //public static GeoLocation GetGeoLocation(XamGeographicMap geoMap, Point mousePosition)
        //{
        //    var location = new GeoLocation();

        //    if (geoMap != null)
        //    {
        //        var xAxis = geoMap.XAxis;
        //        var yAxis = geoMap.YAxis;

        //        var viewport = new Rect(0, 0, geoMap.ActualWidth, geoMap.ActualHeight);
        //        var window = geoMap.WindowRect;

        //        bool isInverted = xAxis.IsInverted;
        //        var param = new ScalerParams(window, viewport, isInverted);
        //        param.EffectiveViewportRect = geoMap.EffectiveViewport;
        //        var longitude = xAxis.GetUnscaledValue(mousePosition.X, param);

        //        isInverted = yAxis.IsInverted;
        //        param = new ScalerParams(window, viewport, isInverted);
        //        var latitude = yAxis.GetUnscaledValue(mousePosition.Y, param);

        //        location = new GeoLocation(longitude, latitude);
        //    }

        //    return location;
        //}
        #endregion

        #region Properties

        #region Apperance
 
        public const string MapCoordinateDisplayModePropertyName = "MapCoordinateDisplayMode";
        /// <summary>
        /// Identifies the MapCoordinateDisplayMode dependency property.
        /// </summary>
        public static readonly DependencyProperty MapCoordinateDisplayModeProperty =
            DependencyProperty.Register(MapCoordinateDisplayModePropertyName, typeof(MapCoordinateDisplayMode), typeof(GeoMapLocationPane),
             new PropertyMetadata(MapCoordinateDisplayMode.GeoDecimalDegrees, (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(MapCoordinateDisplayModePropertyName));
             })); 
        /// <summary>
        /// Gets or sets display mode of the Map Coordinates
        /// </summary>
        public MapCoordinateDisplayMode MapCoordinateDisplayMode
        {
            get { return (MapCoordinateDisplayMode)GetValue(MapCoordinateDisplayModeProperty); }
            set { SetValue(MapCoordinateDisplayModeProperty, value); }
        }
        #endregion
        #region Coordinates

        public const string MapPositionPropertyName = "MapPosition";
        /// <summary>
        /// Identifies the Position dependency property.
        /// </summary>
        public static readonly DependencyProperty MapPositionProperty =
            DependencyProperty.Register(MapPositionPropertyName, typeof(Point), typeof(GeoMapLocationPane),
             new PropertyMetadata(new Point(0, 0), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(MapPositionPropertyName));
             }));
        
        /// <summary>
        /// Gets or sets the position of mouse cursor in pixel coordinates
        /// </summary>
        public Point MapPosition
        {
            get { return (Point)GetValue(MapPositionProperty); }
            set { SetValue(MapPositionProperty, value); }
        }

        public const string MapCrosshairPointPropertyName = "MapCrosshairPoint";
        /// <summary>
        /// Identifies the Position dependency property.
        /// </summary>
        public static readonly DependencyProperty MapCrosshairPointProperty =
            DependencyProperty.Register(MapCrosshairPointPropertyName, typeof(Point), typeof(GeoMapLocationPane),
             new PropertyMetadata(new Point(0, 0), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(MapCrosshairPointPropertyName));
             }));
        /// <summary>
        /// Gets or sets the crosshair point of mouse cursor in map window coordinates
        /// </summary>
        public Point MapCrosshairPoint
        {
            get { return (Point)GetValue(MapCrosshairPointProperty); }
            set { SetValue(MapCrosshairPointProperty, value); }
        }

        public const string MapLocationPropertyName = "MapLocation";
        /// <summary>
        /// Identifies the Location dependency property.
        /// </summary>
        public static readonly DependencyProperty MapLocationProperty =
            DependencyProperty.Register(MapLocationPropertyName, typeof(GeoLocation), typeof(GeoMapLocationPane),
             new PropertyMetadata(new GeoLocation(0, 0), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(MapLocationPropertyName));
             }));
        /// <summary>
        /// Gets or sets the geographic Location of mouse cursor over the map
        /// </summary>
        public GeoLocation MapLocation
        {
            get { return (GeoLocation)GetValue(MapLocationProperty); }
            set { SetValue(MapLocationProperty, value); }
        }

        public const string MapLatitudePropertyName = "MapLatitude";
        /// <summary>
        /// Identifies the Latitude dependency property.
        /// </summary>
        public static readonly DependencyProperty MapLatitudeProperty =
            DependencyProperty.Register(MapLatitudePropertyName, typeof(GeoCoordinate), typeof(GeoMapLocationPane),
             new PropertyMetadata(new GeoCoordinate(GeoCoordinateType.Latitude), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(MapLatitudePropertyName));
             }));

        /// <summary>
        /// Gets or sets the Latitude coordinate of mouse cursor over the map
        /// </summary>
        public GeoCoordinate MapLatitude
        {
            get { return (GeoCoordinate)GetValue(MapLatitudeProperty); }
            set { SetValue(MapLatitudeProperty, value); }
        }

        public const string MapLongitudePropertyName = "MapLongitude";
        /// <summary>
        /// Identifies the Longitude dependency property.
        /// </summary>
        public static readonly DependencyProperty MapLongitudeProperty =
            DependencyProperty.Register(MapLongitudePropertyName, typeof(GeoCoordinate), typeof(GeoMapLocationPane),
             new PropertyMetadata(new GeoCoordinate(GeoCoordinateType.Longitude), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(MapLongitudePropertyName));
             }));
        /// <summary>
        /// Gets or sets the Longitude coordinate of mouse cursor over the map
        /// </summary>
        public GeoCoordinate MapLongitude
        {
            get { return (GeoCoordinate)GetValue(MapLongitudeProperty); }
            set { SetValue(MapLongitudeProperty, value); }
        } 
        #endregion

        public bool IsMapTrackingRestricted { get; set; }

        public GeoRegion MapRestrictionRegion { get; set; }
        
        public override void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (this.IsUpdating) return;
            
            switch (ea.PropertyName)
            {
                case MapControlPropertyName:
                    {
                        if (this.MapControl != null)
                            this.MapControl.MouseMove += OnGeographicMapMouseMove;

                        break;
                    }
                case MapPositionPropertyName:
                    {
                        DebugManager.LogData("GeoMapLocationPane.Updated: " + ea.PropertyName + " " + this.MapPosition);
           
                        break;
                    }
                case MapCoordinateDisplayModePropertyName:
                    {
                        break;
                    }
                #region properties Requiing Synchronization
                case MapLocationPropertyName:
                    {
                        //System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.Updated: " + MapLocationPropertyName + " " + this.MapLocation.ToPoint());
                        DebugManager.LogData("GeoMapLocationPane.Updated: " + ea.PropertyName + " " + this.MapLocation.ToPoint());
           
                        this.IsUpdating = true;

                        this.MapLongitude.Update(this.MapLocation.Longitude);
                        this.MapLatitude.Update(this.MapLocation.Latitude);

                        this.IsUpdating = false;
                        break;
                    }
                case MapLongitudePropertyName:
                    {
                        DebugManager.LogData("GeoMapLocationPane.Updated: " + ea.PropertyName + " " + this.MapLongitude.ToString());
                        //System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.Updated: " + MapLongitudePropertyName + " " + this.MapLongitude.ToString());
                        this.IsUpdating = true;
                        this.MapLocation = new GeoLocation(this.MapLongitude.Total, this.MapLocation.Latitude);

                        this.IsUpdating = false;
                        break;
                    }
                case MapLatitudePropertyName:
                    {
                        DebugManager.LogData("GeoMapLocationPane.Updated: " + ea.PropertyName + " " + this.MapLatitude.ToString());
                        
                        //System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.Updated: " + MapLatitudePropertyName + " " + this.MapLatitude.ToString());
                        this.IsUpdating = true;
                        this.MapLocation = new GeoLocation(this.MapLocation.Longitude, this.MapLatitude.Total);

                        this.IsUpdating = false;
                        break;
                    } 
                #endregion
              
                }
            base.OnPropertyChanged(ea);
        }
        #endregion
     
    }

    public class GeoCoordinate : ObservableModel
    {
        #region Constructors
        public GeoCoordinate(GeoCoordinateType type = GeoCoordinateType.Latitude)
            : this(0, 0, 0.0, type)
        { }

        public GeoCoordinate(int degrees, int minutes, double seconds, GeoCoordinateType type = GeoCoordinateType.Latitude)
        {
            this.IsPositiveCoordinate = !((degrees < 0) || (minutes < 0) || (seconds < 0)); 
            this.Degrees = System.Math.Abs(degrees);
            this.Minutes = System.Math.Abs(minutes);
            this.Seconds = System.Math.Abs(seconds);
            this.CoordinateType = type;
            this.Total = this.ConvertToDouble(); 
        }
        public GeoCoordinate(GeoCoordinate coordinate, GeoCoordinateType type = GeoCoordinateType.Latitude)
        {
            this.IsPositiveCoordinate = coordinate.IsPositiveCoordinate;
            this.Degrees = coordinate.Degrees;
            this.Minutes = coordinate.Minutes;
            this.Seconds = coordinate.Seconds;
            this.CoordinateType = type;
            this.Total = coordinate.Total;
        }
        public GeoCoordinate(double location, GeoCoordinateType type = GeoCoordinateType.Latitude)
            : this(GeoCoordinate.ConvertFromDouble(location), type)
        {}
        #endregion
        #region Properties
        public bool IsPositiveCoordinate
        {
            get { return _isPositive; }
            private set
            {
                if (_isPositive == value) return;
                _isPositive = value;
                OnPropertyChanged("IsPositiveCoordinate");
                OnPropertyChanged("Symbol");
            }
        }
        private bool _isPositive = true;

        public int Degrees
        {
            get { return _degrees; }
            set
            {
                if (_degrees == value) return;
                _degrees = value;
                OnPropertyChanged("Degrees");
                OnPropertyChanged("Symbol");
            }
        }
        private int _degrees = 0;

        public int Minutes
        {
            get { return _minutes; }
            set
            {
                if (_minutes == value) return;
                _minutes = value;
                OnPropertyChanged("Minutes");
                OnPropertyChanged("Symbol");
            }
        }
        private int _minutes = 0;

        public double Seconds
        {
            get { return _seconds; }
            private set
            {
                if (_seconds == value) return;
                _seconds = value;
                OnPropertyChanged("Seconds");
                OnPropertyChanged("Symbol");
            }
        }
        private double _seconds = 0;

        public double Total
        {
            get { return _total; }
            private set
            {
                if (_total == value) return;
                _total = value;
                OnPropertyChanged("Total");
                OnPropertyChanged("Symbol");
            }
        }
        private double _total = 0;

        public string Symbol
        {
            get
            {
                _symbol = this.IsPositiveCoordinate ? this.SymbolPositive : this.SymbolNegative;
                return _symbol;
            }
            private set
            {
                if (_symbol == value) return;
                _symbol = value;
                OnPropertyChanged("Symbol");
            }
        }
        private string _symbol = "N";

        public string SymbolNegative
        {
            get { return _symbolNegative; }
            set
            {
                if (_symbolNegative == value) return;
                _symbolNegative = value;
                OnPropertyChanged("SymbolNegative");
                OnPropertyChanged("Symbol");
            }
        }
        private string _symbolNegative = "S";

        public string SymbolPositive
        {
            get { return _symbolPositive; }
            set
            {
                if (_symbolPositive == value) return;
                _symbolPositive = value;
                OnPropertyChanged("SymbolPositive");
                OnPropertyChanged("Symbol");
            }
        }
        private string _symbolPositive = "N";
  
        public GeoCoordinateType CoordinateType
        {
            get { return _coordinateType; }
            private set
            {
                if (_coordinateType == value) return;
                _coordinateType = value;
                OnPropertyChanged("CoordinateType");
                OnPropertyChanged("Symbol");
            }
        }
        private GeoCoordinateType _coordinateType = GeoCoordinateType.Longitude;

        #endregion
        #region Methods
        public new string ToString()
        {
            var str = string.Empty;
            str += this.Symbol + " " + this.Degrees + " " + this.Minutes + " " + this.Seconds;
            return str;
        }
        public void Update(double location)
        {
            var newCoordinate = GeoCoordinate.ConvertFromDouble(location);
            this.IsPositiveCoordinate = newCoordinate.IsPositiveCoordinate;
            this.Degrees = newCoordinate.Degrees;
            this.Minutes = newCoordinate.Minutes;
            this.Seconds = newCoordinate.Seconds;
            this.Total = newCoordinate.Total;
        }
        public bool IsSouthCoordinate()
        {
            return !this.IsPositiveCoordinate && this.IsLatitudeCoordinate();
        }
        public bool IsNorthCoordinate()
        {
            return this.IsPositiveCoordinate && this.IsLatitudeCoordinate();
        }
        public bool IsWestCoordinate()
        {
            return !this.IsPositiveCoordinate && this.IsLongitudeCoordinate();
        }
        public bool IsEastCoordinate()
        {
            return this.IsPositiveCoordinate && this.IsLongitudeCoordinate();
        }
        public bool IsLongitudeCoordinate()
        {
            return this.CoordinateType == GeoCoordinateType.Longitude;
        }
        public bool IsLatitudeCoordinate()
        {
            return this.CoordinateType == GeoCoordinateType.Latitude;
        }

        public double ConvertToDouble()
        {
            var whole = this.Degrees;
            var fraction = ((this.Minutes * 60) + this.Seconds) / 3600;
            var location = whole + fraction;
            if (this.IsPositiveCoordinate) location *= -1;
            return location;
        }

        public static GeoCoordinate ConvertFromDouble(double location)
        {
            var sign = 1;
            if (location < 0) sign = -1;
            // calculate DMS on positive values
            var total = System.Math.Abs(location); 
            var degrees = (int)System.Math.Floor(total);
            var minutesTotal = (total - degrees) * 60;
            var minutes = (int)System.Math.Floor(minutesTotal);
            var seconds = (minutesTotal - minutes) * 60;
            // return GeoCoordinate with correct sign 
            return new GeoCoordinate(degrees, minutes, sign * seconds);
        }
        #endregion
    }
    public enum GeoCoordinateType
    {
        Longitude = 0,
        Latitude = 1
    }
    public enum MapCoordinateDisplayMode
    {
        GeoDegreeMinuteSeconds = 1,
        GeoDecimalDegrees = 2,
        MapWindowCoordinates = 3,
        MapMouseCoordinates = 4,

    }
    public class MapCoordinateDisplayModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is MapCoordinateDisplayMode))
            {
                return Visibility.Collapsed;
            }

            if (parameter == null || !(parameter is string))
            {
                return Visibility.Collapsed;
            }
             
            var mode = (MapCoordinateDisplayMode) value;
            string format = parameter.ToString();

            if (mode == MapCoordinateDisplayMode.GeoDecimalDegrees && (format.Contains("GDD") || format.Contains("GeoDecimalDegrees")))
            {
                return Visibility.Visible;
            }
            if (mode == MapCoordinateDisplayMode.GeoDegreeMinuteSeconds && (format.Contains("GDMS") || format.Contains("GeoDegreeMinuteSeconds")))
            {
                return Visibility.Visible;
            }
            if (mode == MapCoordinateDisplayMode.MapWindowCoordinates && (format.Contains("MWC") || format.Contains("MapWindowCoordinates")))
            {
                return Visibility.Visible;
            }
            if (mode == MapCoordinateDisplayMode.MapMouseCoordinates && (format.Contains("MMC") || format.Contains("MapMouseCoordinates")))
            {
                return Visibility.Visible;
            }


            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }

    }
  
    public class GeoCoordinateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var geoCoordinate = new GeoCoordinate();
            if (value == null)
            {
                return geoCoordinate;
            }
            if (value is int)
            {
                geoCoordinate = new GeoCoordinate((int)value);
            }
            if (value is double)
            {
                geoCoordinate = new GeoCoordinate((double)value);
            }
            return geoCoordinate;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 0;
            }
            if (value is GeoCoordinate)
            {
                return ((GeoCoordinate)value).ConvertToDouble();
            }

            return 0;
        }

    }

    public class GeoMapSymbolLocalizationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string format = parameter.ToString();

            if (format.Contains("JP"))
            {
                if (CultureInfo.CurrentUICulture.Name == "ja-JP")
                    return Visibility.Visible;
                if (CultureInfo.CurrentUICulture.Name != "ja-JP")
                    return Visibility.Collapsed;
            }

            if (format.Contains("EN"))
            {
                if (CultureInfo.CurrentUICulture.Name == "ja-JP")
                    return Visibility.Collapsed;
                if (CultureInfo.CurrentUICulture.Name != "ja-JP")
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }

    }

}