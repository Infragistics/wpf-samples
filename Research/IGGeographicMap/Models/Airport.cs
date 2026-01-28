using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Models
{
    public class Airport : GeoLocation
    {
        public string Code { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }

        public int FlightsTotal
        {
            get { return _flightsInCount + _flightsOutCount; }
        }
        private int _flightsOutCount;
        public int FlightsOutCount
        {
            get { return _flightsOutCount; }
            set 
            { 
                if (_flightsOutCount == value) return; _flightsOutCount = value;
                OnPropertyChanged("FlightsOutCount"); OnPropertyChanged("FlightsTotal");
            }
        }
        private int _flightsInCount;
        public int FlightsInCount
        {
            get { return _flightsInCount; }
            set 
            { 
                if (_flightsInCount == value) return; _flightsInCount = value;
                OnPropertyChanged("FlightsInCount"); OnPropertyChanged("FlightsTotal"); 
            }
        }

        public Airport Copy()
        {
            var ap = new Airport();
            ap.Code = this.Code;
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