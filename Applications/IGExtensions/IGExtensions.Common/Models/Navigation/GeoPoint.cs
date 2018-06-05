using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace IGExtensions.Common.Models
{
   
    /// <summary>
    /// Represents a point in geographic coordinate system and provides methods for converting between different objects.
    /// </summary>
    public class GeoPoint : IGeoLocatable
    {
        #region Constructor
        public GeoPoint()
            : this(0, 0)
        { }
        public GeoPoint(Point geoPoint)
            : this(geoPoint.X, geoPoint.Y)
        { }
       
        public GeoPoint(double longitude, double latitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        } 
        #endregion
        public static readonly GeoPoint Invalid = new GeoPoint(double.NaN, double.NaN);

        public bool IsValid()
        {
            var isInvalid = double.IsNaN(this.Latitude) || double.IsNaN(this.Longitude) ||
                            double.IsInfinity(this.Latitude) || double.IsInfinity(this.Longitude);
            return !isInvalid;
        }
        public void Update(double longitude, double latitude)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
        }
        //TODO use GeoCalc
        //public double Distance(GeoPoint other)
        //{
        //    //TODO check if shortest path between points crosses D/T line
        //    //TODO and recalculate 
        //    var distance = this.ToPoint().Distance(other.ToPoint());
        //    return distance;
        //}
        //public double Angle(GeoPoint other)
        //{
        //    var angle = Vector(other).Angle;
        //    return angle;
        //}
        //public GeoVector Vector(GeoPoint other)
        //{
        //    var v = new GeoVector(this, other);
        //    return v;
        //}
        //public GeoPoint MidPoint(GeoPoint other)
        //{
        //    var point = this.ToPoint().MidPoint(other.ToPoint());
        //    return new GeoPoint(point);
        //}
       
        #region Comparasion Methods
        /// <summary>
        /// Calculates minimum geographic point from two geographic point
        /// </summary>
        public GeoPoint Min(GeoPoint other)
        {
            var point = this.ToPoint().Min(other.ToPoint());
            var location = new GeoPoint(point);
            return location;
        }
        /// <summary>
        /// Calculates maximum geographic point from two geographic point
        /// </summary>
        public GeoPoint Max(GeoPoint other)
        {
            var point = this.ToPoint().Max(other.ToPoint());
            var location = new GeoPoint(point);
            return location;
        }
        //public static GeoPoint Min(GeoPoint point1, GeoPoint point2)
        //{
        //    var minPoint = point1.ToPoint().Min(point2.ToPoint());
        //    var location = new GeoPoint(minPoint.X, minPoint.Y);
        //    return location;
        //}
        //public static GeoPoint Max(GeoPoint location1, GeoPoint location2)
        //{
        //    var maxPoint = location1.ToPoint().Max(location2.ToPoint());
        //    var location = new GeoPoint(maxPoint.X, maxPoint.Y);
        //    return location;
        //} 
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
        //    return value <= GeoGlobal.LongitudeMax && value >= GeoGlobal.LongitudeMin;
        //}
        ///// <summary>Checks if Latitude value is within acceptable range: -90 and 90 </summary>
        //public static bool IsValidLatitude(double value)
        //{
        //    if (double.IsNaN(value) || double.IsInfinity(value)) return false;
        //    return value <= GeoGlobal.LatitudeMax && value >= GeoGlobal.LatitudeMin;
        //}
        ///// <summary>Checks if the location is within World's actual region</summary>
        //public bool IsWithinActualWorld()
        //{
        //    return IsWithin(GeoGlobal.WorldActualRect);
        //}
        ///// <summary>Checks if the location is within World's region in the SphericalMercator projection</summary>
        //public bool IsWithinSphericalMercatorWorld()
        //{
        //    return IsWithin(GeoGlobal.WorldSphericalMercatorRect);
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

        #region Converters
       
        /// <summary> 
        /// Converts this <see cref="GeoPoint"/> object to a <see cref="Point"/> where 
        /// Point.X is Longitude and Point.Y is Latitude
        /// </summary>
        public Point ToPoint()
        {
            return new Point(this.Longitude, this.Latitude);
        }
        /// <summary>
        /// Converts a <see cref="Point"/> to a <see cref="GeoPoint"/> object where 
        /// Point.X is Longitude and Point.Y is Latitude
        /// </summary>
        public static GeoPoint FromPoint(Point point)
        {
            return new GeoPoint(point);
        }

        #endregion
        #region Properties

        public double Longitude { get; set; }
        public double Latitude { get; set; } 
        #endregion
    }

    public class GeoVector //: ObservableModel
    {
        #region Constructors
        public GeoVector(GeoPoint geoPoint)
            : this(geoPoint.Longitude, geoPoint.Latitude)
        { }
        public GeoVector(GeoPoint geoPoint1, GeoPoint geoPoint2)
        {
            var deltaX = geoPoint1.Longitude - geoPoint2.Longitude;
            var deltaY = geoPoint1.Latitude - geoPoint2.Latitude;
            _vector = new Vector(deltaX, deltaY);
        }
        public GeoVector(Point geoPoint)
            : this(geoPoint.X, geoPoint.Y)
        { }
        public GeoVector(Vector vector)
            : this(vector.X, vector.Y)
        { }
        public GeoVector(double longitude, double latitude)
        {
            _vector = new Vector(longitude, latitude);
        }
        #endregion
        #region Methods
        public double AngleBetween(GeoVector other)
        {
            return AngleBetween(this, other);
        }
        public static double AngleBetween(GeoVector lhs, GeoVector rhs)
        {
            return Vector.AngleBetween(lhs.Vector, rhs.Vector);
        } 
        #endregion
        #region Operators
        /// <summary>
        /// Addition
        /// </summary>
        public static GeoVector operator +(GeoVector lhs, GeoVector rhs)
        {
            return new GeoVector(lhs.Vector + rhs.Vector);
        }
        /// <summary>
        /// Subtraction
        /// </summary>
        public static GeoVector operator -(GeoVector lhs, GeoVector rhs)
        {
            return new GeoVector(lhs.Vector - rhs.Vector);
        }
        /// <summary>
        /// Multiplication
        /// </summary>
        public static GeoVector operator *(GeoVector lhs, double scalar)
        {
            return new GeoVector(lhs.Vector * scalar);
        }
        /// <summary>
        /// Multiplication
        /// </summary>
        public static GeoVector operator *(double scalar, GeoVector vector)
        {
            return new GeoVector((scalar * vector.Vector));
        }
        /// <summary>
        /// Division
        /// </summary>
        public static GeoVector operator /(GeoVector lhs, double scalar)
        {
            return new GeoVector(Vector.Divide(lhs.Vector, scalar));
        }
        #endregion
    
        #region Properties
        public static readonly GeoVector NorthVector = new GeoVector(0, 1);
        public static readonly GeoVector SouthVector = new GeoVector(0, -1);
        public static readonly GeoVector EastVector = new GeoVector(1, 0);
        public static readonly GeoVector WestVector = new GeoVector(-1, 0); 

        /// <summary>
        /// Gets Angle between this vector and <see cref="EastVector"/>
        /// </summary>
        public double Angle
        {
            get { return this.AngleBetween(EastVector); }
        }

        private Vector _vector;
        /// <summary>
        /// Gets Vector property 
        /// </summary>
        public Vector Vector
        {
            get {  return _vector; }
        }

        /// <summary>
        /// Gets or sets Longitude property 
        /// </summary>
        public double Longitude
        {
            get { return _vector.X; }
            set { if (_vector.X == value) return; _vector.X = value; }
        }
        /// <summary>
        /// Gets or sets Latitude property 
        /// </summary>
        public double Latitude
        {
            get { return _vector.Y; }
            set { if (_vector.Y == value) return; _vector.Y = value; }
        } 
        /// <summary>
        /// Gets Length property 
        /// </summary>
        public double Length
        {
            get { return _vector.Length; }
        }

        #endregion
        
    }
    public class GeoPointList : List<GeoPoint>
    {
        public GeoPointList()
        {
            
        }
        public List<Point> ToPoints()
        {
            return this.Select(point => point.ToPoint()).ToList();
        }

        public GeoPointList(IEnumerable<Point> points)
        {
            foreach (var point in points)
            {
                this.Add(new GeoPoint(point));    
            }
        }
        public static GeoPointList Empty = new GeoPointList();
    }

    
}