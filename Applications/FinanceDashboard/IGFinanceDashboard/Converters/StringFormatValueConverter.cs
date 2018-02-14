using System;
using System.Windows.Data;

namespace IGShowcase.FinanceDashboard.Converters
{
    public class StringFormatValueConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string format;

            if (value == null) return value;

            if (parameter == null)
            {
                format = "{0}";
            }
            else
            {
                format = (string)parameter;
            }

            return string.Format(format, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
