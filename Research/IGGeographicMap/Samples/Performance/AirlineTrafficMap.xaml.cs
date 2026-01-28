using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using IGGeographicMap.Models;
using IGGeographicMap.Resources;
using IGGeographicMap.Samples.Custom;
using Infragistics.Controls.Maps;                   // GeographicHighDensityScatterSeries
using Infragistics.Samples.Shared.DataProviders;    // CsvDataProvider
using Infragistics.Samples.Shared.Models;           // GeoPlace
using Infragistics.Samples.Shared.Models.Navigation;
using Infragistics.Samples.Shared.Resources;        // CommonStrings
using System.Globalization;

namespace IGGeographicMap.Samples
{
    public partial class AirlineTrafficMap : Infragistics.Samples.Framework.SampleContainer
    {
        public AirlineTrafficMap()
        {
            InitializeComponent();
            this.SampleLoaded += new EventHandler(AirlineTrafficMap_SampleLoaded);
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;

            this.AirlineTracker = new AirlineTracker();
            this.AirlineTracker.AirTrafficGenerated += OnAirTrafficGenerated;

            // create data provider for reading data from CSV file
            this.AirportsDataProvider = new CsvDataProvider();
            this.AirportsDataProvider.GetCsvDataCompleted += OnGetAirportsDataCompleted;
            this.AirportsDataProvider.GetCsvDataAsync("GeoAirportsAmerica.csv");

            this.AirlineFlightsDataProvider = new CsvDataProvider();
            this.AirlineFlightsDataProvider.GetCsvDataCompleted += OnGetAirlineFlightsDataCompleted;
            this.AirlineFlightsDataProvider.GetCsvDataAsync("GeoFlightsAmerica.csv");
        }

        void AirlineTrafficMap_SampleLoaded(object sender, EventArgs e)
        {
            
        }
        void OnAirTrafficGenerated(object sender, EventArgs e)
        {
            this.GeoMap.Series["AirplanesSeries"].ItemsSource = this.AirlineTracker.Airplanes.Locations;
            this.GeoMap.Series["AirportsSeries"].ItemsSource = this.AirlineTracker.Airports.Locations;

            this.DataContext = this.AirlineTracker;

            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.UnitedStatesLower48Region); // using GeoMapAdapter class
  
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
        }

        protected CsvDataProvider AirportsDataProvider;
        protected CsvDataProvider AirlineFlightsDataProvider;
        protected AirlineTracker AirlineTracker = new AirlineTracker();
    
        private void OnGetAirportsDataCompleted(object sender, GetCsvDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            var airports = ParseAirportData(e.Result);

            this.AirlineTracker.Airports.Add(airports);
            this.AirlineTracker.GenerateAirTraffic();
        }
        private void OnGetAirlineFlightsDataCompleted(object sender, GetCsvDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            var flights = ParseFlightData(e.Result);
            this.AirlineTracker.Flights.Add(flights);

            this.AirlineTracker.GenerateAirTraffic();

       
        }

        public AirportList ParseAirportData(List<string> lines)
        {
            var airports = new AirportList();
            foreach (var line in lines)
            {
                try
                {
                    var vals = line.Split(',');                                      

                    if (vals.Length >= 7 && line != lines[0])
                    {
                        var airport = new Airport
                        {
                            Code = vals[0].Replace("\"","").Trim(),
                            Name = vals[1].Replace("\"", "").Trim(),
                            City = vals[2].Replace("\"", "").Trim(),
                            State = vals[3].Replace("\"", "").Trim(),
                            Country = vals[4].Replace("\"", "").Trim(),
                            Latitude = double.Parse(vals[5], CultureInfo.InvariantCulture),
                            Longitude = double.Parse(vals[6], CultureInfo.InvariantCulture)
                        };
                        airports.Add(airport);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Failed Generating Airports at line " + line);
                    System.Diagnostics.Debug.WriteLine("Exception " + ex);
                    throw;
                }
            }
            return airports;
        }
        public AirlineFlightList ParseFlightData(List<string> lines)
        {
            var flights = new AirlineFlightList();
            int index = 0;
            int maxFlights = 500;
            foreach (var line in lines)
            {
                try
                {
                    var vals = line.Split(',');
                    if (vals.Length >= 5 && line != lines[0] && index < maxFlights)
                    {
                        var flight = new AirlineFlight
                        {
                            CarrierCode = vals[0].Trim(),
                            CarrierName = vals[4].Trim(),
                            Origin = new Airport { Code = vals[1]},
                            Destination = new Airport { Code = vals[2] },
                            FlightNumber = int.Parse(vals[3]),
                        };
                        flights.Add(flight);
                        index++;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Failed parsing Flights at line " + line);
                    System.Diagnostics.Debug.WriteLine("Exception " + ex);
                    throw;
                }
            }
            return flights;
        }
        private void GeoMap_SeriesMouseLeftButtonUp(object sender, Infragistics.Controls.Charts.DataChartMouseButtonEventArgs e)
        {
            var selectedAirplane = e.Item as Airplane;
            if (selectedAirplane != null)
            {
                var series = this.GeoMap.Series["FlightsPathSeriess"] as GeographicPolylineSeries;
                if (series != null)
                    series.ItemsSource = selectedAirplane.Flight.Path.ToPointList();
            }
        }
        private void OnTimeSliderValueChanged(object sender, Infragistics.Controls.Editors.ThumbValueChangedEventArgs<DateTime> e)
        {
            if (!this.AirlineTracker.IsUpdating)
            {
                this.AirlineTracker.UpdateAirTraffic(this.TimeSlider.Value);
            }
        }
        private void TogglePlay_Click(object sender, RoutedEventArgs e)
        {
            this.AirlineTracker.ToggleMotionTimer();

            if (this.AirlineTracker.IsUpdating)
            {
                this.TogglePlay.Content = MapStrings.XWGM_Airline_StopFlights;
            }
            else
            {
                this.TogglePlay.Content = MapStrings.XWGM_Airline_StartFlights;
            }
        }
        private void MotionFrameworkCheckbox_Click(object sender, RoutedEventArgs e)
        {
            var series = this.GeoMap.Series["AirplanesSeries"];

            if (this.MotionFrameworkCheckbox.IsChecked.HasValue && 
                this.MotionFrameworkCheckbox.IsChecked.Value)
            {
                this.AirlineTracker.IsEnabledMotionFramework = true;
                series.TransitionDuration = this.AirlineTracker.MotionTimeInterval; 
            }
            else
            {
                this.AirlineTracker.IsEnabledMotionFramework = false;
                series.TransitionDuration = TimeSpan.FromSeconds(0);
            }
        }
    }
 
}
