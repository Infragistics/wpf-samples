using System;
using System.Globalization;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
   
    /// <summary>
    /// Converts TimeSpan object to a total milliseconds string 
    /// </summary>
    public class TimeSpanToMillisecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;

            var ts = value is TimeSpan ? (TimeSpan)value : new TimeSpan(0, 0, 0, 0, 0);
            return (int)ts.TotalMilliseconds;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return TimeSpan.FromMilliseconds(0);

            var duration = System.Convert.ToDouble(value);
            return TimeSpan.FromMilliseconds(duration);
        }
    }
    /// <summary>
    /// Converts TimeSpan object to a total seconds string 
    /// </summary>
    public class TimeSpanToSecondsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return 0;

            var ts = value is TimeSpan ? (TimeSpan)value : new TimeSpan(0, 0, 0, 0, 0);
            return ts.TotalSeconds;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return TimeSpan.FromMilliseconds(0);

            var duration = System.Convert.ToDouble(value);
            return TimeSpan.FromSeconds(duration);
        }
    }
}