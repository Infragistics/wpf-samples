using Infragistics.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Infragistics.Framework
{
    public class ObservableVector3D : ObservableModel
    {
        public ObservableVector3D()
        {

        }

        public ObservableVector3D(Vector3D defaltVector)
        {
            X = defaltVector.X;
            Y = defaltVector.Y;
            Z = defaltVector.Z;
        }

        private double _x;
        /// <summary> Gets or sets X </summary>
        public double X
        {
            get { return _x; }
            set { if (_x == value) return; _x = value; OnPropertyChanged("X"); OnPropertyChanged("Vector"); }
        }

        private double _y;
        /// <summary> Gets or sets Y </summary>
        public double Y
        {
            get { return _y; }
            set { if (_y == value) return; _y = value; OnPropertyChanged("Y"); OnPropertyChanged("Vector"); }
        }

        private double _z;
        /// <summary> Gets or sets Z </summary>
        public double Z
        {
            get { return _z; }
            set { if (_z == value) return; _z = value; OnPropertyChanged("Z"); OnPropertyChanged("Vector"); }
        }
        public Vector3D Vector
        {
            get
            {
                return new Vector3D(X, Y, Z);
            }
        }

    }


}
