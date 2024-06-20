using System;
using System.Windows.Data;
using System.Windows.Media;

namespace IGRichTextEditor.Converters
{
    public class ForeColorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is Color)
            {
                Color c = (Color)value;
                SolidColorBrush scb = new SolidColorBrush(c);
                return scb;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
