using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Threading;

namespace Infragistics.Models
{
    public class Shape3D : ObservableCollector<ShapePoint3D>  
    {
        public DataColumn X { get; private set; }
        public DataColumn Y { get; private set; }
        public DataColumn Z { get; private set; }
        
        public Shape3D()
        {
            X = new DataColumn();
            Y = new DataColumn();
            Z = new DataColumn();
        }
        #region Methods
        public void AddRange(IList<ShapePoint3D> points)
        {
            foreach (var p in points)
            {
                this.Add(p);
            }
        }
        public Shape3D FlipZ()
        {
            for (var i = 0; i < this.Count; i++)
            {
                var item = this[i];
                item.Z *= -1;
                this[i] = item;
            }
            return this;
        }
        public Shape3D FlipY()
        {
            for (var i = 0; i < this.Count; i++)
            {
                var item = this[i];
                item.Y *= -1;
                this[i] = item;
            }
            return this;
        }
        public Shape3D FlipX()
        {
            for (var i = 0; i < this.Count; i++)
            {
                var item = this[i];
                item.X *= -1;
                this[i] = item;
            }
            return this;
        }
        public new void Add(ShapePoint3D point)
        {
            base.Add(point);
            if (point == null) return;

            if (!double.IsNaN(point.X) ||
                !double.IsInfinity(point.X))
            {
                X.Min = Math.Min(X.Min, point.X);
                X.Max = Math.Max(X.Max, point.X);
            }

            if (!double.IsNaN(point.Y) ||
                !double.IsInfinity(point.Y))
            {
                Y.Min = Math.Min(Y.Min, point.Y);
                Y.Max = Math.Max(Y.Max, point.Y);
            }

            if (!double.IsNaN(point.Z) ||
                !double.IsInfinity(point.Z))
            {
                Z.Min = Math.Min(Z.Min, point.Y);
                Z.Max = Math.Max(Z.Max, point.Y);
            }
        }

        #endregion
        
        public override void Update(ShapePoint3D p, double seed)
        {
            p.IsPropertyNotifyActive = false;
            var z = MathUtilExtended.Noise(0.02 * p.X, 0.02 * p.Y, seed);
            p.Z = z;
            //p.UpdateZ(z); 
        }
        public override string ToString()
        {
            return "Points " + this.Count + ", Z " + this.Z;
        }
    }

    public class DataColumn
    {
        public DataColumn()
        {
            Min = double.MaxValue;
            Max = double.MinValue;
            _intervalSteps = double.NaN;
        }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Range { get { return Max - Min; } } 

        public int IntervalDecimals { get; private set; }
         
        private double _intervalSteps; 
        /// <summary> Gets or sets Interval steps</summary>
        public double IntervalSteps
        {
            get { return _intervalSteps; }
            set
            {
                _intervalSteps = value;
                IntervalDecimals = GetDecimals(value);
            }
        }
        /// <summary> Round a value to closets decimal point of the interval </summary> 
        public double Round(double value)
        {
            return Math.Round(value, IntervalDecimals);
        }
        public override string ToString()
        {
            return string.Format("Min {0:##0.0}, Max {1:##0.0}, Range {2:##0.0}", Min, Max, Range);
        }

        public static int GetDecimals(double value)
        {
            int count = BitConverter.GetBytes(decimal.GetBits((decimal)value)[3])[2];
            return count;
        }
    }
}
