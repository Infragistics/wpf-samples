using Infragistics.Controls.Grids;
using System;
using System.Globalization;
using System.Windows.Data;

namespace IGDockManager.Samples.Helpers
{
    public class PagingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PagingLocation pl = (PagingLocation)value;
            return !(pl == PagingLocation.None || pl == PagingLocation.Hidden);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool?)
            {
                if ((bool?)value ?? false)
                {
                    return PagingLocation.Bottom;
                }
                else
                {
                    return PagingLocation.None;
                }
            }
            return null;
        }
    }
}
