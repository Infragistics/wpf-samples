using System;
using System.Reflection;

namespace System  
{
    public static class PropertyEx
    {
        /// <summary>
        /// Gets property value
        /// </summary>
        /// <remarks> Usage:
        /// <para> 
        /// DateTime now = DateTime.Now;
        /// int min = GetPropValue<int>(now, "TimeOfDay.Minutes");
        /// int hrs = now.GetPropValue<int>("TimeOfDay.Hours");
        /// </para>
        /// </remarks>
        public static object GetPropValue(this object obj, string name)
        {
            foreach (var part in name.Split('.'))
            {
                if (obj == null) { return null; }

                var type = obj.GetType();
                var info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this object obj, string name)
        {
            var retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
        public static object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}