using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    public class Airport : GeoLocation, ISelectable
    {
        public Airport()
        {
            FlightPoints = new List<List<Point>>();
        }
        public Airport(Point geoPoint)
            : base(geoPoint.X, geoPoint.Y)
        {
            FlightPoints = new List<List<Point>>();
        }
        #region Properties
        public double Altitude { get; set; }
        public double Passangers { get; set; }
        public string Code 
        { 
            get
            {
                var code = CodeIATA != string.Empty ? CodeIATA : CodeICAO;
                return code;
            } 
        }

        public string CodeICAO { get; set; }
        public string CodeIATA { get; set; }
   
        public string ID { get; set; }
        public string Timezone { get; set; }
        public string TimeDST { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        private List<Airport> _connections = new List<Airport>();
        /// <summary>
        /// Gets or sets all Connections between this and other Airport
        /// </summary>
        public List<Airport> Connections
        {
            get { return _connections; }
            set { if (_connections == value) return; _connections = value; OnPropertyChanged("Connections"); }
        }
        //public int ConnectionsTotal { get { return this.Connections.Count; } }

        // public int ConnectionsTotal { get; set; }
        private int _connectionsTotal = 0;
        public int ConnectionsTotal
        {
            get { return _connectionsTotal; }
            set
            {
                if (_connectionsTotal == value) return; _connectionsTotal = value;
                OnPropertyChanged("ConnectionsTotal");
            }
        }

        public List<List<Point>> FlightPoints { get; set; }


        public int FlightsTotal
        {
            get { return _flightsInCount + _flightsOutCount; }
        }
        private int _flightsOutCount = 0;
        public int FlightsOutCount
        {
            get { return _flightsOutCount; }
            set
            {
                if (_flightsOutCount == value) return; _flightsOutCount = value;
                OnPropertyChanged("FlightsOutCount"); OnPropertyChanged("FlightsTotal");
            }
        }
        private int _flightsInCount = 0;
        public int FlightsInCount
        {
            get { return _flightsInCount; }
            set
            {
                if (_flightsInCount == value) return; _flightsInCount = value;
                OnPropertyChanged("FlightsInCount"); OnPropertyChanged("FlightsTotal");
            }
        }

        #endregion
        public new string ToString()
        {
            return this.Code + ", " + City + " [" + Longitude + ", " + Latitude + "]";
        }
        public Airport Copy()
        {
            var ap = new Airport();
            ap.CodeIATA = this.CodeIATA;
            ap.CodeICAO = this.CodeICAO;
            ap.Name = this.Name;
            ap.Longitude = this.Longitude;
            ap.Latitude = this.Latitude;
            ap.State = this.State;
            ap.Country = this.Country;
            ap.City = this.City;
            ap.FlightsInCount = this.FlightsInCount;
            ap.FlightsOutCount = this.FlightsOutCount;
            return ap;
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { if (_isSelected == value) return; _isSelected = value; OnPropertyChanged("IsSelected"); }
        }
    }
    public class AirportsViewModel : ObservableModel
    {
        public AirportsViewModel()
        {
            this.Dictionary = new AirportDictionary();
            this.List = new AirportList();
        }
        public AirportDictionary Dictionary { get; private set; }

        private ObservableCollection<Airport> _locations = new ObservableCollection<Airport>();
        public ObservableCollection<Airport> Locations
        {
            get { return _locations; }
            set { if (_locations == value) return; _locations = value; OnPropertyChanged("Locations"); }
        }

        private AirportList _list = new AirportList();
        public AirportList List
        {
            get { return _list; }
            private set { if (_list == value) return; _list = value; OnPropertyChanged("List"); }
        }

        public void Add(AirportList airports)
        {
            this.Dictionary.Add(airports);
            this.List.Clear();
            this.List.AddRange(this.Dictionary.Values.ToList());

            //OnPropertyChanged("List");
        }
    }
    public class AirportList : List<Airport>
    {
        public void FromList(List<Airport> airports)
        {
            this.Clear();
            this.AddRange(airports);
        }
    }
    public class AirportDictionary : Dictionary<string, Airport>
    {
        public void Add(AirportList airports)
        {
            foreach (var airport in airports)
            {
                this.Add(airport);
            }
        }

        public void Add(Airport airport)
        {
            if (airport == null) return;
            var key = airport.Code;
            if (this.ContainsKey(key))
            {
                // add v to existing item in the dictionary
                this[key] = airport;
            }
            else
            {
                // add airport as new item to in the dictionary
                this.Add(key, airport);
            }
        }

    }

}