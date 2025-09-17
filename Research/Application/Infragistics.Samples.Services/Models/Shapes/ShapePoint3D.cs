using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System.Windows;

namespace Infragistics.Models 
{
    public class ShapePoint3D : ObservableModel 
    {
        public ShapePoint3D() : this(double.NaN, double.NaN)
        { }

        public ShapePoint3D(double x, double y, double z = double.NaN)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public ShapePoint3D(Point location, double z = double.NaN)
        {
            this.X = location.X;
            this.Y = location.Y;
            this.Z = z;
        }
        public ShapePoint3D(ShapePoint3D point)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.Z = point.Z;
        }
      
        private double _x;
        /// <summary> Gets or sets X </summary>
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); }
        }

        private double _y;
        /// <summary> Gets or sets Y </summary>
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); }
        }

        private double _z;
        /// <summary> Gets or sets Z </summary>
        public double Z
        {
            get { return _z; }
            set { if (_z == value) return; _z = value; OnPropertyChanged("Z"); }
        }

        public void UpdateZ(double z)
        {
            _z = z;
        }
        /// <summary> Gets X,Y coordinates of the point </summary>
        public Point Coordinates { get { return new Point(X, Y); } }

        public override string ToString()
        {
            return string.Format("X {0:##0.000}, Y {1:##0.000}, Z {2:##0.0} \n", X, Y, Z);
        }

        public string Label { get { return ToString(); } }
        
    }
     
}