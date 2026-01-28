using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Infragistics.Controls.Mapping;
using Infragistics.Controls.Maps;
using System.Reflection;

namespace Infragistics.Framework
{
    public class DemographicIndicator : ObservableModel
    {
        public string Name { get; set; }

        public double Total { get; set; }
        public double Min { get; private set; }
        public double Max { get; private set; }
        public int Count { get; set; }
        public double Average { get; private set; }
        public double Scale { get; private set; }

        public DemographicIndicator(string name, double value = 0)
        {
            Count = 0;
            Min = double.MaxValue;
            Max = double.MinValue;
            Name = name;
            Total = value;
        }

        public void Update(double value)
        {
            if (double.IsNaN(value)) return;
            if (double.IsInfinity(value)) return;

            Count += 1;
            Total += value;

            Max = System.Math.Max(Max, value);
            Min = System.Math.Min(Min, value);
            Average = Calcultor.Divide(Total, Count);
            Scale = Total * 100;
        }

        public override string ToString()
        {
            return this.Name + " Count = " + this.Count + " Total = " + this.Total + " Average = " + this.Average + " (" + this.Min + " : " + this.Max + " ";
        }
    }

    public class DemographicStats : ObservableModel
    {
        public static DemographicIndicator Pop;
        public static DemographicIndicator Countries;
        public static DemographicIndicator Gdp; 
        public static DemographicIndicator Economy;
        public static DemographicIndicator Organization;

        public static int CountriesCount = 0;

        static DemographicStats()
        {
            Pop = new DemographicIndicator("Pop");
            Gdp = new DemographicIndicator("Gdp");
            Countries = new DemographicIndicator("Countries");
            Economy = new DemographicIndicator("Economy");
            Organization = new DemographicIndicator("Organization");
        }
    }

    public class DemographicData : ObservableModel
    {
        public string Name { get; set; }

        public double PopTotal { get; set; }
        public double PopPercent { get; set; }

        public double GdpTotal { get; set; }
        public double GdpPercent { get; set; }
        /// <summary>Gets or sets GDP per person</summary>
        public double GdpIncome { get; set; }
        public string Economy { get; set; }
        public string Region { get; set; }
        public string Continent { get; set; }
        public string Organization { get; set; }
        public string Code { get; set; }
    }

    public class CountryPoint //: ObservableModel
    {
        public double X { get { return Point.X; } }
        public double Y { get { return Point.Y; } }
        public Point Point { get; set; }
        //public DemographicData Data { get; set; }
        public double Value { get; set; }
        public string Name { get; set; }

        public CountryPoint() : this(new DemographicData())
        {
        }
        public CountryPoint(double value)
        {
            Point = new Point(double.NaN, double.NaN);
            Value = value;
        }
        public CountryPoint(DemographicData data)
        {
            Point = new Point(double.NaN, double.NaN);
            Value = double.NaN;
            //Data = data;
        }
        public override string ToString()
        {
            return this.Name + ", Value= " + Value + ", X= " + this.X + ", Y= " + this.Y;
        }
    }

    // rename to Country Shape
    public class CountryData : DemographicData
    { 
        public Rect BorderBounds { get; set; }
        //TODO use ShapePoints[i][ii].Count > max
        public List<CountryPoint> BorderShape { get; set; }

        public List<CountryPoint> BoundingShape { get; set; }

        public List<CountryPoint> LargestShape { get; set; }

        public List<CountryPoint> SimplifiedShape { get; set; }

        public List<List<Point>> ShapePoints { get; set; } 

        public CountryPoint ShapeCenter { get; set; }
        //public DemographicData Data { get { return this; } }

        public List<DemographicIndicator> Indicators { get; set; }

        public List<StockItem> Stocks { get; set; }
        protected static Random Rand = new Random();

        public CountryData()
        {
            Indicators = new List<DemographicIndicator>();
            BorderShape = new List<CountryPoint>();
            BoundingShape = new List<CountryPoint>();
            LargestShape = new List<CountryPoint>();
            ShapePoints = new List<List<Point>>();
            Stocks = StockDataGenerator.Populate(150, this.Name);
        }
        public override string ToString()
        {
            return this.Name + ", Region= " + Region + ", Continent= " + this.Continent + ", Org= " + this.Organization;
        }
        public virtual void Initialize()
        {
            GdpIncome = Calcultor.Divide(GdpTotal, PopTotal);
            GdpPercent = Calcultor.Percent(GdpTotal, DemographicStats.Gdp.Total);
            PopPercent = Calcultor.Percent(PopTotal, DemographicStats.Pop.Total);

            Indicators = new List<DemographicIndicator>();
            Indicators.Add(new DemographicIndicator("Pop %", PopPercent));
            Indicators.Add(new DemographicIndicator("GDP %", GdpPercent));
            
            var simplified = this.ShapePoints.Simplify(5, 7, 3);
            this.SimplifiedShape = new List<CountryPoint>();
            foreach (var points in simplified)
            {
                //var value = this.PopPercent; 
                foreach (var point in points)
                {
                    var value = (Rand.Next(50, 100) / 100.0) * this.PopPercent;
                    this.SimplifiedShape.Add(new CountryPoint { Point = point, Value = value, Name = this.Name });
                }
                if (!points[0].Equals(points[points.Count - 1]))
                {
                    var value = (Rand.Next(50, 100) / 100.0) * this.PopPercent;
                    this.SimplifiedShape.Add(new CountryPoint { Point = points[0], Value = value, Name = this.Name }); 
                }
                this.SimplifiedShape.Add(new CountryPoint());
            }

            this.BorderShape = new List<CountryPoint>();
            foreach (var points in this.ShapePoints)
            {
                //var value = this.PopPercent; 
                foreach (var point in points)
                {
                    var value = (Rand.Next(50, 100) / 100.0) * this.PopPercent;
                    this.BorderShape.Add(new CountryPoint { Point = point, Value = value, Name = this.Name });
                }  
                if (!points[0].Equals(points[points.Count - 1]))
                {
                    var value = (Rand.Next(50, 100) / 100.0) * this.PopPercent;
                    this.BorderShape.Add(new CountryPoint { Point = points[0], Value = value, Name = this.Name }); 
                }
                this.BorderShape.Add(new CountryPoint());
            }
             
            BoundingShape = new List<CountryPoint>();
            foreach (var point in ShapePoints.GetBoundingPoints())
            {
                BoundingShape.Add(new CountryPoint(this.PopPercent) { Point = point });
            }

            var longestShapePoints = ShapePoints.GetLongestShape();
            var dim = longestShapePoints.GetDimensions();

            ShapeCenter = new CountryPoint(this.PopPercent)
            { Point = new Point(dim.X.Center, dim.Y.Center) };

            foreach (var point in longestShapePoints)
            {
                LargestShape.Add(new CountryPoint(this.PopPercent) { Point = point });
            } 

              
        }
    }
     
    public class Plotable<T> where T : ObservableModel, new() //: ObservableModel
    {
        public T Data { get; set; }
        public Rect BorderBounds { get; set; }
        //TODO use ShapePoints[i][ii].Count > max
        public List<Point3D> BorderPoints { get; set; }
        public List<List<Point3D>> ShapePoints { get; set; }
        public Point CenterPoint { get; set; }
        
        public Plotable(T data)
        {
            this.Data = data;
            BorderPoints = new List<Point3D>();
            ShapePoints = new List<List<Point3D>>();
        }

        public Plotable() : this(new T())
        {
        }
        public override string ToString()
        {
            return this.Data.ToString();
        }
    }
     
    public class CountryGroup : CountryData
    {
        public CountryGroup()
        {
            //_Countries = new List<string>();
        }

        //public List<string> Countries { get; set; }

        private CountryGroupInfo _Countries = new CountryGroupInfo();
        public CountryGroupInfo Countries
        {
            get { return _Countries; }
            set { if (_Countries == value) return; _Countries = value; OnPropertyChanged("Countries"); }
        }

        private CountryGroupInfo _CountriesTop5 = new CountryGroupInfo();
        public CountryGroupInfo CountriesTop5
        {
            get { return _CountriesTop5; }
            set { if (_CountriesTop5 == value) return; _CountriesTop5 = value; OnPropertyChanged("CountriesTop5"); }
        }

        public double CountriesPercent { get; set; }
        public double CountriesCount { get { return Countries.Shapes.Count; } }

        public double PopAverage { get; set; } 
        public double GdpAverage { get; set; }

        public string NameCount { get; set; }
        public override void Initialize()
        {
            base.Initialize();

            GdpAverage = GdpIncome / CountriesCount;
            PopAverage = PopTotal / CountriesCount;

            CountriesPercent = Calcultor.Percent(CountriesCount, DemographicStats.CountriesCount);
          
            Indicators.Add(new DemographicIndicator("Countries %", CountriesPercent));

            Countries.Shapes = Countries.Shapes.OrderByDescending(i => i.PopTotal).ToList();
            Countries.Data = Countries.Shapes.Select(i => i as DemographicData).OrderByDescending(i => i.PopTotal).ToList();
            //Name = Name + " (" + Countries.Data.Count + ")";
            NameCount = Name + " (" + Countries.Data.Count + ")";

            CountriesTop5 = new CountryGroupInfo();
            foreach (var item in Countries.Shapes)
            {
                if (CountriesTop5.Shapes.Count >= 5)
                {
                    break;
                }
                CountriesTop5.Shapes.Add(item);
                CountriesTop5.Data.Add(item);
            }

            BoundingShape = new List<CountryPoint>();

            foreach (var country in Countries.Shapes)
            {
                var bounds = country.BoundingShape;

                //foreach (var point in bounds)
                //{
                //    BoundingShape.Add(point);
                //   // BoundingShape.Add(new CountryPoint(country.PopPercent) { Point = point });
                //}

                //var cp = new CountryPoint(country.PopPercent);
                //cp.Point = country.ShapeCenter.Point;
                BoundingShape.Add(country.ShapeCenter);

                //foreach (var points in country.ShapePoints)
                //{
                //    var area = points.GetDimensions().Area;
                //    if (area > 100)
                //    { 
                //        foreach (var point in points)
                //        {
                //            var cp = new CountryPoint(country.PopTotal);
                //            cp.Point = new Point(point.X, point.Y);
                //            BoundingShape.Add(cp);
                //            //BoundingShape.Add(new CountryPoint(country.PopTotal) { Point = point });
                //        }
                //    }
                //}
            }

        }

        public override string ToString()
        {
            return this.Name + ", Countries= " + Countries.Shapes.Count + " Pop= " + this.PopPercent + " Gdp= " + this.GdpPercent;
        }
    }

    //controls number of series
    public enum CounryDataType
    {
        Countries, // 180+
        Regions, // 20+
        Continents, // 6+
        Economies, // 5+
        Organizations, // 4+ 
    }
    // rename to Country Data
    public class CountryGroupInfo : ObservableModel
    {
        private List<DemographicData> _Data = new List<DemographicData>();
        public List<DemographicData> Data
        {
            get { return _Data; }
            set { if (_Data == value) return; _Data = value; OnPropertyChanged("Data"); }
        }

        private List<CountryData> _Shapes = new List<CountryData>();
        public List<CountryData> Shapes
        {
            get { return _Shapes; }
            set { if (_Shapes == value) return; _Shapes = value; OnPropertyChanged("Shapes"); }
        }

        
    }

    public class CountryGroups : CountryGroupInfo // CountryGroupInfo where T : ObservableModel, new() //:: CountryGroupInfo //Dictionary<string, CountryGroup>
    {
        //public CountryGroups(T model)
        //{
        //    Dictionary = new Dictionary<string, CountryGroup>();
        //}
        //public CountryGroups() : this(new T())
        //{
        //}
        public void Add(string key, CountryData country)
        {  
            if (!Dictionary.ContainsKey(key))
            {
                var group = new CountryGroup();
                // region = new CountryGroup();
                group.Name = key;
                group.Region = country.Region;
                group.Organization = country.Organization;
                group.Continent = country.Continent;
                group.Economy = country.Economy;

                //group.ShapePoints.AddRange(country.ShapePoints);

                Dictionary.Add(key, group);
            }
            Dictionary[key].PopTotal += country.PopTotal;
            Dictionary[key].GdpTotal += country.GdpTotal;
            Dictionary[key].Countries.Shapes.Add(country);
            Dictionary[key].ShapePoints.AddRange(country.ShapePoints); 
            //Dictionary[key].Name = key + "(" + Dictionary[key].Countries.Data.Count + ")";
        }

        public List<DemographicData> ToDataList() { return Dictionary.Values.Select(i => i as DemographicData).ToList(); }

        //public Dictionary<string, CountryGroup> Dictionary = new Dictionary<string, CountryGroup>();

        public Dictionary<string, CountryGroup> Dictionary = new Dictionary<string, CountryGroup>();

    }

    public class CountryDictionary : CountryGroupInfo 
    {
        public Dictionary<string, CountryData> Dictionary = new Dictionary<string, CountryData>();
    }
     
    public class CountryDataSources : ObservableModel
    {
        public CountryDataSources()
        {
            //ShapefileName = "world_countries";
            //ShapefileName = "world_countries_admin";
            LoadShapefiles();
        }

        public ShapefileProvider ContinentsShapefile = new ShapefileProvider();
        public ShapefileProvider CountriesShapefile = new ShapefileProvider();

        public CountryGroups Regions = new CountryGroups();
        public CountryGroups Continents = new CountryGroups();
        public CountryGroups Organizations = new CountryGroups();
        public CountryGroups Economies = new CountryGroups();
        public CountryDictionary Countries = new CountryDictionary();

        #region Properties
        //private List<DemographicData> _CountriesData = new List<DemographicData>();
        //public List<DemographicData> CountriesData
        //{
        //    get { return _CountriesData; }
        //    set { if (_CountriesData == value) return; _CountriesData = value; OnPropertyChanged("CountriesData"); }
        //}
        //private List<CountryData> _CountriesShapes = new List<CountryData>();
        //public List<CountryData> CountriesShapes
        //{
        //    get { return _CountriesShapes; }
        //    set { if (_CountriesShapes == value) return; _CountriesShapes = value; OnPropertyChanged("CountriesShapes"); }
        //}

        //private List<DemographicData> _RegionsData = new List<DemographicData>();
        //public List<DemographicData> RegionsData
        //{
        //    get { return _RegionsData; }
        //    set { if (_RegionsData == value) return; _RegionsData = value; OnPropertyChanged("RegionsData"); }
        //}
        //private List<CountryGroup> _RegionsShapes = new List<CountryGroup>();
        //public List<CountryGroup> RegionsShapes
        //{
        //    get { return _RegionsShapes; }
        //    set { if (_RegionsShapes == value) return; _RegionsShapes = value; OnPropertyChanged("RegionsShapes"); }
        //}

        //private List<DemographicData> _ContinentsData = new List<DemographicData>();
        //public List<DemographicData> ContinentsData
        //{
        //    get { return _ContinentsData; }
        //    set { if (_ContinentsData == value) return; _ContinentsData = value; OnPropertyChanged("ContinentsData"); }
        //}

        //private List<DemographicData> _OrganizationsData = new List<DemographicData>();
        //public List<DemographicData> OrganizationsData
        //{
        //    get { return _OrganizationsData; }
        //    set { if (_OrganizationsData == value) return; _OrganizationsData = value; OnPropertyChanged("OrganizationsData"); }
        //}

        //private List<DemographicData> _EconomiesData = new List<DemographicData>();
        //public List<DemographicData> EconomiesData
        //{
        //    get { return _EconomiesData; }
        //    set { if (_EconomiesData == value) return; _EconomiesData = value; OnPropertyChanged("EconomiesData"); }
        //}

        //public List<string> CountryNames
        //{
        //    get { return CountriesData.Select(c => c.Name).Distinct().ToList() ; }
        //}
        //public List<string> RegionNames
        //{
        //    get { return Regions.Dictionary.Keys.ToList(); }
        //}
        //public List<string> ContinentNames
        //{
        //    get { return Continents.Dictionary.Keys.ToList(); }
        //}
        //public List<string> OrganizationNames
        //{
        //    get { return Organizations.Dictionary.Keys.ToList(); } 
        //}

        #endregion
        public virtual void LoadShapefiles()
        {
            ContinentsShapefile.ImportCompleted += OnShapefileImported;
            ContinentsShapefile.ShapefileName = "world_continents_small";
            CountriesShapefile.ImportCompleted += OnShapefileImported;
            CountriesShapefile.ShapefileName = "world_countries_admin";
        }

        public virtual void OnShapefileImported(object sender, AsyncCompletedEventArgs e)
        {
            if (ContinentsShapefile.IsLoaded &&
                CountriesShapefile.IsLoaded)
            {
                ParseShapefiles();
            }
        }

        public void ParseShapefiles()
        {
            //Debug.WriteLine("OnShapefileImported " + ShapefileName + " with " + Shapefile.Count + " records " + Shapefile.GetPointsTotal() + " points");
            // extracting data about countries and regions based on Fields of Shapefile

            // Debug.WriteLine("CountriesShapefile " + DataStats.Create(this.CountriesShapefile));
            // var fields = CountriesShapefile.Select(r => r.Fields).ToList();
            //Debug.WriteLine("CountriesShapefile.Fields " + DataStats.Create(fields));
              
            foreach (var record in CountriesShapefile)
            {
                if (record.Fields == null || record.Fields.Count == 0) continue;

                var country = new CountryData();
                //var country = new CountryData();  
                country.Continent = record.GetString("Continent");
                if (country.Continent.StartsWith("Seven seas"))
                    continue;
                
                country.Name = record.GetString("Name");
                country.Code = record.GetString("AdminCode");
                country.Region = record.GetString("Region"); 

                country.PopTotal = record.GetValue("PopTotal");
                country.GdpTotal = record.GetValue("GdpTotal");
                //country.GdpIncome = Calcultor.Divide(country.GdpTotal, country.PopTotal);
                 
                #region Economy
                country.Economy = record.GetValue<string>("Economy");
                  
                if (country.Economy.Contains("G7"))
                {
                    country.Economy = "Developed";
                    country.Organization = "G7";
                }
                else if (country.Economy.Contains("nonG7"))
                {
                    country.Economy = "Developed";
                    country.Organization = "nonG7";
                }
                else if (country.Economy.Contains("BRIC"))
                {
                    country.Economy = "Emerging";
                    country.Organization = "BRIC";
                }
                else if (country.Economy.Contains("MIKT"))
                {
                    country.Economy = "Emerging";
                    country.Organization = "MIKT";
                }
                else if (country.Economy.Contains("Emerging"))
                {
                    country.Economy = "Emerging";
                    country.Organization = "None";
                }
                else if (country.Economy.Contains("Developing"))
                {
                    country.Economy = "Developing";
                    country.Organization = "None";
                }
                else if (country.Economy.Contains("Least developed"))
                {
                    country.Economy = "Undeveloped";
                    country.Organization = "None";
                }
                else 
                {
                    country.Economy = "Other";
                    country.Organization = "None";
                }
                #endregion

                DemographicStats.CountriesCount += 1;
                DemographicStats.Pop.Total += country.PopTotal;
                DemographicStats.Gdp.Total += country.GdpTotal;

                //var countryShape = new Plottable<CountryData>(country);
                country.ShapePoints = record.Points; 

                //CountriesData.Add(country); 
                Countries.Dictionary.Add(country.Code, country);

                //if (country.Continent.Equals("Antarctica"))
                //    continue;

                Regions.Add(country.Region, country);
                Continents.Add(country.Continent, country);
                Organizations.Add(country.Organization, country);
                Economies.Add(country.Economy, country); 
            }

            foreach (var country in Countries.Dictionary.Values)
            {
                //country.GdpPercent = country.GdpTotal / DemographicStats.Gdp.Total;
                //country.PopPercent = country.PopTotal / DemographicStats.Pop.Total;
                country.Initialize();
            }

            this.Countries.Data = Countries.Dictionary.Values.Select(c => c as DemographicData).ToList();
            this.Countries.Shapes = Countries.Dictionary.Values.Select(i => i).OrderBy(i => i.Continent).ToList();

            foreach (var region in Regions.Dictionary.Values)
            {
                region.Initialize(); 
            }


            foreach (var continent in Continents.Dictionary.Values)
            {
                continent.ShapePoints = new List<List<Point>>();
            }
            foreach (var record in ContinentsShapefile)
            {
                var name = record.GetString("Name");
                if (Continents.Dictionary.ContainsKey(name))
                {
                    var continent = Continents.Dictionary[name];
                    //var simplified = record.Points.Simplify(10, 7, 3);

                    continent.ShapePoints.AddRange(record.Points);


                    //continent.ShapePoints = record.Points; //simplified; // 
                    //continent.Initialize();
                } 
            }

            foreach (var continent in Continents.Dictionary.Values)
            {
                continent.Initialize();
            }
            //foreach (var continent in Continents.Dictionary.Values)
            //{
            //    foreach (var record in ContinentsShapefile)
            //    {
            //        var name = record.Fields["Name"];
            //    }

            //    continent.Initialize();
            //}

            foreach (var org in Organizations.Dictionary.Values)
            {
                org.Initialize();
            }

            foreach (var eco in Economies.Dictionary.Values)
            {
                eco.Initialize();
            }

            var reg = Regions.Dictionary.Values.Select(i => i).ToList();
            reg = reg.OrderBy(i => i.Continent).ToList();

            //Regions.Shapes = Regions.Dictionary.Values.OrderBy(i => i.Continent).ToList();

            Regions.Shapes = Regions.Dictionary.Values.Select(i => i as CountryData).OrderBy(i => i.Continent).ToList();
            Regions.Data = Regions.Dictionary.Values.Select(i => i as DemographicData).OrderBy(i => i.Continent).ToList();

            Economies.Shapes = Economies.Dictionary.Values.Select(i => i as CountryData).OrderBy(i => i.Continent).ToList();
            Economies.Data = Economies.Dictionary.Values.Select(i => i as DemographicData).OrderBy(i => i.Continent).ToList();

            Continents.Shapes = Continents.Dictionary.Values.Select(i => i as CountryData).OrderBy(i => i.Continent).ToList();
            Continents.Data = Continents.Dictionary.Values.Select(i => i as DemographicData).OrderBy(i => i.Continent).ToList();

            Organizations.Shapes = Organizations.Dictionary.Values.Select(i => i as CountryData).OrderBy(i => i.Continent).ToList();
            Organizations.Data = Organizations.Dictionary.Values.Select(i => i as DemographicData).OrderBy(i => i.Continent).ToList();


            //this.RegionsData = Regions.ToDataList().OrderBy(i => i.Continent).ToList();
            //this.EconomiesData = EconomiesDictionary.ToDataList().OrderBy(i => i.Economy).ToList();
            //this.ContinentsData = ContinentsDictionary.ToDataList().OrderBy(i => i.Continent).ToList();
            //this.OrganizationsData = OrganizationsDictionary.ToDataList().OrderBy(i => i.Organization).ToList();

            //this.ContinentsData = ContinentsDictionary.Values.ToList();
            //this.OrganizationsData = OrganizationsDictionary.Values.ToList();
            //this.EconomiesData = EconomiesDictionary.Values.ToList();

            // Debug.WriteLine("Countries " + DataStats.Create(this.Countries));

            //this.CountriesValues = Create<ValueDataItem>(this.Countries);
            //this.CountriesPoints = Create<ShapeDataItem>(this.Countries);
            //this.CountriesBubbles = Create<BubbleDataItem>(this.Countries);
            //this.CountriesQuadples = Create<QuadpleDataItem>(this.Countries);
            //this.CountriesLocations = Create<LocationDataItem>(this.Countries);

            //this.NeastedCountriesValues = new List<List<ValueDataItem>>();
            //this.NeastedCountriesBubbles = new List<List<BubbleDataItem>>();
            //this.NeastedCountriesQuadples = new List<List<QuadpleDataItem>>();
            //this.NeastedCountriesLocations = new List<List<LocationDataItem>>();

            //for each (var region in Regions.Values)
            //{
            //    region.Update();
            //    if (region.Name != "Antarctica")
            //    {
            //        BorderPoints.AddRange(region.BorderPoints);
            //    }

            //    var d = region.Countries.Select(c => c.Name).Distinct().ToList();
            //    if (d.Count() != region.Countries.Count)
            //        Debug.WriteLine(region.Name + " " + d.Count());

            //    //this.NeastedCountriesValues.Add(Create<ValueDataItem>(region.Countries));
            //    //this.NeastedCountriesBubbles.Add(Create<BubbleDataItem>(region.Countries));
            //    //this.NeastedCountriesQuadples.Add(Create<QuadpleDataItem>(region.Countries));
            //    //this.NeastedCountriesLocations.Add(Create<LocationDataItem>(region.Countries));
            //}
            ////Debug.WriteLine("Regions " + DataStats.Create(this.Regions));

            //this.RegionsValues = Create<ValueDataItem>(this.Regions);
            //this.RegionsPoints = Create<ShapeDataItem>(this.Regions);
            //this.RegionsBubbles = Create<BubbleDataItem>(this.Regions);
            //this.RegionsQuadples = Create<QuadpleDataItem>(this.Regions);
            //this.RegionsLocations = Create<LocationDataItem>(this.Regions);



        }


    }

    public class ShapefileProvider : ShapefileConverter
    {
        private string _ShapefileName;
        /// <summary> Gets or sets ShapefileName </summary>
        public string ShapefileName
        {
            get { return _ShapefileName; }
            set { if (_ShapefileName == value) return; _ShapefileName = value; OnPropertyChanged("ShapefileName"); }
        } 
        /// <summary> Gets or sets IsLoaded </summary>
        public bool IsLoaded
        {
            get { return this.Count > 0; }
            //set { if (_IsLoaded == value) return; _IsLoaded = value; OnPropertyChanged("IsLoaded"); }
        } 

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == "ShapefileName")
            {
                var path = "/Infragistics.Framework.Controls;component/shapefiles/";
                //this.IsLoaded = false;
                this.ImportCompleted += OnShapefileImported;
                this.ShapefileSource = new Uri(path + ShapefileName + ".shp", UriKind.RelativeOrAbsolute);
                this.DatabaseSource  = new Uri(path + ShapefileName + ".dbf", UriKind.RelativeOrAbsolute);
            }
        }
        public virtual void OnShapefileImported(object sender, AsyncCompletedEventArgs e)
        {
            //this.IsLoaded = true; 
        }
    }

    public abstract class ShapefileDataSource : ObservableModel
    {
        public ShapefileDataSource()
        {
            //ShapefilePath = "/Infragistics.Framework.Controls;component/shapefiles/";
            //var assembly = this.GetType().Assembly.GetName().Name;
            //ShapefilePath = "/" + assembly + ";component/data/shapefiles/";
            ShapefilePath = "/Infragistics.Framework.Controls;component/shapefiles/";
        }
        public virtual void LoadShapefile()
        {
            if (Shapefile == null)
            {
                Shapefile = new ShapefileConverter();
                Shapefile.ImportCompleted += OnShapefileImported;
                Shapefile.ShapefileSource = new Uri(ShapefilePath + ShapefileName + ".shp", UriKind.RelativeOrAbsolute);
                Shapefile.DatabaseSource  = new Uri(ShapefilePath + ShapefileName + ".dbf", UriKind.RelativeOrAbsolute);
            } 
        }
        public string ShapefileName { get; set; }
        public string ShapefilePath { get; set; }
        public static ShapefileConverter Shapefile { get; set; }

        public virtual void OnShapefileImported(object sender, AsyncCompletedEventArgs e)
        { 
            OnPropertyChanged("Shapefile");
        }


    }

}
