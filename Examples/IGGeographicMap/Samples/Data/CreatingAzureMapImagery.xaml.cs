using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using IGGeographicMap.Resources;
using IGGeographicMap.Samples.Custom;               // GeoMapAdapter
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;    // GeoImageryKeyProvider
using Infragistics.Samples.Shared.Models;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace IGGeographicMap.Samples.Data
{
    public partial class CreatingAzureMapImagery : Infragistics.Samples.Framework.SampleContainer
    {
        public CreatingAzureMapImagery()
        {         
            InitializeComponent();
          
            // must provide your own keys for Azure Maps to display geo-imagery in the Geographic Map control
            this.AzureMadeMapKey = string.Empty;     //  visit https://learn.microsoft.com/en-us/azure/azure-maps/azure-maps-authentication

            // this code block should be comment out when you have your own keys for Azure Maps
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();

            this.Loaded += OnSampleLoaded;
        }
        protected string AzureMadeMapKey;

        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;
            foreach (var element in e.Result)
            {
                if (element.Name == "AzureMaps") this.AzureMadeMapKey = element.Key;
            }

            var azureMapsConnector = new AzureMapsImagery();
            GeoMap.BackgroundContent = azureMapsConnector;
            azureMapsConnector.ApiKey = AzureMadeMapKey;
            azureMapsConnector.ImageryStyle = Infragistics.Controls.Maps.AzureMapsImageryStyle.Satellite; //mapView.ImageryStyle;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.GeoImageryViewComboBox.SelectedIndex = 0;
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);
        }

        private void OnGeoImageryViewComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.GeoMap == null) return;

            var mapView = (GeoImageryView)e.AddedItems[0];
            if (mapView == null) return;

            this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
            if (String.IsNullOrEmpty(AzureMadeMapKey))
            {
                this.DialogInfoPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.DialogInfoPanel.Minimize();
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
                        mapURI = new Uri(@"../../Resources/AzureHybridRoad2.png", UriKind.RelativeOrAbsolute);
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
                        mapURI = new Uri(@"../../Resources/AzureWeatherRadar.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.HybridDarkGreyOverlay:
                        mapURI = new Uri(@"../../Resources/AzureHybridDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.LabelsDarkGreyOverlay:
                        mapURI = new Uri(@"../../Resources/AzureLabelsDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.LabelsRoadOverlay:
                        mapURI = new Uri(@"../../Resources/AzureLabelsRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TerraOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTerraOverlay.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficDelayOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficDelay.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficReducedOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficLight.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficRelativeDarkOverlay:
                        mapURI = new Uri(@"../../Resources/azure_traffic_relative_dark.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficRelativeOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficRelative.png", UriKind.RelativeOrAbsolute);
                        break;
                    default:
                        break;
                }

                //Basic keys are no longer valid with Azure, hence we are showing images. If you have a valid enterprise key you may comment this code out and uncomment out the BackgroundContent below applying the imagery instead and apply your own api key.
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

        public void OnImageryInitialized(object sender, EventArgs e)
        {
        }
      
        private void EnterAzureKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.AzureMadeMapKey = EnterAzureKey.Text;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.AzureMadeMapKey = EnterAzureKey.Text;
            ShowAzureMapsImagery(((AzureMapImageryView)this.GeoImageryViewComboBox.SelectedValue));
            this.DialogInfoPanel.Minimize();
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            EnterAzureKey.Text = String.Empty;
        }
    }


}
