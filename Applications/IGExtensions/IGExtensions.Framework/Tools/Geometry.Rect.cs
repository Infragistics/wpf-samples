
using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Toolity class for rectangle-based calculations.
    /// </summary>
    public static partial class GeometryTool
    {
        public static Rect Inflated(this Rect rect, Thickness thickness)
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
        
        ///// <summary>
        ///// Gets the center of the current rectangle
        ///// </summary>
        ///// <param name="rect">The current rectangle.</param>
        ///// <returns>Center point</returns>
        //public static Point Center(this Rect rect)
        //{
        //    if (rect.IsEmpty)
        //    {
        //        return new Point(Double.NaN, Double.NaN);
        //    }

        //    return new Point(0.5 * (rect.Left + rect.Right), 0.5 * (rect.Bottom + rect.Top));
        //}

        ///// <summary>
        ///// Calculates the area of the current rectangle.
        ///// </summary>
        ///// <param name="rect">The current rectangle.</param>
        ///// <returns>The area of the current rectangle.</returns>
        //public static double Area(this Rect rect)
        //{
        //    if (rect.IsEmpty)
        //    {
        //        return 0.0;
        //    }

        //    return rect.Width * rect.Height;
        //}

        /// <summary>
        /// Gets the attachment point on the current rectangle for the leader line
        /// to the specified anchor.
        /// </summary>
        /// <remarks>
        /// If the anchor lies within the current rectangle, the anchor is returned.  
        /// </remarks>
        /// <param name="rect">Area to join to the anchor</param>
        /// <param name="anchor">Anchor to join to the rectangle</param>
        /// <returns>Attachment point.</returns>
        public static Point Leader(this Rect rect, Point anchor)
        {
            if (rect.Contains(anchor))
            {
                return anchor;
            }

            Point C = new Point(rect.Left + 0.5 * rect.Width, rect.Top + 0.5 * rect.Height);
            Point D = new Point(anchor.X - C.X, anchor.Y - C.Y);
            double p;

            if (D.X != 0)
            {
                p = (rect.Left - C.X) / D.X;
                double y = C.Y + p * D.Y;

                if (y > rect.Top && y < rect.Bottom)
                {
                    return p > 0 ? new Point(rect.Left, y) : new Point(rect.Right, C.Y - p * D.Y);
                }
            }

            p = (rect.Top - C.Y) / D.Y;
            double x = C.X + p * D.X;

            return p > 0 ? new Point(x, rect.Top) : new Point(C.X - p * D.X, rect.Bottom);
        }

        ///// <summary>
        ///// Calculates the square of the distance from the current rectangle
        ///// to the specfied point. 
        ///// </summary>
        ///// <remarks>
        ///// If the point lies within the current rectangle, the separation is considered
        ///// to be zero.
        ///// </remarks>
        ///// <param name="rect">Current rectangle.</param>
        ///// <param name="pt">Point to test.</param>
        ///// <returns>The square of the separation.</returns>
        //public static double DistanceSquared(this Rect rect, Point pt)
        //{
        //    return DistanceSquared(rect, pt.X, pt.Y);
        //}

        ///// <summary>
        ///// Calculates the square of the distance from the current rectangle
        ///// to the specfied point. 
        ///// </summary>
        ///// <param name="rc">Current rectangle.</param>
        ///// <param name="x">Point X coordinate.</param>
        ///// <param name="y">Point Y coordinate.</param>
        ///// <returns></returns>
        //private static double DistanceSquared(this Rect rc, double x, double y)
        //{
        //    if (rc.IsEmpty)
        //    {
        //        return Double.NaN;
        //    }

        //    double vs = x - rc.Left;
        //    double vt = y - rc.Top;
        //    double s = rc.Width * vs;
        //    double t = rc.Height * vt;

        //    if (s > 0)
        //    {
        //        double s0 = rc.Width * rc.Width;

        //        if (s < s0)
        //        {
        //            vs -= (s / s0) * rc.Width;
        //        }
        //        else
        //        {
        //            vs -= rc.Width;
        //        }
        //    }

        //    if (t > 0)
        //    {
        //        double t0 = rc.Height * rc.Height;

        //        if (t < t0)
        //        {
        //            vt -= (t / t0) * rc.Height;
        //        }
        //        else
        //        {
        //            vt -= rc.Height;
        //        }
        //    }

        //    return vs * vs + vt * vt;
        //}

        ///// <summary>
        ///// Calculates the square of the distance from the current rectangle
        ///// to the specfied rectangle. 
        ///// </summary>
        ///// <remarks>
        ///// If the rectangles intersect, their separation is considered
        ///// to be zero.
        ///// </remarks>
        ///// <param name="rect">Current rectangle.</param>
        ///// <param name="rc">Rectangle to test.</param>
        ///// <returns>The square of the separation.</returns>
        //public static double DistanceSquared(this Rect rect, Rect rc)
        //{
        //    double d2=Double.PositiveInfinity;

        //    if (!rect.IsEmpty)
        //    {
        //        d2 = DistanceSquared(rect, rc.Left, rc.Top);
        //        d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rect, rc.Left, rc.Bottom)) : d2;
        //        d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rect, rc.Right, rc.Bottom)) : d2;
        //        d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rect, rc.Right, rc.Top)) : d2;
        //        d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Left, rect.Top)) : d2;
        //        d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Left, rect.Bottom)) : d2;
        //        d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Right, rect.Bottom)) : d2;
        //        d2 = d2 > 0 ? Math.Min(d2, DistanceSquared(rc, rect.Right, rect.Top)) : d2;
        //    }

        //    return d2;
        //}

        ///// <summary>
        ///// Indicates whether the current rectangle wholly contains the specified rectangle.
        ///// </summary>
        ///// <param name="rect">The current rectangle</param>
        ///// <param name="rc">Rectangle to test for strict inclusion</param>
        ///// <returns></returns>
        //public static bool Contains(this Rect rect, Rect rc)
        //{
        //    if (rect.IsEmpty || rc.IsEmpty)
        //    {
        //        return false;
        //    }

        //    if (rect.Left > rc.Left) { return false; }
        //    if (rect.Right < rc.Right) { return false; }
        //    if (rect.Top > rc.Top) { return false; }
        //    if (rect.Bottom < rc.Bottom) { return false; }

        //    return true;
        //}

        ///// <summary>
        ///// Indicates whether the specified rectangle intersects with the current rectangle. 
        ///// </summary>
        ///// <param name="rect">The current rectangle</param>
        ///// <param name="rc">The rectangle to check</param>
        ///// <returns>true if the specified rectangle intersects with the current rectangle; otherwise, false.</returns>
        //public static bool IntersectsWith(this Rect rect, Rect rc)
        //{
        //    if (rect.IsEmpty || rc.IsEmpty)
        //    {
        //        return false;
        //    }

        //    if (rect.Right < rc.Left) { return false; }
        //    if (rect.Left > rc.Right) { return false; }
        //    if (rect.Top > rc.Bottom) { return false; }
        //    if (rect.Bottom < rc.Top) { return false; }

        //    return true;
        //}

        ///// <summary>
        ///// Calculates the area of intersection between the specified rectangle and the current rectangle
        ///// </summary>
        ///// <param name="rect">The current rectangle</param>
        ///// <param name="rc">The rectangle to check</param>
        ///// <returns>The area of intersection or 0.0 if the rectangles do not intersect.</returns>
        //public static double IntersectionArea(this Rect rect, Rect rc)
        //{
        //    if (rect.IsEmpty || rc.IsEmpty)
        //    {
        //        return 0.0;
        //    }

        //    double width = Math.Min(rect.Right, rc.Right) - Math.Max(rect.Left, rc.Left);

        //    if (width <= 0)
        //    {
        //        return 0;
        //    }

        //    double height = Math.Min(rect.Bottom, rc.Bottom) - Math.Max(rect.Top, rc.Top);

        //    if (height <= 0)
        //    {
        //        return 0;
        //    }

        //    return width * height;
        //}
  
    }
}
