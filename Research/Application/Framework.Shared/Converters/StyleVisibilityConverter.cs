using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Infragistics.Framework
{
    //TODO-DEV copy this Converter to DV project   

    public class StyleVisibilityConverter : IValueConverter
    {
        public StyleVisibilityConverter()
        {
            WhenNull = Visibility.Collapsed;
        }
        public Visibility WhenNull { get; set; }


        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string)
            {
                WhenNull = ((string)parameter).ToLower() == "visible" ?
                Visibility.Visible : Visibility.Collapsed;
            }

            var style = value as Style;

            if (style == null) return WhenNull;
            if (style.Setters.Count == 0) return WhenNull;

            if (style.Setters.Count == 1)
            {
                var setters = style.Setters[0] as Setter;
                if (setters == null) return WhenNull;
                if (setters.Property.Name == "Template")
                    return WhenNull;
            }

            return WhenNull == Visibility.Visible ?
                Visibility.Collapsed : Visibility.Visible;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }

}
