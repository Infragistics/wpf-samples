namespace Infragistics.Framework 
{
    /// <summary>
    /// Represents a weight of grid point based on its distance to data point
    /// </summary>
    public class GridWeight
    {
        public GridDistance Distance { get; set; }

        public double Value { get; set; }

        public GridWeight(double value, GridDistance distance)
        {
            Distance = distance;
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString("N4") + " weight, " + Distance;
        }
    }

}