using System;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class ValueToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null && parameter == null)
                return Visibility.Collapsed;

            return (value.ToString() == parameter.ToString()) ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("ValueToVisibilityConverter.ConvertBack");
        }
    }
}
