using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infragistics.Framework
{
    public class SampleSalesTeam : List<SampleSalesPerson>
    {
        public SampleSalesTeam()
        {
            var list = SampleSalesPerson.GenerateSalesData(100);
            this.AddRange(list);
        }
    }

    public class SampleSalesPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Territory { get; set; }
        public int Sales { get; set; }

        public static List<SampleSalesPerson> GenerateSalesData(int count)
        {
            string[] firstNames = {
                "Kyle", "Gina", "Irene", "Chris", "Katie", "Michael", "Oscar", "Ralph",
                "Torrey", "William", "Bill", "Daniel", "Frank", "Pam", "Brenda",
                "Danielle", "Fiona", "Howard", "Jack", "Larry", "Nelly", "Holly",
                "Jennifer", "Liz", "Pete", "Steve", "Vince", "Valerie", "Zeke"
            };

            string[] lastNames = {
                "Adams", "Crowley", "Ellis", "Gable", "Irvine", "Keefe", "Mendoza", "Owens", "Rooney",
                "Waddell", "Thomas", "Betts", "Doran", "Fitzgerald", "Holmes", "Jefferson", "Landry",
                "Newberry", "Perez", "Spencer", "Vargas", "Grimes", "Edwards", "Stark", "Cruise",
                "Fitz", "Chief", "Blanc", "Perry", "Stone", "Williams", "Lane", "Jobs"
            };

            string[] territories = {
                "Australia","Canada","Egypt","Greece","Italy","Kenya","Mexico","Oman","Qatar",
                "Sweden","Uruguay","Yemen","Bulgaria","Denmark","France","Hungary","Japan","Latvia",
                "Netherlands","Portugal","Russia","Turkey","Venezuela","Zimbabwe"
            };

            string[] genders = {
                "GUY","GIRL","GIRL","GIRL","GUY","GUY","GUY","GUY","GUY","GUY","GUY","GUY","GIRL",
                "GIRL","GIRL","GUY","GUY","GUY","GIRL","GIRL","GIRL","GUY","GUY","GUY","GUY"
            };

            var r = new Random();
            var items = new List<SampleSalesPerson>();

            for (int i = 0; i < count; i++)
            {
                int firstIndex = r.Next(0, firstNames.Length - 1);
                int lastIndex = r.Next(0, lastNames.Length - 1);
                int territoryIndex = r.Next(0, territories.Length - 1);
                int salesValue = r.Next(10000, 50000);

                var item = new SampleSalesPerson();
                item.FirstName = firstNames[firstIndex];
                item.LastName = lastNames[lastIndex];
                item.Territory = territories[territoryIndex];
                item.Sales = salesValue;
                items.Add(item);
            }
            return items;
        }
    }
}
