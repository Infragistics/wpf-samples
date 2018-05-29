using System;
using System.Collections.Generic;
using IGExtensions.Common.Models;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.DataSelectors
{
    public static class GeoDataParser
    {

        public static EarthQuakes ProcessEarthQuakes(ShapefileConverter shapefile)
        {
            var earthQuakes = new EarthQuakes();
            foreach (ShapefileRecord record in shapefile)
            {
                var item = new EarthQuakeData();
                if (record.Fields != null)
                {
                    // get geo-location from shape file (SHP)
                    //location.Longitude = record.Points[0][0].X;
                    //location.Latitude = record.Points[0][0].Y;
                    // get data about a location from database file (DBF)
                    //if (record.Fields.ContainsKey("lon")) item.Longitude = (double)(record.Fields["lon"]);
                    //if (record.Fields.ContainsKey("lat")) item.Latitude = (double)(record.Fields["lat"]);

                    //if (record.Fields.ContainsKey("eqid")) item.Id = (string)(record.Fields["eqid"]);
                    //if (record.Fields.ContainsKey("depth")) item.Depth = (double)(record.Fields["depth"]);
                    //if (record.Fields.ContainsKey("magnitude")) item.Magnitude = (double)(record.Fields["magnitude"]);
                    //if (record.Fields.ContainsKey("region")) item.Region = (string)(record.Fields["region"]);
                    //if (record.Fields.ContainsKey("datetime")) item.Description = (string)(record.Fields["datetime"]);
                    // TODO-MT parse date time
                    //location.Updated = (System.DateTime)(record.Fields["datetime"]); // Sun Jun 13 00:00:00 -0400 2010

                    item.Longitude = record.Fields.GetValue<double>("lon");
                    item.Latitude = record.Fields.GetValue<double>("lat");
                    item.Code = record.Fields.GetValue<string>("eqid");
                    item.Depth = record.Fields.GetValue<double>("depth");
                    item.Magnitude = record.Fields.GetValue<double>("magnitude");
                    item.Region = record.Fields.GetValue<string>("region");
                    item.Description = record.Fields.GetValue<string>("datetime");

                    item.Label = "Earthquake: " + item.Magnitude + Environment.NewLine + item.Region
                             //  + Environment.NewLine + "Longitude: " + String.Format("{0:0.0}", item.Longitude)
                             //  + Environment.NewLine + "Latitude: " + String.Format("{0:0.0}", item.Latitude)
                               + Environment.NewLine + "Location: " + String.Format("{0:0.0}", item.Longitude) + ", " 
                               + String.Format("{0:0.0}", item.Latitude);

                    // update countries stats
                    earthQuakes.Magnitude.Update(item.Magnitude);
                    earthQuakes.Depth.Update(item.Depth);

                    // add an item to data collection
                    earthQuakes.Add(item);
                }
            }
           
            return earthQuakes;
        }

        public static WorldCountries ProcessWorldCountries(ShapefileConverter shapefile)
        {
            var countries = new WorldCountries();
            foreach (ShapefileRecord record in shapefile)
            {
                var item = new WorldCountry();
                if (record.Fields != null)
                {
                    // get geo-shape from shape file (SHP)
                    item.LoadPoints(record.Points);
                    //location.Longitude = record.Points[0][0].X;
                    //location.Latitude = record.Points[0][0].Y;

                    // get data about a country from old database file (DBF)
                    if (record.Fields.ContainsKey("CODE")) item.CountryCode = (string)(record.Fields["CODE"]);
                    if (record.Fields.ContainsKey("CNTRY_NAME")) item.CountryName = (string)(record.Fields["CNTRY_NAME"]);
                    if (record.Fields.ContainsKey("POP_CNTRY")) item.Population = (double)(record.Fields["POP_CNTRY"]);
                   
                    // get data about a country from new database file (DBF)
                    if (record.Fields.ContainsKey("ISO_2_CODE")) item.CountryCode = (string)(record.Fields["ISO_2_CODE"]);
                    if (record.Fields.ContainsKey("NAME")) item.CountryName = (string)(record.Fields["NAME"]);
                    if (record.Fields.ContainsKey("REGION")) item.Region = (string)(record.Fields["REGION"]);
                    if (record.Fields.ContainsKey("POP2005")) item.Population = (double)(record.Fields["POP2005"]);
                    if (record.Fields.ContainsKey("AREA")) item.Area = (double)(record.Fields["AREA"]);

                    if (item.Population < 0) item.Population = 0;
                    if (item.Area < 0) item.Area = 0;
                   
                    if (item.CountryName == "US" || item.CountryName == "USA") item.CountryName = "United States";
                    if (item.CountryName == "UK" || item.CountryName == "GB") item.CountryName = "United Kingdom";
               
                    item.Label = "Country: " + item.CountryName + ", " + item.Region
                        + Environment.NewLine + "Population: " + String.Format("{0:#,##0,,.0 M}", item.Population)
                        + Environment.NewLine + "Area: " + String.Format("{0:#,##0,.0 K}", item.Area);
                     
                    // update countries stats
                    countries.Population.Update(item.Population);
                    countries.Area.Update(item.Area);

                    // add an item to data collection
                    countries.Add(item);
                }
            }
            countries.Sort((x, y) => -x.Population.CompareTo(y.Population));
            return countries;
        }

        public static WorldCities ProcessWorldCities(ShapefileConverter shapefile)
        {
            var cities = new WorldCities();
            foreach (ShapefileRecord record in shapefile)
            {
                var item = new WorldCity();
                if (record.Fields != null)
                {
                    if (item.Population < 0) item.Population = 0;
                    // get geo-location from shape file (SHP)
                    item.Longitude = record.Points[0][0].X;
                    item.Latitude = record.Points[0][0].Y;
                    // get data about a location from database file (DBF)
                    if (record.Fields.ContainsKey("COUNTRY_NA")) item.CountryName = (string)(record.Fields["COUNTRY_NA"]);
                    if (record.Fields.ContainsKey("NAME")) item.CityName = (string)(record.Fields["NAME"]);
                    if (record.Fields.ContainsKey("POPULATION")) item.Population = (double)(record.Fields["POPULATION"]);
                    if (record.Fields.ContainsKey("CAPITAL")) item.IsCapital = (string)(record.Fields["CAPITAL"]) == "N" ? false : true;

                    //if (item.CountryName == "US" || item.CountryName == "USA") item.CountryName = "United States";
                    //if (item.CountryName == "UK" || item.CountryName == "GB") item.CountryName = "United Kingdom";
                 
                    item.Label = "City: " + item.CityName + ", " + item.CountryName
                    + Environment.NewLine + "Population: " + String.Format("{0:#,##0,,.0 M}", item.Population);

                    cities.Population.Update(item.Population);
                    
                    // add an item to data collection
                    cities.Add(item);
                }
            }
            cities.Sort((x, y) => -x.Population.CompareTo(y.Population));
            return cities;
        }

        public static List<WeatherStation> ProcessWeatherData(ShapefileConverter shapefile)
        {
            var dataItems = new List<WeatherStation>();

            foreach (ShapefileRecord record in shapefile)
            {
                var item = new WeatherStation();
                if (record.Fields != null)
                {
                    // get geo-location from shape file (SHP)
                    item.Longitude = record.Points[0][0].X;
                    item.Latitude = record.Points[0][0].Y;
                    // get data about a location from database file (DBF)
                    if (record.Fields.ContainsKey("COUNTRY_NA")) item.CountryName = (string)(record.Fields["COUNTRY_NA"]);
                    if (record.Fields.ContainsKey("NAME")) item.StationName = (string)(record.Fields["NAME"]);
                    item.Weather.WeatherCondition = WeatherGenerator.GenerateWeatherCondition(item);
                    item.Weather.Temperature = WeatherGenerator.GenerateTemperature(item);
                    // add an item to data collection
                    dataItems.Add(item);
                }
            }
            return dataItems;
        }
    }
}