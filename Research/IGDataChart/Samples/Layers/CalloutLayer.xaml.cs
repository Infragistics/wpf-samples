using System;
using System.Collections.Generic;

namespace IGDataChart.Samples.Layers
{ 
    public partial class CalloutLayer : Infragistics.Samples.Framework.SampleContainer
    {
        public CalloutLayer()
        {
            InitializeComponent();
            UseDefaultTheme = true;
        } 
    }
  
    public class PopulationInfo
    {
        public double Value { get; set; } 
        public double ValueInMln { get; set; } 
        public double ChangeInValue { get; set; } 
        public double ChangePercentage { get; set; } 
        public int Year { get; set; }

        public int Index { get; set; }        
    }

    public class PopulationData : List<PopulationInfo>
    {
        public string Country { get; set; } 
    }
     
    public class CalloutItem
    {
        // reference to data item
        public object DataItem { get; set; } 
        
        // custom annotation properties
        public string Content { get; set; }
        public string Label  { get; set; } 
    }

    public class PopulationViewModel
    {        
        public PopulationData USA { get; set; }
        public PopulationData RUS { get; set; }
                
        public List<CalloutItem> CalloutsUSA { get; set; }
        public List<CalloutItem> CalloutsRUS { get; set; }

        public PopulationViewModel()
        {
            GenerateDataSources();

            GenerateCallouts();
        }
        
        public void GenerateCallouts()
        {
            this.CalloutsUSA = new List<CalloutItem>();
            this.CalloutsRUS = new List<CalloutItem>();

            for (int i = 0; i < USA.Count; i++)
            {
                var usa = new CalloutItem();
                usa.DataItem = this.USA[i];
                usa.Content = this.USA.Country;

                var rus = new CalloutItem();
                rus.DataItem = this.RUS[i];
                rus.Content = this.RUS.Country;

                if (i == 0)
                {
                    usa.Label = "";                    
                    rus.Label = "";
                }
                else
                {
                    usa.Label = this.USA[i].ChangePercentage.ToString("N1") + "%";                    
                    rus.Label = this.RUS[i].ChangePercentage.ToString("0.0") + "%";
                }
                
                this.CalloutsUSA.Add(usa);
                this.CalloutsRUS.Add(rus);
            }
        }
        
        public void GenerateDataSources()
        {            
            this.RUS = new PopulationData();
            this.RUS.Country = "Russia";
            this.RUS.Add(new PopulationInfo { Year = 1900, Value =  67473000 });
            this.RUS.Add(new PopulationInfo { Year = 1910, Value =  77459000 });
            this.RUS.Add(new PopulationInfo { Year = 1920, Value =  93459000 });
            this.RUS.Add(new PopulationInfo { Year = 1930, Value = 102357000 });
            this.RUS.Add(new PopulationInfo { Year = 1940, Value = 108377000 });
            this.RUS.Add(new PopulationInfo { Year = 1950, Value = 112534000 });
            this.RUS.Add(new PopulationInfo { Year = 1960, Value = 117534000 });
            this.RUS.Add(new PopulationInfo { Year = 1970, Value = 130079000 });
            this.RUS.Add(new PopulationInfo { Year = 1980, Value = 137552000 });
            this.RUS.Add(new PopulationInfo { Year = 1990, Value = 147386000 });
            this.RUS.Add(new PopulationInfo { Year = 2000, Value = 145166731 });
            this.RUS.Add(new PopulationInfo { Year = 2010, Value = 142856836 });
            this.RUS.Add(new PopulationInfo { Year = 2020, Value = 151880432 });

            this.USA = new PopulationData();
            this.USA.Country = "United States";
            this.USA.Add(new PopulationInfo { Year = 1900, Value = 76212168  });
            this.USA.Add(new PopulationInfo { Year = 1910, Value = 92228496  });
            this.USA.Add(new PopulationInfo { Year = 1920, Value = 106021537 });
            this.USA.Add(new PopulationInfo { Year = 1930, Value = 123202624 });
            this.USA.Add(new PopulationInfo { Year = 1940, Value = 132164569 });
            this.USA.Add(new PopulationInfo { Year = 1950, Value = 151325798 });
            this.USA.Add(new PopulationInfo { Year = 1960, Value = 179323175 });
            this.USA.Add(new PopulationInfo { Year = 1970, Value = 203211926 });
            this.USA.Add(new PopulationInfo { Year = 1980, Value = 226545805 });
            this.USA.Add(new PopulationInfo { Year = 1990, Value = 248709873 });
            this.USA.Add(new PopulationInfo { Year = 2000, Value = 281421906 });
            this.USA.Add(new PopulationInfo { Year = 2010, Value = 308745538 });
            this.USA.Add(new PopulationInfo { Year = 2020, Value = 353745538 });

            
            for (int i = 0; i < USA.Count; i++)
            {
                this.USA[i].Index = i;
                this.RUS[i].Index = i;

                this.USA[i].ValueInMln = this.USA[i].Value / 1000000;
                this.RUS[i].ValueInMln = this.RUS[i].Value / 1000000;

                if (i == 0)
                {
                    this.USA[i].ChangeInValue = 0;
                    this.RUS[i].ChangeInValue = 0;

                    this.USA[i].ChangePercentage = 0;
                    this.RUS[i].ChangePercentage = 0;
                }
                else
                {                  
                    this.USA[i].ChangeInValue = this.USA[i].Value - this.USA[i-1].Value;
                    this.RUS[i].ChangeInValue = this.RUS[i].Value - this.RUS[i-1].Value;
                      
                    this.USA[i].ChangePercentage = this.USA[i].ChangeInValue / this.USA[i].Value * 100.0;
                    this.RUS[i].ChangePercentage = this.RUS[i].ChangeInValue / this.RUS[i].Value * 100.0;
                }
            }
        }
    }

 
}
