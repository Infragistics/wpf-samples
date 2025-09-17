using System;
using System.Globalization;
using System.Windows.Data;

namespace IGMap.Converters
{
    public class MapAnimationDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return TimeSpan.FromMilliseconds(1000);
            double duration = System.Convert.ToDouble(value);

            return TimeSpan.FromMilliseconds(duration);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }
    }
}