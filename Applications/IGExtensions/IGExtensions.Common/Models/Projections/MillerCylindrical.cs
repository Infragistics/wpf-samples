using System;
using System.Windows;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents a miller cylindrical projection.
    /// </summary>
    public class MillerCylindrical : GeoProjection
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
        public static readonly DependencyProperty CentralMeridianProperty = DependencyProperty.Register("CentralMeridian", typeof(double), typeof(MillerCylindrical), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));
        #endregion

        #region Radius 
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
        /// Identifies the <see cref="Radius"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty RadiusProperty = DependencyProperty.Register("Radius", typeof(double), typeof(MillerCylindrical), new PropertyMetadata(6378388.0, new PropertyChangedCallback(UpdateConstants)));
        #endregion

        #region Constants
        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            R = Radius;
            lambda_0 = MathUtil.Radians(CentralMeridian);
        }

        private double R;
        private double lambda_0;

        #endregion
        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double x = R * (lambda - lambda_0); // 11-1
            double y = R * MathUtil.Asinh(Math.Tan(0.8 * phi)) / 0.8;   // 11-2a

            return new Point(x, y);
        }
        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double phi = Math.Atan(Math.Sinh(0.8 * y / R)) / 0.8;   // 11-6a
            double lambda = lambda_0 + x / R;                       // 11-7

            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }
    }
}
