using System;
using System.Globalization;
using System.Windows.Data;

namespace IGReporting.Converters
{
    /// <summary>
    /// This convertor adds 'Page' label to page number
    /// </summary>
    public class PageNumberDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Let Parse throw an exception if the input is bad
            int num;
            int.TryParse(value.ToString(), out num);
            return "Page " + num;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
