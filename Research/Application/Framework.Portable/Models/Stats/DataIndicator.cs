using System;
using System.Collections.Generic;
using System.Reflection;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents data indicator with info about numeric data column
    /// </summary>
    public class DataNumericColumn : DataColumn
    {
        public DataNumericColumn()
        {
            Initialize();
        }
        public DataNumericColumn(PropertyInfo prop) : base(prop)
        {
            Initialize();
        } 
        protected void Initialize()
        { 
            Format = "0:##0.0";
            IsLogarithmic = true;
            Min = double.MaxValue;
            Max = double.MinValue;
            Sum = 0;
            Average = 0;
        }

        #region Properties
        /// <summary> Gets or sets value format </summary>
        public string Format { get; set; }

        /// <summary> Gets or sets IsLogarithmic </summary>
        public bool IsLogarithmic { get; set; }

        /// <summary> Gets or sets Minimum </summary>
        public double Min { get; set; }

        /// <summary> Gets or sets Maximum </summary>
        public double Max { get; set; }

        /// <summary> Gets or sets Sum </summary>
        public double Sum { get; set; }

        /// <summary> Gets or sets Sum </summary>
        public double Range { get { return System.Math.Abs(Max - Min); } }

        /// <summary> Gets or sets Average </summary>
        public double Average { get; set; }

        public int IntervalDecimals { get; private set; }

        private double _intervalSteps;

        /// <summary> Gets or sets Interval steps</summary>
        public double IntervalSteps
        {
            get { return _intervalSteps; }
            set
            {
                _intervalSteps = value;
                IntervalDecimals = GetDecimals(value);
            }
        } 
        #endregion
        /// <summary> Round a value to closets decimal point of the interval </summary> 
        public double Round(double value)
        {
            return Math.Round(value, IntervalDecimals);
        } 

        public static int GetDecimals(double value)
        {
            int count = BitConverter.GetBytes(decimal.GetBits((decimal)value)[3])[2];
            return count;
        }

        public override void Reset()
        {
            base.Reset();

            Min = double.MaxValue;
            Max = double.MinValue;

            Sum = 0;
            Average = double.NaN; 
        }
        public override void Update(object item)
        {
            var value = item.GetPropertyValue<double>(this.Property);
            if (double.IsNaN(value)) return;
            if (double.IsInfinity(value)) return;

            ItemsCount++;

            Max = System.Math.Max(Max, value);
            Min = System.Math.Min(Min, value); 
            Sum += value;

            if (System.Math.Abs(ItemsCount) > 0.00001)
                Average = value / ItemsCount; 
        }

        public override string ToString()
        {
            var ret = base.ToString();
            ret += string.Format(" Min {0:" + Format +"}", Min);
            ret += string.Format(" Max {0:" + Format + "}", Max);
            ret += string.Format(" Range {0:" + Format + "}", Range);
            ret += string.Format(" Min {0:" + Format + "}", Min);

            return ret;
        }
        public override string ToString(int typeLength, int nameLength)
        {
            return base.ToString(typeLength, nameLength) +
                "\t Min=" + this.Min.ToString("F4") +
                "\t Max=" + this.Max.ToString("F4") +
                "\t Total=" + this.Sum.ToString("F4");
        }
    }

    /// <summary>
    /// Represents data column with info  
    /// </summary>
    public class DataColumn
    {
        #region Properties

        /// <summary> Gets or sets Name </summary>
        public string Name { get; set; }
        /// <summary> Gets or sets Key </summary>
        public string Key { get; set; }
        /// <summary> Gets or sets Description </summary>
        public string Description { get; set; }
        /// <summary> Gets or sets Symbol </summary>
        public string Symbol { get; set; }

        /// <summary> Gets or sets Tooltip </summary>
        public string Tooltip { get; set; }

        /// <summary> Gets or sets ImagePath </summary>
        public string ImagePath { get; set; }

        ///// <summary> Gets or sets Label </summary>
        //public string Label { get; set; }

        public string PropertyName
        {
            get { return Property == null ? null : Property.Name; }
        }

        public Type PropertyType
        {
            get { return Property == null ? null : Property.PropertyType; }
        }
        public PropertyInfo Property { get; private set; }

        public int ItemsCount { get; protected set; }

        public IEnumerable<object> Items { get; set; }

        #endregion
        public DataColumn()
        { 
            Items = new List<object>();
        }
        public DataColumn(PropertyInfo prop) : this()
        {
            Property = prop; 
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
            ItemsCount = 0;
        }
        public virtual void Update(object item)
        {
            ItemsCount++;
        }

        //public new string ToString()
        //{
        //    return Label + " {" + this.Description + "}";
        //}

        public override string ToString()
        {
            return " Name=" + Name;
        }

        public virtual string ToString(int typeLength, int nameLength)
        {
            var type = this.PropertyType.ToReadableString();
            type = type + new string(' ', typeLength - type.Length);

            var name = this.PropertyName;
            name = name + new string(' ', nameLength - name.Length);

            return " Property=" + type + " Name=" + name;
        }

       
    }

  
}