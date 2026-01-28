using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace Infragistics.Samples.Shared.Converters
{
    public class DictionaryItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string)
            {
                var dict = value as Dictionary<string, object>;
                if (dict != null)
                {
                    string[] parameters = ((string)parameter).Split(':');
                    string key = parameters[0];
                    if (parameters.Length >= 2)
                    {
                        string format = parameters[1];
                        return String.Format("{0:" + format + "}", dict[key]);
                    }
                    return dict[key];
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}