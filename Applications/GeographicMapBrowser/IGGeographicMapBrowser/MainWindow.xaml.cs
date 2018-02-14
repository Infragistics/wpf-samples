using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using IGExtensions.Common.Controls;
using IGShowcase.GeographicMapBrowser.Views;
using Infragistics.Controls.Interactions;

namespace IGShowcase.GeographicMapBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IGExtensions.Framework.Controls.NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();

        }
      
        private void NavShareButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NavAppInfoDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var winInfo = new AboutView();
            var win = new XamDialogWindow
            {
                Width = winInfo.GetDesiredSize().Width,
                Height = winInfo.GetDesiredSize().Height + 40,
                StartupPosition = StartupPosition.Center,
                Content = winInfo,
                IsModal = true,
                //MinimizeButtonVisibility = Visibility.Visible,
                CloseButtonVisibility = Visibility.Visible, 
                HeaderIconVisibility = Visibility.Collapsed,
                Header = "  About",
                ModalBackground = new SolidColorBrush(Colors.White) { Opacity = 0.8 },
                IsResizable = false,
                //Style = Application.Current.Resources["AppDialogWindowStyle"] as Style
            };
            this.AppInfoContainer.Children.Add(win);
        }
        private void NavColorWasherDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ColorWashEditor
            {
                IsModal = true,
                ModalBackground = new SolidColorBrush(Colors.Transparent),
                StartupPosition = StartupPosition.Center,
                CloseButtonVisibility = Visibility.Visible,
                HeaderIconVisibility = Visibility.Collapsed,
                Header = "  Application Theme Washer",
                //Style = Application.Current.Resources["XamDialogWindowStyle"] as Style,
                //Style = Application.Current.Resources["XamDialogWindowStyleOverride"] as Style,
                
            };

            this.AppWashContainer.Children.Add(win);
            win.WashSettingsChanged += OnColorWashSettingsChanged;
        }
        private void OnColorWashSettingsChanged(object sender, ColorWashSettingsChangedEventArgs e)
        {
            var washSettings = e.ColorWashSettings;
            if (this.ResourceWasher != null)
            {
                this.ResourceWasher.WashColor = washSettings.WashColor;
                this.ResourceWasher.WashGroups[0].WashColor = washSettings.WashColor;
                this.ResourceWasher.WashMode = washSettings.WashMode;
                this.ResourceWasher.SourceDictionary = Application.Current.Resources;
                this.ResourceWasher.WashResources();
            }
        }
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }


          
         
       
    }
}
