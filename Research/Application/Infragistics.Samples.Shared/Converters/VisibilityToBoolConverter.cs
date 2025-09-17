using System; 
using System.Globalization; 
using System.Windows;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    public class VisibilityToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;
            return (visibility == Visibility.Visible);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool visibility = (bool)value;
                return visibility ? Visibility.Visible : Visibility.Collapsed;
            }
            else 
            { 
                return Visibility.Hidden; 
            }
        }
    }
}
