using System;
using System.Collections.Generic;

namespace IGExtensions.Framework.Tools
{
    public static partial class ArrayTool
    {
        /// <summary>
        /// Search for key in indexed list. This method is typically used where
        /// the list doesn't contain objects of the same type as the key, or where
        /// the key isn't a literal match to the list contents.
        /// </summary>
        /// <remarks>
        /// The list must be strongly sorted by key. 
        /// </remarks>
        /// <typeparam name="T1"></typeparam>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <param name="comparator"></param>
        /// <returns></returns>
        public static int BinarySearch<T1>(T1 key, int count, Func<int, T1, int> comparator)
        {
            int b = 0;
            int e = count - 1;

            while (b <= e)
            {
                int m = (b + e) / 2;
                int c = comparator(m, key);

                if (c == 0)
                {
                    return m;
                }

                if (c > 0)
                {
                    e = m - 1;
                    continue;
                }

                if (c < 0)
                {
                    b = m + 1;
                    continue;
                }

                return m;
            }

            return ~b;
        }

        /// <summary>
        /// Swap two list items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">Current list object.</param>
        /// <param name="a">Index of first item to swap.</param>
        /// <param name="b">Index of second item to swap.</param>
        public static void Swap<T>(this IList<T> list, int a, int b)
        {
            T t = list[a];
            list[a] = list[b];
            list[b] = t;
        }

        public static IList<int> Split<T>(this IList<T> list, Func<T, bool> isValid)
        {
            int count = list!=null? list.Count: 0;
            var ret = new List<int>();

            for (int i = 0; i < count; ++i)
            {
                if (isValid(list[i]))
                {
                    int b = i;

                    do
                    {
                        ++i;
                    } while (i < count && isValid(list[i]));

                    ret.Add(b);
                    ret.Add(i-1);
                }
            }

            return ret;
        }
    }
}
