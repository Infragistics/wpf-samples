namespace Infragistics.Framework 
{
    /// <summary>
    /// Determines type of measure used to calculate distance between points in the grid
    /// </summary>
    public enum DistanceMeasure
    {
        /// <summary>
        /// Represents fastest measure of distance calculated using sum of delta between X,Y coordinates.
        /// Same as measuring distance while walking around city blocks 
        /// </summary>
        CityBlock,
        /// <summary>
        /// Represents slowest but most accurate measure of distance using Pythagorean term.
        /// Same as measuring distance while flying over city blocks 
        /// </summary>
        Pythagorean,
    }
}