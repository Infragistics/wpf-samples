using System;
using System.Globalization;

#if Xamarin
using Xamarin.Forms;
using Infragistics.Core;
#else
using System.Windows;
#endif

namespace Infragistics.Framework 
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible;
            }

            //if ((Visibility)value == Visibility.Visible)
            //    return true;

            return false;
        } 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }
    }

    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }

            return Visibility.Visible;
        } 
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
            {
                return (Visibility)value == Visibility.Visible;
            }

            return false;
        }
    }

    public class DoubleRoundConverter : IValueConverter
    {
        public int Decimals { get; set; }

        public DoubleRoundConverter()
        {
            Decimals = 0;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                var round = Math.Round((double)value, Decimals);
                return round;
            }
                
            return 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                var round = Math.Round((double)value, Decimals);
                return round;
            }
            return 0.0;
        }
    }

    public class IntToDoubleConverter : IValueConverter
    {   
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            { 
                return System.Convert.ToDouble(value);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                return System.Convert.ToInt32(value); 
            }
            return 0;
        }
    }
}
