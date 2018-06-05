using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Represents a tool with geometry calculations and operations
    /// </summary>
    public static partial class GeometryTool
    {
        /// <summary>
        /// Calculates the coefficients of a polynomial fit or possibly the coefficients for
        /// a piecewise cubic fit
        /// </summary>
        /// <param name="count"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="yp1"></param>
        /// <param name="ypn"></param>
        /// <returns></returns>
        public static double[] Fit(int count, Func<int, double> x, Func<int, double> y, double yp1, double ypn)
        {
            var u = new double[count - 1];
            var y2 = new double[count];

            // start point conditions

            y2[0] = double.IsNaN(yp1) ? 0.0 : -0.5;
            u[0] = double.IsNaN(yp1) ? 0.0 : (3.0 / (x(1) - x(0))) * ((y(1) - y(0)) / (x(1) - x(0)) - yp1);

            // tri-diagonal decomposition

            for (int i = 1; i < count - 1; i++)
            {
                double sig = (x(i) - x(i - 1)) / (x(i + 1) - x(i - 1));
                double p = sig * y2[i - 1] + 2.0;

                y2[i] = (sig - 1.0) / p;
                u[i] = (y(i + 1) - y(i)) / (x(i + 1) - x(i)) - (y(i) - y(i - 1)) / (x(i) - x(i - 1));
                u[i] = (6.0 * u[i] / (x(i + 1) - x(i - 1)) - sig * u[i - 1]) / p;
            }

            // end point conditions

            double qn = double.IsNaN(ypn) ? 0.0 : 0.5;
            double un = double.IsNaN(ypn) ? 0.0 : (3.0 / (x(count - 1) - x(count - 2))) * (ypn - (y(count - 1) - y(count - 2)) / (x(count - 1) - x(count - 2)));

            y2[count - 1] = (un - qn * u[count - 2]) / (qn * y2[count - 2] + 1.0);

            // tri-diagonal back substitution

            for (int i = count - 2; i >= 0; i--)
            {
                y2[i] = y2[i] * y2[i + 1] + u[i];
            }

            return y2;
        }

        #region Interpolating Spline into Path Figure (to remove)
        /// <summary>
        /// Smooth a line
        /// </summary>
        /// <param name="count">Number of points in line.</param>
        /// <param name="x">x coordinate of ith point.</param>
        /// <param name="y">y coordinate of ith point.</param>
        /// <param name="resolution"></param>
        /// <returns>Smooth line.</returns>
        public static PathFigure Smooth(int count, Func<int, double> x, Func<int, double> y, double resolution)
        {
            IList<int> indices = Flatten(count, (i)=>new Point(x(i), y(i)), resolution);

            return Smooth(indices.Count, (i) => { return x(i); }, (i) => { return y(i); });
        }

        /// <summary>
        /// Smooth a line into a PathFigure of quadratic and cubic bezier splines.
        /// </summary>
        /// <param name="count">Number of equator in line.</param>
        /// <param name="x">x coordinate of ith point.</param>
        /// <param name="y">y coordinate of ith point.</param>
        /// <returns>Smooth line.</returns>
        public static PathFigure Smooth(int count, Func<int, double> x, Func<int, double> y)
        {
            var pathFigure = new PathFigure() { StartPoint = new Point(x(0), y(0)) };

            int n = count - 1;

            Point pa;
            Point pb = new Point(x(0), y(0));
            Point pc = new Point(x(0 + 1), y(0 + 1));
            Point pd = new Point(x(0 + 2), y(0 + 2));

            Point eab;
            double mab;

            Point ebc = new Point(pc.X - pb.X, pc.Y - pb.Y);
            double mbc = MathTool.Hypot(ebc.X, ebc.Y);

            Point ecd = new Point(pd.X - pc.X, pd.Y - pc.Y);
            double mcd = MathTool.Hypot(ecd.X, ecd.Y);

            Point tc;
            double sc;

            double alpha = 0.15;
            double beta = 0.45;

            {
                // first quadratic patch

                tc = new Point(pd.X - pb.X, pd.Y - pb.Y); { double m = MathTool.Hypot(tc.X, tc.Y); tc.X /= m; tc.Y /= m; }
                sc = 0.5 + (ebc.X * ecd.X + ebc.Y * ecd.Y) / (2.0 * mbc * mcd);

                QuadraticBezierSegment segment = new QuadraticBezierSegment()
                {
                    Point1 = new Point(pc.X - tc.X * (alpha + beta * sc) * mbc, pc.Y - tc.Y * (alpha + beta * sc) * mbc),
                    Point2 = pc
                };

                if(double.IsNaN(segment.Point1.X)||double.IsNaN(segment.Point1.Y))
                {
                }

                if(double.IsNaN(segment.Point2.X)||double.IsNaN(segment.Point2.Y))
                {
                }

                pathFigure.Segments.Add(segment);
            }

            for (int i = 1; i < n - 1; ++i)
            {
                // intermediate cubic patches

                pa = pb;
                pb = pc;
                pc = pd;
                pd = new Point(x(i + 2), y(i + 2));

                eab = ebc;
                mab = mbc;

                ebc = ecd;
                mbc = mcd;

                ecd = new Point(pd.X - pc.X, pd.Y - pc.Y);
                mcd = MathTool.Hypot(ecd.X, ecd.Y);

                Point tb = tc;
                double sb = sc;

                tc = new Point(pd.X - pb.X, pd.Y - pb.Y); { double m = MathTool.Hypot(tc.X, tc.Y); tc.X /= m; tc.Y /= m; }
                sc = 0.5 + (ebc.X * ecd.X + ebc.Y * ecd.Y) / (2.0 * mbc * mcd);

                BezierSegment segment = new BezierSegment()
                {
                    Point1 = new Point(pb.X + tb.X * (alpha + beta * sb) * mbc, pb.Y + tb.Y * (alpha + beta * sb) * mbc),
                    Point2 = new Point(pc.X - tc.X * (alpha + beta * sc) * mbc, pc.Y - tc.Y * (alpha + beta * sc) * mbc),
                    Point3 = pc
                };
                pathFigure.Segments.Add(segment);

                if (double.IsNaN(segment.Point1.X) || double.IsNaN(segment.Point1.Y))
                {
                }

                if (double.IsNaN(segment.Point2.X) || double.IsNaN(segment.Point2.Y))
                {
                }
                
                if (double.IsNaN(segment.Point3.X) || double.IsNaN(segment.Point3.Y))
                {
                }
            }

            {
                // last quadratic patch

                pa = pb;
                pb = pc;
                pc = pd;
                // pd
                eab = ebc;
                mab = mbc;

                ebc = ecd;
                mbc = mcd;

                Point tb = tc;
                double sb = sc;

                QuadraticBezierSegment segment = new QuadraticBezierSegment()
                {
                    Point1 = new Point(pb.X + tb.X * (alpha + beta * sb) * mbc, pb.Y + tb.Y * (alpha + beta * sb) * mbc),
                    Point2 = pc
                };
                pathFigure.Segments.Add(segment);


                if (double.IsNaN(segment.Point1.X) || double.IsNaN(segment.Point1.Y))
                {
                }

                if (double.IsNaN(segment.Point2.X) || double.IsNaN(segment.Point2.Y))
                {
                }
            }

            return pathFigure;
        }
        #endregion

        #region Interpolating Spline into points (to fix and rename)
        // outputs (p) cp cp p cp cp p cp cp p..
        public static void SplinePoints(IList<Point> ret, int count, Func<int, double> x, Func<int, double> y)
        {
            // flatten at resolution

            var ea = new Point(x(1) - x(0), y(1) - y(0));
            double eam = MathTool.Hypot(ea.X, ea.Y);

            ret.Add(new Point(x(0), y(0)));  // output point[0]
            ret.Add(new Point(x(0), y(0))); // output a bogus control point

            for (int i = 2; i < count; ++i)
            {
                var eb = new Point(x(i) - x(i - 1), y(i) - y(i - 1));
                double ebm = MathTool.Hypot(eb.X, eb.Y);

                double dot = (ea.X * eb.X + ea.Y * eb.Y) / (eam * ebm);
                //double tension = 0.35*Math.Exp(0.5 * (dot + 1.0))/(Math.resolution-1);
                double tension = 0.5 * (dot + 1.0);
                tension = 1.0 + tension * (Math.E - 1.0);
                tension = 0.5 * Math.Log(tension);

                var d = new Point(x(i) - x(i - 2), y(i) - y(i - 2));

                double dm = MathTool.Hypot(d.X, d.Y);

                if (dm == 0) { dm = double.PositiveInfinity; }

                ret.Add(new Point(x(i - 1) - eam * tension * d.X / dm, y(i - 1) - eam * tension * d.Y / dm));
                ret.Add(new Point(x(i - 1), y(i - 1)));
                ret.Add(new Point(x(i - 1) + ebm * tension * d.X / dm, y(i - 1) + ebm * tension * d.Y / dm));

                ea = eb;
                eam = ebm;
            }

            ret.Add(new Point(x(count - 1), y(count - 1))); // output a bogus control point
            ret.Add(new Point(x(count - 1), y(count - 1)));  // output point[0]
        }
        #endregion

        #region Spline into bezier control points
        /// <summary>
        /// Calculates a smoothing spline into bezier control points.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="knots"></param>
        /// <param name="tension"></param>
        public static void SmoothingSpline(IList<Point> points, IList<Point> knots, double tension)
        {
            double t = 0.5 * tension;
            var pi = knots[0];
            var pj = knots[1];
            var pk = new Point();

            var cpi = new Point(pi.X + (pj.X - pi.X) * t, pi.Y + (pj.Y - pi.Y) * t);
            var cpj = new Point();
            var pe = new Point();

            for (int j = 1; j < knots.Count - 1; ++j)
            {
                pk = knots[j + 1];

                pe.X = pj.X + ((pi.X + (pk.X - pi.X) * 0.5) - pj.X) * t;
                pe.Y = pj.Y + ((pi.Y + (pk.Y - pi.Y) * 0.5) - pj.Y) * t;

                cpj.X = pj.X - (pj.X - pi.X) * t;
                cpj.Y = pj.Y - (pj.Y - pi.Y) * t;

                points.Add(cpi);
                points.Add(cpj);
                points.Add(pe);

                cpi.X = pj.X + (pk.X - pj.X) * t;
                cpi.Y = pj.Y + (pk.Y - pj.Y) * t;

                pi = pj;
                pj = pk;
            }

            {
                cpj.X = pj.X - (pj.X - pi.X) * t;
                cpj.Y = pj.Y - (pj.Y - pi.Y) * t;

                points.Add(cpi);
                points.Add(cpj);
                points.Add(pj);
            }
        }

        /// <summary>
        /// Calculates an interpolating spline into bezier control points.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="knots"></param>
        /// <param name="tension"></param>
        public static void InterpolatingSpline(IList<Point> points, IList<Point> knots, double tension)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Spline into flattened polyline
        /// <summary>
        /// Calculates amd flattens a smoothing spline into a polyline points
        /// </summary>
        /// <param name="points"></param>
        /// <param name="knots"></param>
        /// <param name="resolution"></param>
        /// <param name="tension"></param>
        public static void SmoothingSpline(IList<Point> points, double resolution, IList<Point> knots, double tension)
        {
            double t = 0.5 * tension;
            Point pi = knots[0];
            Point pj = knots[1];
            Point pk = new Point();

            Point cpi = new Point(pi.X + (pj.X - pi.X) * t, pi.Y + (pj.Y - pi.Y) * t);
            Point cpj = new Point();
            Point pe = new Point();
            Point pE = pi;

            for (int j = 1; j < knots.Count - 1; ++j)
            {
                pk = knots[j + 1];

                pe.X = pj.X + ((pi.X + (pk.X - pi.X) * 0.5) - pj.X) * t;
                pe.Y = pj.Y + ((pi.Y + (pk.Y - pi.Y) * 0.5) - pj.Y) * t;

                cpj.X = pj.X - (pj.X - pi.X) * t;
                cpj.Y = pj.Y - (pj.Y - pi.Y) * t;

                BezierCurve(points, resolution, pE, cpi, cpj, pe);

                cpi.X = pj.X + (pk.X - pj.X) * t;
                cpi.Y = pj.Y + (pk.Y - pj.Y) * t;
                pE = pe;
                pi = pj;
                pj = pk;
            }

            {
                cpj.X = pj.X - (pj.X - pi.X) * t;
                cpj.Y = pj.Y - (pj.Y - pi.Y) * t;

                BezierCurve(points, resolution, pE, cpi, cpj, pj);
            }
        }

        /// <summary>
        /// Calculates amd flattens an interpolating spline into a polyline points
        /// </summary>
        /// <param name="points"></param>
        /// <param name="knots"></param>
        /// <param name="resolution"></param>
        /// <param name="tension"></param>
        public static void InterpolatingSpline(IList<Point> points, double resolution, IList<Point> knots, double tension)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Flattens a Bezier curve.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="p0">Start point</param>
        /// <param name="p1">First control point.</param>
        /// <param name="p2">Second control point.</param>
        /// <param name="p3">End point.</param>
        /// <param name="resolution"></param>
        public static void BezierCurve(IList<Point> points, double resolution, Point p0, Point p1, Point p2, Point p3)
        {
            double err = Math.Abs(p0.X + p2.X - p1.X - p1.X) +
                        Math.Abs(p0.Y + p2.Y - p1.Y - p1.Y) +
                        Math.Abs(p1.X + p3.X - p2.X - p2.X) +
                        Math.Abs(p1.Y + p3.Y - p2.Y - p2.Y);

            if (err < resolution)
            {
                points.Add(p3);
            }
            else
            {
                Point p01 = new Point((p0.X + p1.X) / 2.0, (p0.Y + p1.Y) / 2.0);
                Point p12 = new Point((p1.X + p2.X) / 2.0, (p1.Y + p2.Y) / 2.0);
                Point p23 = new Point((p2.X + p3.X) / 2.0, (p2.Y + p3.Y) / 2.0);

                Point p012 = new Point((p01.X + p12.X) / 2.0, (p01.Y + p12.Y) / 2.0);
                Point p123 = new Point((p12.X + p23.X) / 2.0, (p12.Y + p23.Y) / 2.0);

                Point p0123 = new Point((p012.X + p123.X) / 2.0, (p012.Y + p123.Y) / 2.0);

                BezierCurve(points, resolution, p0, p01, p012, p0123);
                BezierCurve(points, resolution, p0123, p123, p23, p3);
            }
        }
        #endregion

        #region Spline into flattened, clipped, polyline
        /// <summary>
        /// Calculates, flattens and clips a smoothing spline into a clipper.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="viewport"></param>
        /// <param name="resolution"></param>
        /// <param name="tension"></param>
        /// <param name="knots"></param>
        public static void SmoothingSpline(Clipper points, Rect viewport, double resolution, IList<Point> knots, double tension)
        {
            double t = 0.5 * tension;
            Point pi = knots[0];
            Point pj = knots[1];
            Point pk = new Point();

            Point cpi = new Point(pi.X + (pj.X - pi.X) * t, pi.Y + (pj.Y - pi.Y) * t);
            Point cpj = new Point();
            Point pe = new Point();
            Point pE = pi;

            for (int j = 1; j < knots.Count - 1; ++j)
            {
                pk = knots[j + 1];

                pe.X = pj.X + ((pi.X + (pk.X - pi.X) * 0.5) - pj.X) * t;
                pe.Y = pj.Y + ((pi.Y + (pk.Y - pi.Y) * 0.5) - pj.Y) * t;

                cpj.X = pj.X - (pj.X - pi.X) * t;
                cpj.Y = pj.Y - (pj.Y - pi.Y) * t;

                bezierCurve(points, viewport, 1, resolution, pE, cpi, cpj, pe);

                cpi.X = pj.X + (pk.X - pj.X) * t;
                cpi.Y = pj.Y + (pk.Y - pj.Y) * t;
                pE = pe;
                pi = pj;
                pj = pk;
            }

            {
                cpj.X = pj.X - (pj.X - pi.X) * t;
                cpj.Y = pj.Y - (pj.Y - pi.Y) * t;

                bezierCurve(points, viewport, 1, resolution, pE, cpi, cpj, pj);
            }
        }

        /// <summary>
        /// Calculates, flattens and clips an interpolating spline into a clipper.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="viewport"></param>
        /// <param name="resolution"></param>
        /// <param name="tension"></param>
        /// <param name="knots"></param>
        public static void InterpolatingSpline(Clipper points, Rect viewport, double resolution, IList<Point> knots, double tension)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Flattens and clips a bezier curve.
        /// </summary>
        /// <param name="points"></param>
        /// <param name="viewport"></param>
        /// <param name="resolution"></param>
        /// <param name="p0">Start point</param>
        /// <param name="p1">First control point.</param>
        /// <param name="p2">Second control point.</param>
        /// <param name="p3">End point.</param>
        public static void BezierCurve(Clipper points, Rect viewport, double resolution, Point p0, Point p1, Point p2, Point p3)
        {
            bezierCurve(points, viewport, 1, resolution, p0, p1, p2, p3);
        }

        private static void bezierCurve(Clipper points, Rect viewport, int clip, double resolution, Point p0, Point p1, Point p2, Point p3)
        {
            double err = Math.Abs(p0.X + p2.X - p1.X - p1.X) + Math.Abs(p0.Y + p2.Y - p1.Y - p1.Y) + Math.Abs(p1.X + p3.X - p2.X - p2.X) + Math.Abs(p1.Y + p3.Y - p2.Y - p2.Y);

            if (clip == 1)
            {
                clip = err <= resolution ? clipSegment(viewport, p0, p3) : clipBezierCurve(viewport, p0, p1, p2, p3);
            }

            if (clip == -1 || err <= resolution)
            {
                points.Add(p3);
                return;
            }

            Point p01 = new Point((p0.X + p1.X) / 2.0, (p0.Y + p1.Y) / 2.0);
            Point p12 = new Point((p1.X + p2.X) / 2.0, (p1.Y + p2.Y) / 2.0);
            Point p23 = new Point((p2.X + p3.X) / 2.0, (p2.Y + p3.Y) / 2.0);

            Point p012 = new Point((p01.X + p12.X) / 2.0, (p01.Y + p12.Y) / 2.0);
            Point p123 = new Point((p12.X + p23.X) / 2.0, (p12.Y + p23.Y) / 2.0);

            Point p0123 = new Point((p012.X + p123.X) / 2.0, (p012.Y + p123.Y) / 2.0);

            bezierCurve(points, viewport, clip, resolution, p0, p01, p012, p0123);
            bezierCurve(points, viewport, clip, resolution, p0123, p123, p23, p3);
        }
        private static int clipSegment(Rect rc, Point p0, Point p1)
        {
            #region trivial inclusion and exclusion by bounds
            double xmin = Math.Min(p0.X, p1.X);
            double xmax = Math.Max(p0.X, p1.X);

            if (xmin < rc.Left && xmax < rc.Left)
            {
                return -1;
            }

            if (xmax > rc.Right && xmax > rc.Right)
            {
                return -1;
            }

            double ymin = Math.Min(p0.Y, p1.Y);
            double ymax = Math.Max(p0.Y, p1.Y);

            if (ymin < rc.Top && ymin < rc.Top)
            {
                return -1;
            }

            if (ymax > rc.Bottom && ymax > rc.Bottom)
            {
                return -1;
            }

            if (xmin > rc.Left && xmax < rc.Right && ymin > rc.Top && ymax < rc.Bottom)
            {
                return 0;
            }
            #endregion

            #region trivial clipping
            if (rc.Contains(p0) || rc.Contains(p1))
            {
                return 1;
            }
            #endregion

            #region full clipping, assuming trivial clips and culls have been performed
            if (Math.Sign(p0.Y - rc.Top) != Math.Sign(p1.Y - rc.Top))
            {
                double p = (rc.Top - p0.Y) / (p1.Y - p0.Y);
                double ix = p0.X + p * (p1.X - p0.X);

                if (ix > rc.Left && ix < rc.Right)
                {
                    return 1;
                }
            }

            if (Math.Sign(p0.Y - rc.Bottom) != Math.Sign(p1.Y - rc.Bottom))
            {
                double p = (rc.Bottom - p0.Y) / (p1.Y - p0.Y);
                double ix = p0.X + p * (p1.X - p0.X);

                if (ix > rc.Left && ix < rc.Right)
                {
                    return 1;
                }
            }

            return -1;
            #endregion
        }
        private static int clipBezierCurve(Rect rc, Point p0, Point p1, Point p2, Point p3)
        {
            #region trivial inclusion and exclusion by axis aligned bounding rectangle
            double xmin = MathTool.Min(p0.X, p1.X, p2.X, p3.X);

            if (xmin > rc.Right)
            {
                return -1;
            }

            double ymin = MathTool.Min(p0.Y, p1.Y, p2.Y, p3.Y);

            if (ymin > rc.Bottom)
            {
                return -1;
            }

            double xmax = MathTool.Max(p0.X, p1.X, p2.X, p3.X);

            if (xmax < rc.Left)
            {
                return -1;
            }

            double ymax = MathTool.Max(p0.Y, p1.Y, p2.Y, p3.Y);

            if (ymax < rc.Top)
            {
                return -1;
            }

            if (xmin >= rc.Left && ymin <= rc.Bottom && xmax <= rc.Right && ymin >= rc.Top)
            {
                return 0;
            }
            #endregion

            double p30x = p3.X - p0.X;
            double p30y = p3.Y - p0.Y;
            double s1 = p30x * (p1.Y - p0.Y) - p30y * (p1.X - p0.X);
            double s2 = p30x * (p2.Y - p0.Y) - p30y * (p2.X - p0.X);

            if (Math.Sign(s1) == Math.Sign(s2) /* control points are on the same side of the support */)
            {
                if (rc.Contains(p0) || rc.Contains(p1) || rc.Contains(p2) || rc.Contains(p3))
                {
                    if (rc.Contains(p0) && rc.Contains(p1) && rc.Contains(p2) && rc.Contains(p3))
                    {
                        return 0;   // entire convex hull is contained in bounding rect 
                    }

                    return 1;   // some points are inside, sone points are outside 
                }

                Point[] polygon = { p0, p1, p2, p3 };
                Func<int, double> X = (i) => polygon[i].X;
                Func<int, double> Y = (i) => polygon[i].Y;

                if (GeometryTool.PolygonContains(4, X, Y, new Point(rc.Left, rc.Top)) ||
                    GeometryTool.PolygonContains(4, X, Y, new Point(rc.Left, rc.Bottom)) ||
                    GeometryTool.PolygonContains(4, X, Y, new Point(rc.Right, rc.Bottom)) ||
                    GeometryTool.PolygonContains(4, X, Y, new Point(rc.Right, rc.Top)))
                {
                    return 1;       // rc intersects curve - keep on clipping
                }

                return -1;          // rc does not intersect curve
            }
            else
            {
                // need the convex hull to work out which points to test, but for the moment, just
                // assume that the whole curve is clipped


                return 1;
            }
        }
        #endregion
    }
}
