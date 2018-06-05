using System.Collections.Generic;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents a geographic Ellipsoid shape
    /// </summary>
    public class GeoEllipsoid
    {
        /// <summary>
        /// Initializes a new geographic Ellipsoid from the specified type of geographic Ellipsoid
        /// </summary>
        public GeoEllipsoid(GeoEllipsoidType ellipsoidType)
            : this(ellipsoidType.A(), ellipsoidType.F())
        { }

        /// <summary>
        /// Initializes a new geographic Ellipsoid with the specified a and f parameters.
        /// </summary>
        public GeoEllipsoid(double a, double f)
        {
            this.A = a;
            this.F = f;
        }
        #region Properties
        /// <summary>
        /// Gets the a parameter for the geographic Ellipsoid.
        /// </summary>
        public double A { get; private set; }

        /// <summary>
        /// Gets the f parameter for the geographic Ellipsoid.
        /// </summary>
        public double F { get; private set; }

        /// <summary>
        /// Gets the e squared parameter for the geographic Ellipsoid.
        /// </summary>
        public double E2 { get { return 2 * F - F * F; } }

        /// <summary>
        /// Gets the e prime squared parameter for the geographic Ellipsoid.
        /// </summary>
        public double Ep2 { get { return E2 / (1 - E2); } } 
        #endregion
    }

    /// <summary>
    /// Standard GeoEllipsoids.
    /// </summary>
    /// <remarks>
    /// For many maps, including nearly all maps in commercial atlases, it may be
    /// assumed that the Earth is a sphere. Actually, it is more nearly an oblate GeoEllipsoid
    /// of revolution, also called an oblate shperoid. This is an ellipse rotated about its
    /// shorter axis. The flattening of the ellipse for the Earth is only about one part in
    /// three hundred; but it is sufficient to become a necessary part of calculations in
    /// plotting accurate maps at a scale of 1:100 000 or larger, and is significant even for
    /// 1:5 000 000-scale maps of the United States, affecting plotted shapes by up to
    /// 2/3 percent. On small-scale maps, including single-sheet worldRect maps, the oblateness
    /// is negligble.
    /// <para>
    /// The Earth is not an exact GeoEllipsoid, and deviations from this shape are continuously
    /// evaluated. The geoid is the name given to the shape that the Earth would
    /// assume if it were measured at mean sea-level. This is an undulating surface 
    /// that varies not more than about a hundred metres above or below a well-fitting
    /// GeoEllipsoid, a variation far less than the GeoEllipsoid varies from the sphere. It is
    /// important to remember that elevations and contour lines on the Earth are reported
    /// relative to the geoid, not the GeoEllipsoid. Latitude, Longitude and all plane
    /// coordinate systems, on the other hand, are determined with respect to the GeoEllipsoid. 
    /// </para>
    /// </remarks>
    public enum GeoEllipsoidType
    {
        /// <summary>
        /// The GRS80 map datum.
        /// </summary> 
        GRS1980,
        /// <summary>
        /// The NAD 83 map datum
        /// </summary>
        NAD83,
        /// <summary>
        /// The WGS 72 map datum.
        /// </summary>
        WGS72,
        /// <summary>
        /// The WGS 84 map datum.
        /// </summary>
        WGS84,
        /// <summary>
        /// The Australian national map datum.
        /// </summary>
        Australian,
        /// <summary>
        /// The Krassovsky map datum.
        /// </summary> 
        Krassovsky,
        /// <summary>
        /// The International map datum.
        /// </summary> 
        International,
        /// <summary>
        /// The Hayford map datum.
        /// </summary> 
        Hayford,
        /// <summary>
        /// The Clark 1880 map datum.
        /// </summary> 
        Clarke1880,
        /// <summary>
        /// The Clark 1866 map datum.
        /// </summary> 
        Clarke1866,
        /// <summary>
        /// The Airy map datum
        /// </summary>
        Airy,
        /// <summary>
        /// The Bessel 1841 map datum.
        /// </summary>
        Bessel1841,
        /// <summary>
        /// The Everest map datum.
        /// </summary> 
        Everest,
    }

    /// <summary>
    /// Static utility class for map geographic Ellipsoid definitions.
    /// </summary>
    public static class GeoEllipsoidUtil
    {
        /// <summary>
        /// As the specified geographic Ellipsoid type.
        /// </summary>
        public static double A(this GeoEllipsoidType ellipsoidType)
        { return GeoEllipsoids[ellipsoidType].A; }
        /// <summary>
        /// F of the geographic Ellipsoid.
        /// </summary>
        public static double F(this GeoEllipsoidType ellipsoidType)
        { return GeoEllipsoids[ellipsoidType].F; }

        /// <summary>
        /// E2 of the geographic Ellipsoid.
        /// </summary>
        public static double E2(this GeoEllipsoidType ellipsoidType)
        { return GeoEllipsoids[ellipsoidType].E2; }
        /// <summary>
        /// Ep2 of the geographic Ellipsoid.
        /// </summary>
        public static double Ep2(this GeoEllipsoidType ellipsoidType)
        { return GeoEllipsoids[ellipsoidType].Ep2; }

        static GeoEllipsoidUtil()
        {
            GeoEllipsoids = new Dictionary<GeoEllipsoidType, GeoEllipsoid>();
//            GeoEllipsoids[GeoEllipsoidType.Sphere] = new GeoEllipsoid(6378388, 0);
            GeoEllipsoids[GeoEllipsoidType.GRS1980] = new GeoEllipsoid(6378137, 1 / 298.257);
            GeoEllipsoids[GeoEllipsoidType.WGS72] = new GeoEllipsoid(6378135, 1 / 298.26);
            GeoEllipsoids[GeoEllipsoidType.WGS84] = new GeoEllipsoid(6378137, 1 / 298.257);
            GeoEllipsoids[GeoEllipsoidType.NAD83] = new GeoEllipsoid(6378137, 1 / 298.257223563);
            GeoEllipsoids[GeoEllipsoidType.Australian] = new GeoEllipsoid(6378160, 1 / 298.5);
            GeoEllipsoids[GeoEllipsoidType.Krassovsky] = new GeoEllipsoid(6378245, 1 / 298.3);
            GeoEllipsoids[GeoEllipsoidType.International] = new GeoEllipsoid(6378388, 1 / 297.0);
            GeoEllipsoids[GeoEllipsoidType.Hayford] = new GeoEllipsoid(6378388, 1 / 297.0);
            GeoEllipsoids[GeoEllipsoidType.Clarke1880] = new GeoEllipsoid(6378249.1, 1 / 293.46);
            GeoEllipsoids[GeoEllipsoidType.Clarke1866] = new GeoEllipsoid(6378206.4, 1 / 294.98);
            GeoEllipsoids[GeoEllipsoidType.Airy] = new GeoEllipsoid(6356256.9, 1 / 299.32);
            GeoEllipsoids[GeoEllipsoidType.Bessel1841] = new GeoEllipsoid(6377397.2, 1 / 299.15);
            GeoEllipsoids[GeoEllipsoidType.Everest] = new GeoEllipsoid(6377276.3, 1 / 300.80);
        }

        private static readonly Dictionary<GeoEllipsoidType, GeoEllipsoid> GeoEllipsoids;
    }
}
