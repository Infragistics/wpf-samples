using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq; 
using IGSurfaceChart.Samples.Models; 
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework; 
using System.ComponentModel; 

namespace IGSurfaceChart.Samples.Data
{
    /// <summary>
    /// Interaction logic for LiveData.xaml
    /// </summary>
    public partial class BindingShapefiles : SampleContainer, INotifyPropertyChanged
    { 

        public BindingShapefiles()
        {
            InitializeComponent();
             
        }
        private void OnShapefileImportCompleted(object sender, AsyncCompletedEventArgs e)
        {
            //var shapePoints = new List<ShapePoint>();
            //var shapefile = sender as ShapefileConverter;
            //if (shapefile != null)
            //{
            //    Console.WriteLine(shapefile.Count);
            //    foreach (var item in shapefile)
            //    {
            //        var point = new ShapePoint();
            //        point.X = item.Points[0][0].X;
            //        point.Y = item.Points[0][0].Y;
            //        //point.Z = double.Parse(item.Fields["POLULATUION"].ToString());
            //        //point.Data = item.Fields;
            //        //shapePoints.Add(point);
            //    }
            //}
            
            //this.SurfaceChart.ItemsSource = shapePoints;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, ea);
            }
        }


    }


    public class ShapeCluster : DataPoint3D // provides x,y,z
    {
        public ShapeCluster()
        {
            Items = new List<DataPoint3D>();
        }
        /// <summary> Gets or sets Data </summary>
        public List<DataPoint3D> Items { get; set; }
        
        /// <summary> Gets or sets PropertyName </summary>
        public new double Z { get { return Items.Count == 0 ? 0.0 : Items.Sum(i => i.Z) / Items.Count; } }
    }

    public class ShapefileData : ShapefileConverter
    {
        /// <summary> Gets or sets Points </summary>
        public List<ShapeCluster> Data { get { return this.Dictionary.Values.ToList(); } }
       
        protected Dictionary<string, ShapeCluster> Dictionary { get; private set; }

        public ShapefileData()
        {
            Generate();
            
            this.ImportCompleted += OnShapefileImported;
        }

        private void Generate()
        {
            Dictionary = new Dictionary<string, ShapeCluster>();
            
            XMin = -180.0;
            XMax = 180.0; 
            YMin = -90.0;
            YMax = 90.0;
            for (var x = XMin; x <= XMax; x += 2.0)
            {
                for (var y = YMin; y <= YMax; y += 2.0)
                {
                    var key = GetLocation(x, y);

                    var cluster = new ShapeCluster();
                    cluster.X = x;
                    cluster.Y = y;
                    if (!Dictionary.ContainsKey(key))
                    {
                        Dictionary.Add(key, cluster);
                    }
                 }
            }
            Update();
        }
        private void Update()
        {
            if (string.IsNullOrEmpty(ZMemberPath)) return;

            foreach (var item in this)
            {
                var point = new DataPoint3D();
                point.X = item.Points[0][0].X;
                point.Y = item.Points[0][0].Y;
                point.Z = double.Parse(item.Fields[ZMemberPath].ToString());

                this[point].Items.Add(point);  
            }
            OnPropertyChanged("Data"); 
        }
        public double XMin { get; set; }
        public double XMax { get; set; }
        public double YMin { get; set; }
        public double YMax { get; set; }

        public double XStep { get { return (XMax - XMin) / Math.Sqrt(MaxPoints); } }
        public double YStep { get { return (YMax - YMin) / Math.Sqrt(MaxPoints); } }

        private string GetLocation(double x, double y)
        {
            var xi = (int)Math.Floor(x / 2.0);
            var yi = (int)Math.Floor(y / 2.0);
            return xi + "" + yi;
        }
        public ShapeCluster this[DataPoint3D point]
        {
            get
            {
                var key = GetLocation(point.X, point.Y);
                return this[key];
            }
            set
            {
                var key = GetLocation(point.X, point.Y);
                this[key] = value;
            }
        }
        public ShapeCluster this[double x, double y]
        {
            get
            {
                var key = GetLocation(x,y); 
                return this[key];
            }
            set
            {
                var key = GetLocation(x, y);
                this[key] = value;
            }
        }
        public ShapeCluster this[string key]
        {
            get
            {
                if (this.Dictionary.ContainsKey(key))
                {
                    return this.Dictionary[key];
                }
                return null;
            }
            set
            { 
                if (this.Dictionary.ContainsKey(key))
                { 
                    this.Dictionary[key] = value; 
                }
            }
        }

        private double _maxPoints = 1600;
        /// <summary> Gets or sets Resolution </summary>
        public double MaxPoints
        {
            get { return _maxPoints; }
            set 
            {
                if (_maxPoints == value) return; _maxPoints = value;
                OnPropertyChanged("MaxPoints");
                Generate();
            }
        }
        private string _zMemberPath;
        /// <summary> Gets or sets ZMemberPath </summary>
        public string ZMemberPath
        {
            get { return _zMemberPath;}
            set { if (_zMemberPath == value) return; _zMemberPath = value; OnPropertyChanged("ZMemberPath"); Update(); }
        }

        void OnShapefileImported(object sender, AsyncCompletedEventArgs e)
        {
            Update();
        }

        

        public void Add()
        {
            
        }
    }
}
