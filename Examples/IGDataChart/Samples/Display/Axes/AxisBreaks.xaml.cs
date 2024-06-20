using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System; 
using System.Windows; 

namespace IGDataChart.Samples.Display.Axes
{ 
    public partial class AxisBreaks : SampleContainer
    { 
        public AxisBreaks()
        {
            InitializeComponent();
            
            // setting custom label format to show changes in axis breaks
            XAxis.LabelFormats.Clear();
            XAxis.LabelFormats.Add(new TimeAxisLabelFormat
            {
                //Format = "ddd, MMM dd",
                Format = "MMM dd, ddd",
                Range =  new TimeSpan(0, 0, 0, 0, 0)
            });

            // setting custom label interval to show changes in axis breaks
            XAxis.Intervals.Clear();
            XAxis.Intervals.Add(new TimeAxisInterval
            {
                IntervalType = TimeAxisIntervalType.Days,
                Interval = 1,
                Range = new TimeSpan(0, 0, 0, 0, 0)
            }); 

            this.DataChart.DataContext = new AxisBreaksViewModel();                  
        }

        private void IncludeWeekends_Checked(object sender, RoutedEventArgs e)
        {
            if (XAxis == null)
                return;

            XAxis.Breaks.Clear(); 
        }
        private void ExcludeWeekends_Checked(object sender, RoutedEventArgs e)
        {
            if (XAxis == null)
                return;

            XAxis.Breaks.Clear();
            XAxis.Breaks.Add(allSaturdaysAxisBreak);
            XAxis.Breaks.Add(allSundaysAxisBreak); 
        }
    }  

    public class AxisBreaksViewModel
    {
        public AxisBreaksViewModel()
        {
            var settings = new StockMarketSettings
            {
                DataPoints = 22,

                PriceStart = 300,
                PriceRange = 20,
                PriceSample = 1,

                DateStart = new DateTime(2018, 1, 1),
                DateInterval = TimeSpan.FromDays(1),
            };

            DataSource = new StockMarketDataCollection();
            DataSource.Settings = settings;
        }

        public StockMarketDataCollection DataSource { get; set; } 

    }
}
