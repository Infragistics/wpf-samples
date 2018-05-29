using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace IGExtensions.Framework.Tools
{
    
    public static partial class ArrayTool
    {
        #region In-place by Value
        /// <summary>
        /// </summary>
        /// <remarks>
        /// The list is reordered such that list[0] to list[k-1] are less than list[k] and
        /// all items list[k+1] to list[list.Count-1] are greater than list[k].
        /// <para>
        /// The items within the upper and lower subranges are in no particular order,
        /// and specifically are not sorted.
        /// </para>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">List of values to partition.</param>
        /// <param name="value">Pivot value.</param>
        /// <param name="begin">Index of first item in subrange.</param>
        /// <param name="end">Index of last item in subrange.</param>
        /// <returns>Index of first item greater than the specified pivot value.</returns>
        public static int PartitionByValue<T>(IList<T> values, T value, int begin, int end) where T: IComparable
        {
            int i = begin;
            int j = end;

            while (i <= j)
            {
                while (i <= j && values[i].CompareTo(value) <= 0)
                {
                    ++i;
                }

                while (i < j && values[j].CompareTo(value) > 0)
                {
                    --j;
                }

                if (i < j)
                {
                    values.Swap(i, j);
                    --j;
                }

                ++i;
            }

            return i - 1;
        }
        #endregion

        #region Indirect by Value
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="indices"></param>
        /// <param name="value"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int PartitionByValue<T>(Func<int, T> values, IList<int> indices, T value, int begin, int end) where T : IComparable
        {
            int i = begin;
            int j = end;

            while (i <= j)
            {
                while (i < j && value.CompareTo(values(indices[i])) >= 0)
                {
                    ++i;
                }

                while (i < j && value.CompareTo(values(indices[j])) < 0)
                {
                    --j;
                }

                if (i < j)
                {
                    indices.Swap(i, j);
                    --j;
                }

                ++i;
            }

            return i - 1;
        }
        #endregion

        #region In-place by Order
        /// <summary>
        /// Calculates the kth biggest item in the current list object and partitions
        /// the list accordingly.
        /// </summary>
        /// <remarks>
        /// The list is reordered such that list[0] to list[k-1] are less than list[k] and
        /// all items list[k+1] to list[list.Count-1] are greater than list[k].
        /// <para>
        /// The items within the upper and lower subranges are in no particular order,
        /// and specifically are not sorted.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">Where IComparable.</typeparam>
        /// <param name="list">List object to partition.</param>
        /// <param name="pivotOrder">Pivot item order.</param>
        /// <returns>Pivot item value.</returns>
        public static T PartitionByOrder<T>(this IList<T> list, int pivotOrder) where T : IComparable
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            return PartitionByOrder(list, pivotOrder, 0, list.Count);
        }

        /// <summary>
        /// Calculates the kth biggest item in the specified subrange of the current
        /// list object and partitions it accordingly.
        /// </summary>
        /// <remarks>
        /// The list is reordered such that list[position] to list[position+k-1] are
        /// less than list[position+k] and
        /// all items list[position+k+1] to list[position+Count] are greater than list[k].
        /// <para>
        /// The items within the upper and lower subranges are in no particular order,
        /// and specifically are not sorted.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">Where IComparable.</typeparam>
        /// <param name="list"></param>
        /// <param name="pivotOrder"></param>
        /// <param name="position"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static T PartitionByOrder<T>(this IList<T> list, int pivotOrder, int position, int count) where T : IComparable
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            return PartitionByOrder(list, pivotOrder, position, count, (a, b) => a.CompareTo(b));
        }
        #endregion

        #region Indirect by Order
        /// <summary>
        /// Calculates the kth biggest item in the current list object and partitions
        /// the list accordingly.
        /// </summary>
        /// <remarks>
        /// The list is reordered such that list[0] to list[k-1] are less than list[k] and
        /// all items list[k+1] to list[list.Count-1] are greater than list[k].
        /// <para>
        /// The items within the upper and lower subranges are in no particular order,
        /// and specifically are not sorted.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">Where IComparable.</typeparam>
        /// <param name="list">List object to partition.</param>
        /// <param name="k">Pivot item order.</param>
        /// /// <param name="compare"></param>
        /// <returns>Pivot item value.</returns>
        public static T PartitionByOrder<T>(this IList<T> list, int k, Comparison<T> compare)
        {
            return PartitionByOrder(list, k, 0, list.Count, compare);
        }

        /// <summary>
        /// Calculates the kth biggest item in the specified subrange of the current
        /// list object and partitions it accordingly.
        /// </summary>
        /// <remarks>
        /// The list is reordered such that list[position] to list[position+k-1] are
        /// less than list[position+k] and
        /// all items list[position+k+1] to list[position+Count] are greater than list[k].
        /// <para>
        /// The items within the upper and lower subranges are in no particular order,
        /// and specifically are not sorted.
        /// </para>
        /// </remarks>
        /// <typeparam name="T">Where IComparable.</typeparam>
        /// <param name="list"></param>
        /// <param name="pivotOrder"></param>
        /// <param name="position"></param>
        /// <param name="count"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static T PartitionByOrder<T>(this IList<T> list, int pivotOrder, int position, int count, Comparison<T> compare)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            int left = position;
            int right = position + count - 1;

            for (; ; )
            {
                if (right <= left + 1)
                {
                    if (right == left + 1 && compare(list[right], list[left]) < 0)
                    {
                        list.Swap(left, right);
                    }

                    #region make the partitioning strict
                    // I have the feeling that it should be possible to do away with this
                    // pass by suitably changing the "greater thans" to "greater than or equal tos"

                    int k1 = pivotOrder + 1;
                    T s = list[pivotOrder];

                    for (int i1 = pivotOrder + 1; i1 <= position + count - 1; ++i1)
                    {
                        if (compare(list[i1], s) == 0)
                        {
                            pivotOrder = k1;
                            list.Swap(i1, k1);
                            ++k1;
                        }
                    }
                    #endregion

                    return list[pivotOrder];
                }

                int mid = (left + right) >> 1;
                list.Swap(mid, left + 1);

                if (compare(list[left], list[right]) > 0)
                {
                    list.Swap(left, right);
                }

                if (compare(list[left + 1], list[right]) > 0)
                {
                    list.Swap(left + 1, right);
                }

                if (compare(list[left], list[left + 1]) > 0)
                {
                    list.Swap(left, left + 1);
                }

                int i = left + 1;
                int j = right;
                T a = list[left + 1];

                for (; ; )
                {
                    do
                    {
                        ++i;
                    } while (compare(list[i], a) < 0);

                    do
                    {
                        --j;
                    } while (compare(list[j], a) > 0);

                    if (j < i)
                    {
                        break;
                    }

                    list.Swap(i, j);
                }

                list[left + 1] = list[j];
                list[j] = a;

                if (j >= (pivotOrder + position))
                {
                    right = j - 1;
                }

                if (j <= (pivotOrder + position))
                {
                    left = i;
                }
            }

            // make selection into strict partitioning
        }
        #endregion
    }
}
