using System;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    public class PrecisionConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Double.Parse(value.ToString()).ToString("#0.00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
