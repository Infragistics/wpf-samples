using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using IGExtensions.Common.Controls;
using IGShowcase.FinanceDashboard.Resources;
using IGShowcase.FinanceDashboard.Views;
using Infragistics.Controls.Interactions;

namespace IGShowcase.FinanceDashboard
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
                Height = winInfo.GetDesiredSize().Height + 80,
                StartupPosition = StartupPosition.Center,
                Content = winInfo,
                IsModal = true,
                ModalBackground = new SolidColorBrush(Colors.Black) { Opacity = 0.75},
                IsResizable = false,
                Header = AppStrings.AppView_About,
                Style = Application.Current.Resources["AppDialogWindowStyle"] as Style
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
                Style = Application.Current.Resources["AppDialogWindowStyle"] as Style
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
