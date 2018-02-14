using IgOutlook.Business.Mail;
using System;
using System.Windows.Data;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailFlagImageConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            MailFlags flags = (MailFlags)value;

            if ((flags & MailFlags.Flagged) == MailFlags.Flagged)
                return "/IgOutlook.Modules.Mail;component/Images/Flagged16.png";

            return "/IgOutlook.Modules.Mail;component/Images/FlagEmpty16.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
