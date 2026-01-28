namespace IGGeographicMap.Models
{
    /// <summary>
    /// Represents an world city model in geographic context (GeoLocation)
    /// </summary>
    public class WorldCityModel : Infragistics.Samples.Shared.Models.GeoLocation
    {
        public WorldCityModel()
        {
            this.Population = 0.0;
        }
        public double Population { get; set; }
        public string CountryName { get; set; }
        public bool IsCapital { get; set; }
        public string CityName { get; set; }

    }
}