using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace Infragistics.Framework
{
    [ValueConversion(typeof(object), typeof(Brush))]
    public class NullToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return Brushes.Red;
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return Brushes.Red;
            }
            return value;
        }
    }

    [ValueConversion(typeof(object), typeof(double))]
    public class NullToDoubleNConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? double.NaN : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(object), typeof(double))]
    public class NullToDouble0Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null || double.IsNaN((double)value) ? 0.0 : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    [ValueConversion(typeof(Thickness), typeof(double))]
    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness)value;

            return thickness.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var number = (double)value;
            return new Thickness(number);
        }
    }

    [ValueConversion(typeof(object), typeof(Brush))]
    public class BrushToStingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "NUL";
            }

            return value.ToString();
            //return BrushUtil.ToRgba(value as Brush);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
