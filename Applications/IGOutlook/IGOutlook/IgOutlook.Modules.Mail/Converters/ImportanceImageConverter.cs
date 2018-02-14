using IgOutlook.Business.Mail;
using System;
using System.Windows.Data;

namespace IgOutlook.Modules.Mail.Converters
{
    public class ImportanceImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;

            var importance = (MailPriority)value;

            if (importance == MailPriority.High)
                return "/IgOutlook.Modules.Mail;component/Images/HighImportance16.png";
            else if (importance == MailPriority.Low)
                return "/IgOutlook.Modules.Mail;component/Images/LowImportance16.png";

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
