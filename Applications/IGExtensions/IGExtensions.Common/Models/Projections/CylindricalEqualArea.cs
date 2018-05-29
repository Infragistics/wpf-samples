using System;
using System.Windows;
using System.Windows.Shapes;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// The normal aspect cylindrical equal area projection.
    /// </summary>
    /// <remarks>
    /// The cylindrical equal area projection was proposed by Johann Heinrich Lambert
    /// and is occasionally given his name. 
    /// <para>
    /// Like other regular cylindrical, the graticule of the normal Cylindrical Equal
    /// Area projection consists of straight equally spaced vertical meridians perpendicular
    /// to straight unequally spaced horizontal parallels.
    /// </para>
    /// </remarks>
    public class CylindricalEqualArea : GeoProjection
    {
        #region StandardParallel dependency property
        /// <summary>
        /// Sets or gets the current projections's standard southern parallel
        /// </summary>
        public double StandardParallel
        {
            get
            {
                return (double)GetValue(StandardParallelProperty);
            }
            set
            {
                SetValue(StandardParallelProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="StandardParallel"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty StandardParallelProperty = DependencyProperty.Register("StandardParallel", typeof(double), typeof(CylindricalEqualArea), new PropertyMetadata(60.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region LongitudeOrigin dependency property
        /// <summary>
        /// Sets or gets the current projections's central meridian
        /// </summary>
        public double LongitudeOrigin
        {
            get
            {
                return (double)GetValue(LongitudeOriginProperty);
            }
            set
            {
                SetValue(LongitudeOriginProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="LongitudeOrigin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty LongitudeOriginProperty = DependencyProperty.Register("LongitudeOrigin", typeof(double), typeof(CylindricalEqualArea), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));
        #endregion

        private double a;
        private double e;
        private double e2;
        private double e4;
        private double e6;
        private double k_0;
        private double cosphi_s;
        private double lambda_0;
        private double q_p;

        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            double phi_s = MathUtil.Radians(StandardParallel);
            double sinphi_s = Math.Cos(phi_s);
            double sin2phi_s = sinphi_s * sinphi_s;

            a = EllipsoidType.A();
            e2 = EllipsoidType.E2();
            e = Math.Sqrt(e2);
            e4 = e2 * e2;
            e6 = e4 * e2;
            cosphi_s = Math.Cos(phi_s);
            lambda_0 = MathUtil.Radians(LongitudeOrigin);
            k_0 = cosphi_s / Math.Sqrt(1 - e2 * sin2phi_s);    // 10-13
            q_p = (1 - e2) * (1 / (1 - e2) - (1 / (2 * e)) * Math.Log((1 - e) / (1 + e))); // 3-12 with phi=90 degrees
        }

        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double sinphi = Math.Sin(phi);
            double sin2phi = sinphi*sinphi;

            double q=(1-e2)*(sinphi/(1-e2*sin2phi)-(1/(2*e))*Math.Log((1-e*sinphi)/(1+e*sinphi))); // 3-12
            double x = a * k_0 * (lambda - lambda_0);   // 10-14
            double y = a * q / (2 * k_0);               // 10-15

            return new Point(x, y);
        }

        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double q_p = (1 - e2) * (1 / (1 - e2) - (1 / (2 * e)) * Math.Log((1 - e) / (1 + e))); // 3-12 with phi=90 degrees
            double d = MathUtil.Clamp((2 * y * k_0)/(a * q_p), -1.0, 1.0);

            double beta = Math.Asin(d);

            double phi = beta +
                        (e2 / 3 + 31 * e4 / 180 + 517 * e6 / 5040 /* +... */) * Math.Sin(2 * beta) +
                        (23 * e4 / 360 + 251 * e6 / 3780/* +... */) * Math.Sin(4 * beta) +
                        (761 * e6 / 45360/* +... */) * Math.Sin(6 * beta)/* +... */;
            double lambda = lambda_0 + x / (a * k_0);

            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }

        internal override void RenderGrid(Path path, Rect worldRect, Rect windowRect)
        {
            base.RenderGrid(path, worldRect, windowRect);
        }
    }
}
