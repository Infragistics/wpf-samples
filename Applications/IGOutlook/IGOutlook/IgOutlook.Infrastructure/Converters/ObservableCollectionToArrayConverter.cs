using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Data;

namespace IgOutlook.Infrastructure.Converters
{
    public class ObservableCollectionToArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ObservableCollection<string> emails = value as ObservableCollection<string>;
            if (emails != null)
                return emails.Select(s => s).ToArray();

            return new string[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            object[] emails = value as object[];

            if (emails != null)
                return new ObservableCollection<string>(emails.Select(s => s.ToString()));

            return new ObservableCollection<string>();
        }
    }
}
