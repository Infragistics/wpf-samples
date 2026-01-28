using System;

namespace Infragistics.Extension
{
	/// <summary>
	/// Max-heap collection class.
	/// A max-heap is a binary tree where the nodes always have a key that is 
	/// greater than the keys of their children.
	/// </summary>
	/// <remarks>
	/// Copyright (C) 2009-2012 Rednaxela          (original Java implementation).
	/// Copyright (C) 2012 Olivier Jacot-Descombes (port to C#).
	/// </remarks>
	/// <typeparam name="TKey">Type of key</typeparam>
	/// <typeparam name="TValue">Type of value</typeparam>
	public class MaxHeap<TKey, TValue> : BinaryHeap<TKey, TValue>, IMaxHeap<TKey, TValue>
		where TKey : IComparable<TKey>
	{
			public MaxHeap()
				: base(defaultCapacity, 1)
			{
			}

			public MaxHeap(int capacity)
				: base(capacity, 1)
			{
			}

			public void RemoveMax()
			{
				RemoveTip();
			}

			public void ReplaceMax(TKey key, TValue value)
			{
				ReplaceTip(key, value);
			}

			public TValue GetMax()
			{
				return GetTip();
			}

			public TKey GetMaxKey()
			{
				return GetTipKey();
			}
	}
}
