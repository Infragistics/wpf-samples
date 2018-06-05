using System;
using System.Collections.Generic;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents weather conditions in a geographic location (GeoLocation)
    /// </summary>
    public class WeatherStation : GeoLocation
    {
        //protected DateTime DateTimeInitial = new DateTime(1, 1, 1, 1, 1, 0, 0, DateTimeKind.Utc);
        public WeatherStation()
        {
            this.Weather = new Weather();
            
        }
        ///<summary>
        /// Gets whether station has valid weather data 
        ///</summary>
        public bool IsValid
        {
            get
            {
                var valid = double.IsNaN(Latitude) || double.IsNaN(Latitude) ||
                            this.Weather.DateTime == Weather.DateTimeInitial;
                return valid;
            }
        }
        private Weather _weather;
        /// <summary>
        /// Gets or sets Weather property 
        /// </summary>
        public Weather Weather
        {
            get {  return _weather; }
            set { if (_weather == value) return; _weather = value; OnPropertyChanged("Weather"); }
        }

        public string CountryState { get; set; }
        public string CountryName { get; set; }
        public string StationName { get; set; }
        /// <summary>
        /// Gets or sets station code; e.g. ICAO code 
        /// </summary>
        public string StationCode { get; set; }
    }
    //TODO remove
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
        public static double GenerateTemperature(WeatherCondition weatherCondition)
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

            return temp;
        }
        public static double GenerateTemperature(GeoLocation location)
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

            return temp;
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
    public class Weather : ObservableModel //GeoLocation // ObservableModel
    {
        public static readonly DateTime DateTimeInitial = new DateTime(1, 1, 1, 1, 1, 0, 0, DateTimeKind.Utc);

        public Weather()
        {
            //this.Code = string.Empty;
            //this.Latitude = double.NaN;
            //this.Longitude = double.NaN;
            this.DateTime = DateTimeInitial;   //this.Temperature = 25.0;
            //this.Conditions = WeatherCondition.ClearDay;
        }

        //private WeatherCondition _conditions;
        ///// <summary>
        ///// Gets or sets Condition property 
        ///// </summary>
        //public WeatherCondition Conditions
        //{
        //    get {  return _conditions; }
        //    set { if (_conditions == value) return; _conditions = value; OnPropertyChanged("Conditions"); }
        //}
       
        ////public string ImagePath { get; set; }
        //private double _temperature;
        ///// <summary>
        ///// Gets or sets Temperature property 
        ///// </summary>
        //public double Temperature
        //{
        //    get {  return _temperature; }
        //    set { if (_temperature == value) return; _temperature = value; OnPropertyChanged("Temperature"); }
        //}
      

        /// <summary>
        /// Gets or sets DateTime (UTC) of the last weather update
        /// </summary>
        public DateTime DateTime { get; set; }
        /// <summary>
        /// Gets or sets Wind Pattern
        /// </summary>
        public string WindPattern { get; set; }
        /// <summary>
        /// Gets or sets Wind Speed (in MPH)
        /// </summary>
        public double WindSpeed { get; set; }
        /// <summary>
        /// Gets or sets Wind Speed maximum (in MPH)
        /// </summary>
        public double WindSpeedMax { get; set; }
        /// <summary>
        /// Gets or sets Wind Direction (in degrees) from true North
        /// </summary>
        public double WindDirection { get; set; }
        /// <summary>
        /// Gets or sets Wind Chill
        /// </summary>
        public string Windchill { get; set; }
        /// <summary>
        /// Gets or sets weather Visibility in miles
        /// </summary>
        public double Visibility { get; set; }
        /// <summary>
        /// Gets or sets weather Humidity in percentage
        /// </summary>
        public double Humidity { get; set; }
        /// <summary>
        /// Gets or sets Sky Conditions over the airport
        /// </summary>
        public string SkyConditions { get; set; }
        /// <summary>
        /// Gets or sets weather Pattern over the airport
        /// </summary>
        public string WeatherPattern { get; set; }
        //TODO replace with parser from WeatherPattern
        /// <summary>
        /// Gets or sets weather Pattern over the airport
        /// </summary>
        public WeatherCondition WeatherCondition { get; set; }
        
        /// <summary>
        /// Gets or sets Precipitation (in inches) over last hour 
        /// </summary>
        public double Precipitation { get; set; }
        /// <summary>
        /// Gets or sets Temperature (in Celsius)
        /// </summary>
        public double Temperature { get; set; }
        /// <summary>
        /// Gets or sets Dew Point (in Celsius)
        /// </summary>
        public double DewPoint { get; set; }
        /// <summary>
        /// Gets or sets Altimeter Pressure (in hPa)
        /// </summary>
        public double PressureAltimeter { get; set; }
        /// <summary>
        /// Gets or sets Pressure Tendency (in hPa)
        /// </summary>
        public double PressureTendency { get; set; }
        /// <summary>
        /// Gets or sets Observations in NOAA coded format
        /// </summary>
        public string Observations { get; set; }
        /// <summary>
        /// Gets or sets Cycle
        /// </summary>
        public double Cycle { get; set; }
        /// <summary>
        /// Gets or sets raw weather data in NOAA decoded format
        /// </summary>
        public List<string> Data { get; set; }
        public string ImagePath
        {
            get
            {
                //TODO-MT add ImageFilePathProvider
                //string path = ""; // ImageFilePathProvider.GetImageLocalPath("Weather/" + this.Conditions + ".png");
                string path = "Weather/" + this.WeatherPattern.Replace(" ","") + ".png";
                return path;
            }
        }
        //string path = ImageFilePathProvider.GetImageLocalPath(value.ToString());
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
}