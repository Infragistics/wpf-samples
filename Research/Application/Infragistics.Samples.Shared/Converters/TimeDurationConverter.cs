using System;
using System.Globalization;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    /// <summary>
    /// Converts duration represented in milliseconds to TimeSpan object
    /// </summary>
    public class TimeDurationConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return TimeSpan.FromMilliseconds(1000);
            var duration = System.Convert.ToDouble(value);

            return TimeSpan.FromMilliseconds(duration);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new NotImplementedException();
        }
    }

}