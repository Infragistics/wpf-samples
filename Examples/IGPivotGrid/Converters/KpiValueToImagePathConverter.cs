using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace IGPivotGrid.Converters
{
    public class KpiValueToImagePathConverter : IValueConverter
    {
        string negativeSource = @"../../Images/KpiImageNegative.png";
        string positveSource = @"../../Images/KpiImagePositive.png";
        string zeroSource = @"../../Images/KpiImageZero.png";
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double dvalue;
            if (double.TryParse(value.ToString().Replace("%", ""), out dvalue))
            {
                if (dvalue < 0)
                {
                    return negativeSource;
                }
                else
                {
                    if (dvalue > 0)
                    {
                        return positveSource;
                    }
                    else
                    {
                        return zeroSource;
                    }
                }
            }

            return negativeSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
}
