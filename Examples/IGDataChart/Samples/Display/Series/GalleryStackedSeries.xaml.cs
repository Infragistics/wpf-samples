using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;                    // DataChartStrings
using Infragistics;                             // BrushCollection
using Infragistics.Controls.Charts;             // XamDataChart 
using Infragistics.Samples.Shared.Models;       // GallerySampleViewModel
using Infragistics.Samples.Shared.Resources;    // CommonStrings

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryStackedSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryStackedSeries()
        {
            InitializeComponent();
            InitializeSample();
        }

        private GallerySampleViewModel _vm;

        private void InitializeSample()
        {
            _vm = new GallerySampleViewModel();
            
            _vm.AddSample(this.StackedColumnChart, DataChartStrings.XWDC_CategorySeries_StackedColumnSeries );
            _vm.AddSample(this.Stacked100ColumnChart, DataChartStrings.XWDC_CategorySeries_Stacked100ColumnSeries );

            _vm.AddSample(this.StackedAreaChart, DataChartStrings.XWDC_CategorySeries_StackedAreaSeries );
            _vm.AddSample(this.Stacked100AreaChart, DataChartStrings.XWDC_CategorySeries_Stacked100AreaSeries );
            
            _vm.AddSample(this.StackedLineChart, DataChartStrings.XWDC_CategorySeries_StackedLineSeries );
            _vm.AddSample(this.Stacked100LineChart, DataChartStrings.XWDC_CategorySeries_Stacked100LineSeries );

            _vm.AddSample(this.StackedSplineChart, DataChartStrings.XWDC_CategorySeries_StackedSplineSeries );
            _vm.AddSample(this.Stacked100SplineChart, DataChartStrings.XWDC_CategorySeries_Stacked100SplineSeries );

            _vm.AddSample(this.StackedSplineAreaChart, DataChartStrings.XWDC_CategorySeries_StackedSplineAreaSeries );
            _vm.AddSample(this.Stacked100SplineAreaChart, DataChartStrings.XWDC_CategorySeries_Stacked100SplineAreaSeries );

            _vm.AddSample(this.StackedBarChart, DataChartStrings.XWDC_CategorySeries_StackedBarSeries);
            _vm.AddSample(this.Stacked100BarChart, DataChartStrings.XWDC_CategorySeries_Stacked100BarSeries);
            
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
                if (series is Stacked100BarSeries ||
                    series is StackedBarSeries ||
                    series is Stacked100ColumnSeries ||
                    series is StackedColumnSeries)
                {
                    foreach (StackedFragmentSeries sfs in ((StackedSeriesBase)series).Series)
                    {
                        sfs.Visibility = isVisibile;
                    }
                }
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
