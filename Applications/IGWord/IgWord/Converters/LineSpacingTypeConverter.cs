using IgWord.Infrastructure;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Data;

namespace IgWord.Converters
{
    public class LineSpacingTypeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is LineSpacingTypes)
            {
                return LineSpacingTypeConverter.GetDescription((LineSpacingTypes)value);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }


        public static string GetDescription(Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value is null");
            }

            string description = value.ToString();
            FieldInfo fieldInfo = value.GetType().GetField(description);
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                description = attributes[0].Description;
            }

            return description;
        }
    }
}
