using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infragistics.Extension
{
	public class KDPointsCollection<T> : KDCollectionBase<T, double[]>, ICollection<double[]>
	{
		internal KDPointsCollection(KDTree<T> kdTree)
			: base(kdTree)
		{
		}

		#region ICollection<double[]> Members

		public void Add(double[] point)
		{
			throw new NotSupportedException("The KDPointsCollection is read-only.");
		}

		public void Clear()
		{
			throw new NotSupportedException("The KDPointsCollection is read-only.");
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public bool Remove(double[] point)
		{
			throw new NotSupportedException("The KDPointsCollection is read-only.");
		}

		#endregion

		#region IEnumerable<double[]> Members

		public override IEnumerator<double[]> GetEnumerator()
		{
			Stack<KDNode<T>> stack = new Stack<KDNode<T>>();
			stack.Push(_kdTree);
			do {
				KDNode<T> current = stack.Pop();
				if (current._leafEntries == null) {
					stack.Push(current._left);
					stack.Push(current._right);
				} else {
					for (int i = 0; i < current._count; i++) {
						yield return current._leafEntries[i].Point;
					}
				}
			} while (stack.Count > 0);
		}

		#endregion
	}
}
