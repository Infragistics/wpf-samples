using System;
using System.ComponentModel;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// Represents a strongly typed sorted list of objects accessed by their priority. Provides
    /// methods to add and remove items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class PriorityQueue<T>
    {
        /// <summary>
        /// Initializes a new instance of the PriorityQueue class. 
        /// </summary>
        public PriorityQueue()
        {
            Count = 0;
            Capacity = 15;
            heap = new HeapItem[Capacity];
        }

        /// <summary>
        /// Removes and returns the highest priority object from the priority queue.
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            T result = heap[0].Item;
            --Count;

            TrickleDown(0, heap[Count]);

            return result;
        }
        /// <summary>
        /// Inserts an item into the current priority queue with the specified priority.
        /// </summary>
        /// <param name="item">The object to push onto the priority queue.</param>
        /// <param name="priority">The object's priority.</param>
        public void Enqueue(T item, double priority)
        {
            if (Count == Capacity)
            {
                GrowHeap();
            }

            ++Count;

            BubbleUp(Count - 1, new HeapItem(item, priority));
        }

        /// <summary>
        /// Gets the number of items in the current priority queue.
        /// 
        /// </summary>
        public int Count { get; private set; }
        private int Capacity { get; set; }

        private struct HeapItem
        {
            public HeapItem(T item, double priority)
                : this()
            {
                Item = item;
                Priority = priority;
            }
            public T Item { get; private set; }
            public double Priority { get; private set; }
        }
        private HeapItem[] heap;

        private void BubbleUp(int index, HeapItem he)
        {
            int parent = Parent(index);

            while (index > 0 && heap[parent].Priority < he.Priority)
            {
                heap[index] = heap[parent];
                index = parent;
                parent = Parent(index);
            }

            heap[index] = he;
        }
        private void TrickleDown(int index, HeapItem he)
        {
            int child = LeftChild(index);

            while (child < Count)
            {
                if (child + 1 < Count && heap[child].Priority < heap[child + 1].Priority)
                {
                    ++child;
                }

                heap[index] = heap[child];
                index = child;
                child = LeftChild(index);
            }

            BubbleUp(index, he);
        }
        private void GrowHeap()
        {
            Capacity = (Capacity * 2) + 1;
            HeapItem[] newHeap = new HeapItem[Capacity];
            System.Array.Copy(heap, 0, newHeap, 0, Count);
            heap = newHeap;
        }

        private static int Parent(int index)
        {
            return (index - 1) / 2;
        }
        private static int LeftChild(int index)
        {
            return (index * 2) + 1;
        }
    }
}
