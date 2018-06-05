using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using IGExtensions.Framework;

namespace IGExtensions.Common.Models
{
    public interface IGeoNavigational
    {
        //double Longitude { get; set; }
        //double Latitude { get; set; }
        Rect NavigationRect(Point geoPosition);
    }

 
    public interface IGeoDimensional
    {
        GeoPoint Location { get; }
        Size Size { get; }
        double Width { get; }
        double Height { get; }
    }
    public class GeoRect : IGeoDimensional
    {
        #region Constructors
        public GeoRect()
            : this(0, 0, 0, 0)
        { }
        public GeoRect(GeoPoint southWest, double width, double height)
            : this(southWest.Longitude, southWest.Latitude, width, height)
        { }
        public GeoRect(Point southWest, Size size)
            : this(southWest.X, southWest.Y, size.Width, size.Height)
        { }
        public GeoRect(Point southWest, double width, double height)
            : this(southWest.X, southWest.Y, width, height)
        { }
        public GeoRect(Rect rect)
            : this(rect.X, rect.Y, rect.Width, rect.Height)
        { }
        public GeoRect(double west, double south, Size size)
            : this(west, south, size.Width, size.Height)
        { }
        public GeoRect(double west, double south, double size)
            : this(west, south, size, size)
        { }
        public GeoRect(double west, double south, double east, double north)
        {
            _south = south;
            _north = north;
            _west = west;
            _east = east;
          
            UpdateSize(); 
            UpdateCenter();
        } 
        #endregion
    
        #region Properties
      
        public List<Point> Points
        {
            get
            {
                var points = this.ToRect().ToPoints();
                return points;
            }
        }
        public List<GeoPoint> Corners
        {
            get
            {
                var points = new List<GeoPoint> {
                    this.SouthWest, this.NorthWest,
                    this.NorthEast, this.SouthEast};
                return points;
            }
        }
        #region Locations
        public GeoPoint SouthWest { get { return new GeoPoint(this.West, this.South); } }
        public GeoPoint NorthWest { get { return new GeoPoint(this.West, this.North); } }
        public GeoPoint SouthEast { get { return new GeoPoint(this.East, this.South); } }
        public GeoPoint NorthEast { get { return new GeoPoint(this.East, this.North); } }

        private double _west;
        /// <summary>
        /// Gets or sets West property 
        /// </summary>
        public double West
        {
            get { return _west; }
            set { if (_west == value) return; _west = value; OnCornersChanged(); }
        }
        private double _east;
        /// <summary>
        /// Gets or sets East property 
        /// </summary>
        public double East
        {
            get { return _east; }
            set { if (_east == value) return; _east = value; OnCornersChanged(); }
        }
        private double _south;
        /// <summary>
        /// Gets or sets South property 
        /// </summary>
        public double South
        {
            get { return _south; }
            set { if (_south == value) return; _south = value; OnCornersChanged(); }
        }
        private double _north;
        /// <summary>
        /// Gets or sets North property 
        /// </summary>
        public double North
        {
            get { return _north; }
            set { if (_north == value) return; _north = value; OnCornersChanged(); }
        } 
     
        public GeoPoint Location { get { return this.SouthWest; } }
        
        private GeoPoint _center;
        /// <summary>
        /// Gets or sets Center property 
        /// </summary>
        public GeoPoint Center
        {
            get { return _center; }
            //set { if (_center == value) return; _center = value; OnCenterChanged(); }
        }
    
        #endregion
        #region Size
        private Size _size;
        public Size Size { get { return _size; } }
        public double Width { get { return _size.Width; } }
        public double Height { get { return  _size.Height; } }
        
        public double Area { get { return this.Width * this.Height; } }
        //public double Diagonal { get { return this.SouthWest.Distance(this.NorthEast); } }

        ///// <summary>
        ///// Gets or sets Size property 
        ///// </summary>
        //public Size Size
        //{
        //    get { return _size; }
        //    set { if (_size == value) return; _size = value; OnSizeChanged(); }
        //}
     
        //public double Width
        //{
        //    get { return _size.Width; }
        //    set { if (_size.Width == value) _size.Width = value; OnSizeChanged(); }
        //}
        //public double Height
        //{
        //    get { return _size.Height; }
        //    set { if (_size.Height == value) _size.Height = value; OnSizeChanged(); }
        //} 

        #endregion
        #endregion
        #region Methods
        public bool IsEmpty
        {
            get { return this.East == this.West || this.North == this.South; }
        }

        protected bool IsUpdating = false;
        //private void OnLocationChanged()
        //{
        //    UpdateCenter();
        //    UpdateCorners(this.Location, this.Size);
        //}
        private void OnSizeChanged()
        {
            UpdateCenter();
            UpdateCorners(this.Location, this.Size);
        }
        private void OnCornersChanged()
        {
            UpdateLocation(this.West, this.South, this.North, this.East);
            UpdateSize();
            UpdateCenter();
        }
        private void OnCenterChanged()
        {
            UpdateLocation(this.Center, this.Size);
        }
        private void UpdateCenter()
        {
            IsUpdating = true;

            var longitude = this.West + (this.Width / 2.0);
            var latitude = this.South + (this.Height / 2.0);
            _center = new GeoPoint(longitude, latitude);

            IsUpdating = false;
        }
        private void UpdateCorners(GeoPoint location, Size size)
        {
            IsUpdating = true;
 
            _west = location.Longitude;
            _south = location.Latitude;
            _north = _south + size.Height;
            _east = _west + size.Width;
         
            IsUpdating = false;
        }
        private void UpdateLocation(GeoPoint center, Size size)
        {
            IsUpdating = true;

            var longitude = center.Longitude - (size.Width / 2.0);
            var latitude = center.Latitude - (size.Height / 2.0);
            //_location = new GeoPoint(longitude, latitude);
            _west = longitude;
            _south = latitude;
            _north = _south + size.Height;
            _east = _west + size.Width;

            IsUpdating = false;
        }
       
        private void UpdateSize()
        {
            _size.Width = System.Math.Abs(this.East - this.West);
            _size.Height = System.Math.Abs(this.North - this.South);
        }
        private void UpdateLocation(double west, double south, double north, double east)
        {
            IsUpdating = true;

            _west = System.Math.Min(west, east);
            _east = System.Math.Max(west, east);

            _south = System.Math.Min(south, north);
            _north = System.Math.Max(south, north);

            IsUpdating = false;
        } 
        public Rect ToRect()
        {
            var rect = new Rect(this.West, this.South, this.Width, this.Height);
            //var rect = new Rect(this.Location.ToPoint(), this.Size);
            return rect;
        }
        public Rect ToWorldRect()
        {
            var rect = new Rect(this.West, this.North, this.Width, this.Height);
            //var rect = new Rect(this.Location.ToPoint(), this.Size);
            return rect;
        }
        #endregion

        public override string ToString()
        {
            var result = string.Empty;
            result += this.West + " " + this.South + " " + this.East + " " + this.North + " (" + this.Width + " " + this.Height + ")";
            return result;
        }

        ///// <summary>
        ///// GeoRegion of the whole world
        ///// </summary>
        public static readonly GeoRect WorldRect = GeoGlobal.Worlds.ActualRegion;
        public static readonly GeoRect WorldMercatorRect = GeoGlobal.WorldsMercator.MapRegion;
        //public static GeoRect EmptyRect = new GeoRect(Rect.Empty);
        public static readonly GeoRect Empty = new GeoRect(0, 0, 0, 0);
      
    }

    /// <summary>
    /// Represents a region in geographic coordinate system and provides methods for converting between different objects
    /// <remarks>This is an observable model that implements <see cref="INotifyPropertyChanged"/> interface</remarks>
    /// </summary>
    public class GeoRegion : ObservableModel
    {
        #region Constructors
        
        public GeoRegion()
            : this(new GeoLocation { Longitude = -180, Latitude = -90 }, 
                   new GeoLocation { Longitude = 180, Latitude = 90 })
        {
            //_origins = new GeoLocation { Longitude = -180, Latitude = -90 };
            //_ending = new GeoLocation { Longitude = 180, Latitude = 90 };
            //this.UpdateCenter();
        }
        /// <summary>
        /// Creates a GeoRegion from south-west geo-location and north-east geo-location 
        /// </summary>
        public GeoRegion(Point origin, Point ending)
            : this(new GeoLocation(origin), new GeoLocation(ending))
        {
        }
        /// <summary>
        /// Creates a GeoRegion from south-west geo-location and north-east geo-location 
        /// </summary>
        public GeoRegion(GeoLocation origin, GeoLocation ending)
        {
            _origins = GeoLocation.Min(origin, ending);
            _ending = GeoLocation.Max(origin, ending);
            this.UpdateSize();
            this.UpdateCenter();
        }
        /// <summary>
        /// Creates a GeoRegion from geo-location (center of region) and geo-dimension (height and width of region)
        /// </summary>
        public GeoRegion(GeoLocation center, double width, double height)
            : this(new GeoLocation { Longitude = center.Longitude - (width / 2), Latitude = center.Latitude - (height / 2) },
                   new GeoLocation { Longitude = center.Longitude + (width / 2), Latitude = center.Latitude + (height / 2) })
        { }

        /// <summary>
        /// Creates a GeoRegion from geo-location (center of region) and geo-dimension (height and width of region)
        /// </summary>
        public GeoRegion(GeoLocation center, double dimension)
            : this(center, dimension, dimension)
        { }
         
        public GeoRegion(Rect mapRect)
            : this(new GeoLocation { Longitude = mapRect.X, Latitude = mapRect.Y },
                   new GeoLocation { Longitude = mapRect.X + mapRect.Width, Latitude = mapRect.Y + mapRect.Height })
        { }

        /// <summary>
        /// Creates a GeoRegion from geographic west, south, height, and width values
        /// </summary>
        public GeoRegion(double west, double south, double width, double height)
            : this(new Rect(west, south, width, height))
        //      : this(new Rect(originLongitude, originLatitude + height, width, height))
        { }

        /// <summary>
        /// Creates a GeoRegion from geo-location (south-west) and geo-dimension (height and width)
        /// </summary>
        public GeoRegion(Point origin, double width, double height)
            : this(new Rect(origin.X, origin.Y, width, height))
        { }
        /// <summary>
        /// Creates a GeoRegion from geo-location (south-west) and geo-dimension (height and width)
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="dimension"></param>
        public GeoRegion(Point origin, double dimension)
            : this(new Rect(origin.X, origin.Y, dimension, dimension))
        { }

        public GeoRegion(List<Point> points)
            : this(RectEx.FromPoints(points))
        { }
        public GeoRegion(List<List<Point>> points)
            : this(RectEx.FromShapePoints(points))
        { }
        #endregion
        #region Properties

        public double Area
        {
            get { if (this.IsEmpty()) return 0.0; return this.Width * this.Height; }
        }

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        private string _name;

        //TODO remove/replace with SouthWest
        /// <summary>
        /// Gets or Sets the top-left corner (Origin) of GeoRegion
        /// </summary>
        public GeoLocation Origin { get { return _origins; } set { _origins = value; 
            UpdateSize(); UpdateCenter(); OnPropertyChanged("Origin"); } }
        private GeoLocation _origins;

        //TODO remove/replace with NorthEast
        /// <summary>
        /// Gets or Sets the bottom-right corner (Ending) of GeoRegion
        /// </summary>
        public GeoLocation Ending { get { return _ending; } set { _ending = value; 
            UpdateSize(); UpdateCenter(); OnPropertyChanged("Ending"); } }
        private GeoLocation _ending;
        
        /// <summary>
        /// Gets or Sets the center of GeoRegion
        /// </summary>
        public GeoLocation Center
        {
            get { return _center; }
            set
            {
                if (_center == value) return;
                //System.Diagnostics.Debug.WriteLine("CenterChanging " + _center.ToString() + " -> " + value.ToString());
                if (_center != null) _center.PropertyChanged -= OnCenterPropertyChanged;
                _center = value;
                _center.PropertyChanged += OnCenterPropertyChanged;
                this.UpdateLocation(); OnPropertyChanged("Center");
            }
        }

        private void OnCenterPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("CenterChanged " + this.Center.ToString() + " - " + e.PropertyName);
           // if (!isUpdating)
            //    OnPropertyChanged("Center");
        }
        private GeoLocation _center;

        public double CenterLongitude
        {
            get { return _centerLongitude; }
            set { _centerLongitude = value; UpdateLocation(); OnPropertyChanged("CenterLongitude"); }
        }
        private double _centerLongitude;
        public double CenterLatitude
        {
            get { return _centerLatitude; }
            set { _centerLatitude = value; UpdateLocation(); OnPropertyChanged("CenterLatitude"); }
        }
        private double _centerLatitude;
      
        public double Width
        {
            get { return _width; }
            set { _width = value; UpdateCenter(); OnPropertyChanged("Width"); }
        }
        private double _width;
        public double Height
        {
            get { return _height; }
            set { _height = value; UpdateCenter(); OnPropertyChanged("Height"); }
        }
        private double _height;
      
        public double North
        {
            get { return this.GetLatitudeMax(); }
        }
        public double South
        {
            get { return this.GetLatitudeMin(); }
        }
        public double East
        {
            get { return this.GetLongitudeMax(); }
        }
        public double West
        {
            get { return this.GetLongitudeMin(); }
        }
        public GeoLocation NorthWest 
        {
            get { return new GeoLocation(this.West, this.North); }
            //TODO add setter
        }
        public GeoLocation NorthEast
        {
            get { return new GeoLocation(this.East, this.North); }
            //TODO add setter
        }
        public GeoLocation SouthWest
        {
            get { return new GeoLocation(this.West, this.South); }
            //TODO add setter
        }
        public GeoLocation SouthEast
        {
            get { return new GeoLocation(this.East, this.South); }
            //TODO add setter
        }

        public List<Point> Points
        {
            get { return this.ToPoints(); }
        }
        public List<List<Point>> ShapePoints
        {
            get { return this.ToShapePoints(); }
        }
        
        #endregion

        #region Validation Methods
        public bool Contains(GeoLocation location)
        {
            if (location.Latitude < this.GetLatitudeMin() ||
                location.Latitude > this.GetLatitudeMax() ||
                location.Longitude < this.GetLongitudeMin() ||
                location.Longitude > this.GetLongitudeMax())
            {
                return false;
            }
            return true;
        }
        public bool Contains(GeoRegion region)
        {
            var rect1 = this.ToRect();
            var rect2 = region.ToRect();
            return rect1.Contains(rect2);
        }
        public bool IsEmpty()
        {
            var rect = this.ToRect();
            return rect.IsEmpty || this.Width == 0 || this.Height == 0;
        }
        #endregion
        #region Calculation Methods

        //private void UpdateWidth(double newWidth)
        //{
        //    UpdateSize(newWidth, this.Height);
        //}
        //private void UpdateHeight(double newHeight)
        //{
        //    UpdateSize(this.Width, newHeight);
        //}
        
        private void UpdateSize()
        {
            _width = System.Math.Abs(this.West - this.East);
            _height = System.Math.Abs(this.South - this.North);
            OnPropertyChanged("Height"); OnPropertyChanged("Width"); 
        }
        //private void UpdatePositionFromSize()
        //{
        //    UpdateLocation(_width, _height);
        //}

        //private void UpdateLocation(double newWidth, double newHeight)
        //{
        //    var w = newWidth;
        //    var h = newHeight;
        //    var center = this.Center;
        //    var longitude = center.Longitude - (w / 2);
        //    var latitude = center.Latitude - (h / 2);
        //    _origins = new GeoLocation(longitude, latitude);
        //    _ending = new GeoLocation(longitude + w, latitude + h);

        //    //UpdateCenter();

        //    OnPropertyChanged("Origin"); OnPropertyChanged("Ending"); 
        //}
        private double GetLatitudeMin()
        {
            return System.Math.Min(this.Origin.Latitude, this.Ending.Latitude);
        }
        private double GetLongitudeMin()
        {
            return System.Math.Min(this.Origin.Longitude, this.Ending.Longitude);
        }
        private double GetLatitudeMax()
        {
            return System.Math.Max(this.Origin.Latitude, this.Ending.Latitude);
        }
        private double GetLongitudeMax()
        {
            return System.Math.Max(this.Origin.Longitude, this.Ending.Longitude);
        }

        private void UpdateCenterLatitude(double latitude)
        {
            //UpdateCenter(new GeoLocation(this.Center.Longitude, latitude));
        }

        private void UpdateCenterLongitude(double longitude)
        {
            //UpdateCenter(new GeoLocation(longitude, this.Center.Latitude));
            //_centerLongitude = longitude;
            //var w = this.Width;
            //var h = this.Height;
            //var longitude = value - (w / 2);
            //var latitude = this.Center.Latitude - (h / 2);
            //_origins = new GeoLocation(longitude, latitude);
            //_ending = new GeoLocation(longitude + w, latitude + h);

            //OnPropertyChanged("Origin"); OnPropertyChanged("Ending");
        }

        protected bool IsUpdating = false;
        private void UpdateCenter()
        {
            IsUpdating = true;
            if (_center == null)
            {
                _center = new GeoLocation();
                _center.PropertyChanged += OnCenterPropertyChanged;
            }

            _centerLongitude = this.West + (this.Width / 2.0);
            _centerLatitude = this.South + (this.Height / 2.0);

            _center.Longitude = _centerLongitude;
            _center.Latitude = _centerLatitude;

            OnPropertyChanged("CenterLongitude");
            OnPropertyChanged("CenterLatitude");
            OnPropertyChanged("Center");  
            IsUpdating = false;
        }

        private void UpdateCenter(GeoLocation newCenter)
        {
            UpdateLocation(newCenter);
    
        }

        private void UpdateLocation()
        {
            UpdateLocation(_center);
        }
        public void UpdateLocation(GeoLocation newCenter)
        {
            var w = this.Width;
            var h = this.Height;

            var longitude = newCenter.Longitude - (w / 2);
            var latitude = newCenter.Latitude - (h / 2);

            _origins = new GeoLocation(longitude, latitude);
            _ending = new GeoLocation(longitude + w, latitude + h);

            //UpdateCenter(); 
            //OnPropertyChanged("CenterLatitude"); OnPropertyChanged("CenterLongitude");
            OnPropertyChanged("Origin"); OnPropertyChanged("Ending"); 
        }
        public void Shift(Point offset)
        {
            this.Shift(offset.X, offset.Y);
        }
        public void Shift(double longitude, double latitude)
        {
            var orginLongitude = this.GetLongitudeMin() + longitude;
            var endingLongitude = this.GetLongitudeMax() + longitude;
            var orginLatitude = this.GetLatitudeMin() + latitude;
            var endingLatitude = this.GetLatitudeMax() + latitude;
            this.Origin = new GeoLocation(orginLongitude, orginLatitude);
            this.Ending = new GeoLocation(endingLongitude, endingLatitude);
        }
        public void Shift(double offset)
        {
            this.Shift(offset, offset);
        }
        #endregion
        #region Converters

        /// <summary>
        /// Convert GeoRegion to Rect object
        /// </summary>
        /// <returns></returns>
        public Rect ToRect()
        {
            //this.Origin = GeoLocation.Min(origin, ending);
            //this.Ending = GeoLocation.Max(origin, ending); 
            return new Rect(this.West, this.South, this.Width, this.Height);

            //double xMin = System.Math.Min(this.Origin.Longitude, this.Ending.Longitude);
            //double yMin = System.Math.Min(this.Origin.Latitude, this.Ending.Latitude);
            //double xMax = System.Math.Max(this.Origin.Longitude, this.Ending.Longitude);
            //double yMax = System.Math.Max(this.Origin.Latitude, this.Ending.Latitude);
            //double w = System.Math.Abs(xMax - xMin);
            //double h = System.Math.Abs(yMax - yMin);
            //return new Rect(xMin, yMin + h, w, h);
            //return new Rect(xMin, yMin, w, h);
        }
        
        /// <summary>
        /// Converts shape of <see cref="Rect"/> to a List of <see cref="Point"/>
        /// </summary>
        public List<Point> ToPoints()
        {
            var rect = this.ToRect();
            return rect.ToPoints();
        }
        /// <summary>
        /// Converts shape of <see cref="Rect"/> to a List of List of <see cref="Point"/>
        /// </summary>
        public List<List<Point>> ToShapePoints()
        {
            var points = new List<List<Point>> { this.ToPoints() };
            return points;
        }

        /// <summary>
        /// Converts to Geographic Rect where bottom-left of the rectangle is southEast and top-right is northWest coordinate
        /// </summary>
        /// <returns></returns>
        public Rect ToGeoRect()
        {
            double xMin = System.Math.Min(this.Origin.Longitude, this.Ending.Longitude);
            double yMin = System.Math.Min(this.Origin.Latitude, this.Ending.Latitude);
            double xMax = System.Math.Max(this.Origin.Longitude, this.Ending.Longitude);
            double yMax = System.Math.Max(this.Origin.Latitude, this.Ending.Latitude);
            double w = System.Math.Abs(xMax - xMin);
            double h = System.Math.Abs(yMax - yMin);
            return new Rect(xMin, yMin + h, w, h);
        }
        public Rect ToWindowRect()
        {
            double xMin = System.Math.Min(this.Origin.Longitude, this.Ending.Longitude);
            double yMin = System.Math.Min(this.Origin.Latitude, this.Ending.Latitude);
            double xMax = System.Math.Max(this.Origin.Longitude, this.Ending.Longitude);
            double yMax = System.Math.Max(this.Origin.Latitude, this.Ending.Latitude);

            var min = GeoCalculator.GetWindowCoordinate(xMin, yMin);
            var max = GeoCalculator.GetWindowCoordinate(xMax, yMax);
            
            var size = System.Math.Max(max.X, max.Y );
            return new Rect(min.X, min.Y, size, size);
            //return new Rect(x, y, w, h);
        }
        public new string ToString()
        {
            return this.ToRect().ToString();
        }
        public static GeoRegion FromRect(Rect mapRect)
        {
            return new GeoRegion(mapRect);
        }
        public GeoRegion Clone()
        {
            return new GeoRegion(this.West, this.South, this.Width, this.Height);
        }
        #endregion
    }

    public static class GeoRegions
    {

        public static List<GeoRegion> KnownRegions
        {
            get
            {
                var list = new List<GeoRegion>
                {
                    WorldFullRegion, NorthAmericaRegion, SouthAmericaRegion, 
                    AfricaRegion, EuropeRegion, AsiaRegion, OceaniaRegion
                };
                return list;
            }
        }
        /// <summary>
        /// Finds a geo-region in the list of known geo-regions or returns geo-region of the world
        /// <remarks> </remarks>
        /// </summary>
        /// <param name="regionIdentifier"></param>
        public static GeoRegion FindGeoRegion(string regionIdentifier)
        {
            foreach (GeoRegion region in GeoRegions.KnownRegions)
            {
                if(region.Name.Equals(regionIdentifier))
                {
                    return region;
                }
            }
            return GeoRegions.WorldFullRegion;
        }
        public static GeoRegion EmptyRegion = new GeoRegion
        {
            Origin = new GeoLocation { Longitude = 0, Latitude = 0 },
            Ending = new GeoLocation { Longitude = 0, Latitude = 0 }
        };
        /// <summary>
        /// GeoRegion of the whole world
        /// </summary>
        public static GeoRegion WorldFullRegion = new GeoRegion(GeoGlobal.Worlds.ActualRegion.ToRect())
        {
            Name = "World",
        };
        /// <summary>
        /// GeoRegion of the whole world
        /// </summary>
        public static GeoRegion WorldSphericalMercatorRegion = new GeoRegion(GeoGlobal.WorldsMercator.MapRegion.ToRect())
        {
            Name = "World Spherical Mercator",
        };
        /// <summary>
        /// GeoRegion of the world without polar regions
        /// </summary>
        public static GeoRegion WorldNonPolarRegion = new GeoRegion
        {
            Name = "WorldNonPolar",
            Origin = new GeoLocation { Longitude = -180, Latitude = 60 },
            Ending = new GeoLocation { Longitude = 180, Latitude = -50 }
        };
        /// <summary>
        /// GeoRegion of the world without Antarctic continent
        /// </summary>
        public static GeoRegion WorldNonAntarcticRegion = new GeoRegion
        {
            Name = "WorldNonAntarcticRegion",
            Origin = new GeoLocation { Longitude = -116, Latitude = 84 },
            Ending = new GeoLocation { Longitude = 132, Latitude = -56 }
        };
        /// <summary>
        /// GeoRegion of the world with six main continents
        /// </summary>
        public static GeoRegion WorldSixMainContinents = new GeoRegion
        {
            Name = "WorldSixMainContinents",
            Origin = new GeoLocation { Longitude = -116, Latitude = 85 },
            Ending = new GeoLocation { Longitude = 132, Latitude = -60 }
        };
        /// <summary>
        /// GeoRegion of the world with seven continents
        /// </summary>
        public static GeoRegion WorldSevenContinents = new GeoRegion
        {
            Name = "WorldSevenContinents",
            Origin = new GeoLocation { Longitude = -116, Latitude = 70 },
            Ending = new GeoLocation { Longitude = 132, Latitude = -85 }
        };
        /// <summary>
        /// GeoRegion of the world with North America continent
        /// </summary>
        public static GeoRegion NorthAmericaRegion = new GeoRegion
        {
            Name = "North America",
            Origin = new GeoLocation { Longitude = -180, Latitude = 70 },
            Ending = new GeoLocation { Longitude = -50, Latitude = 5 }
        };
        /// <summary>
        /// GeoRegion of the world with South America continent
        /// </summary>
        public static GeoRegion SouthAmericaRegion = new GeoRegion
        {
            Name = "South America",
            Origin = new GeoLocation { Longitude = -130, Latitude = 15 },
            Ending = new GeoLocation { Longitude = 15, Latitude = -60 }
        };
        /// <summary>
        /// GeoRegion of the world with Africa continent
        /// </summary>
        public static GeoRegion AfricaRegion = new GeoRegion
        {
            Name = "Africa",
            Origin = new GeoLocation { Longitude = -20, Latitude = 40 },
            Ending = new GeoLocation { Longitude = 55, Latitude = -40 }
        };
        /// <summary>
        /// GeoRegion of the world with Europe continent
        /// </summary>
        public static GeoRegion EuropeRegion = new GeoRegion
        {
            Name = "Europe",
            Origin = new GeoLocation { Longitude = -25, Latitude = 70 },
            Ending = new GeoLocation { Longitude = 45, Latitude = 30 }
        };
        /// <summary>
        /// GeoRegion of the world with Asia continent 
        /// </summary>
        public static GeoRegion AsiaRegion = new GeoRegion
        {
            Name = "Asia",
            Origin = new GeoLocation { Longitude = 30, Latitude = 70 },
            Ending = new GeoLocation { Longitude = 180, Latitude = -10 }
        };
        /// <summary>
        /// GeoRegion of the world with Oceania continent
        /// </summary>
        public static GeoRegion OceaniaRegion = new GeoRegion
        {
            Name = "Oceania",
            Origin = new GeoLocation { Longitude = 110, Latitude = -10 },
            Ending = new GeoLocation { Longitude = 180, Latitude = -50 }
        };
        /// <summary>
        /// GeoRegion of the world with Australia continent
        /// </summary>
        public static GeoRegion AustraliaRegion = new GeoRegion
        {
            Name = "Australia",
            Origin = new GeoLocation { Longitude = 108, Latitude = 6 },
            Ending = new GeoLocation { Longitude = 154, Latitude = -46 }
        };
        /// <summary>
        /// GeoRegion of the world with United States
        /// </summary>
        public static GeoRegion UnitedStatesRegion = new GeoRegion
        {
            Name = "United States",
            Origin = new GeoLocation { Longitude = -130, Latitude = 50 },
            Ending = new GeoLocation { Longitude = -50, Latitude = 20 }
        };
        /// <summary>
        /// GeoRegion of the world with United States (lower 48 states)
        /// </summary>
        public static GeoRegion UnitedStatesLower48Region = new GeoRegion
        {
            Name = "United States Lower 48",
            Origin = new GeoLocation { Longitude = -115, Latitude = 55 },
            Ending = new GeoLocation { Longitude = -75, Latitude = 15 }
        };

        /// <summary>
        /// GeoRegion of the world with United States (lower 48 states)
        /// </summary>
        public static GeoRegion IndiaRegion = new GeoRegion
            {
                Origin = new GeoLocation { Longitude = 68, Latitude = 34.5 },
                Ending = new GeoLocation { Longitude = 98, Latitude = 1.5 }
            };
    }
}
