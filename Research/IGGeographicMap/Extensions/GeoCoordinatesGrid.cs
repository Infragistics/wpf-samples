using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Extensions
{
    /// <summary>
    /// Represents the geo-coordinate grid that consist of geo-longitude and geo-latitude lines.
    /// </summary>
    public class GeoCoordinatesGrid : GeoCoordinateLineList, INotifyPropertyChanged  
    {
        public static double LongitudeMin = -300;
        public static double LongitudeMax = 300;

        public static double LatitudeMin = -90;
        public static double LatitudeMax = 90;

        public GeoCoordinatesGrid()
        {
        }
        public GeoCoordinatesGrid(double longitudeInterval = 30, double latitudeInterval = 15)
        {
            CreateCoordinatesGrid(longitudeInterval, latitudeInterval);
        }
        #region Methods
        public void UpdateCoordinatesGrid()
        {
            CreateCoordinatesGrid(this.LongitudeInterval, this.LatitudeInterval);
        }
        public void CreateCoordinatesGrid(double longitudeInterval = 30, double latitudeInterval = 15)
        {
            this.Clear();
            AddLongitudeLines(longitudeInterval);
            AddLatitudeLines(latitudeInterval);
        }
        public void AddLongitudeLines(double longitudeInterval = 30)
        {
            if (!IsValidCoordinateInterval(longitudeInterval)) return;
            
            _longitudeInterval = longitudeInterval;
            for (double longitude = LongitudeMin; longitude <= LongitudeMax; longitude += longitudeInterval)
            {
                this.Add(new GeoLongitudeLine(longitude, 0.25));
            }
        }
        public void AddLatitudeLines(double latitudeInterval = 15)
        {
            if (!IsValidCoordinateInterval(latitudeInterval)) return;

            _latitudeInterval = latitudeInterval;
            for (double latitude = LatitudeMin; latitude <= LatitudeMax; latitude += latitudeInterval)
            {
                this.Add(new GeoLatitudeLine(latitude, 0.25));
            }
        }
        public static bool IsValidCoordinateInterval(double value)
        {
            if (double.IsNaN(value) || value == 0)
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Porperties

        public List<List<Point>> CoordinatePoints
        {
            get
            {
                var points = new List<List<Point>>();
                foreach (var coordinateLine in this)
                {
                    points.Add(coordinateLine.Points);
                }
                return points;
            }
        }

        public double LongitudeInterval
        {
            get { return _longitudeInterval; }
            set
            {
                if (_longitudeInterval == value) return;
                _longitudeInterval = value;
                UpdateCoordinatesGrid();
                OnPropertyChanged("LongitudeInterval");
            }
        }
        private double _longitudeInterval = double.NaN;

        public double LatitudeInterval
        {
            get { return _latitudeInterval; }
            set
            {
                if (_latitudeInterval == value) return;
                _latitudeInterval = value;
                UpdateCoordinatesGrid();
                OnPropertyChanged("LatitudeInterval");
            }
        }
        private double _latitudeInterval = double.NaN;
        #endregion

        #region INotifyPropertyChanged
        public new event PropertyChangedEventHandler PropertyChanged;
        protected new void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected new void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        } 
        #endregion
    }

    #region Coordinate Lines

    /// <summary>
    /// Represents a list of geo-coordinate lines 
    /// </summary>
    public class GeoCoordinateLineList : List<GeoCoordinateLine>, INotifyPropertyChanged  
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        #endregion
    }

    /// <summary>
    /// Represents the base class for all geo-coordinate lines 
    /// </summary>
    public abstract class GeoCoordinateLine : ObservableModel
    {
        protected GeoCoordinateLine()
        {
            this.Points = new List<Point>();
        }

        #region Properties
        /// <summary>
        /// Gets or sets a list of points that defines the geo-coordinate line
        /// </summary>
        public List<Point> Points
        {
            get { return _coordinatePoints; }
            set
            {
                if (_coordinatePoints == value) return;
                _coordinatePoints = value;
                OnPropertyChanged("Points");
            }
        }
        private List<Point> _coordinatePoints;
        /// <summary>
        /// Gets or sets the label for the geo-coordinate line
        /// </summary>
        public string Label
        {
            get { return _label; }
            set
            {
                if (_label.Equals(value)) return;
                _label = value;
                OnPropertyChanged("Label");
            }
        }
        private string _label = string.Empty;
        /// <summary>
        /// Gets or sets latitude of the label in geo-coordinate system
        /// </summary>
        public double LabelLatitude
        {
            get { return _labelLatitude; }
            set
            {
                if (_labelLatitude == value) return;
                _labelLatitude = value;
                OnPropertyChanged("LabelLatitude");
            }
        }

        private double _labelLatitude = double.NaN;

        /// <summary>
        /// Gets or sets longitude of the label in geo-coordinate system
        /// </summary>
        //public double LabelLongitude { get; set; }
        public double LabelLongitude
        {
            get { return _labelLongitude; }
            set
            {
                if (_labelLongitude == value) return;
                _labelLongitude = value;
                OnPropertyChanged("LabelLatitude");
            }
        }

        private double _labelLongitude = double.NaN;

        #endregion
        #region Methods
        public void SetLabel(double value)
        {
            this.Label = value.ToString(); // string.Format("{0:0.0}", value);
            OnPropertyChanged("Label");
        }
        public static List<Point> GetLatitudePoints(double latitude, double interval = 0.25)
        {
            var min = GeoCoordinatesGrid.LongitudeMin;
            var max = GeoCoordinatesGrid.LongitudeMax;
            var points = new List<Point>();
            for (double longitude = min; longitude <= max; longitude += interval)
            {
                points.Add(new Point(longitude, latitude));
            }
            return points;
        }
        public static List<Point> GetLongitudePoints(double longitude, double interval = 0.25)
        {
            var min = GeoCoordinatesGrid.LatitudeMin;
            var max = GeoCoordinatesGrid.LatitudeMax;
            var points = new List<Point>();
            for (double latitude = min; latitude <= max; latitude += interval)
            {
                points.Add(new Point(longitude, latitude));
            }
            return points;
        }

        public override string ToString()
        {
            string result = string.Empty;
            foreach (var point in Points)
            {
                result += string.Format("({0}, {1})", point.X, point.Y);
            }
            return result;
        }
        #endregion
    }
    
    /// <summary>
    /// Represents a geo-longitude lines
    /// </summary>
    public class GeoLongitudeLine : GeoCoordinateLine
    {
        public GeoLongitudeLine()
            : this(0)
        { }

        public GeoLongitudeLine(double longitude)
        {
            CreateLongitudeLine(longitude);
        }
        public GeoLongitudeLine(double longitude, double interval = 0.25)
        {
            CreateLongitudeLine(longitude, interval);
        }
        public void CreateLongitudeLine(double longitude, double interval = 0.25)
        {
            _longitude = longitude;
            this.LabelLongitude = longitude;
            this.SetLabel(longitude);
            var linePoints = GeoCoordinateLine.GetLongitudePoints(longitude, interval);
            this.Points = linePoints;
        }
        public double Longitude
        {
            get { return _longitude; }
            set
            {
                if (_longitude == value) return;
                _longitude = value;
                CreateLongitudeLine(_longitude);
                OnPropertyChanged("Longitude");
            }
        }
        private double _longitude = double.NaN;

    }

    /// <summary>
    /// Represents a geo-latitude lines
    /// </summary>
    public class GeoLatitudeLine : GeoCoordinateLine
    {

        public GeoLatitudeLine()
            : this(0)
        { }

        public GeoLatitudeLine(double longitude, double interval = 0.25)
        {
            CreateLatitudeLine(longitude, interval);
        }

        public void CreateLatitudeLine(double latitude, double interval = 0.25)
        {
            _latitude = latitude;
            this.LabelLatitude = latitude;
            this.SetLabel(latitude);
            var linePoints = GeoCoordinateLine.GetLatitudePoints(latitude, interval);
            this.Points = linePoints;
        }
        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude == value) return;
                _latitude = value;
                CreateLatitudeLine(_latitude);

                OnPropertyChanged("Latitude");
            }
        }
        private double _latitude = double.NaN;

    }

    #endregion
    
    #region Coordinate Labels

    /// <summary>
    /// Represents a location of label for geo-coordinate line
    /// </summary>
    public class GeoCoordinateLabel : DependencyObject 
    {
        public GeoCoordinateLabel()
        {

        }
        public GeoCoordinateLabel(double longitude, double latitude)
        {
            CreateLabelLine(longitude, latitude);
        }
        public void CreateLabelLine(double longitude, double latitude)
        {
            _labelLatitude = latitude;
            _labelLongitude = longitude;
        }
        #region Properties
      
        #region Label Dependency Property

        public const string LabelPropertyName = "Label";

        /// <summary>
        /// Identifies the FillValue dependency property.
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(LabelPropertyName, typeof(string), typeof(GeoCoordinateLabel),
            new PropertyMetadata(string.Empty, (sender, e) =>
            {
                (sender as GeoCoordinateLabel).OnPropertyChanged(new PropertyChangedEventArgs(LabelPropertyName));
            }));

        /// <summary>
        /// Gets or sets the fill value for this scale.
        /// </summary>
        public string Label
        {
            get
            {
                return (string)GetValue(LabelProperty);
            }
            set
            {
                SetValue(LabelProperty, value);
            }
        }
        #endregion

        /// <summary>
        /// Gets or sets latitude of the label in geo-coordinate system
        /// </summary>
        public double Latitude
        {
            get { return _labelLatitude; }
            set
            {
                if (_labelLatitude == value) return;
                _labelLatitude = value;
                OnPropertyChanged("Latitude");
            }
        }

        private double _labelLatitude = double.NaN;

        /// <summary>
        /// Gets or sets longitude of the label in geo-coordinate system
        /// </summary>
        //public double LabelLongitude { get; set; }
        public double Longitude
        {
            get { return _labelLongitude; }
            set
            {
                if (_labelLongitude == value) return;
                _labelLongitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private double _labelLongitude = double.NaN;
        #endregion
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        public void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            //switch (ea.PropertyName)
            //{

            //}
        }

    }

    /// <summary>
    /// Represents the base class for all list of labels for geo-coordinate lines 
    /// </summary>
    public class GeoCoordinateLabelsList : ObservableCollection<GeoCoordinateLabel>
    {
        #region INotifyPropertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public GeoCoordinateLabelsList GetLabels()
        {
            var geoLabels = new GeoCoordinateLabelsList();
            foreach (var item in this)
            {
                geoLabels.Add(item);
            }
            return geoLabels;
        }
        public void AddRange(GeoCoordinateLabelsList list)
        {
            foreach (var item in list)
            {
                this.Add(item);
            }
        }
    }

    /// <summary>
    ///  Represents a list of labels for Longitude, Latitude and custom geo-coordinate lines
    /// </summary>
    public class GeoCoordinateLabels : GeoCoordinateLabelsList
    {
        public GeoCoordinateLabels()
        {
            _longitudeLabels = new GeoLongitudeRangeLabels();
            _latitudeLabels = new GeoLatitudeRangeLabels();
            _customLabels = new GeoCoordinateLabelsList();

            _customLabels.CollectionChanged += OnLabelsCollectionChanged;
            _latitudeLabels.CollectionChanged += OnLabelsCollectionChanged;
            _longitudeLabels.CollectionChanged += OnLabelsCollectionChanged;
        }

        void OnLabelsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateLabels();
        }
        public void UpdateLabels()
        {
            this.Clear();
            foreach (var label in this.LongitudeLabels)
            {
                this.Add(label);
            }
            foreach (var label in this.LatitudeLabels)
            {
                this.Add(label);
            }
            foreach (var label in this.CustomLabels)
            {
                this.Add(label);
            }
        }
        public GeoLongitudeRangeLabels LongitudeLabels
        {
            get { return _longitudeLabels; }
            set
            {
                if (_longitudeLabels == value) return;
                _longitudeLabels = value;
                UpdateLabels();
                OnPropertyChanged("LongitudeLabels");
            }
        }
        private GeoLongitudeRangeLabels _longitudeLabels;

        public GeoLatitudeRangeLabels LatitudeLabels
        {
            get { return _latitudeLabels; }
            set
            {
                if (_latitudeLabels == value) return;
                _latitudeLabels = value;
                UpdateLabels();
                OnPropertyChanged("LatitudeLabels");
            }
        }
        private GeoLatitudeRangeLabels _latitudeLabels;

        public GeoCoordinateLabelsList CustomLabels
        {
            get { return _customLabels; }
            set
            {
                if (_customLabels == value) return;
                _customLabels = value;
                UpdateLabels();
                OnPropertyChanged("CustomLabels");
            }
        }
        private GeoCoordinateLabelsList _customLabels;

        public GeoCoordinateLabels Clone()
        {
            var geoCoordinateLabels = new GeoCoordinateLabels();
            //geoCoordinateLabels.AddRange(this);

            geoCoordinateLabels.LongitudeLabels = this.LongitudeLabels.Clone();
            geoCoordinateLabels.LatitudeLabels = this.LatitudeLabels.Clone();
            geoCoordinateLabels.CustomLabels = this.CustomLabels.GetLabels();
            return geoCoordinateLabels;
        }
    }

    /// <summary>
    ///  Represents a range of labels for Longitude geo-coordinate lines
    /// </summary>
    public class GeoLongitudeRangeLabels : GeoCoordinateLabelsList
    {
        public GeoLongitudeRangeLabels()
        {

        }
        protected void UpdateLabels()
        {
            if (!GeoCoordinatesGrid.IsValidCoordinateInterval(this.LongitudeInterval)) return;

            this.Clear();
            _longitudeInterval = this.LongitudeInterval;
            var min = GeoCoordinatesGrid.LongitudeMin;
            var max = GeoCoordinatesGrid.LongitudeMax;
            for (double longitude = min; longitude <= max; longitude += this.LongitudeInterval)
            {
                this.Add(new GeoCoordinateLabel { Longitude = longitude, Latitude = this.Latitude, Label = longitude.ToString() });
            }
        }
        #region Porperties
        public double LongitudeInterval
        {
            get { return _longitudeInterval; }
            set
            {
                if (_longitudeInterval == value) return;
                _longitudeInterval = value;
                UpdateLabels();
                //OnPropertyChanged(new PropertyChangedEventArgs("LongitudeInterval"));
                OnPropertyChanged("LongitudeInterval");
            }
        }
        private double _longitudeInterval = double.NaN;

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude == value) return;
                _latitude = value;
                UpdateLabels();
                //OnPropertyChanged(new PropertyChangedEventArgs("LatitudeInterval"));
                OnPropertyChanged("Latitude");
            }
        }
        private double _latitude = double.NaN;
        #endregion

        public GeoLongitudeRangeLabels Clone()
        {
            var geoRangeLabels = new GeoLongitudeRangeLabels();
            geoRangeLabels.Latitude = this.Latitude;
            geoRangeLabels.LongitudeInterval = this.LongitudeInterval;
            return geoRangeLabels;
        }
    }

    /// <summary>
    ///  Represents a range of labels for Latitude geo-coordinate lines  
    /// </summary>
    public class GeoLatitudeRangeLabels : GeoCoordinateLabelsList
    {
        public GeoLatitudeRangeLabels()
        {

        }
        public void UpdateLabels()
        {
            if (!GeoCoordinatesGrid.IsValidCoordinateInterval(this.LatitudeInterval)) return;

            this.Clear();
            _latitudeInterval = this.LatitudeInterval;
            var min = GeoCoordinatesGrid.LatitudeMin;
            var max = GeoCoordinatesGrid.LatitudeMax;
            for (double latitude = min; latitude <= max; latitude += this.LatitudeInterval)
            {
                this.Add(new GeoCoordinateLabel { Longitude = this.Longitude, Latitude = latitude, Label = latitude.ToString() });
            }
        }
        #region Porperties
        public double LatitudeInterval
        {
            get { return _latitudeInterval; }
            set
            {
                if (_latitudeInterval == value) return;
                _latitudeInterval = value;
                UpdateLabels();
                //OnPropertyChanged(new PropertyChangedEventArgs("LongitudeInterval"));
                OnPropertyChanged("LatitudeInterval");
            }
        }
        private double _latitudeInterval = double.NaN;

        public double Longitude
        {
            get { return _latitude; }
            set
            {
                if (_latitude == value) return;
                _latitude = value;
                UpdateLabels();
                //OnPropertyChanged(new PropertyChangedEventArgs("LatitudeInterval"));
                OnPropertyChanged("Longitude");
            }
        }
        private double _latitude = double.NaN;
        #endregion
        public GeoLatitudeRangeLabels Clone()
        {
            var geoRangeLabels = new GeoLatitudeRangeLabels();
            geoRangeLabels.Longitude = this.Longitude;
            geoRangeLabels.LatitudeInterval = this.LatitudeInterval;
            return geoRangeLabels;
        }
    }
    #endregion
  
}