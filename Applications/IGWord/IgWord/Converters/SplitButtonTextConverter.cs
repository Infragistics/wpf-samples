using System;
using System.Windows.Data;

namespace IgWord.Converters
{
    public class SplitButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? ResourceStrings.ResourceStrings.Text_RemoveSplit : ResourceStrings.ResourceStrings.Text_Split;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("SplitButtonTextConverter.ConvertBack");
        }
    }
}
