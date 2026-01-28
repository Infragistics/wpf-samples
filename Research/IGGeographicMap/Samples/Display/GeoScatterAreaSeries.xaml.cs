using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Controls;

namespace IGGeographicMap.Samples
{
    public partial class GeoScatterAreaSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoScatterAreaSeries()
        {
            InitializeComponent();

            itfProvider = (ItfConverter)this.LayoutRoot.Resources["itfConverter"];
            itfProvider.PropertyChanged += OnTriangulatedProviderPropertyChanged;

            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
        }
        protected ObservableCollection<Color> SelectedColors = new ObservableCollection<Color>();
        protected ColorsPalette SelectedColorsPalette = new ColorsPalette();
        protected bool IsReversedColorsPalette = false;

        protected ColorScaleInterpolationMode SelectedInterpolationMode;
        protected GeographicScatterAreaSeries GeoSeries;
        protected ItfConverter itfProvider;

        private int GetMapLoadedItemsCount()
        {
            //var itfProvider = this.LayoutRoot.Resources["itfConverter"] as ItfConverter;
            if (itfProvider == null) return 0;

            return itfProvider.TriangulationSource != null ? itfProvider.TriangulationSource.Triangles.Count : 0;
        }

        private void OnTriangulatedProviderPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        { 
            if (e.PropertyName.Equals("TriangulationSource"))
            {
                this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadedItems + " " + this.GetMapLoadedItemsCount();
                this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;

                var itfFileSource = sender as ItfConverter;

                GeoSeries = (GeographicScatterAreaSeries)this.GeoMap.Series[0];
          
                // show and set bounds of geo-map world 
                var mapRegion = new GeoRegion(new GeoLocation(-110, 50),
                                              new GeoLocation(-80, 15));
                Rect geoRect = mapRegion.ToGeoRect();
                this.GeoMap.WorldRect = geoRect;
             
                this.GeoMap.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void BrushScaleMinimumValueSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeriesColorScale(); 
        }

        private void BrushScaleMaximumValueSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateGeoSeriesColorScale();
        }
       
 
        private void BrushPaletteComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SelectedColorsPalette = (ColorsPalette)e.AddedItems[0];

            this.SelectedColors = new ObservableCollection<Color>();
            foreach (Color color in SelectedColorsPalette)
            {
                this.SelectedColors.Add(color);
            }
           
            UpdateGeoSeriesColorScale();

        }
        private void BrushInterpolationModeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.SelectedInterpolationMode = (ColorScaleInterpolationMode)e.AddedItems[0];
       
            UpdateGeoSeriesColorScale();
        }
        
        private void UpdateGeoSeriesColorScale()
        {
            if (GeoSeries != null)
            {
                GeoSeries.ItemsSource = null;
                
                // update fill scale of the GeographicContourLineSeries
                var colorScale = new CustomPaletteColorScale();
                colorScale.Palette = this.SelectedColors;
                colorScale.MinimumValue = this.BrushScaleMinimumValueSlider.Value;
                colorScale.MaximumValue = this.BrushScaleMaximumValueSlider.Value;
                colorScale.InterpolationMode = this.SelectedInterpolationMode;
                GeoSeries.ColorScale = colorScale;

                // refresh the GeographicContourLineSeries
                GeoSeries.ItemsSource = itfProvider.TriangulationSource.Points;
            }
        }
        
        private void BrushOpacityValueSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (GeoSeries != null)
            {
                GeoSeries.Opacity = e.NewValue;
            }
        }
       
    }
}
