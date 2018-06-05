using System;
using System.Windows;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// In 1972, the USGS devised a projection specifically for the revision of a 1954
    /// map of Alaska which, like its predecessors, was based on the Polyconic projection.
    /// It resembles the Transverse Mercator in a very limited manner and cannot
    /// be considered a cylindrical projection.
    /// <para>
    /// For transferring data to and from Alaska maps, it was necessary to determine
    /// projection formulas for computer programming. Since it appeared to be 
    /// unnecessarily complicated to derive formulas based on the correct construction,
    /// it was decided to test empirical formulas with actual coordinates. After
    /// various trial values for scale and standard parallels were tested, the empirical
    /// formulas used in this class were obtained. These agree with measured values
    /// within 0.005 inch at mapping scale for 44 out of 58 measurements made on the map
    /// and within 0.01 inch for 54 of them.
    /// </para>
    /// </remarks>
    public class ModifiedTransverseMercator : GeoProjection
    {
        #region LongitudeOrigin dependency property
        /// <summary>
        /// Sets or gets the current projections's central meridian
        /// </summary>
        public double LongitudeOrigin
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
        /// Identifies the <see cref="LongitudeOrigin"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty CentralMeridianProperty = DependencyProperty.Register(
            "LongitudeOrigin", typeof(double), typeof(ModifiedTransverseMercator), 
            new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));
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
        public static readonly DependencyProperty LatitudeOriginProperty = DependencyProperty.Register(
            "LatitudeOrigin", typeof(double), typeof(ModifiedTransverseMercator), 
            new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region ScaleFactor dependency property
        /// <summary>
        /// Sets or gets the current projections's scale factor
        /// </summary>
        public double ScaleFactor
        {
            get
            {
                return (double)GetValue(ScaleFactorProperty);
            }
            set
            {
                SetValue(ScaleFactorProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="ScaleFactor"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty ScaleFactorProperty = DependencyProperty.Register(
            "ScaleFactor", typeof(double), typeof(ModifiedTransverseMercator), 
            new PropertyMetadata(0.9996, new PropertyChangedCallback(UpdateConstants)));
        #endregion

        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
        }

        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            double phi_deg = geodetic.Y;
            double phi = MathUtil.Radians(phi_deg);

            double lambda_deg = geodetic.X;
            double lambda = MathUtil.Radians(lambda_deg);

            double theta = MathUtil.Radians(0.8625111 * (lambda_deg + 150));                // 8-28
            double rho = 4.1320402 - 0.04441727 * phi_deg + 0.0064816 * Math.Sin(2 * phi);  // 8-29

            double x = rho * Math.Sin(theta);                       // 8-26
            double y = 1.5616640 * rho * Math.Cos(theta);           // 8-27

            return new Point(x, y);
        }

        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double rho = MathUtil.Hypot(x, (1.5616640 - y));                            // 8-32
            double lambda_deg = (1 / 0.8625111) * Math.Atan(x / (1.5616640 - y)) - 150; // 8-30
            double phi_deg=60;
            for (int i = 0; i < 10; ++i)
            {
                phi_deg = (4.1320402 + 0.0064816 * Math.Sin(2 * MathUtil.Radians(phi_deg)) - rho) / 0.04441727; // 8-31
            }

            if(lambda_deg<180) {
                lambda_deg=360+lambda_deg;
            }

            return new Point(lambda_deg, phi_deg);
        }
    }
}
