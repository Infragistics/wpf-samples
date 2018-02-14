using IgOutlook.Business.Mail;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailReadUnreadConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MailFlags == false)
                return Visibility.Visible;

            MailFlags flags = (MailFlags)value;

            if ((flags & MailFlags.Seen) == MailFlags.Seen)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Visible;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
