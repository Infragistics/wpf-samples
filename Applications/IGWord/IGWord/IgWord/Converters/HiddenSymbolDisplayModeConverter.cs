using Infragistics.Controls.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IgWord.Converters
{
    public class HiddenSymbolDisplayModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return false;

            HiddenSymbolDisplayMode displayMode = (HiddenSymbolDisplayMode)value;

            return displayMode == HiddenSymbolDisplayMode.DisplayAllHiddenSymbols;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return HiddenSymbolDisplayMode.DoNotDisplay;

            return (bool)value ? HiddenSymbolDisplayMode.DisplayAllHiddenSymbols : HiddenSymbolDisplayMode.DoNotDisplay;
        }
    }
}
