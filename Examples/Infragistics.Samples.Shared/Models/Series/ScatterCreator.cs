using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Infragistics.Samples.Shared.Models
{
    // for scatter point, line, spline, HD series
    public class ScatterPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

    }
    // for scatter contour or area series
    public class ScatterValue
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double V { get; set; }
    }

    // for scatter bubble series
    public class ScatterBubble : ScatterValue
    {
        public double R { get; set; }
        public double F { get; set; }
    }

    public class ScatterShape : ScatterBubble
    {
        public List<List<Point>> Points { get; set; }
    }

    public class ScatterCreator
    {
        internal static List<ScatterShape> Data;

        static ScatterCreator()
        {
            Data = new List<ScatterShape>();
            // X   Y   V   R  and Points around X and Y
            Data.Add(CreateDataItem(30, 30, 10, 20));
            Data.Add(CreateDataItem(30, 70, 10, 20));
            Data.Add(CreateDataItem(70, 70, 90, 50));
            Data.Add(CreateDataItem(70, 30, 90, 50)); 
        }

        static Random Rand = new Random();

        public ScatterCreator()
        {
            SingleListPoints = new List<ScatterPoint>();
            SingleListValues = new List<ScatterValue>();
            SingleListShapes = new List<ScatterShape>();
            SingleListBubbles = new List<ScatterBubble>();
            SingleListDensity = new List<ScatterPoint>();

            foreach (var i in Data)
            {
                SingleListPoints.Add(new ScatterPoint { X = i.X, Y = i.Y });
                SingleListValues.Add(new ScatterValue { X = i.X, Y = i.Y, V = i.V });
                SingleListShapes.Add(new ScatterShape { X = i.X, Y = i.Y, V = i.V, R = i.R, Points = i.Points });
                SingleListBubbles.Add(new ScatterBubble { X = i.X, Y = i.Y, V = i.V, R = i.R });
                
                SingleListDensity.AddRange(CreateDensityPoints(i.X, i.Y));
            }
              
        }

        public List<ScatterPoint> SingleListDensity { get; set; }
        public List<ScatterBubble> SingleListBubbles { get; set; }
        public List<ScatterValue> SingleListValues { get; set; }
        public List<ScatterPoint> SingleListPoints { get; set; }
        public List<ScatterShape> SingleListShapes { get; set; }
          
        public static ScatterShape CreateDataItem(double x, double y, double v, double r, double offset = 10)
        {
            var item = new ScatterShape { X = x, Y = y, V = v, R = r };
            item.Points = SingleShapePoints(x, y, offset);
            return item;
        }

        public static ScatterPoint Create(double x, double y)
        {
            var item = new ScatterPoint { X = x, Y = y };
            return item;
        }
        public static ScatterValue Create(double x, double y, double v)
        {
            var item = new ScatterValue { X = x, Y = y, V = v };
            return item;
        }
        public static ScatterBubble Create(double x, double y, double v, double r)
        {
            var item = new ScatterBubble { X = x, Y = y, V = v, R = r };
            return item;
        }
        public static ScatterShape Create(double x, double y, double v, double r, double offset)
        {
            var item = new ScatterShape { X = x, Y = y, V = v, R = r };
            item.Points = SingleShapePoints(x, y, offset);
            return item;
        }


        private static List<List<Point>> SingleShapePoints(double x, double y, double offset = 10)
        {
            var points = new List<Point>();

            points.Add(new Point { X = x - offset, Y = y - offset });
            points.Add(new Point { X = x + offset, Y = y - offset });
            points.Add(new Point { X = x + offset, Y = y + offset });
            points.Add(new Point { X = x - offset, Y = y + offset });

            return new List<List<Point>> { points };
        }

        private static List<ScatterPoint> CreateDensityPoints(double x, double y, int range = 10)
        {
            var points = new List<ScatterPoint>();

            for (int p = 0; p < 1000; p++)
            {
                var dx = x + (Rand.Next(-range, range) * Rand.NextDouble());
                var dy = y + (Rand.Next(-range, range) * Rand.NextDouble());
                points.Add(new ScatterPoint { X = dx, Y = dy });
            }
            range = range / 2;
            for (int p = 0; p < 1500; p++)
            {
                var dx = x + (Rand.Next(-range, range) * Rand.NextDouble());
                var dy = y + (Rand.Next(-range, range) * Rand.NextDouble());
                points.Add(new ScatterPoint { X = dx, Y = dy });
            }

            return points;
        }
    }
}
