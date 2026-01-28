using System.Collections.Specialized;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System;
using System.Linq;
using System.Collections;
using System.Windows;
using System.Collections.ObjectModel;
using Infragistics.Controls.Charts;

namespace IGShapeChart.Samples
{
    public partial class BindingShapeChart : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingShapeChart()
        {
            InitializeComponent();
            
            // NOTE: excluding some List properties from ploting in ShapeChart
            Chart1.ExcludedProperties = new string[] { "Count", "Capacity", "Length" };
            Chart2.ExcludedProperties = new string[] { "Count", "Capacity", "Length" };


            this.DataContext = new ShapefilesViewModel(); 
        }
    } 

    public class ShapefilesViewModel : ObservableModel
    {
        KeyValuePair<string, IEnumerable> multiFile;
        public ShapefilesViewModel()
        {
            ChartTypes = new ObservableCollection<ShapeChartType>();
            ChartTypes.Add(ShapeChartType.Automatic);
            ChartTypes.Add(ShapeChartType.ScatterArea);
            ChartTypes.Add(ShapeChartType.ScatterBubble);
            ChartTypes.Add(ShapeChartType.ScatterHighDensity);
            ChartTypes.Add(ShapeChartType.ScatterLine);
            ChartTypes.Add(ShapeChartType.ScatterPoint);
            ChartTypes.Add(ShapeChartType.ScatterSpline);
            ChartTypes.Add(ShapeChartType.ShapePolygon);
            ChartTypes.Add(ShapeChartType.ShapePolyline);
            ChartTypes.Add(ShapeChartType.ScatterContour);

            DataSources = new Dictionary<string, IEnumerable>();
            LoadShapefiles();

            DataSourceNames = new ObservableCollection<string>();
            foreach(string key in DataSources.Keys)
            {
                DataSourceNames.Add(key);
            }
        }
        public Dictionary<string, IEnumerable> DataSources { get; set; }
        
        public ObservableCollection<string> DataSourceNames { get; set; }                
        public ObservableCollection<ShapeChartType> ChartTypes { get; set; }       

        private ShapeChartType _chartType;
        public ShapeChartType ChartType
        {
            get { return _chartType; }
            set
            {
                if(_chartType != value)
                {
                    _chartType = value;
                    OnPropertyChanged("ChartType");
                }
            }
        }

        private IEnumerable _SelectedData;
        /// <summary> Gets or sets SelectedData </summary>
        public IEnumerable SelectedData
        {
            get { return _SelectedData; }
            set { if (_SelectedData == value) return; _SelectedData = value; OnPropertyChanged("SelectedData"); }
        }

        private string _SelectedName;
        /// <summary> Gets or sets SelectedName </summary>
        public string SelectedName
        {
            get { return _SelectedName; }
            set { if (_SelectedName == value) return; _SelectedName = value; OnPropertyChanged("SelectedName"); }
        }

        protected override void OnPropertyChanged(string name)
        {
            base.OnPropertyChanged(name);
            
            if(name == "ChartType")
            {
                if(this.ChartType == ShapeChartType.ScatterContour)
                {
                    if (DataSourceNames.Contains(multiFile.Key))
                    {
                        DataSourceNames.Remove(multiFile.Key);
                    }
                }
                else
                {
                    if (!DataSourceNames.Contains(multiFile.Key))
                    {
                        DataSourceNames.Add(multiFile.Key);
                    }
                }
            }
            
            if (name == "SelectedName")
            {
                if(this.SelectedName.Contains("Multiple"))
                {
                    if (ChartTypes.Contains(ShapeChartType.ScatterContour))
                    {
                        ChartTypes.Remove(ShapeChartType.ScatterContour);
                    }
                }
                else
                {
                    if (!ChartTypes.Contains(ShapeChartType.ScatterContour))
                    {
                        ChartTypes.Add(ShapeChartType.ScatterContour);
                    }
                }

                this.SelectedData = this.DataSources[SelectedName];
            }
        }
        protected List<CountryData> WorldPopulation;
        protected Collection<CountryData> WorldDensity;
        protected CountryData[] WorldArea; 

        protected ShapefileConverter WorldShapefile;
        protected ShapefileConverter AirportShapefile;
        protected ShapefileConverter AirplaneOutline;
        protected ShapefileConverter AirplaneSeats;

        private List<ShapefileItem> _WorldBoarder;
        public List<ShapefileItem> WorldBoarder
        {
            get { return _WorldBoarder; }
            set { if (_WorldBoarder == value) return; _WorldBoarder = value; OnPropertyChanged("WorldBoarder"); }
        }

        private List<List<ShapefileItem>> _AirplaneShapes; 
        public List<List<ShapefileItem>> AirplaneShapes
        {
            get { return _AirplaneShapes; }
            set { if (_AirplaneShapes == value) return; _AirplaneShapes = value; OnPropertyChanged("AirplaneShapes"); }
        }

        public void LoadShapefiles()
        {
            var path = "/IGShapeChart;component/shapefiles/world-countries";
            WorldShapefile = new ShapefileConverter();
            WorldShapefile.ImportCompleted += OnWorldShapefileImported;
            WorldShapefile.ShapefileSource = new Uri(path + ".shp", UriKind.RelativeOrAbsolute);
            WorldShapefile.DatabaseSource  = new Uri(path + ".dbf", UriKind.RelativeOrAbsolute);
            DataSources.Add("World Shapefile - ShapefileConverter", WorldShapefile);
            
            // airports
            path = "/IGShapeChart;component/shapefiles/world-airports"; 
            AirportShapefile = new ShapefileConverter();
            AirportShapefile.ImportCompleted += OnAirportShapefileImported;
            AirportShapefile.ShapefileSource = new Uri(path + ".shp", UriKind.RelativeOrAbsolute);
            AirportShapefile.DatabaseSource  = new Uri(path + ".dbf", UriKind.RelativeOrAbsolute);
            
            // airplane
            path = "/IGShapeChart;component/shapefiles/airplane-shape";
            AirplaneOutline = new ShapefileConverter();
            AirplaneOutline.ShapefileSource = new Uri(path + ".shp", UriKind.RelativeOrAbsolute);
            AirplaneOutline.DatabaseSource  = new Uri(path + ".dbf", UriKind.RelativeOrAbsolute);

            path = "/IGShapeChart;component/shapefiles/airplane-seats";
            AirplaneSeats = new ShapefileConverter();
            AirplaneSeats.ImportCompleted += OnAirplaneShapefileImported;
            AirplaneSeats.ShapefileSource = new Uri(path + ".shp", UriKind.RelativeOrAbsolute);
            AirplaneSeats.DatabaseSource  = new Uri(path + ".dbf", UriKind.RelativeOrAbsolute);

        } 
     
        private void OnAirplaneShapefileImported(object sender, AsyncCompletedEventArgs e)
        {
            var shapefile1 = new List<ShapefileItem>();
            foreach (var record in AirplaneOutline)
            {
                var item = new ShapefileItem();
                item.ShapeType = ShapeType.Polygon;
                item.Fields = record.Fields;
                item.Points = record.Points;
                item.Points.Offset(-305, 100);
                shapefile1.Add(item);
            }
            var shapefile2 = new List<ShapefileItem>();

            var seatsDictionary = new Dictionary<string, List<ShapefileItem>>();

            foreach (var record in AirplaneSeats)
            {
                var item = new ShapefileItem();
                item.ShapeType = ShapeType.Polygon;
                item.Fields = record.Fields;   
                item.Points = record.Points;
                item.Points.Offset(-305, 100);

                var key = record.Fields["CLASS"].ToString();
                if (!seatsDictionary.ContainsKey(key))
                {
                    seatsDictionary.Add(key, new List<ShapefileItem>());
                }
                seatsDictionary[key].Add(item);
            }

            AirplaneShapes = new List<List<ShapefileItem>>();
            AirplaneShapes.Add(shapefile1);
            AirplaneShapes.AddRange(seatsDictionary.Values);
            
        }

        private void OnAirportShapefileImported(object sender, AsyncCompletedEventArgs e)
        {            
            var shapefile1 = new List<ShapefileItem>();
            foreach (var record in WorldShapefile)
            {
                var item = new ShapefileItem();
                item.ShapeType = ShapeType.PolyLine;
                item.Fields = record.Fields;
                item.Points = record.Points; 
                shapefile1.Add(item);
            }
            var shapefile2 = new List<ShapefileItem>();
            foreach (var record in AirportShapefile)
            {
                var item = new ShapefileItem();
                item.ShapeType = ShapeType.Point;

                item.X = record.Points[0][0].X;
                item.Y = record.Points[0][0].Y;
                item.Value = double.Parse(record.Fields["ELEVATION"].ToString());

                item.Points = record.Points;
                shapefile2.Add(item);
            }

            var multipleShapefiles = new List<List<ShapefileItem>>();
            multipleShapefiles.Add(shapefile1);
            multipleShapefiles.Add(shapefile2);

            multiFile = new KeyValuePair<string, IEnumerable>("Multiple Shapefiles - List<Shapefile>", multipleShapefiles);
            DataSources.Add(multiFile.Key, multiFile.Value);

        }

        private void OnWorldShapefileImported(object sender, AsyncCompletedEventArgs e)
        {                          
            var regionDictionary = new Dictionary<string, List<CountryShape>>();
            WorldBoarder = new List<ShapefileItem>();
            WorldPopulation = new List<CountryData>();
            WorldDensity = new Collection<CountryData>();
            WorldArea = new CountryData[WorldShapefile.Count];
            
            var names = WorldShapefile.Select(record => record.Fields["REGION"].ToString()).Distinct();
             
            // create example of Flat Data by extracting numeric values from each record of a shapefile
            for (var i = 0; i < WorldShapefile.Count; i++)
            {
                var record = WorldShapefile[i];
                var recordCenter = record.Points.GetRectMax().GetCenter();

                var name = record.Fields["NAME"].ToString();
                var population = double.Parse(record.Fields["POP2005"].ToString());
                var area = double.Parse(record.Fields["AREA"].ToString());
                var density = double.IsNaN(area) || area == 0 ? 0 : population / area;
                
                var item = new ShapefileItem();
                item.ShapeType = ShapeType.PolyLine;
                item.Points = record.Points;
                WorldBoarder.Add(item);

                // add item to a list of CountryData objects
                var countryPop = new CountryData();
                countryPop.Name = name;
                countryPop.X = recordCenter.X;
                countryPop.Y = recordCenter.Y;
                countryPop.Value = population;
                WorldPopulation.Add(countryPop);

                // add item to an array of CountryData objects
                var countryArea = new CountryData();
                countryArea.Name = name;
                countryArea.X = recordCenter.X;
                countryArea.Y = recordCenter.Y;
                countryArea.Value = area;
                WorldArea[i] = countryArea;
                
                // add item to a collection of CountryData objects
                var countryDensity = new CountryData();
                countryDensity.Name = name;
                countryDensity.X = recordCenter.X;
                countryDensity.Y = recordCenter.Y;
                countryDensity.Value = density;
                WorldDensity.Add(countryDensity);

                // add item to a dictionary of list of CountryShape objects
                var country = new CountryShape();
                country.Name = name;
                country.Points = record.Points;
                country.Value = area;
                
                var regionName = record.Fields["REGION"].ToString();                
                if (regionName.Contains("Caribbean")) regionName = "North America";                
                if (regionName.Contains("Africa")) regionName = "Africa";
                if (regionName == "Pacific") continue;
               
                if (!regionDictionary.ContainsKey(regionName))
                {
                    regionDictionary.Add(regionName, new List<CountryShape>());
                }
                regionDictionary[regionName].Add(country);                                
            }                      

            // adding multiple data source with world boarders data as a reference for:
            DataSources.Add("World Area - CountryData[]", new List<IEnumerable> { WorldBoarder, WorldArea });
            DataSources.Add("World Density - Collection<CountryData>", new List<IEnumerable> { WorldBoarder, WorldDensity });
            DataSources.Add("World Population - List<CountryData>", new List<IEnumerable> { WorldBoarder, WorldPopulation });

            // create example of Hierarchical Data
            var regions = new List<List<CountryShape>>();
            foreach (var region in regionDictionary.Values)
            { 
                regions.Add(region);
            }
            DataSources.Add("World Regions - List<List<CountryShape>>", regions);

            SelectedName = "World Regions - List<List<CountryShape>>";
        }
    }

    /// <summary>
    /// Represents data object with a few properties required for scatter chart types
    /// </summary>
    public class CountryData
    {
        public CountryData()
        { 
        }
        public CountryData(double x, double y, double v)
        {
            X = x;
            Y = y;
            Value = v;
        }
        public string Name { get; set; }

        // NOTE X/Y properties are needed only for all scatter series
        public double X { get; set; }
        public double Y { get; set; }

        // NOTE this property is needed only for scatter bubble/area/contour series
        public double Value { get; set; }
         
        public override string ToString()
        {
            return this.Value + " " + this.Name;
        }
    }

    /// <summary>
    /// Represents data object with a few properties required for shape chart types
    /// </summary>
    public class CountryShape : CountryData
    {
        public CountryShape()
        {
            Points = new List<List<Point>>();
        } 
        // NOTE this property is needed only for shape series
        public List<List<Point>> Points { get; set; }        
    }


    public class ShapefileItem
    {
        public ShapefileRecordFields Fields { get; set; }

        public List<List<Point>> Points { get; set; }

        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; set; }

        public ShapeType ShapeType { get; set; } 
    }

    public static class PointEx
    {
        public static void Offset(this List<List<Point>> shapes, double x, double y)
        {
            for (int i = 0; i < shapes.Count; i++)
            {
                for (int ii = 0; ii < shapes[i].Count; ii++)
                {
                    var p = shapes[i][ii];
                    shapes[i][ii] = new Point(p.X + x, p.Y + y);
                }
            }
        }
        public static Rect GetRectMax(this List<List<Point>> shapes)
        {  
            var areaMax = double.MinValue;
            var rects = shapes.GetRectList();
            var rectMax = Rect.Empty;
            
            foreach (var rect in rects)
            {
                var area = rect.Height * rect.Height;

                if (areaMax < area)
                {
                    areaMax = area;
                    rectMax = rect;
                }
            } 
            return rectMax;
        }
        public static Rect GetRect(this List<Point> points)
        {
            var xMin = double.MaxValue;
            var xMax = double.MinValue;
            var yMin = double.MaxValue;
            var yMax = double.MinValue;

            for (int i = 0; i < points.Count; i++)
            {
                var p = points[i];
                xMin = System.Math.Min(p.X, xMin);
                xMax = System.Math.Max(p.X, xMax);
                yMin = System.Math.Min(p.Y, yMin);
                yMax = System.Math.Max(p.Y, yMax);
            }
            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }
        public static Rect GetRect(this List<List<Point>> shapes)
        {
            var xMin = double.MaxValue;
            var xMax = double.MinValue;
            var yMin = double.MaxValue;
            var yMax = double.MinValue;

            for (int i = 0; i < shapes.Count; i++)
            {
                for (int ii = 0; ii < shapes[i].Count; ii++)
                {
                    var p = shapes[i][ii];
                    xMin = System.Math.Min(p.X, xMin);
                    xMax = System.Math.Max(p.X, xMax);
                    yMin = System.Math.Min(p.Y, yMin);
                    yMax = System.Math.Max(p.Y, yMax);                     
                }
            }
            return new Rect(xMin, yMin, xMax - xMin, yMax - yMin);
        }

        public static List<Rect> GetRectList(this List<List<Point>> shapes)
        {
            var rects = new List<Rect>();

            for (int i = 0; i < shapes.Count; i++)
            {
                var rect = shapes[i].GetRect();

                rects.Add(rect);
            }
            return rects;
        }
        public static Point GetCenter(this Rect rect)
        {
            if (rect.IsEmpty)
                return new Point(double.NaN, double.NaN);

            var x = rect.X + (rect.Width / 2.0);
            var y = rect.Y + (rect.Height / 2.0);

            return new Point(x, y);
        }
    }
}
