using System;
using System.Windows;
using IGGeographicMap.Extensions;
using IGGeographicMap.Samples.Custom;               // GeoMapAdapter
using Infragistics.Controls.Maps;
using Infragistics.Samples.Services;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MapSynchronization : Infragistics.Samples.Framework.SampleContainer
    {
        public MapSynchronization()
        {
            InitializeComponent();

            
            // this code block should be comment out when
            // you have your own keys for Azure Maps
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();
            
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap2, GeoLocations.CityNewYork, 1);

            ShowEsriMapImagery();
        }

        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;

        }

        private void ShowEsriMapImagery()
        {
            var publicMap = new ArcGISOnlineMapImagery();
            publicMap.MapServerUri = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";
            this.GeoMap4.BackgroundContent = publicMap;
        }
    }
}
