 
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    public class Airplane : GeoLocation //GeoPoint // GeoLocation
    {
        public Airplane()
            : this(new AirlineFlight())
        { }
        public Airplane(AirlineFlight flight)
        {
            _flight = flight; 
            this.Speed = GeoGlobal.AirplaneAverageSpeed;
            this.Range = GeoGlobal.AirplaneAverageRange;
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
                if (_flight != null) _flight.PropertyChanged -= OnFlightChanged;
                if (_flight == value) return;
                _flight = value;
                _flight.PropertyChanged += OnFlightChanged;
                //_flight.UpdateFlightStats(this);

                OnPropertyChanged("Flight");
            }
        }

        void OnFlightChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
           // OnPropertyChanged("Flight");
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

        //private double? _bearing;
        private double _bearing = 0;
        public double Bearing
        {
            get
            {
                //if (!_bearing.HasValue)
                //{
                //    _bearing = GeoCalculator.GetBearing(this.Location, this.Flight.Destination);
                //}
                return _bearing;
            }
        }
        #region Methods

        public void StartFlight()
        {
            this.Flight.Progress = 0;
            //this.Location = new GeoLocation(this.Flight.Origin.ToPoint());

        }
        public void CompleteFlight()
        {
            this.Flight.Progress = 1;
            //this.Location = new GeoLocation(this.Flight.Destination.ToPoint());
        }
        #endregion

        protected new void OnPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case "Latitude":
                    {
                        //this.Flight.CalculateProgress(this);
                        break;
                    }
                case "Longitude":
                    {
                        //this.Flight.CalculateProgress(this);
                        break;
                    }
            }
            base.OnPropertyChanged(propertyName);
        }

        public Airplane Copy()
        {
            var ap = new Airplane();
            ap.Code = this.Code;
            //ap.Location = new GeoLocation(this.Longitude, this.Latitude);
            ap.Flight = this.Flight;
            ap.Speed = this.Speed;
            ap.Range = this.Range;
            return ap;
        }
        public new string ToString()
        {
            var str = this.Flight.ToString() + " @ " + this.Longitude + " " + this.Latitude;
            return str;
        }
    }
    public class AirplaneCollection : Infragistics.Collections.CollectionBase<AirplanePosition>
    {
        public void RefreshItems()
        {
            this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
    public class AirplanePosition //: GeoPoint
    {
        public AirplanePosition(AirlineFlight flight)
        {
            _flight = flight;
            //Latitude = _flight.FlightLocation.Latitude;
            //Longitude = _flight.FlightLocation.Longitude;
        }
        public double Latitude { get { return _flight.Latitude; } }
        public double Longitude { get { return _flight.Longitude; } }
        public AirlineFlight Flight { get { return _flight; } }
        private readonly AirlineFlight _flight;// = new AirlineFlight();
        //public  GeoPoint Location { get; set; }
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
        private AirplaneList _list = new AirplaneList();
        public AirplaneList List
        {
            get { return _list; }
            set { if (_list == value) return; _list = value; OnPropertyChanged("List"); }
        }

        //public AirplaneList List { get; set; }

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

    //public class Airplane : GeoLocation
    //{
    //    public Airplane()
    //    {
    //        _flight = new Flight();
    //        _status = AirplaneStatus.Departing;

    //    }
    //    #region Properties
    //    private bool _isSelected;
    //    public bool IsSelected
    //    {
    //        get { return _isSelected; }
    //        set { if (_isSelected == value) return; _isSelected = value; OnPropertyChanged("IsSelected"); }
    //    }

    //    private Flight _flight;
    //    public Flight Flight
    //    {
    //        get { return _flight; }
    //        set { if (_flight == value) return;  _flight = value; OnPropertyChanged("Flight"); }
    //    }

    //    private AirplaneStatus _status;
    //    public AirplaneStatus Status
    //    {
    //        get { return _status; }
    //        set { if (_status == value) return; _status = value; OnPropertyChanged("Status"); }
    //    }

    //    private double? _angle;
    //    public double Angle
    //    {
    //        get
    //        {
    //            if (!_angle.HasValue)
    //            {
    //                _angle = GetAngle(this.Flight.Origin, this.Flight.Destination);
    //            }
    //            return -1 * _angle.Value;
    //        }
    //    }
    
    //    public static double GetAngle(double x1, double y1, double x2, double y2)
    //    {
    //        return System.Math.Atan2((y2 - y1), (x2 - x1)) * 180.0 / System.Math.PI;
    //    }
    //    public static double GetAngle(GeoLocation origin, GeoLocation destination)
    //    {
    //        return GetAngle(origin.Longitude, origin.Latitude, destination.Longitude, destination.Latitude);
    //    }
    //    #endregion

    //}
    public enum AirplaneStatus
    {
        Landing,
        Flying,
        Departing,
        Boarding,
    }
}