using System;
using System.Collections.Generic;

namespace Infragistics.Extension
{
	public class NearestNeighborEnumerable<T> : IEnumerable<Neighbor<T>>
	{
		KDNode<T> _treeRoot;
		int _maxPointsReturned;
		private double[] _searchPoint = null;
		private DistanceFunction _distanceStrategy;

		internal NearestNeighborEnumerable(KDNode<T> treeRoot, double[] searchPoint, int maxPointsReturned, DistanceFunction distanceFunction)
		{
			_treeRoot = treeRoot;
			_maxPointsReturned = maxPointsReturned;
			_searchPoint = new double[searchPoint.Length];
			Array.Copy(searchPoint, _searchPoint, searchPoint.Length);
			_distanceStrategy = distanceFunction;
		}

		private class Enumerator: IEnumerator<Neighbor<T>>
		{
			private NearestNeighborEnumerable<T> _enumerable;
			private int _pointsRemaining;
			private MinHeap<double, KDNode<T>> _pendingPaths;
			private IntervalHeap<double, KDEntry<T>> _evaluatedPoints;

			public Enumerator(NearestNeighborEnumerable<T> enumerable)
			{
				_enumerable = enumerable;
				Reset();
			}

			#region IEnumerator<Neighbor<T>> Members

			public Neighbor<T> Current { get; private set; }

			#endregion

			#region IDisposable Members

			void IDisposable.Dispose()
			{
			}

			#endregion

			#region IEnumerator Members

			object System.Collections.IEnumerator.Current
			{
				get { return Current; }
			}

			bool System.Collections.IEnumerator.MoveNext()
			{
				if (_pointsRemaining == 0) {
					return false;
				}

				while (_pendingPaths.Count > 0 && (_evaluatedPoints.Count == 0 || _pendingPaths._keys[0] < _evaluatedPoints._keys[0])) {
					KDTree<T>.NearestNeighborSearchStep(_pendingPaths, _evaluatedPoints, _pointsRemaining, _enumerable._distanceStrategy, _enumerable._searchPoint);
				}

				// Return the smallest distance point
				_pointsRemaining--;
				Current = new Neighbor<T>(_evaluatedPoints._keys[0], _evaluatedPoints.GetMin());
				_evaluatedPoints.RemoveMin();
				return true;
			}

			public void Reset()
			{
				_pointsRemaining = System.Math.Min(_enumerable._maxPointsReturned, _enumerable._treeRoot._count);
				_pendingPaths = new MinHeap<double, KDNode<T>>();
				_pendingPaths.Add(0, _enumerable._treeRoot);
				_evaluatedPoints = new IntervalHeap<double, KDEntry<T>>();
			}

			#endregion
		}

		#region IEnumerable<Neighbor<T>> Members

		public IEnumerator<Neighbor<T>> GetEnumerator()
		{
			return new Enumerator(this);
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
