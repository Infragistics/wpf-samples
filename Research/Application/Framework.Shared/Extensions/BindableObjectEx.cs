using Infragistics.XamarinForms;
using System;

namespace Xamarin.Forms
{
    public static class BindableObjectEx
    {
        /// <summary>
        /// Checks if the bindable object has default brush for specified property
        /// </summary> 
        public static bool IsDefaultBrush(this BindableObject bindable, BindableProperty prop)
        {
            var brushValue = bindable.GetPropertyValue(prop.PropertyName) as Brush;
            var brushDefault = prop.DefaultValue as Brush;
          
            return brushValue.Equals(brushDefault);
        }

        /// <summary>
        /// Checks if the bindable object has default value for specified property
        /// </summary> 
        public static bool IsDefault(this BindableObject bindable, BindableProperty prop)
        {
            if (prop.ReturnType == typeof(Brush))
            {
                return bindable.IsDefaultBrush(prop);
            } 
            //NOTE add support for other IG types

            var value = bindable.GetPropertyValue(prop.PropertyName);

            if (value == null) return true;

            return value.Equals(prop.DefaultValue); 
        }
    }
}