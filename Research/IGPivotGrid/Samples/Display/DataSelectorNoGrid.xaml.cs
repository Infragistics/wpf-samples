using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Grids;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;

namespace IGPivotGrid.Samples.Display
{
    public partial class DataSelectorNoGrid : SampleContainer
    {
        private const int MAX_NUM_SERIES = 15;

        public DataSelectorNoGrid()
        {
            InitializeComponent();
            //InitializeColors(1);

            this.dataSelector.DataSource.ResultChanged += (sender2, e2) =>
            {
                UpdateData();
            };

            this.dataSelector.DataSource.PropertyChanged += OnDataSourcePropertyChanged;
        }
        private void UpdateData()
        {
            if (this.dataSelector.DataSource.Result != null)
            {
                IResult result = this.dataSelector.DataSource.Result;

                var rowDistinctLeafTuplesIndexesTemp = result.RowAxis.LeafTuples().Distinct().ToList();
                var rowDistinctLeafTuplesIndexes =
                    rowDistinctLeafTuplesIndexesTemp.GetRange(
                        0,
                        System.Math.Min(MAX_NUM_SERIES,
                                        rowDistinctLeafTuplesIndexesTemp.Count)
                        );

                var columnDistinctLeafTuplesIndexesTemp = result.ColumnAxis.LeafTuples().Distinct().ToList();
                var columnDistinctLeafTuplesIndexes =
                    columnDistinctLeafTuplesIndexesTemp.GetRange(
                        0,
                        System.Math.Min(MAX_NUM_SERIES,
                                        columnDistinctLeafTuplesIndexesTemp.Count)
                        );

                List<ITuple> distinctRowLeafTuples = new List<ITuple>();
                List<ITuple> distinctColumnLeafTuples = new List<ITuple>();

                ICell[,] cells = this.dataSelector.DataSource.Result.Cells;

                foreach (int i in columnDistinctLeafTuplesIndexes)
                {
                    distinctColumnLeafTuples.Add(this.dataSelector.DataSource.Result.ColumnAxis.Tuples[i]);
                }

                List<CategoryDataCollection> chartData = new List<CategoryDataCollection>();

                foreach (int i in rowDistinctLeafTuplesIndexes)
                {
                    CategoryDataCollection sdcol = new CategoryDataCollection();
                    foreach (int j in columnDistinctLeafTuplesIndexes)
                    {
                        if (cells[i, j] == null || j >= distinctColumnLeafTuples.Count() || cells[i, j].Value.ToString() == string.Empty)
                        {
                            sdcol.Add(new CategoryDataPoint { Value = 0.0, Label = "" });
                        }
                        else
                        {
                            string label = distinctColumnLeafTuples[j].Members.First().ToString();
                            double value = Double.Parse(cells[i, j].Value.ToString());
                            sdcol.Add(new CategoryDataPoint { Value = value, Label = label });
                        }
                    }
                    sdcol.Title =
                        this.dataSelector.DataSource.Result.RowAxis.Tuples[i].Members.First().ToString();
                    chartData.Add(sdcol);
                    distinctRowLeafTuples.Add(this.dataSelector.DataSource.Result.RowAxis.Tuples[i]);
                }

                //InitializeColors(chartData.Count);
                InitializeChartData(xmDataChart, chartData, xmLegend);

                this.RowsList.ItemsSource = distinctRowLeafTuples;
                this.ColumnsList.ItemsSource = distinctColumnLeafTuples;
            }
        }

        private void InitializeChartData(XamDataChart chart, List<CategoryDataCollection> data, Legend legend)
        {
            chart.DataContext = data;
            chart.Series.Clear();
            CategoryXAxis xmXAxis = new CategoryXAxis();
            NumericYAxis xmYAxis = new NumericYAxis();

            for (int i = 0; i < data.Count(); i++)
            {
                CategoryDataCollection sdcol = data[i];
                ColumnSeries xmSeries = new ColumnSeries();
                if (sdcol == data.First())
                {
                    // define common XAxis
                    xmXAxis.ItemsSource = sdcol;
                    xmXAxis.Label = "{Label}";
                    xmXAxis.LabelSettings = new AxisLabelSettings();
                    xmXAxis.LabelSettings.Extent = 35;
                    xmXAxis.LabelSettings.Location = AxisLabelsLocation.OutsideBottom;
                    xmSeries.XAxis = xmXAxis;

                    // define common YAxis
                    xmYAxis.LabelSettings = new AxisLabelSettings();
                    xmYAxis.LabelSettings.Extent = 55;
                    xmYAxis.LabelSettings.Location = AxisLabelsLocation.OutsideLeft;

                    // add axes to xmDataChart
                    chart.Axes.Clear();
                    chart.Axes.Add(xmXAxis);
                    chart.Axes.Add(xmYAxis);
                }

                // add a Series to xmDataChart
                xmSeries.ValueMemberPath = "Value";
                xmSeries.XAxis = xmXAxis;
                xmSeries.YAxis = xmYAxis;
                xmSeries.ItemsSource = sdcol;
                //xmSeries.Brush = new SolidColorBrush(_colors[i % _colors.Count()]);
                xmSeries.Title = sdcol.Title;
                xmSeries.Legend = legend;

                chart.Series.Add(xmSeries);
            }
        }

        //private List<Color> _colors;
        //private void InitializeColors(int numColors)
        //{
        //    _colors = new List<Color>();
        //    byte R0 = 0x3B;
        //    byte G0 = 0xB7;
        //    byte B0 = 0xEB;
        //    for (int i = 0; i < numColors; i++)
        //    {
        //        _colors.Add(Color.FromArgb(0xFF,
        //                                  (byte)(R0 + i * (26 / numColors)),
        //                                  (byte)(G0 - i * (87 / numColors)),
        //                                  (byte)(B0 - i * (121 / numColors))));
        //    }
        //}

        void OnDataSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
                isBusyIndicator.Visibility = dataSelector.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
