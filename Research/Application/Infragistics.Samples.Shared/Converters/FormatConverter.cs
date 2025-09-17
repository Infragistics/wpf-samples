using System;
using System.Globalization;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    public class FormatConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return value;

            string result = value.ToString();
            string format = parameter.ToString();

            if (format == "DATE" && value is DateTime)
            {
                result = ((DateTime)value).ToShortDateString();
            }
            else if (!string.IsNullOrEmpty(format) && value is TimeSpan)
            {
                DateTime ts = new DateTime(((TimeSpan)value).Ticks);
                result = ts.ToString(format);
            }
            else if (!string.IsNullOrEmpty(format))
            {
                result = string.Format(culture, format, value);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
