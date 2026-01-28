using System.Windows;
using System.Windows.Media;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Controls;
using System.Windows.Controls;

namespace IGGeographicMap.Samples
{
    public partial class GeoContourLineSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoContourLineSeries()
        {
            InitializeComponent();
            this.GeoMap.Visibility = Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
        }
        protected BrushCollection SelectedBrushes = new BrushCollection();
        protected GeographicContourLineSeries GeoSeries;
        protected ItfConverter itfConverter;

        private int GetMapLoadedItemsCount()
        {
            var itfProvider = this.LayoutRoot.Resources["itfConverter"] as ItfConverter;
            if (itfProvider == null) return 0;

            return itfProvider.TriangulationSource != null ? itfProvider.TriangulationSource.Triangles.Count : 0;
        }

        private void OnTriangulatedProviderPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("TriangulationSource"))
            {
                this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadedItems + " " + this.GetMapLoadedItemsCount();
                this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;

                itfConverter = this.LayoutRoot.Resources["itfConverter"] as ItfConverter;
           
                GeoSeries = (GeographicContourLineSeries)this.GeoMap.Series[0];

                // show and set bounds of geo-map world 
                var mapRegion = new GeoRegion(new GeoLocation(-110, 50),
                                              new GeoLocation(-80, 15));
                Rect geoRect = mapRegion.ToGeoRect();
                this.GeoMap.WorldRect = geoRect;
                this.GeoMap.Visibility = Visibility.Visible;
            }
        }

        private void BrushScaleMinimumValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeriesFillScale();        
        }

        private void BrushScaleMaximumValueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeriesFillScale();     
        }
        private void OnBrushPaletteChanged(object sender, SelectionChangedEventArgs e)
        {
            var brushPalette = (BrushPalette)e.AddedItems[0];

            var brushes = new BrushCollection();
            foreach (var item in brushPalette)
            {
                brushes.Add(item);
            }
            this.SelectedBrushes = brushes;
            UpdateGeoSeriesFillScale();
        }
       
        private void UpdateGeoSeriesFillScale()
        {
            if (GeoSeries != null)
            {
                // update fill scale of the GeographicContourLineSeries
                var fillScale = new ValueBrushScale();
                fillScale.Brushes = this.SelectedBrushes;
                fillScale.MinimumValue = this.BrushScaleMinimumValueSlider.Value;
                fillScale.MaximumValue = this.BrushScaleMaximumValueSlider.Value;
                GeoSeries.FillScale = fillScale;

                // refresh the GeographicContourLineSeries
                GeoSeries.ItemsSource = null;
                GeoSeries.ItemsSource = itfConverter.TriangulationSource.Points;               
            }
        }      
    }
}
