using System;
using System.Windows;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class BooleanToConfigurableVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility falseReturnValue = Visibility.Collapsed;
            if (parameter is Visibility)
            {
                falseReturnValue = (Visibility)parameter;
            }
            if (value is bool)
            {
                return (bool)value ? Visibility.Visible : falseReturnValue;
            }
            return falseReturnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("BooleanToConfigurableVisibilityConverter.ConvertBack");
        }
    }
}
