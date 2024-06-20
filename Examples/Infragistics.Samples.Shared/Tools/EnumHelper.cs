using System;
using System.Reflection;

namespace Infragistics.Samples.Framework.Utility
{
    /// <summary>
    /// Provides useful methods that simplify working with enums in samples.
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// Returns an array of lookup objects that contain the name and value of 
        /// the specified Enum type.  This is useful for samples that allow the user
        /// to select the various enum values used by a control's property.
        /// </summary>
        /// <typeparam name="TEnum">The type of enum to inspect.</typeparam>
        public static EnumValue<TEnum>[] GetValues<TEnum>()
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("TEnum is not an enum type.");

            FieldInfo[] fields = typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static);

            EnumValue<TEnum>[] enumValues = new EnumValue<TEnum>[fields.Length];
            for (int i = 0; i < enumValues.Length; ++i)
            {
                string name = fields[i].Name;
                TEnum value = (TEnum)fields[i].GetValue(null);
                enumValues[i] = new EnumValue<TEnum>(name, value);
            }

            return enumValues;
        }        
    }

    public struct EnumValue<TEnum>
    {
        public readonly string Name;
        public readonly TEnum Value;

        internal EnumValue(string name, TEnum value)
        {
            this.Name = name;
            this.Value = value;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}