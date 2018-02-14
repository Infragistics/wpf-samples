using System;
using System.IO;
using System.Windows.Data;

namespace IgExcel.Converters
{
    public class FileIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var input = (string)value;

            if (string.IsNullOrEmpty(input))
            {
                return value;
            }
            else
            {
                var fileExtension = Path.GetExtension(input);

                switch (fileExtension)
                {
                    case ".xls": return "/IgExcel;component/Images/Excel2003-2007File.png";
                    case ".xlt": return "/IgExcel;component/Images/Excel2003-2007TemplateFile.png";
                    case ".xlsm":
                    case ".xltm": return "/IgExcel;component/Images/ExcelMacroEnabledFile.png";
                    case ".xlsx": return "/IgExcel;component/Images/ExcelFile.png";
                    default: return "/IgExcel;component/Images/TextFile.png";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("FileIconConverter.ConvertBack");
        }
    }
}
