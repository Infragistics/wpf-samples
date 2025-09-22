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
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
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
            
            // must provide your own keys for Azure Maps to display geo-imagery in the Geographic Map control
            this.AzureMadeMapKey = string.Empty; //  visit https://learn.microsoft.com/en-us/azure/azure-maps/how-to-manage-account-keys
            this.Loaded += BindingGeoTileSeries_Loaded;
        }

        private void BindingGeoTileSeries_Loaded(object sender, RoutedEventArgs e)
        {
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);

        }

        protected string AzureMadeMapKey;
        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;
        }

        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            //this.GeoMap.NavigateTo(GeoRegions.UnitedStatesRegion); // using GeoMapAdapter class
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
            this.GeoMap.BackgroundContent = null;
            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            series.TileImagery = null;
            // display geo-imagery based on selected map view
            if (mapView.ImagerySource == GeoImagerySource.OpenStreetMapImagery)
            {
                this.DialogInfoPanel.Visibility = Visibility.Collapsed;

                ShowOpenStreetMapImagery();
            }
            else if (mapView.ImagerySource == GeoImagerySource.EsriMapImagery)
            {
                this.DialogInfoPanel.Visibility = Visibility.Collapsed;

                ShowEsriOnlineMapImagery();
            }
            else if (mapView.ImagerySource == GeoImagerySource.AzureMapsImagery)
            {
                this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
                this.DialogInfoPanel.Visibility = Visibility.Visible;
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
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);
            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            series.TileImagery = new OpenStreetMapImagery();
        }

        private void ShowEsriOnlineMapImagery()
        {
            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var esriMap = new ArcGISOnlineMapImagery();
            //esriMap.MapServerUri = "http://services.arcgisonline.com/ArcGIS/rest/services/World_Street_Map/MapServer";
            EsriMapImageryView mapView = new EsriMapImageryView();
            mapView.ImageryStyle = EsriMapImageryStyle.WorldStreetMap;
            esriMap.MapServerUri = mapView.ImageryServer;

            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            series.TileImagery = esriMap;


        }
       
        private void ShowAzureMapsImagery(AzureMapImageryView mapView)
        {
            string mapKey = this.AzureMadeMapKey;
            var mapImage = new Image();
            var mapStyle = mapView.ImageryStyle;
            if (String.IsNullOrEmpty(AzureMadeMapKey))
            {
                this.DialogInfoPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.DialogInfoPanel.Minimize();
            }

            GeoMapAdapter.ZoomMapToLocation(this.GeoMap, GeoLocations.CityNewYork, 2);
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
                        mapURI = new Uri(@"../../Resources/AzureDarkGrey.png", UriKind.RelativeOrAbsolute);
                        break;
                    case AzureMapsImageryStyle.TrafficRelativeOverlay:
                        mapURI = new Uri(@"../../Resources/AzureTrafficRelative.png", UriKind.RelativeOrAbsolute);
                        break;
                    default:
                        break;
                }

                //Basic keys are no longer valid, hence we are showing images. If you have a valid enterprise key you may comment this code out and uncomment out the BackgroundContent below applying the imagery instead and apply your own api key.
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = mapURI;
                bitmapImage.EndInit();
                mapImage.Source = bitmapImage;
                var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
                series.TileImagery = null;
                this.GeoMap.BackgroundContent = mapImage;
                
            }
            else
            {
                var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
                series.TileImagery = new AzureMapsImagery { ImageryStyle = mapStyle, ApiKey = this.AzureMadeMapKey };
                this.GeoMap.BackgroundContent = new OpenStreetMapImagery { Opacity = 0.25 };

            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.AzureMadeMapKey = EnterAzureKey.Text;
            ShowAzureMapsImagery(((AzureMapImageryView)this.GeoImageryViewComboBox.SelectedValue));
            this.DialogInfoPanel.Minimize();

        }

        private void EnterAzureKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.AzureMadeMapKey = this.EnterAzureKey.Text;
        }

        private void ButtonClick2(object sender, RoutedEventArgs e)
        {
            EnterAzureKey.Text = String.Empty;
        }
    }

}
