using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
    //public static class TypeEx
    //{
    //    public static string ToReadableString(this Type type)
    //    {
    //        var str = type.ToString();
    //        str = str.Replace("System.String", "string");
    //        str = str.Replace("System.Boolean", "bool");
    //        str = str.Replace("System.Double", "double");
    //        str = str.Replace("System.Integer", "int");
    //        str = str.Replace("System.Int32", "int");
    //        str = str.Replace("System.Windows.Point", "Point");
    //        str = str.Replace("System.Collections.Generic.", "");
    //        str = str.Replace("System.Windows.Threading.", "");
    //        str = str.Replace("System.Windows.", "");
    //        str = str.Replace("Infragistics.Framework.", "");
    //        str = str.Replace("Infragistics.Controls.Maps.", ""); 
    //        str = str.Replace("`1", "");
    //        str = str.Replace("`2", "");
    //        str = str.Replace("[", "<");
    //        str = str.Replace("]", ">"); 
    //        return str;
    //    }
    //}
    //public static class PropertyEx
    //{
        //public static T GetPropertyValue<T>(this object item, string propertyName)
        //{
        //    if (string.IsNullOrEmpty(propertyName))
        //    {
        //        return default(T);
        //    }
        //    var itemType = item.GetType();
        //    var propertyInfo = itemType.GetProperty(propertyName);
            
        //    return item.GetPropertyValue<T>(propertyInfo);
        //}
        //public static T GetPropertyValue<T>(this object item, PropertyInfo property)
        //{
        //    if (property == null)
        //    {
        //        return default(T);
        //    }
        //    if (string.IsNullOrEmpty(property.Name))
        //    {
        //        return default(T);
        //    }
           
        //    return (T)property.GetValue(item, null);
        //}

        //public static List<PropertyInfo> GetProperties(this object item)
        //{
        //    var itemType = item.GetType();
        //    var properties = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        //    return properties.ToList();
        //}
        //public static List<PropertyInfo> GetProperties<T>(this object item)
        //{
        //    var properties = new List<PropertyInfo>();
        //    foreach (var property in item.GetProperties())
        //    {
        //        if (property.PropertyType == typeof(T))
        //        {
        //            properties.Add(property);
        //        }
        //    }
        //    return properties;
        //}
    //}

    public class DataStats
    {
        public DataStats()
        {
            Columns = new Dictionary<string, DataColumnBase>();

        }

        public Dictionary<string, DataColumnBase> Columns { get; set; }
        public int DataCount { get; internal set; }
        public Type DataType { get; internal set; }
        public Type ItemType { get; internal set; }

        public static DataStats Create(object items)
        { 
            var stats = new DataStats();

            stats.DataCount = 0;
            stats.Columns = new Dictionary<string, DataColumnBase>();

            if (items == null) return stats;

            stats.DataType = items.GetType();

            var list = items as IEnumerable<object>;

            if (list == null) return stats;
            if (list.Count() == 0) return stats;

            var item = list.ElementAt(0);
            stats.DataCount = list.Count();
            stats.ItemType = item.GetType();

            var properties = item.GetProperties();
            foreach (var p in properties)
            {
                if (p.PropertyType == typeof(double))
                {
                    stats.Columns.Add(p.Name, new DoubleDataColumn(p));
                }
                else
                {
                    stats.Columns.Add(p.Name, new ObjectDataColumn(p));
                }
            }
            foreach (var p in properties)
            {
                stats.Columns[p.Name].Items = list;
                stats.Columns[p.Name].Calculate();
                //Debug.WriteLine(stats.Columns[p.Name].ToString());
            }
             
            return stats;
        }
        public override string ToString()
        {
            var type = this.DataType.ToReadableString();
            
            var lines = new StringBuilder(); 
            lines.AppendLine("DataStats: " + this.DataCount + " items in " + type + " ");
            type = this.ItemType.ToReadableString();
            lines.AppendLine(type + " Properties=" + Columns.Count + "");

            var columns = Columns.Values.OrderByDescending(c => c.Type.FullName).ToList();
            var types = columns.Select(c => c.Type.ToReadableString().Length).ToList();
            var names = columns.Select(c => c.Name.Length).ToList();
            var typesMax = types.Max();
            var namesMax = names.Max();

            for (var i = 0; i < columns.Count(); i++)
            {
                lines.AppendLine((i +1).ToString("D2") + " " + columns[i].ToString(typesMax, namesMax));
            }

            return lines.ToString();
        }
    }
    public abstract class DataColumnBase 
    {
        #region Properties
        public string Name
        {
            get { return Property == null ? null : Property.Name; }
        }

        public Type Type
        {
            get { return Property == null ? null : Property.PropertyType; }
        } 
        public PropertyInfo Property { get; set; }
        public int Count { get; set; }

        public IEnumerable<object> Items { get; set; }

        #endregion
        protected DataColumnBase(PropertyInfo prop)
        {
            Property = prop;
            Items = new List<object>();
        }
        public virtual void Calculate()
        {
            Reset();
            foreach (var item in Items)
            {
                this.Update(item);
            }
        }
        public virtual void Reset()
        {
            Count = 0;
        }
        public virtual void Update(object item)
        {
            Count++; 
        }

        public override string ToString()
        {
            var type = this.Type.ToReadableString();
            var name = this.Name;
            return " Property=" + type + " Name=" + name;
        }

        public virtual string ToString(int typeLength, int nameLength)
        {
            var type = this.Type.ToReadableString();
            type = type + new string(' ', typeLength - type.Length);
            var name = this.Name;
            name = name + new string(' ', nameLength - name.Length);

            return " Property=" + type + " Name=" + name;
        }
    }

    public class ObjectDataColumn : DataColumnBase  
    {
        public ObjectDataColumn(PropertyInfo prop) : base(prop)
        {
        }
    }

    //public class DataColumn<T> where T : Type
        //{
        //    public string Name { get; set; }
        //    public T Type { get; set; }

        //    public DataColumn()
        //    {

        //    }
        //}
    public class DoubleDataColumn : DataColumnBase 
    {
        public DoubleDataColumn(PropertyInfo prop) : base(prop)
        {

        }
        #region Properties
        public double Max { get; private set; }
        public double Min { get; private set; }
        public double Range { get; private set; }
        public double Average { get; private set; }
        public double Total { get; private set; }
      
        #endregion
       
        public override void Reset()
        {
            base.Reset();

            Min = double.MaxValue;
            Max = double.MinValue;

            Total = 0;
            Average = double.NaN;
            Range = double.NaN;
        }
        public override void Update(object item)
        { 
            var value = item.GetPropertyValue<double>(this.Property);
            if (double.IsNaN(value)) return;
            if (double.IsInfinity(value)) return;

            Count++;

            Max = System.Math.Max(Max, value);
            Min = System.Math.Min(Min, value);
            Range = Max - Min;
            Total += value;
            
            if (System.Math.Abs(Count) > 0.00001) 
                Average = value / Count; 
             
        }
        public override string ToString(int typeLength, int nameLength)
        {
            return base.ToString(typeLength, nameLength) +
                "\t Min=" + this.Min.ToString("F4") +
                "\t Max=" + this.Max.ToString("F4") +
                "\t Total=" + this.Total.ToString("F4");
        }
    }

}