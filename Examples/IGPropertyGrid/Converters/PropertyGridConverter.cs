using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace IGPropertyGrid.Converters
{
    public class TypeNameShorteningConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string typeFull = value.GetType().FullName;
                string typeName = typeFull.Substring(typeFull.LastIndexOf(".") + 1);
                return typeName;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
