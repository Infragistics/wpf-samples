using System.Windows;
using System.Windows.Controls;
using IGMap.Resources.SampleControlStrings;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using IGMap.Models;
using System.Windows.Navigation;

namespace IGMap.Samples.Localization
{
    public partial class MapResourceStrings : Infragistics.Samples.Framework.SampleContainer
    {
        public MapResourceStrings()
        {
            //Resource strings must be applied before InitializeComponent() is called.             
            //They won't update after the control is initialized.
            XamMap.RegisterResources(
                //The full name of the .resx file to be used.
                "IGMap.Resources.SampleControlStrings.SampleMapStrings",
                //The assembly in which the .resx file is embedded.
                typeof(SampleMapStrings).Assembly);

            InitializeComponent();
            InitializeMap();

            //this.Loaded += OnSampleLoaded;
        }
        // Executes when the user navigates away from this page.
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //Restore the default strings.
            XamMap.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGMap.Resources.SampleControlStrings.SampleMapStrings");

            base.OnNavigatingFrom(e);
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
           // this.NavigationService.Navigating += OnNavigationServiceNavigating;
            this.Loaded -= OnSampleLoaded;

            //Restore the default strings.
            XamMap.UnregisterResources(
                //The name of the embedded resx file that was used for registration.
                "IGMap.Resources.SampleControlStrings.SampleMapStrings");

        }

        private void InitializeMap()
        {
            // initialize map boundary to specific map region
            MapAdapter.SetMapBoundary(this.xamMap, GeoRegions.WorldFullRegion);
            MapAdapter.ZoomMapToRegion(this.xamMap, GeoRegions.WorldNonPolarRegion);             
        }
    }
}
