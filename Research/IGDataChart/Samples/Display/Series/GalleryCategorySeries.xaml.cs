using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;                    // DataChartStrings
using Infragistics;                             // BrushCollection
using Infragistics.Controls.Charts;             // XamDataChart 
using Infragistics.Samples.Shared.Models;       // GallerySampleViewModel
using Infragistics.Samples.Shared.Resources;    // CommonStrings

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryCategorySeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryCategorySeries()
        {
            InitializeComponent();
            UseDefaultTheme = true;
            InitializeSample();
        }

        private GallerySampleViewModel _vm;

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
