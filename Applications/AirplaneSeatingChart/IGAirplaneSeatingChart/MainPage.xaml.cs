using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using IGExtensions.Common.Controls;
using IGShowcase.AirplaneSeatingChart.Resources;
using IGShowcase.AirplaneSeatingChart.Views;
using Infragistics.Controls.Interactions;

namespace IGShowcase.AirplaneSeatingChart
{
    public partial class MainPage : IGExtensions.Framework.Controls.NavigationPage 
    {
        public MainPage()
        {
            InitializeComponent();
            this.Title = AppStrings.AppName;
     
            this.Loaded += new System.Windows.RoutedEventHandler(MainPage_Loaded);
        }
       
        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
      
          
        }
        protected override void OnPageNavigated(NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnPageNavigated");
           
        }

        private void OnContentFrameNavigated(object sender, NavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("OnContentFrameNavigated");
      
        }
        private void OnContentFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            System.Diagnostics.Debug.WriteLine(e.Exception);
        }
        /*
        TODO-MT: fix this
        private void ShareLinkPanel_ShareLinkRequested(object sender, EventArgs e)
        {
           // var link = this.NavigationContext.ToString();
            var link = NavigationFramework.CurrentNavigationLink; 
          
            System.Diagnostics.Debug.WriteLine("ShareLinkPanel_ShareLinkRequested" + link);

            this.ShareLinkPanel.ShareLink.Link = link;
        }*/

        private void ShareLinkPanel_ShareLinkRequested(object sender, EventArgs e)
        {

        }

        public void ShowErrorDialog(string errorMessage, string errorDetails)
        {
            //var ErrorDialog = new AppErrorDialog();

            //ErrorDialog.ErrorDetails = errorDetails;
            //ErrorDialog.ErrorMessage = errorMessage;
            //ErrorDialog.Visibility = System.Windows.Visibility.Visible;
            //this.DialogContainer.Child = ErrorDialog;

            //ErrorDialog.Content = new ErrorContent { Message = errorMessage, ErrorDetails = errorDetails };

            //win.StartupPosition = StartupPosition.Center;

            //ErrorDialog.ErrorDetails = errorDetails;
            //ErrorDialog.ErrorMessage = errorMessage;
            //ErrorDialog.Visibility = System.Windows.Visibility.Visible;
            //this.LayoutRoot.Children.Add(ErrorDialog);

            //this.DialogContainer.Visibility = System.Windows.Visibility.Visible;
            //var win = new AppInfoDialog();
            //win.Show();
        }
        private void NavAppInfoDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var winInfo = new AboutView();
            var win = new XamDialogWindow
            {
                Width = winInfo.GetDesiredSize().Width,
                Height = winInfo.GetDesiredSize().Height + 120,
                StartupPosition = StartupPosition.Center,
                Content = winInfo,
                IsModal = true,
                IsResizable = false,
                RestrictInContainer = true,
                Style = Application.Current.Resources["AppDialogWindowStyle"] as Style
            };
            this.AppInfoContainer.Children.Add(win);
        }

        private void NavColorWasherDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ColorWashEditor
                          {
                              IsModal = true,
                              StartupPosition = StartupPosition.Center,
                              Style = Application.Current.Resources["AppDialogWindowStyle"] as Style,
                              RestrictInContainer = true,
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
