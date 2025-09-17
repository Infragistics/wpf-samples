using System;
using System.Collections.Generic;

namespace Infragistics.Framework
{ 
    /// <summary>
    /// Represents a data point for representing single scatter point  
    /// </summary>
    public class ScatterPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; set; }

        public double Group { get; set; }

        public string Label { get; set; }

        /// <summary> Gets or sets Date </summary>
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return X + ", " + Y;
        }
    }
    /// <summary>
    /// Represents a list of Point objects
    /// </summary>
    public class DataList : List<ScatterPoint>
    { }
}