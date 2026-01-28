using System;
using System.Collections.Generic;
using System.Linq;

namespace Infragistics.Framework
{
    /// <summary>
    /// Represents a provider with data about World
    /// </summary>
    public static class WorldData
    {
        /// <summary> Gets or sets Stats </summary>
        public static WorldCountries Countries { get; internal set; }

        /// <summary> Gets or sets Stats By Country </summary>
        public static Dictionary<string, WorldCountry> StatsByCountry { get; internal set; }

        /// <summary> Gets or sets Stats By Region </summary>
        public static Dictionary<string, WorldCountries> StatsByRegion { get; internal set; }

        /// <summary> Gets or sets Regions </summary>
        public static List<WorldCountry> CountriesList { get { return StatsByCountry.Values.ToList(); } }
        
        /// <summary> Gets or sets Regions </summary>
        public static List<WorldCountries> RegionsList { get { return StatsByRegion.Values.ToList(); } }

        /// <summary> Gets or sets Regions Stats </summary>
        public static List<WorldCountry> Regions { get; set; }

        public static Dictionary<string, List<WorldIndicator>> Indicators { get; internal set; }

        /// <summary> Gets or sets Regions Stats </summary>
        public static WorldCountry World { get { return Countries.Stats; } }

        static WorldData()
        {
            Countries = new WorldCountries();
            StatsByCountry = new Dictionary<string, WorldCountry>();
            StatsByRegion = new Dictionary<string, WorldCountries>();

            LoadData("world-stats.csv");
        }

        public static void ClearData()
        {
            Countries = new WorldCountries();
            StatsByCountry = new Dictionary<string, WorldCountry>();
            StatsByRegion = new Dictionary<string, WorldCountries>();
        }

        /// <summary> Gets or sets IsLoaded </summary>
        public static bool IsLoaded { get; internal set; }

        public static void LoadData(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                fileName = "world-stats.csv";

            if (fileName == "world-stats.csv" && IsLoaded)
            {
                return;
            }

            IsLoaded = false;
             
            var defaultValue = double.NaN; //0.0; // double.NaN;

            Countries = new WorldCountries();
            StatsByCountry = new Dictionary<string, WorldCountry>();
            StatsByRegion = new Dictionary<string, WorldCountries>();

            var csv = DataProvider.GetCsvTable(fileName);
             
            for (var i = 1; i < csv.Rows.Count; i++)
            {
                var isCountry = csv[i, "IsCountry"] == "yes";
                if (!isCountry) continue;

                var country = new WorldCountry();

                #region Data Parsing

                country.Name = csv[i, "Country Name"];
                country.Code = csv[i, "Country Code"];
                country.Region = csv[i, "Region"];

                country.Population = csv[i, "Population Total"].ToDouble(defaultValue) / 1000000;
                country.PopulationDensity = csv[i, "Population Density"].ToDouble(defaultValue);
                country.PopulationGrowth = csv[i, "Population Growth"].ToDouble(defaultValue);
                country.PopulationUrban = csv[i, "Population Urban"].ToDouble(defaultValue);
                country.BirthRate = csv[i, "Birth Rate"].ToDouble(defaultValue);
                country.DeathRate = csv[i, "Death Rate"].ToDouble(defaultValue);
                country.LifeExpectancy = csv[i, "Life Expectancy"].ToDouble(defaultValue);

                country.Migration = csv[i, "Migration"].ToDouble(defaultValue) / 1000000;
                country.LiteracyRate = csv[i, "Literacy Rate"].ToDouble(defaultValue);
                country.UnemploymentRate = csv[i, "Unemployment"].ToDouble(defaultValue);
                country.LandArea = csv[i, "Land Area"].ToDouble(defaultValue) / 1000000;
                country.Gdp = csv[i, "Gdp"].ToDouble(defaultValue) / 1000000;
                country.GdpPerCapita = csv[i, "Gdp Per Capita"].ToDouble(defaultValue);
                country.GdpGrowth = csv[i, "Gdp Growth"].ToDouble(defaultValue);
                country.Trade = csv[i, "Trade"].ToDouble(defaultValue) / 1000000;
                country.DebtTotal = csv[i, "Debt Total"].ToDouble(defaultValue) / 1000000;
                country.DebtPerCapita = csv[i, "Debt Per Captia"].ToDouble(defaultValue);
                country.DebtPerGdp = csv[i, "Debt Per Gdp"].ToDouble(defaultValue);

                country.InternetUsers = csv[i, "Internet Users"].ToDouble(defaultValue) / 1000000;
                country.InternetRate = csv[i, "Internet Rate"].ToDouble(defaultValue);
                country.ElectricityRate = csv[i, "Electricity Rate"].ToDouble(defaultValue);
                country.ElectricityProduction = csv[i, "Electricity Production"].ToDouble(defaultValue) / 1000000;
                country.ElectricityCoal = csv[i, "Electricity Coal"].ToDouble(defaultValue) / 1000000;
                country.ElectricityWater = csv[i, "Electricity Water"].ToDouble(defaultValue) / 1000000;
                country.ElectricityGas = csv[i, "Electricity Gas"].ToDouble(defaultValue) / 1000000;
                country.ElectricityNuclear = csv[i, "Electricity Nuclear"].ToDouble(defaultValue) / 1000000;
                country.ElectricityOil = csv[i, "Electricity Oil"].ToDouble(defaultValue) / 1000000;
                country.ElectricityRenewable = csv[i, "Electricity Renewable"].ToDouble(defaultValue) / 1000000;
                country.EmissionsCo2 = csv[i, "Emissions CO2"].ToDouble(defaultValue) / 1000000;
                country.RoadsDensity = csv[i, "Roads Density"].ToDouble(defaultValue);
                country.RoadsPassengers = csv[i, "Roads Passengers"].ToDouble(defaultValue) / 1000000;
                country.AirPassengers = csv[i, "Air Passengers"].ToDouble(defaultValue) / 1000000;
                country.RailwaysPassengers = csv[i, "Railways Passengers"].ToDouble(defaultValue) / 1000000;
                country.MotorVehicles = csv[i, "Motor Vehicles"].ToDouble(defaultValue) / 1000000;
                country.HospitalBeds = csv[i, "Hospital Beds"].ToDouble(defaultValue) / 1000000;
                country.TelephoneLines = csv[i, "Telephone Lines"].ToDouble(defaultValue) / 1000000;
                country.MobilePhones = csv[i, "Mobile Phones"].ToDouble(defaultValue) / 1000000;
                country.ArmedForcesRate = csv[i, "Armed Forces Rate"].ToDouble(defaultValue);
                country.ArmedForcesTotal = csv[i, "Armed Forces Total"].ToDouble(defaultValue) / 1000000;

                #endregion

                Countries.Add(country);

                if (!StatsByCountry.ContainsKey(country.Name))
                     StatsByCountry.Add(country.Name, country);

                if (StatsByRegion.ContainsKey(country.Region))
                {
                    StatsByRegion[country.Region].Add(country);
                }
                else
                {
                    var items = new WorldCountries {country};
                    StatsByRegion.Add(country.Region, items);
                }
            }
            // update stats
            Countries.UpdateStats("World");
            foreach (var region in StatsByRegion)
            {
                 region.Value.UpdateStats(region.Key);
            }

            Regions = RegionsList.Select(i => i.Stats).ToList();

            Indicators = new Dictionary<string, List<WorldIndicator>>();
            Indicators.Add("LandArea", Regions.Select(r => new WorldIndicator(r.Name, r.LandArea)).ToList());
            Indicators.Add("BirthRate", Regions.Select(r => new WorldIndicator(r.Name, r.BirthRate)).ToList());
            Indicators.Add("DeathRate", Regions.Select(r => new WorldIndicator(r.Name, r.DeathRate)).ToList());
            Indicators.Add("Population", Regions.Select(r => new WorldIndicator(r.Name, r.Population)).ToList());
            Indicators.Add("GdpPerCapita", Regions.Select(r => new WorldIndicator(r.Name, r.GdpPerCapita)).ToList());
            Indicators.Add("DebtPerCapita", Regions.Select(r => new WorldIndicator(r.Name, r.DebtPerCapita)).ToList());

            IsLoaded = true;

        }

        //TODO add asyc loading
        //public event EventHandler<LoadDataEventArgs> LoadDataCompleted;

        //private void OnLoadDataCompleted(IEnumerable data)
        //{
        //    if (this.LoadDataCompleted != null)
        //    {
        //        this.LoadDataCompleted(this, new LoadDataEventArgs(data));
        //    }
        //}
        //private void OnLoadDataCompleted(Exception error)
        //{
        //    if (this.LoadDataCompleted != null)
        //    {
        //        this.LoadDataCompleted(this, new LoadDataEventArgs(error));
        //    }
        //}
    }

    public class WorldIndicator
    {
        public double Value { get; set; }
        public string Name { get; set; }

        public WorldIndicator()
        {
        }
        public WorldIndicator(string name, double value)
        {
            this.Name = name;
            this.Value = value;
        }
    }

}