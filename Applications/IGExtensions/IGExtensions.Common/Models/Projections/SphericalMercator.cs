using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents a Spherical Mercator projection.
    /// </summary>
    public class SphericalMercator : GeoProjection
    {
        #region Properties
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
        /// Central meridian property.
        /// </summary>
        public static readonly DependencyProperty CentralMeridianProperty = DependencyProperty.Register(
            "CentralMeridian", typeof(double), typeof(SphericalMercator), 
            new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

        // defining constants

        double lambda_0;
        double a;

        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            a = EllipsoidType.A();
            lambda_0 = MathUtil.Radians(CentralMeridian);
        } 
        #endregion

        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            geodetic.Y = MathUtil.Clamp(geodetic.Y, -85.05112878, 85.05112878);

            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double sinphi = Math.Sin(phi);

            double x = a * (lambda - lambda_0);                           // 7-1
            double y = (a / 2) * Math.Log((1 + sinphi) / (1 - sinphi));   // 7-2a

            return new Point(x, y);
        }

        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point mercator)
        {
            double y = mercator.Y;
            double x = mercator.X;

            double lambda = x / a + lambda_0;                               // 7-5
            double phi = Math.Atan(Math.Sinh(y / a));                       // 7-4a
            
            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }

    }
}
