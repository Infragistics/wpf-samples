using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.General
{
    public class Country : ObservableModel
    {

        public Country()
        {
        }

        private string countryName;
        public string CountryName
        {
            get
            {
                return this.countryName;
            }
            set
            {
                if (this.countryName != value)
                {
                    this.countryName = value;
                    this.OnPropertyChanged("CountryName");
                }
            }
        }

        private IEnumerable<City> cities;
        public IEnumerable<City> Cities
        {
            get
            {
                return this.cities;
            }
            set
            {
                if (this.cities != value)
                {
                    this.cities = value;
                    this.OnPropertyChanged("Cities");
                }
            }
        }
    }
}
