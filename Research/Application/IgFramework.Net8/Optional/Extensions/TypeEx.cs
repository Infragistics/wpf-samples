using Infragistics.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class TypeEx
    {
        public static PropertyInfo GetProperty(this Type type, string propertyName)
        {
            Logs.All(">>>> GetProperty: " + propertyName + " from " + type.Name );
            
            // searching for a property on current type
            var typeInfo = type.GetTypeInfo();
            foreach (var propInfo in typeInfo.DeclaredProperties)
            {
                if (propInfo.Name == propertyName)
                    return propInfo;
            } 

            // searching for a property on base type
            var baseType = typeInfo.BaseType;
            if (baseType != null)
                return baseType.GetProperty(propertyName);

            Logs.Warning(">>>> GetProperty: " + propertyName + " does not exist on " + type.Name);

            return null;
        }
        public static List<PropertyInfo> GetProperties(this Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return typeInfo.DeclaredProperties.ToList();
        }

        public static List<PropertyInfo> GetProperties<T>(this Type type)
        {
            var properties = new List<PropertyInfo>();
            foreach (var property in type.GetProperties())
            {
                if (property.PropertyType == typeof(T))
                {
                    properties.Add(property);
                }
            }
            return properties;
        }
        public static string ToReadableString(this Type type)
        {
            var str = type.ToString();
            str = str.Replace("System.String", "string");
            str = str.Replace("System.Boolean", "bool");
            str = str.Replace("System.Double", "double");
            str = str.Replace("System.Integer", "int");
            str = str.Replace("System.Int32", "int");
            str = str.Replace("System.Windows.Point", "Point");
            str = str.Replace("System.Collections.Generic.", "");
            str = str.Replace("System.Windows.Threading.", "");
            str = str.Replace("System.Windows.", "");
            str = str.Replace("Infragistics.Framework.", "");
            str = str.Replace("Infragistics.Controls.Maps.", "");
            str = str.Replace("`1", "");
            str = str.Replace("`2", "");
            str = str.Replace("[", "<");
            str = str.Replace("]", ">");
            return str;
        }
    }
}
