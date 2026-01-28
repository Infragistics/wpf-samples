using System;
using System.Collections.Generic;
using System.Windows;

namespace Infragistics.Framework
{
    public class Point3D : Point2D
    {
        public Point3D() : this(double.NaN, double.NaN, double.NaN)
        { }

        public Point3D(double x, double y, double z = double.NaN)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        public Point3D(Point2D location, double z = double.NaN)
        {
            this.X = location.X;
            this.Y = location.Y;
            this.Z = z;
        }
        public Point3D(Point3D point)
        {
            this.X = point.X;
            this.Y = point.Y;
            this.Z = point.Z;
        }

        private double _z;
        /// <summary> Gets or sets Z coordinate </summary>
        public double Z
        {
            get { return _z; }
            set { if (_z == value) return; _z = value; OnPropertyChanged("Z"); }
        }

        public override string ToString()
        {
            return string.Format("X {0:##0.000}, Y {1:##0.000}, Z {2:##0.0} \n", X, Y, Z);
        }

    }
    public class Dimension1D 
    {
        public double Min { get; set; }
        public double Max { get; set; }
        public double Range { get { return Max - Min; } }
        public double Center { get { return Min + (Range / 2.0); } }

    }
    public class Dimension2D
    {
        public Dimension1D X { get; set; }
        public Dimension1D Y { get; set; }
         
        public double Area { get { return X.Range * Y.Range; } }

        public Dimension2D()
        {
            X = new Dimension1D();
            Y = new Dimension1D(); 
        }

    }
    public class Dimension3D : Dimension2D
    { 
        public Dimension1D Z { get; set; }
        public double Volume { get { return Area * Z.Range; } }

        public Dimension3D()
        { 
            Z = new Dimension1D();
        }
    }

    public static class ExPoint3D 
    {
        public static List<Point3D> GetLongest(this List<List<Point3D>> shapes)
        {
            var list = new List<Point3D>();
            var lenght = int.MinValue;
            foreach (var shape in shapes)
            {
                if (lenght < shape.Count)
                {
                    lenght = shape.Count;
                    list = shape;
                }
            }
            return list;
        }
        public static List<Point3D> GetBoundingPoints(this List<Point3D> shape)
        {
            var dim = shape.GetDimensions();
            var points = new List<Point3D>
            {
                new Point3D(dim.X.Min, dim.Y.Min, dim.Z.Min),
                new Point3D(dim.X.Min, dim.Y.Max, dim.Z.Min),
                new Point3D(dim.X.Max, dim.Y.Max, dim.Z.Min),
                new Point3D(dim.X.Max, dim.Y.Min, dim.Z.Min),
                new Point3D(dim.X.Min, dim.Y.Min, dim.Z.Min),
            }; 
            return points;
        }

        public static List<Point3D> GetBoundingPoints(this List<List<Point3D>> shapes)
        {
            var dim = shapes.GetDimensions();
            var points = new List<Point3D>
            {
                new Point3D(dim.X.Min, dim.Y.Min, dim.Z.Min),
                new Point3D(dim.X.Min, dim.Y.Max, dim.Z.Min),
                new Point3D(dim.X.Max, dim.Y.Max, dim.Z.Min),
                new Point3D(dim.X.Max, dim.Y.Min, dim.Z.Min),
                new Point3D(dim.X.Min, dim.Y.Min, dim.Z.Min),
            };
            return points;
        }

        public static Dimension3D GetDimensions(this List<Point3D> shape)
        { 
            var dimensions = new Dimension3D(); 
            foreach (var p in shape)
            {
                dimensions.X.Min = Math.Min(dimensions.X.Min, p.X);
                dimensions.X.Max = Math.Max(dimensions.X.Max, p.X);

                dimensions.Y.Min = Math.Min(dimensions.Y.Min, p.Y);
                dimensions.Y.Max = Math.Max(dimensions.Y.Max, p.Y);

                dimensions.Z.Min = Math.Min(dimensions.Z.Min, p.Z);
                dimensions.Z.Max = Math.Max(dimensions.Z.Max, p.Z);
            }
            return dimensions;
        }

        public static Dimension3D GetDimensions(this List<List<Point3D>> shapes)
        {
            var dimensions = new Dimension3D();
            foreach (var shape in shapes)
            {
                foreach (var p in shape)
                {
                    dimensions.X.Min = Math.Min(dimensions.X.Min, p.X);
                    dimensions.X.Max = Math.Max(dimensions.X.Max, p.X);

                    dimensions.Y.Min = Math.Min(dimensions.Y.Min, p.Y);
                    dimensions.Y.Max = Math.Max(dimensions.Y.Max, p.Y);

                    dimensions.Z.Min = Math.Min(dimensions.Z.Min, p.Z);
                    dimensions.Z.Max = Math.Max(dimensions.Z.Max, p.Z);
                }
            }
            return dimensions;
        }
    }

}