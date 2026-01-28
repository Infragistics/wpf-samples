using System;
using System.Collections.Generic; 

namespace Infragistics.Framework
{  
    public class SampleData : List<SampleDataItem>
    {
        public SampleData()
        {
            var random = new Random();

            var year = 1950;
            for (int i = 0; i < 10; i++)
            {
                var medals = new SampleDataItem();
                medals.Year = year.ToString();
                medals.Gold = random.Next(1, 5);
                medals.Silver = random.Next(5, 10);
                medals.Bronze = random.Next(10, 15);

                medals.Label = "L" + (i + 1);
                this.Add(medals);
                year += 4;
            }
        }
    }
    public class SampleDataItem
    {
        public int Gold { get; set; }
        public int Silver { get; set; }

        public int Bronze { get; set; }

        public string Label { get; set; }
        public string Year { get; set; }

    }
}
