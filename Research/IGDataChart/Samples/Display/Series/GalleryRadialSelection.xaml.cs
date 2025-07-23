using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;
using Infragistics.Samples.Shared.Models;
using Infragistics.Controls.Charts;
using System.Collections.Generic;
using System;
using System.Linq;

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryRadialSelection : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryRadialSelection()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private GallerySampleViewModel _vm;
        private SeriesSelectionMode[] allSeriesSelectionModeValues = (SeriesSelectionMode[])Enum.GetValues(typeof(SeriesSelectionMode));
        private SeriesSelectionBehavior[] allSeriesSelectionBehaviorValues = (SeriesSelectionBehavior[])Enum.GetValues(typeof(SeriesSelectionBehavior));

        #region Event Handlers
        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            _vm = new GallerySampleViewModel();
            _vm.AddSample(this.RadialPieChart, DataChartStrings.XWDC_RadialSeriesType_RadialPieSeries);
            _vm.AddSample(this.RadialColumnChart, DataChartStrings.XWDC_RadialSeriesType_RadialColumnSeries);
            _vm.AddSample(this.RadialAreaChart, DataChartStrings.XWDC_RadialSeriesType_RadialAreaSeries);
            _vm.AddSample(this.RadialLineChart, DataChartStrings.XWDC_RadialSeriesType_RadialLineSeries);
      
            this.PrevItemButton.Click += OnPrevItemButtonClick;
            this.NextItemButton.Click += OnNextItemButtonClick;

            this.ItemSelectionComboBox.ItemsSource = _vm.SamplesNames;
            this.ItemSelectionComboBox.SelectionChanged += OnItemSelectionChanged;
            this.ItemSelectionComboBox.SelectedIndex = 0;

            allSeriesSelectionModeValues = (SeriesSelectionMode[])Enum.GetValues(typeof(SeriesSelectionMode));
            this.ItemSelectionModeComboBox.ItemsSource = allSeriesSelectionModeValues;
            this.ItemSelectionModeComboBox.SelectionChanged += ItemSelectionModeComboBox_SelectionChanged;
            this.ItemSelectionModeComboBox.SelectedIndex = 4;

            allSeriesSelectionBehaviorValues = (SeriesSelectionBehavior[])Enum.GetValues(typeof(SeriesSelectionBehavior));
            this.ItemSelectionBehaviorComboBox.ItemsSource = allSeriesSelectionBehaviorValues;
            this.ItemSelectionBehaviorComboBox.SelectionChanged += ItemSelectionBehaviorComboBox_SelectionChanged; ;
            this.ItemSelectionBehaviorComboBox.SelectedIndex = 5;
            
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
        } 

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

        private void ItemSelectionModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RadialAreaChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.RadialColumnChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.RadialLineChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.RadialPieChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
        }
        private void ItemSelectionBehaviorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.RadialAreaChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.RadialColumnChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.RadialLineChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.RadialPieChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
        }

        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _vm.GetNextSampleIndex();
        }
        private void OnPrevItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _vm.GetPreviousSampleIndex();
        }
        #endregion
    }
}
