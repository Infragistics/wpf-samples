using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace IGDataGrid.Converters
{
    // Convertor from the Passing value to a row background color - over 60 is red otherwise blue
    public class PassingToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dblValue = 0;

            if (double.TryParse(value.ToString(), out dblValue))
            {
                if (dblValue > 60)
                    return Color.FromRgb(210, 60, 112);
                else
                    return Color.FromRgb(60, 78, 174);
            }
            else
                return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
