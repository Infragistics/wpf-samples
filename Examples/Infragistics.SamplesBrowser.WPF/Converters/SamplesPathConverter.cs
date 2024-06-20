using Infragistics.SamplesBrowser.ViewModel;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Infragistics.SamplesBrowser.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class SamplesPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return System.IO.Path.GetFileName(value.ToString());

            return "";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            // we don't intend this to ever be called
            return null;
        }
    }

    public class TocItemToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TocItemViewModel;

            if (parameter == null || item == null || item.HasChildren)
            {
                return ""; // for a control or category group
            }
            else
            {
                var target = parameter.ToString().ToLower();
                if (target == "control")
                {
                    if (item.HasParent && item.Parent.HasParent)
                        return item.Parent.Parent.Name;
                    else
                        return "MISSING: Control Name";
                }
                else if (target == "category")
                {
                    if (item.HasParent)
                        return item.Parent.Name;
                    else
                         return "MISSING: Category Name";
                }
                else if (target == "sample")
                {
                    if (string.IsNullOrEmpty(item.Name))
                        return "MISSING: Control Name";
                    else
                        return item.Name;
                }
                throw new NotSupportedException("Not Supported '" + parameter + "' conversion parameter"); 
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("TocItemToNameConverter.ConvertBack");
        }
    }
    public class TocVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as TocItemViewModel;

            if (item == null || item.HasChildren)
            {
                return Visibility.Collapsed; // for a control or category
            }
            else // it is actual sample
            {
                return Visibility.Visible;
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("TocVisibilityConverter.ConvertBack");
        }
    }
}
