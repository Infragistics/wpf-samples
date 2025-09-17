using System;
using System.Windows;
using System.Windows.Data;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Converters
{
    public class IsLabelIconTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if ((value is FilterUIType) && (FilterUIType)value == FilterUIType.LabelIcons)
                return Visibility.Visible;
            return Visibility.Hidden;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}