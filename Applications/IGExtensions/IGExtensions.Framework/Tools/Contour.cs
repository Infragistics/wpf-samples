using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Toolity class for piecewise construction of indexed polylines.
    /// </summary>
    /// <remarks>
    /// This class represents polyline point as integer ids with the assumption that
    /// two vertices are identical if they have identical ids. The class itself
    /// has no need for any vertex-specific information such as its coordinates.
    /// </remarks>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class PolylineBuilder
    {
        /// <summary>
        /// Clears all polylines from the current object.
        /// </summary>
        virtual public void Clear()
        {
            byBegin.Clear();
            byEnd.Clear();
        }

        /// <summary>
        /// Gets the list of polylines for the current object.
        /// </summary>
        /// <remarks>
        /// There may be zero or more constructed polylines each of length two or more. A polyline
        /// is represented as a list of integer ids as passed to the AddSegment method. Polylines
        /// do not contain self-intersections or adjacent coincident point. A polyline may form
        /// a loop, in which case its first and last point are identical.
        /// </remarks>
        public IEnumerable<List<int>> Polylines
        {
            get
            {
                foreach (List<int> polyline in byBegin.Values)
                {
                    yield return polyline;
                }
            }
        }

        /// <summary>
        /// Adds a segment to the current object.
        /// </summary>
        /// <param name="begin">Id of the first point in the segment</param>
        /// <param name="end">Id of the end point in the segment</param>
        public void AddSegment(int begin, int end)
        {
            List<int> before = null;
            List<int> after = null;

            Debug.Assert(!byBegin.ContainsKey(begin));
            Debug.Assert(!byEnd.ContainsKey(end));

            byEnd.TryGetValue(begin, out before);       // find the polyline which ends where the segment begins
            byBegin.TryGetValue(end, out after);        // find the polyline which begins where the segment begins

            if (before == null && after == null)
            {
                List<int> p2 = new List<int>();         // a new, isolated polyline
                p2.Add(begin);                          // begin
                p2.Add(end);                            // end

                byBegin.Add(begin, p2);                 // new polyline starts at begin
                byEnd.Add(end, p2);                     // and ends at end
            }

            if (before == null && after != null)
            {
                byBegin.Remove(end);                    // existing after's begin is about to change
                after.Insert(0, begin);                 // pretend after
                byBegin.Add(begin, after);              // add after to byBegin at its new position
            }

            if (before != null && after == null)
            {
                byEnd.Remove(begin);                    // before's end is about to change
                before.Add(end);                        // add resolution to before
                byEnd.Add(end, before);                 // add before to byEnd at its new position                  
            }

            if (before != null && after != null)
            {
                //if (before == after /*before.First() == after.Last()*/)
                if (before == after /*before.First() == after.Last()*/)
                {
                    before.Add(end);                    // close the loop
                    byEnd.Remove(begin);                // loops have no end (they don't have a beginning either, but I've got to put it somewhere)
                }
                else
                {
                    Debug.Assert(before[before.Count - 1] != after[0]);
                    byBegin.Remove(after[0]);                   // after is about to disappear
                    byEnd.Remove(after[after.Count-1]);         // after is about to disappear

                    byEnd.Remove(before[before.Count - 1]);     // before's end is about to change
                    before.AddRange(after);                     // append after to before
                    byEnd.Add(before[before.Count - 1], before);   // add before to byEnd at its new position
                }
            }
        }

        protected Dictionary<int, List<int>> byBegin = new Dictionary<int, List<int>>();
        protected Dictionary<int, List<int>> byEnd = new Dictionary<int, List<int>>();
    }

    /// <summary>
    /// Toolity class for piecewise construction of edge-based contours.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class ContourBuilder : PolylineBuilder
    {
        /// <summary>
        /// Clears all polylines and point from the current object.
        /// </summary>
        public override void Clear()
        {
            base.Clear();

            dictionary.Clear();
            x.Clear();
            y.Clear();
        }

        /// <summary>
        /// Gets the list of x coordinates for the line in the current object.
        /// </summary>
        public IList<float> X { get { return x; } }

        /// <summary>
        /// Gets the list of y coordinates for the line in the current object.
        /// </summary>
        public IList<float> Y { get { return y; } }

        /// <summary>
        /// Calculates an edge point for the current object and returns its point id.
        /// </summary>
        /// <remarks>
        /// The begin and edge point ids are hashed to produce a unique point id. The current implementation
        /// of the hash function is reliable for ids of less than 65536.
        /// </remarks>
        /// <param name="b">Edge begin point id</param>
        /// <param name="e">Edge end point id</param>
        /// <param name="xb">Edge begin point x coordinate.</param>
        /// <param name="yb">Edge begin point y coordinate.</param>
        /// <param name="zb">Edge begin point z coordinate.</param>
        /// <param name="xe">Edge end point x coordinate.</param>
        /// <param name="ye">Edge end point y coordinate.</param>
        /// <param name="ze">Edge end point z coordinate.</param>
        /// <param name="z">Contour v.</param>
        /// <returns>Unique vertex id.</returns>
        public int Point(int b, double xb, double yb, double zb, int e, double xe, double ye, double ze, double z)
        {
            Debug.Assert(b < 65536 && e < 65536);

            int hash = Math.Min(b, e) + 65536 * Math.Max(b, e);
            int index = -1;

            if (!dictionary.TryGetValue(hash, out index))
            {
                index = x.Count;

                double p = (z - zb) / (ze - zb);

                dictionary.Add(hash, index);
                x.Add((float)(xb + p * (xe - xb)));
                y.Add((float)(yb + p * (ye - yb)));
            }

            return index;
        }

        private Dictionary<int, int> dictionary = new Dictionary<int, int>();
        private List<float> x = new List<float>();
        private List<float> y = new List<float>();
    }
}
