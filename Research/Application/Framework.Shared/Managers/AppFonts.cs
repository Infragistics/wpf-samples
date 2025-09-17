using System;
using Xamarin.Forms;

namespace Infragistics.Framework 
{
    /// <summary> Represents pre-defined font sizes with extra value compared to <see cref="NamedSize"/> </summary>
    /// <remarks> The exact pixel-value depends on the platform on which an app runs </remarks>
    public enum FontSize
    {
        Default, 
        Micro, 
        Small, 
        Medium, 
        Large,
        Huge,
    }
    /// <summary> Represents a class that manages fonts across multiple platforms </summary>
    public static class AppFonts
    {
        // Gets Micro font size with percentage scale  
        public static readonly double SizeMicro_120 = FontSize.Micro.GetScaleSize(1.2);
        public static readonly double SizeMicro_110 = FontSize.Micro.GetScaleSize(1.1);
        public static readonly double SizeMicro     = FontSize.Micro.GetScaleSize();
        public static readonly double SizeMicro_90  = FontSize.Micro.GetScaleSize(.9);
        public static readonly double SizeMicro_80  = FontSize.Micro.GetScaleSize(.8);
        public static readonly double SizeMicro_70  = FontSize.Micro.GetScaleSize(.7);

        // Gets Small font size with percentage scale  
        public static readonly double SizeSmall_120 = FontSize.Small.GetScaleSize(1.2);
        public static readonly double SizeSmall_110 = FontSize.Small.GetScaleSize(1.1);
        public static readonly double SizeSmall     = FontSize.Small.GetScaleSize();
        public static readonly double SizeSmall_90  = FontSize.Small.GetScaleSize(.9);
        public static readonly double SizeSmall_80  = FontSize.Small.GetScaleSize(.8);
        public static readonly double SizeSmall_70  = FontSize.Small.GetScaleSize(.7);

        // Gets Medium font size with percentage scale  
        public static readonly double SizeMedium_120 = FontSize.Medium.GetScaleSize(1.2);
        public static readonly double SizeMedium_110 = FontSize.Medium.GetScaleSize(1.1);
        public static readonly double SizeMedium     = FontSize.Medium.GetScaleSize(); 
        public static readonly double SizeMedium_90  = FontSize.Medium.GetScaleSize(.9); 
        public static readonly double SizeMedium_80  = FontSize.Medium.GetScaleSize(.8); 
        public static readonly double SizeMedium_70  = FontSize.Medium.GetScaleSize(.7);

        // Gets Large font size with percentage scale  
        public static readonly double SizeLarge_120 = FontSize.Large.GetScaleSize(1.2);
        public static readonly double SizeLarge_110 = FontSize.Large.GetScaleSize(1.1);
        public static readonly double SizeLarge     = FontSize.Large.GetScaleSize();
        public static readonly double SizeLarge_90  = FontSize.Large.GetScaleSize(.9);
        public static readonly double SizeLarge_80  = FontSize.Large.GetScaleSize(.8);
        public static readonly double SizeLarge_70  = FontSize.Large.GetScaleSize(.7);

        // Gets Huge font size with percentage scale  
        public static readonly double SizeHuge_120 = FontSize.Huge.GetScaleSize(1.2);
        public static readonly double SizeHuge_110 = FontSize.Huge.GetScaleSize(1.1);
        public static readonly double SizeHuge     = FontSize.Huge.GetScaleSize();
        public static readonly double SizeHuge_90  = FontSize.Huge.GetScaleSize(.9);
        public static readonly double SizeHuge_80  = FontSize.Huge.GetScaleSize(.8);
        public static readonly double SizeHuge_70  = FontSize.Huge.GetScaleSize(.7);

        /// <summary> Gets Default font based on platform/screen form factor </summary>
        public static readonly double SizeDefault = FontSize.Default.GetScaleSize();
     
        public static double GetNamedSize(NamedSize namedSize, Type type = null)
        {
            if (type == null) type = typeof(Label);
             
            return Device.GetNamedSize(namedSize, type);
        }
        /// <summary> Gets scaled font size based on platform/screen form factor </summary>
        public static double GetScaleSize(this FontSize fontSize, double fontFactor = 1.0)
        {
            double size;
            if (fontSize == FontSize.Default)
            {
                size = GetNamedSize(NamedSize.Default); 
            } 
            //var fontFactor = DeviceEx.OnScreen(0.2, 0.1, 0.1); 
            //if (Device.Idiom == TargetIdiom.Tablet)
            //{
            //    // increase font size for tablets
            //    if (fontSize == FontSize.Smallest)      fontSize = FontSize.Tiny;
            //    else if (fontSize == FontSize.Tiny)     fontSize = FontSize.Micro;
            //    else if (fontSize == FontSize.Micro)    fontSize = FontSize.Small;
            //    else if (fontSize == FontSize.Small)    fontSize = FontSize.Medium;
            //    else if (fontSize == FontSize.Medium)   fontSize = FontSize.Large;
            //    else if (fontSize == FontSize.Large)    fontSize = FontSize.Huge;
            //}  
            else if (fontSize == FontSize.Micro)
            {
                size = GetNamedSize(NamedSize.Micro);
            }       
            else if (fontSize == FontSize.Small)
            {
                size = GetNamedSize(NamedSize.Small);  
            }  
            else if (fontSize == FontSize.Medium)
            {
                size = GetNamedSize(NamedSize.Medium);
            }  
            else if (fontSize == FontSize.Large)
            {
                size = GetNamedSize(NamedSize.Large);
            }   
            else if (fontSize == FontSize.Huge)
            {
                size = GetNamedSize(NamedSize.Large);
                size *= 1.5 ; // scale up once
            }
            else
            {
                size = GetNamedSize(NamedSize.Medium);
            }
            //http://www.imore.com/iphone-se-screen-sizes-and-interfaces-compared


            size *= fontFactor; // scale by factor 

            var min = Math.Min(Mobile.ScreenHeight, Mobile.ScreenWidth);
            if (min < 750)
            { 
                size *= 0.9; // scale down font for small screen sizes
            }

            if (Mobile.HasJapaneseCulture)
            {
                size *= 0.9; // scale down font for large Japanese symbols
                if (min < 1080)
                {
                    size *= 0.9; // scale down font for small screen sizes
                }
            }

            return size;
        } 
    }
     
}