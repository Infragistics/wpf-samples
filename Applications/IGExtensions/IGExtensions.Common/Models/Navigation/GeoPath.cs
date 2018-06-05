using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents a geographic path two geographic locations with computation method
    /// </summary>
    public class GeoPath : ObservableModel //GeoPointList, INotifyPropertyChanged //GeoLocationList //List<Point>
    {
        public GeoPath()
            : this(GeoPoint.Invalid, GeoPoint.Invalid)
        { }
        //public GeoPath(IEnumerable<IGeoLocatable> points, double resolution = 2.0)
        //    : this(points, resolution)
        //{ }
        public GeoPath(IEnumerable<IGeoLocatable> points)
        {
            //Resolution = GeoGlobal.Earths.CircumferenceOneDegree * resolution;
            if (points.Count() >= 2)
            {
                _origin = points.First().ToGeoPoint();
                _destination = points.Last().ToGeoPoint();
                _distance = GeoCalculator.GetDistance(this.Origin, this.Destination).Kilometers;
            }
            else
            {
                _distance = 0.0;
            }
            var pathPoints = points.Select(item => item.ToPoint()).ToList();
            //_points = new List<List<Point>> { pathPoints };
            _points = pathPoints;
        }

        public GeoPath(IGeoLocatable origin, IGeoLocatable destination, double resolution = 2.0)
        {
            Resolution = resolution;
            _origin = origin.ToGeoPoint();
            _destination = destination.ToGeoPoint();

            this.ComputePath();
        }
        /// <summary>
        /// Computes geographic path between <see cref="Origin"/> and <see cref="Destination"/> locations
        /// </summary>
        public void ComputePath()
        {
            if (this.Origin == null || !this.Origin.IsValid()) return;
            if (this.Destination == null || !this.Destination.IsValid()) return;

            try
            {
                var pathLocations = new GeoPathLocations();
                var pathResolution = GeoGlobal.Earths.CircumferenceOneDegree * Resolution;
                var path = GeoCalculator.GetPathPoints(this.Origin, this.Destination, pathResolution);
                var pathDistance = GeoCalculator.GetDistance(this.Origin, this.Destination);
                var bearingInitial = GeoCalculator.GetBearing(this.Origin, this.Destination);
                var bearingFinal = GeoCalculator.GetBearingFinal(this.Origin, this.Destination);
                var pathPoints = new List<Point>();
                foreach (var item in path)
                {
                    var location = new GeoPathLocation(item);
                    //location.Bearing = GeoCalculator.GetBearing(item, this.Destination);
                    location.Distance = GeoCalculator.GetDistance(this.Origin, item).Kilometers;
                    location.Progress = location.Distance / pathDistance.Kilometers;
                    location.Bearing = location.Progress >= 1
                                           ? bearingFinal
                                           : GeoCalculator.GetBearing(item, this.Destination);

                    pathLocations.Add(location);
                    pathPoints.Add(item.ToPoint());
                }

                #region Flattening
                //double maximumValue = 85.05112878 * Math.PI / 180.0;
                //double minimumValue = -85.05112878 * Math.PI / 180.0;
                //double maxSin = Math.Sin(maximumValue);
                //double maxProj = 0.5 * Math.Log((1 + maxSin) / (1 - maxSin));
                //double minSin = Math.Sin(minimumValue);
                //double minProj = 0.5 * Math.Log((1 + minSin) / (1 - minSin));

                //var indexes = Infragistics.Flattener.Flatten(
                //    points.Count,
                //    (i) =>
                //    {
                //        var scaled = ((points[i].X / 180.0) + 1.0) / 2.0;
                //        var x = 1000.0 * scaled;
                //        return x;
                //    },
                //    (i) =>
                //    {
                //        var value = points[i].Y * Math.PI / 180.0;
                //        double valueSin = Math.Sin(value);
                //        double projectedValue = 0.5 * Math.Log((1 + valueSin) / (1 - valueSin));

                //        var scaled = (projectedValue - minProj) / (maxProj - minProj);
                //        var y = 1000.0 * scaled;
                //        return y;
                //    },
                //    10); 
                //var newPoints = new List<Point>();
                //foreach (var index in indexes)
                //{
                //    newPoints.Add(points[index]);
                //}
                //var pathPoints = new List<List<Point>> { newPoints };

                #endregion

                //var pathPoints = points; // new List<List<Point>> { newPoints };

                this.Points = pathPoints;
                this.Distance = pathDistance.Kilometers;
                this.Locations = pathLocations;
                this.BearingInitial = bearingInitial;
                this.BearingFinal = bearingFinal;

            }
            catch (Exception ex)
            {
                var error = "GeoCalculator failed to compute path between " + Origin + " " + Destination;
                throw new AppException(error, ex);
            }
        }

        #region Properties
        private double _bearingInitial;
        /// <summary>
        /// Gets or sets BearingInitial property 
        /// </summary>
        public double BearingInitial
        {
            get { return _bearingInitial; }
            private set { if (_bearingInitial == value) return; _bearingInitial = value; OnPropertyChanged("BearingInitial"); }
        }
        private double _bearingFinal;
        /// <summary>
        /// Gets or sets BearingFinal property 
        /// </summary>
        public double BearingFinal
        {
            get { return _bearingFinal; }
            private set { if (_bearingFinal == value) return; _bearingFinal = value; OnPropertyChanged("BearingFinal"); }
        }
        private GeoPoint _destination = GeoPoint.Invalid;
        /// <summary>
        /// Gets or sets Destination location of the geographic path
        /// </summary>
        public GeoPoint Destination
        {
            get { return _destination; }
            set { if (_destination == value) return; _destination = value; ComputePath(); OnPropertyChanged("Destination"); }
        }
        private GeoPoint _origin = GeoPoint.Invalid;
        /// <summary>
        /// Gets or sets Origin location of the geographic path
        /// </summary>
        public GeoPoint Origin
        {
            get { return _origin; }
            set { if (_origin == value) return; _origin = value; ComputePath(); OnPropertyChanged("Origin"); }
        } 
        private double _resolution;
        /// <summary>
        /// Gets or sets Resolution property 
        /// </summary>
        public double Resolution
        {
            get {  return _resolution; }
            set { if (_resolution == value && value >= 1.0) return; _resolution = value; OnPropertyChanged("Resolution"); }
        }
        //public double Resolution { get; set; }
        private double _distance = 2.0;
        /// <summary>
        /// Gets or sets Distance property 
        /// </summary>
        public double Distance
        {
            get {  return _distance; }
            private set { if (_distance == value) return; _distance = value; OnPropertyChanged("Distance"); }
        }
        private List<Point> _points = new List<Point>();
        /// <summary>
        /// Gets list of <see cref="Point"/>s in the geographic path
        /// </summary>
        public List<Point> Points
        {
            get { return _points; }
            private set { if (_points == value) return; _points = value; OnPropertyChanged("Points"); }
        }
        //private List<List<Point>> _points = new List<List<Point>>();
        ///// <summary>
        ///// Gets list of <see cref="Point"/>s in the geographic path
        ///// </summary>
        //public List<List<Point>> Points
        //{
        //    get {  return _points; }
        //    private set { if (_points == value) return; _points = value; OnPropertyChanged("Points"); }
        //}
        private GeoPathLocations _locations = new GeoPathLocations();
        /// <summary>
        /// Gets list of <see cref="GeoPoint"/>s in the geographic path
        /// </summary>
        public GeoPathLocations Locations
        {
            get { return _locations; }
            private set { if (_locations == value) return; _locations = value; OnPropertyChanged("Locations"); }
        }
        //private GeoPointList _locations = new GeoPointList();
        ///// <summary>
        ///// Gets list of <see cref="GeoPoint"/>s in the geographic path
        ///// </summary>
        //public GeoPointList Locations
        //{
        //    get {  return _locations; }
        //    private set { if (_locations == value) return; _locations = value; OnPropertyChanged("Locations"); }
        //}
        #endregion



    }
    public class GeoPathLocations : List<GeoPathLocation>
    { }
    public class GeoPathLocation : GeoPoint
    {
        public GeoPathLocation() : this(GeoPoint.Invalid)
        { }
        public GeoPathLocation(GeoPoint geoPoint) : base(geoPoint.ToPoint())
        {
            Bearing = 0;
            Progress = 0;
            Distance = 0;
        }
        
        public double Bearing { get; set; }
        public double Progress { get; set; }
        public double Distance { get; set; }


    }
}