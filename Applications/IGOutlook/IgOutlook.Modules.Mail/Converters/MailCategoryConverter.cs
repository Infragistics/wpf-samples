using IgOutlook.Business.Calendar;
using IgOutlook.Business.Mail;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace IgOutlook.Modules.Mail.Converters
{
    public class MailCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ActivityCategory cat = value as ActivityCategory;
            Color color = Colors.LightGray;
            string param = parameter != null ? parameter.ToString() : String.Empty;

            if (cat == null)
            {
                if (param.Equals("Background"))
                    return new SolidColorBrush(Colors.Transparent);
                else
                    return new SolidColorBrush(color);
            }
            else
            {
                if (param.Equals("Background"))
                    return new SolidColorBrush(ChangeColorBrightness(cat.Color, 0.8F));
                else
                    return new SolidColorBrush(cat.Color);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, System.Convert.ToByte(red), System.Convert.ToByte(green), System.Convert.ToByte(blue));
        }
    }
}
