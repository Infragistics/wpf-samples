using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Math = System.Math;   // in case the IG Math library is referenced

namespace IGExtensions.Common.Models
{
    // calculator
    // tools:       http://www.gpsvisualizer.com/calculators
    // formulas:    http://www.movable-type.co.uk/scripts/latlong.html
    
    /// <summary>
    /// Provides formulas for calculating values between geographic objects
    /// <para><see cref="GeoPoint"/>, <see cref="GeoLocation"/>,  </para>
    /// </summary>
    public static class GeoCalculator
    {
        #region Geographic Bearing
        /// <summary>
        /// Calculates angle bearing from origin location towards destination location, in degrees
        /// <para> North = 0, East = 90, South = 180, West = 270 </para>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        public static double GetBearingAppx(IGeoLocatable origin, IGeoLocatable destination)
        {
            // from flight watcher app (not accurate)
            var dLat = destination.Latitude - origin.Latitude;
            var dLon = destination.Longitude - origin.Longitude;

            var angle = Math.Atan2((dLat), (dLon)) * 180.0 / Math.PI;

            return -1 * angle;
            //return angle;
        }
       
        /// <summary>
        /// Calculates the initial bearing from origin location in direction of destination location, in degrees from true North
        /// <para> North = 0, East = 90, South = 180, West = 270 </para>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        public static double GetBearing(IGeoLocatable origin, IGeoLocatable destination)
        {
            origin = origin.ToRadians();
            destination = destination.ToRadians();

            var dLat = (destination.Latitude - origin.Latitude);
            var dLon = (destination.Longitude - origin.Longitude);
            var y = Math.Sin(dLon) * Math.Cos(destination.Latitude);
            var x = Math.Cos(origin.Latitude) * Math.Sin(destination.Latitude) -
                    Math.Sin(origin.Latitude) * Math.Cos(destination.Latitude) * Math.Cos(dLon);
            var angle = Math.Atan2(y, x);
            return angle.ToDegreesNormalized();
        }
       
        /// <summary>
        /// Calculates the final bearing at destination location from origin geographic, in degrees from true North
        /// <para> North = 0, East = 90, South = 180, West = 270 </para>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        public static double GetBearingFinal(IGeoLocatable origin, IGeoLocatable destination)
        {
            var angleInital = GetBearing(destination, origin); // reversed intentionally
            var angleFinal = angleInital.ToDegreesReversed();
            return angleFinal;
        }
        
        /// <summary>
        /// Calculates the final bearing at destination location from origin geographic, in degrees from true North
        /// <para> North = 0, East = 90, South = 180, West = 270 </para>
        /// </summary>
        /// <param name="origin">location in geographic degrees </param>
        /// <param name="bearing">bearing in geographic degrees from origin</param>
        /// <param name="distance">distance in km from origin</param>
        /// <param name="radius">radius in km</param>
        /// <remarks>radius defaults to Earth's mean radius</remarks>
        public static double GetBearingFinal(IGeoLocatable origin, double bearing, double distance, double radius = GeoGlobal.Earths.Radius)
        {
            var destination = GetDestination(origin, bearing, distance, radius);
            var angleFinal = GetBearingFinal(origin, destination);
            return angleFinal;
        }
        
        #endregion
        #region Geographic Locations
        /// <summary>
        /// Calculates the destination location at distance and in direction of bearing from origin location 
        /// <para>Using the Spherical law of cosines </para>
        /// </summary>
        /// <param name="origin">location in geographic degrees </param>
        /// <param name="bearing">bearing in geographic degrees from origin</param>
        /// <param name="distance">distance in km</param>
        /// <param name="radius">radius in km</param>
        /// <remarks>radius defaults to Earth's mean radius</remarks>
        public static GeoPoint GetDestination(IGeoLocatable origin, double bearing, double distance, double radius = GeoGlobal.Earths.Radius)
        {
            origin = origin.ToRadians();
            bearing = bearing.ToRadians();
            distance = distance / radius; // angular distance in radians
            
            var latitude = Math.Asin(Math.Sin(origin.Latitude) * Math.Cos(distance) +
                           Math.Cos(origin.Latitude) * Math.Sin(distance) * Math.Cos(bearing));
            var x = Math.Sin(bearing) * Math.Sin(distance) * Math.Cos(origin.Latitude);
            var y = Math.Cos(distance) - Math.Sin(origin.Latitude) * Math.Sin(origin.Latitude);
            var longitude = origin.Longitude + Math.Atan2(x, y);
            longitude = (longitude + 3 * Math.PI) % (2 * Math.PI) - Math.PI;  // normalize to -180..+180º

            var destination = new GeoPoint(longitude.ToDegrees(), latitude.ToDegrees());
            return destination;
        }
         
        /// <summary>
        /// Calculates the maximum latitude of a great circle path from origin location in direction of bearing angle
        /// <para>using Clairaut’s formula</para>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="bearing">bearing from origin in geographic degrees</param>
        public static double GetLatitudeMax(IGeoLocatable origin, double bearing)
        {
            origin = origin.ToRadians();
            bearing = bearing.ToRadians();
            var latMax = Math.Acos(Math.Abs(Math.Sin(bearing) * Math.Cos(origin.Latitude)));
            return latMax;
        }
         
        /// <summary>
        /// Calculates intersection point of paths from two geographic locations
        /// </summary>
        /// <param name="origin1">origin of first location in geographic degrees</param>
        /// <param name="origin2">origin of second location in geographic degrees</param>
        /// <param name="bearing1">bearing from first location in geographic degrees</param>
        /// <param name="bearing2">bearing from second location in geographic degrees</param>
        /// <param name="radius">radius of a geographic sphere, in kilometers</param>
        /// <remarks>radius defaults to Earth's mean radius</remarks>
        public static GeoPoint GetIntersection(
            IGeoLocatable origin1, double bearing1,
            IGeoLocatable origin2, double bearing2, double radius = GeoGlobal.Earths.Radius)
        {
            origin1 = origin1.ToRadians();
            origin2 = origin2.ToRadians();
            var brng13 = bearing1.ToRadians();
            var brng23 = bearing2.ToRadians();
            var dLat = (origin2.Latitude - origin1.Latitude);
            var dLon = (origin2.Longitude - origin1.Longitude);

            var dist12 = 2 * Math.Asin(Math.Sqrt(Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                             Math.Cos(origin1.Latitude) * Math.Cos(origin2.Latitude) * 
                             Math.Sin(dLon / 2) * Math.Sin(dLon / 2)));
            if (dist12 == 0) return GeoPoint.Invalid;

            // initial/final bearings between points
            var brngA = Math.Acos((Math.Sin(origin2.Latitude) - Math.Sin(origin1.Latitude) * Math.Cos(dist12)) /
                       (Math.Sin(dist12) * Math.Cos(origin1.Latitude)));
            if (double.IsNaN(brngA)) brngA = 0;  // protect against rounding

            var brngB = Math.Acos((Math.Sin(origin1.Latitude) - Math.Sin(origin2.Latitude) * Math.Cos(dist12)) /
                       (Math.Sin(dist12) * Math.Cos(origin2.Latitude)));

            double brng12, brng21;
            if (Math.Sin(dLon) > 0)
            {
                brng12 = brngA;
                brng21 = 2 * Math.PI - brngB;
            }
            else
            {
                brng12 = 2 * Math.PI - brngA;
                brng21 = brngB;
            }
            var alpha1 = (brng13 - brng12 + Math.PI) % (2 * Math.PI) - Math.PI;  // angle 2-1-3
            var alpha2 = (brng21 - brng23 + Math.PI) % (2 * Math.PI) - Math.PI;  // angle 1-2-3

            if (Math.Sin(alpha1) == 0 && Math.Sin(alpha2) == 0) return GeoPoint.Invalid;  // infinite intersections
            if (Math.Sin(alpha1) * Math.Sin(alpha2) < 0) return GeoPoint.Invalid;         // ambiguous intersection

            var alpha3 = Math.Acos(-Math.Cos(alpha1) * Math.Cos(alpha2) +
                         Math.Sin(alpha1) * Math.Sin(alpha2) * Math.Cos(dist12));
            var dist13 = Math.Atan2(Math.Sin(dist12)*Math.Sin(alpha1)*Math.Sin(alpha2),
                         Math.Cos(alpha2) + Math.Cos(alpha1)*Math.Cos(alpha3));
            var lat3 = Math.Asin(Math.Sin(origin1.Latitude) * Math.Cos(dist13) +
                       Math.Cos(origin1.Latitude) * Math.Sin(dist13) * Math.Cos(brng13));
            var dLon13 = Math.Atan2(Math.Sin(brng13) * Math.Sin(dist13) * Math.Cos(origin1.Latitude),
                         Math.Cos(dist13) - Math.Sin(origin1.Latitude) * Math.Sin(lat3));

            var lon3 = origin1.Longitude + dLon13;
            lon3 = (lon3 + 3 * Math.PI) % (2 * Math.PI) - Math.PI;  // normalize to -180..+180º

            return new GeoPoint(lat3.ToDegrees(), lon3.ToDegrees());
        }
       
        /// <summary>
        /// Calculates mid point between two geographic locations on the Great Circle  
        /// <para>Using the Spherical law of cosines </para>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        public static GeoPoint GetMidpoint(IGeoLocatable origin, IGeoLocatable destination)
        {
            origin = origin.ToRadians();
            destination = destination.ToRadians();
            var dLat = (destination.Latitude - origin.Latitude);
            var dLon = (destination.Longitude - origin.Longitude);
          
            var bx = Math.Cos(destination.Latitude) * Math.Cos(dLon);
            var by = Math.Cos(destination.Latitude) * Math.Sin(dLon);
            var lat = Math.Atan2(Math.Sin(origin.Latitude) + Math.Sin(destination.Latitude),
                                 Math.Sqrt((Math.Cos(origin.Latitude) + bx) * (Math.Cos(origin.Latitude) + bx) + by * by));
            var lon = origin.Longitude + Math.Atan2(by, Math.Cos(origin.Latitude) + bx);
            lon = (lon + 3 * Math.PI) % (2 * Math.PI) - Math.PI; // normalize to -180..+180º

            return new GeoPoint(lon.ToDegrees(), lat.ToDegrees());
        }
       
        #endregion
        #region Geographic Distance
        /// <summary>
        /// Calculates distance between two geographic locations on the Great Circle   
        /// <para>Using the Haversine formula</para>
        /// <remarks>"Virtues of the Haversine" by R. W. Sinnott, Sky and Telescope, vol 68, no 2, 1984</remarks>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        /// <param name="radius">radius of a geographic sphere, in kilometers</param>
        /// <remarks>radius defaults to Earth's mean radius</remarks>
        public static GeoDistance GetDistanceH(IGeoLocatable origin, IGeoLocatable destination, double radius = GeoGlobal.Earths.Radius)
        {
            origin = origin.ToRadians();
            destination = destination.ToRadians();

            var dLat = (destination.Latitude - origin.Latitude);
            var dLon = (destination.Longitude - origin.Longitude);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) *
                    Math.Cos(origin.Latitude) * Math.Cos(destination.Latitude);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = radius * c;
            return new GeoDistance { Kilometers = distance };
        }
        
        /// <summary>
        /// Calculates distance between two geographic locations on the Great Circle  
        /// <para>Using the Spherical law of cosines </para>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        /// <param name="radius">radius of a geographic sphere, in kilometers</param>
        /// <remarks>radius defaults to Earth's mean radius</remarks>
        public static GeoDistance GetDistance(IGeoLocatable origin, IGeoLocatable destination, double radius = GeoGlobal.Earths.Radius)
        {
            origin = origin.ToRadians();
            destination = destination.ToRadians();
            var sinProd = Math.Sin(origin.Latitude) * Math.Sin(destination.Latitude);
            var cosProd = Math.Cos(origin.Latitude) * Math.Cos(destination.Latitude);
            var dLon = (destination.Longitude - origin.Longitude);
            
            var angle = Math.Acos(sinProd + cosProd * Math.Cos(dLon));
            var distance = angle * radius;
            return new GeoDistance { Kilometers = distance };
        }
        
        /// <summary>
        /// Calculates distance between two geographic locations on equirectangular map projection  
        /// <para>Using Pythagoras’ theorem </para>
        /// </summary>
        public static GeoDistance GetDistanceAppx(IGeoLocatable origin, IGeoLocatable destination, double radius = GeoGlobal.Earths.Radius)
        {
            origin = origin.ToRadians();
            destination = destination.ToRadians();
            var dLat = (destination.Latitude - origin.Latitude);
            var dLon = (destination.Longitude - origin.Longitude);

            var x = (dLon) * Math.Cos((origin.Latitude + destination.Latitude) / 2);
            var y = (dLat);
            var distance = Math.Sqrt(x * x + y * y) * radius;
            return new GeoDistance { Kilometers = distance };
            //return distance;
       
            //var yMin = Math.Min(origin.Latitude, destination.Latitude);
            //var yMax = Math.Max(origin.Latitude, destination.Latitude);
            //var xMin = Math.Min(origin.Longitude, destination.Longitude);
            //var xMax = Math.Max(origin.Longitude, destination.Longitude);
            //var yDelta = (yMax - yMin) * (yMax - yMin);
            //var xDelta = (xMax - xMin) * (xMax - xMin);

            //var distance = Math.Sqrt(xDelta + yDelta);
            //return new GeoDistance { Degrees = distance };
        }
       
        /// <summary>
        /// Calculates radius of a geographic sphere at a given latitude
        /// </summary>
        /// <param name="latitude">latitude in geographic degrees</param>
        /// <param name="radius">radius of a geographic sphere, in kilometers</param>
        /// <remarks>radius defaults to Earth's equatorial radius</remarks>
        public static double GetRadiusAppx(double latitude, double radius = GeoGlobal.Earths.RadiusEquatorial)
        {
            return radius - 21 * Math.Sin(latitude.ToRadians());
        }
        //public static double GetRadius(double latitude, double radius = GeoGlobal.Earths.RadiusEquatorial)
        //{
        //    latitude = latitude.ToRadians();
        //    var a = GeoGlobal.Earths.RadiusEquatorial;
        //    var b = GeoGlobal.Earths.RadiusPolar;
        //    var e = Math.Pow(b*b/a*a, 1/2);
        //    return a * (1.0 - e * e) / (1.0 - e * 2 * Math.Sin ^ 2(latitude)) ^ (3 / 2);
        //}
        #endregion
        #region Geographic Path

        /// <summary>
        /// Calculates geographic path between two geographic locations on the Great Circle 
        /// <para>Using the Spherical law of cosines </para>
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        /// <param name="interval">interval between consecutive points of the geographic path, in kilometers</param>
        /// <param name="radius">radius of a geographic sphere, in kilometers</param>
        public static GeoPointList GetPathPoints(IGeoLocatable origin, IGeoLocatable destination, double interval = GeoGlobal.Earths.CircumferenceOneDegree, double radius = GeoGlobal.Earths.Radius)
        {
            var path = new GeoPointList();
            var distance = GeoCalculator.GetDistance(origin, destination);
            if (double.IsNaN(distance.Kilometers))
            {
                return path; 
            }

            if (distance.Kilometers <= interval)
            {
                path.Add(origin.ToGeoPoint());
                path.Add(destination.ToGeoPoint());
            }
            else
            {
                var currentPoint = origin.ToGeoPoint();
                for (double dist = interval; dist <= distance.Kilometers; dist += interval)
                {
                    path.Add(currentPoint);
                    var bearing = GeoCalculator.GetBearing(currentPoint, destination);
                    currentPoint = GeoCalculator.GetDestination(currentPoint, bearing, interval);
                }
                path.Add(destination.ToGeoPoint());
            }
            return path; 
        }
        public static GeoPoint GetPathLocation(IGeoLocatable origin, IGeoLocatable destination, double distance, double interval = GeoGlobal.Earths.CircumferenceOneDegree, double radius = GeoGlobal.Earths.Radius)
        {
            interval = interval / 6;
            var currentPoint = origin.ToGeoPoint();
            //var previousPoint = origin.ToGeoPoint();
            for (double dist = 0; dist < distance; dist += interval)
            {
                var previousPoint = currentPoint;
                var bearing = GeoCalculator.GetBearing(currentPoint, destination);
                currentPoint = GeoCalculator.GetDestination(currentPoint, bearing, interval);
                //bearing = GeoCalculator.GetBearingFinal(previousPoint, currentPoint);
            }
            return currentPoint;
        }

        public static GeoPoint GetPathDestination(IGeoLocatable origin, double bearing, double distance, double interval = GeoGlobal.Earths.CircumferenceOneDegree, double radius = GeoGlobal.Earths.Radius)
        {
            var currentPoint = origin.ToGeoPoint();
            //var previousPoint = origin.ToGeoPoint();
            for (double dist = 0; dist <= distance; dist += interval)
            {
                var previousPoint = currentPoint;
                currentPoint = GeoCalculator.GetDestination(currentPoint, bearing, interval);
                bearing = GeoCalculator.GetBearingFinal(previousPoint, currentPoint);
            }
            return currentPoint;
        }

        /// <summary>
        /// Calculates geographic path between two geographic locations on the Great Circle 
        /// <para>Using the Spherical law of cosines </para>
        /// </summary>
        /// <param name="geoShape">list of points in geographic shape</param>
        /// <param name="interval">interval between consecutive points of the geographic path, in kilometers</param>
        /// <param name="radius">radius of a geographic sphere, in kilometers</param>
        public static GeoPointList GetPathPoints(IEnumerable<IGeoLocatable> geoShape, double interval = GeoGlobal.Earths.CircumferenceOneDegree, double radius = GeoGlobal.Earths.Radius)
        {
            var path = new GeoPointList();
            if (geoShape.Count() <= 2)
            {
                path.AddRange(geoShape.Select(point => point.ToGeoPoint()));
            }
            else // if (geoShapePoints.Count > 2)
            {
                var currentPoint = geoShape.First().ToGeoPoint();
                for (int i = 1; i < geoShape.Count(); i++)
                {
                    var nextPoint = geoShape.ElementAt(i).ToGeoPoint();
                    var points = GetPathPoints(currentPoint, nextPoint, interval, radius);
                    path.AddRange(points);
                    currentPoint = nextPoint;
                }
            }
            return path;
        }

        #endregion
        #region Geographic Rhumb
        /// <summary>
        /// Calculates distance/bearing between two geographic locations on Rhumb line (loxodrome)
        /// </summary>
        /// <param name="origin">origin location in geographic degrees</param>
        /// <param name="destination">destination location in geographic degrees</param>
        /// <param name="radius">radius of a geographic sphere, in kilometers</param>
        /// <remarks>radius defaults to Earth's mean radius</remarks>
        public static GeoRhumb GetRhumb(GeoPoint origin, GeoPoint destination, double radius = GeoGlobal.Earths.Radius)
        {
            origin = origin.ToRadians();
            destination = destination.ToRadians();
            var dLat = (destination.Latitude - origin.Latitude);
            var dLon = (destination.Longitude - origin.Longitude);

            var tDestination = Math.Tan(Math.PI / 4 + destination.Latitude / 2);
            var tOrigin = Math.Tan(Math.PI / 4 + origin.Latitude / 2);

            var dPhi = Math.Log(tDestination / tOrigin);    // E-W line gives dPhi=0
            var q = (IsFinite(dLat / dPhi)) ? dLat / dPhi : Math.Cos(origin.Latitude);

            // if dLon over 180° take shorter Rhumb across anti-meridian:
            if (Math.Abs(dLon) > Math.PI)
            {
                dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
            }

            var distance = Math.Sqrt(dLat * dLat + q * q * dLon * dLon) * radius;
            var bearing = Math.Atan2(dLon, dPhi);
            return new GeoRhumb { Distance = distance, Bearing = bearing.ToDegreesNormalized() };
        }

        public class GeoRhumb
        {
            public double Distance { get; set; }
            public double Bearing { get; set; }
        }
        #endregion

        #region Geographic Rect
        //TODO add overload methods for Points,  
        public static GeoRect GetRect(List<List<Point>> geoShapes)
        {
            if (geoShapes == null || geoShapes.Count == 0) return GeoRect.Empty;
            var geoLocations = new List<IGeoLocatable>();
            foreach (var geoPoints in geoShapes)
            {
                foreach (var point in geoPoints)
                {
                    geoLocations.Add(new GeoPoint(point));
                }
            }
            return GetRect(geoLocations);
        }

        public static GeoRect GetRect(List<Point> geoPoints)
        {
            if (geoPoints == null || geoPoints.Count == 0) return GeoRect.Empty;
            var geoLocations = new List<IGeoLocatable>();
            foreach (var point in geoPoints)
            {
                geoLocations.Add(new GeoPoint(point));
            }
            return GetRect(geoLocations);
        }

        public static GeoRect GetRect(List<IGeoLocatable> geoLocations)
        {
            if (geoLocations == null || geoLocations.Count == 0) return GeoRect.Empty;

            var west = double.MaxValue;
            var south = double.MaxValue;
            var east = double.MinValue;
            var north = double.MinValue;

            foreach (var geoPoint in geoLocations)
            {
                west = System.Math.Min(west, geoPoint.Longitude);
                south = System.Math.Min(south, geoPoint.Latitude);
                east = System.Math.Max(east, geoPoint.Longitude);
                north = System.Math.Max(north, geoPoint.Latitude);
            }
            if (west == double.MaxValue || east == double.MinValue ||
               south == double.MaxValue || north == double.MinValue)
                return GeoRect.Empty;
            
            return new GeoRect(west, south, east, north);
        }

        #endregion

        #region Window Unit Coordinates
        public static Point GetWindowCoordinate(IGeoLocatable location)
        {
            return GetWindowCoordinate(location.Longitude, location.Latitude);
        }
        public static Point GetWindowCoordinate(double longitude, double latitude)
        {
            var x = (longitude + GeoGlobal.WorldsMercator.LongitudeMax) / GeoGlobal.WorldsMercator.MapWidth;
            var y = (latitude + GeoGlobal.WorldsMercator.LatitudeMax) / GeoGlobal.WorldsMercator.MapHeight;
            return new Point(x, y);
        } 
        #endregion
        public static bool IsFinite(double x)
        {
            return !double.IsInfinity(x) && !double.IsNaN(x);
        }
    }
    public interface IGeoLocatable
    {
        double Longitude { get; set; }
        double Latitude { get; set; }
        Point ToPoint();
    }
    public static class GeoExtensions
    {
        //public static GeoPoint ToRadians(this GeoPoint geoPoint)
        //{
        //    return new GeoPoint(geoPoint.Longitude.ToRadians(), geoPoint.Latitude.ToRadians());
        //}
        public static GeoPoint ToRadians(this IGeoLocatable geoLocation)
        {
            return new GeoPoint(geoLocation.Longitude.ToRadians(), geoLocation.Latitude.ToRadians());
        }
        public static GeoLocation ToGeoLocation(this GeoPoint geoPoint)
        {
            return new GeoLocation(geoPoint.ToPoint());
        }
        public static GeoPoint ToGeoPoint(this IGeoLocatable geoLocation)
        {
            return new GeoPoint(geoLocation.ToPoint());
        }
        //public static GeoPoint ToGeoPoint(this GeoLocation geoLocation)
        //{
        //    return new GeoPoint(geoLocation.Longitude, geoLocation.Latitude);
        //}
        
    }
    public static class DoubleEx
    {
        /// <summary> Converts a value to radians </summary>
        public static double ToRadians(this double degrees)
        {
            return degrees * Math.PI / 180;
        }
        /// <summary> Converts a value to degrees, in range -180 and 180 </summary>
        public static double ToDegrees(this double radians)
        {
            return (radians * 180.0 / Math.PI);
            //return (radians / 180 * Math.PI);
        }
        /// <summary> Converts a value to degrees with negative values normalized between 0 and 360 </summary>
        public static double ToDegreesNormalized(this double radians)
        {
            var degrees = radians.ToDegrees();
            degrees = (degrees + 360) % 360;
            return degrees;
            //return (radians.ToDegrees() + 360) % 360;
        }
        /// <summary> Converts a angle (in degrees) to reversed angle with negative values normalized between 0 and 360 </summary>
        public static double ToDegreesReversed(this double degrees)
        {
            return (degrees + 180) % 360;
        }

    }
}