//#define METHODINFO

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace IGExtensions.Framework.Tools
{
    /// <summary>
    /// A reflection strategy that uses either traditional reflection or compiled lambda expressions
    /// to get property values from an object.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Constructs the fast reflection helper.
        /// </summary>
        public Property()
        {
        }

        public string PropertyPath
        {
            get { return propertyPath; }
            set
            {
                if(value!=PropertyPath)
                {
                    propertyPath = value;

                    int separator = PropertyPath != null ? PropertyPath.IndexOf('.') : -1;

                    if (separator >= 0)
                    {
                        PropertyName = value.Substring(0, separator);
                        succ = new Property() { PropertyPath = value.Substring(separator + 1) };
                    }
                    else
                    {
                        PropertyName = PropertyPath;
                        succ = null;
                    }
                }
            }
        }
        private string propertyPath;

        /// <summary>
        /// Gets or sets the property name of the current Property object.
        /// </summary>
        public string PropertyName
        {
            get { return propertyName; }
            set
            {
                if (propertyName != value)
                {
                    type = null;

#if METHODINFO
                    getPropertyValueDictionary = new Dictionary<Type, MethodInfo>();
#else
                    getPropertyValueDictionary = new Dictionary<Type, Func<object, object>>();
#endif
                    propertyName = value;
                    succ = null;
                }
            }
        }
        private string propertyName = null;
        private Property succ = null;

        /// <summary>
        /// Gets the property value from the specified item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Property value or null if the property value cannot be determined.</returns>
        public object GetValue(object item)
        {
            if (item == null)
            {
                return null;
            }

            return GetValue(item.GetType(), item);
        }


        private Type type = null;

#if METHODINFO
        private MethodInfo methodInfo = null;
        private Dictionary<Type, MethodInfo> getPropertyValueDictionary = new Dictionary<Type, MethodInfo>();

        private object GetValue(Type itemType, object item)
        {
            if (string.IsNullOrWhiteSpace(PropertyName))
            {
                return item;
            }

            if (itemType != type)
            {
                type = itemType;
                methodInfo = null;

                if (!getPropertyValueDictionary.TryGetValue(type, out methodInfo))
                {
                    if (getPropertyValueDictionary.Count > 32)
                    {
                        getPropertyValueDictionary.Clear();  // don't want any runaways
                    }

                    PropertyInfo propertyInfo = type.GetProperty(propertyName);

                    if (propertyInfo != null)
                    {
                        methodInfo = propertyInfo.GetGetMethod();
                    }

                    getPropertyValueDictionary.Add(type, methodInfo);
                }
            }

            if (methodInfo != null)
            {
                return succ != null ? succ.GetValue(methodInfo.Invoke(item, null)) : methodInfo.Invoke(item, null);
            }

            return null;
        }
#else
        /// <summary>
        /// Gets the property value for the specified item.
        /// </summary>
        /// <param name="itemType">The item type.</param>
        /// <param name="item">The item containing the property.</param>
        /// <returns>Property value or null if the property value cannot be determined.</returns>
        private object GetValue(Type itemType, object item)
        {
            if (string.IsNullOrWhiteSpace(PropertyName))
            {
                return item;
            }

            if (itemType != type)
            {
                type = itemType;

                getPropertyValue = null;
                if (!getPropertyValueDictionary.TryGetValue(type, out getPropertyValue))
                {
                    if (getPropertyValueDictionary.Count > 32)
                    {
                        getPropertyValueDictionary.Clear();  // don't want any runaways
                    }

                    PropertyInfo propertyInfo = type.GetProperty(propertyName);

                    if (propertyInfo != null)
                    {
                        ParameterExpression param = Expression.Parameter(typeof(object), "obj");
                        UnaryExpression conversion;

                        if (type.IsValueType && !(type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>))))
                        {
                            conversion = Expression.Convert(param, type);
                        }
                        else
                        {
                            conversion = Expression.TypeAs(param, type);
                        }

                        UnaryExpression prop = Expression.TypeAs(Expression.Property(conversion, propertyInfo), typeof(object));
                        getPropertyValue = (Func<object, object>)Expression.Lambda(prop, param).Compile();
                    }

                    getPropertyValueDictionary.Add(type, getPropertyValue);
                }
            }

            if (getPropertyValue != null)
            {
                return succ!=null? succ.GetValue(getPropertyValue(item)): getPropertyValue(item);
            }

            return null;
        }
        private Func<object, object> getPropertyValue = null;
        private Dictionary<Type, Func<object, object>> getPropertyValueDictionary = new Dictionary<Type, Func<object, object>>();
#endif
    }
}
