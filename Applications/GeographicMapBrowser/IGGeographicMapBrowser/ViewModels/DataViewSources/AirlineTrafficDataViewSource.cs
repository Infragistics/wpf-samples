using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using IGExtensions.Common.Frameworks;
using IGExtensions.Common.Maps.DataSelectors;
using IGExtensions.Common.Models;
using Infragistics.Controls.Charts;

namespace IGShowcase.GeographicMapBrowser.ViewModels
{
    //TODO support multiple ViewSource and update items based on filters
    abstract public class MotionFrameworkDataViewSource : GeoDataViewSource
    {
        protected MotionFrameworkDataViewSource()
        {
            //this.DataType = GeoDataType.Locations;
            this.DataMotionFramework = new MotionTimeFramework();
            //this.PropertyChanged += OnViewSourcePropertyChanged;
        }

        private MotionTimeFramework _dataMotionFramework;
        /// <summary>
        /// Gets or sets MotionFramework controller 
        /// </summary>
        public MotionTimeFramework DataMotionFramework
        {
            get { return _dataMotionFramework; }
            set { if (_dataMotionFramework == value) return; _dataMotionFramework = value; OnPropertyChanged("DataMotionFramework"); }
        }
        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "DataSource")
            //{
            //    if (this.DataSource == null || this.DataSource.Points == null || this.DataSource.Points.Count == 0) return;
            //    //TODO update DataWorldRect based on geo-location of data items
            //    var geoPoints = new List<Point>();
            //    foreach (var item in DataSource.Points)
            //    {
            //        geoPoints.Add(item.Point);
            //    }
            //    this.UpdateDataWorldRect(geoPoints);
            //}
        }
    }
    public class AirlineTrafficDataViewSource : MotionFrameworkDataViewSource
    {
        public AirlineTrafficDataViewSource()
        {
            //this.DataSource = new List<GeoSite>();
            //this.DataCategory = GeoDataCategory.Unknown;
            this.DataType = GeoDataType.Locations;
            this.PropertyChanged += OnViewSourcePropertyChanged;

            this.DataMotionFramework.MotionSlider.PropertyChanged += OnMotionSliderChanged;

        }
        private List<AirlineFlight> _dataFlightsSource;
        /// <summary>
        /// Gets or sets DataSource for Flights
        /// </summary>
        public List<AirlineFlight> FlightsDataSource
        {
            get { return _dataFlightsSource; }
            set { if (_dataFlightsSource == value) return; _dataFlightsSource = value; OnPropertyChanged("FlightsDataSource"); }
        }

        private List<Airport> _dataAirportsSource;
        /// <summary>
        /// Gets or sets DataSource for Airports
        /// </summary>
        public List<Airport> AirportsDataSource
        {
            get { return _dataAirportsSource; }
            set { if (_dataAirportsSource == value) return; _dataAirportsSource = value; OnPropertyChanged("AirportsDataSource"); }
        }
        private ObservableCollection<Airport> _actualAirports = new ObservableCollection<Airport>();
        /// <summary>
        /// Gets or sets actual Airports   
        /// </summary>
        public ObservableCollection<Airport> ActualAirports
        {
            get { return _actualAirports; }
            set { if (_actualAirports == value) return; _actualAirports = value; OnPropertyChanged("ActualAirports"); }
        }

        private ObservableCollection<AirlineFlight> _actualFlights = new ObservableCollection<AirlineFlight>();
        /// <summary>
        /// Gets or sets actual Flights   
        /// </summary>
        public ObservableCollection<AirlineFlight> ActualFlights
        {
            get { return _actualFlights; }
            private set { if (_actualFlights == value) return; _actualFlights = value; OnPropertyChanged("ActualFlights"); }
        }

        private void OnMotionSliderChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Value") return;

            var currentTime = this.DataMotionFramework.MotionSlider.Value;
            foreach (AirlineFlight flight in this.ActualFlights)
            {
                flight.Update(currentTime);
            }

            var startedFlights = from flight in this.ActualFlights where flight.DepartureTime.Ticks <= currentTime.Ticks select flight;
            var completedFlights = from flight in this.ActualFlights where flight.ArrivalTime.Ticks <= currentTime.Ticks select flight;
            foreach (var airport in this.AirportsDataSource)
            {
                airport.FlightsOutCount = startedFlights.Count(flight => flight.Origin.Code == airport.Code);
                airport.FlightsInCount = completedFlights.Count(flight => flight.Destination.Code == airport.Code);
            }
          
            #region Without Motion Framework
            // testing reset
            //this.ActualAirports.Clear();
            //this.ActualAirplanes.Clear();

            //var currentTime = this.DataMotionFramework.MotionSlider.Value;

            //if (this.FlightsDataSource == null) return;

            //var currentFlights = from f in this.FlightsDataSource
            //                     where f.DepartureTime.Ticks <= currentTime.Ticks &&
            //                           f.ArrivalTime.Ticks >= currentTime.Ticks
            //                     select f;

            //var currentAirplanes = new List<Airplane>();
            //foreach (AirlineFlight flight in currentFlights)
            //{
            //    //if (flight.IsCompleted) continue;

            //    double progress = flight.GetFlightProgress(currentTime);
            //    if (progress > 0) flight.Update(progress);

            //    var plane = new Airplane()
            //    {
            //        Latitude = flight.FlightLocation.Latitude,
            //        Longitude = flight.FlightLocation.Longitude,
            //        Flight = flight
            //    };
            //    currentAirplanes.Add(plane);
            //    this.ActualAirplanes.Add(plane);
            //}

            //var currentAirports = new List<Airport>();

            //var startedFlights = from f in this.FlightsDataSource where f.DepartureTime.Ticks <= currentTime.Ticks select f;
            //foreach (var flight in startedFlights)
            //{
            //    if (currentAirports.Count(x => x.Code == flight.Origin.Code) == 0)
            //    {
            //        var newAirport = flight.Origin.Copy();
            //        currentAirports.Add(newAirport);
            //        this.ActualAirports.Add(newAirport);
            //    }
            //    currentAirports.Where(x => x.Code == flight.Origin.Code).First().FlightsOutCount++;

            //    if (currentAirports.Count(x => x.Code == flight.Destination.Code) == 0)
            //    {
            //        var newAirport = flight.Destination.Copy();
            //        currentAirports.Add(newAirport);
            //        this.ActualAirports.Add(newAirport);
            //    }
            //    currentAirports.Where(x => x.Code == flight.Destination.Code).First().FlightsInCount++;
            //}

            ////this.ActualAirports = currentAirports;
            ////this.ActualFlights = currentAirplanes; 
            #endregion

        }
        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FlightsDataSource")
            {
                if (this.FlightsDataSource == null || this.FlightsDataSource.Count == 0) return;

                var geoLocations = new List<IGeoLocatable>();
                foreach (var item in this.FlightsDataSource)
                {
                    geoLocations.Add(item.Destination);
                    geoLocations.Add(item.Origin);
                    this.ActualFlights.Add(item);
                }
                this.UpdateDataWorldRect(geoLocations);
            }
            if (e.PropertyName == "AirportsDataSource")
            {
                if (this.AirportsDataSource == null || this.AirportsDataSource.Count == 0) return;
                foreach (var item in this.AirportsDataSource)
                {
                    this.ActualAirports.Add(item);
                }
            }
        }
    }

}