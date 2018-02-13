using System;
using System.Windows;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class SplitVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
            {
                return false;
            }

            return (int)values[0] > 0 || (int)values[1] > 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("SplitVisibilityConverter.ConvertBack");
        }
    }
}
