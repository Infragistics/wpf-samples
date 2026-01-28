using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Framework;

namespace IGDataChart.Samples.Display.Series
{
    public partial class OlapAxisStackedSeries : SampleContainer
    {
        HorizontalStackedSeriesBase series;
        string selectedSeriesType = "StackedAreaSeries";
        private int seriesIndex;

        // simple class for storing data from series' items sources
        private class StackedSeriesData
        {
            public StackedSeriesData()
            {
                Data = new List<double>();
            }

            public List<double> Data { get; set; }
        }

        public OlapAxisStackedSeries()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            selectedSeriesType = ((RadioButton)sender).Tag.ToString();
            // redraw the data chart
            ((FlatDataSource)Resources["OlapFlatDataSource"]).RefreshGrid();
        }

        private void OlapXAxis_SeriesCreating(object sender, SeriesCreatingEventArgs e)
        {
            List<StackedSeriesData> commonItemsSource;
            if (series.ItemsSource == null)
            {
                commonItemsSource = Enumerable.Range(0, e.SeriesInfo.ItemsSource.Count)
                    .Select(i => new StackedSeriesData()).ToList();
                series.ItemsSource = commonItemsSource;
            }
            else commonItemsSource = series.ItemsSource as List<StackedSeriesData>;

            for (int i = 0; i < e.SeriesInfo.ItemsSource.Count; i++)
            {
                if (null == e.SeriesInfo.ItemsSource[i].Cell ||
                    string.IsNullOrEmpty(e.SeriesInfo.ItemsSource[i].Cell.Value.ToString()))
                    commonItemsSource[i].Data.Add(0);
                else
                    commonItemsSource[i].Data.Add(Convert.ToDouble(e.SeriesInfo.ItemsSource[i].Cell.Value));
            }

            series.Series.Add(
                new StackedFragmentSeries() { ValueMemberPath = string.Format("Data.[{0}]", seriesIndex), Title = e.SeriesInfo.Title });
            e.Series = series;
            seriesIndex++;
        }

        private void OlapXAxis_SeriesInitializing(object sender, Infragistics.Controls.Charts.SeriesInitializingEventArgs e)
        {
            switch (selectedSeriesType)
            {
                case "StackedAreaSeries":
                    series = new StackedAreaSeries();
                    break;
                case "Stacked100AreaSeries":
                    series = new Stacked100AreaSeries();
                    break;
                case "StackedColumnSeries":
                    series = new StackedColumnSeries();
                    break;
                case "Stacked100ColumnSeries":
                    series = new Stacked100ColumnSeries();
                    break;
                case "StackedLineSeries":
                    series = new StackedLineSeries();
                    break;
                case "Stacked100LineSeries":
                    series = new Stacked100LineSeries();
                    break;
                case "StackedSplineAreaSeries":
                    series = new StackedSplineAreaSeries();
                    break;
                case "Stacked100SplineAreaSeries":
                    series = new Stacked100SplineAreaSeries();
                    break;
                case "StackedSplineSeries":
                    series = new StackedSplineSeries();
                    break;
                case "Stacked100SplineSeries":
                    series = new Stacked100SplineSeries();
                    break;
            }
            seriesIndex = 0;
        }
    }
}
