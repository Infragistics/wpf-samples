using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infragistics.Framework
{
    public class PieData : List<PieDataItem>
    {
        public PieData()
        {            
            for(int i=1; i <= 5; i++)
            {
                PieDataItem item = new PieDataItem() { Label = "Item " + i.ToString(), Value = 1 };
                this.Add(item);                
            }
        }
    }

    public class PieDataItem
    {
        public string Label { get; set; }
        public double Value { get; set; }
    }
}
