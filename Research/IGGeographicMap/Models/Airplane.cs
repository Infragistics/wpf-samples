using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.Navigation;

namespace IGGeographicMap.Models
{
    public class Airplane : GeoLocation 
    {
        public Airplane()
        {
            this.Speed = GeoStats.AirplaneAverageSpeed;
            this.Range = GeoStats.AirplaneAverageRange;
        }
       
        public string Code { get; set; }
        public double Speed { get; set; }
        public double Range { get; set; }

        public new string Name { get { return this.Flight.FlightName; } }
        public bool IsFlying { get { return !this.Flight.IsCompleted; } }

        private AirlineFlight _flight = new AirlineFlight();
        public AirlineFlight Flight
        {
            get { return _flight; }
            set
            {
                if (_flight == value) return; 
                _flight = value;
                //_flight.UpdateFlightStats(this);
 
                OnPropertyChanged("Flight");
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return; _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        private GeoLocation _location = new GeoLocation();
        public GeoLocation Location
        {
            get { return _location; }
            set 
            { 
                if (_location == value) return; _location = value;
                this.Latitude = _location.Latitude;
                this.Longitude = _location.Longitude;
                OnPropertyChanged("Location");
            }
        }
        
        private double? _bearing;
        public double Bearing
        {
            get
            {
                if (!_bearing.HasValue)
                {
                    _bearing = GeoCalculator.GetBearingAngle(this.Flight.Origin, this.Flight.Destination);
                }
                return _bearing.Value;
            }
        }
        #region Methods
      
        public void StartFlight()
        {
            this.Flight.Progress = 0;
            this.Location = this.Flight.Origin;

        }
        public void CompleteFlight()
        {
            this.Flight.Progress = 1;
            this.Location = this.Flight.Destination;

        } 
        #endregion

        protected new void OnPropertyChanged(string propertyName)
        {
            //switch (propertyName)
            //{
                //case "Latitude":
                //    {
                //        this.Flight.CalculateProgress(this);
                //        break;
                //    }
                //case "Longitude":
                //    {
                //        this.Flight.CalculateProgress(this);
                //        break;
                //    }
            //}
            base.OnPropertyChanged(propertyName);
        }
 
        public Airplane Copy()
        {
            var ap = new Airplane();
            ap.Code = this.Code;
            ap.Location = new GeoLocation(this.Longitude, this.Latitude);
            ap.Flight = this.Flight;
            ap.Speed = this.Speed;
            ap.Range = this.Range;
            return ap;
        }
        public new string ToString()
        {
            var str = this.Flight.ToString() + " @ " +  this.Longitude + " " + this.Latitude;
            return str;           
        }
    }


    public class AirplanesViewModel : ObservableModel
    {
        public AirplanesViewModel()
        {
            this.Dictionary = new AirplaneDictionary();
            this.List = new AirplaneList();
        }
        public AirplaneDictionary Dictionary { get; set; }

        private ObservableCollection<Airplane> _airplaneLocations = new ObservableCollection<Airplane>();
        public ObservableCollection<Airplane> Locations
        {
            get { return _airplaneLocations; }
            set { if (_airplaneLocations == value) return; _airplaneLocations = value; OnPropertyChanged("Locations"); }
        }
        //private AirplaneList _list = new AirplaneList();
        //public AirplaneList List
        //{
        //    get { return _list; }
        //    set { if (_list == value) return; _list = value; OnPropertyChanged("List"); }
        //}

        public AirplaneList List { get; set; }

        public void Add(AirplaneList airplanes)
        {
            //this.Dictionary.Add(airplanes);
            //var airplanes = new List<Airplane>(airplanes);

            this.List = new AirplaneList();
            foreach (var airplane in airplanes)
            {
                this.List.Add(airplane.Copy());
            }
            //this.List.AddRange(this.Dictionary.Values.ToList());
           // this.List.AddRange(list);
            //this.List.FromList(this.Dictionary.Values.ToList());
        }

    }
    public class AirplaneList : List<Airplane>
    {
        //public void FromList(List<Airplane> airplanes)
        //{
        //    this.Clear();
        //    this.AddRange(airplanes);
        //}
        public AirplaneList Copy()
        {
            var list = new AirplaneList();
            foreach (var airplane in this)
            {
                list.Add(airplane.Copy());
            }
            return list;
        }
    }
    public class AirplaneDictionary : Dictionary<string, Airplane>
    {
        public void Add(AirplaneList airplanes)
        {
            foreach (var airplane in airplanes)
            {
                this.Add(airplane);
            }
        }
        public void Add(Airplane airplane)
        {
            if (airplane == null) return;
            var key = airplane.Code;
            if (this.ContainsKey(key))
            {
                // add v to existing item in the dictionary
                this[key] = airplane;
            }
            else
            {
                // add airport as new item to in the dictionary
                this.Add(key, airplane);
            }
        }
        public void ToItemsList()
        {
            var list = new AirplaneList();


        }
    }

}