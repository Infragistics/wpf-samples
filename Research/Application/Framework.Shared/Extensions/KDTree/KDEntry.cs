using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infragistics.Extension
{
	public class KDEntry<T>
	{
		internal static EqualityComparer<T> _valueComparer = EqualityComparer<T>.Default;

		public KDEntry(T value, params double[] point)
		{
			_point = point;
			_value = value;
		}

		public KDEntry(KDEntry<T> entry)
		{
			_point = entry.Point;
			_value = entry.Value;
		}

		protected double[] _point;
		public double[] Point
		{
			get { return _point; }
			private set { _point = value; }
		}

		protected T _value;
		public T Value
		{
			get { return _value; }
			private set { _value = value; }
		}

		public override bool Equals(object obj)
		{
			var other = obj as KDEntry<T>;
			if (other == null) {
				return false;
			}
			return Equals(other);
		}

		public bool Equals(KDEntry<T> other)
		{
			if (other._point != null || _point != null) {
				if (other._point == null || _point == null) {
					return false;
				}
				if (other._point.Length != _point.Length) {
					return false;
				}
				for (int i = 0; i < _point.Length; i++) {
					if (other._point[i] != _point[i]) {
						return false;
					}

				}
			}
			return _valueComparer.Equals(other._value, _value);
		}

		public override int GetHashCode()
		{
			int hash = 0;
			if (_point != null) {
				for (int i = 0; i < _point.Length; i++) {
					hash ^= _point[i].GetHashCode();
				}
			}
			if (_value != null) {
				hash ^= _value.GetHashCode();
			}
			return hash;
		}
	}
}
