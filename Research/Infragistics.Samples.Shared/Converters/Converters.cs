using System;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    public class SelectedIndexToDataConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                    return "CODE 39";
                case 1:
                    return "code 39";
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                                  System.Globalization.CultureInfo culture)
        {
            return string.Empty;
        }

        #endregion
    }

    public class BooleanInversionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }
    }
}