using System;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class FilePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var input = (string)value;

            if (string.IsNullOrEmpty(input))
            {
                return value;
            }
            else
            {
                return input.Replace("\\", " » ");
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var input = (string)value;

            if (string.IsNullOrEmpty(input))
            {
                return value;
            }
            else
            {
                return input.Replace(" » ", "\\");
            }
        }
    }
}
