using System;

namespace Infragistics.Extension
{
	/// <summary>
	/// Class for child nodes.
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public class KDNode<T>
	{
		// All types
		internal int _dimensions;
		protected int _bucketCapacity;


		// Leaf only
		internal KDEntry<T>[] _leafEntries;

		// Stem only
		internal KDNode<T> _left, _right, _parent;
		internal int _splitDimension;
		internal double _splitValue;

		// Bounds
		internal double[] _minBound, _maxBound;
		internal bool _singlePoint;

		protected KDNode(KDNode<T> parent, int dimensions, int bucketCapacity)
		{
			// Init base
			_parent = parent;
			_dimensions = dimensions;
			_bucketCapacity = bucketCapacity;
			_singlePoint = true;

			// Init leaf elements
			_leafEntries = new KDEntry<T>[bucketCapacity + 1];
		}

		/* -------- SIMPLE GETTERS -------- */

		internal int _count;
		public int Count
		{
			get { return _count; }
		}

		/* -------- INTERNAL OPERATIONS -------- */

		internal void AddLeafPoint(KDEntry<T> entry)
		{
			// Add the data point
			_leafEntries[_count] = entry;
			ExtendBounds(entry.Point);
			_count++;

			if (_count == _leafEntries.Length - 1) {
				// If the node is getting too large
				if (CalculateSplit()) {
					// If the node successfully had it's split value calculated, split node
					SplitLeafNode();
				} else {
					// If the node could not be split, enlarge node
					IncreaseLeafCapacity();
				}
			}
		}

		private bool CheckBounds(double[] point)
		{
			for (int i = 0; i < _dimensions; i++) {
				if (point[i] > _maxBound[i])
					return false;
				if (point[i] < _minBound[i])
					return false;
			}
			return true;
		}

		internal void ExtendBounds(double[] point)
		{
			if (_minBound == null) {
				_minBound = new double[_dimensions];
				Array.Copy(point, _minBound, _dimensions);

				_maxBound = new double[_dimensions];
				Array.Copy(point, _maxBound, _dimensions);
				return;
			}

			for (int i = 0; i < _dimensions; i++) {
				double pt = point[i];
				if (Double.IsNaN(pt)) {
					if (!Double.IsNaN(_minBound[i]) || !Double.IsNaN(_maxBound[i])) {
						_singlePoint = false;
					}
					_minBound[i] = Double.NaN;
					_maxBound[i] = Double.NaN;
				} else if (_minBound[i] > pt) {
					_minBound[i] = pt;
					_singlePoint = false;
				} else if (_maxBound[i] < pt) {
					_maxBound[i] = pt;
					_singlePoint = false;
				}
			}
		}

		private void IncreaseLeafCapacity()
		{
			int newLength = _leafEntries.Length * 2;

			var newEntries = new KDEntry<T>[newLength];
			Array.Copy(_leafEntries, newEntries, _leafEntries.Length);
			_leafEntries = newEntries;
		}

		private bool CalculateSplit()
		{
			if (_singlePoint)
				return false;

			double width = 0;
			for (int i = 0; i < _dimensions; i++) {
				double dwidth = (_maxBound[i] - _minBound[i]);
				if (Double.IsNaN(dwidth))
					dwidth = 0;
				if (dwidth > width) {
					_splitDimension = i;
					width = dwidth;
				}
			}

			if (width == 0) {
				return false;
			}

			// Start the split in the middle of the variance
			_splitValue = (_minBound[_splitDimension] + _maxBound[_splitDimension]) * 0.5;

			// Never split on infinity or NaN
			if (_splitValue == Double.PositiveInfinity) {
				_splitValue = Double.MaxValue;
			} else if (_splitValue == Double.NegativeInfinity) {
				_splitValue = -Double.MaxValue;
			}

			// Don't let the split value be the same as the upper value as
			// can happen due to rounding errors!
			if (_splitValue == _maxBound[_splitDimension]) {
				_splitValue = _minBound[_splitDimension];
			}

			// Success
			return true;
		}

		private void SplitLeafNode()
		{
			_right = new KDNode<T>(this, _dimensions, _bucketCapacity);
			_left = new KDNode<T>(this, _dimensions, _bucketCapacity);

			// Move locations into children
			for (int i = 0; i < _count; i++) {
				KDEntry<T> oldEntry = _leafEntries[i];
				if (oldEntry.Point[_splitDimension] > _splitValue) {
					_right.AddLeafPoint(oldEntry);
				} else {
					_left.AddLeafPoint(oldEntry);
				}
			}
			_leafEntries = null;
		}
	}
}
