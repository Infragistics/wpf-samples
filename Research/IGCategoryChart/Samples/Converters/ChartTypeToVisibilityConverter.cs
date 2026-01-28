using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace IGCategoryChart.Converters
{
    class ChartTypeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Infragistics.Controls.Charts.CategoryChartType)
            {
                var chartType = (Infragistics.Controls.Charts.CategoryChartType)value;
                if (value.Equals(Infragistics.Controls.Charts.CategoryChartType.Auto))
                    return Visibility.Visible;
            }

            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
