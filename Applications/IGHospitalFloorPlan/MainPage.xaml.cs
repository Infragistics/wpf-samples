using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Navigation;
using IGExtensions.Common.Controls;
using IGShowcase.HospitalFloorPlan.Assets.Resources;
using IGShowcase.HospitalFloorPlan.Views;
using Infragistics.Controls.Interactions;

namespace IGShowcase.HospitalFloorPlan
{
    public partial class MainPage : IGExtensions.Framework.Controls.NavigationPage 
	{
		#region Constructor
		public MainPage()
		{
			InitializeComponent();
            this.Title = Strings.AppSubTitle;
     
		}
		#endregion Constructor

		#region Private Event Handlers
        private void OnContentFrameNavigated(object sender, NavigationEventArgs e)
        {

        }
        private void OnContentFrameNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            System.Diagnostics.Debug.WriteLine(e.Exception);
        }
		
		#endregion Private Event Handlers

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
                RestrictInContainer = true,
            };
            var winStyle = Application.Current.Resources["XamDialogWindowStyle"] as Style;
            if (winStyle != null) win.Style = winStyle;

            this.AppInfoContainer.Children.Add(win);
        }

        private void NavColorWasherDialogButton_Click(object sender, RoutedEventArgs e)
        {
            var win = new ColorWashEditor
            {
                StartupPosition = StartupPosition.Center,
                IsModal = true,
                RestrictInContainer = true,
                ModalBackground = new SolidColorBrush(Colors.Transparent)
            };
            var winStyle = Application.Current.Resources["XamDialogWindowStyle"] as Style;
            if (winStyle != null)
                win.Style = winStyle;

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

        private void ShareLinkPanel_ShareLinkRequested(object sender, EventArgs e)
        {

        }
	}
    public static class ResourceWashManager
    {
        public static event ColorWashSettingsChangedEventHandler WashSettingsChanged;

        public static void UpdateWashSettings(ColorWashSettings washSettings)
        {
            if (WashSettingsChanged != null)
            {
                WashSettingsChanged(null, new ColorWashSettingsChangedEventArgs(washSettings));
            }
        }
    }
}