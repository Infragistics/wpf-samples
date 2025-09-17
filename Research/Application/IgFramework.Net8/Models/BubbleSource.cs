using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using Infragistics.Framework;

namespace TestApp
{

    public class BubbleItem : ObservableModel
    {
        public string Label { get; set; }
        public string Name { get; set; }
        public double Index { get; set; }
        public double Value0 { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }

        public double Open { get; set; }
        public double High { get; set; }
        public double Close { get; set; }
        public double Low { get; set; }
        public DateTime Date { get; set; }
    }

    public class BubbleSource : List<BubbleItem>
    {
        public BubbleSource()
        {

            //this.Add(new BubbleItem() { Name = "A", Value0 = 20, });
            this.Add(new BubbleItem() { Name = "B", Value0 = 40, });


            //this.Add(new BubbleItem() { Name = "G", Value0 = 50, });
            //this.Add(new BubbleItem() { Name = "H", Value0 = 60, });
            //this.Add(new BubbleItem() { Name = "I", Value0 = 130, });
            //this.Add(new BubbleItem() { Name = "J", Value0 = 10, });
            //this.Add(new BubbleItem() { Name = "K", Value0 = 150, });
            //this.Add(new BubbleItem() { Name = "L", Value0 = 70, });
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Index = i;
                this[i].Value1 = this[i].Value0 + 10;
                this[i].Value2 = this[i].Value0 + 20;
            }
        }
    }

}
