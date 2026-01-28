using Infragistics.Documents.Excel;
using System;
using System.Windows.Data;

namespace IGSpreadsheet.Converters
{
    public class ContentEditingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ExcelDefaultableBoolean)
            {
                switch ((ExcelDefaultableBoolean)value)
                {
                    case ExcelDefaultableBoolean.True: return true;
                    case ExcelDefaultableBoolean.False: return false;
                }
            }
            else if (value is FontUnderlineStyle && parameter != null)
            {
                try
                {
                    int parInt = Int32.Parse(parameter.ToString());
                    if (parInt == 1 && (FontUnderlineStyle)value == FontUnderlineStyle.Single)
                        return true;
                    else if (parInt == 2 && (FontUnderlineStyle)value == FontUnderlineStyle.Double)
                        return true;
                }
                catch (Exception) { }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if (targetType == typeof (ExcelDefaultableBoolean))
                {
                    return (bool)value ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                }
            }
            return null;
        }
    }
}
