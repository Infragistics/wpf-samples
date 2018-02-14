using System;
using System.Windows;
using System.Windows.Data;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailFromToTextVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var direction = parameter != null ? parameter.ToString() : String.Empty;

            if ((value.Equals("Inbox") || value.Equals("Deleted")) && direction.Equals("From"))
                return Visibility.Visible;

            if ((value.Equals("Drafts") || value.Equals("Sent")) && direction.Equals("To"))
                return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
