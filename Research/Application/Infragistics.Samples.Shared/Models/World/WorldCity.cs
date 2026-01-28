using System.Collections.Generic;

namespace Infragistics.Samples.Shared.Models
{
    public class WorldCity : GeoLocation
    {
        public WorldCity() {}
        public WorldCity(string name, double latitudeDegrees, double latitudeMinutes, double longitudeDegrees, double longitudeMinutes)
        {
            this.Name = name;
            this.Latitude = latitudeDegrees + latitudeMinutes / 60.0;
            this.Longitude = longitudeDegrees + longitudeMinutes / 60.0;
        }
        /// <summary>
        /// Gets or sets country name for the City
        /// </summary>
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (value == null) return;
                if (value.Equals(_country)) return;
                _country = value.Trim();
                OnPropertyChanged("Country");
            }
        }
        private string _country = string.Empty;

        /// <summary> 
        /// Converts this <see cref="WorldCity"/> object to a <see cref="GeoLocation"/> where 
        /// Point.X is Longitude and Point.Y is Latitude
        /// </summary>
        /// <returns></returns>
        public GeoLocation ToLocation()
        {
            return new GeoLocation(this.Longitude, this.Latitude);
        }
        /// <summary> 
        /// Converts a <see cref="GeoLocation"/> to a <see cref="WorldCity"/> object where 
        /// Point.X is Longitude and Point.Y is Latitude
        /// </summary>
        /// <returns></returns>
        public static WorldCity FromLocation(GeoLocation location)
        {
            string[] names = location.Name.Split(',');
            var city = new WorldCity { Latitude = location.Latitude, Longitude = location.Longitude };
            city.Name = names[0];
            if (names.Length >= 2)
            {
                city.Country = names[1];
            }
            return city; // new City { Latitude = location.Latitude, Longitude = location.Longitude, Name = location.Name };
        }
    }

    public class WorldCityList : List<WorldCity>
    {

    }
}