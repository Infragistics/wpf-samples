using System;

namespace Infragistics.Extension
{
	/// <summary>
	/// Min-heap collection class.
	/// A min-heap is a binary tree where the nodes always have a key that is 
	/// less than the keys of their children.
	/// </summary>
	/// <remarks>
	/// Copyright (C) 2009-2012 Rednaxela          (original Java implementation).
	/// Copyright (C) 2012 Olivier Jacot-Descombes (port to C#).
	/// </remarks>
	/// <typeparam name="TKey">Type of key</typeparam>
	/// <typeparam name="TValue">Type of value</typeparam>
	public class MinHeap<TKey, TValue> : BinaryHeap<TKey, TValue>, IMinHeap<TKey, TValue>
		where TKey : IComparable<TKey>
	{
			public MinHeap()
				: base(defaultCapacity, -1)
			{
			}

			public MinHeap(int capacity)
				: base(capacity, -1)
			{
			}

			public void RemoveMin()
			{
				RemoveTip();
			}

			public void ReplaceMin(TKey key, TValue value)
			{
				ReplaceTip(key, value);
			}

			public TValue GetMin()
			{
				return GetTip();
			}

			public TKey GetMinKey()
			{
				return GetTipKey();
			}
	}
}
