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

            // must provide your own keys for Bing Maps
            // to display geo-imagery in the Geographic Map control
            this.BingMadeMapKey = string.Empty;     //  visit www.bingmapsportal.com

            // this code block should be comment out when
            // you have your own keys for Bing Maps
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();
            
            this.Loaded += OnSampleLoaded;
        }

        protected string BingMadeMapKey; 

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap2, GeoLocations.CityNewYork, 1);

            ShowEsriMapImagery();
        }

        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;

            foreach (var element in e.Result)
            {
                if (element.Name == "BingMaps")
                {
                    this.BingMadeMapKey = element.Key;
                    this.ShowBingMapsImagery(new BingMapsImageryView { ImageryStyle = Infragistics.Controls.Maps.BingMapsImageryStyle.Road });
                }
            }
        }

        private void ShowEsriMapImagery()
        {
            var publicMap = new ArcGISOnlineMapImagery();
            publicMap.MapServerUri = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";
            this.GeoMap4.BackgroundContent = publicMap;
        }
        
        private void ShowBingMapsImagery(BingMapsImageryView mapView)
        {
            string mapKey = this.BingMadeMapKey;

            if (!String.IsNullOrEmpty(mapKey))
            {
                var mapStyle = (Infragistics.Controls.Maps.BingMapsImageryStyle)mapView.ImageryStyle;
                var mapStyle2 = Infragistics.Samples.Services.BingMapsImageryStyle.AerialWithLabels;

                this.GeoMap1.BackgroundContent = new BingMapsMapImagery { ImageryStyle = mapStyle, ApiKey = mapKey, IsDeferredLoad = false };
                this.GeoMap3.BackgroundContent = new BingMapsMapImagery { ImageryStyle = (Infragistics.Controls.Maps.BingMapsImageryStyle)mapStyle2, ApiKey = mapKey, IsDeferredLoad = false };
            }
        }
    }
}
