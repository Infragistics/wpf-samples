using System;
using System.Windows.Data;
using System.Windows.Media;

namespace IGDataGrid.Converters
{
    public class RateToGradientStopConverter : IValueConverter
    {
        // Defines a "bar chart type" background using a of a cell based on its value 
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dblValue = 0;

            if (double.TryParse(value.ToString(), out dblValue))
            {
                var linBrush = new LinearGradientBrush();
                linBrush.StartPoint = new System.Windows.Point(0, 0);
                linBrush.EndPoint = new System.Windows.Point(1, 0);
                linBrush.GradientStops.Add(new GradientStop(Color.FromRgb(189, 203, 107), dblValue / 100));
                linBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 255, 255), dblValue / 100 + 0.001));
                return linBrush;
            }
            else
                return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
