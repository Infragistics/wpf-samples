using System;
using System.Collections.Generic;
using Infragistics.Samples.Shared.DataProviders;
using System.Globalization;

namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Represents a a collection of geographic places.
    /// </summary>
    public class GeoPlaceCollection : List<GeoPlace>
    {
        public GeoPlaceCollection()
        {
            
        }
        /// <summary>
        /// Occurs when data loading has completed
        /// </summary>
        public event EventHandler<EventArgs> LoadingDataCompleted;
        protected void OnLoadingDataCompleted(EventArgs eventArgs)
        {
            if (this.LoadingDataCompleted != null)
            {
                this.LoadingDataCompleted(this, eventArgs);
            }
        }
        /// <summary>
        /// Loads GeoPlace data from a given CSV file in format: "PlaceName, latitude, longitude"
        /// </summary>
        /// <param name="csvFileName"></param>
        public void Load(string csvFileName)
        {
            // create data provider for reading data from CSV file
            this.CsvDataProvider = new CsvDataProvider();
            this.CsvDataProvider.GetCsvDataCompleted += OnGetCsvDataCompleted;
            this.CsvDataProvider.GetCsvDataAsync(csvFileName);
    
        }
        protected CsvDataProvider CsvDataProvider;
        private void OnGetCsvDataCompleted(object sender, GetCsvDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            this.Clear();

            this.AddRange(ParseGeoPlaces(e.Result));

            OnLoadingDataCompleted(EventArgs.Empty);
        }
        public static List<GeoPlace> ParseGeoPlaces(List<string> csvFileLines)
        {
            var places = new List<GeoPlace>();
            try
            {                                
                CultureInfo culture = CultureInfo.CreateSpecificCulture("en-US");

                foreach (var line in csvFileLines)
                {
                    var vals = line.Split(',');
                    if (vals.Length >= 3)
                    {
                        var name = vals[0];
                        var latitude = double.Parse(vals[1], culture);
                        var longitude = double.Parse(vals[2], culture);
                        var place = new GeoPlace { Name = name, Longitude = longitude, Latitude = latitude };
                        places.Add(place);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }
       
            return places;
        }

    }

    /// <summary>
    /// Represents a geographic place
    /// </summary>
    public class GeoPlace
    {
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}