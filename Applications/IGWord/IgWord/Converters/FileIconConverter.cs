using System;
using System.IO;
using System.Windows.Data;

namespace IgWord.Converters
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
                    case ".docx":
                    case ".rtf": return "/IgWord;component/Images/WordFile.png";
                    case ".htm":
                    case ".html": return "/IgWord;component/Images/HtmlFile.png";

                    default: return "/IgWord;component/Images/TextFile.png";
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("FileIconConverter.ConvertBack");
        }
    }
}
