using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Themes;
using Infragistics.Windows.Ribbon;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace IGRibbon.Samples.Styling
{
    public partial class SettingATheme : SampleContainer
    {
        public SettingATheme()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();
        }
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }
         
    }

    public class ThemeViewModel : INotifyPropertyChanged
    {
        private BitmapImage BoldImg;
        private BitmapImage BoldLightImg;
        private BitmapImage ItalicImg;
        private BitmapImage ItalicLightImg;
        private BitmapImage UnderlineImg;
        private BitmapImage UnderlineLightImg;
        private BitmapImage LeftAlignImg;
        private BitmapImage LeftAlignLightImg;
        private BitmapImage CenterAlignImg;
        private BitmapImage CenterAlignLightImg;
        private BitmapImage RightAlignImg;
        private BitmapImage RightAlignLightImg;
        private BitmapImage JustifyAlignImg;
        private BitmapImage JustifyAlignLightImg;

        public ThemeViewModel()
        {
            BoldImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/Bold.png");
            BoldLightImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/BoldLight.png");
            ItalicImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/Italic.png");
            ItalicLightImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/ItalicLight.png");
            UnderlineImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/Underline.png");
            UnderlineLightImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/UnderlineLight.png");
            LeftAlignImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/LeftAlign.png");
            LeftAlignLightImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/LeftAlignLight.png");
            CenterAlignImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/CenterAlign.png");
            CenterAlignLightImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/CenterAlignLight.png");
            RightAlignImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/RightAlign.png");
            RightAlignLightImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/RightAlignLight.png");
            JustifyAlignImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/JustifyAlign.png");
            JustifyAlignLightImg = GetImageFromUriString("pack://application:,,,/IGRibbon;component/Images/JustifyAlignLight.png");

            Bold = BoldImg;
            Italic = ItalicImg;
            Underline = UnderlineImg;
            LeftAlign = LeftAlignImg;
            CenterAlign = CenterAlignImg;
            RightAlign = RightAlignImg;
            JustifyAlign = JustifyAlignImg;
        }

        private string _theme = "RoyalLight";
        public string Theme
        {
            get { return _theme; }
            set
            {
                _theme = value;
                OnPropertyChanged("Theme");
                if (_theme.Equals("MetroDark") || _theme.Equals("RoyalDark"))
                {
                    Bold = BoldLightImg;
                    Italic = ItalicLightImg;
                    Underline = UnderlineLightImg;
                    LeftAlign = LeftAlignLightImg;
                    CenterAlign = CenterAlignLightImg;
                    RightAlign = RightAlignLightImg;
                    JustifyAlign = JustifyAlignLightImg;
                }
                else
                {
                    Bold = BoldImg;
                    Italic = ItalicImg;
                    Underline = UnderlineImg;
                    LeftAlign = LeftAlignImg;
                    CenterAlign = CenterAlignImg;
                    RightAlign = RightAlignImg;
                    JustifyAlign = JustifyAlignImg;
                }
            }
        }

        private BitmapImage _bold;
        public BitmapImage Bold
        {
            get { return _bold; }
            set { _bold = value; OnPropertyChanged("Bold"); }
        }

        private BitmapImage _italic;
        public BitmapImage Italic
        {
            get { return _italic; }
            set { _italic = value; OnPropertyChanged("Italic"); }
        }

        private BitmapImage _underline;
        public BitmapImage Underline
        {
            get { return _underline; }
            set { _underline = value; OnPropertyChanged("Underline"); }
        }

        private BitmapImage _leftAlign;
        public BitmapImage LeftAlign
        {
            get { return _leftAlign; }
            set { _leftAlign = value; OnPropertyChanged("LeftAlign"); }
        }

        private BitmapImage _centerAlign;
        public BitmapImage CenterAlign
        {
            get { return _centerAlign; }
            set { _centerAlign = value; OnPropertyChanged("CenterAlign"); }
        }

        private BitmapImage _rightAlign;
        public BitmapImage RightAlign
        {
            get { return _rightAlign; }
            set { _rightAlign = value; OnPropertyChanged("RightAlign"); }
        }

        private BitmapImage _justifyAlign;
        public BitmapImage JustifyAlign
        {
            get { return _justifyAlign; }
            set { _justifyAlign = value; OnPropertyChanged("JustifyAlign"); }
        }

        private BitmapImage GetImageFromUriString(string uriString)
        {
            Uri uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.UriSource = uri;
            bmpImage.EndInit();
            return bmpImage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
