using System;
using System.Collections.Generic;
using Infragistics.Framework;

namespace TestApp
{
    public class DataItem : ObservableModel
    {
        public string Empty { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public double Index { get; set; }
        public double IndexPlus { get; set; }
        public double IndexMinus { get; set; }

        public double Value0 { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
        public double Value4 { get; set; }

        public double Value { get; set; }
        public double Radius { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public double Open { get; set; }
        public double High { get; set; }
        public double Close { get; set; }
        public double Low { get; set; }
        public DateTime Date { get; set; }

        public double H { get; set; }
        public double L { get; set; }
        public double C { get; set; }
        public double O { get; set; }
    }

    public class DataSource : List<DataItem>
    {
        public DataSource()
        {
            this.Add(new DataItem() { Name = "JAN", Value0 = 2.0, Radius = 10, X = 1, Y = 5 });
            this.Add(new DataItem() { Name = "FEB", Value0 = 4.0, Radius = 10, X = 2, Y = 5 });
            this.Add(new DataItem() { Name = "MAR", Value0 = 2.0, Radius = 10, X = 3, Y = 5 });
            this.Add(new DataItem() { Name = "APR", Value0 = 6.0, Radius = 10, X = 4, Y = 5 });
            this.Add(new DataItem() { Name = "MAY", Value0 = 3.0, Radius = 10, X = 5, Y = 5 });
            this.Add(new DataItem() { Name = "JUL", Value0 = 5.0, Radius = 10, X = 6, Y = 5 });
            this.Add(new DataItem() { Name = "AUG", Value0 = 4.0, Radius = 10, X = 7, Y = 5 });
            this.Add(new DataItem() { Name = "SEP", Value0 = 2.0, Radius = 10, X = 8, Y = 5 });
            this.Add(new DataItem() { Name = "OCT", Value0 = 3.0, Radius = 60, X = 9, Y = 5 });

            //this.Add(new DataItem() { Name = "G", Value0 = 50, });
            //this.Add(new DataItem() { Name = "H", Value0 = 60, });
            //this.Add(new DataItem() { Name = "I", Value0 = 130, });
            //this.Add(new DataItem() { Name = "J", Value0 = 10, });
            //this.Add(new DataItem() { Name = "K", Value0 = 150, });
            //this.Add(new DataItem() { Name = "L", Value0 = 70, });

            var rand = new Random();
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Index = i;
                this[i].IndexMinus = i - 0.5;
                this[i].IndexPlus = i + 0.5;
                this[i].Value1 = this[i].Value0 + 5;
                this[i].Value2 = this[i].Value0 - 5;
                this[i].Value3 = this[i].Value0 + 20;
                this[i].Value4 = this[i].Value0 + 30;

                this[i].O = this[i].Value0;
                this[i].H = this[i].O + rand.Next(10, 20);
                this[i].L = this[i].O - rand.Next(10, 20);
                this[i].C = rand.Next((int)this[i].L, (int)this[i].H);
            }
        }
    }

    public class SimpleData : List<DataItem>
    {
        public SimpleData()
        {
            this.Add(new DataItem() { Name = "A", Value0 = 0, });
            this.Add(new DataItem() { Name = "B", Value0 = 0, });
            this.Add(new DataItem() { Name = "C", Value0 = 0, });
            this.Add(new DataItem() { Name = "D", Value0 = 0, });
            this.Add(new DataItem() { Name = "E", Value0 = 0, });
            this.Add(new DataItem() { Name = "F", Value0 = 0, });

            for (int i = 0; i < this.Count; i++)
            {
                this[i].Index = i;
                this[i].Value1 = this[i].Value0 + 10;
                this[i].Value2 = this[i].Value0 + 20;
                this[i].Value3 = this[i].Value0 + 30;
                this[i].Value4 = this[i].Value0 + 40;
            }
        }
    }


    public class TestDataSourceNaN : List<TestDataItem>
    {
        public TestDataSourceNaN()
        {
            this.Add(new TestDataItem { Label = "A", Value = 20, Value2 = 0, Value3 = 10, Value4 = 0 });
            this.Add(new TestDataItem { Label = "B", Value = 25, Value2 = double.NaN, Value3 = 20, Value4 = 10 });
            this.Add(new TestDataItem { Label = "C", Value = 10, Value2 = 20, Value3 = double.NaN, Value4 = 20 });
            this.Add(new TestDataItem { Label = "D", Value = 15, Value2 = 30, Value3 = 40, Value4 = 30 });
            this.Add(new TestDataItem { Label = "E", Value = 30, Value2 = double.NaN, Value3 = 20, Value4 = 40 });
            this.Add(new TestDataItem { Label = "F", Value = 15, Value2 = 20, Value3 = 40, Value4 = 5 });
            this.Add(new TestDataItem { Label = "G", Value = 10, Value2 = 35, Value3 = 50, Value4 = 10 }); 
        }
    }

    public class TestDataSource : List<TestDataItem>
    {
        public TestDataSource()
        {
            this.Add(new TestDataItem { Label = "A", Value = 20, Value2 = 0, Value3 = 10, Value4 = 0 });
            this.Add(new TestDataItem { Label = "B", Value = 25, Value2 = 10, Value3 = 20, Value4 = 10 });
            this.Add(new TestDataItem { Label = "C", Value = 10, Value2 = 20, Value3 = 30, Value4 = 20 });
            this.Add(new TestDataItem { Label = "D", Value = 15, Value2 = 30, Value3 = 40, Value4 = 30 });
            this.Add(new TestDataItem { Label = "E", Value = 30, Value2 = 40, Value3 = 20, Value4 = 40 });
            this.Add(new TestDataItem { Label = "F", Value = 15, Value2 = 20, Value3 = 40, Value4 = 5 });
            this.Add(new TestDataItem { Label = "G", Value = 10, Value2 = 35, Value3 = 50, Value4 = 10 });
        }
    }

    public class TestDataItem : ObservableModel
    {
        public string Label { get; set; } 
        public double Value { get; set; }
        //public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
        public double Value4 { get; set; }

    }

    public class FinDataSource : List<DataItem>
    {
        public FinDataSource()
        {
            var rand = new Random();
            var value = 50;
            var date = new DateTime(2024, 1, 1, 0, 0, 0);
            
            for (int i = 0; i < 10; i++)
            {
                var item = new DataItem();
                item.Name = i.ToString();
                item.Index = i;

                //if (i != 5 && i != 6) && i % 6 != 1 date.DayOfWeek != DayOfWeek.Friday

                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    item.Open = value;
                    item.High = value + rand.Next(5, 10);
                    item.Low = value - rand.Next(2, 10);
                    item.Close = value + rand.Next(-7, 7);
                }
                else
                {
                    item.Open = double.NaN;
                    item.High = double.NaN;
                    item.Low = double.NaN;
                    item.Close = double.NaN;

                }

                item.Date = date;
                value += rand.Next(-5, 6);
                date = date.AddDays(1);
                 
                Console.WriteLine(date.DayOfWeek + " Ticks " + date.Ticks);
                this.Add(item);
            }
        }
    }

    public class LargeDataSource : List<DataItem>
    {
        public LargeDataSource()
        {
            for (int i = 0; i < 30; i++)
            {
                var item = new DataItem();
                item.Name = i.ToString();
                item.Index = i;
                if (i % 3 == 0)
                    item.Value0 = 20;
                else if (i % 6 == 0)
                    item.Value0 = 30;
                else
                    item.Value0 = 10;

                this.Add(item);
            }
        }
    }

    public class SmallDataSource : DataSource
    {
        public SmallDataSource()
        {
            foreach (var item in this)
            {
                item.Value0 = item.Value0 / 10.0;
            }
        }
    }

}
