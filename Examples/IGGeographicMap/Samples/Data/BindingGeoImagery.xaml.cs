using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using IGGeographicMap.Resources;
using IGGeographicMap.Samples.Custom;               // GeoMapAdapter
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

            // must provide your own keys for Bing Maps
            // to display geo-imagery in the Geographic Map control
            this.BingMadeMapKey = string.Empty;     //  visit www.bingmapsportal.com
            this.AzureMadeMapKey = string.Empty;
            // this code block should be comment out when
            // you have your own keys for Bing Maps  
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();

            this.Loaded += OnSampleLoaded;
        }
        protected string BingMadeMapKey;
        protected string AzureMadeMapKey;

        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;

            foreach (var element in e.Result)
            {
                if (element.Name == "BingMaps") this.BingMadeMapKey = element.Key;
                if (element.Name == "AzureMaps") this.AzureMadeMapKey = element.Key;
            }
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            var items = new GeoImageryViews();
            
            items.Add(new OpenStreetMapImageryView());
            items.Add(new BingMapsImageryView { ImageryStyle = BingMapsImageryStyle.Aerial });
            items.Add(new BingMapsImageryView { ImageryStyle = BingMapsImageryStyle.AerialWithLabels });
            items.Add(new BingMapsImageryView { ImageryStyle = BingMapsImageryStyle.CanvasDark });
            items.Add(new BingMapsImageryView { ImageryStyle = BingMapsImageryStyle.Road });

            items.Add(new AzureMapImageryView { ImageryStyle = AzureMapsImageryStyle.Satellite });
            items.Add(new AzureMapImageryView { ImageryStyle = AzureMapsImageryStyle.DarkGrey });
            items.Add(new AzureMapImageryView { ImageryStyle = AzureMapsImageryStyle.Road });
            items.Add(new AzureMapImageryView { ImageryStyle = AzureMapsImageryStyle.HybridRoadOverlay });
            items.Add(new AzureMapImageryView { ImageryStyle = AzureMapsImageryStyle.WeatherInfraredOverlay });

            //items.Add(new MapQuestImageryView { ImageryStyle = MapQuestImageryStyle.SatelliteMapStyle });
            //items.Add(new MapQuestImageryView { ImageryStyle = MapQuestImageryStyle.StreetMapStyle });
            // add Esri maps
            items.Add(EsriMapImageryViews.WorldStreetMap);
            items.Add(EsriMapImageryViews.WorldTopographicMap);
            items.Add(EsriMapImageryViews.WorldImageryMap);
            items.Add(EsriMapImageryViews.WorldOceansMap);
            items.Add(EsriMapImageryViews.WorldTerrainMap);
            items.Add(EsriMapImageryViews.WorldDeLormesMap);
            items.Add(EsriMapImageryViews.WorldLightGrayMap);
            items.Add(EsriMapImageryViews.WorldPhysicalMap); 
            items.Add(EsriMapImageryViews.WorldAdministrativeOverlay); 
            
            GeoImageryViewComboBox.ItemsSource = items;
            
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
            else if (mapView.ImagerySource == GeoImagerySource.AzureMapsImagery)
            {
                this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
                this.DialogInfoPanel.Visibility = Visibility.Visible;

                ShowAzureMapsImagery((AzureMapImageryView)mapView);
            }
            else if (mapView.ImagerySource == GeoImagerySource.BingMapsImagery)
            {
                if (this.BingMadeMapKey != string.Empty)
                    ShowBingMapsImagery((BingMapsImageryView)mapView);
                else
                {
                    this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
                    this.DialogInfoPanel.Visibility = Visibility.Visible;
                }
            }
            else if (mapView.ImagerySource == GeoImagerySource.EsriMapImagery)
            {
                ShowEsriOnlineMapImagery((EsriMapImageryView)mapView);
            }
            else if (mapView.ImagerySource == GeoImagerySource.MapQuestImagery)
                ShowMapQuestImagery((MapQuestImageryView)mapView);
        }

        private void ShowOpenStreetMapImagery()
        {
            this.GeoMap.BackgroundContent = new OpenStreetMapImagery();
        }

        private void ShowEsriOnlineMapImagery(EsriMapImageryView mapView)
        {
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var esriMap = new ArcGISOnlineMapImagery();
            //esriMap.MapServerUri = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";

            esriMap.MapServerUri = mapView.ImageryServer;

            this.GeoMap.BackgroundContent = esriMap;
        }
        private void ShowMapQuestImagery(MapQuestImageryView mapView)
        {
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
                    default:
                        break;
                }
                
                //Now that Bing is retired, basic keys are no longer valid, hence we are showing images. If you have a valid enterprise key you may comment this code out and uncomment out the BackgroundContent below applying the imagery instead and apply your own api key.
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = mapURI;
                bitmapImage.EndInit();
                mapImage.Source = bitmapImage;
                this.GeoMap.BackgroundContent = mapImage;
                //this.GeoMap.BackgroundContent = new BingMapsMapImagery { ImageryStyle = mapStyle, ApiKey = mapKey, IsDeferredLoad = false };
            }
            else
            {
                this.GeoMap.BackgroundContent = new AzureMapsImagery { ImageryStyle = mapStyle, ApiKey = this.AzureMadeMapKey };
            }
        }

        private void ShowBingMapsImagery(BingMapsImageryView mapView)
        {
            string mapKey = this.BingMadeMapKey;
            var mapImage = new Image();

            if (!String.IsNullOrEmpty(mapKey))
            {
                var mapStyle = (Infragistics.Controls.Maps.BingMapsImageryStyle)mapView.ImageryStyle;
                Uri mapURI = null;
                switch (mapStyle)
                {
                    case Infragistics.Controls.Maps.BingMapsImageryStyle.Aerial:
                        mapURI = new Uri(@"../../Resources/BingAerial.png", UriKind.RelativeOrAbsolute);
                        break;
                    case Infragistics.Controls.Maps.BingMapsImageryStyle.AerialWithLabels:
                        mapURI = new Uri(@"../../Resources/BingAerialWithLabels.png", UriKind.RelativeOrAbsolute);
                        break;
                    case Infragistics.Controls.Maps.BingMapsImageryStyle.Road:
                        mapURI = new Uri(@"../../Resources/BingRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case Infragistics.Controls.Maps.BingMapsImageryStyle.CanvasDark:
                        mapURI = new Uri(@"../../Resources/BingCanvasDark.png", UriKind.RelativeOrAbsolute);
                        break;
                    default:
                        break;
                }
                
                //Now that Bing is retired, basic keys are no longer valid, hence we are showing images. If you have a valid enterprise key you may comment this code out and uncomment out the BackgroundContent below applying the imagery instead and apply your own api key.
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = mapURI;
                bitmapImage.EndInit();
                mapImage.Source = bitmapImage;
                this.GeoMap.BackgroundContent = mapImage;
                //this.GeoMap.BackgroundContent = new BingMapsMapImagery { ImageryStyle = mapStyle, ApiKey = mapKey, IsDeferredLoad = false };

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

