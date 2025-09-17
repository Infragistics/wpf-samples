using Infragistics.Controls.Grids;
using System;
using System.Globalization;
using System.Windows.Data;

namespace IGDockManager.Samples.Helpers
{
    public class SummaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SummaryRowLocation pl = (SummaryRowLocation)value;
            return !(pl == SummaryRowLocation.None);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool?)
            {
                if ((bool?)value ?? false)
                {
                    return SummaryRowLocation.Bottom;
                }
                else
                {
                    return SummaryRowLocation.None;
                }
            }
            return null;
        }
    }
}
