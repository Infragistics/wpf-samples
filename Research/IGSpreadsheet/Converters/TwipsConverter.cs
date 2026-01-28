using System;
using System.Windows.Data;

namespace IGSpreadsheet.Converters
{
    public class TwipsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int result = (int)value / 20;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double result = (double)value * 20;
            return System.Convert.ToInt32(result);
        }
    }
}
