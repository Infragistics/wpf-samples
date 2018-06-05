using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using IGExtensions.Common.Controls;
using IGShowcase.EarthQuake.Resources;
using IGShowcase.EarthQuake.Views;
using Infragistics.Controls.Interactions;

namespace IGShowcase.EarthQuake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += OnMainWindowLoaded;
            this.ContentFrame.Navigating += OnContentFrameNavigating;
		}
        protected ColorWasher ResourceColorWasher;
        
        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            this.ResourceColorWasher = this.Resources["ResourceWasher2"] as ColorWasher;
        }

        private void OnContentFrameNavigating(object sender, NavigatingCancelEventArgs e)
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
                ModalBackground = new SolidColorBrush(Colors.Transparent),
                Style = Application.Current.Resources["XamDialogWindowStyle"] as Style,
            };
            this.AppWashContainer.Children.Add(win);
            win.WashSettingsChanged += OnColorWashSettingsChanged;
        }

        private void OnColorWashSettingsChanged(object sender, ColorWashSettingsChangedEventArgs e)
        {
            var washSettings = e.ColorWashSettings;
            //if (this.ResourceWasher != null)
            //{
            //    this.ResourceWasher.WashColor = washSettings.WashColor;
            //    this.ResourceWasher.WashGroups[0].WashColor = washSettings.WashColor;
            //    this.ResourceWasher.WashMode = washSettings.WashMode;
            //    this.ResourceWasher.SourceDictionary = Application.Current.Resources;
            //    this.ResourceWasher.WashResources();
            //}

            if (this.ResourceColorWasher != null)
            {
                //this.ResourceColorWasher.WashResources(Application.Current.Resources, e.ColorWashSettings);
                this.ResourceColorWasher.WashResources(e.ColorWashSettings);
            }
        }

    }
}
