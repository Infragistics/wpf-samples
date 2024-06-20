using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class CountryDataModel : ObservableModel
    {
        #region Properties


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

        private double? _population;
        public double? Population
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

        /// <summary>
        /// Updates this CountryDataModel with values from passed CountryDataModel
        /// </summary>
        /// <param name="newCountry"></param>
        public void UpdateWith(CountryDataModel newCountry)
        {
            if (newCountry.CountryName != null) this.CountryName = newCountry.CountryName;
            if (newCountry.GdpPerCapita.HasValue) this.GdpPerCapita = newCountry.GdpPerCapita;
            if (newCountry.BirthRate.HasValue) this.BirthRate = newCountry.BirthRate;
            if (newCountry.ElectricityProduction.HasValue) this.ElectricityProduction = newCountry.ElectricityProduction;
            if (newCountry.InternetUsers.HasValue) this.InternetUsers = newCountry.InternetUsers;
            if (newCountry.MedianAge.HasValue) this.MedianAge = newCountry.MedianAge;
            if (newCountry.OilProduction.HasValue) this.OilProduction = newCountry.OilProduction;
            if (newCountry.Population.HasValue) this.Population = newCountry.Population;
            if (newCountry.PublicDebt.HasValue) this.PublicDebt = newCountry.PublicDebt;
            if (newCountry.Televisions.HasValue) this.Televisions = newCountry.Televisions;
            if (newCountry.UnemploymentRate.HasValue) this.UnemploymentRate = newCountry.UnemploymentRate;
        }
        public override string ToString()
        {
            return " PublicDebt = " + PublicDebt + " " + CountryName + " Population = " + Population + " GDP = " + GdpPerCapita;
        } 
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(CountryName)) return false;
            if (!this.GdpPerCapita.HasValue || this.GdpPerCapita < 1) return false;
            if (!this.PublicDebt.HasValue || this.PublicDebt < 1) return false;
            if (!this.Population.HasValue || this.Population < 1) return false;
             
            return true;
        }
    }
    public class CountryDataCollection : ObservableCollection<CountryDataModel>
    {

    }
}
