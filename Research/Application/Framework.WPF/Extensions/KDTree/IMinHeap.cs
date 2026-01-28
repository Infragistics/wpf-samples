using System;

namespace Infragistics.Extension
{
	/// <summary>
	/// Interface for a min-heap.
	/// A min-heap is a binary tree where the nodes always have a key that is 
	/// less than the keys of their children.
	/// </summary>
	/// <remarks>
	/// Copyright (C) 2009-2012 Rednaxela          (original Java implementation).
	/// Copyright (C) 2012 Olivier Jacot-Descombes (port to C#).
	/// </remarks>
	/// <typeparam name="TKey">Type of key</typeparam>
	/// <typeparam name="TValue">Type of value</typeparam>
	public interface IMinHeap<TKey, TValue>
		where TKey : IComparable<TKey>
	{
		int Count { get; }

		void Add(TKey key, TValue value);
		void ReplaceMin(TKey key, TValue value);
		void RemoveMin();

		TValue GetMin();
		TKey GetMinKey();
	}
}
