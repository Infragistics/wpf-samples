using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class CategoryStackedDataSample : CategoryStackedData
    {

        public CategoryStackedDataSample()
        {
            this.Add(new CategoryStackedDataPoint { Category = "Series A", GroupBy = "Stack 1", Value1 = -10, Value2 = -10, Value3 = -10 });
            this.Add(new CategoryStackedDataPoint { Category = "Series A", GroupBy = "Stack 2", Value1 = -20, Value2 = -20, Value3 = -20 });
            this.Add(new CategoryStackedDataPoint { Category = "Series A", GroupBy = "Stack 3", Value1 = -30, Value2 = -30, Value3 = -30 });

            this.Add(new CategoryStackedDataPoint { Category = "Series B", GroupBy = "Stack 1", Value1 = 10, Value2 = 10, Value3 = 10 });
            this.Add(new CategoryStackedDataPoint { Category = "Series B", GroupBy = "Stack 2", Value1 = 20, Value2 = 20, Value3 = 20 });
            this.Add(new CategoryStackedDataPoint { Category = "Series B", GroupBy = "Stack 3", Value1 = 30, Value2 = 30, Value3 = 30 });

            this.Add(new CategoryStackedDataPoint { Category = "Series C", GroupBy = "Stack 1", Value1 = 10, Value2 = 20, Value3 = 30 });
            this.Add(new CategoryStackedDataPoint { Category = "Series C", GroupBy = "Stack 2", Value1 = 10, Value2 = 20, Value3 = 30 });
            this.Add(new CategoryStackedDataPoint { Category = "Series C", GroupBy = "Stack 3", Value1 = 10, Value2 = 20, Value3 = 30 });

            //this.Add(new CategoryStackedDataPoint { Category = "America", GroupByStack = "United States", Value1 = -10, Value2 = -10, Value3 = -10 });
            //this.Add(new CategoryStackedDataPoint { Category = "America", GroupByStack = "Canada", Value1 = -20, Value2 = -20, Value3 = -20 });
            //this.Add(new CategoryStackedDataPoint { Category = "America", GroupByStack = "Mexico", Value1 = -30, Value2 = -30, Value3 = -30 });

            //this.Add(new CategoryStackedDataPoint { Category = "Europe", GroupByStack = "Germany", Value1 = 10, Value2 = 10, Value3 = 10 });
            //this.Add(new CategoryStackedDataPoint { Category = "Europe", GroupByStack = "Italy", Value1 = 20, Value2 = 20, Value3 = 20 });
            //this.Add(new CategoryStackedDataPoint { Category = "Europe", GroupByStack = "United Kingdom", Value1 = 30, Value2 = 30, Value3 = 30 });

            //this.Add(new CategoryStackedDataPoint { Category = "Series C", GroupByStack = "Stack 1", Value1 = 10, Value2 = 20, Value3 = 30 });
            //this.Add(new CategoryStackedDataPoint { Category = "Series C", GroupByStack = "Stack 2", Value1 = 10, Value2 = 20, Value3 = 30 });
            //this.Add(new CategoryStackedDataPoint { Category = "Series C", GroupByStack = "Stack 3", Value1 = 10, Value2 = 20, Value3 = 30 });

        }
          
    }
   
    //public class CountryStackedDataSample : ObservableCollection<CountryDataModel> 
    //{
    //    public CountryStackedDataSample()
    //    {
    //        this.Add(new CountryDataModel { CountryName = "United States", Region = "America", Population = 310, GdpPerCapita = 45, PublicDebt = 61 });
    //        this.Add(new CountryDataModel { CountryName = "Canada", Region = "America", Population = 33, GdpPerCapita = 38, PublicDebt = 64 });
    //        this.Add(new CountryDataModel { CountryName = "Mexico", Region = "America", Population = 110, GdpPerCapita = 12, PublicDebt = 23 });
            
    //        this.Add(new CountryDataModel { CountryName = "Germany", Region = "Europe", Population = 82, GdpPerCapita = 34, PublicDebt = 65 });
    //        this.Add(new CountryDataModel { CountryName = "Italy", Region = "Europe", Population = 58, GdpPerCapita = 31, PublicDebt = 104 });
    //        this.Add(new CountryDataModel { CountryName = "United Kingdom", Region = "Europe", Population = 60, GdpPerCapita = 35, PublicDebt = 44 });
           
    //        this.Add(new CountryDataModel { CountryName = "South Korea", Region = "Assia", Population = 48, GdpPerCapita = 25, PublicDebt = 28 });
    //        this.Add(new CountryDataModel { CountryName = "China", Region = "Assia", Population = 1350, GdpPerCapita = 5, PublicDebt = 18 });
    //        this.Add(new CountryDataModel { CountryName = "India", Region = "Assia", Population = 1140, GdpPerCapita = 3, PublicDebt = 58 });
        
    //    }
    //}
    public class CategoryStackedData : ObservableCollection<CategoryStackedDataPoint>
    {
 
    }
   
    public class CategoryStackedDataPoint  
    {
        public string Category { get; set; }
        public string GroupBy { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
    }

    
}