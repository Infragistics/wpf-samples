using System;
using System.Windows.Data;

namespace IGGantt.Samples.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool?))
            {
                throw new InvalidOperationException("The target must be a boolean");
            }

            if (value != null)
            {
                return !(bool)value;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            return value;
        }

        #endregion
    }
}
