using System;
using System.Linq;
using System.Collections.Generic;


namespace Infragistics.Extension
{

	/// <summary>
	/// Space-partitioning data structure for organizing points in a k-dimensional space.
	/// </summary>
	/// <remarks>
	/// Copyright (C) 2009-2012 Rednaxela          (original Java implementation).
	/// Copyright (C) 2012 Olivier Jacot-Descombes (port to C# and further development). 
	/// </remarks>
	/// <typeparam name="T">Value type</typeparam>
	public class KDTree<T> : KDNode<T>, ICollection<KDEntry<T>>
	{
		private Func<T, double[][]> _accessMultiplePoints;
		private Func<T, double[]> _accessSinglePoint;

		public KDTree(int dimensions)
			: this(dimensions, 24)
		{
		}

		public KDTree(int dimensions, Func<T, double[]> singlePointAccessor)
			: base(null, dimensions, 24)
		{
			_accessSinglePoint = singlePointAccessor;
		}

		public KDTree(int dimensions, Func<T, double[][]> multiPointAccessor)
			: base(null, dimensions, 24)
		{
			_accessMultiplePoints = multiPointAccessor;
		}

		public KDTree(int dimensions, int bucketCapacity)
			: base(null, dimensions, bucketCapacity)
		{
		}

		public KDTree(int dimensions, int bucketCapacity, Func<T, double[]> singlePointAccessor)
			: base(null, dimensions, bucketCapacity)
		{
			_accessSinglePoint = singlePointAccessor;
		}

		public KDTree(int dimensions, int bucketCapacity, Func<T, double[][]> multiPointAccessor)
			: base(null, dimensions, bucketCapacity)
		{
			_accessMultiplePoints = multiPointAccessor;
		}

		public void Add(T value, params double[] point)
		{
			Add(new KDEntry<T>(value, point));
		}

		public void Add(T value)
		{
			if (_accessSinglePoint != null) {
				Add(new KDEntry<T>(value, _accessSinglePoint(value)));
			} else if (_accessMultiplePoints != null) {
				var points = _accessMultiplePoints(value);
				for (int i = 0; i < points.Length; i++) {
					Add(new KDEntry<T>(value, points[i]));
				}
			} else {
				throw new InvalidOperationException("You must specify a point accessor in the constructor of KDTree<T> in order to use this overload of Add.");
			}
		}

		public bool Remove(T value, params double[] point)
		{
			return Remove(new KDEntry<T>(value, point));
		}

		public bool Remove(T value)
		{
			if (_accessSinglePoint != null) {
				return Remove(new KDEntry<T>(value, _accessSinglePoint(value)));
			} else if (_accessMultiplePoints != null) {
				bool ok = true;
				var points = _accessMultiplePoints(value);
				for (int i = 0; i < points.Length; i++) {
					ok &= Remove(new KDEntry<T>(value, points[i]));
				}
				return ok;
			} else {
				throw new InvalidOperationException("You must specify a point accessor in the constructor of KDTree<T> in order to use this overload of Remove.");
			}
		}

        //protected NearestNeighborEnumerable<T> GetNeighbors(int maxPointsReturned, params double[] searchPoint)
        //{
        //    // ChebyshevDistance SquareEuclideanDistance ManhattanDistance
        //    return new NearestNeighborEnumerable<T>(this, searchPoint, maxPointsReturned, SquareEuclideanDistance.Instance);
        //}

        protected List<Neighbor<T>> GetNeighbors(int maxNeighbors, DistanceFunction searchStrategy, params double[] searchPoint)
		{
            return new NearestNeighborEnumerable<T>(this, searchPoint, maxNeighbors, searchStrategy).ToList();
		}

        public List<Neighbor<T>> GetNeighbors(int maxNeighbors, DistanceFunction searchStrategy, bool useDistict, params double[] searchPoint)
	    {
            var neighbors = this.GetNeighbors(maxNeighbors, searchStrategy, searchPoint);
            if (useDistict)
            {
                var distict = new List<Neighbor<T>>();
                var keys = new List<double>();
                foreach (var neighbor in neighbors)
                {
                    if (!keys.Contains(neighbor.Distance))
                    {
                        keys.Add(neighbor.Distance);
                        distict.Add(neighbor);
                    }
                }
                distict.Sort((i1, i2) =>
                {
                    if (i1.Distance < i2.Distance) return -1;
                    if (i1.Distance > i2.Distance) return 1;
                    return 0;
                });

                return distict;
            }
            return neighbors;
        }

        public List<Neighbor<T>> GetNeighbors(int maxNeighbors, params double[] searchPoint)
	    {
            return GetNeighbors(maxNeighbors, SquareEuclideanDistance.Instance, true, searchPoint);
	    }


        public MaxHeap<double, KDEntry<T>> GetNeighborsHeap(int maxNeighbors, DistanceFunction searchStrategy, params double[] searchPoint)
		{
			var pendingPaths = new MinHeap<double, KDNode<T>>();
			var evaluatedPoints = new MaxHeap<double, KDEntry<T>>();
            var pointsRemaining = System.Math.Min(maxNeighbors, _count);
			pendingPaths.Add(0, this);

			while (pendingPaths.Count > 0 && (evaluatedPoints.Count < pointsRemaining || (pendingPaths._keys[0] < evaluatedPoints.GetMaxKey()))) {
                NearestNeighborSearchStep(pendingPaths, evaluatedPoints, pointsRemaining, searchStrategy, searchPoint);
			}

			return evaluatedPoints;
		}

		internal static void NearestNeighborSearchStep(
				MinHeap<double, KDNode<T>> pendingPaths, IMaxHeap<double, KDEntry<T>> evaluatedPoints, int desiredPoints,
				DistanceFunction distanceFunction, double[] searchPoint)
		{
			// If there are pending paths possibly closer than the nearest evaluated point, check it out
			KDNode<T> cursor = pendingPaths.GetMin();
			pendingPaths.RemoveMin();

			// Descend the tree, recording paths not taken
			while (cursor._leafEntries == null) {
				KDNode<T> pathNotTaken;
				if (searchPoint[cursor._splitDimension] > cursor._splitValue) {
					pathNotTaken = cursor._left;
					cursor = cursor._right;
				} else {
					pathNotTaken = cursor._right;
					cursor = cursor._left;
				}

				// OJD 23.05.2012 Beacuse of null reference in DistanceToRect. Not sure why this happened. Is something wrong with the remove function?
				// Did I change the coordinates of an added item?
				if (pathNotTaken._minBound == null) {
					pathNotTaken._minBound = new double[pathNotTaken._dimensions];
					pathNotTaken._maxBound = new double[pathNotTaken._dimensions];
				}

				double otherDistance = distanceFunction.DistanceToRect(searchPoint, pathNotTaken._minBound, pathNotTaken._maxBound);
				// Only add a path if we either need more points or it's closer than furthest point on list so far
				if (evaluatedPoints.Count < desiredPoints || otherDistance <= evaluatedPoints.GetMaxKey()) {
					pendingPaths.Add(otherDistance, pathNotTaken);
				}
			}

			if (cursor._singlePoint && cursor._count > 0) { // ODJ: Added "&& cursor._count > 0"
				double nodeDistance = distanceFunction.Distance(cursor._leafEntries[0].Point, searchPoint);
				// Only add a point if either need more points or it's closer than furthest on list so far
				if (evaluatedPoints.Count < desiredPoints || nodeDistance <= evaluatedPoints.GetMaxKey()) {
					for (int i = 0; i < cursor._count; i++) {
						// If we don't need any more, replace max
						if (evaluatedPoints.Count == desiredPoints) {
							evaluatedPoints.ReplaceMax(nodeDistance, cursor._leafEntries[i]);
						} else {
							evaluatedPoints.Add(nodeDistance, cursor._leafEntries[i]);
						}
					}
				}
			} else {
				// Add the points at the cursor
				for (int i = 0; i < cursor._count; i++) {
					KDEntry<T> entry = cursor._leafEntries[i];
					double distance = distanceFunction.Distance(entry.Point, searchPoint);
					// Only add a point if either need more points or it's closer than furthest on list so far
					if (evaluatedPoints.Count < desiredPoints) {
						evaluatedPoints.Add(distance, entry);
					} else if (distance < evaluatedPoints.GetMaxKey()) {
						evaluatedPoints.ReplaceMax(distance, entry);
					}
				}
			}
		}

		KDValuesCollection<T> _valuesCollection;
		public ICollection<T> Values
		{
			get
			{
				if (_valuesCollection == null) {
					_valuesCollection = new KDValuesCollection<T>(this);
				}
				return _valuesCollection;
			}
		}

		KDPointsCollection<T> _pointsCollection;
		public ICollection<double[]> Points
		{
			get
			{
				if (_pointsCollection == null) {
					_pointsCollection = new KDPointsCollection<T>(this);
				}
				return _pointsCollection;
			}
		}

		#region ICollection<KDEntry<T>> Members

		public void Add(KDEntry<T> entry)
		{
			KDNode<T> cursor = this;
			while (cursor._leafEntries == null) {
				cursor.ExtendBounds(entry.Point);
				cursor._count++;
				if (entry.Point[cursor._splitDimension] > cursor._splitValue) {
					cursor = cursor._right;
				} else {
					cursor = cursor._left;
				}
			}
			cursor.AddLeafPoint(entry);
		}

		public void Clear()
		{
			_singlePoint = true;
			_left = null;
			_right = null;
			_splitDimension = 0;
			_splitValue = 0.0;
			_minBound = null;
			_maxBound = null;

			// Init leaf elements
			_leafEntries = new KDEntry<T>[_bucketCapacity + 1];
			_count = 0;
		}

		public bool Contains(KDEntry<T> entry)
		{
			return this.Contains(entry, null); // The null parameter prevents a recursive call to itself and calls the LINQ extension method.
		}

		public void CopyTo(KDEntry<T>[] array, int arrayIndex)
		{
			if (array == null) {
				throw new ArgumentNullException("'array' is null");
			}
			if (arrayIndex < 0) {
				throw new ArgumentOutOfRangeException("'arrayIndex' is less than 0");
			}
			if (arrayIndex >= array.Length) {
				throw new ArgumentException("`arrayIndex` is equal to or greater than the length of `array`");
			}
			if (array.Length - arrayIndex < _count) {
				throw new ArgumentException("The number of elements is greater than the available space from `arrayIndex` to the end of the destination array.");
			}

			int i = arrayIndex;
			foreach (KDEntry<T> entry in this) {
				array[i++] = entry;
			}
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(KDEntry<T> entry)
		{
			// TODO: adjust _minBound and _maxBound after removing an item.
			// According to Rednaxela, "... the bounds affect the correctness if they are *too small*, 
			// but the bounds being too big (as is the case after removing) has no impact. The only
			// consequence is that the searches may be slightly slower than optimal. If you are
			// adjusting the bounds though, you might as well adjust the parent ones as well, because
			// that's where a large chunk of the (smallish) performance impact would be."

			KDNode<T> cursor = this;
			while (cursor._leafEntries == null) {
				if (entry.Point[cursor._splitDimension] > cursor._splitValue) {
					cursor = cursor._right;
				} else {
					cursor = cursor._left;
				}
			}
			for (int i = 0; i < cursor._count; i++) {
				if (cursor._leafEntries[i].Equals(entry)) {
					// According to Rednaxela, we can remove an item from the bucket in O(1) time due to the fact that
					// 1) unused array elements are expected, and
					// 2) the array of points in a leaf node are unordered.
					// We just need to overwrite the removed element with the last element, and then remove the last element.
					cursor._leafEntries[i] = cursor._leafEntries[cursor._count - 1];
					cursor._leafEntries[cursor._count - 1] = default(KDEntry<T>);
					do {
						cursor._count--;
						cursor = cursor._parent;
					} while (cursor != null);
					return true;
				}
			}
			return false;
		}

		#endregion

		#region IEnumerable<KDEntry<T>> Members

		public IEnumerator<KDEntry<T>> GetEnumerator()
		{
			Stack<KDNode<T>> stack = new Stack<KDNode<T>>();
			stack.Push(this);
			do {
				KDNode<T> current = stack.Pop();
				if (current._leafEntries == null) {
					stack.Push(current._left);
					stack.Push(current._right);
				} else {
					for (int i = 0; i < current._count; i++) {
						yield return current._leafEntries[i];
					}
				}
			} while (stack.Count > 0);
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
