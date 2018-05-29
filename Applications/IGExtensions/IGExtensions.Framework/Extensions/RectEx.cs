using System.Collections.Generic;

namespace System.Windows
{
    public static class RectEx
    {
        public static Rect UnionMax(this Rect rect, Rect otherRect)
        {
            var xMin = System.Math.Min(rect.Left, otherRect.Left);
            var xMax = System.Math.Max(rect.Right, otherRect.Right);
            var yMin = System.Math.Min(rect.Top, otherRect.Top);
            var yMax = System.Math.Max(rect.Bottom, otherRect.Bottom);
            var h = yMax - yMin;
            var w = xMax - xMin;
            var bounds = new Rect(xMin, yMin, w, h);
            return bounds;
        }


        #region Validation
        public static bool IsValid(this Rect rect)
        {
            if (rect.IsEmpty) return false;
            if (rect.Width == 0.0 || rect.Height == 0.0) return false;
            return true;
        }
        /// <summary>
        /// Indicates whether the current rectangle wholly contains the specified rectangle.
        /// </summary>
        /// <param name="rect">The current rectangle</param>
        /// <param name="rc">Rectangle to test for strict inclusion</param>
        /// <returns></returns>
        public static bool Contains(this Rect rect, Rect rc)
        {
            if (rect.IsEmpty || rc.IsEmpty) return false;

            if (rect.Left > rc.Left) { return false; }
            if (rect.Right < rc.Right) { return false; }
            if (rect.Top > rc.Top) { return false; }
            if (rect.Bottom < rc.Bottom) { return false; }

            return true;
        }
        #endregion

        #region Attributes
        /// <summary>
        /// Calculates the area of the current rectangle.
        /// </summary>
        /// <param name="rect">The current rectangle.</param>
        /// <returns>The area of the current rectangle.</returns>
        public static double Area(this Rect rect)
        {
            if (rect.IsEmpty) return 0.0;
            return rect.Width * rect.Height;
        }
        /// <summary>
        /// Gets the center of the current rectangle
        /// </summary>
        public static Point Center(this Rect rect)
        {
            if (rect.IsEmpty)
            {
                return new Point(Double.NaN, Double.NaN);
            }
            return new Point(0.5 * (rect.Left + rect.Right), 0.5 * (rect.Bottom + rect.Top));
        }
//#if SILVERLIGHT
        public static Point TopLeft(this Rect rect)
        {
            return new Point(rect.Left, rect.Top);
        }
        public static Point TopRight(this Rect rect)
        {
            return new Point(rect.Right, rect.Top);
        }
        public static Point BottomRight(this Rect rect)
        {
            return new Point(rect.Right, rect.Bottom);
        }
        public static Point BottomLeft(this Rect rect)
        {
            return new Point(rect.Left, rect.Bottom);
        }
        public static List<Point> Corners(this Rect rect)
        {
            return rect.ToPoints();
        } 
//#endif
        #endregion

        public static void Normalize(this Rect rect, Rect boundingRect)
        {
            //TODO implement Normalize method
            var x = rect.X < boundingRect.X ? boundingRect.X : rect.X;
            x = (rect.X + rect.Width) > boundingRect.Right ? boundingRect.X : rect.X;

            //if (boundingRect.Contains(rect))
            //    return rect;
            //if (!boundingRect.IntersectsWith(rect))
            //    return boundingRect;
        }
        #region Intersection
      
        /// <summary>
        /// Indicates whether the specified rectangle intersects with the current rectangle. 
        /// </summary>
        /// <param name="rect">The current rectangle</param>
        /// <param name="rc">The rectangle to check</param>
        /// <returns>true if the specified rectangle intersects with the current rectangle; otherwise, false.</returns>
        public static bool IntersectsWith(this Rect rect, Rect rc)
        {
            if (rect.IsEmpty || rc.IsEmpty) return false;

            if (rect.Right < rc.Left) { return false; }
            if (rect.Left > rc.Right) { return false; }
            if (rect.Top > rc.Bottom) { return false; }
            if (rect.Bottom < rc.Top) { return false; }

            return true;
        }
        /// <summary>
        /// Calculates the area of intersection between the specified rectangle and the current rectangle
        /// </summary>
        /// <param name="rect">The current rectangle</param>
        /// <param name="rc">The rectangle to check</param>
        /// <returns>The area of intersection or 0.0 if the rectangles do not intersect.</returns>
        public static double IntersectionArea(this Rect rect, Rect rc)
        {
            if (rect.IsEmpty || rc.IsEmpty) return 0.0;

            double width = Math.Min(rect.Right, rc.Right) - Math.Max(rect.Left, rc.Left);
            if (width <= 0) return 0;

            double height = Math.Min(rect.Bottom, rc.Bottom) - Math.Max(rect.Top, rc.Top);
            if (height <= 0) return 0;

            return width * height;
        } 
        #endregion

        #region Distance 
        
        /// <summary>
        /// Calculates the square of the distance from the current rectangle to the specified point. 
        /// </summary>
        /// <remarks>
        /// If the point lies within the current rectangle, the separation is considered
        /// to be zero.
        /// </remarks>
        /// <param name="rect">Current rectangle.</param>
        /// <param name="pt">Point to test.</param>
        /// <returns>The square of the separation.</returns>
        public static double DistanceSquared(this Rect rect, Point pt)
        {
            return DistanceSquared(rect, pt.X, pt.Y);
        }

        /// <summary>
        /// Calculates the square of the distance from the current rectangle to the specified point. 
        /// </summary>
        /// <param name="rect">Current rectangle.</param>
        /// <param name="x">Point X coordinate.</param>
        /// <param name="y">Point Y coordinate.</param>
        /// <returns></returns>
        private static double DistanceSquared(this Rect rect, double x, double y)
        {
            if (rect.IsEmpty)
            {
                return Double.NaN;
            }

            double vs = x - rect.Left;
            double vt = y - rect.Top;
            double s = rect.Width * vs;
            double t = rect.Height * vt;

            if (s > 0)
            {
                double s0 = rect.Width * rect.Width;

                if (s < s0)
                {
                    vs -= (s / s0) * rect.Width;
                }
                else
                {
                    vs -= rect.Width;
                }
            }

            if (t > 0)
            {
                double t0 = rect.Height * rect.Height;

                if (t < t0)
                {
                    vt -= (t / t0) * rect.Height;
                }
                else
                {
                    vt -= rect.Height;
                }
            }

            return vs * vs + vt * vt;
        }

        /// <summary>
        /// Calculates the square of the distance from the current rectangle to the specified rectangle. 
        /// </summary>
        /// <remarks>
        /// If the rectangles intersect, their separation is considered
        /// to be zero.
        /// </remarks>
        /// <param name="rect">Current rectangle.</param>
        /// <param name="rc">Rectangle to test.</param>
        /// <returns>The square of the separation.</returns>
        public static double DistanceSquared(this Rect rect, Rect rc)
        {
            double d2 = Double.PositiveInfinity;

            if (!rect.IsEmpty)
            {
                d2 = DistanceSquared(rect, rc.Left, rc.Top);
                d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rect, rc.Left, rc.Bottom)) : d2;
                d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rect, rc.Right, rc.Bottom)) : d2;
                d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rect, rc.Right, rc.Top)) : d2;
                d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Left, rect.Top)) : d2;
                d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Left, rect.Bottom)) : d2;
                d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Right, rect.Bottom)) : d2;
                d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Right, rect.Top)) : d2;
            }

            return d2;
        }


        #endregion

        #region Expand
        public static Rect Expand(this Rect rect, Thickness thickness)
        {
            if (rect.IsEmpty) return rect;

            double left = rect.X - thickness.Left;
            double top = rect.Top - thickness.Top;
            double right = rect.Right + thickness.Right;
            double bottom = rect.Bottom + thickness.Bottom;

            if (right < left || bottom < top)
            {
                return Rect.Empty;
            }
            return new Rect(left, top, right - left, bottom - top);
        }

        public static Rect Expand(this Rect rect, Point expand)
        {
            if (rect.IsEmpty) return rect;
            double x = rect.X - expand.X;
            double y = rect.Top - expand.Y;
            double w = rect.Width + expand.X;
            double h = rect.Height + expand.Y;

            return new Rect(x, y, w, h);
        }
        public static Rect Expand(this Rect rect, double expandX, double expandY)
        {
            return rect.Expand(new Point(expandX, expandY));
        }
        public static Rect Expand(this Rect rect, int expand)
        {
            return rect.Expand(expand, expand);
        }
        public static Rect Expand(this Rect rect, Size expand)
        {
            return rect.Expand(expand.Width, expand.Height);
        }
        #endregion

        #region Shift
        public static Rect Shift(this Rect rect, Point offset)
        {
            return rect.Shift(offset.X, offset.Y);
        }
        public static Rect Shift(this Rect rect, double offsetX, double offsetY)
        {
            return new Rect(rect.X + offsetX, rect.Y + offsetY, rect.Width, rect.Height);
        }
        public static Rect Shift(this Rect rect, double offset)
        {
            return rect.Shift(offset, offset);
        }
        public static Rect Clone(this Rect rect)
        {
            return new Rect(rect.X, rect.Y, rect.Width, rect.Height);
        }
        #endregion

        #region Convertion
        public static void FromPoints(this Rect rect, List<Point> points)
        {
            var boundingRect = RectEx.FromPoints(points);
            rect.X = boundingRect.X;
            rect.Y = boundingRect.Y;
            rect.Width = boundingRect.Width;
            rect.Height = boundingRect.Height;
        }
        public static Rect FromPoints(List<Point> points)
        {
            if (points.Count < 2) return Rect.Empty;

            double xMin = double.MaxValue, xMax = double.MinValue,
                   yMin = double.MaxValue, yMax = double.MinValue;
            foreach (var point in points)
            {
                xMin = System.Math.Min(point.X, xMin);
                yMin = System.Math.Min(point.Y, yMin);
                xMax = System.Math.Max(point.X, xMax);
                yMax = System.Math.Max(point.Y, yMax);
            }
            if (xMin == double.MaxValue || yMin == double.MaxValue ||
                xMax == double.MinValue || yMax == double.MinValue)
                return Rect.Empty;

            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }
        public static void FromShapePoints(this Rect rect, List<List<Point>> points)
        {
            var boundingRect = RectEx.FromShapePoints(points);
            rect.X = boundingRect.X;
            rect.Y = boundingRect.Y;
            rect.Width = boundingRect.Width;
            rect.Height = boundingRect.Height;
        }

        public static Rect FromShapePoints(List<List<Point>> points)
        {
            double yMin = double.MaxValue, xMax = double.MinValue,
                   xMin = double.MaxValue, yMax = double.MinValue;

            foreach (var list in points)
            {
                foreach (var point in list)
                {
                    xMin = System.Math.Min(point.X, xMin);
                    yMin = System.Math.Min(point.Y, yMin);
                    xMax = System.Math.Max(point.X, xMax);
                    yMax = System.Math.Max(point.Y, yMax);
                }
            }
            if (xMin == double.MaxValue || yMin == double.MaxValue ||
                xMax == double.MinValue || yMax == double.MinValue)
                return Rect.Empty;

            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        /// <summary>
        /// Converts shape of <see cref="Rect"/> to a List of <see cref="Point"/>
        /// </summary>
        public static List<Point> ToPoints(this Rect rect)
        {
            
            var points = new List<Point>();
            points.Add(rect.TopLeft());
            points.Add(rect.TopRight());
            points.Add(rect.BottomRight());
            points.Add(rect.BottomLeft());
            //points.Add(rect.TopLeft());
            return points;
        }
        /// <summary>
        /// Converts shape of <see cref="Rect"/> to a List of List of <see cref="Point"/>
        /// </summary>
        public static List<List<Point>> ToShapePoints(this Rect rect)
        {
            var geoShapePoints = new List<List<Point>> { rect.ToPoints() };
            return geoShapePoints;
      
        }

        #endregion
    }
}