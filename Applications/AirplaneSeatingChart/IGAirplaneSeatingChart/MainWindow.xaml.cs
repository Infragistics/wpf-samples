using System;
using System.Windows;
using System.Windows.Media;
using IGExtensions.Common.Controls;
using IGShowcase.AirplaneSeatingChart.Resources;
using IGShowcase.AirplaneSeatingChart.Views;
using Infragistics.Controls.Interactions;

namespace IGShowcase.AirplaneSeatingChart
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
            var winInfo = new AboutView();
            var win = new XamDialogWindow
            {
                Width = winInfo.GetDesiredSize().Width,
                Height = winInfo.GetDesiredSize().Height + 80,
                StartupPosition = StartupPosition.Center,
                Content = winInfo,
                IsModal = true,
                IsResizable = false,
                Header = AppStrings.About, 
            };
            this.AppInfoContainer.Children.Add(win);
        }

        private void NavColorWasherDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ColorWashEditor 
            { 
                StartupPosition = StartupPosition.Center,
                IsModal = true,
                ModalBackground = new SolidColorBrush(Colors.Transparent)
            };
       
            this.AppWashContainer.Children.Add(win);
            win.WashSettingsChanged += OnColorWashSettingsChanged;
        }
        private void OnColorWashSettingsChanged(object sender, ColorWashSettingsChangedEventArgs e)
        {
            var washSettings = e.ColorWashSettings;

            var resourceWasher = this.ResourceWasher;  
            if (resourceWasher != null)
            {
                resourceWasher.WashColor = washSettings.WashColor;
                resourceWasher.WashGroups[0].WashColor = washSettings.WashColor;
                resourceWasher.WashMode = washSettings.WashMode;
                resourceWasher.SourceDictionary = Application.Current.Resources;
                resourceWasher.WashResources();
            }
        }
    }
}
