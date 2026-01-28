using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.Navigation;

namespace IGGeographicMap.Models
{
    public class AirlineConnection : ObservableModel
    {
        #region Properties
        private Airport _destination = new Airport();
        public Airport Destination
        {
            get { return _destination; }
            set { if (_destination == value) return; _destination = value; OnPropertyChanged("Destination"); OnPropertyChanged("Path"); }
        }
        private Airport _origin = new Airport();
        public Airport Origin
        {
            get { return _origin; }
            set { if (_origin == value) return; _origin = value; OnPropertyChanged("Origin"); OnPropertyChanged("Path"); }
        }

        public GeoLocation MidLocation
        {
            get { return GetMidLocation(); }
        }
        public GeoPath Path
        {
            get { return GetConnectionPath(); }
        }
        private double? _distanceTotal;
        public double DistanceTotal
        {
            get
            {
                if (!_distanceTotal.HasValue)
                {
                    _distanceTotal = GetDistanceTotal();
                }
                return _distanceTotal.HasValue ? _distanceTotal.Value : 0.0;
            }
        }
        public string ConnectionCode { get { return GetConnectionCode(); } }
        #endregion

        #region Methods
        public double? GetDistanceTotal()
        {
            double? distance = null;
            if (this.IsValidConnection)
                distance = GeoCalculator.GetGreatCircleDistance(this.Origin, this.Destination);

            return distance;
        }
        public GeoLocation GetMidLocation()
        {
            return GeoCalculator.GetMidLocation(this.Origin, this.Destination);
        }
        public string GetConnectionCode()
        {
            return this.Origin.Code + "-" + this.Destination.Code;
        }
        public bool IsValidConnection
        {
            get { return this.Origin.IsValidGeoLocation() && this.Destination.IsValidGeoLocation(); }
        }
        public GeoPath GetConnectionPath()
        {
            var path = new GeoPath { this.Origin.ToPoint(), this.Destination.ToPoint() };
            return path;
        }
        #endregion
    }

}