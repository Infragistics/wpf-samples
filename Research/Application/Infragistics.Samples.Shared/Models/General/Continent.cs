using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models.General
{
    public class Continent : ObservableModel
    {

        public Continent()
        {
        }

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }


        private IEnumerable<Country> countries;
        public IEnumerable<Country> Countries
        {
            get
            {
                return this.countries;
            }
            set
            {
                if (this.countries != value)
                {
                    this.countries = value;
                    this.OnPropertyChanged("Countries");
                }
            }
        }
    }
}