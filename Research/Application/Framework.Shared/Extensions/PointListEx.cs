using Infragistics.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace System.Collections.Generic
{
    public static class Point3D_Exensions
    {
        public static Point3D ToPoint3D(this Point p, double value = double.NaN)
        {
            return new Point3D(p.X, p.Y, value);
        }

        public static List<Point3D> ToPointList(this List<Point> points, double value = double.NaN)
        {
            var list = new List<Point3D>();
            foreach (var p in points)
            {
                list.Add(p.ToPoint3D(value));
            }
            return list;
        }
        public static List<List<Point3D>> ToShapePoints(this List<List<Point>> shapes, double value = double.NaN)
        {
            var list = new List<List<Point3D>>();
            foreach (var shape in shapes)
            {
                list.Add(shape.ToPointList(value));
            }
            return list;
        }

        public static List<Point3D> GetLongest(this List<List<Point>> shapes, double value = double.NaN)
        {
            var list = new List<Point3D>();
            var lenght = int.MinValue;
            foreach (var shape in shapes)
            {
                if (lenght < shape.Count)
                {
                    lenght = shape.Count;
                    list = shape.ToPointList();
                } 
            }
            return list;
        }

    }
}
