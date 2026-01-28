using System.Windows;
using System.Windows.Controls;
using IGDataChart.Resources;
using Infragistics.Samples.Shared.Models;
using Infragistics.Controls.Charts;

namespace IGDataChart.Samples.Display.Series
{
    public partial class GalleryRadialSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GalleryRadialSeries()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private GallerySampleViewModel _vm;

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
