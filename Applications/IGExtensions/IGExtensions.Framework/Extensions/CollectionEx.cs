using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace System.Collections.ObjectModel  
{
    public static class CollectionEx
    {

        //public static int LastIndexOf<TSource>(this Collection<TSource> collection) //where T : Series
        //{
        //    var index = -1;
        //    for (var i = 0; i < collection.Count; i++)
        //    {
        //        if (collection.GetType() == typeof(T))
        //        {
        //            index = i;
        //        }
        //    }
        //    return index;
        //}
        //public static List<T> ToList<T>(this IEnumerable<T> list)
        //{
        //    var collection = new List<T>();
        //    foreach (var e in list)
        //        collection.Add(e);
        //    return collection;
        //}
        //public static T LastItem<T>(this List<T> list)
        //{
        //    if (list == null || list.Count == 0)
        //        return default(T);

        //    var count = list.Count();
        //    return list[count - 1];
        //}
        //public static T FirstItem<T>(this List<T> list)
        //{
        //    if (list == null || list.Count == 0)
        //        return default(T);
            
        //    return list[0];
        //}
       
    }
}