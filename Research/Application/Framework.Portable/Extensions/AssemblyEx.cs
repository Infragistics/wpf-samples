using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 

namespace System.Reflection
{
    public static class AssemblyEx
    {
        public static List<Type> GetTypesList(this Assembly assembly)
        { 
            var infos = assembly.DefinedTypes.ToList();
            var types = new List<Type>();
            foreach (var item in infos)
            {
                types.Add(item.AsType());
            }
            //types = types.OrderBy(t => t.FullName).ToList();

            return types;
        }
        /// <summary>
        /// Gets stream of embedded resource file in assembly for specified name
        /// </summary> 
        public static Stream GetResourceStream(this Assembly assembly, string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentException("resourceName");

            resourceName = resourceName.ToLower();
            var resurces = assembly.GetManifestResourceNames();
            foreach (var name in resurces)
            {
                if (name.ToLower().EndsWith(resourceName))
                    return assembly.GetManifestResourceStream(name);
            }
            return null;
        }
        /// <summary>
        /// Gets stream of embedded resource names in specified assembly  
        /// </summary> 
        public static List<string> GetResourceNames(this Assembly assembly)
        {
            var resurces = assembly.GetManifestResourceNames().ToList();
            resurces.Sort(); 
            return resurces;
        }
    }
}

namespace System.Linq
{
    public static class LinqEx
    {
        public static List<List<TSource>> ToLists<TSource>(this IEnumerable<IEnumerable<TSource>> source)
        {
            var lists = new List<List<TSource>>();
            foreach (var collection in source)
            {
                lists.Add(collection.ToList());
            }
            return lists;
        }
        public static TSource[][] ToArrays<TSource>(this IEnumerable<IEnumerable<TSource>> source)
        {
            var sourceList = source.ToList();
            var arrays = new TSource[sourceList.Count][];
            for (int i = 0; i < sourceList.Count; i++)
            {
                arrays[i] = sourceList[i].ToArray();
            }
            return arrays;
        }
    }
}

namespace System.Collections.Generic
{
    //public static class ListEx
    //{
    //    /// <summary>
    //    /// Gets an element at modulus index of list or null if the list is null or empty   
    //    /// </summary> 
    //    public static T ElementAtMod<T>(this IList<T> list, int index)
    //    {
    //        if (list == null) return default(T);
    //        if (list.Count == 0) return default(T);
    //        return list[index % list.Count];
    //    }
    //}
}