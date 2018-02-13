using System;
using System.Windows.Data;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime dateTime = (DateTime)value;

            if (dateTime.Date == DateTime.Today.Date)
                return dateTime.ToString("hh:mm tt");
            else if (dateTime.Date < DateTime.Today.Date.AddDays(-12))
                return dateTime.ToString("MM/dd/yyyy");

            return dateTime.ToString("ddd hh:mm tt");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
