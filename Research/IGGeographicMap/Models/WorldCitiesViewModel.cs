using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Models
{
    /// <summary>
    /// Represents the world city view model with a collection of cities
    /// </summary>
    public class WorldCitiesViewModel : ObservableModel
    {
        public WorldCitiesViewModel()
        {
            _worldCities = new ObservableCollection<WorldCityModel>();

            _worldCitiesFilter = new WorldCitiesFilter();
            _worldCitiesFilter.PropertyChanged += OnWorldCitiesFilterPropertyChanged;

            _worldCitiesViewSource = new CollectionViewSource();
            _worldCitiesViewSource.Source = this.WorldCities;
            _worldCitiesViewSource.Filter += this.WorldCitiesFilter.FilterCities;
        }
        public void SortCitiesBy(string propertyName, ListSortDirection direction = ListSortDirection.Descending)
        {
            this.WorldCitiesViewSource.SortBy(propertyName, direction);
            this.WorldCitiesViewSource.Source = _worldCities;
            OnPropertyChanged("WorldCitiesViewSource");
        }
        #region Properties
        private ObservableCollection<WorldCityModel> _worldCities;
        public ObservableCollection<WorldCityModel> WorldCities
        {
            get { return _worldCities; }
            set
            {
                _worldCities = value;
                this.WorldCitiesViewSource.Source = _worldCities;
                OnPropertyChanged("WorldCities");
            }
        }

        private CollectionViewSource _worldCitiesViewSource;
        public CollectionViewSource WorldCitiesViewSource
        {
            get { return _worldCitiesViewSource; }
            private set
            {
                _worldCitiesViewSource = value;
                OnPropertyChanged("WorldCitiesViewSource");
            }
        }
        private WorldCitiesFilter _worldCitiesFilter;
        public WorldCitiesFilter WorldCitiesFilter
        {
            get { return _worldCitiesFilter; }
            set
            {
                _worldCitiesFilter = value;
                _worldCitiesViewSource.Filter += _worldCitiesFilter.FilterCities;
                OnPropertyChanged("WorldCitiesFilter");
            }
        } 
        #endregion
        private void OnWorldCitiesFilterPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            string propertyName = e.PropertyName;

            if (propertyName == "CityMinPopulation" || propertyName == "CityMaxPopulation" ||
                propertyName == "CityCountry" )
            {
                this.WorldCitiesViewSource.View.Refresh();
            }
        }
    }

    /// <summary>
    /// Represents a filter of collection of City models for CollectionViewSource
    /// </summary>
    public class WorldCitiesFilter : ObservableModel
    {
        public WorldCitiesFilter()
            : this(10000.0, 50000000.0)
        { }
        public WorldCitiesFilter(double minPopulation, double maxPopulation)
        {
            _cityMinPopulation = minPopulation;
            _cityMaxPopulation = maxPopulation;
            _cityCountry = string.Empty;
        }

        public void FilterCities(object sender, FilterEventArgs e)
        {
            var city = ((WorldCityModel)e.Item);
             
            e.Accepted = IsCityInPopulationRange(city) &&
                         IsCityInSameCountry(city);
        }


        public bool IsCityInPopulationRange(WorldCityModel city)
        {
            if (city == null) return false;
            if (city.Population < 0) return false;

            return city.Population >= this.CityMinPopulation && city.Population <= this.CityMaxPopulation;
        }
        public bool IsCityInSameCountry(WorldCityModel city)
        {
            if (city == null) return false;
            if (this.CityCountry == string.Empty) return true;

            return city.CityName.Equals(this.CityCountry);
        }

        private double _cityMinPopulation;
        public double CityMinPopulation
        {
            get { return _cityMinPopulation; }
            set
            {
                _cityMinPopulation = value;
                OnPropertyChanged("CityMinPopulation");
            }
        }
        private double _cityMaxPopulation;
        public double CityMaxPopulation
        {
            get { return _cityMaxPopulation; }
            set
            {
                _cityMaxPopulation = value;
                OnPropertyChanged("CityMaxPopulation");
            }
        }
        private string _cityCountry;
        public string CityCountry
        {
            get { return _cityCountry; }
            set
            {
                _cityCountry = value;
                OnPropertyChanged("CityCountry");
            }
        }

    }

}