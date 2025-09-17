using Infragistics.Framework;
using System.Collections.Generic;
using System.Windows;

namespace System
{
    public static class PointEx
    {
        /// <summary>
        /// Tolerance for comparing points
        /// </summary>
        /// 
        public static double Tolerance = 0.0001;
        /// <summary>
        /// Checks is the current point between points A and B (or in a rectangle created from A and B)
        /// </summary>
        public static bool IsBetween(this Point p, Point a, Point b)
        {
            if (!p.IsPlottable()) return false;
            if (!a.IsPlottable()) return false;
            if (!b.IsPlottable()) return false;

            if (a.X <= p.X && p.X >= b.X &&
                a.Y <= p.Y && p.Y >= b.Y)
                return true;

            if (b.X <= p.X && p.X >= a.X &&
                b.Y <= p.Y && p.Y >= a.Y)
                return true;

            return false;
        }

        public static bool IsBetween(this double p, double a, double b)
        {
            return (a <= p && p <= b) || 
                   (b <= p && p <= a);
        }

        /// <summary>
        /// Checks is the current point is plottable (not Nan and not Infinity)
        /// </summary>
        public static bool IsPlottable(this Point p)
        {
            return !double.IsNaN(p.X) && !double.IsInfinity(p.X) 
                && !double.IsNaN(p.Y) && !double.IsInfinity(p.Y);
        }


        /// <summary>
        /// Checks is the current point on a line passing between points A and B
        /// </summary>
        public static bool IsOnLine(this Point p, Point a, Point b)
        {
            if (!p.IsPlottable()) return false;
         
            if (a == p) return true;
            if (b == p) return true;

            var productA = (b.X - a.X) * (p.Y - a.Y);
            var productB = (p.X - a.X) * (b.Y - a.Y);

            return System.Math.Abs(productA - productB) < Tolerance;

            //var crossProduct = (p.Y - a.Y) * (b.X - a.X) - (p.X - a.X) * (b.Y - a.Y);
            //if (System.Math.Abs(crossProduct) > 0.0001) return false;

            //return true;
        }

        /// <summary>
        /// Checks is the current point is on a line segment between points A and B
        /// </summary>
        public static bool IsOnSegment(this Point p, Point a, Point b)
        {
            var isOnLine = p.IsOnLine(a, b);
            var isSame = System.Math.Abs(a.X - b.X) > Tolerance;
            var isBetween = isSame ? p.X.IsBetween(a.X, b.X) : p.Y.IsBetween(a.Y, b.Y);

            return isOnLine && isBetween;
        
            //if (a == p) return true;
            //if (b == p) return true;

            //var crossProduct = (p.Y - a.Y) * (b.X - a.X) - (p.X - a.X) * (b.Y - a.Y);
            //if (System.Math.Abs(crossProduct) > 0.0001) return false;

            //var dotProduct = (p.X - a.X) * (b.X - a.X) + (p.Y - a.Y) * (b.Y - a.Y);
            //if (dotProduct < 0) return false;

            //var squaredlengthba = (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
            //if (dotProduct > squaredlengthba) return false;

            //return true;
        }

        /// <summary>
        /// Gets intersection from the current point to a line passing between points A and B
        /// </summary>
        public static Point GetIntersectionWith(this Point p, Point a, Point b)
        {
            if (a == b) return a;

            var ap = new Point(p.X - a.X, p.Y - a.Y); // vector A -> P
            var ab = new Point(b.X - a.X, b.Y - a.Y); // vector A -> B

            var squareAB = (ab.X * ab.X) + (ab.Y * ab.Y);

            // The dot product of A -> P and A -> B vectors
            var product = (ap.X * ab.X) + (ap.Y * ab.Y);
            // The normalized "distance" from a to closest point
            var distance = product / squareAB;
            // add the distance to A, moving towards B
            var x = a.X + ab.X * distance;
            var y = a.Y + ab.Y * distance;
            return new Point(x, y);
        }

        //TODO move to ShapefileConverterEx
        public static List<Point> GetLongestShape(this List<List<Point>> shapes)
        {
            var list = new List<Point>();
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
        public static List<Point> GetLargestShape(this List<List<Point>> shapes)
        {
            var list = new List<Point>();
            var current = double.MinValue;
            foreach (var shape in shapes)
            {
                var area = shape.GetDimensions().Area;
                if (area > current)
                {
                    current = area;
                    list = shape;
                }
            }
            return list;
        }
        public static List<Point> GetBoundingPoints(this List<Point> shape)
        {
            var dim = shape.GetDimensions();
            var points = new List<Point>
            {
                new Point(dim.X.Min, dim.Y.Min),
                new Point(dim.X.Min, dim.Y.Max),
                new Point(dim.X.Max, dim.Y.Max),
                new Point(dim.X.Max, dim.Y.Min),
                new Point(dim.X.Min, dim.Y.Min),
            };
            return points;
        }
        public static List<Point> GetBoundingPoints(this List<List<Point>> shapes)
        {
            var dim = shapes.GetDimensions();
            var points = new List<Point>
            {
                new Point(dim.X.Min, dim.Y.Min),
                new Point(dim.X.Min, dim.Y.Max),
                new Point(dim.X.Max, dim.Y.Max),
                new Point(dim.X.Max, dim.Y.Min),
                //new Point(dim.X.Min, dim.Y.Min),
            };
            return points;
        }
        public static Dimension2D GetDimensions(this List<Point> shape)
        {
            var dimensions = new Dimension2D();
            foreach (var p in shape)
            {
                dimensions.X.Min = Math.Min(dimensions.X.Min, p.X);
                dimensions.X.Max = Math.Max(dimensions.X.Max, p.X);

                dimensions.Y.Min = Math.Min(dimensions.Y.Min, p.Y);
                dimensions.Y.Max = Math.Max(dimensions.Y.Max, p.Y); 
            }
            return dimensions;
        }

        public static Dimension2D GetDimensions(this List<List<Point>> shapes)
        {
            var dimensions = new Dimension2D();
            foreach (var shape in shapes)
            {
                foreach (var p in shape)
                {
                    dimensions.X.Min = Math.Min(dimensions.X.Min, p.X);
                    dimensions.X.Max = Math.Max(dimensions.X.Max, p.X);

                    dimensions.Y.Min = Math.Min(dimensions.Y.Min, p.Y);
                    dimensions.Y.Max = Math.Max(dimensions.Y.Max, p.Y); 
                }
            }
            return dimensions;
        }

        public static List<Point> Simplify(this List<Point> shape, double minArea, double minLength, int minCount = 2)
        {
            var points = new List<Point>();
            if (shape == null || shape.Count < minCount)
                return points;

            var dimensions = shape.GetDimensions();
            if (dimensions.Area < minArea)
                return points;
              
            for (int i = 0; i < shape.Count; i++)
            {
                if (i == 0)
                {
                    points.Add(shape[0]);
                }
                else
                {
                    var x = Math.Abs(points.Last<Point>().X - shape[i].X);
                    var y = Math.Abs(points.Last<Point>().Y - shape[i].Y);
                    if (x >= minLength || y >= minLength)
                    {
                        points.Add(shape[i]);
                    }
                }
            }
            return points;
        }
        public static List<List<Point>> Simplify(this List<List<Point>> shapes, double minArea, double minLength, int minCount = 3)
        {
            var simple = new List<List<Point>>();
            foreach (var shape in shapes)
            {
                var points = shape.Simplify(minArea, minLength, minCount);
                if (points.Count > minCount)
                    simple.Add(points);
            }
            
            return simple;
        }

    }
} 