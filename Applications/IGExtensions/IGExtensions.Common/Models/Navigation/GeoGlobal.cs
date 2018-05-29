using System;
using System.Windows;

namespace IGExtensions.Common.Models
{
    public static class GeoGlobal
    {
        #region Earth's constants

        public static class Earths
        {
            /// <summary>
            /// Earth's mean radius in kilometers
            /// </summary>
            public const double Radius = 6371.0;
            /// <summary>
            /// Earth's polar radius in kilometers
            /// </summary>
            public const double RadiusPolar = 6357.0;
            /// <summary>
            /// Earth's equatorial radius in kilometers  
            /// </summary>
            public const double RadiusEquatorial = 6378.0;

            /// <summary>
            /// Earth's equatorial circumference in kilometers  
            /// </summary>
            public const double CircumferenceEquatorial = 2 * Math.PI * RadiusEquatorial;
            /// <summary>
            /// Earth's equatorial circumference in kilometers  
            /// </summary>
            public const double CircumferencePolar = 2 * Math.PI * RadiusPolar;
            /// <summary>
            /// Earth's mean circumference in kilometers  
            /// </summary>
            public const double Circumference = 2 * Math.PI * Radius;
            /// <summary>
            /// Earth's one degree of mean circumference in kilometers  
            /// </summary>
            public const double CircumferenceOneDegree = Circumference / 360.0;
            public const double CircumferenceTenDegrees = CircumferenceOneDegree * 10;
            //public const double CircumferenceTenDegrees = CircumferenceOneDegree * 10;

        }
        #endregion

        #region World's constants
        /// <summary>
        /// World's actual geographic constants
        /// </summary>
        public static class Worlds
        {
            public const double LongitudeMin = -180.0;
            public const double LongitudeMax = 180.0;
            public const double LatitudeMin = -90.0;
            public const double LatitudeMax = 90.0;

            /// <summary>
            /// World's actual width in geographic degrees
            /// </summary>
            public const double ActualWidth = LongitudeMax - LongitudeMin;
            /// <summary>
            /// World's actual height in geographic degrees
            /// </summary>
            public const double ActualHeight = LatitudeMax - LatitudeMin;

            /// <summary>
            /// World's actual region in geographic degrees
            /// </summary>
            public static readonly GeoRect ActualRegion = new GeoRect(LongitudeMin, LatitudeMin, LongitudeMax, LatitudeMax);
            ///// <summary>
            ///// World's actual rect in geographic degrees
            ///// </summary>
            //public static Rect ActualRect = ActualRegion.ToRect();  
        }
        /// <summary>
        /// Map geographic constants in the Spherical Mercator projection
        /// </summary>
        public class WorldsMercator
        {
            public const double LongitudeMin = -180.0;
            public const double LongitudeMax = 180.0;
            //public const double LatitudeMin = -85.0;
            //public const double LatitudeMax = 85.0;
            public const double LatitudeMin = -85.05112878;
            public const double LatitudeMax = 85.05112878;

            /// <summary>
            /// World's width in geographic degrees on Spherical Mercator projection 
            /// </summary>
            public const double MapWidth = LongitudeMax - LongitudeMin;
            /// <summary>
            /// World's height in geographic degrees on Spherical Mercator projection 
            /// </summary>
            public const double MapHeight = LatitudeMax - LatitudeMin;

            /// <summary>
            /// World's region in geographic degrees on Spherical Mercator projection 
            /// </summary>
            public static readonly GeoRect MapRegion = new GeoRect(LongitudeMin, LatitudeMin, LongitudeMax, LatitudeMax);
            ///// <summary>
            ///// World's rect in geographic degrees on Spherical Mercator projection 
            ///// </summary>
            //public static Rect MapRect = MapRegion.ToRect(); 
        }

        #endregion

        #region Converters
        public static class Converters
        {
            /// <summary>
            /// converter factor for kilometers to nautical miles 
            /// </summary>
            public const double KilometersToMiles = 0.5399568034557235; //0.621371192237334;
            /// <summary>
            /// converter factor for nautical miles to kilometers 
            /// </summary>
            public const double MilesToKilometers = 1.0 / KilometersToMiles; // 1.852; //1.609344;
            /// <summary>
            /// converter factor for geographic degrees to kilometers 
            /// </summary>
            public const double DegreesToKilometers = Earths.CircumferenceOneDegree; // 111.1946888183929;
            /// <summary>
            /// converter factor for kilometers to geographic degrees 
            /// </summary>
            public const double KilometersToDegrees = 1.0 / Earths.CircumferenceOneDegree; // 0.0089932352941176;
        }

        #endregion
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