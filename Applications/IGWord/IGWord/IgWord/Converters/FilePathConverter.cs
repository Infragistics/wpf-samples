using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IgWord.Converters
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
