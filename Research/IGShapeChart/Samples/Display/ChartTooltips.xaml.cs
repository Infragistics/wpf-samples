using IGShapeChart.Resources;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace IGShapeChart.Samples
{ 
    public partial class ChartTooltips : SampleContainer
    {
        public ChartTooltips()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            Chart.Legend = this.Legend;
        }
         
        private void Chart_SeriesAdded(object sender, ChartSeriesEventArgs args)
        {
            var shapeSeries = args.Series as ScatterPolygonSeries;
            if (shapeSeries != null)
            {
                var data = shapeSeries.ItemsSource as ShapefileLoader;
                shapeSeries.Title = data.FilterValue.ToString().Replace("NorthAfrica","North Africa"); 
            }

            var pointSeries = args.Series as ScatterSeries;
            if (pointSeries != null)
            {  
                pointSeries.Title = ShapeChartStrings.XW_Cities;
            }
        }
    } 
}
