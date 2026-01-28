using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel; 

namespace IGFinancialChart.Samples.Display
{ 
    public partial class ScaleBreaks : SampleContainer
    {
        public ScaleBreaks()
        {
            InitializeComponent();  

            VM = new ScaleBreaksViewModel();
            VM.PropertyChanged += VM_PropertyChanged;

            this.Chart1.ZoomSliderType = FinancialChartZoomSliderType.None;  
            this.Chart1.SeriesAdded += Chart_SeriesAdded;
            this.Chart1.ItemsSource = VM.DataSources;
            this.Chart1.YAxisInterval = 20;
                        
            this.Chart2.ZoomSliderType = FinancialChartZoomSliderType.None;  
            this.Chart2.SeriesAdded += Chart_SeriesAdded;
            this.Chart2.ItemsSource = VM.DataSources;
            this.Chart2.YAxisInterval = 20;

            this.DateSelector.ItemsSource = VM.DateSelections;
        }

        private ScaleBreaksViewModel VM;
        private void VM_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "AxisBreaks")
            {
                Chart1.XAxisBreaks.Clear();
                foreach (var item in VM.AxisBreaks)
                {                    
                    Chart1.XAxisBreaks.Add(item);
                }
            }
        }
          
        private void Chart_SeriesAdded(object sender, ChartSeriesEventArgs args)
        {
            var series = args.Series as FinancialPriceSeries;
            if (series == null || series.Name.Contains("zoom")) return;
                        
            var xAxis = series.XAxis as TimeXAxis;
            if (xAxis == null) return;
            
            // setting custom label format to show changes in axis breaks
            xAxis.LabelFormats.Clear();
            xAxis.LabelFormats.Add(new TimeAxisLabelFormat
            {
                //Format = "ddd, MMM dd",
                Format = "MMM dd, ddd",
                Range =  new TimeSpan(0, 0, 0, 0, 0)
            });

            // setting custom label interval to show changes in axis breaks
            xAxis.Intervals.Clear();
            xAxis.Intervals.Add(new TimeAxisInterval
            {
                IntervalType = TimeAxisIntervalType.Days,
                Interval = 1,
                Range = new TimeSpan(0, 0, 0, 0, 0)
            }); 
        } 
    }
     
    
    public class ScaleBreaksViewModel : ObservableModel
    {
        public ScaleBreaksViewModel()
        { 
            var includeWeekends = true;
            var interval = TimeSpan.FromDays(1);
            var start  = new DateTime(2018, 1, 1, 0, 0, 0);
            var end    = new DateTime(2018, 1, 23, 0, 0, 0);
            var stock1 = new StockList(start, end, "TSLA", interval, 300, 30000, includeWeekends); 

            DataSources = new List<StockList>();
            DataSources.Add(stock1); 

            DateSelections = new List<DayOfWeekSelection>();  
            var time = start;
            for (int i = 0; i < 7; i++)
            {
                var isSelected = time.DayOfWeek == DayOfWeek.Saturday || time.DayOfWeek == DayOfWeek.Sunday;
                var selection = new DayOfWeekSelection(time, isSelected);
                selection.PropertyChanged += OnDateSelectionsChanged;

                DateSelections.Add(selection);
                time = time.AddDays(1);
            }
        }


        public List<StockList> DataSources { get; set; }
        public List<DayOfWeekSelection> DateSelections { get; set; }
         
        private List<TimeAxisBreak> _AxisBreaks = new List<TimeAxisBreak>(); 
        public List<TimeAxisBreak> AxisBreaks
        {
            get { return _AxisBreaks; }
            set { if (_AxisBreaks == value) return; _AxisBreaks = value; OnPropertyChanged("AxisBreaks"); }
        } 
        
        private void OnDateSelectionsChanged(object sender, PropertyChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            var breaks = new List<TimeAxisBreak>();    
            var time = new DateTime(2018, 1, 1, 0, 0, 0);
            for (int i = 0; i < this.DateSelections.Count; i++)
            {
                var item = this.DateSelections[i];                  
                if (item.IsSelected && item.DayOfWeek == time.DayOfWeek)
                {
                    breaks.Add(CreateWeaklyBreak(time.Year, time.Month, time.Day));
                }

                time = time.AddDays(1);
            }
             
            this.AxisBreaks = breaks;
        }

        TimeAxisBreak CreateWeaklyBreak(int year, int month, int day)
        {  
            var axisBreak = new TimeAxisBreak();
            axisBreak.Start = new DateTime(year, month, day, 0, 0, 0);  
            axisBreak.End   = new DateTime(year, month, day, 23, 59, 59, 999);  
            axisBreak.Interval = new TimeSpan(7, 0, 0, 0);
            return axisBreak;
        } 
    }
     
    public class DayOfWeekSelection : ObservableModel
    {
        public DateTime DateTime { get; set; }
        public string DisplayName { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
         
        private bool _IsSelected; 
        public bool IsSelected
        {
            get { return _IsSelected; }
            set { if (_IsSelected == value) return; _IsSelected = value; OnPropertyChanged("IsSelected"); }
        }
         
        public DayOfWeekSelection(DateTime dt, bool isSelected)
        {
            IsSelected = isSelected;
            DisplayName = dt.ToString("dddd");
            DateTime = dt;
            DayOfWeek = dt.DayOfWeek;
        }
    } 
}
