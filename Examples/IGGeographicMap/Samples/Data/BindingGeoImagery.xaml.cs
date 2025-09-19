using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using IGGeographicMap.Resources;
using IGGeographicMap.Samples.Custom;               // GeoMapAdapter
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;    // GeoImageryKeyProvider
using Infragistics.Samples.Shared.Models;
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingGeoImagery : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingGeoImagery()
        {
            InitializeComponent();

            // must provide your own keys for Azure Maps
            // to display geo-imagery in the Geographic Map control
            this.AzureMadeMapKey = string.Empty;
            // this code block should be comment out when
            // you have your own keys for Azure Maps  
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();

            this.Loaded += OnSampleLoaded;
        }

        // must provide your own key for Azure Maps to display geo-imagery in the Geographic Map control
        protected string AzureMadeMapKey; //  visit https://learn.microsoft.com/en-us/azure/azure-maps/azure-maps-authentication

        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;

            foreach (var element in e.Result)
            {
                if (element.Name == "AzureMaps") this.AzureMadeMapKey = element.Key;
            }
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.GeoImageryViewComboBox.SelectedIndex = 0;
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);
        }

        private void OnGeoImageryViewComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.GeoMap == null) return;

            var mapView = (GeoImageryView) e.AddedItems[0];
            if (mapView == null) return;
            
            this.DialogInfoPanel.Visibility = Visibility.Collapsed;

            // display geo-imagery based on selected map view
            if (mapView.ImagerySource == GeoImagerySource.OpenStreetMapImagery)
            {
                ShowOpenStreetMapImagery();
            }            
            else if (mapView.ImagerySource == GeoImagerySource.EsriMapImagery)
            {
                ShowEsriOnlineMapImagery((EsriMapImageryView)mapView);
            }
            else if (mapView.ImagerySource == GeoImagerySource.MapQuestImagery)
            {
                ShowMapQuestImagery((MapQuestImageryView)mapView);
            }
            else if (mapView.ImagerySource == GeoImagerySource.AzureMapsImagery)
            {
                this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
                if (String.IsNullOrEmpty(AzureMadeMapKey))
                {
                    this.DialogInfoPanel.Visibility = Visibility.Visible;
                }
                ShowAzureMapsImagery((AzureMapImageryView)mapView);

                if (((IGGeographicMap.Extensions.AzureMapImageryView)this.GeoImageryViewComboBox.SelectedValue).ImageryStyle == AzureMapsImageryStyle.WeatherInfraredOverlay
                || ((IGGeographicMap.Extensions.AzureMapImageryView)this.GeoImageryViewComboBox.SelectedValue).ImageryStyle == AzureMapsImageryStyle.WeatherRadarOverlay)
                {
                    this.GeoMap.ResetZoom();

                }
                else
                {
                    GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);

                }
            }

        }

        private void ShowOpenStreetMapImagery()
        {
            this.GeoMap.BackgroundContent = new OpenStreetMapImagery();
        }

        private void ShowEsriOnlineMapImagery(EsriMapImageryView mapView)
        {
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var esriMap = new ArcGISOnlineMapImagery();
            //esriMap.MapServerUri = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";

            esriMap.MapServerUri = mapView.ImageryServer;

            this.GeoMap.BackgroundContent = esriMap;
        }
        private void ShowMapQuestImagery(MapQuestImageryView mapView)
        {
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);

            if (mapView.ImageryStyle == MapQuestImageryStyle.StreetMapStyle)
                this.GeoMap.BackgroundContent = new MapQuestStreetImagery();

            else if (mapView.ImageryStyle == MapQuestImageryStyle.SatelliteMapStyle)
                this.GeoMap.BackgroundContent = new MapQuestSatelliteImagery();
        }
        private void ShowAzureMapsImagery(AzureMapImageryView mapView)
        {
            string mapKey = this.AzureMadeMapKey;
            var mapImage = new Image();
            var mapStyle = mapView.ImageryStyle;

            if (String.IsNullOrEmpty(mapKey))
            {
                Uri mapURI = null;
                switch (mapStyle)
                {
                    case AzureMapsImageryStyle.DarkGrey:
                        mapURI = new Uri(@"../../Resources/AzureDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.HybridRoadOverlay:
                        mapURI = new Uri(@"../../Resources/AzureHybridRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.Road:
                        mapURI = new Uri(@"../../Resources/AzureRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.Satellite:
                        mapURI = new Uri(@"../../Resources/AzureImagery.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficAbsoluteOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficAndRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.WeatherInfraredOverlay:
                        mapURI = new Uri(@"../../Resources/AzureWeatherInfraredRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.WeatherRadarOverlay:
                        mapURI = new Uri(@"../../Resources/AzureWeatherInfraredRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.HybridDarkGreyOverlay:
                        mapURI = new Uri(@"../../Resources/AzureDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.LabelsDarkGreyOverlay:
                        mapURI = new Uri(@"../../Resources/AzureDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.LabelsRoadOverlay:
                        mapURI = new Uri(@"../../Resources/AzureRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TerraOverlay:
                        mapURI = new Uri(@"../../Resources/AzureWeatherInfraredRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficDelayOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficDelay.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficReducedOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficReduced.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficRelativeDarkOverlay:
                        mapURI = new Uri(@"../../Resources/AzureDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficRelativeOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficLight.png", UriKind.RelativeOrAbsolute);
                        break;
                    default:
                        break;
                }
                
                //Basic keys are no longer optional, hence we are showing images. If you have a valid enterprise key you may comment this code out and uncomment out the BackgroundContent below applying the imagery instead and apply your own api key.
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = mapURI;
                bitmapImage.EndInit();
                mapImage.Source = bitmapImage;
                this.GeoMap.BackgroundContent = mapImage;
            }
            else
            {
                this.GeoMap.BackgroundContent = new AzureMapsImagery { ImageryStyle = mapStyle, ApiKey = this.AzureMadeMapKey };
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogInfoPanel.Visibility = Visibility.Collapsed;
            this.AzureMadeMapKey = EnterAzureKey.Text;
            ShowAzureMapsImagery(((AzureMapImageryView)this.GeoImageryViewComboBox.SelectedValue));
        }

        private void EnterAzureKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.AzureMadeMapKey = EnterAzureKey.Text;
        }
    }

 
}

