using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class WorldCitiesDataSample : WorldCityList
    {
        public WorldCitiesDataSample()
        {
            ObservableCollection<GeoLocation> cities = GeoLocations.Cities;
            foreach (GeoLocation city in cities)
            {
                this.Add(WorldCity.FromLocation(city));
            }
            
        }
    }
    
}