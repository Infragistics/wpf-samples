using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;

namespace IGExtensions.Framework.Tools
{
    
    public static partial class GeometryTool
    {
        static void Wrap(int east, List<Point> points)
        {
            var output = new List<List<Point>>();

            Point pt=points[0];
            bool inside = pt.X < east;

            var current=new List<Point>();
            output.Add(current);
            current.Add(pt);

            for (int i = 1; i < points.Count; ++i)
            {
                pt = points[i];

                // reduce pt until it's less than 360 and greater than zero

                // reduction changed the value

                if (inside != pt.X > east)
                {
                    Point xsection=new Point(); // get intersection
                    
                    current.Add(xsection);

                    current = new List<Point>();
                    output.Add(current);

                    current.Add(xsection);
                    current.Add(pt);

                    inside = !inside;
                }

                current.Add(pt);
            }

            // there we go.
        }

        #region Line Flattening
        // there's some bogosity going on here which I'm pretty sure results in too
        // many points getting added
        //
        // Flatten should guarantee to add the first point, not the first and last
        // maybe it should just guarantee to add the last point
        public static List<int> Flatten(int count, Func<int, Point> XY, double resolution)
        {
            return Flatten(count, (i) => XY(i).X, (i) => XY(i).Y, resolution);
        }
        public static List<int> Flatten(int index, int count, Func<int, Point> XY, double resolution)
        {
            return Flatten(index, count, (i) => XY(i).X, (i) => XY(i).Y, resolution);
        }

        public static List<int> Flatten(int count, Func<int, double> X, Func<int, double> Y, double resolution)
        {
            List<int> ret = new List<int>();

            Flatten(ret, X, Y, 0, count - 1, MathTool.Sqr(resolution));
            ret.Add(count - 1);

            return ret;
        }
        public static List<int> Flatten(int index, int count, Func<int, double> X, Func<int, double> Y, double resolution)
        {
            List<int> ret = new List<int>();

            Flatten(ret, X, Y, index, count - 1, MathTool.Sqr(resolution));
            ret.Add(count - 1);

            return ret;
        }

        private static IList<int> Flatten(IList<int> result, Func<int, double> X, Func<int, double> Y, int b, int e, double E2)
        {
            if (b >= e)
            {
                if (b == e)
                {
                    result.Add(b);
                }

                return result;
            }

            double Xb = X(b);
            double Yb = Y(b);
            double Xe = X(e);
            double Ye = Y(e);

            int si = -1;
            double se = E2;
            double L = MathTool.Hypot2(Xe - Xb, Ye - Yb);

            if (L == 0.0)
            {
                for (int i = b + 1; i < e; ++i)
                {
                    double Xi = X(i);
                    double Yi = Y(i);

                    if (!double.IsNaN(Xi) && !double.IsNaN(Yi))
                    {
                        double err = MathTool.Hypot2(Xe - X(i), Ye - Y(i));

                        if (err >= se)
                        {
                            se = err;
                            si = i;
                        }
                    }
                }
            }
            else
            {
                double vx = Xe - Xb;
                double vy = Ye - Yb;

                for (int i = b + 1; i < e; ++i)
                {
                    double Xi = X(i);
                    double Yi = Y(i);

                    if (!double.IsNaN(Xi) && !double.IsNaN(Yi))
                    {
                        double err = double.NaN;
                        double wx = Xi - Xb;
                        double wy = Yi - Yb;

                        double c1 = vx * wx + vy * wy;

                        if (c1 <= 0.0)
                        {
                            err = MathTool.Hypot2(Xb - Xi, Yb - Yi);
                        }
                        else
                        {
                            double c2 = vx * vx + vy * vy;

                            if (c2 <= c1)
                            {
                                err = MathTool.Hypot2(Xe - Xi, Ye - Yi);
                            }
                            else
                            {
                                double p = c1 / c2;
                                err = MathTool.Hypot2(Xb + p * vx - Xi, Yb + p * vy - Yi);
                            }
                        }

                        if (err >= se)
                        {
                            se = err;
                            si = i;
                        }
                    }
                }
            }

            if (si == -1)
            {
                result.Add(b);
 //               result.Add(e);
            }
            else
            {
                // the error point cannot be the first or last point - that wouldn't make any sense

                Debug.Assert(si != b);
                Debug.Assert(si != e);

                Flatten(result, X, Y, b, si, E2);      // big error - flatten from b to si
                Flatten(result, X, Y, si, e, E2);    // big error - flatten from si+1 to e
            }

            return result;
        }
        #endregion

        // tests for D in the circumcircle of A, B, C
        public static double InCircumCircle(Point D, Point A, Point B, Point C)
        {
            double m00 = A.X - D.X; double m01 = A.Y - D.Y; double m02 = (A.X * A.X - D.X* D.X) + (A.Y * A.Y - D.Y * D.Y);
            double m10 = B.X - D.X; double m11 = B.Y - D.Y; double m12 = (B.X * B.X - D.X * D.X) + (B.Y * B.Y - D.Y * D.Y);
            double m20 = C.X - D.X; double m21 = C.Y - D.Y; double m22 = (C.X * C.X - D.X * D.X) + (C.Y * C.Y - D.Y * D.Y);

            return m00*m11*m22 + m01*m12*m20 + m02*m10*m21 - m00*m12*m21 - m01*m10*m22 - m02*m11*m20;
        }

        #region Point Triangulation
        public class Triangle
        {
            public int v0 { get; internal set; }
            public int v1 { get; internal set; }
            public int v2 { get; internal set; }
        }

        public static List<Triangle> PointsTriangulation(int count, Func<int, double> x, Func<int, double> y)
        {
            List<int> indices = new List<int>() { Capacity = count };

            #region sort points
            for (int i = 0; i < count; ++i)
            {
                indices.Add(i);
            }

            indices.Sort((a, b) => x(a) < x(b) ? -1 : x(a) > x(b) ? 1 : y(a).CompareTo(y(b)));
            #endregion

            #region point getter delegate including supervertics at negative indices
            double xmin = x(indices[0]);
            double xmax = x(indices[count - 1]);

            double ymin = y(indices[0]);
            double ymax = ymin;

            for (int i = 1; i < count; i++)
            {
                ymin = Math.Min(ymin, y(indices[i]));
                ymax = Math.Max(ymax, y(indices[i]));
            }

            double dx = xmax - xmin;
            double dy = ymax - ymin;
            double dmax = Math.Max(dx, dy);
            double xmid = (xmax + xmin) / 2;
            double ymid = (ymax + ymin) / 2;

            Point s0 = new Point(xmid - 20 * dmax, ymid - dmax);
            Point s1 = new Point(xmid, ymid + 20 * dmax);
            Point s2 = new Point(xmid + 20 * dmax, ymid - dmax);

            Func<int, Point> XY = (i) =>
            {
                if (i < count) { return new Point(x(indices[i]), y(indices[i])); }
                if (i == count) { return s0; }
                if (i == count + 1) { return s1; }

                return s2;
            };
            #endregion

            return null;
        }

        public static List<Triangle> PointsTriangulation(int count, Func<int, Point> xy, Func<bool> cancel)
        {
            List<Triangle> Triangles = new List<Triangle>();

            if (count >= 3)
            {
                List<int> indices = new List<int>() { Capacity = count };

                for (int i = 0; i < count; ++i)
                {
                    indices.Add(i);
                }

                Comparison<int> comparison = (a, b) => xy(a).X < xy(b).X ? -1 : xy(a).X > xy(b).X ? 1 : xy(a).Y.CompareTo(xy(b).Y);

                indices.Sort(comparison);

                #region add super triangle vertices
                double xmin = xy(indices[0]).X;
                double xmax = xy(indices[count - 1]).X;

                double ymin = xy(indices[0]).Y;
                double ymax = ymin;

                for (int i = 1; i < count; i++)
                {
                    ymin = Math.Min(ymin, xy(indices[i]).Y);
                    ymax = Math.Max(ymax, xy(indices[i]).Y);
                }

                double dx = xmax - xmin;
                double dy = ymax - ymin;
                double dmax = Math.Max(dx, dy);
                double xmid = (xmax + xmin) / 2;
                double ymid = (ymax + ymin) / 2;

                Point s0 = new Point(xmid - 20 * dmax, ymid - dmax);
                Point s1 = new Point(xmid, ymid + 20 * dmax);
                Point s2 = new Point(xmid + 20 * dmax, ymid - dmax);
                #endregion

                Triangle[] triangleBuffer = new Triangle[4 * count];
                bool[] complete = new bool[triangleBuffer.Length];
                HalfEdgeSet edgeBuffer = new HalfEdgeSet();
                PointTester pointTester = new PointTester();

                int triangleCount = 0;

                #region add super triangle and initial triangle buffer
                triangleBuffer[0] = new Triangle() { v0 = count, v1 = count + 1, v2 = count + 2 };
                complete[0] = false;

                for (int i = 1; i < triangleBuffer.Length; ++i)
                {
                    triangleBuffer[i] = new Triangle();
                    complete[i] = false;
                };

                ++triangleCount;
                #endregion

                #region point getter delegate
                Func<int, Point> XY = (i) =>
                {
                    if (i < count) { return xy(indices[i]); }
                    if (i == count) { return s0; }
                    if (i == count + 1) { return s1; }

                    return s2;
                };
                #endregion

                #region triangulate
                for (int i = 0; i < count; ++i)
                {
                    if (cancel != null && cancel())
                    {
                        return null;
                    }

                    edgeBuffer.Clear();

                    for (int j = 0; j < triangleCount; ++j)
                    {
                        if (complete[j])
                        {
                            continue;
                        }

                        pointTester.Test(XY(i), XY(triangleBuffer[j].v0), XY(triangleBuffer[j].v1), XY(triangleBuffer[j].v2));

                        complete[j] = pointTester.Complete;

                        if (pointTester.Inside)
                        {
                            HalfEdge e;

                            e = new HalfEdge(triangleBuffer[j].v0, triangleBuffer[j].v1);
                            if (edgeBuffer.Contains(e)) { edgeBuffer.Remove(e); } else { edgeBuffer.Add(e); }

                            e = new HalfEdge(triangleBuffer[j].v1, triangleBuffer[j].v2);
                            if (edgeBuffer.Contains(e)) { edgeBuffer.Remove(e); } else { edgeBuffer.Add(e); }

                            e = new HalfEdge(triangleBuffer[j].v2, triangleBuffer[j].v0);
                            if (edgeBuffer.Contains(e)) { edgeBuffer.Remove(e); } else { edgeBuffer.Add(e); }

                            triangleBuffer[j].v0 = triangleBuffer[triangleCount - 1].v0;
                            triangleBuffer[j].v1 = triangleBuffer[triangleCount - 1].v1;
                            triangleBuffer[j].v2 = triangleBuffer[triangleCount - 1].v2;

                            complete[j] = complete[triangleCount - 1];

                            --triangleCount;
                            --j;
                        }
                    }

                    // (re)create triangles

                    foreach (HalfEdge edge in edgeBuffer)
                    {
                        triangleBuffer[triangleCount].v0 = edge.B;
                        triangleBuffer[triangleCount].v1 = edge.E;
                        triangleBuffer[triangleCount].v2 = i;
                        complete[triangleCount] = false;

                        ++triangleCount;
                    }
                }
                #endregion

                #region save permanent mesh
                for (int i = 0; i < triangleCount; ++i)
                {
                    if (triangleBuffer[i].v0 < count && triangleBuffer[i].v1 < count && triangleBuffer[i].v2 < count)
                    {
                        Triangles.Add(new Triangle() { v0 = indices[triangleBuffer[i].v0], v1 = indices[triangleBuffer[i].v1], v2 = indices[triangleBuffer[i].v2] });//triangleBuffer[i]);
                    }
                }
                #endregion
            }

            return Triangles;
        }

        /// <summary>
        /// A directed half-edge segment defined by two vertex codes.
        /// </summary>
        private class HalfEdge
        {
            /// <summary>
            /// HalfEdge constructor.
            /// </summary>
            public HalfEdge(int b, int e)
            {
                this.B = b;
                this.E = e;
            }
            /// <summary>
            /// B.
            /// </summary>
            public int B { get; set; }
            /// <summary>
            /// resolution.
            /// </summary>
            public int E { get; set; }
        }

        /// <summary>
        /// Represents an unordered set of half-edges.
        /// </summary>
        /// <remarks>
        /// The set may not contain two half-edges which form together form a full edge, and it is an
        /// error to attempt to insert such a pair.
        /// </remarks>
        private class HalfEdgeSet : IEnumerable<HalfEdge>
        {
            /// <summary>
            /// Returns an enumerator that iterates through a collection. 
            /// </summary>
            /// <returns>An IEnumerator object that can be used to iterate through the collection.</returns>
            public IEnumerator<HalfEdge> GetEnumerator()
            {
                return edges.Keys.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return this.GetEnumerator(); }

            /// <summary>
            /// Adds a HalfEdge to the set.
            /// </summary>
            /// <param name="edge">The HalfEdge to add.</param>
            public void Add(HalfEdge edge)
            {
                edges.Add(edge, null);
            }
            /// <summary>
            /// Removes a HalfEdge from the set.
            /// </summary>
            /// <param name="edge">The HalfEdge to remove from the set.</param>
            public void Remove(HalfEdge edge)
            {
                edges.Remove(edge);
            }

            /// <summary>
            /// Clears the set.
            /// </summary>
            public void Clear()
            {
                edges.Clear();
            }

            /// <summary>
            /// Count of HalfEdges in the set.
            /// </summary>
            public int Count
            {
                get { return edges.Count; }
            }

            /// <summary>
            /// Determines whether or not the given HalfEdge exists in the set.
            /// </summary>
            /// <param name="edge">The HalfEdge under observation.</param>
            /// <returns>True if the set contains the HalfEdge, otherwise False.</returns>
            public bool Contains(HalfEdge edge)
            {
                return edges.ContainsKey(edge);
            }

            private class EdgeComparer : IEqualityComparer<HalfEdge>
            {
                public bool Equals(HalfEdge e1, HalfEdge e2)
                {
                    return (e1.B == e2.B && e1.E == e2.E) || (e1.B == e2.E && e1.E == e2.B);
                }

                public int GetHashCode(HalfEdge e1)
                {
                    return 65536 * Math.Max(e1.B, e1.E) + Math.Min(e1.B, e1.E);
                }
            }

            private Dictionary<HalfEdge, object> edges = new Dictionary<HalfEdge, object>(new EdgeComparer());
        }

        private class PointTester
        {
            public bool Test(Point P, Point P1, Point P2, Point P3)
            {
                double fabsy1y2 = Math.Abs(P1.Y - P2.Y);
                double fabsy2y3 = Math.Abs(P2.Y - P3.Y);
                double xc = 0;
                double yc = 0;

                if (fabsy1y2 == 0.0 && fabsy2y3 == 0.0)
                {
                    return false;   // test failed
                }

                if (fabsy1y2 == 0 && fabsy2y3 != 0)
                {
                    xc = (P2.X + P1.X) / 2.0;
                    yc = (-(P3.X - P2.X) / (P3.Y - P2.Y)) * (xc - ((P2.X + P3.X) / 2.0)) + ((P2.Y + P3.Y) / 2.0);
                }

                if (fabsy1y2 != 0 && fabsy2y3 == 0.0)
                {
                    xc = (P3.X + P2.X) / 2.0;
                    yc = (-(P2.X - P1.X) / (P2.Y - P1.Y)) * (xc - ((P1.X + P2.X) / 2.0)) + ((P1.Y + P2.Y) / 2.0);
                }

                if (fabsy1y2 != 0 && fabsy2y3 != 0.0)
                {
                    double m1 = -(P2.X - P1.X) / (P2.Y - P1.Y);
                    double m2 = -(P3.X - P2.X) / (P3.Y - P2.Y);
                    double mx1 = (P1.X + P2.X) / 2.0;
                    double mx2 = (P2.X + P3.X) / 2.0;
                    double my1 = (P1.Y + P2.Y) / 2.0;
                    double my2 = (P2.Y + P3.Y) / 2.0;

                    xc = (m1 * mx1 - m2 * mx2 + my2 - my1) / (m1 - m2);
                    yc = fabsy1y2 > fabsy2y3 ? m1 * (xc - mx1) + my1 : m2 * (xc - mx2) + my2;
                }

                double dx = P2.X - xc;
                double dy = P2.Y - yc;
                double rsqr = dx * dx + dy * dy;

                dx = P.X - xc;
                dy = P.Y - yc;
                double drsqr = dx * dx + dy * dy;

                Inside = drsqr <= rsqr;
                Complete = xc < P.X && ((P.X - xc) * (P.X - xc)) > rsqr;

                return true;
            }

            public bool Complete { get; private set; }
            public bool Inside { get; set; }
        }
        #endregion

        private static double Distance2(Point p0, Point p1)
        {
            return MathTool.Sqr(p1.X - p0.X) + MathTool.Sqr(p1.Y - p0.Y);
        }

        #region Convex Hull
        /// <summary>
        /// Find the convex hull of a point cloud using "Gift-wrap" algorithm 
        /// </summary>
        /// <remarks>
        /// Runs in O(N*S) time where S is number of sides of resulting polygon.
        /// Worst case: point cloud is all vertices of convex polygon: O(N^2).
        /// There may be faster algorithms to do this, should you need one -
        /// this is just the simplest. You can get O(N log N) expected time if you
        /// try, I think, and O(N) if you restrict inputs to simple polygons.
        /// Returns null if number of vertices passed is less than 3.
        /// Results should be passed through convex decomposition afterwards
        /// to ensure that each shape has few enough points to be used in Box2d.
        /// <para>May be buggy with colinear points on hull.</para>
        /// <para>From Eric Jordan's convex decomposition library (box2D rev 32)</para>
        /// </remarks>
        /// <param name="vertices">The vertices.</param>
        /// <returns>List of vertex indices</returns>
        public static List<int> PointsConvexHull(IList<Point> vertices)
        {
            List<int> edgeList = new List<int>();

            if (vertices.Count < 3)
            {
                for (int i = 0; i < vertices.Count; ++i)
                {
                    edgeList.Add(i);
                }

                return edgeList;
            }

            double minY = double.MaxValue;
            int minYIndex = vertices.Count;

            for (int i = 0; i < vertices.Count; ++i)
            {
                if (vertices[i].Y < minY)
                {
                    minY = vertices[i].Y;
                    minYIndex = i;
                }
            }

            int startIndex = minYIndex;
            int winIndex = -1;
            double dx = -1.0f;
            double dy = 0.0f;

            while (winIndex != minYIndex)
            {
                double maxDot = -2.0f;
                double nrm;

                for (int i = 0; i < vertices.Count; ++i)
                {
                    if (i != startIndex)
                    {

                        double newdx = vertices[i].X - vertices[startIndex].X;
                        double newdy = vertices[i].Y - vertices[startIndex].Y;

                        nrm = (float)Math.Sqrt(newdx * newdx + newdy * newdy);
                        nrm = (nrm == 0.0f) ? 1.0f : nrm;
                        newdx /= nrm;
                        newdy /= nrm;

                        double newDot = newdx * dx + newdy * dy;

                        if (newDot > maxDot)
                        {
                            maxDot = newDot;
                            winIndex = i;
                        }
                    }

                }

                edgeList.Add(winIndex);

                dx = vertices[winIndex].X - vertices[startIndex].X;
                dy = vertices[winIndex].Y - vertices[startIndex].Y;
                nrm = (float)Math.Sqrt(dx * dx + dy * dy);
                nrm = (nrm == 0.0f) ? 1.0f : nrm;
                dx /= nrm;
                dy /= nrm;
                startIndex = winIndex;
            }

            return edgeList;
        }
        #endregion
    }
}
