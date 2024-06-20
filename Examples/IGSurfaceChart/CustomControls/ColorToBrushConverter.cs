using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;


namespace IGSurfaceChart.CustomControls
{
    public class ColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = (Color) value;
            return new SolidColorBrush() { Color = color };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
