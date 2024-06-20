using Infragistics.Documents.Excel;
using System;
using System.Windows.Data;

namespace IGSpreadsheet.Converters
{
    public class ExcelDefaultableBooleanConverter : IValueConverter
    {
        // convert ExcelDefaultableBoolean to bool?
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ExcelDefaultableBoolean defBool = (ExcelDefaultableBoolean)value;
            bool? result = null;
            if (defBool == ExcelDefaultableBoolean.True) result = true;
            else if (defBool == ExcelDefaultableBoolean.False) result = false;
            return result;
        }

        // convert bool? to ExcelDefaultableBoolean
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? nullBool = value as bool?;
            ExcelDefaultableBoolean result = ExcelDefaultableBoolean.Default;
            if (nullBool != null && nullBool.HasValue)
            {
                result = nullBool.Value ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
            }
            return result;
        }
    }
}
