using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using IGGeographicMap.Extensions;
using IGGeographicMap.Resources;
using Infragistics.Controls.Maps;
using System.Collections.Specialized;
using Infragistics.Samples.Shared.Resources;

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
                if (this.AzureMadeMapKey != string.Empty)
                    ShowAzureMapsImagery((AzureMapImageryView)mapView);
                else
                {
                    this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
                    this.DialogInfoPanel.Visibility = Visibility.Visible;
                }
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

            if (!String.IsNullOrEmpty(mapKey))
            {
                var mapStyle = (Infragistics.Controls.Maps.AzureMapsImageryStyle)mapView.ImageryStyle;

                var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
                series.TileImagery = new AzureMapsImagery { ImageryStyle = mapStyle, ApiKey = mapKey };
            }
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogInfoPanel.Visibility = Visibility.Collapsed;

        }
    }

}
