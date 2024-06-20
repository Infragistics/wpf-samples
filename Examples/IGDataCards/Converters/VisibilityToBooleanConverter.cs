using System;
using System.Windows.Controls;
using System.Windows.Data;

namespace IGDataCards.Converters
{
    public class VisibilityToBooleanConverter : IValueConverter
    {
        private BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return converter.ConvertBack(value, targetType, parameter, culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return converter.Convert(value, targetType, parameter, culture);
        }

        #endregion
    }
}