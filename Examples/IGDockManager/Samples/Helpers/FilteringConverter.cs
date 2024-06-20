using Infragistics.Controls.Grids;
using System;
using System.Globalization;
using System.Windows.Data;

namespace IGDockManager.Samples.Helpers
{
    public class FilteringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FilterUIType pl = (FilterUIType)value;
            return !(pl == FilterUIType.None);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool?)
            {
                if ((bool?)value ?? false)
                {
                    return FilterUIType.FilterRowTop;
                }
                else
                {
                    return FilterUIType.None;
                }
            }
            return null;
        }
    }
}
