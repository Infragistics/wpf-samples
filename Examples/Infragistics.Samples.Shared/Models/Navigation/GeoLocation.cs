using System;
using System.Collections.ObjectModel;
using System.Windows;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.Models
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
        public GeoLocation(Point location)
        {
            this.Latitude = location.Y;
            this.Longitude = location.X;
        }
        public GeoLocation(double longitude, double latitude)
            : this(new Point(longitude, latitude))
        { }
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
            set {
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

    public class GeoLocationList : ObservableCollection<GeoLocation>
    {

    }
    public class GeoLocations
    {

        public static ObservableCollection<GeoLocation> Cities
        {
            get
            {
                var cities = new ObservableCollection<GeoLocation>
                {
                    CityTokyo, CityShanghai, CityMumbai, CityDelhi, CitySeoul, CitySedney, 
                    CityNewYork, CityLosAngeles, CityMexico, CityManila, CitySaoPaulo, 
                    CityWarsaw, CityLondon, CityBerlin, CityMoscow
                };
                return cities;
            }
        }
        // Europe
        public static GeoLocation CityWarsaw = new GeoLocation { Name = ModelStrings.XWM_Location_Warsaw, Latitude = 52.21, Longitude = 21 };
        public static GeoLocation CityLondon = new GeoLocation { Name = ModelStrings.XWM_Location_London, Latitude = 51.50, Longitude = 0.12 };
        public static GeoLocation CityBerlin = new GeoLocation { Name = ModelStrings.XWM_Location_Berlin, Latitude = 52.50, Longitude = 13.33 };
        public static GeoLocation CityMoscow = new GeoLocation { Name = ModelStrings.XWM_Location_Moscow, Latitude = 55.75, Longitude = 37.51 };
        public static GeoLocation CitySedney = new GeoLocation { Name = ModelStrings.XWM_Location_Sedney, Latitude = -33.83, Longitude = 151.2 };
        // Asia
        public static GeoLocation CityTokyo = new GeoLocation { Name = ModelStrings.XWM_Location_Tokyo, Latitude = 35.6895, Longitude = 139.6917 };
        public static GeoLocation CitySeoul = new GeoLocation { Name = ModelStrings.XWM_Location_Seoul, Latitude = 37.5665, Longitude = 126.9780 };
        public static GeoLocation CityDelhi = new GeoLocation { Name = ModelStrings.XWM_Location_Delhi, Latitude = 28.6353, Longitude = 77.2250 };
        public static GeoLocation CityMumbai = new GeoLocation { Name = ModelStrings.XWM_Location_Mumbai, Latitude = 19.0177, Longitude = 72.8562 };
        public static GeoLocation CityManila = new GeoLocation { Name = ModelStrings.XWM_Location_Manila, Latitude = 14.6010, Longitude = 120.9762 };
        public static GeoLocation CityShanghai = new GeoLocation { Name = ModelStrings.XWM_Location_Shanghai, Latitude = 31.2244, Longitude = 121.4759 };
        // Americas
        public static GeoLocation CityMexico = new GeoLocation { Name = ModelStrings.XWM_Location_MexicoCity, Latitude = 19.4270, Longitude = -99.1276 };
        public static GeoLocation CityNewYork = new GeoLocation { Name = ModelStrings.XWM_Location_NewYork, Latitude = 40.7561, Longitude = -73.9870 };
        public static GeoLocation CitySaoPaulo = new GeoLocation { Name = ModelStrings.XWM_Location_SaoPaulo, Latitude = -23.5489, Longitude = -46.6388 };
        public static GeoLocation CityLosAngeles = new GeoLocation { Name = ModelStrings.XWM_Location_LosAngeles, Latitude = 34.0522, Longitude = -118.2434 };

        public static GeoLocation WorldCenter = new GeoLocation { Name = "World Center", Latitude = 0, Longitude = 0 };
    }
}
