using System;
using System.Linq;
using System.Windows.Threading;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Models
{
    public class AirlineTracker : ObservableModel
    {
        public AirlineTracker()
        {
            this.Airports = new AirportsViewModel();
            this.Airplanes = new AirplanesViewModel();
            this.Flights = new AirlineFlightsViewModel();

            this.MotionSlider = new AirlineTrackSlider();
            this.MotionSlider.MinValue = DateTime.Now.Date.AddSeconds(0);  
            this.MotionSlider.MaxValue = DateTime.Now.Date.AddDays(2).AddSeconds(0);  
            this.MotionSlider.Value = this.MotionSlider.MinValue;

            UpdateMotionFramework();

            this.MotionTimer.Tick += OnMotionTimerTick;

        }
        #region Event Handlers
        public event EventHandler AirTrafficGenerated;
        protected void OnAirTrafficGenerated()
        {
            EventHandler handler = this.AirTrafficGenerated;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }  

        private void OnMotionTimerTick(object sender, EventArgs e)
        {
            this.MotionSlider.Value = MotionSlider.Value.Add(this.MotionSliderInterval);

            if (this.IsEnabledMotionFramework)
            {
                UpdateAirTrafficWithMotionFramework(this.MotionSlider.Value);
            }
            else
            {
                UpdateAirTraffic(this.MotionSlider.Value);
            }
            
            if (MotionSlider.Value >= MotionSlider.MaxValue)
            {
                ToggleMotionTimer();
            }
        }
        #endregion

        #region Methods
        public void ToggleMotionTimer()
        {
            if (MotionTimer.IsEnabled)
            {
                MotionTimer.Stop();
            }
            else
            {
                if (MotionSlider.Value >= MotionSlider.MaxValue)
                {

                    MotionSlider.Value = MotionSlider.MinValue;
                }
                MotionTimer.Start();
            }
            OnPropertyChanged("IsUpdating");
            OnPropertyChanged("IsMotionSliderEnabled");
     
        }
        public void UpdateSelectedFlights(Airplane airplane)
        {
            //var currentFlights = from f in this.Flights.List
            //                     where currentTime.Ticks <= f.ArrivalTime.Ticks
            //                     //where f.DepartureTime.Ticks <= currentTime.Ticks && currentTime.Ticks <= f.ArrivalTime.Ticks
            //                     select f;
            var key = airplane.Code;
            if (this.Airplanes.Dictionary.ContainsKey(key))
            {
                //this.Airplanes.Dictionary[key]
            }
        }
        /// <summary>
        /// Generate air traffic by populating flights with locations of airports and other flight information, such as:
        /// <para>Flight's departure time, arrival time, origin, destination and etc.</para>
        /// </summary>
        public void GenerateAirTraffic()
        {
            if (this.Airports.List.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("IGGeographicMap -> Failed to generate air traffic with no airport data!");
                return;
            }
            if (this.Flights.List.Count == 0)
            {
                System.Diagnostics.Debug.WriteLine("IGGeographicMap -> Failed to generate air traffic with no flight data!");
                return;
            }
            System.Diagnostics.Debug.WriteLine("IGGeographicMap -> Generating air traffic with " + Flights.List.Count +
                " flights between " + Airports.List.Count + " airports...");

            long min = long.MaxValue;
            long max = long.MinValue;

            //var airplanes = new AirplaneList();
            foreach (var flight in this.Flights.List)
            {
                if (this.Airports.Dictionary.ContainsKey(flight.Origin.Code))
                    flight.Origin = this.Airports.Dictionary[flight.Origin.Code];

                if (this.Airports.Dictionary.ContainsKey(flight.Destination.Code))
                    flight.Destination = this.Airports.Dictionary[flight.Destination.Code];

                if (flight.IsValidFlight)
                {
                    var airplane = new Airplane();
                    flight.DepartureTime = flight.GetDepartureTime();
                    flight.ArrivalTime = flight.GetArrivalTime(airplane);

                    min = System.Math.Min(flight.DepartureTime.Ticks, min);
                    max = System.Math.Max(flight.ArrivalTime.Ticks, max);

                    //var origin = flight.Origin;
                    //airplane.Location = flight.Origin;
                    //airplane.Code = DateTime.Now.TimeOfDay.TotalSeconds.ToString() + " " + flight.FlightCode;
                    //airplane.Flight = flight;
                    //airplanes.Add(airplane);
                }

            }

            this.MotionSlider.MinValue = new DateTime(min - TimeSpan.FromSeconds(1).Ticks);
            this.MotionSlider.MaxValue = new DateTime(max + TimeSpan.FromSeconds(1).Ticks);
            this.MotionSlider.Value = this.MotionSlider.MinValue;

            //this.Airplanes.Add(airplanes);

            OnAirTrafficGenerated();
        }
        public void UpdateAirTrafficWithMotionFramework(DateTime currentTime)
        {
            if (this.Flights == null) return;
            if (this.Flights.List.Count == 0) return;

            Airports.Locations.Clear();
            //Airplanes.Locations.Clear();

            #region Current Flights

            var currentFlights = from f in this.Flights.List
                                 where currentTime.Ticks <= f.ArrivalTime.Ticks
                                 //where f.DepartureTime.Ticks <= currentTime.Ticks && currentTime.Ticks <= f.ArrivalTime.Ticks
                                 select f;

            foreach (AirlineFlight flight in currentFlights)
            {
                var newLocation = flight.CalculateLocation(currentTime);
                var newAirplane = new Airplane
                {
                    Code = flight.FlightCode,
                    Flight = flight,
                    Latitude = newLocation.Latitude,
                    Longitude = newLocation.Longitude,
                };

                var key = newAirplane.Code;
                if (this.Airplanes.Locations.Count(x => x.Code == key) == 1) // if (this.Airplanes.Dictionary.ContainsKey(key))
                {
                    foreach (var airplane in this.Airplanes.Locations)
                    {
                        if (airplane.Code == newAirplane.Code && airplane.IsFlying)
                        {
                            System.Diagnostics.Debug.WriteLine("Updating Airplane " + airplane.ToString());
                            airplane.Latitude = newAirplane.Latitude;
                            airplane.Longitude = newAirplane.Longitude;
                            airplane.Flight = newAirplane.Flight;
                            airplane.Flight.Progress = newAirplane.Flight.Progress;

                            if (airplane.Flight.IsCompleted)
                            {
                                System.Diagnostics.Debug.WriteLine("Landed Airplane " + airplane.ToString());
                            }
                            break;
                        }
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Adding Airplane " + newAirplane.ToString());
                    this.Airplanes.Dictionary.Add(newAirplane);
                    this.Airplanes.Locations.Add(newAirplane);
                }
            }
            #endregion
          
            #region Started Flights

            var startedFlights = from f in this.Flights.List
                                 where f.DepartureTime.Ticks <= currentTime.Ticks //&& currentTime.Ticks <= f.ArrivalTime.Ticks
                                 select f;

            foreach (AirlineFlight flight in startedFlights)
            {
                if (Airports.Locations.Count(x => x.Code == flight.Origin.Code) == 0)
                {
                    var newAirport = flight.Origin.Copy();
                    //newAirport.FlightsOutCount++;
                    Airports.Locations.Add(newAirport);
                }
                Airports.Locations.Where(x => x.Code == flight.Origin.Code).First().FlightsOutCount++;

                if (Airports.Locations.Count(x => x.Code == flight.Destination.Code) == 0)
                {
                    var newAirport = flight.Destination.Copy();
                    //newAirport.FlightsInCount++;
                    Airports.Locations.Add(newAirport);
                }
            }
            #endregion
            #region Completed Flights

            var completedFlights = from f in this.Flights.List
                                   where f.ArrivalTime.Ticks <= currentTime.Ticks
                                   select f;

            foreach (AirlineFlight flight in completedFlights)
            {
                var key = flight.FlightCode;
                // if (this.Airplanes.Dictionary.ContainsKey(key) && this.Airplanes.Dictionary[key].IsFlying)
                if (this.Airplanes.Locations.Count(x => x.Code == key) == 1) 
                {
                    foreach (var airplane in this.Airplanes.Locations)
                    {
                        if (airplane.Code == flight.FlightCode)
                        {
                            var location = flight.CalculateLocation(currentTime);

                            System.Diagnostics.Debug.WriteLine("Landing Airplane " + airplane.ToString());
                            airplane.Latitude = location.Latitude;
                            airplane.Longitude = location.Longitude;
                            airplane.Flight = flight;
                            airplane.Flight.Progress = flight.Progress; // al.Flight.CalculateProgress(currentTime);
                            if (airplane.Flight.IsCompleted)
                            {
                                System.Diagnostics.Debug.WriteLine("Landed Airplane " + airplane.ToString());
                            }
                            break;
                        }
                    }
                }

                if (Airports.Locations.Count(x => x.Code == flight.Destination.Code) == 0)
                {
                    var newAirport = flight.Destination.Copy();
                    //newAirport.FlightsInCount++;
                    Airports.Locations.Add(newAirport);
                }

                Airports.Locations.Where(x => x.Code == flight.Destination.Code).First().FlightsInCount++;

            }
            #endregion
         
        }
        public void UpdateAirTraffic(DateTime currentTime)
        {
            if (this.Flights == null) return;
            if (this.Flights.List.Count == 0) return;

            this.Airports.Locations.Clear();
            this.Airplanes.Locations.Clear();

            var currentFlights = from f in this.Flights.List
                                 where f.DepartureTime.Ticks <= currentTime.Ticks && currentTime.Ticks <= f.ArrivalTime.Ticks
                                 select f;

            foreach (AirlineFlight flight in currentFlights)
            {
                var newLocation = flight.CalculateLocation(currentTime);
                var newAirplane = new Airplane
                {
                    Code = flight.FlightCode,
                    Flight = flight,
                    Latitude = newLocation.Latitude,
                    Longitude = newLocation.Longitude,
                };

                System.Diagnostics.Debug.WriteLine("Adding Airplane " + newAirplane.ToString());
                this.Airplanes.Dictionary.Add(newAirplane);
                this.Airplanes.Locations.Add(newAirplane);

            }

            #region Started Flights

            var startedFlights = from f in this.Flights.List
                                 where f.DepartureTime.Ticks <= currentTime.Ticks //&& currentTime.Ticks <= f.ArrivalTime.Ticks
                                 select f;

            foreach (AirlineFlight flight in startedFlights)
            {
                if (Airports.Locations.Count(x => x.Code == flight.Origin.Code) == 0)
                {
                    var newAirport = flight.Origin.Copy();
                    Airports.Locations.Add(newAirport);
                }
                Airports.Locations.Where(x => x.Code == flight.Origin.Code).First().FlightsOutCount++;

                if (Airports.Locations.Count(x => x.Code == flight.Destination.Code) == 0)
                {
                    var newAirport = flight.Destination.Copy();
                    Airports.Locations.Add(newAirport);
                }
            }
            #endregion
            #region Completed Flights

            var completedFlights = from f in this.Flights.List
                                   where f.ArrivalTime.Ticks <= currentTime.Ticks
                                   select f;

            foreach (AirlineFlight flight in completedFlights)
            {
                if (Airports.Locations.Count(x => x.Code == flight.Destination.Code) == 0)
                {
                    var newAirport = flight.Destination.Copy();
                    Airports.Locations.Add(newAirport);
                }
                Airports.Locations.Where(x => x.Code == flight.Destination.Code).First().FlightsInCount++;
            }
            #endregion
   
        }

        private void UpdateMotionFramework()
        {
            if (IsEnabledMotionFramework)
            {
                this.MotionSliderInterval = TimeSpan.FromMinutes(15);

                this.MotionTimeInterval = TimeSpan.FromMilliseconds(500);
                this.MotionTimer.Interval = this.MotionTimeInterval;
            }
            else
            {
                this.MotionSliderInterval = TimeSpan.FromMinutes(1);

                this.MotionTimeInterval = TimeSpan.FromMilliseconds(26);
                this.MotionTimer.Interval = this.MotionTimeInterval;
            }
        }
        #endregion
        #region Properties
        public AirlineTrackSlider MotionSlider { get; set; }

        protected DispatcherTimer MotionTimer = new DispatcherTimer();
        public TimeSpan MotionTimeInterval { get; set; }
        public TimeSpan MotionSliderInterval { get; set; }
       
        public bool IsUpdating { get { return this.MotionTimer.IsEnabled; } }

        public bool IsMotionSliderEnabled
        {
            get { return !this.IsUpdating; }
        }

        private bool _isEnabledMotionFramework;
        public bool IsEnabledMotionFramework
        {
            get { return _isEnabledMotionFramework; }
            set 
            {
                if (_isEnabledMotionFramework == value) return; _isEnabledMotionFramework = value;
                UpdateMotionFramework();
                OnPropertyChanged("IsEnabledMotionFramework");
            }
        }
     
        private AirportsViewModel _airports = new AirportsViewModel();
        public AirportsViewModel Airports
        {
            get { return _airports; }
            set { if (_airports == value) return; _airports = value; OnPropertyChanged("Airports"); }
        }
        
        private AirplanesViewModel _airplanes = new AirplanesViewModel();
        public AirplanesViewModel Airplanes
        {
            get { return _airplanes; }
            set { if (_airplanes == value) return; _airplanes = value; OnPropertyChanged("Airplanes"); }
        }

        private AirlineFlightsViewModel _flights = new AirlineFlightsViewModel();
        public AirlineFlightsViewModel Flights
        {
            get { return _flights; }
            set { if (_flights == value) return; _flights = value; OnPropertyChanged("Flights"); }
        }
   
        #endregion
    }
    public class AirlineTrackSlider : ObservableModel
    {
        private DateTime _minValue;
        public DateTime MinValue
        {
            get { return _minValue; }
            set { if (_minValue == value) return; _minValue = value; OnPropertyChanged("MinValue"); }
        }
        private DateTime _maxValue;
        public DateTime MaxValue
        {
            get { return _maxValue; }
            set { if (_maxValue == value) return; _maxValue = value; OnPropertyChanged("MaxValue"); }
        }
        private DateTime _value;
        public DateTime Value
        {
            get { return _value; }
            set { if (_value == value) return; _value = value; OnPropertyChanged("Value"); }
        }
    }
   

}