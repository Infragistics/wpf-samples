using System.Collections.Generic;
using System.Windows;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents a country in the world and provides its shape as <see cref="GeoShapesList"/>
    /// </summary>
    public class WorldCountry :  GeoShapesList //ObservableModel
    {
        public WorldCountry()
        {
            //this.CountryShape = new List<List<Point>>();
            //_countryShapes = new GeoShapesList();
        }
        #region Properties
        public string Label { get; set; }
        //private GeoShapesList _countryShapes;
        //public GeoShapesList CountryShapes
        //{
        //    get { return _countryShapes; }
        //    set
        //    {
        //        if (_countryShapes == value) return;
        //        _countryShapes = value; OnPropertyChanged("CountryShapes");
        //    }
        //}
        private string _countryCode;
        public string CountryCode
        {
            get { return _countryCode; }
            set
            {
                if (_countryCode == value) return;
                _countryCode = value; OnPropertyChanged("CountryCode");
            }
        }

        private string _countryName;
        public string CountryName
        {
            get { return _countryName; }
            set
            {
                if (_countryName == value) return;
                _countryName = value; OnPropertyChanged("CountryName");
            }
        }

        private string _region;
        public string Region
        {
            get { return _region; }
            set
            {
                if (_region == value) return;
                _region = value; OnPropertyChanged("Region");
            }
        }

        private double? _gdpPerCapita;
        public double? GdpPerCapita
        {
            get { return _gdpPerCapita; }
            set
            {
                if (_gdpPerCapita == value) return;
                _gdpPerCapita = value;
                OnPropertyChanged("GdpPerCapita");
            }
        }

        private double? _unemploymentRate;
        public double? UnemploymentRate
        {
            get { return _unemploymentRate; }
            set
            {
                if (_unemploymentRate == value) return;
                _unemploymentRate = value; OnPropertyChanged("UnemploymentRate");
            }
        }

        private double? _televisions;
        public double? Televisions
        {
            get { return _televisions; }
            set
            {
                if (_televisions == value) return;
                _televisions = value; OnPropertyChanged("Televisions");
            }
        }

        private double? _publicDebt;
        public double? PublicDebt
        {
            get { return _publicDebt; }
            set
            {
                if (_publicDebt == value) return;
                _publicDebt = value; OnPropertyChanged("PublicDebt");
            }
        }
        private double? _area;
        public double? Area
        {
            get { return _area; }
            set
            {
                if (_area == value) return;
                _area = value; OnPropertyChanged("Area");
            }
        }

        private double _population;
        public double Population
        {
            get { return _population; }
            set
            {
                if (_population == value) return;
                _population = value; OnPropertyChanged("Population");
            }
        }

        private double? _oilProduction;
        public double? OilProduction
        {
            get { return _oilProduction; }
            set
            {
                if (_oilProduction == value) return;
                _oilProduction = value; OnPropertyChanged("OilProduction");
            }
        }

        private double? _medianAge;
        public double? MedianAge
        {
            get { return _medianAge; }
            set
            {
                if (_medianAge == value) return;
                _medianAge = value; OnPropertyChanged("MedianAge");
            }
        }

        private double? _internetUsers;
        public double? InternetUsers
        {
            get { return _internetUsers; }
            set
            {
                if (_internetUsers == value) return;
                _internetUsers = value; OnPropertyChanged("InternetUsers");
            }
        }

        private double? _electricityProduction;
        public double? ElectricityProduction
        {
            get { return _electricityProduction; }
            set
            {
                if (_electricityProduction == value) return;
                _electricityProduction = value; OnPropertyChanged("ElectricityProduction");
            }
        }

        private double? _birthRate;
        public double? BirthRate
        {
            get { return _birthRate; }
            set
            {
                if (_birthRate == value) return;
                _birthRate = value; OnPropertyChanged("BirthRate");
            }
        }
        #endregion
        public bool ContainsRegion(List<string> regions)
        {
            foreach (var region in regions)
            {
                var regionName = this.Region.Replace(" ", "").ToLower();
                if (regionName.Contains(region.ToLower()))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Updates this CountryDataModel with values from passed CountryDataModel
        /// </summary>
        /// <param name="newCountry"></param>
        public void UpdateWith(WorldCountry newCountry)
        {
            if (newCountry.CountryName != null) this.CountryName = newCountry.CountryName;
            if (newCountry.GdpPerCapita.HasValue) this.GdpPerCapita = newCountry.GdpPerCapita;
            if (newCountry.BirthRate.HasValue) this.BirthRate = newCountry.BirthRate;
            if (newCountry.ElectricityProduction.HasValue) this.ElectricityProduction = newCountry.ElectricityProduction;
            if (newCountry.InternetUsers.HasValue) this.InternetUsers = newCountry.InternetUsers;
            if (newCountry.MedianAge.HasValue) this.MedianAge = newCountry.MedianAge;
            if (newCountry.OilProduction.HasValue) this.OilProduction = newCountry.OilProduction;
            this.Population = newCountry.Population;
            if (newCountry.PublicDebt.HasValue) this.PublicDebt = newCountry.PublicDebt;
            if (newCountry.Televisions.HasValue) this.Televisions = newCountry.Televisions;
            if (newCountry.UnemploymentRate.HasValue) this.UnemploymentRate = newCountry.UnemploymentRate;
        }
    }
    
    public class WorldCountries : List<WorldCountry>
    {
        public WorldCountries()
        {
            Initialize();
        }
        public void Initialize()
        {
            this.Population = new DoubleRange();
            this.GdpPerCapita = new DoubleRange();
            this.BirthRate = new DoubleRange();
            this.ElectricityProduction = new DoubleRange();
            this.MedianAge = new DoubleRange();
            this.OilProduction = new DoubleRange();
            this.PublicDebt = new DoubleRange();
            this.UnemploymentRate = new DoubleRange();
            this.Televisions = new DoubleRange();
            this.InternetUsers = new DoubleRange();
            this.Area = new DoubleRange();
        }
        public void Recalculate()
        {
            Initialize();
            foreach (var item in this)
            {
                this.Area.Update(item.Area);
                this.GdpPerCapita.Update(item.GdpPerCapita);
                this.Population.Update(item.Population);
                this.BirthRate.Update(item.BirthRate);
                this.ElectricityProduction.Update(item.ElectricityProduction);
                this.MedianAge.Update(item.MedianAge);
                this.OilProduction.Update(item.OilProduction);
                this.PublicDebt.Update(item.PublicDebt);
                this.UnemploymentRate.Update(item.UnemploymentRate);
                this.Televisions.Update(item.Televisions);
                this.InternetUsers.Update(item.InternetUsers);
            }
        }
        public DoubleRange Area { get; set; }
        public DoubleRange Population { get; set; }
        public DoubleRange GdpPerCapita { get; set; }
       
        public DoubleRange BirthRate { get; set; }
        public DoubleRange ElectricityProduction { get; set; }
        public DoubleRange MedianAge { get; set; }
        public DoubleRange OilProduction { get; set; }
        public DoubleRange PublicDebt { get; set; }
        public DoubleRange UnemploymentRate { get; set; }
        public DoubleRange Televisions { get; set; }
        public DoubleRange InternetUsers { get; set; }
      

    }
}