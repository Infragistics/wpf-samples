using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows;

namespace IGExtensions.Framework.Tools
{
    public static partial class GeometryTool
    {
        public class ListPoint
        {
            public ListPoint(int suppliedCount, Func<int, Point> suppliedPoints)
            {
                count = suppliedCount;
                points = suppliedPoints;
            }

            public int Count
            {
                get { return count; }
            }
            public Point this[int index]
            {
                get { return points(index); }
            }

            private int count;
            private Func<int, Point> points;
        }

        public class Triad
        {
            public int a, b, c;
            public int ab, bc, ac;  // adjacent edges index to neighbouring triangle.

            // Position and radius squared of circumcircle
            public double circumcircleR2, circumcircleX, circumcircleY;

            public Triad(int x, int y, int z)
            {
                a = x; b = y; c = z; ab = -1; bc = -1; ac = -1;
                circumcircleR2 = -1; x = 0; y = 0;
            }

            public void Initialize(int a, int b, int c, int ab, int bc, int ac, ListPoint points)
            {
                this.a = a;
                this.b = b;
                this.c = c;
                this.ab = ab;
                this.bc = bc;
                this.ac = ac;

                FindCircumcirclePrecisely(points);
            }

            /// <summary>
            /// If current orientation is not clockwise, swap b<->c
            /// </summary>
            public void MakeClockwise(ListPoint points)
            {
                double centroidX = (points[a].X + points[b].X + points[c].X) / 3.0f;
                double centroidY = (points[a].Y + points[b].Y + points[c].Y) / 3.0f;

                double dr0 = points[a].X - centroidX, dc0 = points[a].Y - centroidY;
                double dx01 = points[b].X - points[a].X, dy01 = points[b].Y - points[a].Y;

                double df = -dx01 * dc0 + dy01 * dr0;

                if (df > 0)
                {
                    // Need to swap vertices b<->c and edges ab<->bc
                    int t = b;
                    b = c;
                    c = t;

                    t = ab;
                    ab = ac;
                    ac = t;
                }
            }

            /// <summary>
            /// Find location and radius ^2 of the circumcircle (through all 3 points)
            /// This is the most critical routine in the entire set of code.  It must
            /// be numerically stable when the points are nearly collinear.
            /// </summary>
            public bool FindCircumcirclePrecisely(ListPoint points)
            {
                // Use coordinates relative to point `a' of the triangle
                Point pa = points[a], pb = points[b], pc = points[c];

                double xba = pb.X - pa.X;
                double yba = pb.Y - pa.Y;
                double xca = pc.X - pa.X;
                double yca = pc.Y - pa.Y;

                // Squares of lengths of the edges incident to `a'
                double balength = xba * xba + yba * yba;
                double calength = xca * xca + yca * yca;

                // Calculate the denominator of the formulae. 
                double D = xba * yca - yba * xca;
                if (D == 0)
                {
                    circumcircleX = 0;
                    circumcircleY = 0;
                    circumcircleR2 = -1;
                    return false;
                }

                double denominator = 0.5 / D;

                // Calculate offset (from pa) of circumcenter
                double xC = (yca * balength - yba * calength) * denominator;
                double yC = (xba * calength - xca * balength) * denominator;

                double radius2 = xC * xC + yC * yC;
                if ((radius2 > 1e10 * balength || radius2 > 1e10 * calength))
                {
                    circumcircleX = 0;
                    circumcircleY = 0;
                    circumcircleR2 = -1;
                    return false;
                }

                circumcircleR2 = (double)radius2;
                circumcircleX = (double)(pa.X + xC);
                circumcircleY = (double)(pa.Y + yC);

                return true;
            }

            /// <summary>
            /// Return true iff Point p is inside the circumcircle of this triangle
            /// </summary>
            public bool InsideCircumcircle(Point p)
            {
                double dx = circumcircleX - p.X;
                double dy = circumcircleY - p.Y;
                double r2 = dx * dx + dy * dy;
                return r2 < circumcircleR2;
            }

            /// <summary>
            /// Change any adjacent triangle index that matches fromIndex, to toIndex
            /// </summary>
            public void ChangeAdjacentIndex(int fromIndex, int toIndex)
            {
                if (ab == fromIndex)
                    ab = toIndex;
                else if (bc == fromIndex)
                    bc = toIndex;
                else if (ac == fromIndex)
                    ac = toIndex;
                else
                    Debug.Assert(false);
            }

            /// <summary>
            /// Determine which edge matches the triangleIndex, then which vertex the vertexIndex
            /// Set the indices of the opposite vertex, left and right edges accordingly
            /// </summary>
            public void FindAdjacency(int vertexIndex, int triangleIndex, out int indexOpposite, out int indexLeft, out int indexRight)
            {
                if (ab == triangleIndex)
                {
                    indexOpposite = c;

                    if (vertexIndex == a)
                    {
                        indexLeft = ac;
                        indexRight = bc;
                    }
                    else
                    {
                        indexLeft = bc;
                        indexRight = ac;
                    }
                }
                else if (ac == triangleIndex)
                {
                    indexOpposite = b;

                    if (vertexIndex == a)
                    {
                        indexLeft = ab;
                        indexRight = bc;
                    }
                    else
                    {
                        indexLeft = bc;
                        indexRight = ab;
                    }
                }
                else if (bc == triangleIndex)
                {
                    indexOpposite = a;

                    if (vertexIndex == b)
                    {
                        indexLeft = ab;
                        indexRight = ac;
                    }
                    else
                    {
                        indexLeft = ac;
                        indexRight = ab;
                    }
                }
                else
                {
                    Debug.Assert(false);
                    indexOpposite = indexLeft = indexRight = 0;
                }
            }

            public override string ToString()
            {
                return string.Format("Triad vertices {0} {1} {2} ; edges {3} {4} {5}", a, b, c, ab, ac, bc);
            }
        }

        public class HullPoint
        {
            public int pointsIndex;
            public int triadIndex;
            public double X;
            public double Y;

            public HullPoint(ListPoint points, int pointIndex)
            {
                X = points[pointIndex].X;
                Y = points[pointIndex].Y;
                pointsIndex = pointIndex;
                triadIndex = 0;
            }
        }

        /// <summary>
        /// Hull represents a list of vertices in the convex hull, and keeps track of
        /// their indices (into the associated points list) and triads
        /// </summary>
        public class Hull : List<HullPoint>
        {
            private int NextIndex(int index)
            {
                if (index == Count - 1)
                    return 0;
                else
                    return index + 1;
            }

            /// <summary>
            /// Return vector from the hull point at index to next point
            /// </summary>
            public void VectorToNext(int index, out double dx, out double dy)
            {
                HullPoint et = this[index], en = this[NextIndex(index)];

                dx = en.X - et.X;
                dy = en.Y - et.Y;
            }

            /// <summary>
            /// Return whether the hull vertex at index is visible from the supplied coordinates
            /// </summary>
            public bool EdgeVisibleFrom(int index, double dx, double dy)
            {
                double idx, idy;
                VectorToNext(index, out idx, out idy);

                double crossProduct = -dy * idx + dx * idy;
                return crossProduct < 0;
            }

            /// <summary>
            /// Return whether the hull vertex at index is visible from the point
            /// </summary>
            public bool EdgeVisibleFrom(int index, HullPoint point)
            {
                double idx, idy;
                VectorToNext(index, out idx, out idy);

                double dx = point.X - this[index].X;
                double dy = point.Y - this[index].Y;

                double crossProduct = -dy * idx + dx * idy;
                return crossProduct < 0;
            }

            /// <summary>
            /// Return whether the hull vertex at index is visible from the point
            /// </summary>
            public bool EdgeVisibleFrom(int index, Point point)
            {
                double idx, idy;
                VectorToNext(index, out idx, out idy);

                double dx = point.X - this[index].X;
                double dy = point.Y - this[index].Y;

                double crossProduct = -dy * idx + dx * idy;
                return crossProduct < 0;
            }
        }

        public class Triangulator
        {
            public Triangulator()
            {
            }

            private static double Distance2To(Point thisOne, Point other)
            {
                double dx = thisOne.X - other.X;
                double dy = thisOne.Y - other.Y;
                return dx * dx + dy * dy;
            }
            private static double DistanceTo(Point thisOne, Point other)
            {
                return (double)Math.Sqrt(Distance2To(thisOne, other));
            }

            /// <summary>
            /// Does a bunch of stuff, including creating the convex hull and initial poor, but valid, triangulation
            /// </summary>
            /// <param name="points"></param>
            /// <param name="hull"></param>
            /// <param name="triads"></param>
            /// <param name="rejectDuplicatePoints"></param>
            /// <param name="hullOnly"></param>
            private static void Analyse(ListPoint points, Hull hull, List<Triad> triads, bool rejectDuplicatePoints, bool hullOnly)
            {
                if (points.Count < 3)
                    throw new ArgumentException("Number of points supplied must be >= 3");

                //this.points = suppliedPoints;
                int nump = points.Count;

                double[] distance2ToCentre = new double[nump];
                int[] sortedIndices = new int[nump];

                // Choose first point as the seed
                for (int k = 0; k < nump; k++)
                {
                    distance2ToCentre[k] = Distance2To(points[0], points[k]);
                    sortedIndices[k] = k;
                }

                // Sort by distance to seed point
                Array.Sort(distance2ToCentre, sortedIndices);

                // Duplicates are more efficiently rejected now we have sorted the vertices
                if (rejectDuplicatePoints)
                {
                    // Search backwards so each removal is independent of any other
                    for (int k = nump - 2; k >= 0; k--)
                    {
                        // If the points are identical then their distances will be the same,
                        // so they will be adjacent in the sorted list
                        if ((points[sortedIndices[k]].X == points[sortedIndices[k + 1]].X) &&
                            (points[sortedIndices[k]].Y == points[sortedIndices[k + 1]].Y))
                        {
                            // Duplicates are expected to be rare, so this is not particularly efficient
                            Array.Copy(sortedIndices, k + 2, sortedIndices, k + 1, nump - k - 2);
                            Array.Copy(distance2ToCentre, k + 2, distance2ToCentre, k + 1, nump - k - 2);
                            nump--;
                        }
                    }
                }

                Debug.WriteLine((points.Count - nump).ToString() + " duplicate points rejected");

                if (nump < 3)
                    throw new ArgumentException("Number of unique points supplied must be >= 3");

                int mid = -1;
                double romin2 = double.MaxValue, circumCentreX = 0, circumCentreY = 0;

                // Find the point which, with the first two points, creates the triangle with the smallest circumcircle
                Triad tri = new Triad(sortedIndices[0], sortedIndices[1], 2);
                for (int kc = 2; kc < nump; kc++)
                {
                    tri.c = sortedIndices[kc];
                    if (tri.FindCircumcirclePrecisely(points) && tri.circumcircleR2 < romin2)
                    {
                        mid = kc;
                        // Centre of the circumcentre of the seed triangle
                        romin2 = tri.circumcircleR2;
                        circumCentreX = tri.circumcircleX;
                        circumCentreY = tri.circumcircleY;
                    }
                    else if (romin2 * 4 < distance2ToCentre[kc])
                        break;
                }

                // Change the indices, if necessary, to make the 2th point produce the smallest circumcircle with the 0th and 1th
                if (mid != 2)
                {
                    int indexMid = sortedIndices[mid];
                    double distance2Mid = distance2ToCentre[mid];

                    Array.Copy(sortedIndices, 2, sortedIndices, 3, mid - 2);
                    Array.Copy(distance2ToCentre, 2, distance2ToCentre, 3, mid - 2);
                    sortedIndices[2] = indexMid;
                    distance2ToCentre[2] = distance2Mid;
                }

                // These three points are our seed triangle
                tri.c = sortedIndices[2];
                tri.MakeClockwise(points);
                tri.FindCircumcirclePrecisely(points);

                // Add tri as the first triad, and the three points to the convex hull
                triads.Add(tri);
                hull.Add(new HullPoint(points, tri.a));
                hull.Add(new HullPoint(points, tri.b));
                hull.Add(new HullPoint(points, tri.c));

                // Sort the remainder according to their distance from its centroid
                // Re-measure the points' distances from the centre of the circumcircle
                Point centre = new Point(circumCentreX, circumCentreY);
                for (int k = 3; k < nump; k++)
                    distance2ToCentre[k] = Distance2To(points[sortedIndices[k]], centre);

                // Sort the _other_ points in order of distance to circumcentre
                Array.Sort(distance2ToCentre, sortedIndices, 3, nump - 3);

                // Add new points into hull (removing obscured ones from the chain)
                // and creating triangles....
                int numt = 0;
                for (int k = 3; k < nump; k++)
                {
                    int pointsIndex = sortedIndices[k];
                    HullPoint ptx = new HullPoint(points, pointsIndex);

                    double dx = ptx.X - hull[0].X, dy = ptx.Y - hull[0].Y;  // outwards pointing from hull[0] to pt.

                    int numh = hull.Count, numh_old = numh;
                    List<int> pidx = new List<int>(), tridx = new List<int>();
                    int hidx;  // new hull point location within hull.....

                    if (hull.EdgeVisibleFrom(0, dx, dy))
                    {
                        // starting with a visible hull facet !!!
                        int e2 = numh;
                        hidx = 0;

                        // check to see if segment numh is also visible
                        if (hull.EdgeVisibleFrom(numh - 1, dx, dy))
                        {
                            // visible.
                            pidx.Add(hull[numh - 1].pointsIndex);
                            tridx.Add(hull[numh - 1].triadIndex);

                            for (int h = 0; h < numh - 1; h++)
                            {
                                // if segment h is visible delete h
                                pidx.Add(hull[h].pointsIndex);
                                tridx.Add(hull[h].triadIndex);
                                if (hull.EdgeVisibleFrom(h, ptx))
                                {
                                    hull.RemoveAt(h);
                                    h--;
                                    numh--;
                                }
                                else
                                {
                                    // quit on invisibility
                                    hull.Insert(0, ptx);
                                    numh++;
                                    break;
                                }
                            }
                            // look backwards through the hull structure
                            for (int h = numh - 2; h > 0; h--)
                            {
                                // if segment h is visible delete h + 1
                                if (hull.EdgeVisibleFrom(h, ptx))
                                {
                                    pidx.Insert(0, hull[h].pointsIndex);
                                    tridx.Insert(0, hull[h].triadIndex);
                                    hull.RemoveAt(h + 1);  // erase end of chain
                                }
                                else
                                    break; // quit on invisibility
                            }
                        }
                        else
                        {
                            hidx = 1;  // keep pt hull[0]
                            tridx.Add(hull[0].triadIndex);
                            pidx.Add(hull[0].pointsIndex);

                            for (int h = 1; h < numh; h++)
                            {
                                // if segment h is visible delete h  
                                pidx.Add(hull[h].pointsIndex);
                                tridx.Add(hull[h].triadIndex);
                                if (hull.EdgeVisibleFrom(h, ptx))
                                {                     // visible
                                    hull.RemoveAt(h);
                                    h--;
                                    numh--;
                                }
                                else
                                {
                                    // quit on invisibility
                                    hull.Insert(h, ptx);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        int e1 = -1, e2 = numh;
                        for (int h = 1; h < numh; h++)
                        {
                            if (hull.EdgeVisibleFrom(h, ptx))
                            {
                                if (e1 < 0)
                                    e1 = h;  // first visible
                            }
                            else
                            {
                                if (e1 > 0)
                                {
                                    // first invisible segment.
                                    e2 = h;
                                    break;
                                }
                            }
                        }

                        // triangle pidx starts at e1 and ends at e2 (inclusive).	
                        if (e2 < numh)
                        {
                            for (int e = e1; e <= e2; e++)
                            {
                                pidx.Add(hull[e].pointsIndex);
                                tridx.Add(hull[e].triadIndex);
                            }
                        }
                        else
                        {
                            for (int e = e1; e < e2; e++)
                            {
                                pidx.Add(hull[e].pointsIndex);
                                tridx.Add(hull[e].triadIndex);   // there are only n-1 triangles from n hull pts.
                            }
                            pidx.Add(hull[0].pointsIndex);
                        }

                        // erase elements e1+1 : e2-1 inclusive.
                        if (e1 < e2 - 1)
                            hull.RemoveRange(e1 + 1, e2 - e1 - 1);

                        // insert ptx at location e1+1.
                        hull.Insert(e1 + 1, ptx);
                        hidx = e1 + 1;
                    }

                    if (!hullOnly)
                    {

                        int a = pointsIndex, T0;

                        int npx = pidx.Count - 1;
                        numt = triads.Count;
                        T0 = numt;

                        for (int p = 0; p < npx; p++)
                        {
                            Triad trx = new Triad(a, pidx[p], pidx[p + 1]);
                            trx.FindCircumcirclePrecisely(points);

                            trx.bc = tridx[p];
                            if (p > 0)
                                trx.ab = numt - 1;
                            trx.ac = numt + 1;

                            // index back into the triads.
                            Triad txx = triads[tridx[p]];
                            if ((trx.b == txx.a && trx.c == txx.b) | (trx.b == txx.b && trx.c == txx.a))
                                txx.ab = numt;
                            else if ((trx.b == txx.a && trx.c == txx.c) | (trx.b == txx.c && trx.c == txx.a))
                                txx.ac = numt;
                            else if ((trx.b == txx.b && trx.c == txx.c) | (trx.b == txx.c && trx.c == txx.b))
                                txx.bc = numt;

                            triads.Add(trx);
                            numt++;
                        }
                        // Last edge is on the outside
                        triads[numt - 1].ac = -1;

                        hull[hidx].triadIndex = numt - 1;
                        if (hidx > 0)
                            hull[hidx - 1].triadIndex = T0;
                        else
                        {
                            numh = hull.Count;
                            hull[numh - 1].triadIndex = T0;
                        }
                    }
                }
            }

            #region Convex Hull
            /// <summary>
            /// Return the convex hull of the supplied points,
            /// Don't check for duplicate points
            /// </summary>
            /// <param name="points">List of 2D vertices</param>
            /// <returns></returns>
            public static List<int> ConvexHull(ListPoint points)
            {
                return ConvexHull(points, false);
            }

            /// <summary>
            /// Return the convex hull of the supplied points,
            /// Optionally check for duplicate points
            /// </summary>
            /// <param name="points">List of 2D vertices</param>
            /// <param name="rejectDuplicatePoints">Whether to omit duplicated points</param>
            /// <returns></returns>
            public static List<int> ConvexHull(ListPoint points, bool rejectDuplicatePoints)
            {
                Hull hull = new Hull();
                List<Triad> triads = new List<Triad>();
                List<int> ret = new List<int>();

                Analyse(points, hull, triads, rejectDuplicatePoints, true);

                foreach (HullPoint hv in hull)
                {
                    ret.Add(hv.pointsIndex);
                }

                return ret;
            }
            #endregion

            #region Triangulation
            /// <summary>
            /// Return the Delaunay triangulation of the supplied points
            /// Don't check for duplicate points
            /// </summary>
            /// <param name="points">List of 2D vertices</param>
            /// <returns>Triads specifying the triangulation</returns>
            public static List<Triad> Triangulation(ListPoint points)
            {
                return Triangulation(points, false);
            }

            /// <summary>
            /// Return the Delaunay triangulation of the supplied points
            /// Optionally check for duplicate points
            /// </summary>
            /// <param name="points">List of 2D vertices</param>
            /// <param name="rejectDuplicatePoints">Whether to omit duplicated points</param>
            /// <returns></returns>
            public static List<Triad> Triangulation(ListPoint points, bool rejectDuplicatePoints)
            {
                List<Triad> triads = new List<Triad>();
                Hull hull = new Hull();

                Analyse(points, hull, triads, rejectDuplicatePoints, false);

//return triads;

                // Now, need to flip any pairs of adjacent triangles not satisfying
                // the Delaunay criterion
                int numt = triads.Count;
                bool[] idsA = new bool[numt];
                bool[] idsB = new bool[numt];

                // We maintain a "list" of the triangles we've flipped in order to propogate any
                // consequent changes
                // When the number of changes is large, this is best maintained as a vector of bools
                // When the number becomes small, it's best maintained as a set
                // We switch between these regimes as the number flipped decreases
                int flipped = FlipTriangles(points, triads, idsA);

                int iterations = 1;
                while (flipped > (int)(fraction * (double)numt))
                {
                    if ((iterations & 1) == 1)
                        flipped = FlipTriangles(points, triads, idsA, idsB);
                    else
                        flipped = FlipTriangles(points, triads, idsB, idsA);

                    iterations++;
                }

                Set<int> idSetA = new Set<int>(), idSetB = new Set<int>();
                flipped = FlipTriangles(points, triads, ((iterations & 1) == 1) ? idsA : idsB, idSetA);

                iterations = 1;
                while (flipped > 0)
                {
                    if ((iterations & 1) == 1)
                        flipped = FlipTriangles(points, triads, idSetA, idSetB);
                    else
                        flipped = FlipTriangles(points, triads, idSetB, idSetA);

                    iterations++;
                }

                return triads;
            }

            public static double fraction = 0.3f;

            /// <summary>
            /// Test the triad against its 3 neighbours and flip it with any neighbour whose opposite point
            /// is inside the circumcircle of the triad
            /// </summary>
            /// <param name="points"></param>
            /// <param name="triads">The triads</param>
            /// <param name="triadIndexToTest">The index of the triad to test</param>
            /// <param name="triadIndexFlipped">Index of adjacent triangle it was flipped with (if any)</param>
            /// <returns>true iff the triad was flipped with any of its neighbours</returns>
            private static bool FlipTriangle(ListPoint points, List<Triad> triads, int triadIndexToTest, out int triadIndexFlipped)
            {
                int oppositePoint = 0, edge1, edge2, edge3 = 0, edge4 = 0;
                triadIndexFlipped = 0;

                Triad tri = triads[triadIndexToTest];
                // test all 3 neighbours of tri 

                if (tri.bc >= 0)
                {
                    triadIndexFlipped = tri.bc;
                    Triad t2 = triads[triadIndexFlipped];
                    // find relative orientation (shared limb).
                    t2.FindAdjacency(tri.b, triadIndexToTest, out oppositePoint, out edge3, out edge4);
                    if (tri.InsideCircumcircle(points[oppositePoint]))
                    {  // not valid in the Delaunay sense.
                        edge1 = tri.ab;
                        edge2 = tri.ac;
                        if (edge1 != edge3 && edge2 != edge4)
                        {
                            int tria = tri.a, trib = tri.b, tric = tri.c;
                            tri.Initialize(tria, trib, oppositePoint, edge1, edge3, triadIndexFlipped, points);
                            t2.Initialize(tria, tric, oppositePoint, edge2, edge4, triadIndexToTest, points);

                            // change knock on triangle labels.
                            if (edge3 >= 0)
                                triads[edge3].ChangeAdjacentIndex(triadIndexFlipped, triadIndexToTest);
                            if (edge2 >= 0)
                                triads[edge2].ChangeAdjacentIndex(triadIndexToTest, triadIndexFlipped);
                            return true;
                        }
                    }
                }


                if (tri.ab >= 0)
                {
                    triadIndexFlipped = tri.ab;
                    Triad t2 = triads[triadIndexFlipped];
                    // find relative orientation (shared limb).
                    t2.FindAdjacency(tri.a, triadIndexToTest, out oppositePoint, out edge3, out edge4);
                    if (tri.InsideCircumcircle(points[oppositePoint]))
                    {  // not valid in the Delaunay sense.
                        edge1 = tri.ac;
                        edge2 = tri.bc;
                        if (edge1 != edge3 && edge2 != edge4)
                        {
                            int tria = tri.a, trib = tri.b, tric = tri.c;
                            tri.Initialize(tric, tria, oppositePoint, edge1, edge3, triadIndexFlipped, points);
                            t2.Initialize(tric, trib, oppositePoint, edge2, edge4, triadIndexToTest, points);

                            // change knock on triangle labels.
                            if (edge3 >= 0)
                                triads[edge3].ChangeAdjacentIndex(triadIndexFlipped, triadIndexToTest);
                            if (edge2 >= 0)
                                triads[edge2].ChangeAdjacentIndex(triadIndexToTest, triadIndexFlipped);
                            return true;
                        }
                    }
                }

                if (tri.ac >= 0)
                {
                    triadIndexFlipped = tri.ac;
                    Triad t2 = triads[triadIndexFlipped];
                    // find relative orientation (shared limb).
                    t2.FindAdjacency(tri.a, triadIndexToTest, out oppositePoint, out edge3, out edge4);
                    if (tri.InsideCircumcircle(points[oppositePoint]))
                    {  // not valid in the Delaunay sense.
                        edge1 = tri.ab;   // .ac shared limb
                        edge2 = tri.bc;
                        if (edge1 != edge3 && edge2 != edge4)
                        {
                            int tria = tri.a, trib = tri.b, tric = tri.c;
                            tri.Initialize(trib, tria, oppositePoint, edge1, edge3, triadIndexFlipped, points);
                            t2.Initialize(trib, tric, oppositePoint, edge2, edge4, triadIndexToTest, points);

                            // change knock on triangle labels.
                            if (edge3 >= 0)
                                triads[edge3].ChangeAdjacentIndex(triadIndexFlipped, triadIndexToTest);
                            if (edge2 >= 0)
                                triads[edge2].ChangeAdjacentIndex(triadIndexToTest, triadIndexFlipped);
                            return true;
                        }
                    }
                }

                return false;
            }

            /// <summary>
            /// Flip triangles that do not satisfy the Delaunay condition
            /// </summary>
            private static int FlipTriangles(ListPoint points, List<Triad> triads, bool[] idsFlipped)
            {
                int numt = (int)triads.Count;
                Array.Clear(idsFlipped, 0, numt);

                int flipped = 0;
                for (int t = 0; t < numt; t++)
                {
                    int t2;
                    if (FlipTriangle(points, triads, t, out t2))
                    {
                        flipped += 2;
                        idsFlipped[t] = true;
                        idsFlipped[t2] = true;

                    }
                }

                return flipped;
            }

            private static int FlipTriangles(ListPoint points, List<Triad> triads, bool[] idsToTest, bool[] idsFlipped)
            {
                int numt = (int)triads.Count;
                Array.Clear(idsFlipped, 0, numt);

                int flipped = 0;
                for (int t = 0; t < numt; t++)
                {
                    if (idsToTest[t])
                    {
                        int t2;
                        if (FlipTriangle(points, triads, t, out t2))
                        {
                            flipped += 2;
                            idsFlipped[t] = true;
                            idsFlipped[t2] = true;
                        }
                    }
                }

                return flipped;
            }

            private static int FlipTriangles(ListPoint points, List<Triad> triads, bool[] idsToTest, Set<int> idsFlipped)
            {
                int numt = (int)triads.Count;
                idsFlipped.Clear();

                int flipped = 0;
                for (int t = 0; t < numt; t++)
                {
                    if (idsToTest[t])
                    {
                        int t2;
                        if (FlipTriangle(points, triads, t, out t2))
                        {
                            flipped += 2;
                            idsFlipped.Add(t);
                            idsFlipped.Add(t2);
                        }
                    }
                }

                return flipped;
            }

            private static int FlipTriangles(ListPoint points, List<Triad> triads, Set<int> idsToTest, Set<int> idsFlipped)
            {
                int flipped = 0;
                idsFlipped.Clear();

                foreach (int t in idsToTest)
                {
                    int t2;
                    if (FlipTriangle(points, triads, t, out t2))
                    {
                        flipped += 2;
                        idsFlipped.Add(t);
                        idsFlipped.Add(t2);
                    }
                }

                return flipped;
            }

            #endregion

            #region Debug verification routines: verify that triad adjacency and indeces are set correctly
#if DEBUG
            private void VerifyHullContains(Hull hull, int idA, int idB)
            {
                if (
                    ((hull[0].pointsIndex == idA) && (hull[hull.Count - 1].pointsIndex == idB)) ||
                    ((hull[0].pointsIndex == idB) && (hull[hull.Count - 1].pointsIndex == idA)))
                    return;

                for (int h = 0; h < hull.Count - 1; h++)
                {
                    if (hull[h].pointsIndex == idA)
                    {
                        Debug.Assert(hull[h + 1].pointsIndex == idB);
                        return;
                    }
                    else if (hull[h].pointsIndex == idB)
                    {
                        Debug.Assert(hull[h + 1].pointsIndex == idA);
                        return;
                    }
                }

            }

            private void VerifyTriadContains(Triad tri, int nbourTriad, int idA, int idB)
            {
                if (tri.ab == nbourTriad)
                {
                    Debug.Assert(
                        ((tri.a == idA) && (tri.b == idB)) ||
                        ((tri.b == idA) && (tri.a == idB)));
                }
                else if (tri.ac == nbourTriad)
                {
                    Debug.Assert(
                        ((tri.a == idA) && (tri.c == idB)) ||
                        ((tri.c == idA) && (tri.a == idB)));
                }
                else if (tri.bc == nbourTriad)
                {
                    Debug.Assert(
                        ((tri.c == idA) && (tri.b == idB)) ||
                        ((tri.b == idA) && (tri.c == idB)));
                }
                else
                    Debug.Assert(false);
            }

            private void VerifyTriads(List<Triad> triads, Hull hull)
            {
                for (int t = 0; t < triads.Count; t++)
                {
                    if (t == 17840)
                        t = t + 0;

                    Triad tri = triads[t];
                    if (tri.ac == -1)
                        VerifyHullContains(hull, tri.a, tri.c);
                    else
                        VerifyTriadContains(triads[tri.ac], t, tri.a, tri.c);

                    if (tri.ab == -1)
                        VerifyHullContains(hull, tri.a, tri.b);
                    else
                        VerifyTriadContains(triads[tri.ab], t, tri.a, tri.b);

                    if (tri.bc == -1)
                        VerifyHullContains(hull, tri.b, tri.c);
                    else
                        VerifyTriadContains(triads[tri.bc], t, tri.b, tri.c);

                }
            }

            private void WriteTriangles(List<Triad> triangles, string name)
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(name + ".dtt"))
                {
                    writer.WriteLine(triangles.Count.ToString());
                    for (int i = 0; i < triangles.Count; i++)
                    {
                        Triad t = triangles[i];
                        writer.WriteLine(string.Format("{0}: {1} {2} {3} - {4} {5} {6}",
                            i + 1,
                            t.a, t.b, t.c,
                            t.ab + 1, t.bc + 1, t.ac + 1));
                    }
                }
            }

#endif

            #endregion
        }
    }
}
