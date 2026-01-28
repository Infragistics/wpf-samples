using System;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;                   // GeographicHighDensityScatterSeries
using Infragistics.Samples.Shared.DataProviders;    // CsvDataProvider
using Infragistics.Samples.Shared.Models;           // GeoPlaceCollection
using Infragistics.Samples.Shared.Resources;        // CommonStrings

namespace IGGeographicMap.Samples
{
    public partial class GeoHighDensitySeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoHighDensitySeries()
        {
            InitializeComponent();
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
            this.heatMaximumSlider.Value = 50;

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.AustraliaRegion);  
         
            // create geographic places from a CSV file);
            this.Places.LoadingDataCompleted += OnLoadingDataCompleted;
            this.Places.Load("GeoPlaces.Australia.csv");

        }
        
        private void OnLoadingDataCompleted(object sender, EventArgs e)
        {
            var series = this.GeoMap.Series[0] as GeographicHighDensityScatterSeries;
            series.ItemsSource = this.Places;
 
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
        }

        protected CsvDataProvider CsvDataProvider;
        protected GeoPlaceCollection Places = new GeoPlaceCollection();

        private void GenerateScatterDataButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.GenerateScatterDataButton.IsEnabled = false;
            // clear the high density data
            var series = this.GeoMap.Series[0] as GeographicHighDensityScatterSeries;
            series.ItemsSource = null;

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.AustraliaRegion);

            // create geographic places from a CSV file);
            this.Places.LoadingDataCompleted += OnLoadingDataCompleted;
            this.Places.Load("GeoPlaces.Australia.csv");
        }

        private void OnResolutionSliderChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (resolutionSlider != null)
            {
                resolutionSlider.Value = Math.Round(resolutionSlider.Value);
            }
        }
        private void OnHeatMinimumSliderChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (heatMinimumSlider != null)
            {
                heatMinimumSlider.Value = Math.Round(heatMinimumSlider.Value);
            }
        }
        private void OnHeatMaximumSliderChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            if (heatMaximumSlider != null)
            {
                heatMaximumSlider.Value = Math.Round(heatMaximumSlider.Value);
            }
        }
    
       
        private void MouseOverEnabled_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MouseOverEnabled.IsChecked == null) return;

            var series = this.GeoMap.Series[0] as GeographicHighDensityScatterSeries;
            if (series != null)
                series.MouseOverEnabled = (bool)MouseOverEnabled.IsChecked;
  
        }

        private void MinColorPicker_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            Color MinSelectedColor = (Color)e.NewColor;
            ((GeographicHighDensityScatterSeries)this.GeoMap.Series[0]).HeatMinimumColor = MinSelectedColor;
        }

        private void MaxColorPicker_SelectedColorChanged(object sender, Infragistics.Controls.Editors.SelectedColorChangedEventArgs e)
        {
            Color MaxSelectedColor = (Color)e.NewColor;
            ((GeographicHighDensityScatterSeries)this.GeoMap.Series[0]).HeatMaximumColor = MaxSelectedColor;
        }

        private void PointExtentSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (PointExtentSlider == null)
            {
                return;
            }
            PointExtentSlider.Value = Math.Round(PointExtentSlider.Value);
            ((GeographicHighDensityScatterSeries)this.GeoMap.Series[0]).PointExtent = (int)PointExtentSlider.Value;
        }


        private void GeographicHighDensityScatterSeries_OnProgressiveLoadStatusChanged(object sender, ProgressiveLoadStatusEventArgs e)
        {
            this.SeriesLoadingProgressBar.Opacity = 1;
            this.SeriesLoadingProgressBar.Value = e.CurrentStatus;
            if (e.CurrentStatus == 100)
            {
                this.SeriesLoadingProgressBar.Opacity = 0;
                this.GenerateScatterDataButton.IsEnabled = true;
            }
        }

        private void UseBruteForce_OnClick(object sender, RoutedEventArgs e)
        {
            ((GeographicHighDensityScatterSeries)this.GeoMap.Series[0]).UseBruteForce = (bool)this.UseBruteForce.IsChecked;
            if (!(bool)this.UseBruteForce.IsChecked)
            {
                this.GenerateScatterDataButton.IsEnabled = false;
                this.SeriesLoadingProgressBar.Opacity = 1;
            }
        }
    }
    
}
