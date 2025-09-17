using System.Collections.Generic;

namespace Infragistics.Framework 
{ 
    /// <summary>
    /// Represents gird cell's location/index
    /// </summary>
    public class GridLocation
    {
        public GridLocation()
        {
            Split = new List<GridLocation>();
        }

        public GridLocation(int row, int col)
        {
            Split = new List<GridLocation>();
            this.Row = row;
            this.Col = col;
        }
        public double X { get; internal set; }
        public double Y { get; internal set; }

        /// <summary> Gets or sets Row index corresponding to data 
        /// point's Y value snapped to grid cells </summary>
        public int Row { get; internal set; }

        /// <summary> Gets or sets Col index corresponding to data 
        /// point's X value snapped to grid cells </summary>
        public int Col { get; internal set; }

        public override string ToString()
        {
            return string.Format("C {0:##0}, R {1:##0}", Col, Row);
        }

        public List<GridLocation> Split { get; protected set; }
        /// <summary> Gets gird location one cell left from this location </summary> 
        public GridLocation Left()
        {
            return new GridLocation(this.Row, this.Col - 1);
        }
        /// <summary> Gets gird location one cell right from this location </summary> 
        public GridLocation Right()
        {
            return new GridLocation(this.Row, this.Col + 1);
        }
        /// <summary> Gets gird location one cell above from this location </summary> 
        public GridLocation Above()
        {
            return new GridLocation(this.Row + 1, this.Col);
        }
        /// <summary> Gets gird location one cell below from this location </summary> 
        public GridLocation Below()
        {
            return new GridLocation(this.Row - 1, this.Col);
        }

        #region Compare Locations
        public override int GetHashCode()
        {
            return Col.GetHashCode() ^ Row.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var cell = obj as GridLocation;
            if (cell == null)
            {
                return false;
            }
            return (this.Row == cell.Row && this.Col == cell.Col);
        }

        public bool Equals(GridLocation cell)
        {
            if (cell == null)
            {
                return false;
            }
            return (this.Row == cell.Row && this.Col == cell.Col);
        }
        #endregion
    }

}