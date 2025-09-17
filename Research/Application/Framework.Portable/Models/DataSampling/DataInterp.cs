using System.Collections.Generic;

namespace Infragistics.Framework 
{
    /// <summary>
    /// Determines type of interpolation used for interpolating grid points
    /// </summary>
    public enum DataInterp
    {
        /// <summary>
        /// Specifies fastest interpolation that calculates average of all neighboring data points
        /// or defaults to zero if no neighboring points found in grid cell
        /// </summary>
        AverageLocalNeighbors,
        /// <summary>
        /// Specifies slower interpolation that uses Maximum of Closest Neighboring data point in grid cell   
        /// </summary>
        MaxClosestNeighbors,
        /// <summary>
        /// Specifies slowest interpolation that calculates Inverse Distance Weight (IDW) 
        /// of neighboring data points in grid cell  
        /// </summary>
        InverseDistanceWeight,
    }

    public class DataInterpList : List<DataInterp>
    {
        public DataInterpList()
        {
            this.Add(DataInterp.AverageLocalNeighbors);
            this.Add(DataInterp.MaxClosestNeighbors);
            this.Add(DataInterp.InverseDistanceWeight);
        }
    }
}