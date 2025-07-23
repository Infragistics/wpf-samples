using System;
using System.Collections.Generic;
using System.Linq;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.Navigation;

namespace IGGeographicMap.Models
{
    public class AirlineFlight : AirlineConnection  
    {
        public AirlineFlight()
        {
            this.ArrivalTime = DateTime.Now;
            this.DepartureTime = DateTime.Now;
        }
        public string FlightCode { get { return GetFlightCode(); } }
        public string FlightName { get { return GetFlightName(); } }
        public int FlightNumber { get; set; }

        public bool Visualized { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string CarrierName { get; set; }
        public string CarrierCode { get; set; }
          
        private double _progress = 0.0;
        public double Progress
        {
            get { return _progress; }
            set 
            { 
                //if (_progress == value) return;
                _progress = value; 
                OnPropertyChanged("Progress"); OnPropertyChanged("IsCompleted");
            }
        }

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

        private double _distanceRemaining;
        public double DistanceRemaining
        {
            get { return _distanceRemaining; }
            private set { if (_distanceRemaining == value) return; _distanceRemaining = value; OnPropertyChanged("DistanceRemaining"); }
        }

        private double _distanceTraveled;
        public double DistanceTraveled
        {
            get { return _distanceTraveled; }
            private set { if (_distanceTraveled == value) return; _distanceTraveled = value; OnPropertyChanged("DistanceTraveled"); }
        }

        public bool IsCompleted
        {
            get { return this.Progress < 1.0 ? false : true; }
        }
        public bool IsStarted
        {
            get { return this.Progress > 0.0 ? true : false; }
        }
        public bool IsInProgress
        {
            get { return this.IsStarted && !this.IsCompleted; }
        }
        public bool IsValidFlight
        {
            get { return this.IsValidConnection; }
        }

        protected static Random Genrator = new Random();

        public DateTime GetDepartureTime()
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;
            var day = DateTime.Now.Day;
            var hour = AirlineFlight.Genrator.Next(0, 24);
            var minutes = AirlineFlight.Genrator.Next(0, 60);
            return new DateTime(year, month, day, hour, minutes, 0);
        }
        public DateTime GetArrivalTime(Airplane airplane)
        {
            var duration = CalculateDuration(airplane, this.Origin, this.Destination);
            var arrivalTime = this.DepartureTime.Add(duration);
            return arrivalTime;
        }

        public string GetFlightCode()
        {
            return this.CarrierCode + "-" + this.FlightNumber + ":" + this.ConnectionCode;
        }
        public string GetFlightName()
        {
            return this.CarrierCode + "-" + this.FlightNumber + " " + this.ConnectionCode;
        }

        public void UpdateFlightStats(Airplane airplane)
        {
            CalculateProgress(airplane);
            CalculateDuration(airplane);
        }
        public void UpdateFlightStats(DateTime currentTime)
        {
            CalculateProgress(currentTime);
            CalculateDuration(currentTime);
        }
        #region Calculations
        public double CalculateProgress(Airplane airplane)
        {
            var togoDistance = GeoCalculator.GetGreatCircleDistance(airplane.Location, this.Destination);
            var travelDistance = GeoCalculator.GetGreatCircleDistance(this.Origin, airplane.Location);
            var progress = 0.0;

            if (this.DistanceTotal != 0.0)
                progress = travelDistance / this.DistanceTotal;

            this.DistanceTraveled = travelDistance;
            this.DistanceRemaining = togoDistance;
            this.Progress = progress;
            return this.Progress;
        }
        public double CalculateProgress(DateTime currentTime)
        {
            double delta = (this.ArrivalTime.Ticks - this.DepartureTime.Ticks) * 1.0;
            double progress = (currentTime.Ticks - this.DepartureTime.Ticks) / delta;
            if (progress > 1) progress = 1;
            if (progress < 0) progress = 0;

            if (this.DistanceTotal != 0.0)
            {
                this.DistanceTraveled = this.DistanceTotal * progress;
                this.DistanceRemaining = this.DistanceTotal * (1 - progress);
            }

            this.Progress = progress;
            return this.Progress;
        }
        public GeoLocation CalculateLocation()
        {
            return CalculateLocation(this.Progress);
        }
        public GeoLocation CalculateLocation(DateTime currentTime)
        {
            var progress = CalculateProgress(currentTime);
            return CalculateLocation(progress);
        }
        public GeoLocation CalculateLocation(double progress)
        {
            var latitude = this.Origin.Latitude + ((this.Destination.Latitude - this.Origin.Latitude) * progress);
            var longitude = this.Origin.Longitude + ((this.Destination.Longitude - this.Origin.Longitude) * progress);
            return new GeoLocation(longitude, latitude);
        }

        public TimeSpan CalculateDuration(DateTime currentTime)
        {
            var delta = (currentTime.Ticks - this.ArrivalTime.Ticks);
            if (delta < 0) delta = 0;
            this.Duration = TimeSpan.FromTicks(delta);
            return this.Duration;
        }
        public TimeSpan CalculateDuration(Airplane airplane)
        {
            this.Duration = CalculateDuration(airplane, this.Origin, this.Destination);
            return this.Duration;
        }
        public static TimeSpan CalculateDuration(Airplane airplane, GeoLocation origin, GeoLocation destination)
        {
            var travelDistance = GeoCalculator.GetGreatCircleDistance(origin, destination);
            var durationSec = (travelDistance / airplane.Speed) * 3600;
            return TimeSpan.FromSeconds((int)durationSec);
        } 
        #endregion

        public new string ToString()
        {
            var str = this.FlightCode + " " + this.DepartureTime.TimeOfDay + " " + this.Progress;
            return str;
        }
    }
    public class AirlineFlightsViewModel : ObservableModel
    {
        public AirlineFlightsViewModel()
        {
            this.Dictionary = new AirlineFlightDictionary();
            this.List = new AirlineFlightList();

        }
        public AirlineFlightDictionary Dictionary { get; private set; }
        //public AirlineFlightList List { get; set; }

        private AirlineFlightList _list = new AirlineFlightList();
        public AirlineFlightList List
        {
            get { return _list; }
            private set { if (_list == value) return; _list = value; OnPropertyChanged("List"); }
        }

        public void Add(AirlineFlightList flights)
        {
            this.Dictionary.Add(flights);
            this.List.Clear();
            this.List.AddRange(this.Dictionary.Values.ToList());

            //OnPropertyChanged("List");
        }
    }
    public class AirlineFlightList : List<AirlineFlight>
    {

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