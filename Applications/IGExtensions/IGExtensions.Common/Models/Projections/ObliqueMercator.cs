using System;
using System.Windows;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// The Hotine Oblique Mercator Projection
    /// </summary>
    /// <remarks>
    /// There are several geographical regions such as the Alaska panhandle
    /// centered along lines which are neither parallels nor meridians, but 
    /// which may be taken as great circle routes passing through the region.
    /// If conformality is desired in such regions, the Oblique Mercator is
    /// a projection which should be considered.
    /// <para>
    /// The Oblique Mercator projection is used in the spherical form for
    /// a few atlas maps. In the ellipsoid form it was used for Switzerland
    /// and Madagascar as well as Malaya and Borneo and Italy. It is used
    /// in the Hotine form by the USGS for grid marks on the panhandle of
    /// Alaska as well as by the US Lake Survey for mapping of the five
    /// great lakes, the St Lawrence River, and the US-Canada border lakes.
    /// </para>
    /// <para>
    /// Until the implementation of Space Oblique Mercator, the Hotine Oblique
    /// Mercator was probably the most suitable projection available for
    /// mapping Landsat type data.
    /// </para>
    /// </remarks>
    public class ObliqueMercator : GeoProjection
    {
        #region LatitudeOrigin dependency property (latitude of projection center)
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
        public static readonly DependencyProperty LatitudeOriginProperty = DependencyProperty.Register("LatitudeOrigin", typeof(double), typeof(ObliqueMercator), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region LongitudeOrigin dependency property (longitude of projection center)
        /// <summary>
        /// Sets or gets the current projections's longitude origin
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
        public static readonly DependencyProperty LongitudeOriginProperty = DependencyProperty.Register("LongitudeOrigin", typeof(double), typeof(ObliqueMercator), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region ScaleFactor dependency property (scale factor at projection center)
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
        public static readonly DependencyProperty ScaleFactorProperty = DependencyProperty.Register("ScaleFactor", typeof(double), typeof(ObliqueMercator), new PropertyMetadata(0.9996, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region Azimuth dependency property
        /// <summary>
        /// Sets or gets the current projections's azimuth
        /// </summary>
        public double Azimuth
        {
            get
            {
                return (double)GetValue(AzimuthProperty);
            }
            set
            {
                SetValue(AzimuthProperty, value);
            }
        }

        /// <summary>
        /// Identifies the <see cref="Azimuth"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AzimuthProperty = DependencyProperty.Register("Azimuth", typeof(double), typeof(ObliqueMercator), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

        #endregion

        #region Constants
        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            a = EllipsoidType.A();
            e2 = EllipsoidType.E2();
            e4 = e2 * e2;
            e6 = e4 * e2;
            e8 = e6 * e2;
            e = Math.Sqrt(e2);
            k_0 = ScaleFactor;
            phi_0 = MathUtil.Radians(LatitudeOrigin);
            lambda_c = MathUtil.Radians(LongitudeOrigin);

            alpha_c = MathUtil.Radians(Azimuth);
            sinalpha_c = Math.Sin(alpha_c);
            cosalpha_c = Math.Cos(alpha_c);

            double sinphi_0 = Math.Sin(phi_0);
            double sin2phi_0 = sinphi_0 * sinphi_0;
            double sin4phi_0 = sin2phi_0 * sin2phi_0;

            double cosphi_0 = Math.Cos(phi_0);
            double cos2phi_0 = cosphi_0 * cosphi_0;
            double cos4phi_0 = cos2phi_0 * cos2phi_0;

            B = Math.Sqrt(1 + e2 * cos4phi_0 / (1 - e2));   // 9-11
            A = a * B * k_0 * Math.Sqrt(1 - e2) / (1 - e2 * sin2phi_0); // 9-12
            double t_0 = Math.Tan(Math.PI / 4 - phi_0 / 2) / Math.Pow((1 - e * sinphi_0) / (1 + e * sinphi_0), e / 2); // 9-13
            double D = B * Math.Sqrt(1 - e2) / (cosphi_0 * Math.Sqrt(1 - e2 * sin2phi_0)); // 9-14

            double D2 = Math.Max(D * D, 1.0);
            double F = D + Math.Sign(phi_0) * Math.Sqrt(D2 - 1);                                // 9-35
            E = F * Math.Pow(t_0, B);                                                    // 9-36
            double G = (F - 1 / F) / 2;                                                         // 9-19
            double gamma_0 = Math.Asin(sinalpha_c / D);                                         // 9-37
            singamma_0 = Math.Sin(gamma_0);
            cosgamma_0 = Math.Cos(gamma_0);

            lambda_0 = lambda_c - Math.Asin(G * Math.Tan(gamma_0)) / B;                  // 9-38
        }

        private double a;
        private double e;
        private double e2;
        private double e4;
        private double e6;
        private double e8;
        private double k_0;
        private double phi_0;
        private double lambda_c;
        private double alpha_c;

        private const double ef = 0;
        private const double nf = 0;

        private double sinalpha_c;
        private double cosalpha_c;

        private double A;
        private double B;
        private double E;
        private double singamma_0;
        private double cosgamma_0;
        private double lambda_0; 
        #endregion

        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double sinphi = Math.Sin(phi);

            double t = Math.Tan(Math.PI / 4 - phi / 2) / Math.Pow((1 - e * sinphi) / (1 + e * sinphi), e / 2); // 9-13(0)
            double Q = E / Math.Pow(t, B);  // 9-25
            double S = (Q - 1 / Q) / 2; // 9-26
            double T = (Q + 1 / Q) / 2; // 9-27
            double V = Math.Sin(B * (lambda - lambda_0)); // 9-28
            double U = (-V*cosgamma_0+S*singamma_0)/T;    // 9-29
            double v = A * Math.Log((1 - U) / (1 + U)) / (2 * B);   // 9-30
            double u = A * Math.Atan((S * cosgamma_0 + V * singamma_0) / Math.Cos(B * (lambda - lambda_0))) / B;    // 9-31

            double x = v * cosalpha_c + u * sinalpha_c + ef;   // 9-33
            double y = u * cosalpha_c - v * sinalpha_c + nf;   // 9-34

            return new Point(x, y);
        }

        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double v = (x - ef) * cosalpha_c - (y - nf) * sinalpha_c; // 9-40
            double u = (y - nf) * cosalpha_c + (x - ef) * sinalpha_c; // 9-41

            double Qp = Math.Exp(-B * v / A);   // 9-42
            double Sp = (Qp - 1 / Qp) / 2;      // 9-43
            double Tp = (Qp + 1 / Qp) / 2;      // 9-44
            double Vp = Math.Sin(B * u / A);    // 9-45
            double Up = (Vp * cosgamma_0 + Sp * singamma_0) / Tp; // 9-46
            double t = Math.Pow(E / Math.Sqrt((1 + Up) / (1 - Up)), 1 / B); // 9-47

            double chi=Math.PI/2-2*Math.Atan(t);
            double phi = chi +
                        (e2 / 2 + 5 * e4 / 24 + e6 / 12 + 13 * e8 / 360 /* +... */) * Math.Sin(2 * chi) +
                        (7 * e4 / 48 + 29 * e6 / 240 + 811 * e8 / 11520 /* +... */) * Math.Sin(4 * chi) +
                        (7 * e6 / 120 + 81 * e8 / 1120 /* +... */) * Math.Sin(6 * chi) +
                        (4279 * e8 / 161280 /* +... */) * Math.Sin(8 * chi) /* + ... */;


            double lambda = lambda_0 - Math.Atan((Sp * cosgamma_0 - Vp * singamma_0) / Math.Cos(B * u / A)) / B;  // 9-48
 
            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }
    }
}
