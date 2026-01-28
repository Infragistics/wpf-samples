using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infragistics.Extension
{
	/// <summary>
	/// Base class for the Values and Points collections of KDTree.
	/// </summary>
	/// <typeparam name="T">Type of values as defined in KDTree&lt;T&gt;."/></typeparam>
	/// <typeparam name="U">Type of values implemented by the Values and Points collections.</typeparam>
	public abstract class KDCollectionBase<T, U> : IEnumerable<U>
	{
		protected KDTree<T> _kdTree;

		internal KDCollectionBase(KDTree<T> kdTree)
		{
			_kdTree = kdTree;
		}

		public KDTree<T> KDTree { get { return _kdTree; } }

		#region Implements ICollection<T> members in derived class for Type U

		public int Count
		{
			get { return _kdTree._count; }
		}

		public bool Contains(U value)
		{
			return this.Contains(value, null); // The null parameter prevents a recursive call to itself and calls the LINQ extension method.
		}

		public void CopyTo(U[] array, int arrayIndex)
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
			if (array.Length - arrayIndex < _kdTree._count) {
				throw new ArgumentException("The number of elements is greater than the available space from `arrayIndex` to the end of the destination array.");
			}

			int i = arrayIndex;
			foreach (U item in this) {
				array[i++] = item;
			}
		}

		#endregion

		#region IEnumerable<U> Members

		public abstract IEnumerator<U> GetEnumerator();

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
