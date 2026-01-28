using System;

namespace Infragistics.Extension
{
    /// <summary>
    /// An implementation of an implicit binary heap. Min-heap and max-heap both supported. 
    /// Copyright © 2012 Atlassian.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class BinaryHeap<TKey, TValue>
		where TKey : IComparable<TKey>
	{
		protected const int defaultCapacity = 64;

		private readonly int _direction;
		private TValue[] _data;
		internal TKey[] _keys;
		private int _capacity;
		private int _size;

		protected BinaryHeap(int capacity, int direction)
		{
			this._direction = direction;
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
			_data[_size] = value;
			_keys[_size] = key;
			SiftUp(_size);
			_size++;
		}

		protected void RemoveTip()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}

			_size--;
			_data[0] = _data[_size];
			_keys[0] = _keys[_size];
			_data[_size] = default(TValue);
			SiftDown(0);
		}

		protected void ReplaceTip(TKey key, TValue value)
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}

			_data[0] = value;
			_keys[0] = key;
			SiftDown(0);
		}

		protected TValue GetTip()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}

			return _data[0];
		}

		protected TKey GetTipKey()
		{
			if (_size == 0) {
				throw new InvalidOperationException();
			}
			return _keys[0];
		}

		private void SiftUp(int c)
		{
			for (int p = (c - 1) / 2; c != 0 && _direction * _keys[c].CompareTo(_keys[p]) > 0; c = p, p = (c - 1) / 2) {
				TValue pData = _data[p];
				TKey pDist = _keys[p];
				_data[p] = _data[c];
				_keys[p] = _keys[c];
				_data[c] = pData;
				_keys[c] = pDist;
			}
		}

		private void SiftDown(int p)
		{
			for (int c = p * 2 + 1; c < _size; p = c, c = p * 2 + 1) {
				if (c + 1 < _size && _direction * _keys[c].CompareTo(_keys[c + 1]) < 0) {
					c++;
				}
				if (_direction * _keys[p].CompareTo(_keys[c]) < 0) {
					// Swap the points
					TValue pData = _data[p];
					TKey pDist = _keys[p];
					_data[p] = _data[c];
					_keys[p] = _keys[c];
					_data[c] = pData;
					_keys[c] = pDist;
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
	}
}
