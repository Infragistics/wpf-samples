using IgOutlook.Business.Mail;
using System;
using System.Windows.Data;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailStateImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is MailFlags == false) return null;

            MailFlags flags = (MailFlags)value;

            if ((flags & MailFlags.Forwarded) == MailFlags.Forwarded)
                return "/IgOutlook.Modules.Mail;component/Images/Forward16.png";
            else if ((flags & MailFlags.Answered) == MailFlags.Answered)
                return "/IgOutlook.Modules.Mail;component/Images/Reply16.png";
            else if ((flags & MailFlags.Seen) == MailFlags.Seen)
                return "/IgOutlook.Modules.Mail;component/Images/Mail16.png";
            else
                return "/IgOutlook.Modules.Mail;component/Images/Unread16.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
