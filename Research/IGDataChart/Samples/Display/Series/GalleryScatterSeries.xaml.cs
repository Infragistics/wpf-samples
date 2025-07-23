using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using Infragistics.Controls.Charts;
using System.Windows.Data;
using System;
using System.Globalization;

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryScatterSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryScatterSeries()
        { 
            InitializeComponent();
            UseDefaultTheme = true;
            InitializeSample();
            
        }
        private GallerySampleViewModel _vm;              
         
        private void InitializeSample()
        {
            _vm = new GallerySampleViewModel();

            _vm.AddSample(this.BubbleChart, DataChartStrings.XWDC_BubbleSeriesType_BubbleSeries);
            _vm.AddSample(this.MarkerChart, DataChartStrings.XWDC_ScatterSeries_ScatterSeries);
            _vm.AddSample(this.LineChart, DataChartStrings.XWDC_ScatterSeries_ScatterLineSeries);
            _vm.AddSample(this.SplineChart, DataChartStrings.XWDC_ScatterSeries_ScatterSplineSeries);
            _vm.AddSample(this.ContourChart, DataChartStrings.XWDC_ScatterSeries_ScatterContourSeries + " (NEW)");
            _vm.AddSample(this.AreaChart, DataChartStrings.XWDC_ScatterSeries_ScatterAreaSeries + " (NEW)");
              
            InitializeCharts();

            this.PrevItemButton.Click += OnPrevItemButtonClick;
            this.NextItemButton.Click += OnNextItemButtonClick;

            this.ItemSelectionComboBox.ItemsSource = _vm.SamplesNames;
            this.ItemSelectionComboBox.SelectionChanged += OnItemSelectionChanged;
            this.ItemSelectionComboBox.SelectedIndex = 0;
        }
        private void InitializeCharts()
        {
            foreach (XamDataChart chart in _vm.Samples)
            {
                Update(chart, Visibility.Collapsed);
            }
        }
        private void Update(XamDataChart chart, Visibility isVisibile)
        {
            foreach (var series in chart.Series)
            {
                series.Visibility = isVisibile;
                series.ReplayTransitionIn();
            }
            chart.Visibility = isVisibile;
            chart.WindowRect = new Rect(0, 0, 1, 1);
            
            if (chart == ContourChart || chart == ContourChart)
                this.Legend.Visibility = Visibility.Collapsed;
            else
                this.Legend.Visibility = Visibility.Visible;
        } 
        
        #region Event Handlers
      
        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _vm.ShowSample(this.ItemSelectionComboBox.SelectedIndex);
            for (int i = 0; i < _vm.Samples.Count; i++)
            {
                var chart = _vm.Samples[i] as XamDataChart;
                if (i == this.ItemSelectionComboBox.SelectedIndex)
                    Update(chart, Visibility.Visible);
                else
                    Update(chart, Visibility.Collapsed);
            }
        }
        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _vm.GetNextSampleIndex();
        }

        private void OnPrevItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _vm.GetPreviousSampleIndex();
        }
           
        private void OnFilterHighIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita >= 30000;
            }
        }
        private void OnFilterMediumIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita > 10000 &&
                             model.GdpPerCapita < 30000;
            }
        }
        private void OnFilterLowIncome(object sender, FilterEventArgs e)
        {
            var model = e.Item as CountryDataModel;
            if (model != null)
            {
                e.Accepted = model.GdpPerCapita <= 10000;
            }
        }

        #endregion

        

     
    }
   
}
