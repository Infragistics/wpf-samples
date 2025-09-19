using IGGeographicMap.Extensions;
using IGGeographicMap.Resources;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingGeoTileSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingGeoTileSeries()
        {
            InitializeComponent();
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;

            this.GeoImageryViewComboBox.SelectedIndex = 0;
            this.AzureMadeMapKey = string.Empty;
            //  visit https://learn.microsoft.com/en-us/azure/azure-maps/how-to-manage-account-keys
            // this code block should be comment out when
            // you have your own keys for Bing Maps  
            this.BingMadeMapKey = string.Empty;    
            //  visit http://www.bingmapsportal.com
            // this code block should be comment out when
            // you have your own keys for Bing Maps  

            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            if (series.TileImagery is BingMapsMapImagery)
            {
                var mapKeyProvoder = new GeoImageryKeyProvider();
                mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
                mapKeyProvoder.GetMapKeys();
            }
           
        } 
        protected string BingMadeMapKey;
        protected string AzureMadeMapKey;
        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;

            foreach (var element in e.Result)
            {
                if (element.Name == "BingMaps") this.BingMadeMapKey = element.Key; 
            }
            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            series.TileImagery = new BingMapsMapImagery { ImageryStyle = BingMapsImageryStyle.Aerial, ApiKey = BingMadeMapKey, IsDeferredLoad = false };
        }

        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnShapefileCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.LoadedItemsCount += e.NewItems.Count;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadingItem + " " + this.LoadedItemsCount + "...";
        }
        protected int LoadedItemsCount;

       
        private void OnGeoImageryViewComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.GeoMap == null) return;

            var mapView = (GeoImageryView)e.AddedItems[0];
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
                if (String.IsNullOrEmpty(AzureMadeMapKey))
                {
                    this.DialogInfoPanel.Visibility = Visibility.Visible;
                }
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
                ShowEsriOnlineMapImagery();
            }
        }

        private void ShowOpenStreetMapImagery()
        {
            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            series.TileImagery = new OpenStreetMapImagery();
        }

        private void ShowEsriOnlineMapImagery()
        {
            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            var publicMap = new ArcGISOnlineMapImagery();
            publicMap.MapServerUri = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";
            series.TileImagery = publicMap;
        }
         
        private void ShowBingMapsImagery(BingMapsImageryView mapView)
        {
            string mapKey = this.BingMadeMapKey;

            if (!String.IsNullOrEmpty(mapKey))
            {
                var mapStyle = (Infragistics.Controls.Maps.BingMapsImageryStyle)mapView.ImageryStyle;

                var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
                series.TileImagery = new BingMapsMapImagery { ImageryStyle = mapStyle, ApiKey = mapKey, IsDeferredLoad = false };
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
            this.AzureMadeMapKey = this.EnterAzureKey.Text;
        }
    }

}
