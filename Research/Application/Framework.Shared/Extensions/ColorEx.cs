using System;
using System.Collections.Generic;

#if Xamarin
using Infragistics.XamarinForms;
using Xamarin.Forms;
#else
using System.Windows.Media;
#endif


#if Xamarin 
namespace Xamarin.Forms
#else
namespace System.Windows.Media
#endif 
{
    public static partial class ColorEx 
    {
        public static Color FromArgb(byte a, byte r, byte g, byte b)
        {
#if Xamarin
            return Color.FromRgba(r, g, b, a);
#else
            return Color.FromArgb(a, r, g, b);
#endif
        }

        //public static Color FromRgba(byte r, byte g, byte b, byte a)
        //{
        //    return Color.FromRgba(a, r, g, b);
        //}

        public static Color ToGraphicColor(this Color color)
        {  
            var a = Convert.ToByte(color.A * 255);
            var b = Convert.ToByte(color.B * 255);
            var r = Convert.ToByte(color.R * 255);
            var g = Convert.ToByte(color.G * 255);

            return FromArgb(a, r, g, b);
        }

        public static SolidColorBrush ToSolidBrush(this Color color, double opacity = double.NaN)
        { 
            var newColor = color.Opacity(opacity);
            return new SolidColorBrush(newColor.ToGraphicColor());  
        }
        
        public static Color Opacity(this Color color, double opacity)
        {
            Byte a, b, r, g;
#if WPF  
            a = color.A;
            b = color.B;
            r = color.R;
            g = color.G;
#else 
            a = Convert.ToByte(color.A * 255);
            b = Convert.ToByte(color.B * 255);
            r = Convert.ToByte(color.R * 255);
            g = Convert.ToByte(color.G * 255);
#endif
            if (!double.IsNaN(opacity) && !double.IsInfinity(opacity))
                a = (byte)(255 * opacity);

            var result = FromArgb(a, r, g, b);
            return result;
        } 
    } 
}