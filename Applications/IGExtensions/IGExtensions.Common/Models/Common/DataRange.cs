using System;

namespace IGExtensions.Common.Models
{
    public class DoubleRange
    {
        public DoubleRange()
        {
            this.Min = double.MaxValue;
            this.Max = double.MinValue;
            this.Value = 0;
        }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Value { get; set; }

        public void Update(double value)
        {
            this.Min = System.Math.Min(this.Min, value);
            this.Max = System.Math.Max(this.Max, value);
            this.Value += value;
        }
        public void Update(double? value)
        {
            if (value != null)
            {
                this.Min = System.Math.Min(this.Min, (double)value);
                this.Max = System.Math.Max(this.Max, (double)value);
                this.Value += (double)value;
            }
        }
    }
    public class IntegerRange
    {
        public IntegerRange()
        {
            this.Min = int.MaxValue;
            this.Max = int.MinValue;
            this.Value = 0;
        }
        public int Min { get; set; }
        public int Max { get; set; }
        public int Value { get; set; }

        public void Update(int value)
        {
            this.Min = System.Math.Min(this.Min, value);
            this.Max = System.Math.Max(this.Max, value);
            this.Value += value;
        }
    }
    public class DateTimeRange
    {
        public DateTimeRange()
        {
            this.Min = DateTime.MaxValue;
            this.Max = DateTime.MinValue;
            //this.Value = 0;
        }
        public DateTime Min { get; set; }
        public DateTime Max { get; set; }
        //public DateTime Value { get; set; }

        public void Update(DateTime value)
        {
            if (value < this.Min) this.Min = value;
            if (value > this.Max) this.Max = value;
        }
    }
}