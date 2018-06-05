using System.Collections.Generic; 
using System.Linq;

namespace System // IGExtensions.Framework.Extensions
{
    public static class ObjectEx
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }
        public static bool Between<T>(this T value, T from, T to) where T : IComparable<T>
        {
            return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
        }
        public static string GetTypeName(this object obj)
        {
            return obj.GetType(false);
        }
        public static string GetType(this object obj, bool includeNamespace = true)
        {
            var type = obj.GetType().ToString();
            return includeNamespace ? type : type.Split('.').ToList().LastItem();
        }
       
    }
}