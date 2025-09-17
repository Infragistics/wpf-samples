namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a location in the Geodetic system which uses Longitude (X), Latitude (Y) as coordinates
    /// </summary>
    public class GeoLocation : ObservableModel
    {
        #region Constructors
        public GeoLocation()
        {
            this.Latitude = double.NaN;
            this.Longitude = double.NaN;
        } 
        public GeoLocation(double longitude, double latitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
        public void Update(double longitude, double latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
        }
        #endregion

        #region Static

        public static double LongitudeMin = -180;
        public static double LongitudeMax = 180;
        public static double LatitudeMin = -90;
        public static double LatitudeMax = 90;

        #endregion

        #region Methods
        /// <summary> 
        /// Checks if the Latitude and the Latitude values are within acceptable range
        /// Latitude: -90 and 90, Longitude: -180 and 180 </summary>
        /// <returns></returns>
        public bool IsValidGeoLocation()
        {
            return IsValidGeoLatitude() && IsValidGeoLongitude();
        }

        public bool IsValidGeoLongitude()
        {
            return IsValidGeoLongitude(this.Longitude);
        }
        public bool IsValidGeoLatitude()
        {
            return IsValidGeoLatitude(this.Latitude);
        }

        /// <summary>
        /// Checks if the Longitude value is within acceptable range: -180 and 180
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidGeoLongitude(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) return false;
            return value <= LongitudeMax && value >= LongitudeMin;
        }

        /// <summary>
        /// Checks if the Latitude value is within acceptable range: -90 and 90
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidGeoLatitude(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value)) return false;
            return value <= LatitudeMax && value >= LatitudeMin;
        }
        #endregion

        #region Properties
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
                OnPropertyChanged("Longitude");
            }
        }
        private double _longitude = double.NaN;
        /// <summary>
        /// Gets or sets name for the GeoLocation
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value == null) return;
                if (value.Equals(_name)) return;
                _name = value.Trim();
                OnPropertyChanged("Name");
            }
        }
        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets description for the GeoLocation
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value == null) return;
                if (_description.Equals(value.Trim())) return;
                _description = value.Trim();
                OnPropertyChanged("Description");
            }
        }
        private string _description = string.Empty;


        /// <summary> 
        /// Gets or sets an address in the format: 
        /// Street Name, City, State/ZipCode, Country (Optional) </summary>
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if (value == null) return;
                if (value.Equals(_address)) return;
                _address = value.Trim();
                OnPropertyChanged("Address");
            }
        }
        private string _address = string.Empty;

        #endregion

        #region Converters
#if WPF || SILVERLIGHT
        /// <summary> 
        /// Converts this <see cref="GeoLocation"/> object to a <see cref="Point"/> where 
        /// Point.X is Longitude and Point.Y is Latitude
        /// </summary>
        /// <returns></returns>
        public Point ToPoint()
        {
            return new Point(this.Longitude, this.Latitude);
        } 
        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="GeoLocation"/> object where 
        /// Point.X is Longitude and Point.Y is Latitude
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        public static GeoLocation FromPoint(Point coordinates)
        {
            return new GeoLocation { Latitude = coordinates.Y, Longitude = coordinates.X };
        }
        
#endif
        #endregion

        #region Operators
        /// <summary>
        /// Add Longitude and Latitude values of left-hand-side and right-hand-side GeoLocation objects
        /// </summary>
        /// <param name="left"> left-hand-side GeoLocation </param>
        /// <param name="right"> right-hand-side GeoLocation </param>
        /// <returns></returns>
        public static GeoLocation operator +(GeoLocation left, GeoLocation right)
        {
            var temp = new GeoLocation
            {
                Longitude = left.Longitude + right.Longitude,
                Latitude = left.Latitude + right.Latitude
            };
            return temp;
        }
        /// <summary>
        /// Subtract Longitude and Latitude values of left-hand-side and right-hand-side GeoLocation objects
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static GeoLocation operator -(GeoLocation left, GeoLocation right)
        {
            var temp = new GeoLocation
            {
                Longitude = left.Longitude - right.Longitude,
                Latitude = left.Latitude - right.Latitude
            };
            return temp;
        }
        #endregion

        public override string ToString()
        {
            return this.Name;
        }
    }
}