using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace Infragistics.Samples.Shared.Converters
{
    public class ModifierKeysConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ModifierKeys type = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), value.ToString(), true);
            return type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new NotImplementedException();
        }
    }
}