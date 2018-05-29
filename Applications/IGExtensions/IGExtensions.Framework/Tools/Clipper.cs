using System.Collections.Generic;
using System.Windows;
using System;
using System.ComponentModel;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Modified Sutherland-Hodge clipping
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Clipper
    {
        public IList<Point> Target
        {
            get
            {
                return target;
            }
            set
            {
                if (Head is EdgeClipper)
                {
                    Head.Clear();
                }

                target = value;

                IList<Point> head = target;

                if (leftClipper != null) { leftClipper.Dst = head; head = leftClipper; }
                if (bottomClipper != null) { bottomClipper.Dst = head; head = bottomClipper; }
                if (rightClipper != null) { rightClipper.Dst = head; head = rightClipper; }
                if (topClipper != null) { topClipper.Dst = head; head = topClipper; }

                Head = head;
            }
        }

        private IList<Point> Head;

        private IList<Point> target;
        private LeftClipper leftClipper;
        private BottomClipper bottomClipper;
        private RightClipper rightClipper;
        private TopClipper topClipper;

        /// <summary>
        /// Initializes a new instance of the Clipper class.
        /// </summary>
        /// <param name="clip">Clip rectangle</param>
        /// <param name="isClosed">True to clip as polygon, false to clip as polyline</param>
        public Clipper(Rect clip, bool isClosed)
        {
            leftClipper = new LeftClipper() { Edge = clip.Left, IsClosed = isClosed };
            bottomClipper = new BottomClipper() { Edge = clip.Bottom, IsClosed = isClosed };
            rightClipper = new RightClipper() { Edge = clip.Right, IsClosed = isClosed };
            topClipper = new TopClipper() { Edge = clip.Top, IsClosed = isClosed };
        }

        /// <summary>
        /// Initializes a new instance of the Clipper class.
        /// </summary>
        /// <param name="left">Left edge of clip rectangle or NaN.</param>
        /// <param name="bottom">Bottom edge of clip rectangle or NaN.</param>
        /// <param name="right">Right edge of clip rectangle or NaN.</param>
        /// <param name="top">Top edge of clip rectangle or NaN.</param>
        /// <param name="isClosed">True to clip as polygon, false to clip as polyline</param>
        public Clipper(double left, double bottom, double right, double top, bool isClosed)
        {
            leftClipper = !double.IsNaN(left) ? new LeftClipper() { Edge = left, IsClosed = isClosed } : null;
            bottomClipper = !double.IsNaN(bottom) ? new BottomClipper() { Edge = bottom, IsClosed = isClosed } : null;
            rightClipper = !double.IsNaN(right) ? new RightClipper() { Edge = right, IsClosed = isClosed } : null;
            topClipper = !double.IsNaN(top) ? new TopClipper() { Edge = top, IsClosed = isClosed } : null;
        }

        public void Add(Point point)
        {
            Head.Add(point);
        }
        public void AddRange(IEnumerable<Point> points)
        {
            foreach (Point point in points)
            {
                Head.Add(point);
            }
        }
        #region EdgeClipper
        /// <summary>
        /// Represents a clipping stage in the Sutherland-Hodge clipper.
        /// </summary>
        /// <remarks>
        /// EdgeClipper implements IList so that it can be transparently
        /// pipe to either another edge clipper or a "real" IList implementation.
        /// </remarks>
        internal abstract class EdgeClipper : IList<Point>
        {
            /// <summary>
            /// Sets or gets the destination for the current edge clipper object.
            /// </summary>
            /// <remarks>
            /// Setting an edge clipper's destination resets the stage.
            /// </remarks>
            public IList<Point> Dst
            {
                get { return dst; }
                set
                {
                    if (dst != value)
                    {
                        init = true;
                        dst = value;
                    }
                }
            }
            public IList<Point> dst;

            private bool init = true;
            private Point First;

            private Point Prev;
            private bool PrevInside;

            public bool IsClosed;
            private bool IsOutput = false;

            /// <summary>
            /// Adds a point to the current edge clipper, resulting in zero, one or two
            /// points being piped to the desitnation IList.
            /// </summary>
            /// <param name="cur">Point to add to the clipping stage.</param>
            public void Add(Point cur)
            {
                bool CurInside = IsInside(cur);

                if (init)
                {
                    init = false;
                    First = cur;
                }
                else
                {
                    if (true)// Prev.X != cur.X || Prev.Y != cur.Y)
                    {
                        if (CurInside)
                        {
                            if (!PrevInside)
                            {
                                Dst.Add(Intersection(Prev, cur));
                            }
                            else
                            {
                                if (!IsClosed && !IsOutput)
                                {
                                    Dst.Add(Prev);
                                    IsOutput = true;
                                }
                            }

                            Dst.Add(cur);
                        }
                        else
                        {
                            if (PrevInside)
                            {
                                if (!IsClosed && !IsOutput)
                                {
                                    Dst.Add(Prev);
                                    IsOutput = true;
                                }

                                Dst.Add(Intersection(Prev, cur));
                            }
                        }
                    }
                }

                Prev = cur;
                PrevInside = CurInside;
            }

            /// <summary>
            /// Flushes the edge clipping stage.
            /// </summary>
            public void Clear()
            {
                if (IsClosed)
                {
                    Add(First);
                }

                if (Dst is EdgeClipper)
                {
                    (Dst as EdgeClipper).Clear();
                }

                init = true;
                IsOutput = false;
            }

            /// <summary>
            /// Gets the status of the point with respect to the current clipping stage's edge.
            /// </summary>
            /// <param name="pt">Point to test</param>
            /// <returns>True if the point is inside or on the edge, false otherwise</returns>
            protected abstract bool IsInside(Point pt);

            /// <summary>
            /// Gets the intersection of an edge with the current clipping stage's edge.
            /// </summary>
            /// <param name="b">Start of edge</param>
            /// <param name="e">End of edge</param>
            /// <returns>Intersection of edge with the current clipping stage's edge</returns>
            protected abstract Point Intersection(Point b, Point e);

            #region place-holder implementations for the other members of IList
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return null;
            }
            public System.Collections.Generic.IEnumerator<Point> GetEnumerator() { return null; }
            public bool IsReadOnly { get { return false; } }
            public int Count { get { return 0; } }
            public bool Remove(Point pt) { return false; }
            public void RemoveAt(int n) { }
            public void CopyTo(Point[] pt, int n) { }
            public bool Contains(Point pt) { return false; }
            public Point this[int n] { get { return new Point(0, 0); } set { } }
            public void Insert(int n, Point pt) { }
            public int IndexOf(Point pt) { return -1; }
            #endregion
        }
        #endregion

        #region EdgeClipper for left edges
        /// <summary>
        /// Represents a specialised clipping stage for a clip window's left edge.
        /// </summary>
        internal class LeftClipper : EdgeClipper
        {
            public double Edge;

            protected override bool IsInside(Point pt)
            {
                return pt.X >= Edge;
            }

            protected override Point Intersection(Point b, Point e)
            {
                double p = (Edge - b.X) / (e.X - b.X);
                double x = b.X + p * (e.X - b.X);
                double y = b.Y + p * (e.Y - b.Y);

                return new Point(Edge, b.Y + (e.Y - b.Y) * (Edge - b.X) / (e.X - b.X));
            }
        }
        #endregion

        #region EdgeClipper for bottom edges
        /// <summary>
        /// Represents a specialised clipping stage for a clip window's bottom edge.
        /// </summary>
        internal class BottomClipper : EdgeClipper
        {
            public double Edge;

            protected override bool IsInside(Point pt)
            {
                return pt.Y <= Edge;
            }

            protected override Point Intersection(Point b, Point e)
            {
                double p = (Edge - b.Y) / (e.Y - b.Y);
                double x = b.X + p * (e.X - b.X);
                double y = b.Y + p * (e.Y - b.Y);

                return new Point(b.X + (e.X - b.X) * (Edge - b.Y) / (e.Y - b.Y), Edge);
            }
        }
        #endregion

        #region EdgeClipper for right edges
        /// <summary>
        /// Represents a specialised clipping stage for a clip window's right edge.
        /// </summary>
        internal class RightClipper : EdgeClipper
        {
            public double Edge;

            protected override bool IsInside(Point pt)
            {
                return pt.X <= Edge;
            }

            protected override Point Intersection(Point b, Point e)
            {
                double p = (Edge - b.X) / (e.X - b.X);
                double x = b.X + p * (e.X - b.X);
                double y = b.Y + p * (e.Y - b.Y);

                return new Point(Edge, b.Y + (e.Y - b.Y) * (Edge - b.X) / (e.X - b.X));
            }
        }
        #endregion

        #region EdgeClipper for top edges
        /// <summary>
        /// Represents a specialised clipping stage for a clip window's top edge.
        /// </summary>
        internal class TopClipper : EdgeClipper
        {
            public double Edge;

            protected override bool IsInside(Point pt)
            {
                return pt.Y >= Edge;
            }

            protected override Point Intersection(Point b, Point e)
            {
                double p = (Edge - b.Y) / (e.Y - b.Y);
                double x = b.X + p * (e.X - b.X);
                double y = b.Y + p * (e.Y - b.Y);

                return new Point(b.X + (e.X - b.X) * (Edge - b.Y) / (e.Y - b.Y), Edge);
            }
        }
        #endregion
    }
}
