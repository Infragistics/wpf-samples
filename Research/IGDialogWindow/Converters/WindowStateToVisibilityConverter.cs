using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IGDialogWindow.Converters
{
    public class WindowStateToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() != Infragistics.Controls.Interactions.WindowState.Hidden.ToString())
                return Visibility.Collapsed;
            else
                return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
