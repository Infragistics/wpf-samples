using System.Collections.Specialized;
using System.Linq;
using IGGeographicMap.Models;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IGGeographicMap.Samples
{
    public partial class GeoTileImagerySeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoTileImagerySeries()
        {
            InitializeComponent();
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;

            this.TileImageryShapeComboBox.SelectedIndex = 1; 
            this.BingMadeMapKey = string.Empty;     //  visit http://www.bingmapsportal.com
            // this code block should be comment out when
            // you have your own keys for Bing Maps  
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();

        } 
        protected string BingMadeMapKey;

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
            if (this.MapLoadingContainer != null)
                this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnShapefileCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.LoadedItemsCount += e.NewItems.Count;
            if(this.MapLoadingStatus != null)
                this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadingItem + " " + this.LoadedItemsCount + "...";
        }
        protected int LoadedItemsCount;

        private void OnClearMapBackgroundCheckBoxChanged(object sender, RoutedEventArgs e)
        {
            ShowMapBackground((bool)this.ClearMapBackgroundCheckBox.IsChecked);
        }
        private void OnTileImageryShapeComboBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            this.GeoRegionHorizontalSlider.IsEnabled = false;
            this.GeoRegionVerticalSlider.IsEnabled = false;
            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            switch (this.TileImageryShapeComboBox.SelectedIndex)
            {
                case 0:
                    var worldRegionPoints = new List<Point> { new Point(-180, 85), new Point(180, 85), new Point(180, -85), new Point(-180, -85) };
                    var worldRegion = new List<List<Point>> { worldRegionPoints };
                    series.ShapeMemberPath = "";
                    series.ItemsSource = worldRegion;
                    break;
                case 1:
                    series.ShapeMemberPath = "Points";
                    series.ItemsSource = this.Resources["WorldContinentsShapefile"] as ShapefileConverter;
                    break;
                case 3:
                    var americaRegionPoints = new List<Point> { new Point(-180, 70), new Point(-50, 70), new Point(-50, 5), new Point(-180, 5) };
                    var americaRegion = new List<List<Point>> { americaRegionPoints };
                    series.ShapeMemberPath = "";
                    series.ItemsSource = americaRegion;
                    break;
                case 2:
                    this.GeoRegionHorizontalSlider.IsEnabled = true;
                    this.GeoRegionVerticalSlider.IsEnabled = true;
                    UpdateCustomRegion();
                    break;
            }
        }
        private void ShowMapBackground(bool isMapBackgroundEnabled)
        {
            var opacity = isMapBackgroundEnabled ? 0 : 1;
            this.GeoMap.BackgroundContent = new OpenStreetMapImagery { Opacity = opacity };
        }
        private void UpdateCustomRegion()
        {
            double west = -100; ;
            double east = 100; ;
            double north = 60; ;
            double south = -30; ;

            var customRegion = new List<List<Point>>();
            if (this.GeoRegionHorizontalSlider != null)
            {
                if (this.GeoRegionHorizontalSlider.Thumbs.Count > 1)
                {
                    west = this.GeoRegionHorizontalSlider.Thumbs[0].Value;
                    east = this.GeoRegionHorizontalSlider.Thumbs[1].Value;
                }
            }
            if (this.GeoRegionVerticalSlider != null)
            {
                if (this.GeoRegionVerticalSlider.Thumbs.Count > 1)
                {
                    south = this.GeoRegionVerticalSlider.Thumbs[0].Value;
                    north = this.GeoRegionVerticalSlider.Thumbs[1].Value;
                }
            }

            var shapePoints = new List<Point> { new Point(west, north), new Point(east, north), new Point(east, south), new Point(west, south) };
            customRegion.Add(shapePoints);

            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            series.ShapeMemberPath = "";
            series.ItemsSource = customRegion;
        }
        private void OnGeoRegionSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateCustomRegion();
        }
    }
   
}
