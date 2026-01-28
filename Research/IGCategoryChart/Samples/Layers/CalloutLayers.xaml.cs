using IGCategoryChart.Resources; 
using System;
using System.Collections.Generic; 

namespace IGCategoryChart.Samples
{ 
    public partial class CalloutLayers : Infragistics.Samples.Framework.SampleContainer
    {
        public CalloutLayers()
        {
            InitializeComponent();
        }
    }
    
    public class WeatherMeasure
    {
        // data properties
        public DateTime Date { get; set; }
        public double High { get; set; }
        public double Low { get; set; } 
        public string Month { get { return Date.ToString("MMM"); } }
        
        // annotation properties
        public double Index { get; set; } 
        public double Value { get; set; } 
        public string Info { get; set; } 
    }

    public class WeatherData : List<WeatherMeasure>
    { 
        public WeatherData()
        {
            var strings = CategoryChartStrings.ResourceManager;
            var date = new DateTime(DateTime.Now.Year, 1, 1);

            this.Add(new WeatherMeasure { High = 74, Low = 65, Date = date, });
            this.Add(new WeatherMeasure { High = 74, Low = 71, Date = date.AddMonths(1)  });
            this.Add(new WeatherMeasure { High = 76, Low = 73, Date = date.AddMonths(2)  });
            this.Add(new WeatherMeasure { High = 78, Low = 74, Date = date.AddMonths(3)  });
            this.Add(new WeatherMeasure { High = 83, Low = 76, Date = date.AddMonths(4)  });
            this.Add(new WeatherMeasure { High = 87, Low = 82, Date = date.AddMonths(5)  });
            this.Add(new WeatherMeasure { High = 94, Low = 87, Date = date.AddMonths(6)  });
            this.Add(new WeatherMeasure { High = 97, Low = 92, Date = date.AddMonths(7)  });
            this.Add(new WeatherMeasure { High = 93, Low = 88, Date = date.AddMonths(8)  });
            this.Add(new WeatherMeasure { High = 86, Low = 83, Date = date.AddMonths(9)  });
            this.Add(new WeatherMeasure { High = 81, Low = 78, Date = date.AddMonths(10) });
            this.Add(new WeatherMeasure { High = 79, Low = 71, Date = date.AddMonths(11) });

            var min = double.MaxValue;
            var max = double.MinValue;
            WeatherMeasure minItem = null;
            WeatherMeasure maxItem = null;

            // setting annotation properties: Index and Info
            for (int i = 0; i < this.Count; i++)
            {
                var item = this[i];
                item.Index = i;
                if (min > item.Low)
                {
                    min = item.Low;
                    minItem = item;
                    minItem.Index = i + 0.5;
                }
                if (max < item.High)
                {
                    max = item.High;
                    maxItem = item;
                } 
                                
                item.Value = item.High;

                if (item.Date.Month == 1 || item.Date.Month == 2 || item.Date.Month == 12)
                    item.Info = "WINTER";
                else if (item.Date.Month == 3 || item.Date.Month == 4 || item.Date.Month == 5)
                    item.Info = "SPRING";
                else if (item.Date.Month == 6 || item.Date.Month == 7 || item.Date.Month == 8)
                    item.Info = "SUMMER";
                else if (item.Date.Month == 9 || item.Date.Month == 10 || item.Date.Month == 11)
                    item.Info = "FALL";
                                
            }
           
            minItem.Info = "MIN";
            maxItem.Info = "MAX";
            minItem.Value = minItem.Low;
            maxItem.Value = maxItem.High;
        }
    } 
}
