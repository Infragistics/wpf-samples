using Infragistics;
using System;
using System.Globalization;
using System.Windows;

namespace IgExcel.Converters
{
    public class ValueToBooleanConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string)
                return value != null && string.Compare((string)parameter, value.ToString(), StringComparison.CurrentCulture) == 0;

            return object.Equals(value, parameter);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
