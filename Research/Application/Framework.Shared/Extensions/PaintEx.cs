
using Infragistics.XamarinForms;
using System;
using System.Collections.Generic;
using System.Text;

#if Xamarin
using Xamarin.Forms;
#endif

namespace System
{
    public static class PaintEx
    {
        public static Xamarin.Forms.Color ToColor(this Paint paint)
        {
            var a = paint.A / 255.0;
            var b = paint.B / 255.0;
            var r = paint.R / 255.0;
            var g = paint.G / 255.0;
            var color = Xamarin.Forms.Color.FromRgba(r, g, b, a);
            return color;
        }
        public static Color ToGraphicColor(this Paint paint)
        { 
            var color = Color.FromRgba(paint.R, paint.G, paint.B, paint.A);
            return color;
        }
        //public static SolidColorBrush ToBrush2(this Paint paint, double opacity = double.NaN)
        //{
        //    return paint.ToColor().ToSolidBrush();
        //}
        public static SolidColorBrush ToSolidBrush(this Paint paint, double opacity = double.NaN)
        {
            return new SolidColorBrush (paint.ToGraphicColor());
        }
    }
}
