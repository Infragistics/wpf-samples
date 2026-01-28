using System;
using System.Collections.Generic; 
using System.Linq; 

namespace Infragistics.Framework  
{ 
    /// <summary>
    /// Represents base class for data interpolators
    /// </summary>
    public abstract class DataInterpolatorBase 
    {
        /// <summary> Gets or sets Grid </summary>
        public SurfaceGrid Grid { get; private set; }
         
        protected DataInterpolatorBase(SurfaceGrid grid)
        {
            this.Grid = grid;  
        }
          
        public virtual void Update(IList<GridPoint> gridPoints)
        {
            foreach (var point in gridPoints)
            {
                Update(point);
            }
        }
        public abstract void Update(GridPoint gridPoint);
    }
    /// <summary>
    /// Represents data interpolators that support a few interpolations, e.g. <see cref="DataInterp"/>
    /// </summary>
    public class DataInterpolator : DataInterpolatorBase
    {
         
        public DataInterp Interpolation { get { return Grid.DataInterpolation; } }

        public DataInterpolator(SurfaceGrid grid)
            : base(grid)
        { 
        }
        
        public override void Update(GridPoint point)
        {
            var neighbors = Grid.GetNeighbors(point);

            var z = double.MinValue;

            var info = "";

            #region MaxClosestNeighbors
            if (this.Interpolation == DataInterp.MaxClosestNeighbors)
            {
                var distances = new List<GridDistance>();
                var distanceMin = double.MaxValue;
                // find closest neighbor  
                foreach (var neighbor in neighbors)
                {
                    var distance = Grid.GetDistance(point, neighbor);
                    distances.Add(distance);
                    distanceMin = Math.Min(distanceMin, distance.Length);
                    info += distance.ToString();
                }

                var closests = distances.Where(
                    d => Grid.IsEqual(d.Length, distanceMin)).ToList();

                foreach (var distance in closests)
                {
                    z = Math.Max(z, distance.DataPoint.Z);
                }
            }
            #endregion
            #region InverseDistanceWeight
            else if (this.Interpolation == DataInterp.InverseDistanceWeight)
            {
                var sum = 0.0;
                // find closest neighbor  
                foreach (var neighbor in neighbors)
                {
                    var weight = Grid.GetWeight(point, neighbor);
                    sum += weight.Value * neighbor.Z;
                    info += weight.ToString();
                }
                z = sum / neighbors.Count;
            } 
            #endregion
            #region AverageLocalNeighbors
            else if (this.Interpolation == DataInterp.AverageLocalNeighbors)
            {
                if (neighbors.Count == 0)
                {
                    //TODO perhaps we should use absolute min Z value as default
                    z = 0.0;
                }
                else
                {
                    var sum = 0.0;
                    foreach (var neighbor in neighbors)
                    {
                        sum += neighbor.Z;
                        info += neighbor.ToString();
                    }
                    z = sum / neighbors.Count;
                } 
            } 
            #endregion
               
            point.Info = info;
            point.Z = z;
        }
    }
      

}