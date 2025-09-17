using System;
using System.Collections.Generic;
using System.Linq;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents information about world country 
    /// </summary>
    public class WorldCountry : ObservableModel
    {

        public WorldCountry()
        {
            _cities = new CityList();
        }
         
        public override string ToString()
        {
            var ret = this.Name;
            ret += " " + this.Population.ToStringShort();
            ret += " " + this.LandArea.ToStringShort();
            ret += " " + this.GdpPerCapita.ToStringShort();
            ret += " " + this.DebtPerCapita.ToStringShort();

            return ret;
        }
        #region Properties

        private string _countryName;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country")]
        public string Name
        {
            get { return _countryName; }
            set
            {
                if (_countryName == value) return;
                _countryName = value; OnPropertyChanged("Name");
            }
        }
      
        private string _countryCode;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Code")]
        public string Code
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode == value) return;
                _countryCode = value; OnPropertyChanged("Code");
            }
        }

        private string _region;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Region")]
        public string Region
        {
            get { return _region; }
            set
            {
                if (_region == value) return;
                _region = value; OnPropertyChanged("Region");
            }
        }

        private double _gdpPerCapita;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_GdpPerCapita")]
        public double GdpPerCapita
        {
            get { return _gdpPerCapita; }
            set
            {
                if (_gdpPerCapita == value) return;
                _gdpPerCapita = value;
                OnPropertyChanged("GdpPerCapita");
            }
        }
      
        private double _unemploymentRate;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_UnemploymentRate")]
        public double UnemploymentRate
        {
            get { return _unemploymentRate; }
            set
            {
                if (_unemploymentRate == value) return;
                _unemploymentRate = value; OnPropertyChanged("UnemploymentRate");
            }
        }


        private double _population;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_Population")]
        public double Population
        {
            get { return _population; }
            set
            {
                if (_population == value) return;
                _population = value; OnPropertyChanged("Population");
            }
        }

        /// <summary> Gets or sets PopulationFormat </summary>
        public string PopulationFormat { get { return Population.ToStringShort(); } }

        private double _internetUsers;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_InternetUsers")]
        public double InternetUsers
        {
            get { return _internetUsers; }
            set
            {
                if (_internetUsers == value) return;
                _internetUsers = value; OnPropertyChanged("InternetUsers");
            }
        }

        private double _electricityProduction;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityProduction")]
        public double ElectricityProduction
        {
            get { return _electricityProduction; }
            set
            {
                if (_electricityProduction == value) return;
                _electricityProduction = value; OnPropertyChanged("ElectricityProduction");
            }
        }

        private double _birthRate;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_BirthRate")]
        public double BirthRate
        {
            get { return _birthRate; }
            set
            {
                if (_birthRate == value) return;
                _birthRate = value; OnPropertyChanged("BirthRate");
            }
        }


        private IEnumerable<WorldCity> _cities;
        public IEnumerable<WorldCity> Cities
        {
            get
            {
                return _cities;
            }
            set
            {
                if (_cities == value) return;
                _cities = value; OnPropertyChanged("Cities");
            }
        }

        private double _populationDensity;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_PopulationDensity")]
        public double PopulationDensity
        {
            get { return _populationDensity; }
            set { if (_populationDensity == value) return; _populationDensity = value; OnPropertyChanged("PopulationDensity"); }
        }

        private double _populationGrowth;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_PopulationGrowth")]
        public double PopulationGrowth
        {
            get { return _populationGrowth; }
            set { if (_populationGrowth == value) return; _populationGrowth = value; OnPropertyChanged("PopulationGrowth"); }
        }
        private double _populationUrban;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_PopulationUrban")]
        public double PopulationUrban
        {
            get { return _populationUrban; }
            set { if (_populationUrban == value) return; _populationUrban = value; OnPropertyChanged("PopulationUrban"); }
        }
        private double _deathRate;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_DeatRate")]
        public double DeathRate
        {
            get { return _deathRate; }
            set { if (_deathRate == value) return; _deathRate = value; OnPropertyChanged("DeathRate"); }
        }
        private double _lifeExpectancy;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_LifeExpectancy")]
        public double LifeExpectancy
        {
            get { return _lifeExpectancy; }
            set { if (_lifeExpectancy == value) return; _lifeExpectancy = value; OnPropertyChanged("LifeExpectancy"); }
        }
        private double _migration;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_Migration")]
        public double Migration
        {
            get { return _migration; }
            set { if (_migration == value) return; _migration = value; OnPropertyChanged("Migration"); }
        }

        private double _literacyRate;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_LiteracyRate")]
        public double LiteracyRate
        {
            get { return _literacyRate; }
            set { if (_literacyRate == value) return; _literacyRate = value; OnPropertyChanged("LiteracyRate"); }
        }


        private double _landArea;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_LandArea")]
        public double LandArea
        {
            get { return _landArea; }
            set { if (_landArea == value) return; _landArea = value; OnPropertyChanged("LandArea"); }
        }

        ///// <summary> Gets or sets Population format string </summary>
        //public string PopulationFormat { get { return this.Population.ToStringShort(); } }
        ///// <summary> Gets or sets  GdpPerCapita format string </summary>
        //public string GdpPerCapitaFormat { get { return this.GdpPerCapita.ToStringShort(); } }
        ///// <summary> Gets or sets Gdp format string </summary>
        //public string GdpFormat { get { return this.Gdp.ToStringShort(); } }
        ///// <summary> Gets or sets GdpGrowth format string </summary>
        //public string GdpGrowthFormat { get { return this.GdpGrowth.ToStringShort(); } }
        ///// <summary> Gets or sets LandArea format string </summary>
        //public string LandAreaFormat { get { return this.LandArea.ToStringShort(); } }

        ///// <summary> Gets or sets DebtPerGdp format string </summary>
        //public string DebtPerGdpFormat { get { return this.DebtPerGdp.ToStringShort(); } }
        ///// <summary> Gets or sets DebtPerCapita format string </summary>
        //public string DebtPerCaptiaFormat { get { return this.DebtPerCapita.ToStringShort(); } }


        ///// <summary> Gets or sets LandAreaSqrtKm </summary>
        //public double LandAreaSqrtKm { get { return 2.589988110336 * LandAreaSqrtMiles;  } }

        private double _gdp;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_Gdp")]
        public double Gdp
        {
            get { return _gdp; }
            set { if (_gdp == value) return; _gdp = value; OnPropertyChanged("Gdp"); }
        }

        private double _gdpGrowth;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_GdpGrowth")]
        public double GdpGrowth
        {
            get { return _gdpGrowth; }
            set { if (_gdpGrowth == value) return; _gdpGrowth = value; OnPropertyChanged("GdpGrowth"); }
        }
        private double _trade;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_Trade")]

        /// <summary> Gets or sets trade value as percentage of Gdp </summary>
        public double Trade
        {
            get { return _trade; }
            set { if (_trade == value) return; _trade = value; OnPropertyChanged("Trade"); }
        }
        private double _debtTotal;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_DebtTotal")]
        public double DebtTotal
        {
            get { return _debtTotal; }
            set { if (_debtTotal == value) return; _debtTotal = value; OnPropertyChanged("DebtTotal"); }
        }
        private double _debtPerCapita;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_DebtPerCaptia")]
        public double DebtPerCapita
        {
            get { return _debtPerCapita; }
            set { if (_debtPerCapita == value) return; _debtPerCapita = value; OnPropertyChanged("DebtPerCapita"); }
        }
        private double _debtPerGdp;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_DebtPerGdp")]
        public double DebtPerGdp
        {
            get { return _debtPerGdp; }
            set { if (_debtPerGdp == value) return; _debtPerGdp = value; OnPropertyChanged("DebtPerGdp"); }
        }
        private double _internetRate;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_InternetRate")]
        public double InternetRate
        {
            get { return _internetRate; }
            set { if (_internetRate == value) return; _internetRate = value; OnPropertyChanged("InternetRate"); }
        }
        private double _electricityRate;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityRate")]
        public double ElectricityRate
        {
            get { return _electricityRate; }
            set { if (_electricityRate == value) return; _electricityRate = value; OnPropertyChanged("ElectricityRate"); }
        }
        private double _electricityCoal;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityCoal")]
        public double ElectricityCoal
        {
            get { return _electricityCoal; }
            set { if (_electricityCoal == value) return; _electricityCoal = value; OnPropertyChanged("ElectricityCoal"); }
        }

        private double _electricityWater;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityWater")]
        public double ElectricityWater
        {
            get { return _electricityWater; }
            set { if (_electricityWater == value) return; _electricityWater = value; OnPropertyChanged("ElectricityWater"); }
        }

        private double _electricityGas;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityGas")]
        public double ElectricityGas
        {
            get { return _electricityGas; }
            set { if (_electricityGas == value) return; _electricityGas = value; OnPropertyChanged("ElectricityGas"); }
        }

        private double _electricityNuclear;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityNuclear")]
        public double ElectricityNuclear
        {
            get { return _electricityNuclear; }
            set { if (_electricityNuclear == value) return; _electricityNuclear = value; OnPropertyChanged("ElectricityNuclear"); }
        }
        private double _electricityOil;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityOil")]
        public double ElectricityOil
        {
            get { return _electricityOil; }
            set { if (_electricityOil == value) return; _electricityOil = value; OnPropertyChanged("ElectricityOil"); }
        }

        private double _electricityRenewable;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ElectricityRenewable")]
        public double ElectricityRenewable
        {
            get { return _electricityRenewable; }
            set { if (_electricityRenewable == value) return; _electricityRenewable = value; OnPropertyChanged("ElectricityRenewable"); }
        }
        private double _emissionsCo2;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_EmissionsCo2")]
        public double EmissionsCo2
        {
            get { return _emissionsCo2; }
            set { if (_emissionsCo2 == value) return; _emissionsCo2 = value; OnPropertyChanged("EmissionsCo2"); }
        }
        private double _roadsDensity;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_RoadsDensity")]
        public double RoadsDensity
        {
            get { return _roadsDensity; }
            set { if (_roadsDensity == value) return; _roadsDensity = value; OnPropertyChanged("RoadsDensity"); }
        }
        private double _roadsPassengers;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_RoadsPassengers")]
        public double RoadsPassengers
        {
            get { return _roadsPassengers; }
            set { if (_roadsPassengers == value) return; _roadsPassengers = value; OnPropertyChanged("RoadsPassengers"); }
        }
        private double _airPassengers;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_AirPassengers")]
        public double AirPassengers
        {
            get { return _airPassengers; }
            set { if (_airPassengers == value) return; _airPassengers = value; OnPropertyChanged("AirPassengers"); }
        }
        private double _railwaysPassengers;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_RailwaysPassengers")]
        public double RailwaysPassengers
        {
            get { return _railwaysPassengers; }
            set { if (_railwaysPassengers == value) return; _railwaysPassengers = value; OnPropertyChanged("RailwaysPassengers"); }
        }
        private double _motorVehicles;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_MotorVehicles")]
        public double MotorVehicles
        {
            get { return _motorVehicles; }
            set { if (_motorVehicles == value) return; _motorVehicles = value; OnPropertyChanged("MotorVehicles"); }
        }
        private double _hospitalBeds;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_HospitalBeds")]
        public double HospitalBeds
        {
            get { return _hospitalBeds; }
            set { if (_hospitalBeds == value) return; _hospitalBeds = value; OnPropertyChanged("HospitalBeds"); }
        }
        private double _telephoneLines;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_TelephoneLines")]
        public double TelephoneLines
        {
            get { return _telephoneLines; }
            set { if (_telephoneLines == value) return; _telephoneLines = value; OnPropertyChanged("TelephoneLines"); }
        }

        private double _mobilePhones;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_MobilePhones")]
        public double MobilePhones
        {
            get { return _mobilePhones; }
            set { if (_mobilePhones == value) return; _mobilePhones = value; OnPropertyChanged("MobilePhones"); }
        }
        private double _armedForcesRate;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ArmedForcesRate")]
        public double ArmedForcesRate
        {
            get { return _armedForcesRate; }
            set { if (_armedForcesRate == value) return; _armedForcesRate = value; OnPropertyChanged("ArmedForcesRate"); }
        }
        private double _armedForcesTotal;
        //[Annotation(ResourceType = typeof(Resources.ModelStrings), Name = "Country_ArmedForcesTotal")]
        public double ArmedForcesTotal
        {
            get { return _armedForcesTotal; }
            set { if (_armedForcesTotal == value) return; _armedForcesTotal = value; OnPropertyChanged("ArmedForcesTotal"); }
        }
         
        #endregion
         
    }
    public class WorldCountries : List<WorldCountry>
    {
        public WorldCountries()
        {
            DataColumns = new Dictionary<string, string>();
            Stats = new WorldCountry();
        }

        public Dictionary<string, string> DataColumns { get; set; }

        public List<string> DataProperties
        {
            get { return DataColumns.Keys.ToList(); }
        }
        public List<string> DataTitles
        {
            get { return DataColumns.Values.ToList(); }
        }

        public WorldCountry Stats { get; private set; }

        public void UpdateStats(string name)
        {
            this.Stats = new WorldCountry();
            this.Stats.Name = name;
            this.Stats.Region = "World";
           
            if (this.Count == 0) return;

            foreach (var item in this)
            {
                //total
                if (!double.IsNaN(item.Population)) this.Stats.Population += item.Population;
                if (!double.IsNaN(item.PopulationUrban)) this.Stats.PopulationUrban += item.PopulationUrban;
                if (!double.IsNaN(item.LandArea)) this.Stats.LandArea += item.LandArea;
                if (!double.IsNaN(item.Gdp)) this.Stats.Gdp += item.Gdp;
                if (!double.IsNaN(item.Migration)) this.Stats.Migration += item.Migration;
                if (!double.IsNaN(item.Trade)) this.Stats.Trade += item.Trade;
                if (!double.IsNaN(item.DebtTotal)) this.Stats.DebtTotal += item.DebtTotal;
                if (!double.IsNaN(item.InternetUsers)) this.Stats.InternetUsers += item.InternetUsers;
                if (!double.IsNaN(item.ElectricityProduction)) this.Stats.ElectricityProduction += item.ElectricityProduction;
                if (!double.IsNaN(item.ElectricityCoal)) this.Stats.ElectricityCoal += item.ElectricityCoal;
                if (!double.IsNaN(item.ElectricityWater)) this.Stats.ElectricityWater += item.ElectricityWater;
                if (!double.IsNaN(item.ElectricityGas)) this.Stats.ElectricityGas += item.ElectricityGas;
                if (!double.IsNaN(item.ElectricityNuclear)) this.Stats.ElectricityNuclear += item.ElectricityNuclear;
                if (!double.IsNaN(item.ElectricityOil)) this.Stats.ElectricityOil += item.ElectricityOil;
                if (!double.IsNaN(item.ElectricityRenewable)) this.Stats.ElectricityRenewable += item.ElectricityRenewable;
                if (!double.IsNaN(item.EmissionsCo2)) this.Stats.EmissionsCo2 += item.EmissionsCo2;
                if (!double.IsNaN(item.RoadsPassengers)) this.Stats.RoadsPassengers += item.RoadsPassengers;
                if (!double.IsNaN(item.AirPassengers)) this.Stats.AirPassengers += item.AirPassengers;
                if (!double.IsNaN(item.RailwaysPassengers)) this.Stats.RailwaysPassengers += item.RailwaysPassengers;
                if (!double.IsNaN(item.MotorVehicles)) this.Stats.MotorVehicles += item.MotorVehicles;
                if (!double.IsNaN(item.MobilePhones)) this.Stats.MobilePhones += item.MobilePhones;
                if (!double.IsNaN(item.ArmedForcesTotal)) this.Stats.ArmedForcesTotal += item.ArmedForcesTotal;
                // rates
                if (!double.IsNaN(item.GdpGrowth)) this.Stats.GdpGrowth += item.GdpGrowth;
                if (!double.IsNaN(item.BirthRate)) this.Stats.BirthRate += item.BirthRate;
                if (!double.IsNaN(item.DeathRate)) this.Stats.DeathRate += item.DeathRate;
                if (!double.IsNaN(item.LifeExpectancy)) this.Stats.LifeExpectancy += item.LifeExpectancy;
                if (!double.IsNaN(item.LiteracyRate)) this.Stats.LiteracyRate += item.LiteracyRate;
                if (!double.IsNaN(item.UnemploymentRate)) this.Stats.UnemploymentRate += item.UnemploymentRate;
                if (!double.IsNaN(item.InternetRate)) this.Stats.InternetRate += item.InternetRate;
                if (!double.IsNaN(item.ElectricityRate)) this.Stats.ElectricityRate += item.ElectricityRate;
                if (!double.IsNaN(item.RoadsDensity)) this.Stats.RoadsDensity += item.RoadsDensity;
                if (!double.IsNaN(item.HospitalBeds)) this.Stats.HospitalBeds += item.HospitalBeds;
                if (!double.IsNaN(item.ArmedForcesRate)) this.Stats.ArmedForcesRate += item.ArmedForcesRate;
            }

            this.Stats.GdpPerCapita = this.Stats.Gdp / this.Stats.Population;
            this.Stats.DebtPerCapita = this.Stats.DebtTotal / this.Stats.Population;
            this.Stats.DebtPerGdp = this.Stats.DebtTotal / this.Stats.Gdp;
            this.Stats.PopulationDensity = this.Stats.Population / this.Stats.LandArea;
            // rates average
            this.Stats.GdpGrowth = this.Stats.GdpGrowth / this.Count;
            this.Stats.BirthRate = this.Stats.BirthRate / this.Count;
            this.Stats.DeathRate = this.Stats.DeathRate / this.Count;
            this.Stats.LifeExpectancy = this.Stats.LifeExpectancy / this.Count;
            this.Stats.LiteracyRate = this.Stats.LiteracyRate / this.Count;
            this.Stats.UnemploymentRate = this.Stats.UnemploymentRate / this.Count;
            this.Stats.InternetRate = this.Stats.InternetRate / this.Count;
            this.Stats.ElectricityRate = this.Stats.ElectricityRate / this.Count;
            this.Stats.RoadsDensity = this.Stats.RoadsDensity / this.Count;
            this.Stats.HospitalBeds = this.Stats.HospitalBeds / this.Count;
            this.Stats.ArmedForcesRate = this.Stats.ArmedForcesRate / this.Count;
           
        }
    }
}