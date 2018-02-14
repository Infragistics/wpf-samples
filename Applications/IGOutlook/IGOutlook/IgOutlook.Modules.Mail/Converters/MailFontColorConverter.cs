using IgOutlook.Business.Mail;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailFontColorConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            MailFlags flags = (MailFlags)value;
            string mailSection = parameter == null? String.Empty: parameter.ToString();

            if ((flags & MailFlags.Flagged) == MailFlags.Flagged)
                return new SolidColorBrush(Colors.Red);

            if (mailSection.Equals("Subject"))
                return new SolidColorBrush(Colors.Gray);
            else
                return new SolidColorBrush(Colors.Black);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
