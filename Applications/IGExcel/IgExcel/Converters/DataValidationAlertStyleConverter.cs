using IgExcel.Dialogs;
using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace IgExcel.Converters
{
    public class DataValidationAlertStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is CustomComboBoxItem)
            {
                string name = ((CustomComboBoxItem)value).ItemName;
                string path = null;
                switch (name)
                {
                    case "AlertStyleType0":
                        path = "/IgExcel;component/Images/alertStop.png";
                        break;
                    case "AlertStyleType1":
                        path = "/IgExcel;component/Images/alertWarning.png";
                        break;
                    case "AlertStyleType2":
                        path = "/IgExcel;component/Images/alertInformation.png";
                        break;
                }
                Uri imageUri = new Uri(path, UriKind.Relative);
                BitmapImage imageBitmap = new BitmapImage(imageUri);
                return imageBitmap;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException("DataValidationAlertStyleConverter.ConvertBack");
        }
    }
}
