using System.Collections.Generic;
using System.Windows;
using Infragistics.Framework;

namespace Infragistics.Framework 
{
    public class GridPoint : Point3D
    {
        /// <summary> Gets or sets Location </summary>
        public GridLocation Location { get; private set; }

        /// <summary> Gets grid point which is in the center of grid cell </summary>
        //public GridPoint GridPoint { get; private set; }

        /// <summary> Gets a list of data points snapped to this grid cell </summary>
        public List<Point3D> Neighbors { get; private set; }

        public string Info { get; set; }
        
        ///// <summary> Gets bounding box of snapped data points </summary>
        //public Rect SnapBounds { get; private set; }


        public GridPoint(GridLocation location, double sizeX, double sizeY)
            : base(location.X, location.Y, double.NaN)
        {
            Neighbors = new List<Point3D>();

            Location = location;

            //var left = location.X - (sizeX / 2.0);
            //var top = location.Y - (sizeY / 2.0);

            //SnapBounds = new Rect(left, top, sizeX, sizeY);
        }

        public override string ToString()
        {
            return "Cell " + this.Location + " " + base.ToString();
        }
    }

    public class GridDictionary : Dictionary<GridLocation, GridPoint>
    {
        public bool Contains(GridLocation location)
        {
            return this.ContainsKey(location);
        }
        public bool Contains(int row, int col)
        {
            var location = new GridLocation();
            location.Row = row;
            location.Col = col;

            return this.Contains(location);
        }
    }

    
}