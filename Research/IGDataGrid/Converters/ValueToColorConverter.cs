using System;
using System.Windows.Data;
using System.Windows.Media;

namespace IGDataGrid.Converters
{
    public class ValueToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                double val = System.Convert.ToDouble(value);

                if (val < 100)
                    return new SolidColorBrush(Color.FromArgb(255, 159, 179, 40));

                if (val < 500)
                    return new SolidColorBrush(Color.FromArgb(255, 249, 98, 50));

                return new SolidColorBrush(Color.FromArgb(255, 46, 156, 166));
            }
            catch
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Since this is only one-way converter, ConvertBack should never get called.
            return false;
        }
    }
}