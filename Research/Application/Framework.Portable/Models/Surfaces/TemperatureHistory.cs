using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Infragistics.Framework
{ 
    public class TemperatureHistory : List<Temperature>
    {
        public TemperatureHistory(int max = 40, int min = 10, int variance = 5)
        {
            var date = new DateTime(DateTime.Now.Year, 1, 1);
            var random = new Random(); 

            for (int i = 1; i <= 365; i++)
            {
                var rad = ((date.Month * 25) * Math.PI / 180) - Math.PI; 
                var multipler = (2 + Math.Cos(rad)) / 3; 
                var temp = multipler * max; 
                temp = Math.Max(min, temp);

                var dist = (date.Month * 20) * Math.PI / 180;
                var value = temp + (Math.Sin(dist) * variance * 2);

                var item = new Temperature
                {
                    Date = date,
                    Label = date.ToString("MMM dd"),
                    Value = temp,
                    Min = value - random.Next(variance, variance*2),
                    Max = value + random.Next(variance, variance*2), 
                };
                this.Add(item);
                date = date.AddDays(1); 
            } 
        }
    }

    [DebuggerDisplay("{ToString()}")]
    public class Temperature : ObservableModel
    {  
        public double Value { get; set; }

        /// <summary> Gets or sets Max </summary>
        public double Max { get; set; }

        /// <summary> Gets or sets Min </summary>
        public double Min { get; set; }

        public string Label { get; set; }

        //public string Label { get { return this.Date.ToString("MMM dd"); } }

        public int Day { get { return this.Date.Day; } }
        public int Month { get { return this.Date.Month; } }
        public int Year { get { return this.Date.Year; } }

        public DateTime Date { get; set; }

        public override string ToString()
        {
            return Label + ", " +
                Min.ToString("00") + " -" +
                Max.ToString("00")  ;
        }
    }
}