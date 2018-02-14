using System;
using System.Globalization;
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

namespace IGShowcase.FinanceDashboard.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            System.Diagnostics.Debug.WriteLine("CurrentCulture is: " + CultureInfo.CurrentUICulture.ToString());

            DateTime currentTime = DateTime.Now;

            String enformat = "MM/dd/yyyy h:mm:ss tt";
            String jpformat = "yyyy/MM/dd H:mm:ss";

            if (CultureInfo.CurrentUICulture.Name == "ja-JP")
            {
                if (value != null)
                {
                    string newDate = currentTime.ToString(jpformat);

                    return newDate;
                }

                return value;
            }

            if (CultureInfo.CurrentUICulture.Name != "ja-JP")
            {
                if (value != null)
                {
                    string newDate = currentTime.ToString(enformat);

                    return newDate;
                }

                return value;
            }

            return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }

    }
}
