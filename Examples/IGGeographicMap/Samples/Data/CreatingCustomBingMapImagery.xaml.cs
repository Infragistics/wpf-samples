using System;
using System.Windows;
using System.Windows.Controls;
using IGGeographicMap.Extensions;
using IGGeographicMap.Models;
using IGGeographicMap.Resources;
using IGGeographicMap.Samples.Custom;               // GeoMapAdapter
using Infragistics.Controls.Maps;
using Infragistics.Samples.Services;                // BingMapsConnector
using Infragistics.Samples.Shared.DataProviders;    // GeoImageryKeyProvider
using Infragistics.Samples.Shared.Models;
using BingMapsImageryStyle = Infragistics.Controls.Maps.BingMapsImageryStyle;
using System.Globalization;
using System.Threading;

namespace IGGeographicMap.Samples.Data
{
    public partial class CreatingCustomBingMapImagery : Infragistics.Samples.Framework.SampleContainer
    {
        public CreatingCustomBingMapImagery()
        {         
            InitializeComponent();
          
            // must provide your own keys for Bing Maps to display geo-imagery in the Geographic Map control
            this.BingMadeMapKey = string.Empty;     //  visit www.bingmapsportal.com

            // this code block should be comment out when you have your own keys for Bing Maps
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();

            this.Loaded += OnSampleLoaded;
        }
        protected string BingMadeMapKey;

        private void OnGetMapKeyCompleted(object sender, GetMapKeyCompletedEventArgs e)
        {
            if (e.Error != null) return;
            foreach (var element in e.Result)
            {
                if (element.Name == "BingMaps") this.BingMadeMapKey = element.Key;
            }

            var bingMapsConnector = new BingMapsMapImagery();
            GeoMap.BackgroundContent = bingMapsConnector;
            bingMapsConnector.ApiKey = BingMadeMapKey;
            bingMapsConnector.ImageryStyle = BingMapsImageryStyle.Aerial; //mapView.ImageryStyle;
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

            if (this.BingMadeMapKey != string.Empty)
            {
                ShowBingMapsImagery((BingMapsImageryView)mapView);
            }
            else
            {
                this.DialogInfoTextBlock.Text = MapStrings.XWGM_MissingMicrosoftMapKey;
                this.DialogInfoPanel.Visibility = Visibility.Visible;
            } 
        }

        private void ShowBingMapsImagery(BingMapsImageryView mapView)
        {
            string mapKey = this.BingMadeMapKey;

            if (!String.IsNullOrEmpty(mapKey))
            {
                var bingMapsConnector = new BingMapsConnector();
                bingMapsConnector.ImageryInitialized += OnImageryInitialized;
                bingMapsConnector.ImageryStyle = mapView.ImageryStyle;
                bingMapsConnector.ApiKey = mapKey;
                try
                {
                    bingMapsConnector.Initialize();
                }
                catch(Exception)
                {
                    MessageBox.Show(MapStrings.GeoMapInternetRequired, MapStrings.GeoMapNoInternet, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void OnImageryInitialized(object sender, EventArgs e)
        {

            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
               (Action)(() => UpdateBingMaps(sender)));
        }
        private void UpdateBingMaps(object sender)
        {
            // display geo-imagery from Bing Maps
            var connector = (BingMapsConnector)sender;
            this.GeoMap.BackgroundContent =
                new BingMapsMapImagery()
                {
                    TilePath = connector.TilePath,
                    SubDomains = connector.SubDomains
                };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogInfoPanel.Visibility = Visibility.Collapsed;
        }
    }


}
