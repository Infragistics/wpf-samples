using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using Infragistics.Documents.RichText;

namespace IGRichTextEditor.Converters
{
    public class HighlightColorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is HighlightColor)
            {
                switch ((HighlightColor)value)
                {
                    case HighlightColor.None:
                        return (ImageBrush)parameter;
                    case HighlightColor.Black:
                        return new SolidColorBrush(Colors.Black);
                    case HighlightColor.Blue:
                        return new SolidColorBrush(Colors.Blue);
                    case HighlightColor.Cyan:
                        return new SolidColorBrush(Colors.Cyan);
                    case HighlightColor.Green:
                        return new SolidColorBrush(Colors.Green);
                    case HighlightColor.Magenta:
                        return new SolidColorBrush(Colors.Magenta);
                    case HighlightColor.Red:
                        return new SolidColorBrush(Colors.Red);
                    case HighlightColor.Yellow:
                        return new SolidColorBrush(Colors.Yellow);
                    case HighlightColor.White:
                        return new SolidColorBrush(Colors.White);
                    case HighlightColor.DarkBlue:
                        return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x8B));
                    case HighlightColor.DarkCyan:
                        return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x8B, 0x8B));
                    case HighlightColor.DarkGreen:
                        return new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x64, 0x00));
                    case HighlightColor.DarkMagenta:
                        return new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0x00, 0x80));
                    case HighlightColor.DarkRed:
                        return new SolidColorBrush(Color.FromArgb(0xFF, 0x8B, 0x00, 0x00));
                    case HighlightColor.DarkYellow:
                        return new SolidColorBrush(Color.FromArgb(0xFF, 0x80, 0x80, 0x00));
                    case HighlightColor.DarkGray:
                        return new SolidColorBrush(Colors.DarkGray);
                    case HighlightColor.LightGray:
                        return new SolidColorBrush(Colors.LightGray);
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
