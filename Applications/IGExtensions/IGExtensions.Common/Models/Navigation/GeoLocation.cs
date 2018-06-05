using System.Collections.ObjectModel;
using System.Windows;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents a location in geographic coordinate system and provides methods for converting between different objects.
    /// <remarks>This is an ObservableModel object and all properties provide PropertyChanged notification </remarks>
    /// </summary>
    public class GeoLocation : ObservableModel, IGeoLocatable
    {
        #region Constructors
        public GeoLocation()
            : this(double.NaN, double.NaN)
        { }
        public GeoLocation(Point geoPoint)
            : this(geoPoint.X, geoPoint.Y)
        { }
        public GeoLocation(Point geoPoint, string label)
            : this(geoPoint.X, geoPoint.Y, label)
        { }
        public GeoLocation(double longitude, double latitude)
            : this(longitude, latitude, string.Empty)
        { }
        public GeoLocation(double longitude, double latitude, string label)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Label = label;
        }

        public void Update(double longitude, double latitude)
        {
            _longitude = longitude;
            _latitude = latitude;
        }
        #endregion

        #region Comparasion Methods
        public GeoLocation Min(GeoLocation other)
        {
            var point = this.ToPoint().Min(other.ToPoint());
            var location = new GeoLocation(point.X, point.Y);
            return location;
        }
        public GeoLocation Max(GeoLocation other)
        {
            var point = this.ToPoint().Max(other.ToPoint());
            var location = new GeoLocation(point.X, point.Y);
            return location;
        }
        /// <summary>
        /// Calculates minimum geographic location from two geographic location
        /// </summary>
        public static GeoLocation Min(GeoLocation location1, GeoLocation location2)
        {
            var location = location1.Min(location2);
            return location;
        }
        /// <summary>
        /// Calculates maximum geographic location from two geographic location
        /// </summary>
        public static GeoLocation Max(GeoLocation location1, GeoLocation location2)
        {
            var location = location1.Max(location2);
            return location;
        }
        /// <summary>Compares the location has the same Longitude and Latitude values with specified location </summary>
        public bool IsSameLocation(GeoLocation location)
        {
            return (this.Longitude == location.Longitude) &&
                   (this.Latitude == location.Latitude);
        }
        /// <summary>Compares the location has the same Longitude and Latitude values with specified geographic point </summary>
        public bool IsSameLocation(Point geoPoint)
        {
            return (this.Longitude == geoPoint.X) &&
                   (this.Latitude == geoPoint.Y);
        }
        #endregion

        #region Validation Methods
        //TODO move to GeoCalc
        ///// <summary> 
        ///// Checks if Latitude and Latitude values are within acceptable range
        ///// Latitude: -90 and 90, Longitude: -180 and 180 </summary>
        //public bool IsValidLocation()
        //{
        //    return IsValidLatitude() && IsValidLongitude();
        //}
        ///// <summary>Checks if Longitude value is within acceptable range: -180 and 180</summary>
        //public bool IsValidLongitude()
        //{
        //    return IsValidLongitude(this.Longitude);
        //}
        ///// <summary>Checks if Latitude value is within acceptable range: -90 and 90 </summary>
        //public bool IsValidLatitude()
        //{
        //    return IsValidLatitude(this.Latitude);
        //}
        ///// <summary>Checks if Longitude value is within acceptable range: -180 and 180</summary>
        //public static bool IsValidLongitude(double value)
        //{
        //    if (double.IsNaN(value) || double.IsInfinity(value)) return false;
        //    return value <= GeoGlobal.Worlds.LongitudeMax && value >= GeoGlobal.Worlds.LongitudeMin;
        //}
        ///// <summary>Checks if Latitude value is within acceptable range: -90 and 90 </summary>
        //public static bool IsValidLatitude(double value)
        //{
        //    if (double.IsNaN(value) || double.IsInfinity(value)) return false;
        //    return value <= GeoGlobal.Worlds.LatitudeMax && value >= GeoGlobal.Worlds.LatitudeMin;
        //}
        ///// <summary>Checks if the location is within World's actual region</summary>
        //public bool IsWithinActualWorld()
        //{
        //    return IsWithin(GeoGlobal.Worlds.ActualRect);
        //}
        ///// <summary>Checks if the location is within World's region in the SphericalMercator projection</summary>
        //public bool IsWithinSphericalMercatorWorld()
        //{
        //    return IsWithin(GeoGlobal.MapSphericalMercator.MapRect);
        //}
        /// <summary>Checks if the location is within specified region</summary>
        public bool IsWithin(Rect rect)
        {
            if (!rect.IsValid()) return false;
            return rect.Contains(this.ToPoint());
        }
        public bool ContainsLongitudeRange(double min, double max)
        {
            if (double.IsNaN(this.Longitude) || double.IsInfinity(this.Longitude)) return false;
            return this.Longitude <= max && this.Longitude >= min;
        }
        public bool ContainsLatitudeRange(double min, double max)
        {
            if (double.IsNaN(this.Latitude) || double.IsInfinity(this.Latitude)) return false;
            return this.Latitude <= max && this.Latitude >= min;
        }
        #endregion

        #region Properties
        public string Label { get; set; }
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
        public Point ToPoint()
        {
            return new Point(this.Longitude, this.Latitude);
        }
        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="GeoLocation"/> object where 
        /// Point.X is Longitude and Point.Y is Latitude
        /// </summary>
        public static GeoLocation FromPoint(Point coordinates)
        {
            return new GeoLocation { Latitude = coordinates.Y, Longitude = coordinates.X };
        }
        
        #endregion
        #region Operators
        ///// <summary>
        ///// check if left-hand-side and right-hand-side GeoLocation objects have the same Longitude and Latitude
        ///// </summary>
        ///// <param name="left"> left-hand-side GeoLocation </param>
        ///// <param name="right"> right-hand-side GeoLocation </param>
        ///// <returns></returns>
        //public static bool operator ==(GeoLocation left, GeoLocation right)
        //{
        //    //if (left.Longitude != right.Longitude) return false;
        //    //if (left.Latitude != right.Latitude) return false;
        //    //return true;
        //    return Object.Equals(left, right);
        //}
        ///// <summary>
        ///// check if left-hand-side and right-hand-side GeoLocation objects have different Longitude and Latitude
        ///// </summary>
        ///// <param name="left"> left-hand-side GeoLocation </param>
        ///// <param name="right"> right-hand-side GeoLocation </param>
        ///// <returns></returns>
        //public static bool operator !=(GeoLocation left, GeoLocation right)
        //{
        //    //if (left.Longitude != right.Longitude) return true;
        //    //if (left.Latitude != right.Latitude) return true;
        //    //return false;
        //    return !Object.Equals(left, right);
        //}
        ///// <summary>
        ///// Get HashCode for this object calculated using current values of Longitude and Latitude
        ///// </summary>
        ///// <returns></returns>
        //public override int GetHashCode()
        //{
        //    return (int)System.Math.Sqrt(System.Math.Pow(this.Longitude, 2) *
        //                          System.Math.Pow(this.Latitude, 2));
        //}
        ///// <summary>
        ///// check if this and the other GeoLocation objects have the same Longitude and Latitude
        ///// </summary>
        ///// <param name="other"></param>
        ///// <returns></returns>
        //public override bool Equals(object other)
        //{
        //    bool result = false;
        //    var that = other as GeoLocation;
        //    if (that != null)
        //        result = (this.Longitude == that.Longitude) &&
        //                 (this.Latitude == that.Latitude);
        //    return result;
        //}
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

        public static readonly GeoLocation Invalid = new GeoLocation(double.NaN, double.NaN);
        //public new string ToString()
        //{
        //    return this.ToPoint() + this.Name; ;
        //}
        //public override string ToString()
        //{
        //    return this.Name;
        //}
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
        public static GeoLocation Invalid = new GeoLocation { Name = "Invalid", Latitude = double.NaN, Longitude = double.NaN };
        // Europe
        public static GeoLocation CityWarsaw = new GeoLocation { Name = GeoStrings.XWM_Location_Warsaw, Latitude = 52.21, Longitude = 21 };
        public static GeoLocation CityLondon = new GeoLocation { Name = GeoStrings.XWM_Location_London, Latitude = 51.50, Longitude = 0.12 };
        public static GeoLocation CityBerlin = new GeoLocation { Name = GeoStrings.XWM_Location_Berlin, Latitude = 52.50, Longitude = 13.33 };
        public static GeoLocation CityMoscow = new GeoLocation { Name = GeoStrings.XWM_Location_Moscow, Latitude = 55.75, Longitude = 37.51 };
        public static GeoLocation CitySedney = new GeoLocation { Name = GeoStrings.XWM_Location_Sedney, Latitude = -33.83, Longitude = 151.2 };
        // Asia
        public static GeoLocation CityTokyo = new GeoLocation { Name = GeoStrings.XWM_Location_Tokyo, Latitude = 35.6895, Longitude = 139.6917 };
        public static GeoLocation CitySeoul = new GeoLocation { Name = GeoStrings.XWM_Location_Seoul, Latitude = 37.5665, Longitude = 126.9780 };
        public static GeoLocation CityDelhi = new GeoLocation { Name = GeoStrings.XWM_Location_Delhi, Latitude = 28.6353, Longitude = 77.2250 };
        public static GeoLocation CityMumbai = new GeoLocation { Name = GeoStrings.XWM_Location_Mumbai, Latitude = 19.0177, Longitude = 72.8562 };
        public static GeoLocation CityManila = new GeoLocation { Name = GeoStrings.XWM_Location_Manila, Latitude = 14.6010, Longitude = 120.9762 };
        public static GeoLocation CityShanghai = new GeoLocation { Name = GeoStrings.XWM_Location_Shanghai, Latitude = 31.2244, Longitude = 121.4759 };
        // Americas
        public static GeoLocation CityMexico = new GeoLocation { Name = GeoStrings.XWM_Location_MexicoCity, Latitude = 19.4270, Longitude = -99.1276 };
        public static GeoLocation CityNewYork = new GeoLocation { Name = GeoStrings.XWM_Location_NewYork, Latitude = 40.7561, Longitude = -73.9870 };
        public static GeoLocation CitySaoPaulo = new GeoLocation { Name = GeoStrings.XWM_Location_SaoPaulo, Latitude = -23.5489, Longitude = -46.6388 };
        public static GeoLocation CityLosAngeles = new GeoLocation { Name = GeoStrings.XWM_Location_LosAngeles, Latitude = 34.0522, Longitude = -118.2434 };

        public static GeoLocation WorldCenter = new GeoLocation { Name = "World Center", Latitude = 0, Longitude = 0 };
   
    }
}
