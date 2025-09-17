using System;
using System.Windows;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    public class PercentValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dv = (double)value;
            if (dv > 0)
                return dv / 100;

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
