using System;
using System.Globalization;
using System.Windows.Data;

namespace IGSchedule.Converters
{
    public class BooleanInverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool ret = false;
                if (bool.TryParse(value.ToString(), out ret))
                {
                    return !ret;
                }
            }
            //return DependencyProperty.UnsetValue;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return DependencyProperty.UnsetValue;
            return value;
        }
    }
}
