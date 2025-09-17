using System;
using System.Windows;
using System.Windows.Data;

namespace IGSpreadsheet.Converters
{
    public class AlignmentConv : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (parameter is string)
                return value != null && string.Compare((string)parameter, value.ToString(), StringComparison.CurrentCulture) == 0;

            return object.Equals(value, parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
