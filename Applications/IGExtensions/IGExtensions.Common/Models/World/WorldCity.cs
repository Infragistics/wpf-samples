 
using System.Collections.Generic;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents an world city model in geographic context (GeoLocation)
    /// </summary>
    public class WorldCity : GeoLocation
    {
        public WorldCity()
        {
            this.Population = 0.0;
        }
    
        private double _population;
        /// <summary>
        /// Gets or sets Population property 
        /// </summary>
        public double Population
        {
            get {  return _population; }
            set { if (_population == value) return; _population = value; OnPropertyChanged("Population"); }
        }

        public string CityName { get; set; }
        public string CountryName { get; set; }
        public bool IsCapital { get; set; }

        public new string ToString()
        {
            return this.CityName + ", " + CountryName + " [" + Longitude + ", " + Latitude + "]";
        }
    }
    public class WorldCities : List<WorldCity>
    {
        public WorldCities()
        {
            Initialize();
            
        }
        public void Initialize()
        {
            this.Population = new DoubleRange();
            //this.GdpPerCapita = new DoubleRange();
            //this.BirthRate = new DoubleRange();
            //this.ElectricityProduction = new DoubleRange();
            //this.MedianAge = new DoubleRange();
            //this.PublicDebt = new DoubleRange();
            //this.UnemploymentRate = new DoubleRange();
            //this.Televisions = new DoubleRange();
            //this.InternetUsers = new DoubleRange();
            //this.Area = new DoubleRange();
        }
        public void Recalculate()
        {
            Initialize();
            foreach (var item in this)
            {
                this.Population.Update(item.Population);
            }
        }
        public DoubleRange Population { get; set; }
        //public DoubleRange BirthRate { get; set; }
        //public DoubleRange ElectricityProduction { get; set; }
        //public DoubleRange MedianAge { get; set; }
        //public DoubleRange PublicDebt { get; set; }
        //public DoubleRange UnemploymentRate { get; set; }
        //public DoubleRange Televisions { get; set; }
        //public DoubleRange InternetUsers { get; set; }


    }
}