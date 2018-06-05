using System;
using System.Windows;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents a cylindrical equidistance projection.
    /// </summary>
    public class CylindricalEquidistant : GeoProjection
    {
        #region Radius dependency property
        /// <summary>
        /// Sets or gets the current projections's central meridian
        /// </summary>
        public double Radius
        {
            get
            {
                return (double)GetValue(RadiusProperty);
            }
            set
            {
                SetValue(RadiusProperty, value);
            }
        }

        /// <summary>
        /// The radius dependency property
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(CylindricalEquidistant), new PropertyMetadata(6378388.0, new PropertyChangedCallback(UpdateConstants)));
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
        /// Identifies the <see cref="CentralMeridian"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CentralMeridianProperty = DependencyProperty.Register("CentralMeridian", typeof(double), typeof(CylindricalEquidistant), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));
        #endregion
 
        #region StandardParallel dependency property
        /// <summary>
        /// Sets or gets the current projections's central meridian
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
        public static readonly DependencyProperty StandardParallelProperty = DependencyProperty.Register("StandardParallel", typeof(double), typeof(CylindricalEquidistant), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));
        #endregion

        public override double LatitudeMin
        {
            get { return -90; }
        }

        public override double LatitudeMax
        {
            get { return 90; }
        }

        private double R;
        private double cosphi_1;
        private double lambda_0;

        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            R = Radius;
            cosphi_1 = Math.Cos(MathUtil.Radians(StandardParallel));
            lambda_0 = MathUtil.Radians(CentralMeridian);
        }

        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double x = R * (lambda - lambda_0) * cosphi_1;  // 12-1
            double y = R * phi;     // 12-2

            return new Point(x, y);
        }

        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double phi = y / R; // 12-5
            double lambda = lambda_0 + x / (R * cosphi_1); // 12-6

            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }
    }
}
