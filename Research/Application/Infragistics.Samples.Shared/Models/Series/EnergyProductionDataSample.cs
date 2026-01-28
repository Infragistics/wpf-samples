using System.Collections.ObjectModel;
using Infragistics.Samples.Shared.Resources;

namespace Infragistics.Samples.Shared.Models
{
    public class EnergyProductionDataSample : EnergyProductionData
    {
        public EnergyProductionDataSample()
        {
            this.Add(new EnergyProduction { Id = "Canada", Region = ModelStrings.XWM_Region_NorthAmerica, Country = ModelStrings.XWM_Country_Canada, Coal = 400, Oil = 100, Gas = 175, Nuclear = 225, Hydro = 350 });
            this.Add(new EnergyProduction { Id = "China", Region = ModelStrings.XWM_Region_Asia, Country = ModelStrings.XWM_Country_China, Coal = 925, Oil = 200, Gas = 350, Nuclear = 400, Hydro = 625 });
            this.Add(new EnergyProduction { Id = "Russia", Region = ModelStrings.XWM_Region_Asia, Country = ModelStrings.XWM_Country_Russia, Coal = 550, Oil = 200, Gas = 250, Nuclear = 475, Hydro = 425 });
            this.Add(new EnergyProduction { Id = "Australia", Region = ModelStrings.XWM_Region_Australia, Country = ModelStrings.XWM_Country_Australia, Coal = 450, Oil = 100, Gas = 150, Nuclear = 175, Hydro = 350 });
            this.Add(new EnergyProduction { Id = "USA", Region = ModelStrings.XWM_Region_NorthAmerica, Country = ModelStrings.XWM_Country_United_States, Coal = 800, Oil = 250, Gas = 475, Nuclear = 575, Hydro = 750 });
            this.Add(new EnergyProduction { Id = "France", Region = ModelStrings.XWM_Region_Europe, Country = ModelStrings.XWM_Country_France, Coal = 375, Oil = 150, Gas = 350, Nuclear = 275, Hydro = 325 });
        }
    }
    public class EnergyProductionDataHistory : EnergyProductionData
    {
        public EnergyProductionDataHistory()
        {
            this.Add(new EnergyProduction { Year = "2000", Id = "Canada", Country = ModelStrings.XWM_Country_Canada, Coal = 400, Oil = 100, Gas = 175, Nuclear = 225, Hydro = 350 });
            this.Add(new EnergyProduction { Year = "2010", Id = "Canada", Country = ModelStrings.XWM_Country_Canada, Coal = 450, Oil = 150, Gas = 225, Nuclear = 255, Hydro = 400 });
            this.Add(new EnergyProduction { Year = "2020", Id = "Canada", Country = ModelStrings.XWM_Country_Canada, Coal = 300, Oil = 200, Gas = 275, Nuclear = 325, Hydro = 450 });
            this.Add(new EnergyProduction { Year = "2000", Id = "China", Country = ModelStrings.XWM_Country_China, Coal = 825, Oil = 200, Gas = 350, Nuclear = 400, Hydro = 625 });
            this.Add(new EnergyProduction { Year = "2010", Id = "China", Country = ModelStrings.XWM_Country_China, Coal = 925, Oil = 250, Gas = 400, Nuclear = 420, Hydro = 685 });
            this.Add(new EnergyProduction { Year = "2020", Id = "China", Country = ModelStrings.XWM_Country_China, Coal = 975, Oil = 300, Gas = 450, Nuclear = 450, Hydro = 725 });
            this.Add(new EnergyProduction { Year = "2000", Id = "Russia", Country = ModelStrings.XWM_Country_Russia, Coal = 550, Oil = 200, Gas = 250, Nuclear = 475, Hydro = 425 });
            this.Add(new EnergyProduction { Year = "2010", Id = "Russia", Country = ModelStrings.XWM_Country_Russia, Coal = 600, Oil = 220, Gas = 280, Nuclear = 525, Hydro = 485 });
            this.Add(new EnergyProduction { Year = "2020", Id = "Russia", Country = ModelStrings.XWM_Country_Russia, Coal = 650, Oil = 250, Gas = 350, Nuclear = 575, Hydro = 525 });
            this.Add(new EnergyProduction { Year = "2000", Id = "Australia", Country = ModelStrings.XWM_Country_Australia, Coal = 450, Oil = 100, Gas = 150, Nuclear = 175, Hydro = 350 });
            this.Add(new EnergyProduction { Year = "2010", Id = "Australia", Country = ModelStrings.XWM_Country_Australia, Coal = 480, Oil = 120, Gas = 250, Nuclear = 225, Hydro = 400 });
            this.Add(new EnergyProduction { Year = "2020", Id = "Australia", Country = ModelStrings.XWM_Country_Australia, Coal = 550, Oil = 180, Gas = 350, Nuclear = 275, Hydro = 450 });
            this.Add(new EnergyProduction { Year = "2000", Id = "USA", Country = ModelStrings.XWM_Country_United_States, Coal = 800, Oil = 250, Gas = 475, Nuclear = 575, Hydro = 750 });
            this.Add(new EnergyProduction { Year = "2010", Id = "USA", Country = ModelStrings.XWM_Country_United_States, Coal = 850, Oil = 300, Gas = 525, Nuclear = 625, Hydro = 800 });
            this.Add(new EnergyProduction { Year = "2020", Id = "USA", Country = ModelStrings.XWM_Country_United_States, Coal = 900, Oil = 350, Gas = 575, Nuclear = 675, Hydro = 850 });
            this.Add(new EnergyProduction { Year = "2000", Id = "France", Country = ModelStrings.XWM_Country_France, Coal = 375, Oil = 150, Gas = 350, Nuclear = 275, Hydro = 325 });
            this.Add(new EnergyProduction { Year = "2010", Id = "France", Country = ModelStrings.XWM_Country_France, Coal = 425, Oil = 200, Gas = 400, Nuclear = 325, Hydro = 385 });
            this.Add(new EnergyProduction { Year = "2020", Id = "France", Country = ModelStrings.XWM_Country_France, Coal = 455, Oil = 250, Gas = 450, Nuclear = 375, Hydro = 425 });
        }
    }
    public class EnergyProductionData : ObservableCollection<EnergyProduction>
    {

    }
    public class EnergyProduction
    {
        public string Id { get; set; }
       
        public string Country { get; set; }
        public string Region { get; set; }
        public string Year { get; set; }

        // Non-Renewable Energy Sources
        private double _nuclear;
        public double Nuclear { get { return _nuclear; } set { _nuclear = value; UpdateValues(); } }

        private double _coal;
        public double Coal { get { return _coal; } set { _coal = value; UpdateValues(); } }

        private double _oil;
        public double Oil { get { return _oil; } set { _oil = value; UpdateValues(); } }

        private double _gas;
        public double Gas { get { return _gas; } set { _gas = value; UpdateValues(); } }
        // Renewable Energy Sources
        private double _hydro;
        public double Hydro { get { return _hydro; } set { _hydro = value; UpdateValues(); } }

        private double _geoThermal;
        public double GeoThermal { get { return _geoThermal; } set { _geoThermal = value; UpdateValues(); } }

        private double _solar;
        public double Solar { get { return _solar; } set { _solar = value; UpdateValues(); } }

        private double _wind;
        public double Wind { get { return _wind; } set { _wind = value; UpdateValues(); } }

        public double Total { get; set; }
        public double Renewable { get; set; }
        public double NonRenewable { get; set; }

        public void UpdateValues()
        {
            this.Renewable = Hydro + GeoThermal + Wind + Solar;
            this.NonRenewable = Coal + Oil + Gas + Nuclear;
            this.Total = this.Renewable + this.NonRenewable;
        }
    }
    public class EnergyProduction2
    {

        public string Country { get; set; }
        public string Year { get; set; }

        public double Nuclear { get; set; }
        public double Coal { get; set; }
        public double Oil { get; set; }
        public double Gas { get; set; }

        public double Hydro { get; set; }
        public double GeoThermal { get; set; }
        public double Solar { get; set; }
        public double Wind { get; set; }

        public double Total
        {
            get { return this.Renewable + this.NonRenewable; }
        }
        public double Renewable
        {
            get { return Hydro + GeoThermal + Wind + Solar; }
        }
        public double NonRenewable
        {
            get { return Coal + Oil + Gas + Nuclear; }
        }
    }

}