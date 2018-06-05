using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// GeoCoordinateGrid is collection of vertical (Longitude) and horizontal (Latitude) lines in Geodetic coordinate system. 
    /// It also provides functionality for creating coordinate lines at specific intervals
    /// </summary>
    public class GeoCoordinateGrid : ObservableModel
    {
        public GeoCoordinateGrid() : this(30, 15)
        {
        }
        public GeoCoordinateGrid(double longLineInterval = 15, double latLineInterval = 15)
        {
            // create Longitude Lines in intervals equal longLineInterval
            _longitudeLines = GetLongitudeLines(longLineInterval);
            // create Latitude Lines in intervals equal latLineInterval
            _latitudeLines = GetLatitudeLines(latLineInterval);

        }
        #region Properties
        /// <summary>
        /// List of Geodetic Longitude (vertical) lines
        /// </summary>
        public List<GeoLongitudeLine> LongitudeLines
        {
            get
            {
                return _longitudeLines;
            }
            set
            {
                if (_longitudeLines == value) return;
                _longitudeLines = value;
                OnPropertyChanged("LongitudeLines");
            }
        }
        private List<GeoLongitudeLine> _longitudeLines = new List<GeoLongitudeLine>();

        /// <summary>
        /// List of Geodetic Latitude (horizontal) lines
        /// </summary>
        public List<GeoLatitudeLine> LatitudeLines
        {
            get
            {
                return _latitudeLines;
            }
            set
            {
                if (_latitudeLines == value) return;
                _latitudeLines = value;
                OnPropertyChanged("LatitudeLines");
            }
        }
        private List<GeoLatitudeLine> _latitudeLines = new List<GeoLatitudeLine>();

         
        public static GeoLatitudeLine TropicOfCapricornLatitude = new GeoLatitudeLine(-23);
        public static GeoLatitudeLine TropicOfCancerLatitude = new GeoLatitudeLine(23);
        #endregion

        #region Methods - Longitude
        /// <summary>
        /// Create of Geodetic Longitude (vertical) lines every 10 degrees
        /// </summary>
        /// <returns></returns>
        public static List<GeoLongitudeLine> GetLongitudeLines()
        {
            return GetLongitudeLines(10);
        }
        public static List<GeoLongitudeLine> GetLongitudeLines(double interval)
        {
            List<GeoLongitudeLine> lines = new List<GeoLongitudeLine>();
            for (double i = -180; i <= 180; i += interval)
            {
                lines.Add(new GeoLongitudeLine(i));
            }
            return lines;
        }
        public static List<GeoLongitudeLine> GetLongitudeLines(double interval, double min, double max)
        {
            List<GeoLongitudeLine> lines = new List<GeoLongitudeLine>();
            for (double i = min; i <= max; i += interval)
            {
                lines.Add(new GeoLongitudeLine(i));
            }
            return lines;
        }
        #endregion
        #region Methods - Latitude
        
        /// <summary>
        /// Create of Geodetic Latitude (horizontal) lines every 10 degrees
        /// </summary>
        /// <returns></returns>
        public static List<GeoLatitudeLine> GetLatitudeLines()
        {
            return GetLatitudeLines(10);
        }
        public static List<GeoLatitudeLine> GetLatitudeLines(double interval)
        {
            List<GeoLatitudeLine> lines = new List<GeoLatitudeLine>();
            for (double i = -90; i <= 90; i += interval)
            {
                lines.Add(new GeoLatitudeLine(i));
            }
            return lines;
        }
        public static List<GeoLatitudeLine> GetLatitudeLines(double interval, double min, double max)
        {
            List<GeoLatitudeLine> lines = new List<GeoLatitudeLine>();
            for (double i = min; i <= max; i += interval)
            {
                lines.Add(new GeoLatitudeLine(i));
            }
            return lines;
        }
        #endregion
    }
   
    /// <summary>
    /// GeoCoordinateLine is a line in Geodetic coordinate system between two GeoLocation objects
    /// </summary>
    public class GeoCoordinateLine : ObservableModel
    {
        #region Constructors
        public GeoCoordinateLine()
            : this(new GeoLocation { Longitude = -180, Latitude = 0 }, 
                   new GeoLocation { Longitude = 180, Latitude = 0 })
        { }
        public GeoCoordinateLine(Point origin, Point ending)
            : this(GeoLocation.FromPoint(origin), GeoLocation.FromPoint(ending))
        { }
        public GeoCoordinateLine(GeoLocation origin, GeoLocation ending)
        {
            _line = new Line {Stroke = new SolidColorBrush(Colors.Black), StrokeThickness = 0.5};
            this.Origin = origin;
            this.Ending = ending;
        }
        #endregion
        /// <summary>
        /// Gets or Sets the top-left corner (Origin) of GeoRegion
        /// </summary>
        public GeoLocation Origin { get { return _origins; } set { _origins = value; OnPropertyChanged("Origin"); } }
        private GeoLocation _origins;

        /// <summary>
        /// Gets or Sets the bottom-right corner (Ending) of GeoRegion
        /// </summary>
        public GeoLocation Ending { get { return _ending; } set { _ending = value; OnPropertyChanged("Ending"); } }
        private GeoLocation _ending;

        public Line Line { get { return _line; } set { _line = value; OnPropertyChanged("Line"); } }
        private Line _line;
        
    }

    /// <summary>
    /// GeoLongitudeLine is a vertical line with specific Longitude value in Geodetic coordinate system
    /// </summary>
    public class GeoLongitudeLine : GeoCoordinateLine
    {
        #region Constructors
        public GeoLongitudeLine()
            : this(0)
        { }

        public GeoLongitudeLine(double longitude)
        {
            CreateLongitudeLine(longitude);
        }
        public void CreateLongitudeLine(double longitude)
        {
            _longitude = longitude;
            this.Origin = new GeoLocation { Longitude = longitude, Latitude = -90 };
            this.Ending = new GeoLocation { Longitude = longitude, Latitude = 90 };
        }

        #endregion
        /// <summary> 
        /// Gets or sets the Longitude: location of a place on Earth 
        /// East (+) or West (-) of the prime meridian (Greenwich, in England), Range -180 to 180 </summary>
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                //if (!IsValidLongitude(value)) return;
                if (_longitude == value) return;
                _longitude = value;
                CreateLongitudeLine(_longitude);
    
                OnPropertyChanged("Longitude");
            }
        }
        private double _longitude = double.NaN;
   
    }
   
    /// <summary>
    /// GeoLatitudeLine is a horizontal line with specific Longitude value in Geodetic coordinate system
    /// </summary>
    public class GeoLatitudeLine : GeoCoordinateLine
    {
        #region Constructors
        public GeoLatitudeLine()
            : this(0)
        { }

        public GeoLatitudeLine(double latitude)
        {
            CreateLatitudeLine(latitude);
        }
        public void CreateLatitudeLine(double latitude)
        {
            _latitude = latitude;
            this.Origin = new GeoLocation { Longitude = -180, Latitude = latitude };
            this.Ending = new GeoLocation { Longitude = 180, Latitude = latitude };
        }

        #endregion
        /// <summary> 
        /// Gets or sets the Latitude: location of a place on Earth 
        /// North (+) or South (-) of the equator, Range -90 to 90 </summary>
        public double Latitude
        {
            get
            {
                return _latitude;
            }
            set
            {
                //if (!IsValidLatitude(value)) return;
                if (_latitude == value) return;
                _latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        private double _latitude = double.NaN;
     

    }
}