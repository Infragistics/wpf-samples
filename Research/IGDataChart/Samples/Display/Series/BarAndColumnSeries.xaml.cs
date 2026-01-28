using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Shared.ViewModels.DataChart;
using Infragistics.Samples.Shared.Models.DataChart;
using Infragistics.Samples.Shared.Resources;

namespace IGDataChart.Samples.Display.Series
{
    public partial class BarAndColumnSeries : Infragistics.Samples.Framework.SampleContainer
    {
        private BarColumnViewModel _vm;

        public BarAndColumnSeries()
        {
            InitializeComponent();
            InitializeSampleViewModel();
            InitializeSelectionComboBox();
        }

        private void OnSeriesTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshDataChartSeries();
        }

        private void OnPreviousItemButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.cbSeriesType.SelectedIndex == 0)
            {
                this.cbSeriesType.SelectedIndex = this.cbSeriesType.Items.Count - 1;
            }
            else
            {
                this.cbSeriesType.SelectedIndex -= 1;
            }
        }

        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.cbSeriesType.SelectedIndex = (this.cbSeriesType.SelectedIndex + 1) % this.cbSeriesType.Items.Count;
        }

        private void InitializeSampleViewModel()
        {
            _vm = new BarColumnViewModel();
            _vm.Samples.Add(new DataChartModel(this.barChart, DataChartStrings.XWDC_CategorySeries_BarSeries));
            _vm.Samples.Add(new DataChartModel(this.stackedBarChart, DataChartStrings.XWDC_CategorySeries_StackedBarSeries));
            _vm.Samples.Add(new DataChartModel(this.stacked100BarChart, DataChartStrings.XWDC_CategorySeries_Stacked100BarSeries));
            _vm.Samples.Add(new DataChartModel(this.columnChart, DataChartStrings.XWDC_CategorySeries_ColumnSeries));
            _vm.Samples.Add(new DataChartModel(this.stackedColumnChart, DataChartStrings.XWDC_CategorySeries_StackedColumnSeries));
            _vm.Samples.Add(new DataChartModel(this.stacked100ColumnChart, DataChartStrings.XWDC_CategorySeries_Stacked100ColumnSeries));

            foreach (DataChartModel sample in _vm.Samples)
            {
                (sample.Control as XamDataChart).Visibility = Visibility.Collapsed;
                foreach (var series in (sample.Control as XamDataChart).Series)
                {
                    series.Visibility = Visibility.Collapsed;
                    if (series is StackedBarSeries ||
                        series is Stacked100BarSeries ||
                        series is StackedColumnSeries ||
                        series is Stacked100ColumnSeries)
                    {
                        foreach (StackedFragmentSeries sfs in ((StackedSeriesBase)series).Series)
                        {
                            sfs.Visibility = Visibility.Collapsed;
                        }
                    }
                }
            }
        }

        private void InitializeSelectionComboBox()
        {
            foreach (DataChartModel sample in _vm.Samples)
            {
                this.cbSeriesType.Items.Add(new ComboBoxItem { Content = sample.Name });
            }
            this.cbSeriesType.SelectedIndex = 0;
        }

        private void RefreshDataChartSeries()
        {
            if (this.cbSeriesType.SelectedIndex < 0) return;

            int i = 0;
            foreach (DataChartModel sample in _vm.Samples)
            {
                if (i == this.cbSeriesType.SelectedIndex)
                {
                    (sample.Control as XamDataChart).Visibility = Visibility.Visible;
                    foreach (var series in (sample.Control as XamDataChart).Series)
                    {
                        series.Visibility = Visibility.Visible;
                        if (series is StackedBarSeries ||
                            series is Stacked100BarSeries ||
                            series is StackedColumnSeries ||
                            series is Stacked100ColumnSeries)
                        {
                            foreach (StackedFragmentSeries sfs in ((StackedSeriesBase)series).Series)
                            {
                                sfs.Visibility = Visibility.Visible;
                            }
                        }
                    }
                }
                else
                {
                    (sample.Control as XamDataChart).Visibility = Visibility.Collapsed;
                    foreach (var series in (sample.Control as XamDataChart).Series)
                    {
                        series.Visibility = Visibility.Collapsed;
                        if (series is StackedBarSeries ||
                            series is Stacked100BarSeries ||
                            series is StackedColumnSeries ||
                            series is Stacked100ColumnSeries)
                        {
                            foreach (StackedFragmentSeries sfs in ((StackedSeriesBase)series).Series)
                            {
                                sfs.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
                i++;
            }
        }
    }
}
