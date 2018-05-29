using System.Collections.Generic;
using System.Windows;

namespace IGExtensions.Common.Models
{
    //http://mathworld.wolfram.com/MercatorProjection.html

    /// <summary>
    /// Represents a geographic projector for projecting points between geodetic and Cartesian coordinate systems
    /// </summary>
    public static class GeoProjector
    {
        #region Project -> Cartesian
        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        public static Point ProjectToCartesian(Point geodetic, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var point = Projection(projectionType).ProjectToCartesian(geodetic);
            return point;
        }
        /// <summary>
        /// Projects a list of list of geodetic points to Cartesian points
        /// </summary>
        public static List<Point> ProjectToCartesian(List<Point> geodetic, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var points = Projection(projectionType).ProjectToCartesian(geodetic);
            return points;
        }
        /// <summary>
        /// Projects a list of list of geodetic points to Cartesian points
        /// </summary>
        public static List<List<Point>> ProjectToCartesian(List<List<Point>> geodetic, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var points = Projection(projectionType).ProjectToCartesian(geodetic);
            return points;
        }

        public static GeoPoint ProjectToCartesian(IGeoLocatable geodetic, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var point = ProjectToCartesian(geodetic.ToPoint(), projectionType);
            return new GeoPoint(point);
        } 
        #endregion

        #region Project -> Geographic
        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        public static Point ProjectToGeographic(Point cartesian, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var point = Projection(projectionType).ProjectToCartesian(cartesian);
            return point;
        }
        /// <summary>
        /// Projects a list of Cartesian points to geodetic points
        /// </summary>
        public static List<Point> ProjectToGeographic(List<Point> cartesian, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var points = Projection(projectionType).ProjectToCartesian(cartesian);
            return points;
        }
        /// <summary>
        /// Projects a list of list of Cartesian points to geodetic points
        /// </summary>
        public static List<List<Point>> ProjectToGeographic(List<List<Point>> cartesian, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var points = Projection(projectionType).ProjectToCartesian(cartesian);
            return points;
        }
        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        public static GeoPoint ProjectToGeographic(IGeoLocatable geodetic, GeoProjectionType projectionType = GeoProjectionType.SphericalMercator)
        {
            var point = ProjectToGeographic(geodetic.ToPoint(), projectionType);
            return new GeoPoint(point);
        } 
        #endregion

        /// <summary>
        /// Projections the specified projection type.
        /// </summary>
        /// <param name="projectionType">Type of geographic projection</param>
        /// <returns></returns>
        public static GeoProjection Projection(this GeoProjectionType projectionType)
        {
            return Projections[projectionType];
        }

        private static readonly Dictionary<GeoProjectionType, GeoProjection> Projections;
        static GeoProjector()
        {
            Projections = new Dictionary<GeoProjectionType, GeoProjection>();

            //          projections[GeoProjectionType.LambertConformalConic] = new LambertConformalConic() { };
            //          projections[GeoProjectionType.ModifiedTransverseMercator] = new ModifiedTransverseMercator() { };
            Projections[GeoProjectionType.SphericalMercator] = new SphericalMercator() { };
            Projections[GeoProjectionType.Mercator] = new Mercator() { };
            Projections[GeoProjectionType.ObliqueMercator] = new ObliqueMercator() { };
            //          projections[GeoProjectionType.TransverseMercator] = new TransverseMercator() {};

            // cylindrical equal area (normal aspect)
            Projections[GeoProjectionType.Lambert] = new CylindricalEqualArea() { StandardParallel = 30.0, LongitudeOrigin = 0.0 };
            Projections[GeoProjectionType.Behrmann] = new CylindricalEqualArea() { StandardParallel = 30.0, LongitudeOrigin = 0.0 };
            Projections[GeoProjectionType.TristanEdwards] = new CylindricalEqualArea() { StandardParallel = 37.383, LongitudeOrigin = 0.0 };
            Projections[GeoProjectionType.Peters] = new CylindricalEqualArea() { StandardParallel = 44.138, LongitudeOrigin = 0.0 };
            Projections[GeoProjectionType.GallOrthographic] = new CylindricalEqualArea() { StandardParallel = 45.0, LongitudeOrigin = 0.0 };
            Projections[GeoProjectionType.Balthasart] = new CylindricalEqualArea() { StandardParallel = 50.0, LongitudeOrigin = 0.0 };

            Projections[GeoProjectionType.MillerCylindrical] = new MillerCylindrical() { CentralMeridian = 0.0 };

            // cylindrical equidistant
            Projections[GeoProjectionType.Equirectangular] = new CylindricalEquidistant() { StandardParallel = 0.0, CentralMeridian = 0.0 };
            Projections[GeoProjectionType.Miller37] = new CylindricalEquidistant() { StandardParallel = 37.5, CentralMeridian = 0.0 };
            Projections[GeoProjectionType.Miller43] = new CylindricalEquidistant() { StandardParallel = 43.0, CentralMeridian = 0.0 };
            Projections[GeoProjectionType.Miller50] = new CylindricalEquidistant() { StandardParallel = 50.0, CentralMeridian = 0.0 };
        }
    }

    /// <summary>
    /// Defines a projection type used by coordinate systems to convert geodetic to Cartesian coordinates.
    /// </summary>
    public enum GeoProjectionType
    {

     #region NOT IMPLEMENTED
        ///// <summary>
        ///// Lambert conformal conic projection.
        ///// </summary>
        //LambertConformalConic,
        ///// <summary>
        ///// Modified transverse Mercator projection.
        ///// </summary>
        //ModifiedTransverseMercator,
        ///// <summary>
        ///// Transverse Mercator projection.
        ///// </summary>
        //TransverseMercator, 
	#endregion

        /// <summary>
        /// SphericalMercator projection.
        /// </summary>
        SphericalMercator,
        /// <summary>
        /// Mercator projection.
        /// </summary>
        Mercator,
        /// <summary>
        /// Oblique Mercator projection.
        /// </summary>
        ObliqueMercator,
        /// <summary>
        /// Lambert Cylindrical Equal Area
        /// </summary>
        /// <remarks>
        /// The Lambert cylindrical equal-area mapProjection is a cylindrical
        /// equal-area mapProjection with a standard parallel of thirty degrees. 
        /// </remarks>
        Lambert,
        /// <summary>
        /// Behrmann Cylindrical Equal Area Projection
        /// </summary>
        /// <remarks>
        /// The Behrmann cylindrical equal-area mapProjection is a cylindrical
        /// equal-area mapProjection with a standard parallel of thirty degrees. 
        /// </remarks>
        Behrmann,
        /// <summary>
        /// Tristan Edwards Cylindrical Equal Area Projection
        /// </summary>
        /// <remarks>
        /// The Tristan Edwards cylindrical equal-area mapProjection is a cylindrical
        /// equal-area mapProjection with a standard parallel of 37.383 degrees. 
        /// </remarks>
        TristanEdwards,
        /// <summary>
        /// Peters Cylindrical Equal Area Projection
        /// </summary>
        /// <remarks>
        /// The Tristan Edwards cylindrical equal-area mapProjection is a cylindrical
        /// equal-area mapProjection with a standard parallel of 44.138 degrees. 
        /// </remarks>
        Peters,
        /// <summary>
        /// Gall Orthographic Cylindrical Equal Area Projection
        /// </summary>
        /// <remarks>
        /// The Tristan Edwards cylindrical equal-area mapProjection is a cylindrical
        /// equal-area mapProjection with a standard parallel of 45 degrees. 
        /// </remarks>
        GallOrthographic,
        /// <summary>
        /// Balthasart Cylindrical Equal Area Projection
        /// </summary>
        /// <remarks>
        /// The Balthasart cylindrical equal-area mapProjection is a cylindrical
        /// equal-area mapProjection with a standard parallel of 50 degrees. 
        /// </remarks>
        Balthasart,
        /// <summary>
        /// Miller cylindrical projection.
        /// </summary>
        MillerCylindrical,
        /// <summary>
        /// Equirectangular Projection
        /// </summary>
        /// <remarks>
        /// An equirectangular mapProjection is a cylindrical equidistant mapProjection,
        /// also called a rectangular mapProjection, plane chart, plate carre, or
        /// projected from map, in which the horizontal coordinate is the longitude
        /// and the vertical coordinate is the latitude, so the standard parallel
        /// is taken as zero degrees. 
        /// </remarks>
        Equirectangular,
        /// <summary>
        /// Cylindrical equidistant mapProjection with a standard
        /// parallel of 37.5 degrees
        /// </summary>
        /// <remarks>
        /// Miller cylindrical equidistant mapProjection with a standard
        /// parallel of 37.5 degrees gives minimal overall scale distortion.
        /// </remarks>
        Miller37,
        /// <summary>
        /// Cylindrical equidistant mapProjection with a standard
        /// parallel of 45.0 degrees
        /// </summary>
        /// <remarks>
        /// Miller cylindrical equidistant mapProjection with a standard
        /// parallel of 45.0 degrees gives scale distortion over continents.
        /// </remarks>
        Miller43,
        /// <summary>
        /// Cylindrical equidistant mapProjection with a standard
        /// parallel of 50.0 degrees
        /// </summary>
        Miller50
    }
}
