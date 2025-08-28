using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using IGGeographicMap.Resources;
using IGGeographicMap.Samples.Custom;               // GeoMapAdapter
using Infragistics.Controls.Maps;
using Infragistics.Samples.Services;                // BingMapsConnector
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
    public partial class CreatingCustomAzureMapImagery : Infragistics.Samples.Framework.SampleContainer
    {
        public CreatingCustomAzureMapImagery()
        {         
            InitializeComponent();
          
            // must provide your own keys for Azure Maps to display geo-imagery in the Geographic Map control
            this.AzureMadeMapKey = string.Empty;     //  visit https://learn.microsoft.com/en-us/azure/azure-maps/how-to-manage-account-keys

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

            var azureMapsConnector = new AzureMapsMapImagery();
            GeoMap.BackgroundContent = azureMapsConnector;
            azureMapsConnector.ApiKey = AzureMadeMapKey;
            azureMapsConnector.ImageryStyle = Infragistics.Controls.Maps.AzureMapsImageryStyle.Imagery; //mapView.ImageryStyle;
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

            this.DialogInfoPanel.Visibility = Visibility.Collapsed;

            if (this.AzureMadeMapKey != string.Empty)
            {
                ShowAzureMapsImagery((AzureMapImageryView)mapView);
            }
            else
            {
                this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
                this.DialogInfoPanel.Visibility = Visibility.Visible;
            } 
        }

        private void ShowAzureMapsImagery(AzureMapImageryView mapView)
        {
            string mapKey = this.AzureMadeMapKey;
            var mapImage = new Image();
            if (!String.IsNullOrEmpty(mapKey))
            {
                var mapStyle = mapView.ImageryStyle;
                Uri mapURI = null;
                switch (mapStyle)
                {
                    case AzureMapsImageryStyle.DarkGrey:
                        mapURI = new Uri(@"../../Resources/AzureDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.HybridRoad:
                        mapURI = new Uri(@"../../Resources/AzureHybridRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.Road:
                        mapURI = new Uri(@"../../Resources/AzureRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.Imagery:
                        mapURI = new Uri(@"../../Resources/AzureImagery.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficAbsolute:
                        mapURI = new Uri(@"../../Resources/AzureTrafficAndRoad.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.WeatherInfrared:
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
        }
        //private void ShowBingMapsImagery(BingMapsImageryView mapView)
        //{
        //    string mapKey = this.BingMadeMapKey;

        //    if (!String.IsNullOrEmpty(mapKey))
        //    {
        //        var bingMapsConnector = new BingMapsConnector();
        //        bingMapsConnector.ImageryInitialized += OnImageryInitialized;
        //        bingMapsConnector.ImageryStyle = mapView.ImageryStyle;
        //        bingMapsConnector.ApiKey = mapKey;
        //        try
        //        {
        //            bingMapsConnector.Initialize();
        //        }
        //        catch (Exception)
        //        {
        //            MessageBox.Show(MapStrings.GeoMapInternetRequired, MapStrings.GeoMapNoInternet, MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}

        public void OnImageryInitialized(object sender, EventArgs e)
        {

            //Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
            //   (Action)(() => UpdateAzureMaps(sender)));
        }
        //private void UpdateBingMaps(object sender)
        //{
        //    // display geo-imagery from Bing Maps
        //    //var connector = (BingMapsConnector)sender;
        //    //this.GeoMap.BackgroundContent =
        //    //    new BingMapsMapImagery()
        //    //    {
        //    //        TilePath = connector.TilePath,
        //    //        SubDomains = connector.SubDomains
        //    //    };
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogInfoPanel.Visibility = Visibility.Collapsed;
        }
    }


}
