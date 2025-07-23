using System;
using System.Windows.Data;

namespace IGDataCards.Converters
{
    public class DblConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            return String.Format(culture, "{0:N2}", value);
        }


        public object ConvertBack(object value, Type targetType, object parameter,
                              System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
