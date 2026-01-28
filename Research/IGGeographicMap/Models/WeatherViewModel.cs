using System.Collections.ObjectModel;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Models
{
    public class WeatherViewModel : ObservableModel
    {
        public WeatherViewModel()
        {
            _weatherLocations = new ObservableCollection<WeatherLocation>();
        }
        private ObservableCollection<WeatherLocation> _weatherLocations;
        public ObservableCollection<WeatherLocation> WeatherLocations
        {
            get { return _weatherLocations; }
            set
            {
                _weatherLocations = value;
                OnPropertyChanged("WeatherLocations");
            }
        }

    }
}