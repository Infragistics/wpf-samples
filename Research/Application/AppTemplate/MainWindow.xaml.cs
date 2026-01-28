using System;
using System.Collections.Generic; 
using System.Linq; 
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Controls.Charts; 

namespace Infragistics.Framework
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            // note that legend and chart are created in XAML code
     
            var dataSources = new AnnualBirthRateRegionalData();

            //var xAxis = new CategoryXAxis();
            //xAxis.Label = "{Col}";
            xAxis.ItemsSource = dataSources;
            stackedSeries.ItemsSource = dataSources;

            //// overriding Clustering Mode to match Excel screenshot
            //// this setting adds extra "margin" on left side of 1st item and
            //// right side of the last item
            //// however, we use this setting only when ColumnSeries are plotted
            //// because it wastes a lot of space in plotting area which could be used
            //// to show more details/data in senarios with a lot of data points
            //xAxis.UseClusteringMode = true;

            //// hiding tick marks althoguht they are useful to align labels with
            //// data items of line series
            //xAxis.TickLength = 0;

            //var yAxis = new NumericYAxis(); 
            //// overriding auto-calulcated axis range to match Excel screenshot
            //// this is a bit dangerous because you might clip some data points
            //// that are outside of this axis range.
            //yAxis.MinimumValue = 0; 
            //yAxis.MaximumValue = 2000; 
            //yAxis.Interval = 1000; 

            //this.Chart.Axes.Add(xAxis);
            //this.Chart.Axes.Add(yAxis);
            //this.Chart.Title = "Line";
            //this.Chart.Legend = this.Legend;

            //this.Legend.Orientation = Orientation.Horizontal;
            //this.Legend.HorizontalAlignment = HorizontalAlignment.Center;
            //this.Legend.ItemsFontSize = 15;

            //// creating a new line series for each data source
            //foreach (var data in dataSources)
            //{
            //    var series = new LineSeries();
            //    series.Title = data[0].Row;
            //     // hiding markers to match Excel screenshot
            //     // but I remcommand showing markers for data sources with less than 500-1000 items
            //    series.MarkerType = MarkerType.None;
            //    series.Thickness = 3;
            //    series.XAxis = xAxis;
            //    series.YAxis = yAxis;
            //    series.ValueMemberPath = "Cell";
            //    series.ItemsSource = data;
            //    this.Chart.Series.Add(series);
            //}

        }

        private void stackedSeries_AssigningCategoryStyle(object sender, AssigningCategoryStyleEventArgs args)
        {
            var s = args.Stroke = new SolidColorBrush { Color = Colors.Red };
        }

        //// groupping data items into multiple data sources (each source for 1 series)
        //public List<List<DataItem>> GroupDataItems()
        //{ 
        //    var dataSources = new Dictionary<string, List<DataItem>>();

        //    foreach (var item in GetDataItems())
        //    {
        //        // using row names as an identifier of data sources
        //        // but we could alternativly use column names as an identifier
        //        var id = item.Row;
        //        if (dataSources.ContainsKey(id))
        //        {
        //            dataSources[id].Add(item);
        //        }
        //        else
        //        {
        //            dataSources[id] = new List<DataItem>();
        //            dataSources[id].Add(item);
        //        }
        //    }


        //    return dataSources.Values.ToList();
        //}

        //// creating a simple list of data items 
        //public List<DataItem> GetDataItems()
        //{
        //    var dataItems = new List<DataItem>();

        //    dataItems.Add(new DataItem { Row = "North", Col = "Jan", Cell = 350 });
        //    dataItems.Add(new DataItem { Row = "North", Col = "Feb", Cell = 370 });
        //    dataItems.Add(new DataItem { Row = "North", Col = "Mar", Cell = 400 });

        //    dataItems.Add(new DataItem { Row = "South", Col = "Jan", Cell = 700 });
        //    dataItems.Add(new DataItem { Row = "South", Col = "Feb", Cell = 650 });
        //    dataItems.Add(new DataItem { Row = "South", Col = "Mar", Cell = 500 });

        //    dataItems.Add(new DataItem { Row = "East", Col = "Jan", Cell = 1250 });
        //    dataItems.Add(new DataItem { Row = "East", Col = "Feb", Cell = 1400 });
        //    dataItems.Add(new DataItem { Row = "East", Col = "Mar", Cell = 900 });

        //    dataItems.Add(new DataItem { Row = "West", Col = "Jan", Cell = 75 });
        //    dataItems.Add(new DataItem { Row = "West", Col = "Feb", Cell = 80 });
        //    dataItems.Add(new DataItem { Row = "West", Col = "Mar", Cell = 100 });

        //    return dataItems;
        //}

    }


    public class AnnualBirthRateRegionalInfo
    {
        public string Year { get; set; }
        public double Asia { get; set; }
        public double Africa { get; set; }
        public double Europe { get; set; }
        public double NorthAmerica { get; set; }
        public double SouthAmerica { get; set; }
        public double Oceania { get; set; }
    }

    public class AnnualBirthRateRegionalData : List<AnnualBirthRateRegionalInfo>
    {
        public AnnualBirthRateRegionalData()
        {
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "1950", Asia = 62, Africa = 13, Europe = 10, NorthAmerica = 4, SouthAmerica = 8, Oceania = 2 });
            this.Add(null);
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "1960", Asia = 68, Africa = 12, Europe = 15, NorthAmerica = 4, SouthAmerica = 9, Oceania = 2 });
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "1970", Asia = 80, Africa = 17, Europe = 11, NorthAmerica = 3, SouthAmerica = 9, Oceania = 2 });
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "1980", Asia = 77, Africa = 21, Europe = 12, NorthAmerica = 3, SouthAmerica = 10, Oceania = 2 });
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "1990", Asia = 87, Africa = 24, Europe = 9, NorthAmerica = 3, SouthAmerica = 10, Oceania = 2 });
            
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "2000", Asia = double.NaN, Africa = 28, Europe = double.NaN, NorthAmerica = 4, SouthAmerica = 9, Oceania = 2 });
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "2010", Asia = 78, Africa = 35, Europe = 10, NorthAmerica = 4, SouthAmerica = 8, Oceania = 2 });
            this.Add(new AnnualBirthRateRegionalInfo() { Year = "2020", Asia = 75, Africa = 43, Europe = 7, NorthAmerica = 4, SouthAmerica = 7, Oceania = 2 });
        }
    }

}
