using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MappingGeoLocations : Infragistics.Samples.Framework.SampleContainer
    {
        public MappingGeoLocations()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            ZoomMapToLocation(GeoLocations.CityNewYork);
        }

        private void OnWorldCitiesListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var geoLocation = e.AddedItems[0] as GeoLocation;
            if (geoLocation != null)
            {
                // navigate to a geographic location 
                ZoomMapToLocation(geoLocation);
            }
        }
        private void ZoomMapToLocation(GeoLocation geoLocation)
        {
            // navigate to a geographic location 
            var geoRect = new Rect(geoLocation.Longitude - 3, geoLocation.Latitude - 3, 6, 6);
            Rect windowRect = this.GeoMap.GetZoomFromGeographic(geoRect);
            this.GeoMap.WindowRect = windowRect;
        }
    }
}
