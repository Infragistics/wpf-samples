using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Infragistics.Controls.Editors;

namespace IGCalendar.Converters
{
    public class CalendarDisplayModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is int)
            {
                return (CalendarZoomMode)((int)value);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CalendarZoomMode)
            {
                return (int)((CalendarZoomMode)value);
            }
            return DependencyProperty.UnsetValue;

        }
    }
}
