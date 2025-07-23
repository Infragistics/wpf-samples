using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel; 
using System.Windows;
using System.Windows.Threading;

namespace IGShapeChart.Samples
{ 
    public partial class BindingAirplaneBlueprint : SampleContainer
    {
        public ObservableCollection<ShapefileLoader> Data { get; set; }

        public BindingAirplaneBlueprint()
        {
            InitializeComponent();

            this.Loaded += OnSampleLoaded;

            ShapefileLoader loader1 = new ShapefileLoader() { OffsetX = -306, OffsetY = 0, FilePath = "/IGShapeChart;component/shapefiles/airplane-shape.shp" };
            ShapefileLoader loader2 = new ShapefileLoader() { OffsetX = -153, OffsetY = 0, FilePath = "/IGShapeChart;component/shapefiles/airplane-seats.shp", FilterValue = "First" };
            ShapefileLoader loader3 = new ShapefileLoader() { OffsetX = -153, OffsetY = 0, FilePath = "/IGShapeChart;component/shapefiles/airplane-seats.shp", FilterValue = "Business" };
            ShapefileLoader loader4 = new ShapefileLoader() { OffsetX = -153, OffsetY = 0, FilePath = "/IGShapeChart;component/shapefiles/airplane-seats.shp", FilterValue = "Travel+" };
            ShapefileLoader loader5 = new ShapefileLoader() { OffsetX = -153, OffsetY = 0, FilePath = "/IGShapeChart;component/shapefiles/airplane-seats.shp", FilterValue = "Travel" };
            ShapefileLoader loader6 = new ShapefileLoader() { OffsetX = -153, OffsetY = 0, FilePath = "/IGShapeChart;component/shapefiles/airplane-seats.shp", FilterValue = "Global" };

            Data = new ObservableCollection<ShapefileLoader>();
            Data.Add(loader1);
            Data.Add(loader2);
            Data.Add(loader3);
            Data.Add(loader4);
            Data.Add(loader5);
            Data.Add(loader6);

            this.DataContext = this;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            Chart.Legend = this.Legend;
        }
         
        private void Chart_SeriesAdded(object sender, ChartSeriesEventArgs args)
        {
            var series = args.Series as ScatterPolygonSeries;
            if (series != null)
            {
                var data = series.ItemsSource as ShapefileLoader;
                if (data.FilterValue == null)
                    series.Title = "Airplane";
                else
                    series.Title = data.FilterValue + " Class";
            }
        }

        private void Chart_Loaded(object sender, RoutedEventArgs e)
        {
            Chart.ItemsSource = this.Data;
        }
    }
}
