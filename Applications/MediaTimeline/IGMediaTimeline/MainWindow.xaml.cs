using System.Windows;
using System.Windows.Media;
using IGExtensions.Common.Controls;
using IGExtensions.Framework.Controls;
using Infragistics.Controls.Interactions;
using MediaTimeline.Views;

namespace MediaTimeline
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NavAppInfoDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var winInfo = new About();
            var win = new XamDialogWindow
            {
                Width = winInfo.GetDesiredSize().Width,
                Height = winInfo.GetDesiredSize().Height + 80,
                StartupPosition = StartupPosition.Center,
                Content = winInfo,
                IsModal = true,
                IsResizable = false,
                Style = Application.Current.Resources["XamDialogWindowStyle"] as Style
            };
            this.AppInfoContainer.Children.Add(win);
        }

        private void NavColorWasherDialogButton_Click(object sender, RoutedEventArgs e)
        {
            //var win = new ColorWashEditor { StartupPosition = StartupPosition.Center };
            //win.Style = Application.Current.Resources["AppDialogWindowStyle"] as Style;
            //this.AppWashContainer.Children.Add(win);
            //win.WashSettingsChanged += OnColorWashSettingsChanged;
            var win = new ColorWashEditor
            {
                StartupPosition = StartupPosition.Center,
                IsModal = true,
                ModalBackground = new SolidColorBrush(Colors.Transparent),
                Style = Application.Current.Resources["XamDialogWindowStyle"] as Style,
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
    }
}
