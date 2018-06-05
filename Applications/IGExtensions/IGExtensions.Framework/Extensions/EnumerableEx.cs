using System.Collections.ObjectModel;
using System.Linq;

namespace System.Collections.Generic //System.Linq
{
    public static class EnumerableGenEx
    {
        /// <summary> Gets element at index or it modulus index </summary>
        public static T ElementAtOrNext<T>(this IEnumerable<T> list, int index)
        {
            if (list == null || list.Count() == 0)
                return default(T);
            
            var itemIndex = index % list.Count();
            return list.ElementAt(itemIndex);              
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
        {
            var collection = new ObservableCollection<T>();
            foreach (var e in list)
                collection.Add(e);
            return collection;
        }
        public static T LastItem<T>(this IEnumerable<T> items)
        {
            var list = items as IList<T> ?? items.ToList();
            
            if (!list.Any())
                return default(T);

            var count = list.Count();
            return list.At(count - 1);
        }
      
        public static T At<T>(this IEnumerable<T> items, int index)
        {
            var list = items as IList<T> ?? items.ToList();

            if (index < 0 || list.Count() <= index)
                return default(T);

            return list[index];
        }

        public static IEnumerable<T> Reverse<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            } 
        }
        /// <summary>
        /// Performs the specified action on each object in the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">The collection.</param>
        /// <param name="action">The action to perform.</param>
        /// <returns></returns>
        /// <remarks>This is an extension method provided by <seealso cref="EnumerableEx"/></remarks>
        public static IEnumerable<T> Each<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var o in values)
            {
                action(o);
            }

            return values;
        }
        //public static int Count<T>(this IEnumerable<T> source)
        //{
        //    var c = source as ICollection<T>;
        //    if (c != null)
        //        return c.Count;

        //    var collection = source as ICollection;
        //    if (collection != null)
        //    {
        //        return collection.Count;
        //    }

        //    int result = 0;
        //    using (IEnumerator<T> enumerator = source.GetEnumerator())
        //    {
        //        while (enumerator.MoveNext())
        //            result++;
        //    }
        //    return result;
        //}
    }
}
namespace System.Collections //.Generic //System.Linq
{
    public static class EnumerableEx
    {
        public static int Count(this IEnumerable source)
        {
            var collection = source as ICollection;
            if (collection != null)
            {
                return collection.Count;
            }
            int result = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext())
                result++;

            //using (IEnumerator enumerator = source.GetEnumerator())
            //{
            //    while (enumerator.MoveNext())
            //        result++;
            //}
            return result;
        }
    }
}