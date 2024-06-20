using System;
using System.Collections.Generic; 
using System.Linq; 
using IGSurfaceChart.Samples.Models; 

namespace IGSurfaceChart.Samples
{ 
    /// <summary>
    /// Represents 3D data loaded from a shapefile and then flattened and 
    /// sampled to a regular grid of <see cref="DataPoint3D"/> 
    /// </summary>
    public class ShapefileSampler : ShapefileBase
    {
        /// <summary> Gets 3D Data Points </summary>
        public List<ShapefileCluster> Data { get { return this.Dictionary.Values.ToList(); } }

        protected Dictionary<string, ShapefileCluster> Dictionary { get; private set; }

        public ShapefileSampler()
        {
            Dictionary = new Dictionary<string, ShapefileCluster>();
        }
        
        public override void UpdateRange()
        {
            base.UpdateRange();
            
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            if (!IsReady()) return; 

            Dictionary.Clear();
            for (var x = XMin; x <= XMax; x += XStep)
            {
                for (var y = YMin; y <= YMax; y += YStep)
                {
                    var key = GetLocation(x, y);

                    var cluster = new ShapefileCluster { X = x, Y = y };
                    if (!Dictionary.ContainsKey(key))
                    {
                        Dictionary.Add(key, cluster);
                    }
                }
            }
        }

        public override void UpdateData()
        {
            if (!IsReady()) return; 
         
            foreach (var cluster in Dictionary.Values)
            {
                cluster.Items.Clear();
            }

            var locations = new Dictionary<string,bool>();
            var total = 0;
            // flatten and sample shapefile to a grid with Max Points
            foreach (var record in this)
            {
                var z = double.Parse(record.Fields[ZMemberPath].ToString());
                foreach (var shape in record.Points)
                {
                    foreach (var item in shape)
                    {
                        total++;

                        var point = new DataPoint3D();
                        point.Z = z;
                        point.X = item.X;
                        point.Y = item.Y;
                        var loc = point.Location.ToString();
                        
                        // skip duplicated points
                        if (!locations.ContainsKey(loc))
                        {
                            locations.Add(loc, true);
                            this[point].Items.Add(point);
                        }
                         
                    }
                } 
            }
            OnPropertyChanged("Data");
        }
         
        protected double XStep { get { return Math.Floor((XMax - XMin) / Math.Sqrt(MaxPoints)); } }
        protected double YStep { get { return Math.Floor((YMax - YMin) / Math.Sqrt(MaxPoints)); } }

        protected string GetLocation(double x, double y)
        {
            var xi = (int)Math.Floor(x / XStep);
            var yi = (int)Math.Floor(y / YStep);
            return xi + "," + yi;
        }
        protected ShapefileCluster this[DataPoint3D point]
        {
            get
            {
                var key = GetLocation(point.X, point.Y);
                return this[key];
            }
            set
            {
                var key = GetLocation(point.X, point.Y);
                if (this[key] == null) return;
                this[key] = value;
            }
        }
        protected ShapefileCluster this[double x, double y]
        {
            get
            {
                var key = GetLocation(x, y);
                return this[key];
            }
            set
            {
                var key = GetLocation(x, y);
                if (this[key] == null) return;

                this[key] = value;
            }
        }
        protected ShapefileCluster this[string key]
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

        private double _maxPoints = 900;
        /// <summary> Gets or sets Resolution </summary>
        public double MaxPoints
        {
            get { return _maxPoints; }
            set
            {
                if (_maxPoints == value) return; _maxPoints = value;
                OnPropertyChanged("MaxPoints");
                UpdateRange();
                UpdateData();
            }
        }
        
    }

    public class ShapefileCluster : DataPoint3D // provides x,y,z
    {
        public ShapefileCluster()
        {
            Items = new List<DataPoint3D>();
        }
        /// <summary> Gets or sets clustered points </summary>
        public List<DataPoint3D> Items { get; set; }

        /// <summary> Gets or sets Z value </summary>
        public new double Z
        {
            get { return Items.Count == 0 ? 0 : Items.Sum(i => i.Z) / Items.Count; }
        }

        public new string ToString()
        {
            return base.ToString() + string.Format(", C {0:0}", Items.Count);
        }
    }


}