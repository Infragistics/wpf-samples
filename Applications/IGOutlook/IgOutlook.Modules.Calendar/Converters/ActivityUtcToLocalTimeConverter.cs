using System;
using System.Windows.Data;

namespace IgOutlook.Modules.Calendar.Converters
{
    public class ActivityUtcToLocalTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var utc = (DateTime)value; 
            var local = new DateTime(utc.Year, utc.Month, utc.Day, utc.Hour, utc.Minute, utc.Second, DateTimeKind.Utc).ToLocalTime();
            return local;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                var local = (DateTime)value;
                var utc = new DateTime(local.Year, local.Month, local.Day, local.Hour, local.Minute, local.Second, DateTimeKind.Local).ToUniversalTime();
                return utc;
            }
            return null;
        }
    }
}
