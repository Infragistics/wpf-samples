using System;
using System.Windows;
using System.Windows.Data;

namespace IGCalculationManager.Converters
{
    public class StringToIntConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format("${0:0.00}", value);
        }

        #endregion
    }
}
