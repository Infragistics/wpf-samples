using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using System.Windows;
using System.Globalization;

namespace Infragistics.Samples.Shared.Models
{
    /// <summary>
    /// Represents an EarthQuake data model in geographic context: <seealso cref="GeoLocation"/>
    /// </summary>
    public class EarthQuake : GeoLocation
    {
        public EarthQuake()
        {
            this.Magnitude = 0.0;
            this.Depth = 0.0;
        }
        public double Depth { get; set; }
        public double Magnitude { get; set; }
        public string Source { get; set; }
        public string Date { get; set; }

        
    }

    /// <summary>
    /// Represents a data provider for <seealso cref="EarthQuake"/> data models.
    /// </summary>
    public class EarthQuakesProvider
    {
        public EarthQuakesProvider()
        {
            List = new List<EarthQuake>();
            Grid = new List<EarthQuake>();
        }

        /// <summary>
        /// Occurs when data loading has completed
        /// </summary>
        public event EventHandler<EventArgs> LoadingCompleted;
        protected void OnLoadingCompleted(EventArgs eventArgs)
        {
            if (this.LoadingCompleted != null)
            {
                this.LoadingCompleted(this, eventArgs);
            }
        }

        /// <summary>
        /// Loads EarthQuake data from csv file  
        /// </summary>
        public void Load()
        {
            var dataProvider = new CsvDataProvider();
            dataProvider.GetCsvDataCompleted += OnDataLoaded;
            dataProvider.GetCsvDataAsync("earthquakes-usgs.csv");
        } 
        protected Dictionary<string, EarthQuake> Dictionary { get; set; }
        /// <summary>
        /// Gets all EarthQuakes
        /// </summary>
        public List<EarthQuake> List { get; private set; }
        /// <summary>
        /// Gets biggest EarthQuakes
        /// </summary>
        public List<EarthQuake> Grid { get; private set; }


        private void OnDataLoaded(object sender, GetCsvDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            
            List = ParseCsvData(e.Result);

            Dictionary = new Dictionary<string, EarthQuake>();

            var xMin = (int)List.Min(i => i.Longitude);
            var xMax = (int)List.Max(i => i.Longitude);
            var xStep = (xMax - xMin) / 20;

            var yMin = (int)List.Min(i => i.Latitude);
            var yMax = (int)List.Max(i => i.Latitude);
            var yStep = (yMax - yMin) / 20;

            // filterting out data points          
            foreach (var item in List)
            {
                //var x = (int)Math.Floor(item.Longitude / XStep);
                //var y = (int)Math.Floor(item.Longitude / YStep);

                var x = (int)(item.Longitude) / xStep;
                var y = (int)(item.Latitude)  / yStep;

                var key = x + " " + y;

                if (!Dictionary.ContainsKey(key))
                {
                    Dictionary[key] = item;
                }
                else
                {
                    if (Dictionary[key].Magnitude < item.Magnitude)
                    {
                        Dictionary[key] = item;
                    }
                }
            }
            var earthQuakes = Dictionary.Values.ToList();
            earthQuakes.OrderBy(i => i.Magnitude);

            Grid = earthQuakes;
            
            OnLoadingCompleted(EventArgs.Empty);
        }
        private static List<EarthQuake> ParseCsvData(List<string> csvLines)
        {
            var earthQuakes = new List<EarthQuake>();
            double longitude = double.NaN, latitude = double.NaN;
            double depth = double.NaN, magnitude = double.NaN;
            List<string> vals;
            try
            {
                NumberStyles style = NumberStyles.Number;
                

                for (var i = 1; i < csvLines.Count; i++)
                {
                    vals = csvLines[i].Split(',').ToList();
                    
                    if (vals.Count >= 9)
                    {
                        double.TryParse(vals[1], style, CultureInfo.InvariantCulture, out longitude);
                        double.TryParse(vals[8], style, CultureInfo.InvariantCulture, out latitude);
                        double.TryParse(vals[3], style, CultureInfo.InvariantCulture, out depth);
                        double.TryParse(vals[9], style, CultureInfo.InvariantCulture, out magnitude);

                        var earthQuake = new EarthQuake
                        {
                            Name = vals[2].Replace("\"", ""),
                            Longitude = longitude,
                            Latitude = latitude,
                            Depth = depth,
                            Magnitude = magnitude,
                            Date = vals[7],
                            Source = vals[4],
                        };
                        //if (earthQuake.Name.Contains("\""))
                        //    earthQuake.Name = earthQuake.Name.Replace("\"","");

                        earthQuakes.Add(earthQuake);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }

            return earthQuakes;
        }        
    }
}