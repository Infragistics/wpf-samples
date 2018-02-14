using Infragistics;
using System;
using System.Globalization;

namespace IgWord.Converters
{
    public class MagnificationConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = (double)System.Convert.ChangeType(value, typeof(double));

            if (i == 1)
                return 50d;
            else if (i < 1)
                return (i - 0.2) / 0.8 * 50d;
            else
                return (i - 1) / 3d * 50d + 50d;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var i = (double)System.Convert.ChangeType(value, typeof(double));

            if (i == 50d)
                return 1;
            else if (i < 50d)
                return (double)Math.Max((i / 50d * 0.8d + 0.2), 0.2d);
            else
                return (double)Math.Min(((i - 50d) / 50d * 3 + 1), 4d);
        }
    }

}
