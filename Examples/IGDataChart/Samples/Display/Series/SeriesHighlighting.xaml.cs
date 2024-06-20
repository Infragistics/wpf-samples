using System;
using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;                    // DataChartStrings
using Infragistics;                             // BrushCollection
using Infragistics.Controls.Charts;             // XamDataChart 
using Infragistics.Samples.Shared.Models;       // GallerySampleViewModel
using Infragistics.Samples.Shared.Resources;    // CommonStrings

namespace IGDataChart.Samples.Display.Series
{
    public partial class SeriesHighlighting : Infragistics.Samples.Framework.SampleContainer
    {
        public SeriesHighlighting()
        {
            InitializeComponent();
            InitializeSample();
        }

        private GallerySampleViewModel _samplesViewModel;

        private void InitializeSample()
        {
            _samplesViewModel = new GallerySampleViewModel();

            _samplesViewModel.AddSample(this.AreaChart, DataChartStrings.XWDC_CategorySeries_AreaSeries);
            _samplesViewModel.AddSample(this.ColumnChart, DataChartStrings.XWDC_CategorySeries_ColumnSeries);
            _samplesViewModel.AddSample(this.BarChart, DataChartStrings.XWDC_CategorySeries_BarSeries);
            _samplesViewModel.AddSample(this.LineChart, DataChartStrings.XWDC_CategorySeries_LineSeries);
            _samplesViewModel.AddSample(this.PointChart, DataChartStrings.XWDC_CategorySeries_PointSeries);
            _samplesViewModel.AddSample(this.SplineChart, DataChartStrings.XWDC_CategorySeries_SplineSeries);
            _samplesViewModel.AddSample(this.SplineAreaChart, DataChartStrings.XWDC_CategorySeries_SplineAreaSeries);
            _samplesViewModel.AddSample(this.StepLineChart, DataChartStrings.XWDC_CategorySeries_StepLineSeries);
            _samplesViewModel.AddSample(this.StepAreaChart, DataChartStrings.XWDC_CategorySeries_StepAreaSeries);
            _samplesViewModel.AddSample(this.RangeColumnChart, DataChartStrings.XWDC_CategorySeries_RangeColumnSeries);
            _samplesViewModel.AddSample(this.RangeAreaChart, DataChartStrings.XWDC_CategorySeries_RangeAreaSeries);
            _samplesViewModel.AddSample(this.WaterfallChart, DataChartStrings.XWDC_CategorySeries_WaterfallSeries);
            _samplesViewModel.AddSample(this.FinancialPriceChart, DataChartStrings.XWDC_FinancialPriceSeries);
            _samplesViewModel.AddSample(this.FinancialIndicatorChart, DataChartStrings.XWDC_FinancialIndicators);
            _samplesViewModel.AddSample(this.FinancialOverlayChart, DataChartStrings.XWDC_FinancialOverlays);
        
            InitializeCharts();

            this.PrevItemButton.Click += OnPrevItemButtonClick;
            this.NextItemButton.Click += OnNextItemButtonClick;

            this.ItemSelectionComboBox.ItemsSource = _samplesViewModel.SamplesNames;
            this.ItemSelectionComboBox.SelectionChanged += OnItemSelectionChanged;
            this.ItemSelectionComboBox.SelectedIndex = 1;

            this.HighlightModeComboBox.SelectionChanged += OnHighlightModeChanged;
            this.HighlightModeComboBox.SelectedIndex = 0;

            this.HighlightBehaviorComboBox.SelectionChanged += OnHighlightBehaviorChanged;
            this.HighlightBehaviorComboBox.SelectedIndex = 1;
        }

        private void OnHighlightModeChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (XamDataChart chart in _samplesViewModel.Samples)
            {
                if(HighlightModeComboBox.SelectedIndex == -1)
                {
                    chart.HighlightingMode = SeriesHighlightingMode.Auto;
                }
                else if (HighlightModeComboBox.SelectedIndex == 0)
                {
                    chart.HighlightingMode = SeriesHighlightingMode.Auto;
                }
                else if (HighlightModeComboBox.SelectedIndex == 1)
                {
                    chart.HighlightingMode = SeriesHighlightingMode.FadeOthers;
                }
            }
        }

        private void OnHighlightBehaviorChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (XamDataChart chart in _samplesViewModel.Samples)
            {
                if (HighlightBehaviorComboBox.SelectedIndex == -1)
                {
                    chart.HighlightingBehavior = SeriesHighlightingBehavior.Auto;
                }
                else if (HighlightBehaviorComboBox.SelectedIndex == 0)
                {
                    chart.HighlightingBehavior = SeriesHighlightingBehavior.Auto;
                }
                else if (HighlightBehaviorComboBox.SelectedIndex == 1)
                {
                    chart.HighlightingBehavior = SeriesHighlightingBehavior.DirectlyOver;
                }
                else if (HighlightBehaviorComboBox.SelectedIndex == 2)
                {
                    chart.HighlightingBehavior = SeriesHighlightingBehavior.NearestItems;
                }
                else if (HighlightBehaviorComboBox.SelectedIndex == 3)
                {
                    chart.HighlightingBehavior = SeriesHighlightingBehavior.NearestItemsAndSeries;
                }
                else if (HighlightBehaviorComboBox.SelectedIndex == 4)
                {
                    chart.HighlightingBehavior = SeriesHighlightingBehavior.NearestItemsRetainMainShapes;
                }
            }
        }

        private void InitializeCharts()
        {
            foreach (XamDataChart chart in _samplesViewModel.Samples)
            {
                ChangeChartsVisibility(chart, Visibility.Collapsed);
            }
        }
        private void ChangeChartsVisibility(XamDataChart chart, Visibility isVisibile)
        {
            if (chart.IsFinancialChart() && isVisibile == Visibility.Visible)
            {
                this.FinancialLegend.Visibility = Visibility.Visible;
                this.Legend.Visibility = Visibility.Collapsed;
            }
            if (chart.IsCategoryChart() && isVisibile == Visibility.Visible)
            {
                this.Legend.Visibility = Visibility.Visible;
                this.FinancialLegend.Visibility = Visibility.Collapsed;
            }
            foreach (var series in chart.Series)
            {
                series.Visibility = isVisibile;
                if (series is StackedSeriesBase)
                {
                    foreach (var sfs in ((StackedSeriesBase)series).Series)
                    {
                        sfs.Visibility = isVisibile;
                    }
                }
            }
            chart.Visibility = isVisibile;

        }
        #region Event Handlers
        private void OnItemSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = 0;
            foreach (XamDataChart chart in _samplesViewModel.Samples)
            {
                var isVisibile = (i == this.ItemSelectionComboBox.SelectedIndex) ? Visibility.Visible : Visibility.Collapsed;
                ChangeChartsVisibility(chart, isVisibile);
                i++;
            }
            _samplesViewModel.ShowSample(this.ItemSelectionComboBox.SelectedIndex);
        }

        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _samplesViewModel.GetNextSampleIndex();
        }
        private void OnPrevItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.ItemSelectionComboBox.SelectedIndex = _samplesViewModel.GetPreviousSampleIndex();
        }

        #endregion

        private void TransitionDurationSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (TransitionDurationSlider != null && 
                _samplesViewModel != null && 
                _samplesViewModel.Samples.Count > 0)
            {
                foreach (XamDataChart chart in _samplesViewModel.Samples)
                {
                    chart.HighlightingTransitionDuration =
                        TimeSpan.FromMilliseconds((this.TransitionDurationSlider.Value)*1000);
                }
            }
        }
    }

}
