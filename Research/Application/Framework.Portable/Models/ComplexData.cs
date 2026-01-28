 
using System;
using System.Collections.Generic; 

namespace Infragistics.Framework
{
    public class ComplexData : List<CountryModel>
    {
        public ComplexData()
        {
            var random = new Random();

            for (int i = 0; i < Countries.Length; i++)
            {
                var country = CreateCountry();
                country.Name = Countries[i];

                this.Add(country);
            }
        }
        protected Random Random = new Random();
        protected string[] Countries = new string[] { "USA", "GER", "RUS", "POL", "BRA", "CAN", "FRA", "AUS", "ITA", "CHN" };


        public CountryModel CreateCountry()
        {
            var country = new CountryModel();
            country.Population = Random.Next(10, 100);
            country.Gdp = Random.Next(50, 100);
            country.Dept = Random.Next(50, 100);
            for (int i = 0; i < 20; i++)
            {
                var city = CreateCity();
                city.Name = "City" + i;
                country.Cities.Add(city);
            }
            return country;
        }
        public CityModel CreateCity()
        {
            var city = new CityModel();
            city.Lat = Random.Next(-85, 85);
            city.Long = Random.Next(180, 180);
            city.Spending = Random.Next(100, 200);
            city.Income = Random.Next(100, 200);

            return city;
        }
    }


    public class CountryModel
    {
        public List<CityModel> Cities { get; set; }

        public string Name { get; set; }
        public int Population { get; set; }
        public int Gdp { get; set; }
        public int Dept { get; set; }

        public CountryModel()
        {
            Cities = new List<CityModel>();
        }
    }

    public class CityModel
    {
        public string Name { get; set; }
        public int Lat { get; set; }
        public int Long { get; set; }

        public int Spending { get; set; }
        public int Income { get; set; }

    }




}
