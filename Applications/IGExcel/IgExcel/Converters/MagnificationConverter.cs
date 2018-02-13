using Infragistics;
using System;
using System.Globalization;

namespace IgExcel.Converters
{
    public class MagnificationConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = (double)System.Convert.ChangeType(value, typeof(double));

            if (i == 100)
                return 50d;
            else if (i < 100)
                return (i - 10) / 90 * 50d;
            else
                return (i - 100) / 300d * 50d + 50d;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = (double)System.Convert.ChangeType(value, typeof(double));

            if (i == 50d)
                return 100;
            else if (i < 50d)
                return (int)Math.Max((i / 50d * 90d + 10), 10d);
            else
                return (int)Math.Min(((i - 50d) / 50d * 300 + 100), 400d);
        }
    }
}
