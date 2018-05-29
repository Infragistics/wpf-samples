using System;
using System.Windows;
using System.Windows.Shapes;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// The Lambert Conformal Conic projection is used for the 1:1 000 000-scale regional
    /// worldRect aeronautical charts, the 1:500 000-scale sectional aeronautical charts and
    /// 1:500 000-scale State base maps (all 48 contiguous states have the same
    /// standard parallels of lat. 33 and 45N, and thus match). Also cast on the Lambert
    /// are most of the 1:24 000-scale 5.7 minute quadrangle maps prepared after 1957
    /// which lie in zones for which the Lambert is the base for the SPCS. In the latter
    /// case,  the standard parallels for the zone are used, rather than the parameters
    /// designed for the individual quadrangles.
    /// <para>
    /// The Lambert Conformal Conic has also been adopted as the official topographic
    /// representation for some other countries.
    /// </para>
    /// <para>
    /// The pole in the same hemisphere as the standard parallels is shown on the 
    /// Lambert conformal conic as a point, the pole in the other hemisphere is 
    /// at infinity. Straight lines between points approximate great circle
    /// arcs for maps of moderate coverage.
    /// </para>
    /// <para>
    /// In some atlases, partiularly British, the Lambert Conformal Conic is called the 
    /// "Conical Orthomorophic" projection.
    /// </para>
    /// <para>
    /// This code has been verified against the numerical examples in "Map Projections -
    /// A Working Manual" by John P Snyder, United States Geological Survey Professional
    /// Paper 1395" for forward and reverse ellipsoid.
    /// </para>
    /// </remarks>
    public class LambertConformalConic : GeoProjection
    {
        #region StandardParallelNorth dependency property
        /// <summary>
        /// Sets or gets the current projections's standard southern parallel
        /// </summary>
        public double StandardParallelNorth
        {
            get
            {
                return (double)GetValue(StandardParallelNorthProperty);
            }
            set
            {
                SetValue(StandardParallelNorthProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="StandardParallelNorth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StandardParallelNorthProperty = DependencyProperty.Register("StandardParallelNorth", typeof(double), typeof(LambertConformalConic), new PropertyMetadata(60.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region StandardParallelSouth dependency property
        /// <summary>
        /// Sets or gets the current projections's standard southern parallel
        /// </summary>
        public double StandardParallelSouth
        {
            get
            {
                return (double)GetValue(StandardParallelSouthProperty);
            }
            set
            {
                SetValue(StandardParallelSouthProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="StandardParallelSouth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StandardParallelSouthProperty = DependencyProperty.Register("StandardParallelSouth", typeof(double), typeof(LambertConformalConic), new PropertyMetadata(20.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region LatitudeOrigin dependency property
        /// <summary>
        /// Sets or gets the current projections's latitude origin
        /// </summary>
        public double LatitudeOrigin
        {
            get
            {
                return (double)GetValue(LatitudeOriginProperty);
            }
            set
            {
                SetValue(LatitudeOriginProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="LatitudeOrigin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LatitudeOriginProperty = DependencyProperty.Register("LatitudeOrigin", typeof(double), typeof(LambertConformalConic), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region CentralMeridian dependency property
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
        /// Identifies the <see cref="CentralMeridianProperty"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CentralMeridianProperty = DependencyProperty.Register("CentralMeridianProperty", typeof(double), typeof(LambertConformalConic), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));
        #endregion

        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            a = EllipsoidType.A();
            e2 = EllipsoidType.E2();
            e = Math.Sqrt(e2);

            e4=e2*e2;
            e6=e4*e2;
            e8=e4*e4;

            double phi_0 = MathUtil.Radians(LatitudeOrigin);
            double phi_1 = MathUtil.Radians(StandardParallelSouth);
            double phi_2 = MathUtil.Radians(StandardParallelNorth);
            lambda_0 = MathUtil.Radians(CentralMeridian);

            double cosphi_1 = Math.Cos(phi_1);
            double sinphi_1 = Math.Sin(phi_1);
            double m_1 = cosphi_1 / Math.Sqrt(1 - e2 * sinphi_1 * sinphi_1);                                                              // 14-15(1)
            double t_1 = Math.Sqrt(((1 - sinphi_1) / (1 + sinphi_1)) * Math.Pow((1 + e * sinphi_1) / (1 - e * sinphi_1), e));   // 15-9a(1)
         
            double cosphi_2 = Math.Cos(phi_2);
            double sinphi_2 = Math.Sin(phi_2);
            double m_2 = cosphi_2 / Math.Sqrt(1 - e2 * sinphi_2 * sinphi_2);                                                              // 14-15(2)
            double t_2 = Math.Sqrt(((1 - sinphi_2) / (1 + sinphi_2)) * Math.Pow((1 + e * sinphi_2) / (1 - e * sinphi_2), e));   // 15-9a(2)

            n = (Math.Log(m_1) - Math.Log(m_2)) / (Math.Log(t_1) - Math.Log(t_2));                           // 15-8
            F = m_1 / (n * Math.Pow(t_1, n));                                                                // 15-10

            double sinphi_0 = Math.Sin(phi_0);

            double t_0 = Math.Sqrt(((1 - sinphi_0) / (1 + sinphi_0)) * Math.Pow((1 + e * sinphi_0) / (1 - e * sinphi_0), e));   // 15-9a(2)
            rho_0 = a * F * Math.Pow(t_0, n);                                                                                       // 15-7(a)
        }

        double a;
        double e;
        double e2;
        double e4;
        double e6;
        double e8;
        double lambda_0;    // longitude of the origin of rectangular coordinates

        double n;
        double F;
        double rho_0;

        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double sinphi = Math.Sin(phi);
            double cosphi = Math.Cos(phi);

            double t = Math.Sqrt(((1 - sinphi) / (1 + sinphi)) * Math.Pow((1 + e * sinphi) / (1 - e * sinphi), e));   // 15-9a
            double rho = a * F * Math.Pow(t, n);                                                                    // 15-7
            double theta = n * (lambda - lambda_0);                                                                       // 14-4

            double x = rho * Math.Sin(theta);  /* 14-1 */
            double y = rho_0 - rho * Math.Cos(theta); /* 14-2 */

            return new Point(x, y);
        }

        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double theta = Math.Atan(x/(rho_0-y));                                                              // 14-11
            double rho = Math.Sign(n) * Math.Sqrt(x * x + (rho_0 - y) * (rho_0 - y));                                     // 14-10
            double t = Math.Pow(rho / (a * F), 1 / n);                                                          // 15-11

            double lambda = theta / n + lambda_0;                                                               // 14-9

            double chi = Math.PI / 2 - 2 * Math.Atan(t);                                                        // 7-13
            double phi = chi +
                            (e2 / 2 + 5 * e4 / 24 + e6 / 12 + 13*e8 / 360 /* + ... */) * Math.Sin(2 * chi) +
                            (7 * e4 / 48 + 29 * e6 / 240 + 811 * e8 / 11520 /* + ... */) * Math.Sin(4 * chi) +
                            (7 * e6 / 120 + 81 * e8 / 1120 /* + ... */) * Math.Sin(6 * chi) +
                            (4279 * e8 / 161280 /* + ... */) * Math.Sin(8 * chi) /* + ... */;                   // 3-5

            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }

        internal override void RenderGrid(Path path, Rect worldRect, Rect windowRect)
        {
            return; // grids are not supported on lambert 
        }
    }
}
