using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace Infragistics.Samples.Shared.Converters
{
    public class ColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Color)
            {
                return new SolidColorBrush((Color)value);
            }
            return null;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var brush = value as SolidColorBrush;
            if (brush != null)
            {
                return brush.Color;
            }
            return null;
        }
    }
}
