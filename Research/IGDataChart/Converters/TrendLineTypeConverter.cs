using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Infragistics.Controls.Charts;

namespace IGDataChart.Converters
{
    public class TrendLineTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (TrendLineType)Enum.Parse(typeof(TrendLineType), value.ToString(), true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
    public class TrendLinePeriodVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TrendLineType type = (TrendLineType)Enum.Parse(typeof(TrendLineType), value.ToString(), true);
            if (type == TrendLineType.ExponentialAverage ||
                type == TrendLineType.ModifiedAverage ||
                type == TrendLineType.WeightedAverage ||
                type == TrendLineType.SimpleAverage)
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

    }
}