using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using System.Collections.Specialized;
using System.Windows.Media;

namespace IGGeographicMap.Samples.Display
{
    public partial class GeoTileSeriesFillRule : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoTileSeriesFillRule()
        {
            InitializeComponent();
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.GeoMap.GridAreaRectChanged += OnMapGridAreaRectChanged;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
             
            this.BingMadeMapKey = string.Empty;     //  visit http://www.bingmapsportal.com
            // this code block should be comment out when
            // you have your own keys for Bing Maps 
            var mapKeyProvoder = new GeoImageryKeyProvider();
            mapKeyProvoder.GetMapKeyCompleted += OnGetMapKeyCompleted;
            mapKeyProvoder.GetMapKeys();

            var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
            var shapefilePoints = new List<List<Point>>();
            var rectPoints = new List<Point> { new Point(30, 60), new Point(30, 0), new Point(-30, 0), new Point(-30, 60) };
            var rectPoints1 = new List<Point> { new Point(0, 30), new Point(60, 30), new Point(60, -30), new Point(0, -30) };

            shapefilePoints.Add(rectPoints);
            shapefilePoints.Add(rectPoints1);
            series.ShapeMemberPath = "";
            series.ItemsSource = shapefilePoints;
        }

        void OnMapGridAreaRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            FillModeComboBox.SelectedIndex = 0;
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
            this.GeoMap.Visibility = Visibility.Visible;
            this.MapLoadingContainer.Visibility = Visibility.Collapsed;
        }

        private void OnShapefileCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.LoadedItemsCount += e.NewItems.Count;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadingItem + " " + this.LoadedItemsCount + "...";
        }
        protected int LoadedItemsCount;

        private void OnFillModeComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cmb = sender as ComboBox;
            if (cmb != null)
            {
                var tag = ((ComboBoxItem)cmb.SelectedItem).Tag as string;
                var series = this.GeoMap.Series.OfType<GeographicTileSeries>().First();
                series.ClippingFillRule = (FillRule)System.Enum.Parse(typeof(FillRule), tag, true);
            }

        }
    }
}
