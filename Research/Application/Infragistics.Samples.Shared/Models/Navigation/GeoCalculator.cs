using System;
using System.Windows;

namespace Infragistics.Samples.Shared.Models.Navigation
{
    public static class GeoCalculator
    {
       
        /// <summary>
        /// Calculates angle bearing from origin geographic location towards destination geographic location
        /// </summary>
        public static double GetBearingAngle(GeoLocation origin, GeoLocation destination)
        {
            var latitudeDelta = destination.Latitude - origin.Latitude;
            var longitudeDelta = destination.Longitude - origin.Longitude;

            var angle = Math.Atan2((latitudeDelta), (longitudeDelta)) * 180.0 / Math.PI;

            return -1 * angle;
            //return angle;
        }
        /// <summary>
        /// Calculates distance between two geographic locations given a radius
        /// </summary>
        public static double GetGreatCircleDistance(GeoLocation orign, GeoLocation destination, double radius = GeoStats.EarthRadius)
        {
            var dLat = (destination.Latitude - orign.Latitude) * Math.PI / 180;
            var dLon = (destination.Longitude - orign.Longitude) * Math.PI / 180;

            var lat1 = orign.Latitude * Math.PI / 180;
            var lat2 = destination.Latitude * Math.PI / 180;

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = radius * c;

            return distance;
        }
      
        public static GeoLocation GetMidLocation(GeoLocation orign, GeoLocation destination)
        {
            var latitudeDelta = destination.Latitude - orign.Latitude;
            var longitudeDelta = destination.Longitude - orign.Longitude;

            var bx = Math.Cos(destination.Latitude) * Math.Cos(longitudeDelta);
            var by = Math.Cos(destination.Latitude) * Math.Sin(longitudeDelta);
            var lat3 = Math.Atan2(Math.Sin(orign.Latitude) + Math.Sin(destination.Latitude),
                                  Math.Sqrt((Math.Cos(orign.Latitude) + bx) * (Math.Cos(orign.Latitude) + bx) + by * by));
            var lon3 = orign.Longitude + Math.Atan2(by, Math.Cos(orign.Latitude) + bx);

            return new GeoLocation(lon3, lat3);
        }
        public static Point GetWindowCoordinate(GeoLocation location)
        {
            var x = GetWindowCoordinateX(location);
            var y = GetWindowCoordinateY(location);
            return new Point(x, y);
        }
        public static double GetWindowCoordinateX(GeoLocation location)
        {
            return GetWindowCoordinateX(location.Longitude);
        }
        public static double GetWindowCoordinateY(GeoLocation location)
        {
            return GetWindowCoordinateY(location.Latitude);
        }
        public static double GetWindowCoordinateX(double longitude)
        {
            var x = (longitude + 180) / 360;
            return x;
        }
        public static double GetWindowCoordinateY(double latitude)
        {
            var y = (latitude + 85) / 170;
            return y;
        }
    }
    public static class GeoStats
    {
        /// <summary>
        /// Earth radius in km
        /// </summary>
        public const double EarthRadius = 6371;

        /// <summary>
        /// Airplane average speed in km per hour, based on Boeing-747
        /// </summary>
        public const double AirplaneAverageSpeed = 920;
        /// <summary>
        /// Airplane average speed in km per hour, based on Boeing-747
        /// </summary>
        public const double AirplaneAverageRange = 13450;

    }
}