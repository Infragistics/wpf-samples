using System;
using System.Globalization;
using System.Windows.Data;
using Infragistics.Controls;
using Infragistics.Controls.Charts;

namespace IGDataChart.Converters
{
    public class InteractionStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            InteractionState type = (InteractionState)Enum.Parse(typeof(InteractionState), value.ToString(), true);
            return type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new NotImplementedException();
        }
    }
}