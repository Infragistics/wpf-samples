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
    public partial class GalleryCategorySelection : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryCategorySelection()
        {
            InitializeComponent();
            UseDefaultTheme = true;
            InitializeSample();
        }

        private GallerySampleViewModel _vm;
        private SeriesSelectionMode[] allSeriesSelectionModeValues = (SeriesSelectionMode[])Enum.GetValues(typeof(SeriesSelectionMode));
        private SeriesSelectionBehavior[] allSeriesSelectionBehaviorValues = (SeriesSelectionBehavior[])Enum.GetValues(typeof(SeriesSelectionBehavior));

        private void InitializeSample()
        {
            _vm = new GallerySampleViewModel();
            _vm.AddSample(this.ColumnChart, DataChartStrings.XWDC_CategorySeries_ColumnSeries);         
            _vm.AddSample(this.AreaChart, DataChartStrings.XWDC_CategorySeries_AreaSeries);           
            _vm.AddSample(this.LineChart, DataChartStrings.XWDC_CategorySeries_LineSeries);           
            _vm.AddSample(this.SplineChart, DataChartStrings.XWDC_CategorySeries_SplineSeries);           
            _vm.AddSample(this.SplineAreaChart, DataChartStrings.XWDC_CategorySeries_SplineAreaSeries);           
            _vm.AddSample(this.PointChart, DataChartStrings.XWDC_CategorySeries_PointSeries);
            _vm.AddSample(this.StepLineChart, DataChartStrings.XWDC_CategorySeries_StepLineSeries);
            _vm.AddSample(this.StepAreaChart, DataChartStrings.XWDC_CategorySeries_StepAreaSeries);
            _vm.AddSample(this.RangeColumnChart, DataChartStrings.XWDC_CategorySeries_RangeColumnSeries);
            _vm.AddSample(this.RangeAreaChart, DataChartStrings.XWDC_CategorySeries_RangeAreaSeries);
            _vm.AddSample(this.WaterfallChart, DataChartStrings.XWDC_CategorySeries_WaterfallSeries);
            _vm.AddSample(this.BarChart, DataChartStrings.XWDC_CategorySeries_BarSeries);
            
            InitializeCharts();

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

        private void ItemSelectionModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ColumnChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.AreaChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.LineChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.SplineChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.SplineAreaChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.PointChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.StepLineChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.StepAreaChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.RangeColumnChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.RangeAreaChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.WaterfallChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];
            this.BarChart.SelectionMode = allSeriesSelectionModeValues[ItemSelectionModeComboBox.SelectedIndex];

        }
        private void ItemSelectionBehaviorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.ColumnChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.AreaChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.LineChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.SplineChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.SplineAreaChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.PointChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.StepLineChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.StepAreaChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.RangeColumnChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.RangeAreaChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.WaterfallChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];
            this.BarChart.SelectionBehavior = allSeriesSelectionBehaviorValues[ItemSelectionBehaviorComboBox.SelectedIndex];

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
