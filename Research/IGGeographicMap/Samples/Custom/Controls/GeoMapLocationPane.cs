using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using IGGeographicMap.Resources;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples.Custom
{
    /// <summary>
    /// Represents map location pane that provides a set of information about location of a mouse cursor in the <see cref="XamGeographicMap"/> control.  
    /// Location can be displayed in different coordinate systems and formats such as: 
    /// Geographic Degree-Minute-Seconds, Geographic Decimal Degrees, Map Window Coordinates, or Map Mouse Coordinates 
    /// </summary>
    public class GeoMapLocationPane : GeoMapPane 
    {
        public GeoMapLocationPane()
        {
            this.Latitude.SymbolPositive = MapStrings.GeoMapLocationPane_Symbol_N;
            this.Latitude.SymbolNegative = MapStrings.GeoMapLocationPane_Symbol_S;
            this.Longitude.SymbolPositive = MapStrings.GeoMapLocationPane_Symbol_E;
            this.Longitude.SymbolNegative = MapStrings.GeoMapLocationPane_Symbol_W;

            this.MapCoordinateDisplayMode = MapCoordinateDisplayMode.GeoDegreeMinuteSeconds;
            this.MapPaneOrientation = Orientation.Horizontal;
          
            DefaultStyleKey = typeof(GeoMapLocationPane);
            
            this.IsMovable = true;
        }
        
        #region Event Handlers 
        private void OnGeographicMapMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
            var mousePosition = e.GetPosition(this.GeographicMap);
          
            var geoLocation = GetGeoLocation(this.GeographicMap, mousePosition);

            System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.OnGeoMapMouseMoved: " + geoLocation);
            var crosshairPoint = this.GeographicMap.CrosshairPoint;

            this.Location = geoLocation;
            this.Position = mousePosition;
            this.CrosshairPoint = crosshairPoint;
        }
       
        #endregion
      
        #region Methods
        public static GeoLocation GetGeoLocation(XamGeographicMap geoMap, Point mousePosition)
        {
            var location = new GeoLocation();

            if (geoMap != null)
            {
                Point p = geoMap.GetGeographicPoint(mousePosition);
                location = new GeoLocation(p.X, p.Y);
            }

            return location;
        }
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
     
        public const string PositionPropertyName = "Position";
        /// <summary>
        /// Identifies the Position dependency property.
        /// </summary>
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.Register(PositionPropertyName, typeof(Point), typeof(GeoMapLocationPane),
             new PropertyMetadata(new Point(0, 0), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(PositionPropertyName));
             }));
        
        /// <summary>
        /// Gets or sets the position of mouse cursor in pixel coordinates
        /// </summary>
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public const string CrosshairPointPropertyName = "CrosshairPoint";
        /// <summary>
        /// Identifies the Position dependency property.
        /// </summary>
        public static readonly DependencyProperty CrosshairPointProperty =
            DependencyProperty.Register(CrosshairPointPropertyName, typeof(Point), typeof(GeoMapLocationPane),
             new PropertyMetadata(new Point(0, 0), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(CrosshairPointPropertyName));
             }));
        /// <summary>
        /// Gets or sets the crosshair point of mouse cursor in map window coordinates
        /// </summary>
        public Point CrosshairPoint
        {
            get { return (Point)GetValue(CrosshairPointProperty); }
            set { SetValue(CrosshairPointProperty, value); }
        }

        public const string LocationPropertyName = "Location";
        /// <summary>
        /// Identifies the Location dependency property.
        /// </summary>
        public static readonly DependencyProperty LocationProperty =
            DependencyProperty.Register(LocationPropertyName, typeof(GeoLocation), typeof(GeoMapLocationPane),
             new PropertyMetadata(new GeoLocation(0, 0), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(LocationPropertyName));
             }));
        /// <summary>
        /// Gets or sets the geographic Location of mouse cursor over the map
        /// </summary>
        public GeoLocation Location
        {
            get { return (GeoLocation)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }

        public const string LatitudePropertyName = "Latitude";
        /// <summary>
        /// Identifies the Latitude dependency property.
        /// </summary>
        public static readonly DependencyProperty LatitudeProperty =
            DependencyProperty.Register(LatitudePropertyName, typeof(GeoCoordinate), typeof(GeoMapLocationPane),
             new PropertyMetadata(new GeoCoordinate(GeoCoordinateType.Latitude), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(LatitudePropertyName));
             }));

        /// <summary>
        /// Gets or sets the Latitude coordinate of mouse cursor over the map
        /// </summary>
        public GeoCoordinate Latitude
        {
            get { return (GeoCoordinate)GetValue(LatitudeProperty); }
            set { SetValue(LatitudeProperty, value); }
        }

        public const string LongitudePropertyName = "Longitude";
        /// <summary>
        /// Identifies the Longitude dependency property.
        /// </summary>
        public static readonly DependencyProperty LongitudeProperty =
            DependencyProperty.Register(LongitudePropertyName, typeof(GeoCoordinate), typeof(GeoMapLocationPane),
             new PropertyMetadata(new GeoCoordinate(GeoCoordinateType.Longitude), (sender, e) =>
             {
                 (sender as GeoMapLocationPane).OnPropertyChanged(new PropertyChangedEventArgs(LongitudePropertyName));
             }));
        /// <summary>
        /// Gets or sets the Longitude coordinate of mouse cursor over the map
        /// </summary>
        public GeoCoordinate Longitude
        {
            get { return (GeoCoordinate)GetValue(LongitudeProperty); }
            set { SetValue(LongitudeProperty, value); }
        } 
        #endregion
  
        public override void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (this.IsUpdating) return;
            
            switch (ea.PropertyName)
            {
                case GeographicMapPropertyName:
                    {
                        if (this.GeographicMap != null)
                            this.GeographicMap.MouseMove += OnGeographicMapMouseMove;

                        break;
                    }
                case PositionPropertyName:
                    {
                        System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.Updated: " + PositionPropertyName + " " + this.Position);
                        break;
                    }
                case MapCoordinateDisplayModePropertyName:
                    {
                        break;
                    }
                #region properties Requiing Synchronization
                case LocationPropertyName:
                    {
                        System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.Updated: " + LocationPropertyName + " " + this.Location.ToPoint());
                        this.IsUpdating = true;

                        this.Longitude.Update(this.Location.Longitude);
                        this.Latitude.Update(this.Location.Latitude);

                        this.IsUpdating = false;
                        break;
                    }
                case LongitudePropertyName:
                    {
                        System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.Updated: " + LongitudePropertyName + " " + this.Longitude.ToString());
                        this.IsUpdating = true;
                        this.Location = new GeoLocation(this.Longitude.Total, this.Location.Latitude);

                        this.IsUpdating = false;
                        break;
                    }
                case LatitudePropertyName:
                    {
                        System.Diagnostics.Debug.WriteLine("GeoMapLocationPane.Updated: " + LatitudePropertyName + " " + this.Latitude.ToString());
                        this.IsUpdating = true;
                        this.Location = new GeoLocation(this.Location.Longitude, this.Latitude.Total);

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
            System.Diagnostics.Debug.WriteLine("CurrentCulture is: " + CultureInfo.CurrentUICulture.ToString());

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