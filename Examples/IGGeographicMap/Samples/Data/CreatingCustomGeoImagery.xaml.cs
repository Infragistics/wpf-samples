using System.Windows;
using System.Windows.Controls;
using IGGeographicMap.Extensions;           // GeoImageryView
using IGGeographicMap.Samples.Custom;       // GeoMapAdapter
using Infragistics.Samples.Shared.Models;   // GeoLocations
 
namespace IGGeographicMap.Samples.Data
{
    public partial class CreatingCustomGeoImagery : Infragistics.Samples.Framework.SampleContainer
    {
        public CreatingCustomGeoImagery()
        {
            InitializeComponent();
       
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.GeoImageryViewComboBox.SelectedIndex = 0;

            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 0.5);
        }

        private void GeoImageryViewComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.GeoMap == null) return;

            var mapView = (GeoImageryView)e.AddedItems[0];
            if (mapView == null) return;

            if (mapView.ImagerySource == GeoImagerySource.MapQuestImagery)
                ShowMapQuestImagery((MapQuestImageryView)mapView);
        }

        private void ShowMapQuestImagery(MapQuestImageryView mapView)
        {
            if (mapView.ImageryStyle == MapQuestImageryStyle.StreetMapStyle)
                this.GeoMap.BackgroundContent = new MapQuestStreetImagery();

            else if (mapView.ImageryStyle == MapQuestImageryStyle.SatelliteMapStyle)
                this.GeoMap.BackgroundContent = new MapQuestSatelliteImagery();
        }
    }

}
