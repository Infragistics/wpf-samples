using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics.CodeAnalysis;

namespace IGExtensions.Framework.Tools
{
    
    public static partial class GeometryTool
    {
        public static bool PolygonContains(int count, Func<int, double> x, Func<int, double> y, Point point)
        {
            bool hit = false;
            double xj = x(count - 1);
            double yj = y(count - 1);

            for (int i = 0; i < count; ++i)
            {
                double xi = x(i);
                double yi = y(i);

                if (((yi > point.Y) != (yj > point.Y)) && (point.X < (xj - xi) * (point.Y - yi) / (yj - yi) + xi))
                {
                    hit = !hit;
                }

                xj = xi;
                yj = yi;
            }

            return hit;
        }
        public static Point PolygonCentroid(int count, Func<int, double> x, Func<int, double> y)
        {
            // assumes the polygon is closed
            double cx=0.0;
            double cy=0.0;
            double area=PolygonSignedArea(count, x, y);

            double xi = x(0);
            double yi = y(0);

            for (int i = 0; i < count; ++i)
            {
                double xj = x((i+1)%(count));
                double yj = y((i + 1) % (count));

                cx += (xi + xj) * (xi * yj - xj * yi);
                cy += (yi + yj) * (xi * yj - xj * yi);

                xi = xj;
                yi = yj;
            }

            return new Point(cx / (6.0 * area), cy / (6.0 * area));

        }

        public static double PolygonSignedArea(int count, Func<int, double> x, Func<int, double> y)
        {
            double a = 0.0;

            if (count > 2)
            {
                double xi = x(count - 1);
                double yi = y(count - 1);

                for (int j = 0; j < count; ++j)
                {
                    double xj = x(j);
                    double yj = y(j);

                    a += yj * xi - xj * yi;
                    xi = xj;
                    yi = yj;
                }
            }

            return 0.5 * a;
        }
        public static int PolygonOrientation(int count, Func<int, double> x, Func<int, double> y)
        {
            return Math.Sign(PolygonSignedArea(count, x, y));
        }

        public static List<int> PolygonTriangulation(int count, Func<int, double> x, Func<int, double> y, int ornt)
        {
            if (ornt == 0)
            {
                ornt = PolygonOrientation(count, x, y);
            }

            var vp = new List<int> { Capacity = count };
            var t = new List<int>();

            for (int i = 0; i < count; ++i)
            {
                vp.Add(i);
            }

            while (count > 3)
            {
                double min_dist = double.PositiveInfinity;
                int min_vert = 0;

                for (int prev = count - 2, cur = count - 1, next = 0; next < count; prev = cur, cur = next, ++next)
                {
                    int s = Math.Sign(determinant(x, y, vp[prev], vp[cur], vp[next]));

                    if (s == 0)
                    {
                        min_dist = 0.0;
                        min_vert = cur;

                        break;
                    }

                    if (s == ornt && no_interior(x, y, ornt, vp[prev], vp[cur], vp[next], count, vp))
                    {
                        double dx = x(vp[prev]) - x(vp[next]);
                        double dy = y(vp[prev]) - y(vp[next]);
                        double dist = dx * dx + dy * dy;

                        if (dist < min_dist)
                        {
                            min_dist = dist;
                            min_vert = cur;
                        }
                    }
                }

                if (min_dist == double.PositiveInfinity)
                {
                    return null;	// didn't find a triangle with no interior equator
                }

                if (min_dist > 0.0)
                {
                    int prev = min_vert == 0 ? count - 1 : min_vert - 1;
                    int next = min_vert == count - 1 ? 0 : min_vert + 1;

                    t.Add(vp[prev]);
                    t.Add(vp[min_vert]);
                    t.Add(vp[next]);
                }

                --count;

                vp.RemoveAt(min_vert);
            }

            t.Add(vp[0]);
            t.Add(vp[1]);
            t.Add(vp[2]);

            return t;
        }
        private static double determinant(Func<int, double> x, Func<int, double> y, int v1, int v2, int v3)
        {
            return (x(v2) - x(v1)) * (y(v3) - y(v1)) - (x(v3) - x(v1)) * (y(v2) - y(v1));
        }
        private static bool no_interior(Func<int, double> x, Func<int, double> y, int ornt, int prv, int cur, int nxt, int count, List<int> vp)
        {
            for (int i = 0; i < count; ++i)
            {
                int p = vp[i];	// test point

                if ((p == prv) || (p == cur) || (p == nxt))
                {
                    continue;
                }

                int s0 = Math.Sign(determinant(x, y, prv, cur, p)); if (s0 == 0) { continue; }
                int s1 = Math.Sign(determinant(x, y, cur, nxt, p)); if (s1 == 0) { continue; }
                int s2 = Math.Sign(determinant(x, y, nxt, prv, p)); if (s2 == 0) { continue; }

                if (s0 == ornt && s1 == ornt && s2 == ornt)
                {
                    return false;
                }
            }

            return true;
        }

        public static IList<int> StripCoincident(int count, Func<int, double> x, Func<int, double> y, double resolution)
        {
            IList<int> ret = new List<int>();

            for (int i = 0; i < count; )
            {
                double cx = x(i);
                double cy = y(i);

                int j = i;
                ret.Add(j);

                for (++i; i < count && MathTool.Hypot(x(j) - cx, y(j) - cy) < resolution; ++i)
                {
                    cx = (cx * (double)(i - j) + x(i)) / (double)(i - j + 1);
                    cy = (cy * (double)(i - j) + y(i)) / (double)(i - j + 1);
                }
            }

            return ret;
        }

        #region Simple Polygon Convex Hull
        /// <summary>
        /// Find the convex hull of s a simple polygon or polyline
        /// </summary>
        /// <param name="count"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static List<int> SimplePolygonHull(int count, Func<int, double> x, Func<int, double> y)
        {
            #region initialize deque with counterclockwise triangle
            int[] D = new int[2 * count + 1];
            int bot = count - 2;
            int top = bot + 3;           // initial bottom and top deque indices
            D[bot] = D[top] = 2;         // 3rd vertex is at both bot and top

            if (isLeft(x, y, 0, 1, 2) > 0)
            {
                D[bot + 1] = 0;
                D[bot + 2] = 1;          // ccw vertices are: 2,0,1,2
            }
            else
            {
                D[bot + 1] = 1;
                D[bot + 2] = 0;          // ccw vertices are: 2,1,0,2
            }
            #endregion

            #region compute the hull on deque
            for (int i = 3; i < count; i++)
            {
                if ((isLeft(x, y, D[bot], D[bot + 1], i) > 0) && (isLeft(x, y, D[top - 1], D[top], i) > 0))
                {
                    continue;         // skip an interior vertex
                }

                // get the rightmost tangent at the deque bot
                while (isLeft(x, y, D[bot], D[bot + 1], i) <= 0)
                {
                    ++bot;                // remove bot of deque
                }

                D[--bot] = i;             // insert V[i] at bot of deque

                // get the leftmost tangent at the deque top
                while (isLeft(x, y, D[top - 1], D[top], i) <= 0)
                {
                    --top;                // pop top of deque
                }

                D[++top] = i;          // push V[i] onto top of deque
            }
            #endregion

            #region save deque into return
            List<int> H = new List<int>(top - bot + 1);

            for (int h = bot; h <= top; ++h)
            {
                H.Add(D[bot + h]);
            }

            return H;
            #endregion
        }
        private static double isLeft(Func<int, double> X, Func<int, double> Y, int p0, int p1, int p2)
        {
            double x0 = X(p0);
            double y0 = Y(p0);
            double x1 = X(p1);
            double y1 = Y(p1);
            double x2 = X(p2);
            double y2 = Y(p2);

            return (x1 - x0) * (y2 - y0) - (x2 - x0) * (y1 - y0);
        }
        #endregion
    }
}
