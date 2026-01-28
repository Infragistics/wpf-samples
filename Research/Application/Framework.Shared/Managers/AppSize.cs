using System;
using System.Diagnostics.CodeAnalysis;
using Xamarin.Forms;

namespace Infragistics.Framework 
{ 
    public static class AppSize
    {
        /// <summary> Defines factor for increasing font size on Desktop devices </summary>
        public const double SizeDesktopFactor = 3.0;
        /// <summary> Defines factor for increasing font size on Tablet devices </summary>
        public const double SizeTabletFactor = 2.0;
        /// <summary> Defines factor for increasing font size on Phone devices </summary>
        public const double SizePhoneFactor = 1.0;

        public static readonly double Small = OnPlatformSize(10, 10, 15);
        public static readonly double Normal = OnPlatformSize(15, 15, 20);
        public static readonly double Medium = OnPlatformSize(20, 20, 25);
        public static readonly double Large = OnPlatformSize(25, 25, 30);
        public static readonly double VeryLarge = OnPlatformSize(40, 40, 45);
        public static readonly double Huge = OnPlatformSize(50, 50, 55);

        public static double OnPlatformSize(double ios, double android, double winPhone)
        {
            var factor = 1.0;

            if (Device.Idiom == TargetIdiom.Phone)
                factor = SizePhoneFactor;
            else if (Device.Idiom == TargetIdiom.Tablet)
                factor = SizeTabletFactor;
            else if (Device.Idiom == TargetIdiom.Desktop)
                factor = SizeDesktopFactor;

            ios = (factor * ios);
            android = (factor * android);
            winPhone = (factor * winPhone);
                        
            return DeviceEx.OnPlatform(ios, android, winPhone);
        }
    }

}