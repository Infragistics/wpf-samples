using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Infragistics.Samples.Shared.Models
{
    #region Inheritance Hierarchy
    public class Class
    {
        /// <summary>
        /// The methods of the class.
        /// </summary>
        public IEnumerable<Method> Methods { get; set; }

        /// <summary>
        /// The interfaces, which the class implements.
        /// </summary>
        public IEnumerable<Interface> Interfaces { get; set; }

        /// <summary>
        /// The properties of the class.
        /// </summary>
        public IEnumerable<Property> Properties { get; set; }

        /// <summary>
        /// The derived classes.
        /// </summary>
        public IEnumerable<Class> DerivedClasses { get; set; }

        /// <summary>
        /// The name of the class.
        /// </summary>
        public string ClassName { get; set; }
    }

    public class Method
    {
        /// <summary>
        /// Creates a new <see cref="Method"/> object.
        /// </summary>
        public Method(MethodInfo methodInfo)
        {
            this.MethodName = methodInfo.Name;
        }

        /// <summary>
        /// The name of the method.
        /// </summary>
        public string MethodName { get; set; }
    }

    public class Interface
    {       
        /// <summary>
        /// Creates a new <see cref="Interface"/> object.
        /// </summary>
        public Interface(Type iface)
        {
            this.InterfaceName = iface.Name;
        }

        /// <summary>
        /// The name of the interface.
        /// </summary>
        public string InterfaceName { get; set; }
    }

    public class Property
    {
        /// <summary>
        /// Creates a new <see cref="Property"/> object.
        /// </summary>
        public Property(PropertyInfo propertyInfo)
        {
            this.PropertyName = propertyInfo.Name;
        }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string PropertyName { get; set; }
    }
    #endregion
}
