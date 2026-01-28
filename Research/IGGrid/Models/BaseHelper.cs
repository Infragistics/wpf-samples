using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace IGGrid.Models
{
    public class BaseHelper<T> : ICustomTypeProvider, INotifyPropertyChanged
    {
        private static List<CustomPropertyInfo> propertyList = new List<CustomPropertyInfo>();
        private Dictionary<string, object> propertyDictionary;
        private CustomType customType;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged

        public BaseHelper()
        {
            propertyDictionary = new Dictionary<string, object>();
            foreach (var property in this.GetCustomType().GetProperties())
            {
                propertyDictionary.Add(property.Name, null);
            }
        }

        public static void AddProperty(string name, Type propertyType)
        {
            if (!CheckIfNameExists(name))
                propertyList.Add(new CustomPropertyInfo(name, propertyType));
        }

        public static bool CheckIfNameExists(string name)
        {
            if ((from p in propertyList select p.Name).Contains(name) || (from p in typeof(T).GetProperties() select p.Name).Contains(name))
            {
                return true;
            }
            else return false;
        }

        public void SetPropertyValue(string propertyName, object value)
        {
            CustomPropertyInfo propertyInfo = (from prop in propertyList where prop.Name == propertyName select prop).FirstOrDefault();

            if (!propertyDictionary.ContainsKey(propertyName))
            {
                if (propertyInfo != null)
                {
                    if (propertyDictionary == null)
                    {
                        propertyDictionary = new Dictionary<string, object>();
                    }

                    propertyDictionary.Add(propertyInfo.Name, null);
                }
                else
                {
                    throw new Exception("No property with name: " + propertyName);
                }
            }

            if (ValidateValueType(value, propertyInfo.t))
            {
                if (propertyDictionary[propertyName] != value)
                {
                    propertyDictionary[propertyName] = value;
                    NotifyPropertyChanged(propertyName);
                }
            }
            else
            {
                throw new Exception("Validate type error.");
            }
        }

        public static void CleanPropertyList()
        {
            propertyList.Clear();
        }

        private bool ValidateValueType(object value, Type type)
        {
            if (value == null)
            {
                if (!type.IsValueType)
                {
                    return true;
                }
                else
                {
                    return (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
                }
            }
            else
            {
                return type.IsAssignableFrom(value.GetType());
            }
        }

        public object GetPropertyValue(string propertyName)
        {
            if (propertyDictionary.ContainsKey(propertyName))
                return propertyDictionary[propertyName];
            else
                throw new Exception("No property with name: " + propertyName);
        }

        public PropertyInfo[] GetProperties()
        {
            return this.GetCustomType().GetProperties();
        }

        public Type GetCustomType()
        {
            if (customType == null)
            {
                customType = new CustomType(typeof(T));
            }
            return customType;
        }

        private class CustomType : Type
        {
            Type t;
            public CustomType(Type type)
            {
                t = type;
            }

            public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
            {
                PropertyInfo[] clrProperties = t.GetProperties(bindingAttr);
                if (clrProperties != null)
                {
                    return clrProperties.Concat(propertyList).ToArray();
                }
                else
                    return propertyList.ToArray();
            }

            protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr,
                Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
            {
                PropertyInfo propertyInfo = (from prop in GetProperties(bindingAttr) where prop.Name == name select prop).FirstOrDefault();
                if (propertyInfo == null)
                {
                    propertyInfo = (from prop in propertyList where prop.Name == name select prop).FirstOrDefault();
                }
                return propertyInfo;
            }

            public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder,
                object target, object[] args, ParameterModifier[] modifiers, System.Globalization.CultureInfo culture, string[] namedParameters)
            {
                return t.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
            }

            public override string Name
            {
                get { return t.Name; }
            }

            #region Default Implementation

            public override Assembly Assembly
            {
                get { throw new NotImplementedException(); }
            }

            public override string AssemblyQualifiedName
            {
                get { throw new NotImplementedException(); }
            }

            public override Type BaseType
            {
                get { throw new NotImplementedException(); }
            }

            public override string FullName
            {
                get { throw new NotImplementedException(); }
            }

            public override Guid GUID
            {
                get { throw new NotImplementedException(); }
            }

            protected override TypeAttributes GetAttributeFlagsImpl()
            {
                throw new NotImplementedException();
            }

            protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
            {
                throw new NotImplementedException();
            }

            public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override Type GetElementType()
            {
                throw new NotImplementedException();
            }

            public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override EventInfo[] GetEvents(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override FieldInfo GetField(string name, BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override FieldInfo[] GetFields(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override Type GetInterface(string name, bool ignoreCase)
            {
                throw new NotImplementedException();
            }

            public override Type[] GetInterfaces()
            {
                throw new NotImplementedException();
            }

            public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
            {
                throw new NotImplementedException();
            }

            public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override Type GetNestedType(string name, BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            public override Type[] GetNestedTypes(BindingFlags bindingAttr)
            {
                throw new NotImplementedException();
            }

            protected override bool HasElementTypeImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsArrayImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsByRefImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsCOMObjectImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsPointerImpl()
            {
                throw new NotImplementedException();
            }

            protected override bool IsPrimitiveImpl()
            {
                throw new NotImplementedException();
            }

            public override Module Module
            {
                get { throw new NotImplementedException(); }
            }

            public override string Namespace
            {
                get { throw new NotImplementedException(); }
            }

            public override Type UnderlyingSystemType
            {
                get { throw new NotImplementedException(); }
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            public override object[] GetCustomAttributes(bool inherit)
            {
                throw new NotImplementedException();
            }

            public override bool IsDefined(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            #endregion // Default implementation
        }

        private class CustomPropertyInfo : PropertyInfo
        {
            public string n;
            public Type t;

            public CustomPropertyInfo(string name, Type type)
            {
                n = name;
                t = type;
            }

            public override Type PropertyType
            {
                get { return t; }
            }

            public override string Name
            {
                get { return n; }
            }

            public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
            {

                return obj.GetType().GetMethod("GetPropertyValue").Invoke(obj, new[] { n });
            }

            public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, System.Globalization.CultureInfo culture)
            {
                obj.GetType().GetMethod("SetPropertyValue").Invoke(obj, new[] { n, value });
            }

            #region Default implementation

            public override PropertyAttributes Attributes
            {
                get { throw new NotImplementedException(); }
            }

            public override bool CanRead
            {
                get { throw new NotImplementedException(); }
            }

            public override bool CanWrite
            {
                get { throw new NotImplementedException(); }
            }

            public override MethodInfo[] GetAccessors(bool nonPublic)
            {
                throw new NotImplementedException();
            }

            public override MethodInfo GetGetMethod(bool nonPublic)
            {
                throw new NotImplementedException();
            }

            public override ParameterInfo[] GetIndexParameters()
            {
                throw new NotImplementedException();
            }

            public override MethodInfo GetSetMethod(bool nonPublic)
            {
                throw new NotImplementedException();
            }

            public override Type DeclaringType
            {
                get { throw new NotImplementedException(); }
            }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                //throw new NotImplementedException();
                return null;
            }

            public override object[] GetCustomAttributes(bool inherit)
            {
                //throw new NotImplementedException();
                return null;
            }

            public override bool IsDefined(Type attributeType, bool inherit)
            {
                throw new NotImplementedException();
            }

            public override Type ReflectedType
            {
                get { throw new NotImplementedException(); }
            }

            #endregion // Default implementation
        }
    }
}
