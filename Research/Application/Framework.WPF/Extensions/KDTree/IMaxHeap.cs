using System;

namespace Infragistics.Extension
{
	/// <summary>
	/// Interface for a max-heap.
	/// A max-heap is a binary tree where the nodes always have a key that is 
	/// greater than the keys of their children.
	/// </summary>
	/// <remarks>
	/// Copyright (C) 2009-2012 Rednaxela          (original Java implementation).
	/// Copyright (C) 2012 Olivier Jacot-Descombes (port to C#).
	/// </remarks>
	/// <typeparam name="TKey">Type of key</typeparam>
	/// <typeparam name="TValue">Type of value</typeparam>
	public interface IMaxHeap<TKey, TValue>
		where TKey : IComparable<TKey>
	{
		int Count { get; }

		void Add(TKey key, TValue value);
		void ReplaceMax(TKey key, TValue value);
		void RemoveMax();

		TValue GetMax();
		TKey GetMaxKey();
	}
}
