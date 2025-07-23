using System;
using System.Windows.Data;

namespace IGPropertyGrid.Converters
{
    public class IntegerToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is int)
            {
                return System.Convert.ToDouble((int)value);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is double)
            {
                double d = (double)value;
                return System.Convert.ToInt32(d);
            }
            return null;
        }
    }
}
