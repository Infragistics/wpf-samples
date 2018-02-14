using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailAddressCollectionDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<string> emails = value as ObservableCollection<string>;
            if (emails != null)
                return String.Join("; ", emails);

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string emails = value as string;
            if (!String.IsNullOrWhiteSpace(emails))
            {
                var results = emails.Split(new[] { ";", "; " }, StringSplitOptions.RemoveEmptyEntries);
                return new ObservableCollection<string>(results.Select(s=>s.Trim()));
            }

            return null;
        }
    }
}
