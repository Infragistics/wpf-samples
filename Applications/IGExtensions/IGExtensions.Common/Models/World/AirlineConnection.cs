using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    public class AirlineConnection : ObservableModel // GeoPath // 
    {
        //public GeoConnection()
        //    : this(new Airport(), new Airport())
        //{ }
        public AirlineConnection()
        {
            this.ConnectionPath = new GeoPath();
        }

        public AirlineConnection(Airport origin, Airport destination)
          //  : base(origin, destination)
        {
            //_origin = origin;
            //_destination = destination;
            ConnectionCode = origin.Code + "-" + destination.Code;
            this.ConnectionPath = new GeoPath(origin, destination);
        }
        public GeoPath ConnectionPath { get; set; }
    
        public string ConnectionCode { get; set; }
        

    }
    public class AirlineFlightManager
    {
       
    }
    public class AirplanePath //: List<AirlineFlightLocation>
    {
        //public AirplanePath()
        //{
        //    Dictionary = new Dictionary<DateTime, AirlineFlightLocation>();
        //}
        public AirplanePath(AirlineFlight flight)
        {
            Dictionary = new Dictionary<DateTime, AirlineFlightLocation>();

            this.Flight = flight;
            //this.FlightPath = new GeoPath(flight.Origin, flight.Destination);
            //var pathDistance = GeoCalculator.GetDistance(flight.Origin, flight.Destination);
            //var bearingInitial = GeoCalculator.GetBearing(flight.Origin, flight.Destination);
            //var bearingFinal = GeoCalculator.GetBearingFinal(flight.Origin, flight.Destination);

            var location1 = GetLocation(this.Flight.DepartureTime);
            var location2 = GetLocation(this.Flight.ArrivalTime); 
        }
        public List<AirlineFlightLocation> GetPathLocations()
        {
            var locations = new List<AirlineFlightLocation>();
            foreach (var pair in this.Dictionary)
            {
                locations.Add(pair.Value);
            }
            return locations;
        }
        //public static TimeSpan Interval { get; set; }
        public AirlineFlightLocation GetLocation(DateTime currentTime)
        {
            if (!this.Dictionary.ContainsKey(currentTime))
            {
                var location = ComputeLocation(currentTime);
                this.Dictionary.Add(currentTime, location);
                //if (location.IsValid())
                //{
                //    this.Dictionary.Add(currentTime, location);
                //}
                //else
                //{
                //    return location;
                //}
            }
            return this.Dictionary[currentTime];
        }
        public AirlineFlight Flight { get; private set; }
        public List<AirlineFlightLocation> Locations { get { return GetPathLocations(); } }
        protected Dictionary<DateTime, AirlineFlightLocation> Dictionary { get; set; }

        public AirlineFlightLocation ComputeLocation(DateTime currentTime)
        {
            var airplane = new AirlineFlightLocation();
            airplane.Time = currentTime;
            if (currentTime.Ticks >= Flight.DepartureTime.Ticks || 
                currentTime.Ticks <= Flight.ArrivalTime.Ticks)
            {
                var flightDuration = this.Flight.ComputeDuration(currentTime);
                var flightProgress = this.Flight.ComputeProgress(currentTime);

                var flightDistance = this.Flight.FlightPath.Distance * flightProgress;
                var flightLocation = GeoCalculator.GetPathLocation(this.Flight.Origin, this.Flight.Destination, flightDistance);

                airplane.Distance = flightDistance;
                airplane.Duration = flightDuration;
                airplane.Progress = flightProgress;
                airplane.Bearing = flightProgress >= 0.905
                                       ? this.Flight.FlightPath.BearingFinal
                                       : GeoCalculator.GetBearing(flightLocation, this.Flight.Destination);
            }
            return airplane;
        }
    }
    public class AirlineFlightLocation : GeoPathLocation
    {
        public AirlineFlightLocation()
        {
            Time = DateTime.MinValue;
            Duration = TimeSpan.FromMinutes(0);
            //FlightProgress = 0;
        }
        //public double FlightProgress { get { return this.Progress;  } }
        public DateTime Time { get; set; }
        public TimeSpan Duration { get; set; }
       // public double FightBearing { get { return this.Bearing; } }

        //double FlightDistance
    }
   
    public class AirlineFlight : ObservableModel //AirlineConnection
    {

        public AirlineFlight()
        {

        }
        public AirlineFlight(Airport origin, Airport destination)
        {
            _progress = 0.0;
            //_flightLocation = origin;
            _flightLatitude = origin.Latitude;
            _flightLongitude = origin.Longitude; 
            _flightOrigin = origin;
            _flightDestination = destination;
            //this.ConnectionCode = origin.Code + "-" + destination.Code;
            this.FlightPath = new GeoPath(origin, destination);
            this.FlightBearing = this.FlightPath.BearingInitial;
           
            InitializeLocations();
        }

        public void InitializeLocations()
        {
            this.LocationsDictionary = new Dictionary<DateTime, AirlineFlightLocation>();
            AddLocation(this.ArrivalTime);
            AddLocation(this.DepartureTime);
        }
        //public List<AirlineFlightLocation> Locations { get { return GetPathLocations(); } }
        //public AirlineFlightLocation FlightLocation { get; private set; }
      
        protected Dictionary<DateTime, AirlineFlightLocation> LocationsDictionary { get; set; }

        public List<AirlineFlightLocation> GetPathLocations()
        {
            var locations = new List<AirlineFlightLocation>();
            foreach (var pair in this.LocationsDictionary)
            {
                locations.Add(pair.Value);
            }
            return locations;
        }

        public void AddLocation(DateTime currentTime)
        {
            if (!this.LocationsDictionary.ContainsKey(currentTime))
            {
                var location = ComputeLocation(currentTime);
                if (location.IsValid())
                {
                    this.LocationsDictionary.Add(currentTime, location);
                }
            }
        }

        public AirlineFlight Flight { get { return this; } }

        protected static int CachedCounter = 0;
        public void Update(DateTime currentTime)
        {
            //if (currentTime.Ticks < this.DepartureTime.Ticks)
            //    this.Visibility = Visibility.Visible;
            //    currentTime = this.DepartureTime;

            if (currentTime.Ticks > this.ArrivalTime.Ticks)
            {
                currentTime = this.ArrivalTime;
            }
             
            AirlineFlightLocation location;
            if (this.LocationsDictionary.ContainsKey(currentTime))
            {
                location = this.LocationsDictionary[currentTime];
                CachedCounter++;
                //System.Diagnostics.Debug.WriteLine("AirlineFlight cached location for " + currentTime + " " + CachedCounter);
            }
            else
            {
                location = ComputeLocation(currentTime);
                //System.Diagnostics.Debug.WriteLine("AirlineFlight computing location for " + currentTime + " ...");
                if (location.IsValid())
                {
                    this.LocationsDictionary.Add(currentTime, location);
                }
            }
            if (this.Progress == 1.0)
                this.Visibility = Visibility.Collapsed;
            else
                this.Visibility = Visibility.Visible;
         
            this.Latitude = location.Latitude;
            this.Longitude = location.Longitude;
            this.FlightBearing = location.Bearing;
            this.FlightDistance = location.Distance;
            this.Duration = location.Duration;
            this.Progress = location.Progress;
            //return this.LocationsDictionary[currentTime];
        }
        public AirlineFlightLocation ComputeLocation(DateTime currentTime)
        {
            var airplane = new AirlineFlightLocation();
            airplane.Time = currentTime;
            if (currentTime.Ticks < this.DepartureTime.Ticks) // check if the flight started
            {
                //airplane.Bearing = this.FlightPath.BearingInitial;
                //airplane.Latitude = this.Origin.Latitude;
                //airplane.Longitude = this.Origin.Longitude;
                //airplane.Duration = TimeSpan.FromMinutes(0);
                //airplane.Distance = 0.0;
                //airplane.Progress = 0.0;
            }
            else if (currentTime.Ticks > this.ArrivalTime.Ticks) // check if flight ended
            {
                airplane.Bearing = this.FlightPath.BearingFinal;
                airplane.Latitude = this.Destination.Latitude;
                airplane.Longitude = this.Destination.Longitude;
                airplane.Duration = this.DurationTotal;
                airplane.Distance = this.FlightPath.Distance;
                airplane.Progress = 1.0;
            }
            else if (currentTime.Ticks >= this.DepartureTime.Ticks &&
                     currentTime.Ticks <= this.ArrivalTime.Ticks)
            {
                var flightDuration = this.ComputeDuration(currentTime);
                var flightProgress = this.ComputeProgress(currentTime);

                var flightDistance = this.FlightPath.Distance * flightProgress;
                var flightLocation = GeoCalculator.GetPathLocation(this.Origin, this.Destination, flightDistance);

                airplane.Latitude = flightLocation.Latitude;
                airplane.Longitude = flightLocation.Longitude;
                airplane.Distance = flightDistance;
                airplane.Duration = flightDuration;
                airplane.Progress = flightProgress;
                airplane.Bearing = flightProgress >= 0.905
                                       ? this.FlightPath.BearingFinal
                                       : GeoCalculator.GetBearing(flightLocation, this.Destination);
            }
            return airplane;
        }
  
        #region Info Properties
        public string ConnectionCode { get { return Origin.Code + "-" + Destination.Code; } }
        public string GetFlightCode()
        {
            return this.CarrierCode + "-" + this.FlightNumber; // +":" + this.ConnectionCode;
        }
        public double GetFlightProgress(DateTime currentTime)
        {
            double progress = (currentTime.TimeOfDay - this.DepartureTime.TimeOfDay).TotalSeconds /
                              (this.ArrivalTime.TimeOfDay - this.DepartureTime.TimeOfDay).TotalSeconds;
            if (progress < 0.0) progress = 0.0;
            if (progress > 1.0) progress = 1.0;
            return progress;
        }
        public string GetFlightName()
        {
            return this.CarrierCode + "-" + this.FlightNumber + " " + this.ConnectionCode;
        }

        public int FlightNumber { get; set; }
        public string FlightCode { get { return GetFlightCode(); } }
        public string FlightName { get { return GetFlightName(); } }

        public string CarrierName { get; set; }
        public string CarrierCode { get; set; }

        public new string ToString()
        {
            return this.Origin.Code + " " + Origin.ConnectionsTotal + " " + Destination.Code + " " + Destination.ConnectionsTotal; 
        }

        #endregion 
        #region Time Properties
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        private TimeSpan _duration = TimeSpan.FromSeconds(0);
        public TimeSpan Duration
        {
            get { return _duration; }
            private set { if (_duration == value) return; _duration = value; OnPropertyChanged("Duration"); }
        }
        public TimeSpan DurationTotal
        {
            get { return this.ArrivalTime.Subtract(this.DepartureTime); }
        }
        public TimeSpan ComputeDuration(DateTime currentTime)
        {
            var duration = TimeSpan.FromSeconds(0);
            if (currentTime.Ticks >= this.DepartureTime.Ticks ||
               currentTime.Ticks <= this.ArrivalTime.Ticks)
            {
                duration = currentTime.Subtract(this.DepartureTime);
            }
            return duration;
        }
        #endregion
        #region Validation Properties
        public bool IsVisualized { get; set; }
        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { if (_isSelected == value) return; _isSelected = value; OnPropertyChanged("IsSelected"); }
        }
        public bool IsCompleted
        { 
            get { return this.Progress < 1.0 ? false : true; }
        }
        public bool IsStarted
        {
            get { return this.Progress >= 0.0 ? true : false; }
        }
        public bool IsInProgress
        {
            get { return this.IsStarted && !this.IsCompleted; }
        }
        public bool IsPending
        {
            get { return this.Progress == 0.0 ? true : false; }
        }
        #endregion

        #region Progress
        private Visibility _visibility = Visibility.Visible;
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                if (_visibility == value) return; _visibility = value; OnPropertyChanged("Visibility");
            }
        }
        private double _progress = 0.0;
        public double Progress
        {
            get { return _progress; }
            set
            {
                if (_progress == value) return; _progress = value;
                OnPropertyChanged("IsCompleted"); OnPropertyChanged("Progress"); 
            }
        }
        public double ComputeProgress(DateTime currentTime)
        {
            var progress = 0.0;
            if (currentTime.Ticks >= this.DepartureTime.Ticks ||
                currentTime.Ticks <= this.ArrivalTime.Ticks)
            {
                var flightDuration = currentTime.Subtract(this.DepartureTime);
                progress = flightDuration.Ticks / (this.DurationTotal.Ticks * 1.0);
            }
            return progress;
        }
        public double ComputeProgress(IGeoLocatable currentLocation)
        {
            var distanceTraveled = GeoCalculator.GetDistance(this.Origin, currentLocation);
            return ComputeProgress(distanceTraveled.Kilometers);
        }
        public double ComputeProgress(double distanceTraveled)
        {
            var progress = 0.0;
            if (this.FlightPath.Distance != 0)
            {
                progress = distanceTraveled / this.FlightPath.Distance;
            }
            return progress;
        }
        
        private Airport _flightDestination = new Airport();
        /// <summary>
        /// Gets or sets Destination location of the geographic path
        /// </summary>
        public Airport Destination
        {
            get { return _flightDestination; }
            set { if (_flightDestination == value) return; _flightDestination = value; ComputePath(); OnPropertyChanged("FlightDestination"); }
        }
        private Airport _flightOrigin = new Airport();
        /// <summary>
        /// Gets or sets Origin location of the geographic path
        /// </summary>
        public Airport Origin
        {
            get { return _flightOrigin; }
            set { if (_flightOrigin == value) return; _flightOrigin = value; ComputePath(); OnPropertyChanged("FlightOrigin"); }
        } 

        private GeoPath _flightPath = new GeoPath();
        /// <summary>
        /// Gets Flight's Path property 
        /// </summary>
        public GeoPath FlightPath
        {
            get { return _flightPath; }
            private set { if (_flightPath == value) return; _flightPath = value; OnPropertyChanged("FlightPath"); }
        }
        private GeoLocation _flightLocation = new GeoLocation();
        /// <summary>
        /// Gets FlightLocation property 
        /// </summary>
        public GeoLocation FlightLocation
        {
            get { return _flightLocation; }
            private set { if (_flightLocation == value) return; _flightLocation = value; OnPropertyChanged("FlightLocation"); }
        }
        private double _flightLatitude;
        /// <summary>
        /// Gets Flight's Latitude property   
        /// </summary>
        public double Latitude
        {
            get { return _flightLatitude; }
            set { if (_flightLatitude == value) return; _flightLatitude = value; OnPropertyChanged("Latitude"); }
        }

        private double _flightLongitude;
        /// <summary>
        /// Gets Flight's Longitude property  
        /// </summary>
        public double Longitude
        {
            get { return _flightLongitude; }
            set { if (_flightLongitude == value) return; _flightLongitude = value; OnPropertyChanged("Longitude"); }
        }

        private double _flightBearing;
        /// <summary>
        /// Gets Flight's Bearing from true North
        /// </summary>
        public double FlightBearing
        {
            get { return _flightBearing; }
            private set { if (_flightBearing == value) return; _flightBearing = value; OnPropertyChanged("FlightBearing"); OnPropertyChanged("FlightAngle"); }
        }
        /// <summary>
        /// Gets Flight's Angle property offset by 270 degrees
        /// </summary>
        public double FlightAngle { get { return (this.FlightBearing + 270) % 360; } }

        private double _flightDistance;
        /// <summary>
        /// Gets Flight's Distance property 
        /// </summary>
        public double FlightDistance
        {
            get { return _flightDistance; }
            private set { if (_flightDistance == value) return; _flightDistance = value; OnPropertyChanged("FlightDistance"); }
        }
         
        //public double FlightBearing
        //{
        //    get
        //    {
        //        if (!IsCompleted)
        //        {
        //            _flightBearing = GeoCalculator.GetBearing(this.FlightLocation, this.Destination);
        //            //_flightBearing = GeoCalculator.GetBearingFinal(this.FlightLocation, this.Destination);
        //        }
        //        return _flightBearing;
        //    }
        //}
        #endregion
        private void ComputePath()
        {
            this.FlightPath = new GeoPath(this.Origin, this.Destination);
        }
        public void Update(double flightProgress)
        {
            if (flightProgress == this.Progress) return;

            double flightBearing = this.FlightPath.BearingInitial;
            double flightDistance = 0.0;
            TimeSpan flightDuration = TimeSpan.FromMinutes(0);
            GeoPoint flightLocation;
        
            // skip calculation of flight's parameters for known progress
            if (flightProgress <= 0.0)
            {
                flightProgress = 0.0;
                //flightDistance = 0.0;
                //flightBearing = this.FlightPath.BearingInitial;
                flightLocation = new GeoPoint(this.Origin.ToPoint());
            }
            else if (flightProgress >= 1.0)
            {
                flightProgress = 1.0;
                flightDuration = this.DurationTotal;
                flightDistance = this.FlightPath.Distance;
                flightBearing = this.FlightPath.BearingFinal;
                flightLocation = new GeoPoint(this.Destination.ToPoint());
            }
            else
            {
                // re-calculate flight's parameters for new progress
                flightDuration = TimeSpan.FromMinutes(this.DurationTotal.TotalMinutes * flightProgress);
                flightDistance = this.FlightPath.Distance * flightProgress;
                flightLocation = GeoCalculator.GetPathLocation(this.Origin, this.Destination, flightDistance);
                //flightLocation = GeoCalculator.GetPathDestination(Origin, this.BearingInitial, travelDistance);
                //flightBearing = GeoCalculator.GetBearing(flightLocation, Destination);

                if (flightProgress >= 0.905)
                {
                    flightBearing = this.FlightPath.BearingFinal;
                }
                else
                {
                    flightBearing = GeoCalculator.GetBearing(flightLocation, this.Destination);
                }
            }
            
            this.Progress = flightProgress;
            this.Duration = flightDuration;
            this.FlightDistance = flightDistance;
            this.FlightBearing = flightBearing;
            //this.FlightLocation = flightLocation.ToGeoLocation();
            //FlightPath = new GeoPath(this.Origin, flightLocation);

            //OnPropertyChanged("FlightBearing"); OnPropertyChanged("FlightLocation");
        }
       

        public void Update(Airplane airplane)
        {
            _flightPath = new GeoPath(this.Origin, airplane.Location);

            //var togoDistance = GeoCalculator.GetGreatCircleDistance(airplane.Location, this.Destination);
            //var distanceTravel = GeoCalculator.GetDistance(this.Origin, airplane.Location);
            //var distanceRemaning = this.Distance - distanceTravel.Kilometers;
          
            var progress = 0.0;

            if (this.FlightPath.Distance != 0.0)
                progress = _flightPath.Distance / this.FlightPath.Distance;

            //this.DistanceTraveled = travelDistance;
            //this.DistanceRemaining = togoDistance;
            this.Progress = progress;
            //FlightLocation = airplane.Location;

            OnPropertyChanged("TravelPath"); 
        }
        
        
        //public override string ToString()
        //{
        //    return this.Origin.Country + "." + Origin.Name + " -> " + this.Destination.Country + "." + Destination.Name;
        //}
    }
    public class AirlineFlightsViewModel : ObservableModel
    {
        public AirlineFlightsViewModel()
        {
            this.Dictionary = new AirlineFlightDictionary();
            //this.List = new AirlineFlightList();

        }
        public AirlineFlightDictionary Dictionary { get; private set; }
        //public AirlineFlightList List { get; set; }

        private AirlineFlightList _list = new AirlineFlightList();
        public AirlineFlightList List
        {
            //get { return this.Dictionary.Values.ToList(); }
            get { return _list; }
            private set { if (_list == value) return; _list = value; OnPropertyChanged("List"); }
        }
        public void Add(AirlineFlight flight)
        {
            this.Dictionary.Add(flight);
        }
        public void Compute()
        {
            this.List.Clear();
            this.List.AddRange(this.Dictionary.Values.ToList());
        }
        public void Add(AirlineFlightList flights)
        {
            this.Dictionary.Clear();
            this.Dictionary.Add(flights);
            this.List.Clear();
            this.List.AddRange(this.Dictionary.Values.ToList());

            //OnPropertyChanged("List");
        }
    }
    public class AirlineFlightList : List<AirlineFlight>
    {
        public AirlineFlightList()
        {
            
        }

        public AirlineFlightList(IEnumerable<AirlineFlight> flights)
        {
            this.AddRange(flights);
        }
    }
    public class AirlineFlightDictionary : Dictionary<string, AirlineFlight>
    {
        public void Add(AirlineFlightList flights)
        {
            foreach (var flight in flights)
            {
                this.Add(flight);
            }
        }
        public void Add(AirlineFlight flight)
        {
            if (flight == null) return;
            var key = flight.FlightCode;
            if (key.Length == 0)
            {
                key = flight.GetFlightCode();
            }
            if (this.ContainsKey(key))
            {
                // add v to existing item in the dictionary
                this[key] = flight;
            }
            else
            {
                // add airport as new item to in the dictionary
                this.Add(key, flight);
            }
        }
    }
}