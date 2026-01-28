using System.Collections.Generic;

namespace IGDataChart.Samples.ChartData.ChartSeries
{
    /// <summary>
    /// Represents a contour data model of ContourDataPoint objects with samples values 
    /// </summary>
    public class ContourDataSample : ContourData
    {
        public ContourDataSample()
        {
            this.Add(new ContourDataPoint { X = 30, Y = 60, Value = 40 });
            this.Add(new ContourDataPoint { X = 40, Y = 50, Value = 40 });
            this.Add(new ContourDataPoint { X = 60, Y = 60, Value = 40 });
            this.Add(new ContourDataPoint { X = 40, Y = 70, Value = 40 });
            this.Add(new ContourDataPoint { X = 30, Y = 60, Value = 40 });

            this.Add(new ContourDataPoint { X = 20, Y = 60, Value = 30 });
            this.Add(new ContourDataPoint { X = 40, Y = 30, Value = 30 });
            this.Add(new ContourDataPoint { X = 80, Y = 60, Value = 30 });
            this.Add(new ContourDataPoint { X = 40, Y = 90, Value = 30 });
            this.Add(new ContourDataPoint { X = 20, Y = 60, Value = 30 });

            this.Add(new ContourDataPoint { X = 10, Y = 60, Value = 20 });
            this.Add(new ContourDataPoint { X = 40, Y = 10, Value = 20 });
            this.Add(new ContourDataPoint { X = 80, Y = 30, Value = 20 });
            this.Add(new ContourDataPoint { X = 120, Y = 60, Value = 20 });
            this.Add(new ContourDataPoint { X = 80, Y = 90, Value = 20 });
            this.Add(new ContourDataPoint { X = 40, Y = 110, Value = 20 });
            this.Add(new ContourDataPoint { X = 10, Y = 60, Value = 20 });
        }
    }
    /// <summary>
    /// Represents a contour data model of ContourDataPoint objects
    /// </summary>
    public class ContourData : List<ContourDataPoint>
    { }
    /// <summary>
    /// Represents a scatter data point for representing single point of a contour
    /// </summary>
    public class ContourDataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; set; }
    }
}