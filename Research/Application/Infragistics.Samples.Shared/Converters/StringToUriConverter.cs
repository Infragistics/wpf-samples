using System;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    public class StringToUriConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string uriString = value as string;

            if (String.IsNullOrEmpty(uriString))
            {
                return value;
            }
            else
            {
                Uri uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
                return uri;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
