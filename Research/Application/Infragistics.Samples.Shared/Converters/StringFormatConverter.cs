using System;
using System.Globalization;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{

    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (parameter == null)
            {
                return value;
            }

            string returnString = value.ToString();
            switch (parameter.ToString())
            {
                case "ToUpper":
                    returnString = returnString.ToUpper(culture);
                    break;

                case "ToLower":
                    returnString = returnString.ToLower(culture);
                    break;

                case "TrimDecimal":
                    returnString = String.Format("{0:0.}", Double.Parse(returnString));
                    break;

                case "DoublePrecisionDecimal":
                    returnString = String.Format("{0:0.##}", Double.Parse(returnString));
                    break;
                case "int,int":
                    try
                    {
                        string[] tmp = returnString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        returnString = int.Parse(tmp[0]) + "," + int.Parse(tmp[1]);
                    }
                    catch
                    {
                        returnString = "1,1";
                    }
                    break;
                case "ToChar":
                    if (returnString.Length > 0)
                        return returnString.ToCharArray()[0];
                    break;

                default:
                    returnString = string.Format(string.Format("{{0:{0}}}", parameter), value);
                    break;
            }

            return returnString;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }
    }

    public enum StringCase
    {
        Upper,
        Lower,
        // TOOD ToTitle 
    }
    public class StringCaseConverter : IValueConverter
    {
        public StringCaseConverter()
        {
            StringCase = StringCase.Upper;
        }
        public StringCase StringCase { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var str = value.ToString();
            if (StringCase == StringCase.Upper)
            {
                str = str.ToUpper(culture);
            }
            else if (StringCase == StringCase.Lower)
            {
                str = str.ToLower(culture);
            }
            return str;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //NOTE: The back conversion is not used.
            throw new System.NotImplementedException();
        }
    }
}
