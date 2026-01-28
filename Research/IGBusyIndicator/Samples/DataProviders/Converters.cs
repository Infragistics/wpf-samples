using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IGBusyIndicator.Samples.DataProviders
{
    public class TimeSpanToSecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double seconds = 0;
            if (value != null)
                seconds = ((TimeSpan)value).TotalSeconds;

            return seconds;            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var interval = TimeSpan.FromMilliseconds(0);
            if (value != null)
                interval = TimeSpan.FromSeconds(System.Convert.ToDouble(value));

            return interval;
        }  
    }

    public class ColorToSolidColorBrushValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush result = null;

            if (value is Color)
                result = new SolidColorBrush((Color)value);

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new Color();

            if (value is SolidColorBrush)
                result = ((SolidColorBrush)value).Color;

            return result;
        }
    }

    public class SolidColorBrushToColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = new Color();

            if (value is SolidColorBrush)
                result = ((SolidColorBrush)value).Color;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush result = null;

            if (value is Color)
                result = new SolidColorBrush((Color)value);

            return result;
        }
    }

    public class BooleanValueInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return !(bool)value;
            else
                return DependencyProperty.UnsetValue; // Fallback for non-boolean input values
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IntergerToThicknessValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thicknessValue = new Thickness(0);

            if (value != null)
            {
                thicknessValue = new Thickness(System.Convert.ToInt32(value));
            }

            return thicknessValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness && value != null)
            {
                var thickness = (Thickness)value;

                return thickness.Left;
            }

            return null;
        }
    }

    public class ThicknessToIntegerValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Thickness && value != null)
            {
                var thickness = (Thickness)value;

                return thickness.Left;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thicknessValue = new Thickness(0);

            if (value != null)
            {
                thicknessValue = new Thickness(System.Convert.ToInt32(value));
            }

            return thicknessValue;
        }
    }

    public class ToggleVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visiblity = Visibility.Collapsed;

            if (value != null)
            {
                visiblity = Visibility.Visible;
            }

            return visiblity;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
