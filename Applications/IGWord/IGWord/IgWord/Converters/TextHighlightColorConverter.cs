using Infragistics.Documents.RichText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace IgWord.Converters
{
    public class TextHighlightColorConverter :IValueConverter
    {
        

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is HighlightColor)
            {
                var highlightColor = (HighlightColor)value;
                if (highlightColor == HighlightColor.None)
                {
                    return Colors.White;
                }
                else
                {
                    return (Color)ColorConverter.ConvertFromString(value.ToString());
                }
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
