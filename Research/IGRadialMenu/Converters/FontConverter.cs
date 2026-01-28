using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace IGRadialMenu.Converters
{
    public class FontConverter : IValueConverter
    {
        private FontFamily defaultFont;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                return new FontFamily((string)value);
            }
            if (defaultFont == null)
            {
                RichTextBox rtb = new RichTextBox();
                defaultFont = rtb.FontFamily;
            }
            return defaultFont;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
