using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace IGExtensions.Common.Models
{
    /// <summary>
    /// Represents single shape built from a list of <see cref="Point"/>s
    /// </summary>
    public class ShapePoints : List<Point>
    {
        public ShapePoints()
        {
            
        }
        public ShapePoints(IEnumerable<Point> points)
        {
            this.AddRange(points);
        }

        public double ShapeArea { get { return this.ShapeRect.Area(); } }
        public Rect ShapeRect { get { return this.ToShapeRect(); } }
        public Point ShapeCenter { get { return this.ShapeRect.Center(); } }
        public bool IsShapeValid { get { return this.ShapeRect.IsValid(); } }
    }
    /// <summary>
    /// Represents a list of shapes built from <see cref="ShapePoints"/>
    /// </summary>
    public class ShapesList : List<ShapePoints>, INotifyPropertyChanged 
    {
        public ShapesList()
        {
           // _points = new List<List<Point>>();
        }
        //public ShapesList(IEnumerable<Point> points)
        //{
        //    this.Add(points);
        //}
        public ShapesList(ShapePoints shapePoints)
        {
            System.Diagnostics.Debug.WriteLine("ShapesList(points)");
            this.Add(shapePoints);
          //  _points = shapePoints.ToShapes(); // GetPoints();
        }
     
        #region Properties
        //private List<List<Point>> _points;
        ///// <summary>
        ///// Gets or sets Points property 
        ///// </summary>
        //public List<List<Point>> Points
        //{
        //    get { return GetPoints(); }
        //    private set { if (_points == value) return; _points = value; this.Update(); OnPropertyChanged("Points"); }
        //}
        // public List<List<Point>> ShapePoints { get { return this.Points; } }

        //private List<List<Point>> GetPoints()
        //{
        //    if (this.Count != _points.Count)
        //    {
        //        System.Diagnostics.Debug.WriteLine("ShapesList.Generating points " + this.Count);
        //        _points = new List<List<Point>>();
        //        foreach (var shape in this)
        //        {
        //            _points.Add(shape);
        //        }
        //    }
        //    return _points;
        //    //_points = new List<List<Point>>();
        //    //foreach (var shape in this)
        //    //{
        //    //    _points.Add(shape.ToList());
        //    //}
        //    //return _points;
        //} 
        #endregion
        //public void Update()
        //{
        //    System.Diagnostics.Debug.WriteLine("ShapesList.Update()");
        //    this.Clear();
        //    var shapes = new List<ShapePoints>();
        //    foreach (var shape in this.Points)
        //    {
        //        shapes.Add(new ShapePoints(shape));
        //    }
        //    this.AddRange(shapes);
        //}
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the PropertyChanged event for provided property name
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.WriteLine("ShapesList.OnPropertyChanged " + propertyName);
            //DebugManager.LogTrace("PropertyChanging: " + this.GetType() + "." + propertyName);
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
    /// Represents single geographic shape built from a list of <see cref="GeoPoint"/>s
    /// <para>This is an observable model and it implements <see cref="INotifyPropertyChanged"/> interface on all its properties</para>
    /// </summary>
    public class GeoShapePoints : List<GeoPoint>, IGeoNavigational, INotifyPropertyChanged //: List<Point> ObservableModel
    {
        #region Constructor
        public GeoShapePoints()
            //: this(new ShapePoints())
        {
            _shapePoints = new ShapePoints();
            //this.CollectionChanged += OnCollectionChanged; 
        }
        public GeoShapePoints(IEnumerable<Point> points)
            : this(new ShapePoints(points))
        { }
        public GeoShapePoints(ShapePoints points)
        {
            this.ShapePoints = points;
            //foreach (var point in points)
            //{
            //    this.Add(new GeoPoint(point));
            //}
        }
        #endregion
        #region Properties
        
        private double _shapeArea;
        /// <summary>
        /// Gets Area of the shape points  
        /// </summary>
        public double ShapeArea
        {
            get { return _shapeArea; }
            private set { if (_shapeArea == value) return; _shapeArea = value; OnPropertyChanged("ShapeArea"); }
        }
        private bool _isShapeValid;
        /// <summary>
        /// Gets or sets IsShapeValid property 
        /// </summary>
        public bool IsShapeValid
        {
            get { return _isShapeValid; }
            set { if (_isShapeValid == value) return; _isShapeValid = value; OnPropertyChanged("IsShapeValid"); }
        }
        private Rect _shapeRect;
        /// <summary>
        /// Gets bounding Rect of the shape points 
        /// </summary>
        public Rect ShapeRect
        {
            get { return _shapeRect; }
            private set { if (_shapeRect == value) return; _shapeRect = value; OnPropertyChanged("ShapeRect"); }
        }
        private GeoPoint _shapeCenter;
        /// <summary>
        /// Gets center of the shape points 
        /// </summary>
        public GeoPoint ShapeCenter
        {
            get { return _shapeCenter; }
            private set { if (_shapeCenter == value) return; _shapeCenter = value; OnPropertyChanged("ShapeCenter"); }
        }
        private ShapePoints _shapePoints;
        /// <summary>
        /// Gets or sets Points of the shape 
        /// </summary>
        public ShapePoints ShapePoints
        {
            get { return GetShapePoints(); }
            private set { if (_shapePoints == value) return; _shapePoints = value; Update(); OnPropertyChanged("ShapePoints"); }
        }
        public ShapePoints Points { get { return this.ShapePoints; } }
      
        #endregion
        #region Methods
        private ShapePoints GetShapePoints()
        {
            if (this.Count != _shapePoints.Count)
            {
                _shapePoints = new ShapePoints();
                foreach (var point in this)
                {
                    _shapePoints.Add(point.ToPoint());
                }
            }
            return _shapePoints;
        }
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //var gp = e.NewItems[0] as GeoPoint;
            //System.Diagnostics.Debug.WriteLine("GeoShape[" + this.Count + "] OnCollectionChanged" + e.Action + " " + e.NewItems.Count + " " + gp.ToPoint().ToString());
            if (!this.IsUpdating)
            {
                this.IsUpdating = true;
                System.Diagnostics.Debug.WriteLine("GeoShape[" + this.Count + "] Updating Points");
                var points = new ShapePoints();
                foreach (var point in this)
                {
                    points.Add(point.ToPoint());
                }
                this.ShapePoints = points;
                this.IsUpdating = false;
                System.Diagnostics.Debug.WriteLine("GeoShape[" + this.Count + "] Updating Points finished");
            }
        } 
        protected bool IsUpdating;
        public void Update()
        {
            this.ShapeRect = this.ShapePoints.ToShapeRect();
            this.ShapeCenter = new GeoPoint(this.ShapeRect.Center());
            this.ShapeArea = this.ShapeRect.Area();
            
            if (this.IsUpdating) return;
            this.IsUpdating = true;
            this.Clear();
            foreach (var point in this.ShapePoints)
            {
                this.Add(new GeoPoint(point));
            }
            System.Diagnostics.Debug.WriteLine("GeoShape[" + this.Count + "] Updating collection finished");
            this.IsUpdating = false;
        }
        public Rect NavigationRect(Point geoPosition)
        {
            var shape = this.ShapePoints.FindShapeContainer(geoPosition);
            if (shape.IsShapeEmpty())
                return Rect.Empty;
            return shape.ToShapeRect();
        }
        #endregion
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the PropertyChanged event for provided property name
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            //System.Diagnostics.Debug.WriteLine("GeoShape[" + this.Count + "] OnPropertyChanged " + propertyName);
            //DebugManager.LogTrace("PropertyChanging: " + this.GetType() + "." + propertyName);
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //PropertyChangedEventHandler handler = this.PropertyChanged;
            //if (handler != null)
            //    handler(sender, e);
            //PropertyChangedEventHandler handler = this.PropertyChanged;
            if (this.PropertyChanged != null)
                this.PropertyChanged(sender, e);
        }
        #endregion

    }

    /// <summary>
    /// Represents a list of geographic shapes built from <see cref="GeoShapePoints"/>
    /// <para>This is an observable model and it implements <see cref="INotifyPropertyChanged"/> interface on all its properties</para>
    /// </summary>
    public class GeoShapesList : List<GeoShapePoints>, IGeoNavigational, INotifyPropertyChanged // List<GeoShape>, List<List<Point>> ObservableModel
    {
        public GeoShapesList() //: this(new List<List<Point>>())
        {
            _points = new List<List<Point>>();
           // this.CollectionChanged += OnCollectionChanged;
             //this.Points = new List<List<Point>>();
        }
        public GeoShapesList(List<List<Point>> points)
        {
            this.ShapePoints = points;
             //foreach (var list in points)
            //{
            //    this.Add(new GeoShape(list));
            //}
            //this.Points = points;
        }
        
        public Rect NavigationRect(Point geoPosition)
        {
            var shape = this.ShapePoints.FindShapeContainer(geoPosition);
            if (shape.IsShapeEmpty())
                return Rect.Empty;
            return shape.ToShapeRect();
        }
        public void LoadPoints(List<List<Point>> points)
        {
            //this.Clear();
            //foreach (var list in points)
            //{
            //    this.Add(new GeoShape(list));
            //}
            this.ShapePoints = points;
          
        }
        public List<List<Point>> ToPoints()
        {
            //var points = new List<List<Point>>();
            //foreach (var shape in this)
            //{
            //    points.Add(shape.Points);
            //}
            return _points;
        }

        #region Properties
        //public string Label { get; set; }

        public GeoShapePoints BiggestShape { get { return new GeoShapePoints(this.ShapePoints.GetBiggestShape()); } }
        public GeoShapePoints SmallestShape { get { return new GeoShapePoints(this.ShapePoints.GetSmallestShape()); } }
    
        private List<List<Point>> _points = new List<List<Point>>();
        /// <summary>
        /// Gets or sets Points of the shape 
        /// </summary>
        public List<List<Point>> ShapePoints
        {
            get {
                return _points; //GetShapePoints(); 
            }
            private set { if (_points == value) return; _points = value; Update(); OnPropertyChanged("ShapePoints"); }
        }
        public List<List<Point>> Points { get { return this.ShapePoints;  } }

        private List<List<Point>> GetShapePoints()
        {
            if (this.Count != _points.Count)
            {
                _points = new List<List<Point>>();
                foreach (var shape in this)
                {
                    _points.Add(shape.ShapePoints);
                }
            }
            return _points;
        }

        private double _area;
        /// <summary>
        /// Gets Area of the shape points  
        /// </summary>
        public double ShapeArea
        {
            get { return _area; }
            private set { if (_area == value) return; _area = value; OnPropertyChanged("ShapeArea"); }
        }
        private Rect _rect;
        /// <summary>
        /// Gets bounding Rect of the shape points 
        /// </summary>
        public Rect ShapeRect
        {
            get { return _rect; }
            private set { if (_rect == value) return; _rect = value; OnPropertyChanged("ShapeRect"); }
        }
        private Point _center;
        /// <summary>
        /// Gets center of the shape points 
        /// </summary>
        public Point ShapeCenter
        {
            get { return _center; }
            private set { if (_center == value) return; _center = value; OnPropertyChanged("ShapeCenter"); }
        }
        #endregion
        protected bool IsUpdating;
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("GeoShapesList. OnCollectionChanged" + e.Action + " " + e.NewItems.Count + e.NewItems[0]);
            if (!this.IsUpdating)
            {
                this.IsUpdating = true;
                 System.Diagnostics.Debug.WriteLine("GeoShapesList. Updating points");
                var points = new List<List<Point>>();
                foreach (var shape in this)
                {
                    points.Add(shape.ShapePoints);
                }
                this.ShapePoints = points;
                this.IsUpdating = false;
                System.Diagnostics.Debug.WriteLine("GeoShapesList. Updating points finished");
            }
        }
        public void Update()
        {
            this.ShapeRect = this.ShapePoints.ToShapeRect();
            this.ShapeCenter = this.ShapeRect.Center();
            this.ShapeArea = this.ShapePoints.GetShapeArea();
            
            if (this.IsUpdating) return;

            this.IsUpdating = true;
            //this.Clear();

            //foreach (var shape in this.ShapePoints)
            //{
            //    this.Add(new GeoShapePoints(shape));
            //}
            //if (this.ShapePoints.Count > 0 && this.ShapePoints[0].Count > 0)
            //    System.Diagnostics.Debug.WriteLine("GeoShapesList[" + this.Count + "] " + this[0].Count + "Updating collection finished");
            this.IsUpdating = false;
        }

        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the PropertyChanged event for provided property name
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            //System.Diagnostics.Debug.WriteLine("GeoShapesList. OnPropertyChanged " + propertyName);
            //DebugManager.LogTrace("PropertyChanging: " + this.GetType() + "." + propertyName);
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
    /// Provides extension methods for working with a list of <see cref="Point"/>s and a list of lists of <see cref="Point"/>s/>
    /// </summary>
    public static class GeoShapesListEx
    {

        #region Area Methods
        /// <summary>
        /// Calculates approx. shape area of all elements of shapes
        ///  </summary>
        public static double GetShapeArea(this List<List<Point>> shapes)
        {
            var area = 0.0;
            foreach (var shapePoints in shapes)
            {
                area += shapePoints.GetShapeArea();
            }
            return area;
        }
        /// <summary>
        /// Calculates approx. shape area of the shape
        ///  </summary>
        public static double GetShapeArea(this List<Point> shapePoints)
        {
            var area = 0.0;
            var rect = shapePoints.ToShapeRect();
            area += rect.Area();
            return area;
        } 
        #endregion
        #region Validation Methods
        public static bool IsShapeEmpty(this List<Point> shapes)
        {
            return shapes == null || shapes.Count <= 2;
        }
        public static bool ContainsShapePoint(this List<Point> polygon, Point point)
        {
            bool result = false;
            int j = polygon.Count - 1;
            for (int i = 0; i < polygon.Count; i++)
            {
                if (polygon[i].Y < point.Y && polygon[j].Y >= point.Y || polygon[j].Y < point.Y && polygon[i].Y >= point.Y)
                {
                    if (polygon[i].X + (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) * (polygon[j].X - polygon[i].X) < point.X)
                    {
                        result = !result;
                    }
                }
                j = i;
            }
            return result;
        }
        public static bool ContainsShapePoint(this List<List<Point>> shapes, Point point)
        {
            foreach (var shapePoints in shapes)
            {
                if (shapePoints.ContainsShapePoint(point))
                    return true;
            }
            return false;
        }

        #endregion
        #region Find Methods
        public static List<Point> FindShapeContainer(this List<List<Point>> shapes, Point point)
        {
            foreach (var shapePoints in shapes)
            {
                if (shapePoints.ContainsShapePoint(point))
                    return shapePoints;
            }
            return new List<Point>();
        }
        public static List<Point> FindShapeContainer(this List<Point> shape, Point point)
        {
            if (shape.ContainsShapePoint(point))
                return shape;

            return new List<Point>();
        }
        /// <summary>
        /// Finds the smallest list of shape points from all elements of shapes
        /// </summary>
        public static List<Point> GetSmallestShape(this List<List<Point>> shapes)
        {
            var area = double.MaxValue;
            var smallestShape = new List<Point>();
            if (shapes.Count == 1)
                return shapes[0];

            foreach (var shapePoints in shapes)
            {
                var region = shapePoints.ToShapeRect();
                if (region.Area() < area)
                {
                    area = region.Area();
                    smallestShape = shapePoints;
                }
            }
            return smallestShape;
        }
        /// <summary>
        /// Finds the biggest list of shape points from all elements of shapes
        /// </summary>
        public static List<Point> GetBiggestShape(this List<List<Point>> shapes)
        {
            var area = double.MinValue;
            var biggestShape = new List<Point>();
            if (shapes.Count == 1)
                return shapes[0];

            foreach (var shapePoints in shapes)
            {
                var region = shapePoints.ToShapeRect();
                if (region.Area() > area)
                {
                    area = region.Area();
                    biggestShape = shapePoints;
                }
            }
            return biggestShape;
        }

        #endregion
        #region Converstion Methods
        public static List<List<Point>> ToShapes(this List<Point> shapePoints)
        {
            var shapes = new List<List<Point>>();
            shapes.Add(shapePoints);
            return shapes;
        }
        /// <summary>
        /// Converts list of points to shape bounding rect
        /// </summary>
        public static Rect ToShapeRect(this List<Point> shapePoints)
        {
            var xMin = double.MaxValue;
            var xMax = double.MinValue;
            var yMin = double.MaxValue;
            var yMax = double.MinValue;

            foreach (var point in shapePoints)
            {
                xMin = System.Math.Min(xMin, point.X);
                xMax = System.Math.Max(xMax, point.X);
                yMin = System.Math.Min(yMin, point.Y);
                yMax = System.Math.Max(yMax, point.Y);
            }
            if (xMin == double.MaxValue || xMax == double.MinValue ||
                yMin == double.MaxValue || yMax == double.MinValue)
                return Rect.Empty;

            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }
        /// <summary>
        /// Converts shape points to shape bounding rect
        /// </summary>
        public static Rect ToShapeRect(this List<List<Point>> shapes)
        {
            var xMin = double.MaxValue;
            var xMax = double.MinValue;
            var yMin = double.MaxValue;
            var yMax = double.MinValue;

            foreach (var shape in shapes)
            {
                foreach (var point in shape)
                {
                    xMin = System.Math.Min(xMin, point.X);
                    xMax = System.Math.Max(xMax, point.X);
                    yMin = System.Math.Min(yMin, point.Y);
                    yMax = System.Math.Max(yMax, point.Y);
                }
            }
            if (xMin == double.MaxValue || xMax == double.MinValue ||
                yMin == double.MaxValue || yMax == double.MinValue)
                return Rect.Empty;

            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        #endregion
    }

}