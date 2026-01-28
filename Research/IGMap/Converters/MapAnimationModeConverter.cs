using System;
using System.Globalization;
using System.Windows.Data;
using Infragistics.Controls.Maps;

namespace IGMap.Converters
{
    public class MapAnimationModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (MapWindowAnimationMode)Enum.Parse(typeof(MapWindowAnimationMode), value.ToString(), true);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }
    }
}