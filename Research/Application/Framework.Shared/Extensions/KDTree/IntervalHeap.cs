using System;
using System.Text;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Infragistics.Extension")]  

namespace Infragistics.Extension
{
	/// <summary>
	/// An implementation of an implicit binary interval heap.
	/// Copyright © 2012 Atlassian.
	/// </summary>
	/// <remarks>
	/// Copyright (C) 2009-2012 Rednaxela          (original Java implementation).
	/// Copyright (C) 2012 Olivier Jacot-Descombes (port to C#).
	/// </remarks>
	/// <typeparam name="T"></typeparam>
	public class IntervalHeap<TKey, TValue> : IMinHeap<TKey, TValue>, IMaxHeap<TKey, TValue>
		where TKey : IComparable<TKey>
	{
		private const int DefaultCapacity = 64;

		private TValue[] _data;
		internal TKey[] _keys;
		private int _capacity;
		private int _size;

		public IntervalHeap()
			: this(DefaultCapacity)
		{
		}

		public IntervalHeap(int capacity)
		{
			this._data = new TValue[capacity];
			this._keys = new TKey[capacity];
			this._capacity = capacity;
			this._size = 0;
		}

		public void Add(TKey key, TValue value)
		{
			// If move room is needed, TKey array size
			if (_size >= _capacity) {
				_capacity *= 2;

				var newData = new TValue[_capacity];
				Array.Copy(_data, newData, _data.Length);
				_data = newData;

				var newKeys = new TKey[_capacity];
				Array.Copy(_keys, newKeys, _keys.Length);
				_keys = newKeys;
			}

			// Insert new value at the end
			_size++;
			_data[_size - 1] = value;
			_keys[_size - 1] = key;
			SiftInsertedValueUp();
		}

		public void RemoveMin()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}

			unchecked {
				_size--;
				_data[0] = _data[_size];
				_keys[0] = _keys[_size];
				_data[_size] = default(TValue);
				SiftDownMin();
			}
		}

		public void ReplaceMin(TKey key, TValue value)
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}

			_data[0] = value;
			_keys[0] = key;
			if (_size > 1) {
				// Swap with pair if necessary
				if (_keys[1].CompareTo(key) < 0) {
					Swap(0, 1);
				}
				SiftDownMin();
			}
		}

		public void RemoveMax()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			} else if (_size == 1) {
				RemoveMin();
				return;
			}

			_size--;
			_data[1] = _data[_size];
			_keys[1] = _keys[_size];
			_data[_size] = default(TValue);
			SiftDownMax(1);
		}

		public void ReplaceMax(TKey key, TValue value)
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			} else if (_size == 1) {
				ReplaceMin(key, value);
				return;
			}

			_data[1] = value;
			_keys[1] = key;
			// Swap with pair if necessary
			if (key.CompareTo(_keys[0]) < 0) {
				Swap(0, 1);
			}
			SiftDownMax(1);
		}

		public TValue GetMin()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}
			return _data[0];
		}

		public TValue GetMax()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			} else if (_size == 1) {
				return _data[0];
			}
			return _data[1];
		}

		public TKey GetMinKey()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}
			return _keys[0];
		}

		public TKey GetMaxKey()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			} else if (_size == 1) {
				return _keys[0];
			}
			return _keys[1];
		}

		private int Swap(int x, int y)
		{
			TValue tempVal = _data[y];
			TKey tempKey = _keys[y];

			_data[y] = _data[x];
			_keys[y] = _keys[x];

			_data[x] = tempVal;
			_keys[x] = tempKey;

			return y;
		}

		/**
		 * Min-side (u % 2 == 0):
		 * - leftchild:  2u + 2
		 * - rightchild: 2u + 4
		 * - parent:     (x/2-1)&~1
		 *
		 * Max-side (u % 2 == 1):
		 * - leftchild:  2u + 1
		 * - rightchild: 2u + 3
		 * - parent:     (x/2-1)|1
		 */

		private void SiftInsertedValueUp()
		{
			int u = _size - 1;
			if (u == 0) {
				// Do nothing if it's the only element!
			} else if (u == 1) {
				// If it is the second element, just sort it with it's pair
				if (_keys[u].CompareTo(_keys[u - 1]) < 0) { // If less than it's pair
					Swap(u, u - 1); // Swap with it's pair
				}
			} else if (u % 2 == 1) {
				// Already paired. Ensure pair is ordered right
				int p = (u / 2 - 1) | 1; // The larger value of the parent pair
				if (_keys[u].CompareTo(_keys[u - 1]) < 0) { // If less than it's pair
					u = Swap(u, u - 1); // Swap with it's pair
					if (_keys[u].CompareTo(_keys[p - 1]) < 0) { // If smaller than smaller parent pair
						// Swap into min-heap side
						u = Swap(u, p - 1);
						SiftUpMin(u);
					}
				} else {
					if (_keys[u].CompareTo(_keys[p]) > 0) { // If larger that larger parent pair
						// Swap into max-heap side
						u = Swap(u, p);
						SiftUpMax(u);
					}
				}
			} else {
				// Inserted in the lower-value slot without a partner
				int p = (u / 2 - 1) | 1; // The larger value of the parent pair
				if (_keys[u].CompareTo(_keys[p]) > 0) { // If larger that larger parent pair
					// Swap into max-heap side
					u = Swap(u, p);
					SiftUpMax(u);
				} else if (_keys[u].CompareTo(_keys[p - 1]) < 0) { // If smaller than smaller parent pair
					// Swap into min-heap side
					u = Swap(u, p - 1);
					SiftUpMin(u);
				}
			}
		}

		private void SiftUpMin(int c)
		{
			// Min-side parent: (x/2-1)&~1
			for (int p = (c / 2 - 1) & ~1; p >= 0 && _keys[c].CompareTo(_keys[p]) < 0; c = p, p = (c / 2 - 1) & ~1) {
				Swap(c, p);
			}
		}

		private void SiftUpMax(int c)
		{
			// Max-side parent: (x/2-1)|1
			for (int p = (c / 2 - 1) | 1; p >= 0 && _keys[c].CompareTo(_keys[p]) > 0; c = p, p = (c / 2 - 1) | 1) {
				Swap(c, p);
			}
		}

		private void SiftDownMin()
		{
			int p = 0;
			unchecked {
				for (int c = /* 2*p + */ 2; c < _size; p = c, c = 2 * p + 2) {
					if (c + 2 < _size && _keys[c + 2].CompareTo(_keys[c]) < 0) {
						c += 2;
					}
					if (_keys[c].CompareTo(_keys[p]) < 0) {
						Swap(p, c);
						// Swap with pair if necessary
						if (c + 1 < _size && _keys[c + 1].CompareTo(_keys[c]) < 0) {
							Swap(c, c + 1);
						}
					} else {
						break;
					}
				}
			}
		}

		private void SiftDownMax(int p)
		{
			for (int c = p * 2 + 1; c <= _size; p = c, c = p * 2 + 1) {
				if (c == _size) {
					// If the left child only has half a pair
					if (_keys[c - 1].CompareTo(_keys[p]) > 0) {
						Swap(p, c - 1);
					}
					break;
				} else if (c + 2 == _size) {
					// If there is only room for a right child lower pair
					if (_keys[c + 1].CompareTo(_keys[c]) > 0) {
						if (_keys[c + 1].CompareTo(_keys[p]) > 0) {
							Swap(p, c + 1);
						}
						break;
					}
				} else if (c + 2 < _size) {
					// If there is room for a right child upper pair
					if (_keys[c + 2].CompareTo(_keys[c]) > 0) {
						c += 2;
					}
				}
				if (_keys[c].CompareTo(_keys[p]) > 0) {
					Swap(p, c);
					// Swap with pair if necessary
					if (_keys[c - 1].CompareTo(_keys[c]) > 0) {
						Swap(c, c - 1);
					}
				} else {
					break;
				}
			}
		}

		public int Count
		{
			get { return _size; }
		}

		public int Capacity
		{
			get { return _capacity; }
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(typeof(IntervalHeap<TKey, TValue>).Name);
			sb.Append(", size: ").Append(Count).Append(" capacity: ").Append(Capacity);
			int i = 0, p = 2;
			while (i < Count) {
				int x = 0;
				sb.Append("\t");
				while ((i + x) < Count && x < p) {
					sb.Append(_keys[i + x].ToString()).Append(", ");
					x++;
				}
				sb.Append("\n");
				i += x;
				p *= 2;
			}
			return sb.ToString();
		}

		private bool ValidateHeap()
		{
			// Validate left-right
			for (int i = 0; i < _size - 1; i += 2) {
				if (_keys[i].CompareTo(_keys[i + 1]) > 0)
					return false;
			}
			// Validate within parent interval
			for (int i = 2; i < _size; i++) {
				TKey maxParent = _keys[(i / 2 - 1) | 1];
				TKey minParent = _keys[(i / 2 - 1) & ~1];
				if (_keys[i].CompareTo(maxParent) > 0 || _keys[i].CompareTo(minParent) < 0)
					return false;
			}
			return true;
		}
	}
}
