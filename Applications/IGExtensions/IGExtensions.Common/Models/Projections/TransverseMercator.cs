using System;
using System.Windows;
using Infragistics;

namespace IGExtensions.Common.Models
{
    /// <remarks>
    /// Since the Mercator projection has little error close to the Equator, it has
    /// been found very useful in the transverse form, with the equator of the projection
    /// rotated 90 degrees to coincide with the desired central meridian.
    /// <para>
    /// The formulas are for the complete ellipsoid are practical only within
    /// a band between four degrees of longitude and some ten to fifteen degrees
    /// of arc distance on either side of the central meridian, because of the
    /// much more significant scale errors fundamental to any projection covering
    /// a larger area.
    /// </para>
    /// <para>
    /// Little use has been made of the Transverse Mercator for single maps of
    /// continental areas. 
    /// </para>
    /// <para>
    /// This code has been verified against the numerical examples in "Map Projections -
    /// A Working Manual" by John P Snyder, United States Geological Survey Professional
    /// Paper 1395" for forward and reverse ellipsoid.
    /// </para>
    /// </remarks>
    public class TransverseMercator : GeoProjection
    {
        #region UTMZone Dependency Property
        /// <summary>
        /// Sets or gets the current projections's datum
        /// </summary>
        public TransverseZone Zone
        {
            get
            {
                return (TransverseZone)GetValue(ZoneProperty);
            }
            set
            {
                SetValue(ZoneProperty, value);
            }
        }
        /// <summary>
        /// Identifies the UTMZone dependency property.
        /// </summary>
        public static readonly DependencyProperty ZoneProperty = DependencyProperty.Register(
            "Zone", typeof(TransverseZone), typeof(TransverseMercator),
            new PropertyMetadata(TransverseZone.utm1N, new PropertyChangedCallback(ZoneChanged)));
        /// <summary>
        /// Zones the changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="System.Windows.DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        protected static void ZoneChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var projection = (d as TransverseMercator);
            if (projection != null)
            {
                int zoneNumber = (projection.Zone - TransverseZone.utm1X) + 1;

                while (zoneNumber < 0)
                {
                    zoneNumber += 60;
                }

                projection.CentralMeridian = (zoneNumber - 1) * 6 - 180 + 3;  //+3 puts origin in middle of zone
                projection.LatitudeOrigin = 31.0;   // need to work out the latitude origin (from the zone letter)
                projection.UpdateConstants();
            }
        }

        #endregion

        #region LongitudeOrigin dependency property
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
        public static readonly DependencyProperty CentralMeridianProperty = DependencyProperty.Register("LongitudeOrigin", typeof(double), typeof(TransverseMercator), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));
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
        public static readonly DependencyProperty LatitudeOriginProperty = DependencyProperty.Register("LatitudeOrigin", typeof(double), typeof(TransverseMercator), new PropertyMetadata(0.0, new PropertyChangedCallback(UpdateConstants)));

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
            "ScaleFactor", typeof(double), typeof(TransverseMercator), new PropertyMetadata(0.9996, new PropertyChangedCallback(UpdateConstants)));
        #endregion

        #region Constants
        /// <summary>
        /// Updates the constants.
        /// </summary>
        protected override void UpdateConstants()
        {
            base.UpdateConstants();

            k_0 = ScaleFactor;
            a = EllipsoidType.A();
            e2 = EllipsoidType.E2();
            e4 = e2 * e2;
            e6 = e4 * e2;
            ep2 = EllipsoidType.Ep2();
            e_1 = (1 - Math.Sqrt(1 - e2)) / (1 + Math.Sqrt(1 - e2));
            e2_1 = e_1 * e_1;
            e3_1 = e2_1 * e_1;
            e4_1 = e3_1 * e_1;

            lambda_0 = MathUtil.Radians(CentralMeridian);
            phi_0 = MathUtil.Radians(LatitudeOrigin);

            M_0 = a * ((1 - e2 / 4 - 3 * e4 / 64 - 5 * e6 / 256 /* - ... */) * phi_0 - (3 * e2 / 8 + 3 * e4 / 32 + 45 * e6 / 1024 /* + ... */) * Math.Sin(2 * phi_0) + (15 * e4 / 256 + 45 * e6 / 1024 /* + ... */) * Math.Sin(4 * phi_0) - (35 * e6 / 3072 /* + ... */) * Math.Sin(6 * phi_0) /* + ... */);
        }

        private double k_0;
        private double a;
        private double e2;
        private double e4;
        private double e6;
        private double ep2;
        private double e_1;
        private double e2_1;
        private double e3_1;
        private double e4_1;

        private double lambda_0;
        private double phi_0;
        private double M_0; 
        #endregion

        /// <summary>
        /// Projects a geodetic point to Cartesian.
        /// </summary>
        protected override Point ConvertToCartesian(Point geodetic)
        {
            double phi = MathUtil.Radians(geodetic.Y);
            double lambda = MathUtil.Radians(geodetic.X);

            double sinphi=Math.Sin(phi);
            double sin2phi=sinphi*sinphi;
            double cosphi=Math.Cos(phi);
            double cos2phi=cosphi*cosphi;
            double tanphi=Math.Tan(phi);

            double N=a/Math.Sqrt(1-e2*sin2phi);
            double A=(lambda-lambda_0)*cosphi;
            double A2=A*A;
            double A3=A2*A;
            double A4=A3*A;
            double A5=A4*A;
            double A6=A5*A;
            double T=tanphi*tanphi;
            double T2=T*T;
            double C=ep2*cos2phi;
            double C2=C*C;

            double M = a * ((1 - e2 / 4 - 3 * e4 / 64 - 5 * e6 / 256 /* - ... */) * phi - (3 * e2 / 8 + 3 * e4 / 32 + 45 * e6 / 1024 /* + ... */) * Math.Sin(2 * phi) + (15 * e4 / 256 + 45 * e6 / 1024 /* + ... */) * Math.Sin(4 * phi) - (35 * e6 / 3072 /* + ... */) * Math.Sin(6 * phi) /* + ... */);

            double x = k_0 * N * (A + (1 - T + C) * A3 / 6 + (5 - 18 * T + T2 + 72 * C - 58 * ep2) * A5 / 120);
            double y = k_0 * (M - M_0 + N * tanphi * (A2 / 2 + (5 - T + 9 * C + 4 * C2) * A4 / 24 + (61 - 58 * T + T2 + 600 * C - 330 * ep2) * A6 / 720));

            return new Point(x, y);
        }

        /// <summary>
        /// Projects a Cartesian point to geodetic point 
        /// </summary>
        protected override Point ConvertToGeographic(Point cartesian)
        {
            double y = cartesian.Y;
            double x = cartesian.X;

            double M = M_0 + y / k_0;
            double mu = M / (a * (1 - e2 / 4 - 3 * e4 / 64 - 5 * e6 / 256 /* -... */));

            double phi_1 = mu +
                        (3 * e_1 / 2 - 27 * e3_1 / 32 /* + ... */) * Math.Sin(2 * mu) +
                        (21 * e2_1 / 16 - 55 * e4_1 / 32 /* + ... */) * Math.Sin(4 * mu) +
                        (151 * e3_1 / 96  /* + ... */) * Math.Sin(6 * mu) +
                        (1097 * e4_1 / 512 /* - ... */) * Math.Sin(8 * mu);

            double tanphi_1=Math.Tan(phi_1);
            double cosphi_1 = Math.Cos(phi_1);
            double cos2phi_1 = cosphi_1 * cosphi_1;
            double sin2phi_1 = Math.Sin(phi_1) * Math.Sin(phi_1);

            double C_1 = ep2 * cos2phi_1;
            double T_1 = tanphi_1 * tanphi_1;
            double N_1 = a / Math.Sqrt(1 - e2 * sin2phi_1);
            double R_1 = a*(1-e2)/Math.Pow(1-e2*sin2phi_1, 1.5);
            double D = x/(N_1*k_0);

            double C2_1=C_1*C_1;
            double T2_1=T_1*T_1;
            double D2 = D * D;
            double D3 = D2 * D;
            double D4 = D3 * D;
            double D5 = D4 * D;
            double D6 = D5 * D;

            double phi = phi_1 - (N_1 * tanphi_1 / R_1) * (D2 / 2 - (5 + 3 * T_1 + 10 * C_1 - 4 * C2_1 - 9 * ep2) * D4 / 24 + (61 + 90 * T_1 + 298 * C_1 + 45 * T2_1 - 252 * ep2 - 3 * C2_1) * D6 / 720);
            double lambda = lambda_0 + (D - (1 + 2 * T_1 + C_1) * D3 / 6 + (5 - 2 * C_1 + 28 * T_1 - 3 * C2_1 + 8 * ep2 + 24 * T2_1) * D5 / 120) / cosphi_1;

            return new Point(MathUtil.Degrees(lambda), MathUtil.Degrees(phi));
        }

        /// <summary>
        /// Tests this instance.
        /// </summary>
        public static void Test()
        {
            var projection = new TransverseMercator();

            projection.EllipsoidType = GeoEllipsoidType.Clarke1866;    // a=6378206.4, e2=0.00676866
            projection.LatitudeOrigin = 0.0;                        // phi_0=0.0
            projection.CentralMeridian = 75.0;                      // lambda_0=75.0
            projection.ScaleFactor = 0.9996;                        // k0=0.9996

            var cartesian = new Point(127106.5, 4484124.4);
            Point geodetic = projection.ProjectToGeographic(cartesian);        // 40.5000, 73.5000
        }
    }


    /// <summary>
    /// Specifies a universal transverse Mercator zone.
    /// </summary>
    public enum TransverseZone
    {
#pragma warning disable 1591
// ReSharper disable InconsistentNaming
        utm1C, utm2C, utm3C, utm4C, utm5C, utm6C, utm7C, utm8C, utm9C, utm10C,
        utm11C, utm12C, utm13C, utm14C, utm15C, utm16C, utm17C, utm18C, utm19C, utm20C,
        utm21C, utm22C, utm23C, utm24C, utm25C, utm26C, utm27C, utm28C, utm29C, utm30C,
        utm31C, utm32C, utm33C, utm34C, utm35C, utm36C, utm37C, utm38C, utm39C, utm40C,
        utm41C, utm42C, utm43C, utm44C, utm45C, utm46C, utm47C, utm48C, utm49C, utm50C,
        utm51C, utm52C, utm53C, utm54C, utm55C, utm56C, utm57C, utm58C, utm59C, utm60C,

        utm1D, utm2D, utm3D, utm4D, utm5D, utm6D, utm7D, utm8D, utm9D, utm10D,
        utm11D, utm12D, utm13D, utm14D, utm15D, utm16D, utm17D, utm18D, utm19D, utm20D,
        utm21D, utm22D, utm23D, utm24D, utm25D, utm26D, utm27D, utm28D, utm29D, utm30D,
        utm31D, utm32D, utm33D, utm34D, utm35D, utm36D, utm37D, utm38D, utm39D, utm40D,
        utm41D, utm42D, utm43D, utm44D, utm45D, utm46D, utm47D, utm48D, utm49D, utm50D,
        utm51D, utm52D, utm53D, utm54D, utm55D, utm56D, utm57D, utm58D, utm59D, utm60D,

        utm1E, utm2E, utm3E, utm4E, utm5E, utm6E, utm7E, utm8E, utm9E, utm10E,
        utm11E, utm12E, utm13E, utm14E, utm15E, utm16E, utm17E, utm18E, utm19E, utm20E,
        utm21E, utm22E, utm23E, utm24E, utm25E, utm26E, utm27E, utm28E, utm29E, utm30E,
        utm31E, utm32E, utm33E, utm34E, utm35E, utm36E, utm37E, utm38E, utm39E, utm40E,
        utm41E, utm42E, utm43E, utm44E, utm45E, utm46E, utm47E, utm48E, utm49E, utm50E,
        utm51E, utm52E, utm53E, utm54E, utm55E, utm56E, utm57E, utm58E, utm59E, utm60E,

        utm1F, utm2F, utm3F, utm4F, utm5F, utm6F, utm7F, utm8F, utm9F, utm10F,
        utm11F, utm12F, utm13F, utm14F, utm15F, utm16F, utm17F, utm18F, utm19F, utm20F,
        utm21F, utm22F, utm23F, utm24F, utm25F, utm26F, utm27F, utm28F, utm29F, utm30F,
        utm31F, utm32F, utm33F, utm34F, utm35F, utm36F, utm37F, utm38F, utm39F, utm40F,
        utm41F, utm42F, utm43F, utm44F, utm45F, utm46F, utm47F, utm48F, utm49F, utm50F,
        utm51F, utm52F, utm53F, utm54F, utm55F, utm56F, utm57F, utm58F, utm59F, utm60F,

        utm1G, utm2G, utm3G, utm4G, utm5G, utm6G, utm7G, utm8G, utm9G, utm10G,
        utm11G, utm12G, utm13G, utm14G, utm15G, utm16G, utm17G, utm18G, utm19G, utm20G,
        utm21G, utm22G, utm23G, utm24G, utm25G, utm26G, utm27G, utm28G, utm29G, utm30G,
        utm31G, utm32G, utm33G, utm34G, utm35G, utm36G, utm37G, utm38G, utm39G, utm40G,
        utm41G, utm42G, utm43G, utm44G, utm45G, utm46G, utm47G, utm48G, utm49G, utm50G,
        utm51G, utm52G, utm53G, utm54G, utm55G, utm56G, utm57G, utm58G, utm59G, utm60G,

        utm1H, utm2H, utm3H, utm4H, utm5H, utm6H, utm7H, utm8H, utm9H, utm10H,
        utm11H, utm12H, utm13H, utm14H, utm15H, utm16H, utm17H, utm18H, utm19H, utm20H,
        utm21H, utm22H, utm23H, utm24H, utm25H, utm26H, utm27H, utm28H, utm29H, utm30H,
        utm31H, utm32H, utm33H, utm34H, utm35H, utm36H, utm37H, utm38H, utm39H, utm40H,
        utm41H, utm42H, utm43H, utm44H, utm45H, utm46H, utm47H, utm48H, utm49H, utm50H,
        utm51H, utm52H, utm53H, utm54H, utm55H, utm56H, utm57H, utm58H, utm59H, utm60H,

        utm1J, utm2J, utm3J, utm4J, utm5J, utm6J, utm7J, utm8J, utm9J, utm10J,
        utm11J, utm12J, utm13J, utm14J, utm15J, utm16J, utm17J, utm18J, utm19J, utm20J,
        utm21J, utm22J, utm23J, utm24J, utm25J, utm26J, utm27J, utm28J, utm29J, utm30J,
        utm31J, utm32J, utm33J, utm34J, utm35J, utm36J, utm37J, utm38J, utm39J, utm40J,
        utm41J, utm42J, utm43J, utm44J, utm45J, utm46J, utm47J, utm48J, utm49J, utm50J,
        utm51J, utm52J, utm53J, utm54J, utm55J, utm56J, utm57J, utm58J, utm59J, utm60J,

        utm1K, utm2K, utm3K, utm4K, utm5K, utm6K, utm7K, utm8K, utm9K, utm10K,
        utm11K, utm12K, utm13K, utm14K, utm15K, utm16K, utm17K, utm18K, utm19K, utm20K,
        utm21K, utm22K, utm23K, utm24K, utm25K, utm26K, utm27K, utm28K, utm29K, utm30K,
        utm31K, utm32K, utm33K, utm34K, utm35K, utm36K, utm37K, utm38K, utm39K, utm40K,
        utm41K, utm42K, utm43K, utm44K, utm45K, utm46K, utm47K, utm48K, utm49K, utm50K,
        utm51K, utm52K, utm53K, utm54K, utm55K, utm56K, utm57K, utm58K, utm59K, utm60K,

        utm1L, utm2L, utm3L, utm4L, utm5L, utm6L, utm7L, utm8L, utm9L, utm10L,
        utm11L, utm12L, utm13L, utm14L, utm15L, utm16L, utm17L, utm18L, utm19L, utm20L,
        utm21L, utm22L, utm23L, utm24L, utm25L, utm26L, utm27L, utm28L, utm29L, utm30L,
        utm31L, utm32L, utm33L, utm34L, utm35L, utm36L, utm37L, utm38L, utm39L, utm40L,
        utm41L, utm42L, utm43L, utm44L, utm45L, utm46L, utm47L, utm48L, utm49L, utm50L,
        utm51L, utm52L, utm53L, utm54L, utm55L, utm56L, utm57L, utm58L, utm59L, utm60L,

        utm1M, utm2M, utm3M, utm4M, utm5M, utm6M, utm7M, utm8M, utm9M, utm10M,
        utm11M, utm12M, utm13M, utm14M, utm15M, utm16M, utm17M, utm18M, utm19M, utm20M,
        utm21M, utm22M, utm23M, utm24M, utm25M, utm26M, utm27M, utm28M, utm29M, utm30M,
        utm31M, utm32M, utm33M, utm34M, utm35M, utm36M, utm37M, utm38M, utm39M, utm40M,
        utm41M, utm42M, utm43M, utm44M, utm45M, utm46M, utm47M, utm48M, utm49M, utm50M,
        utm51M, utm52M, utm53M, utm54M, utm55M, utm56M, utm57M, utm58M, utm59M, utm60M,

        utm1N, utm2N, utm3N, utm4N, utm5N, utm6N, utm7N, utm8N, utm9N, utm10N,
        utm11N, utm12N, utm13N, utm14N, utm15N, utm16N, utm17N, utm18N, utm19N, utm20N,
        utm21N, utm22N, utm23N, utm24N, utm25N, utm26N, utm27N, utm28N, utm29N, utm30N,
        utm31N, utm32N, utm33N, utm34N, utm35N, utm36N, utm37N, utm38N, utm39N, utm40N,
        utm41N, utm42N, utm43N, utm44N, utm45N, utm46N, utm47N, utm48N, utm49N, utm50N,
        utm51N, utm52N, utm53N, utm54N, utm55N, utm56N, utm57N, utm58N, utm59N, utm60N,

        utm1P, utm2P, utm3P, utm4P, utm5P, utm6P, utm7P, utm8P, utm9P, utm10P,
        utm11P, utm12P, utm13P, utm14P, utm15P, utm16P, utm17P, utm18P, utm19P, utm20P,
        utm21P, utm22P, utm23P, utm24P, utm25P, utm26P, utm27P, utm28P, utm29P, utm30P,
        utm31P, utm32P, utm33P, utm34P, utm35P, utm36P, utm37P, utm38P, utm39P, utm40P,
        utm41P, utm42P, utm43P, utm44P, utm45P, utm46P, utm47P, utm48P, utm49P, utm50P,
        utm51P, utm52P, utm53P, utm54P, utm55P, utm56P, utm57P, utm58P, utm59P, utm60P,

        utm1Q, utm2Q, utm3Q, utm4Q, utm5Q, utm6Q, utm7Q, utm8Q, utm9Q, utm10Q,
        utm11Q, utm12Q, utm13Q, utm14Q, utm15Q, utm16Q, utm17Q, utm18Q, utm19Q, utm20Q,
        utm21Q, utm22Q, utm23Q, utm24Q, utm25Q, utm26Q, utm27Q, utm28Q, utm29Q, utm30Q,
        utm31Q, utm32Q, utm33Q, utm34Q, utm35Q, utm36Q, utm37Q, utm38Q, utm39Q, utm40Q,
        utm41Q, utm42Q, utm43Q, utm44Q, utm45Q, utm46Q, utm47Q, utm48Q, utm49Q, utm50Q,
        utm51Q, utm52Q, utm53Q, utm54Q, utm55Q, utm56Q, utm57Q, utm58Q, utm59Q, utm60Q,

        utm1R, utm2R, utm3R, utm4R, utm5R, utm6R, utm7R, utm8R, utm9R, utm10R,
        utm11R, utm12R, utm13R, utm14R, utm15R, utm16R, utm17R, utm18R, utm19R, utm20R,
        utm21R, utm22R, utm23R, utm24R, utm25R, utm26R, utm27R, utm28R, utm29R, utm30R,
        utm31R, utm32R, utm33R, utm34R, utm35R, utm36R, utm37R, utm38R, utm39R, utm40R,
        utm41R, utm42R, utm43R, utm44R, utm45R, utm46R, utm47R, utm48R, utm49R, utm50R,
        utm51R, utm52R, utm53R, utm54R, utm55R, utm56R, utm57R, utm58R, utm59R, utm60R,

        utm1S, utm2S, utm3S, utm4S, utm5S, utm6S, utm7S, utm8S, utm9S, utm10S,
        utm11S, utm12S, utm13S, utm14S, utm15S, utm16S, utm17S, utm18S, utm19S, utm20S,
        utm21S, utm22S, utm23S, utm24S, utm25S, utm26S, utm27S, utm28S, utm29S, utm30S,
        utm31S, utm32S, utm33S, utm34S, utm35S, utm36S, utm37S, utm38S, utm39S, utm40S,
        utm41S, utm42S, utm43S, utm44S, utm45S, utm46S, utm47S, utm48S, utm49S, utm50S,
        utm51S, utm52S, utm53S, utm54S, utm55S, utm56S, utm57S, utm58S, utm59S, utm60S,

        utm1T, utm2T, utm3T, utm4T, utm5T, utm6T, utm7T, utm8T, utm9T, utm10T,
        utm11T, utm12T, utm13T, utm14T, utm15T, utm16T, utm17T, utm18T, utm19T, utm20T,
        utm21T, utm22T, utm23T, utm24T, utm25T, utm26T, utm27T, utm28T, utm29T, utm30T,
        utm31T, utm32T, utm33T, utm34T, utm35T, utm36T, utm37T, utm38T, utm39T, utm40T,
        utm41T, utm42T, utm43T, utm44T, utm45T, utm46T, utm47T, utm48T, utm49T, utm50T,
        utm51T, utm52T, utm53T, utm54T, utm55T, utm56T, utm57T, utm58T, utm59T, utm60T,

        utm1U, utm2U, utm3U, utm4U, utm5U, utm6U, utm7U, utm8U, utm9U, utm10U,
        utm11U, utm12U, utm13U, utm14U, utm15U, utm16U, utm17U, utm18U, utm19U, utm20U,
        utm21U, utm22U, utm23U, utm24U, utm25U, utm26U, utm27U, utm28U, utm29U, utm30U,
        utm31U, utm32U, utm33U, utm34U, utm35U, utm36U, utm37U, utm38U, utm39U, utm40U,
        utm41U, utm42U, utm43U, utm44U, utm45U, utm46U, utm47U, utm48U, utm49U, utm50U,
        utm51U, utm52U, utm53U, utm54U, utm55U, utm56U, utm57U, utm58U, utm59U, utm60U,

        utm1V, utm2V, utm3V, utm4V, utm5V, utm6V, utm7V, utm8V, utm9V, utm10V,
        utm11V, utm12V, utm13V, utm14V, utm15V, utm16V, utm17V, utm18V, utm19V, utm20V,
        utm21V, utm22V, utm23V, utm24V, utm25V, utm26V, utm27V, utm28V, utm29V, utm30V,
        utm31V, utm32V, utm33V, utm34V, utm35V, utm36V, utm37V, utm38V, utm39V, utm40V,
        utm41V, utm42V, utm43V, utm44V, utm45V, utm46V, utm47V, utm48V, utm49V, utm50V,
        utm51V, utm52V, utm53V, utm54V, utm55V, utm56V, utm57V, utm58V, utm59V, utm60V,

        utm1W, utm2W, utm3W, utm4W, utm5W, utm6W, utm7W, utm8W, utm9W, utm10W,
        utm11W, utm12W, utm13W, utm14W, utm15W, utm16W, utm17W, utm18W, utm19W, utm20W,
        utm21W, utm22W, utm23W, utm24W, utm25W, utm26W, utm27W, utm28W, utm29W, utm30W,
        utm31W, utm32W, utm33W, utm34W, utm35W, utm36W, utm37W, utm38W, utm39W, utm40W,
        utm41W, utm42W, utm43W, utm44W, utm45W, utm46W, utm47W, utm48W, utm49W, utm50W,
        utm51W, utm52W, utm53W, utm54W, utm55W, utm56W, utm57W, utm58W, utm59W, utm60W,

        utm1X, utm2X, utm3X, utm4X, utm5X, utm6X, utm7X, utm8X, utm9X, utm10X,
        utm11X, utm12X, utm13X, utm14X, utm15X, utm16X, utm17X, utm18X, utm19X, utm20X,
        utm21X, utm22X, utm23X, utm24X, utm25X, utm26X, utm27X, utm28X, utm29X, utm30X,
        utm31X, /*utm32X,*/ utm33X, /*utm34X,*/ utm35X, /* utm36X, */ utm37X, utm38X, utm39X, utm40X,
        utm41X, utm42X, utm43X, utm44X, utm45X, utm46X, utm47X, utm48X, utm49X, utm50X,
        utm51X, utm52X, utm53X, utm54X, utm55X, utm56X, utm57X, utm58X, utm59X, utm60X,
        // ReSharper restore InconsistentNaming
#pragma warning restore 1591
    }

}
