using System;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class EndModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? ResourceStrings.ResourceStrings.Text_EndMode : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("EndModeConverter.EndModeConverter");
        }
    }
}
