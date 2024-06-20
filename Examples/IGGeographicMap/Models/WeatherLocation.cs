using System;
using System.Collections.Generic;
using IGGeographicMap.Resources;
using Infragistics.Samples.Shared.DataProviders;    // ImageFilePathProvider
using Infragistics.Samples.Shared.Models;           // GeoLocation

namespace IGGeographicMap.Models
{
    /// <summary>
    /// Represents weather conditions in a geographic location (GeoLocation)
    /// </summary>
    public class WeatherLocation : GeoLocation
    {
        public WeatherLocation()
        {
            this.Weather = new Weather();
            
        }
        public Weather Weather { get; set; }
      
        public string CountryName { get; set; }
        public string CityName { get; set; }
    }

    /// <summary>
    /// Represents a generator of weather conditions 
    /// </summary>
    public class WeatherGenerator
    {
        protected static Random Random = new Random();

        public static double GenerateTemperature()
        {
            double temp = Random.Next(-20, 40);
            return temp;
        }
        public static Temperature GenerateTemperature(WeatherCondition weatherCondition)
        {
            double temp = 0.0;

            if (weatherCondition == WeatherCondition.HazyDay)
                temp = Random.Next(15, 25);

            else if (weatherCondition == WeatherCondition.ClearDay)
                temp = Random.Next(10, 35);

            else if (weatherCondition == WeatherCondition.MostlyCloudyDay)
                temp = Random.Next(10, 15);

            else if (weatherCondition == WeatherCondition.PartlyCloudyDay)
                temp = Random.Next(10, 20);

            else if (weatherCondition == WeatherCondition.RainyDay)
                temp = Random.Next(5, 20);

            else if (weatherCondition == WeatherCondition.WindyDay)
                temp = Random.Next(0, 15);

            else if (weatherCondition == WeatherCondition.ThunderstormDay)
                temp = Random.Next(10, 25);

            else if (weatherCondition == WeatherCondition.SnowyDay)
                temp = Random.Next(-15, 5);

            temp += Random.NextDouble();

            return new Temperature(temp);
        }
        public static Temperature GenerateTemperature(GeoLocation location)
        {
            double temp = 0.0;

            if (location.Latitude < -65 || location.Latitude > 50)
                temp = Random.Next(-20, 0); // ColdWeatherConditions

            else if (location.Latitude < -50 || location.Latitude > 40)
                temp = Random.Next(-5, 10); // AllWeatherConditions

            else if (location.Latitude < -40 || location.Latitude > 25)
                temp = Random.Next(10, 20); // WarmWeatherConditions

            else if (location.Latitude > 10 || location.Latitude < 25)
                temp = Random.Next(20, 20); // DesertWeatherConditions

            else if (location.Latitude > -40 || location.Latitude < 10)
                temp = Random.Next(15, 25); // TopicalWeatherConditions

            temp += Random.NextDouble();

            return new Temperature(temp);
        }

        public static WeatherCondition GenerateWeatherCondition(GeoLocation location)
        {
            //if (location.Latitude >= -20 && location.Latitude <= 20)
            //    return GenerateWeatherCondition(WeatherGenerator.DesertWeatherConditions);

            if (location.Latitude < -65 || location.Latitude > 50)
                return GenerateWeatherCondition(WeatherGenerator.ColdWeatherConditions);

            else if (location.Latitude < -50 || location.Latitude > 40)
                return GenerateWeatherCondition(WeatherGenerator.AllWeatherConditions);

            else if (location.Latitude < -40 || location.Latitude > 25)
                return GenerateWeatherCondition(WeatherGenerator.WarmWeatherConditions);

            else if (location.Latitude > 10 || location.Latitude < 25)
                return GenerateWeatherCondition(WeatherGenerator.DesertWeatherConditions);

            else if (location.Latitude > -40 || location.Latitude < 10)
                return GenerateWeatherCondition(WeatherGenerator.TopicalWeatherConditions);


            return GenerateWeatherCondition();
        }

        public static WeatherCondition GenerateWeatherCondition()
        {
            return GenerateWeatherCondition(WeatherGenerator.AllWeatherConditions);
        }
        public static WeatherCondition GenerateWeatherCondition(List<WeatherCondition> conditions)
        {
            int index = Random.Next(0, conditions.Count - 1);
            return conditions[index];
        }

        #region Weather Patterns
        public static List<WeatherCondition> AllWeatherConditions
        {
            get
            {
                var list = new List<WeatherCondition>
                {
                    WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, 
                    WeatherCondition.MostlyCloudyDay, WeatherCondition.MostlyCloudyDay, WeatherCondition.MostlyCloudyDay, 
                    WeatherCondition.PartlyCloudyDay, WeatherCondition.PartlyCloudyDay, WeatherCondition.PartlyCloudyDay, WeatherCondition.PartlyCloudyDay,  WeatherCondition.PartlyCloudyDay,
                    WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, 
                    WeatherCondition.WindyDay, WeatherCondition.WindyDay, WeatherCondition.WindyDay, WeatherCondition.WindyDay, 
                    WeatherCondition.HazyDay, WeatherCondition.HazyDay, WeatherCondition.HazyDay, 
                    WeatherCondition.ThunderstormDay, WeatherCondition.ThunderstormDay, 
                    WeatherCondition.SnowyDay, WeatherCondition.SnowyDay,
                };
                return list;
            }
        }
        public static List<WeatherCondition> ColdWeatherConditions
        {
            get
            {
                var list = new List<WeatherCondition>
                {
                    WeatherCondition.ClearDay, WeatherCondition.ClearDay, 
                    WeatherCondition.MostlyCloudyDay, WeatherCondition.MostlyCloudyDay, WeatherCondition.MostlyCloudyDay, WeatherCondition.MostlyCloudyDay, WeatherCondition.MostlyCloudyDay, 
                    WeatherCondition.PartlyCloudyDay, WeatherCondition.PartlyCloudyDay,  WeatherCondition.PartlyCloudyDay,
                    WeatherCondition.WindyDay, WeatherCondition.WindyDay, WeatherCondition.WindyDay, WeatherCondition.WindyDay, 
                    WeatherCondition.SnowyDay, WeatherCondition.SnowyDay, WeatherCondition.SnowyDay, WeatherCondition.SnowyDay, WeatherCondition.SnowyDay,
                };
                return list;
            }
        }
        public static List<WeatherCondition> WarmWeatherConditions
        {
            get
            {
                var list = new List<WeatherCondition>
                {
                    WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, 
                    WeatherCondition.MostlyCloudyDay, WeatherCondition.MostlyCloudyDay, 
                    WeatherCondition.PartlyCloudyDay,  WeatherCondition.PartlyCloudyDay,
                    WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, 
                    WeatherCondition.HazyDay, WeatherCondition.HazyDay, WeatherCondition.HazyDay, WeatherCondition.HazyDay, 
                    WeatherCondition.ThunderstormDay, WeatherCondition.ThunderstormDay, 
                };
                return list;
            }
        }
        public static List<WeatherCondition> TopicalWeatherConditions
        {
            get
            {
                var list = new List<WeatherCondition>
                {
                    WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, 
                    WeatherCondition.PartlyCloudyDay, WeatherCondition.PartlyCloudyDay, WeatherCondition.PartlyCloudyDay,  WeatherCondition.PartlyCloudyDay,
                    WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, WeatherCondition.RainyDay, 
                    WeatherCondition.HazyDay, WeatherCondition.HazyDay, WeatherCondition.HazyDay, 
                    WeatherCondition.ThunderstormDay, WeatherCondition.ThunderstormDay, 
                };
                return list;
            }
        }
        public static List<WeatherCondition> DesertWeatherConditions
        {
            get
            {
                var list = new List<WeatherCondition>
                {
                    WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, WeatherCondition.ClearDay, 
                    WeatherCondition.PartlyCloudyDay,   
                    WeatherCondition.HazyDay, WeatherCondition.HazyDay, WeatherCondition.HazyDay, WeatherCondition.HazyDay, 
                };
                return list;
            }
        } 
        #endregion

    }

    /// <summary>
    /// Represents weather pattern
    /// </summary>
    public class Weather 
    {
        public Weather()
        {
            this.Temperature = new Temperature();
            this.Conditions = WeatherCondition.ClearDay;
        }
        public WeatherCondition Conditions { get; set; }
        /// <summary>
        /// Gets or sets Temperature in Celsius
        /// </summary>
        public Temperature Temperature { get; set; }
        //public string ImagePath { get; set; }

        public string ImagePath
        {
            get
            {

                string path = ImageFilePathProvider.GetImageLocalPath("Weather/" + this.Conditions + ".png");
                return path;
            }
        }
        //string path = ImageFilePathProvider.GetImageLocalPath(value.ToString());
    }

    /// <summary>
    /// Represents a temperature, including the scale the temperature is recorded in.
    /// </summary>
    public class Temperature
    {
        /// <summary>
        /// Gets or sets the temperature value. 
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Gets the scale symbol of the temperature.
        /// The scale symbol is set when the temperature scale is changed.
        /// </summary>
        public string Symbol { get; private set; }

        private TemperatureScale scale;

        private double celsiusValue, kelvinValue, fahrenheitValue, rankineValue;

        public Temperature()
        {
            this.celsiusValue = 25.0;
            this.Scale = TemperatureScale.Celsius;
            convertTemperatures();

        }

        public Temperature(double temperature)
        {
            this.celsiusValue = temperature;
            this.Scale = TemperatureScale.Celsius;

            convertTemperatures();
        }

        
        /// <summary>
        /// Automatically sets the temperature, and symbol to the appropriate values when the scale is set.
        /// </summary>
        public TemperatureScale Scale
        {
            get { return scale; }

            set
            {
                switch (value)
                {
                    case TemperatureScale.Celsius:
                        this.Value = this.celsiusValue;
                        this.Symbol = MapStrings.XWGM_Celsius_Symbol;
                        break;
                    case TemperatureScale.Kelvin:
                        this.Value = this.kelvinValue;
                        this.Symbol = MapStrings.XWGM_Kelvin_Symbol;
                        break;
                    case TemperatureScale.Fahrenheit:
                        this.Value = this.fahrenheitValue;
                        this.Symbol = MapStrings.XWGM_Fahrenheit_Symbol;
                        break;
                    case TemperatureScale.Rankine:
                        this.Value = this.rankineValue;
                        this.Symbol = MapStrings.XWGM_Rankine_Symbol;
                        break;
                }
                    
                scale = value;
            }
        }

        private void convertTemperatures()
        {
            this.fahrenheitValue = this.celsiusValue * 1.8 + 32;
            this.kelvinValue = this.celsiusValue + 273.15;          
            this.rankineValue = (this.celsiusValue + 273.15) * 1.8;
        }
    }

    /// <summary>
    /// Specifies weather conditions 
    /// </summary>
    public enum WeatherCondition
    {
        ClearDay,
        ClearNight,
        HazyDay,
        HazyNight,
        MostlyCloudyDay,
        MostylyCloudyNight,
        PartlyCloudyDay,
        PartlyCloudyNight,
        RainyDay,
        RainyNight,
        SnowyDay,
        SnowyNight,
        ThunderstormDay,
        ThunderstormNight,
        WindyDay,
        WindyNight,
    }

    /// <summary>
    /// Specifies temperature scales.
    /// </summary>
    public enum TemperatureScale
    {
        Celsius = 0,         
        Kelvin = 1,
        Fahrenheit = 2,
        Rankine = 3
    }
}