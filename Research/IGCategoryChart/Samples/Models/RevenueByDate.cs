using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IGCategoryChart.Samples.Models
{
    public class DatedRevenue
    {
        public string YearMonth { get; set; }
        public double Revenue { get; set; }

    }
    public class RevenueByDate : ObservableCollection<DatedRevenue>
    {
        public RevenueByDate()
        {
            this.Add(new DatedRevenue() { YearMonth = "201510", Revenue = 7000 });
            this.Add(new DatedRevenue() { YearMonth = "201511", Revenue = 4320 });
            this.Add(new DatedRevenue() { YearMonth = "201512", Revenue = 6700 });
            this.Add(new DatedRevenue() { YearMonth = "201602", Revenue = 5600 });
            this.Add(new DatedRevenue() { YearMonth = "201603", Revenue = 4500 });
            this.Add(new DatedRevenue() { YearMonth = "201604", Revenue = 5000 });
            this.Add(new DatedRevenue() { YearMonth = "201605", Revenue = 4000 });
            this.Add(new DatedRevenue() { YearMonth = "201606", Revenue = 4700 });
            this.Add(new DatedRevenue() { YearMonth = "201607", Revenue = 2300 });
            this.Add(new DatedRevenue() { YearMonth = "201608", Revenue = 4500 });
            this.Add(new DatedRevenue() { YearMonth = "201609", Revenue = 3400 });
            this.Add(new DatedRevenue() { YearMonth = "201610", Revenue = 7500 });
        }
    }
    
}
