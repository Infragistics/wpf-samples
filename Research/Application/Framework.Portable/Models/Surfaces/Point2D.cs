using System.Windows;

namespace Infragistics.Framework
{ 
    public class Point2D : ObservableModel
    {
        public Point2D() : this(double.NaN, double.NaN)
        { }

        public Point2D(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        //public Point2D(Point location)
        //{
        //    this.X = location.X;
        //    this.Y = location.Y;
        //}
        public Point2D(Point2D point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        #region Properties
        private double _x;
        /// <summary> Gets or sets X coordinate </summary>
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); }
        }

        private double _y;
        /// <summary> Gets or sets Y coordinate </summary>
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); }
        }
        //TODO remove
        /// <summary> Gets X,Y coordinates of the point </summary>
        public Point2D Coordinates { get { return new Point2D(X, Y); } }

        private string _Label = string.Empty;
        /// <summary> Gets or sets Label </summary>
        public string Label
        {
            get
            {
                return _Label == null ? ToString() : _Label;
            }
            set
            {
                if (_Label == value) return; _Label = value; OnPropertyChanged("Label");
            }
        }

        #endregion

        public override string ToString()
        {
            return string.Format("X {0:##0.000}, Y {1:##0.000}\n", X, Y);
        }

    }

}
