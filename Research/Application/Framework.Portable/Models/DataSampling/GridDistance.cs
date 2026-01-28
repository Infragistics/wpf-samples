using System;
using Infragistics.Framework;

namespace Infragistics.Framework 
{ 
    /// <summary>
    /// Represents a distance of grid point to data point 
    /// </summary>
    public class GridDistance
    {
        public GridDistance() : this(DistanceMeasure.CityBlock)
        { 
        }
        public GridDistance(DistanceMeasure measure)
        {
            Measure = measure;
        }
        /// <summary> Gets or sets GridPoint </summary>
        public Point3D GridPoint { get; set; }

        /// <summary> Gets or sets DataPoint </summary>
        public Point3D DataPoint { get; set; }

        /// <summary> Gets or sets Distance </summary>
        //public double Length { get; set; }
        public double Length { get { return Calculate(); } }

        public DistanceMeasure Measure { get; set; }

        protected double Calculate()
        {
            var value = double.NaN;
            if (GridPoint == null || DataPoint == null)
            {
                return value;
            }

            var x = Math.Abs(GridPoint.X - DataPoint.X);
            var y = Math.Abs(GridPoint.Y - DataPoint.Y);

            if (Measure == DistanceMeasure.CityBlock)
            {
                value = x + y;
            }
            else if (Measure == DistanceMeasure.Pythagorean)
            {
                //square root is expensive so this measure is slower than CityBlock
                value = Math.Sqrt((x * x) + (y * y));
            }
            return value;
        }

        public override string ToString()
        {
            return Length.ToString("N4") + " distance to data " + DataPoint;
        }
    } 

}