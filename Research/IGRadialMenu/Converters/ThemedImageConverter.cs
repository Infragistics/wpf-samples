using Infragistics.Samples.Shared.Models;
using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace IGRadialMenu.Converters
{
    public class ThemedImageConverter : IValueConverter
    {
        private static Image imgBold = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Bold.png") };
        private static Image imgItalic = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Italic.png") };
        private static Image imgFontSize = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Size.png") };
        private static Image imgFont = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Font.png") };

        private static Image imgBoldLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/BoldLight.png") };
        private static Image imgItalicLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/ItalicLight.png") };
        private static Image imgFontSizeLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/SizeLight.png") };
        private static Image imgFontLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/FontLight.png") };
        
        private static BitmapImage GetImageFromUriString(string uriString)
        {
            Uri uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.UriSource = uri;
            return bmpImage;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string buttonType = parameter.ToString();
            if (value is Theme)
            {
                if ("MetroDark".Equals(((Theme)value).ThemeId))
                {
                    // use light icons for the metro dark theme only
                    switch (buttonType)
                    {
                        case "Bold": return ThemedImageConverter.imgBoldLight;
                        case "Italic": return ThemedImageConverter.imgItalicLight;
                        case "Size": return ThemedImageConverter.imgFontSizeLight;
                        case "Font": return ThemedImageConverter.imgFontLight;
                    }
                }
                else
                {
                    // for all other themes use the dark icons
                    switch (buttonType)
                    {
                        case "Bold": return ThemedImageConverter.imgBold;
                        case "Italic": return ThemedImageConverter.imgItalic;
                        case "Size": return ThemedImageConverter.imgFontSize;
                        case "Font": return ThemedImageConverter.imgFont;
                    }
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
