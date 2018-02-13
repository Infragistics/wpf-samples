using Infragistics;
using System;
using System.Globalization;
using System.Windows;

namespace IgExcel.Converters
{
    public class TwipsToPointsConverter : ValueConverterBase
    {
        private static int? ToInt(object value, CultureInfo culture)
        {
            if (value == null)
                return null;
            else
                return (int?)System.Convert.ChangeType(value, typeof(int));
        }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = ToInt(value, culture);
            return i == null ? DependencyProperty.UnsetValue : i / 20;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = ToInt(value, culture);
            return i == null ? DependencyProperty.UnsetValue : i * 20;
        }
    }

}
