using System;
using System.Windows;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <remarks>
    /// The well-known Mercator projection was perhaps the first projection to be
    /// regularly identified when atlases of over a century ago gradually began to
    /// name projections used, a practice now fairly commonplace.
    /// <para>
    /// The meridians of longitude of the Mercator projection are vertical parallel
    /// equally spaced lines, cut at right angles by horizontal straight parallels
    /// which are increasingly spaced at each pole so that conformality exists.
    /// </para>
    /// <para>
    /// The major navigational feature of the projection is found in the facts that a
    /// sailing route between two points is shown as a straight line, if the direction or
    /// azimuth of the ship remains constant with respect to north. This kind of route
    /// is called a loxodrome or Rhumb line and is usually longer than the great circle
    /// path. 
    /// </para>
    /// <para>
    /// This projection has been standard since 1910 for nautical charts prepared by the
    /// former U.S. Coast and Geodetic Survey.
    /// </para>
    /// <para>
    /// This code has been verified against the numerical examples in "Map Projections -
    /// A Working Manual" by John P Snyder, United States Geological Survey Professional
    /// Paper 1395" for forward and reverse spherical and ellipsoid.
    /// </para>
    /// </remarks>
    public class Mercator : GeoProjection
    {
        #region CentralMeridian  
        /// <summary>
        /// Sets or gets the current projections's central meridian
        /// </summary>
        public double CentralMeridian
        {
            get
            {
                return (double)GetValue(CentralMeridianProperty);
            }
            set
            {
                SetValue(CentralMeridianProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="CentralMeridian"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CentralMeridianProperty = DependencyProperty.Register(
            "CentralMeridian", typeof(double), typeof(Mercator), 
            new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region Constatns
        // ellipsoid constants
        private double a;
        private double e;
        private double e2;
        private double e4;
        private double e6;
        private double e8;

        // defining constants
        double lambda_0;

        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            a = EllipsoidType.A();
            e2 = EllipsoidType.E2();
            e = Math.Sqrt(e2);
            e4 = e2 * e2;
            e6 = e4 * e2;
            e8 = e4 * e4;

            lambda_0 = MathUtil.Radians(CentralMeridian);
        } 
        #endregion
        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            geodetic.Y = MathUtil.Clamp(geodetic.Y, this.LatitudeMin, this.LatitudeMax);

            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double sinphi = Math.Sin(phi);

            double x = a * (lambda - lambda_0);                                                                             // 7-6
            double y = (a / 2) * Math.Log((1 + sinphi) / (1 - sinphi) * Math.Pow((1 - e * sinphi) / (1 + e * sinphi), e));  // 7-7a

            return new Point(x, y);
        }
        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double t = Math.Exp(-y / a);                                                                // 7-10
            double chi = Math.PI / 2 - 2 * Math.Atan(t);                                                // 7-13

            double lambda = x / a + lambda_0;                                                           // 7-12
            double phi = chi +
                            (e2 / 2 + 5 * e4 / 24 + e6 / 12 + 13 * e8 / 360) * Math.Sin(2 * chi) +
                            (7 * e4 / 48 + 29 * e6 / 240 + 811 * e8 / 11520) * Math.Sin(4 * chi) +
                            (7 * e6 / 120 + 81 * e8 / 1120) * Math.Sin(6 * chi) +
                            (4279 * e8 / 161280) * Math.Sin(8 * chi);                                   // 3-5

            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }
    }
}
