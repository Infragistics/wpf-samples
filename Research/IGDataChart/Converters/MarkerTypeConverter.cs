using System;
using System.Globalization;
using System.Windows.Data;
using Infragistics.Controls.Charts;

namespace IGDataChart.Converters
{
    public class MarkerTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MarkerType)Enum.Parse(typeof(MarkerType), value.ToString(), true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}