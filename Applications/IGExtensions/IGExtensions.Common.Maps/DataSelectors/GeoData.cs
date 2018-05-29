using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using IGExtensions.Common.Models;
using IGExtensions.Framework;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace IGExtensions.Common.Maps.DataSelectors
{
    public class GeoEarthQuakes : GeoDataLocations
    {
        public GeoEarthQuakes()
        {

        }
      
    }
    public class GeoDataLocations : GeoDataSource
    {
        public GeoDataLocations()
        {
            this.DataType = GeoDataType.Locations;

            this.SupportedSeries = new List<Type>();
            this.SupportedSeries.Add(typeof(GeographicSymbolSeries));
            this.SupportedSeries.Add(typeof(GeographicProportionalSymbolSeries));

            this.DataMembers = new List<string>();
            this.DataMembers.Add("Latitude");
            this.DataMembers.Add("Longitude");
        }

        public List<string> DataMembers { get; set; }

       
        private List<GeoLocation> _dataItems;
        /// <summary>
        /// Gets or sets DataItems property 
        /// </summary>
        public List<GeoLocation> DataItems
        {
            get {  return _dataItems; }
            set { if (_dataItems == value) return; _dataItems = value; OnPropertyChanged("DataItems"); }
        }


        public List<Type> SupportedSeries { get; set; }
    }
    public class GeoDataShapes : GeoDataSource
    {
        public GeoDataShapes()
        {
            this.DataType = GeoDataType.Locations;

            this.SupportedSeries = new List<Type>();
            this.SupportedSeries.Add(typeof(GeographicShapeSeries));
            this.SupportedSeries.Add(typeof(GeographicShapeControlSeries));
        }

        public List<Type> SupportedSeries { get; set; }
    }
    
    abstract public class GeoDataSortSettings : ObservableModel //, IComparer
    {
        protected GeoDataSortSettings()
        {
            this.SortPropertyName = "Magnitude";
            this.SortDirection = ListSortDirection.Descending;
        }
        private ListSortDirection _sortDirection;
        /// <summary>
        /// Gets or sets ListSortDirection property 
        /// </summary>
        public ListSortDirection SortDirection
        {
            get { return _sortDirection; }
            set { if (_sortDirection == value) return; _sortDirection = value; OnPropertyChanged("SortDirection"); }
        }
        private string _sortPropertyName;
        /// <summary>
        /// Gets or sets SortPropertyName property 
        /// </summary>
        public string SortPropertyName
        {
            get { return _sortPropertyName; }
            set { if (_sortPropertyName == value) return; _sortPropertyName = value; OnPropertyChanged("SortPropertyName"); }
        }

        //public int Compare(object x, object y)
        //{
        //    var eq1 = x as EarthQuakeData;
        //    var eq2 = y as EarthQuakeData;
        //    if (eq1 == null) return 0;
        //    if (eq2 == null) return 0;

        //    return eq1.Name.CompareTo(eq2.Name);
        //}
    }
    abstract public class GeoDataFilterSettings : ObservableModel, IGeoDataFilterSettings
    {
        abstract public bool Filter(object item);
        private string _itemsSourceKey;
        /// <summary>
        /// Gets or sets ItemsSourceKey property 
        /// </summary>
        public string ItemsSourceKey
        {
            get {  return _itemsSourceKey; }
            set { if (_itemsSourceKey == value) return; _itemsSourceKey = value; OnPropertyChanged("ItemsSourceKey"); }
        }
        //TODO add min/max/value properties and update these properties in derived classes 
        //TODO add maxCount item property
    }
    public interface IGeoDataFilterSettings
    {
        bool Filter(object item);
    }
    public class GeoDataFilters : ObservableModel
    {
        public GeoDataFilters()
        {
            this.EarthQuakeFilter = new EarthQuakeFilterSettings();
            this.WorldCitiesFilter = new WorldCitiesFilterSettings();
            this.WorldCountriesFilter = new WorldCountriesFilterSettings();
        }
        #region Properties

        public List<GeoDataFilterSettings> List
        {
            get
            {
                var list = new List<GeoDataFilterSettings>();
                list.Add(this.EarthQuakeFilter);
                list.Add(this.WorldCitiesFilter);
                list.Add(this.WorldCountriesFilter);
                return list;
            }
        }

        private EarthQuakeFilterSettings _earthQuakeFilter;
        /// <summary>
        /// Gets or sets EarthQuakeFilter property 
        /// </summary>
        public EarthQuakeFilterSettings EarthQuakeFilter
        {
            get { return _earthQuakeFilter; }
            set { if (_earthQuakeFilter == value) return; _earthQuakeFilter = value; OnPropertyChanged("EarthQuakeFilter"); }
        }

        private WorldCitiesFilterSettings _worldCitiesFilter;
        /// <summary>
        /// Gets or sets WorldCitiesFilter property 
        /// </summary>
        public WorldCitiesFilterSettings WorldCitiesFilter
        {
            get { return _worldCitiesFilter; }
            set { if (_worldCitiesFilter == value) return; _worldCitiesFilter = value; OnPropertyChanged("WorldCitiesFilter"); }
        }
        private WorldCountriesFilterSettings _worldCountriesFilter;
        /// <summary>
        /// Gets or sets WorldCountriesFilter property 
        /// </summary>
        public WorldCountriesFilterSettings WorldCountriesFilter
        {
            get { return _worldCountriesFilter; }
            set { if (_worldCountriesFilter == value) return; _worldCountriesFilter = value; OnPropertyChanged("WorldCountriesFilter"); }
        } 
        #endregion
    }

    //TODO add GeoRect property that includes all geo-points and test it with GeoMap world rect
    abstract public class GeoDataViewSource : ObservableCollectionViewSource 
    {
        protected GeoDataViewSource()
        {
            //this.DataCategory = GeoDataCategory.Unknown;
            this.DataType = GeoDataType.Unknown;
            this.DataWorldRect = GeoRect.WorldRect;
            //this.SortSettings = new EarthQuakeSortSettings();
            //this.SortSettings.PropertyChanged += OnSortSettingsChanged;

            //this.FilterSettings = new EarthQuakeFilterSettings();
            //this.FilterSettings.PropertyChanged += OnFilterSettingsChanged;
        }
        #region Properties
        private GeoRect _dataWorldRect;
        /// <summary>
        /// Gets or sets geographic world rect associated with the data source
        /// </summary>
        public GeoRect DataWorldRect
        {
            get { return _dataWorldRect; }
            set { if (_dataWorldRect == value) return; _dataWorldRect = value; OnPropertyChanged("DataWorldRect"); }
        }

        private string _dataSourceTrademark;
        /// <summary>
        /// Gets or sets DataSourceTrademark property 
        /// </summary>
        public string DataSourceTrademark
        {
            get { return _dataSourceTrademark; }
            set { if (_dataSourceTrademark == value) return; _dataSourceTrademark = value; OnPropertyChanged("DataSourceTrademark"); }
        }

        private string _dataSourceKey;
        /// <summary>
        /// Gets or sets ItemsSourceKey property 
        /// </summary>
        public string DataSourceKey
        {
            get { return _dataSourceKey; }
            set { if (_dataSourceKey == value) return; _dataSourceKey = value; OnPropertyChanged("DataSourceKey"); }
        }

        public int DataItemsCount
        {
            get
            {
                int result = 0;
                if (this.Source != null)
                {
                    var collection = this.Source as IEnumerable;
                    result = collection.Count();
                }
                return result;
            }
        }
        public GeoDataType DataType { get; set; }
        //public GeoDataCategory DataCategory { get; set; }

        private GeoDataSortSettings _sortSettings;
        /// <summary>
        /// Gets or sets SortSettings property 
        /// </summary>
        public GeoDataSortSettings SortSettings
        {
            get { return _sortSettings; }
            set
            {
                if (_sortSettings == value) return;
                if (_sortSettings != null) _sortSettings.PropertyChanged -= OnSortSettingsChanged;
                _sortSettings = value;
                _sortSettings.PropertyChanged += OnSortSettingsChanged;
                OnPropertyChanged("SortSettings");
            }
        }
        private GeoDataFilterSettings _filterSettings;
        /// <summary>
        /// Gets or sets FilterSettings property 
        /// </summary>
        public GeoDataFilterSettings FilterSettings
        {
            get { return _filterSettings; }
            set
            {
                if (_filterSettings == value) return;
                if (_filterSettings != null) _filterSettings.PropertyChanged -= OnFilterSettingsChanged;
                _filterSettings = value;
                _filterSettings.PropertyChanged += OnFilterSettingsChanged;
                OnPropertyChanged("FilterSettings");
            }
        }

        #endregion

        #region Event Handlers
        private void OnFilterSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.View == null) return;

            this.View.Filter = this.FilterSettings.Filter;
            //this.View.Filter = EarthQuakesFilter;
            //this.View.Refresh();
        }
        private void OnSortSettingsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.View == null) return;

            this.View.SortDescriptions.Clear();
            this.View.SortDescriptions.Add(new SortDescription(SortSettings.SortPropertyName, SortSettings.SortDirection));
        }
        #endregion
        #region Methods
        public void UpdateDataWorldRect(List<IGeoLocatable> geoLocations)
        {
            if (geoLocations == null || geoLocations.Count == 0) return;
           
            var geoRect = GeoCalculator.GetRect(geoLocations);
            if (!geoRect.IsEmpty) this.DataWorldRect = geoRect;
        }
        public void UpdateDataWorldRect(List<Point> geoPoint)
        {
            if (geoPoint == null || geoPoint.Count == 0) return;

            var geoRect = GeoCalculator.GetRect(geoPoint);
            if (!geoRect.IsEmpty) this.DataWorldRect = geoRect;
        }
        #endregion
    }

    public class GeoDataViewSourceDictionary : Dictionary<string, GeoDataViewSource>
    {
        public GeoDataViewSourceDictionary()
        {
            //this.Filters = new List<GeoDataFilterSettings>();
            this.Filters = new GeoDataFilters();

            //this.Filters = new Dictionary<string, GeoDataFilterSettings>();
            //this.EarthQuakeFilter = new EarthQuakeFilterSettings();
            //this.WorldCitiesFilter = new WorldCitiesFilterSettings();
            //this.WorldCountriesFilter = new WorldCountriesFilterSettings();

        }
        //public void UpdateFilter(GeoDataFilterSettings dataFilter)
        //{
        //    if (this.ContainsKey(dataFilter.ItemsSourceKey))
        //    {
        //        this[dataFilter.ItemsSourceKey] = dataViewSource;
        //    }
        //}

        private void UpdateFilters(GeoDataFilters dataFilters)
        {
            if (dataFilters == null) return;
            if (dataFilters.List == null) return;
            //if (dataFilters.List.Count == 0) return;
            
            foreach (var filter in dataFilters.List)
            {
                if (filter.ItemsSourceKey != null && this.ContainsKey(filter.ItemsSourceKey))
                {
                    this[filter.ItemsSourceKey].FilterSettings = filter;
                }
            }
            
            //else
            //{
            //    this.Add(key, dataViewSource);
            //    //this.Filters.Add(dataViewSource.FilterSettings);
            //}
        }

        public void Update(string key, GeoDataViewSource dataViewSource)
        {
            dataViewSource.DataSourceKey = key;
            if (dataViewSource.FilterSettings != null)
                dataViewSource.FilterSettings.ItemsSourceKey = key;

            if (this.ContainsKey(key))
            {
                this[key] = dataViewSource;
            }
            else
            {
                this.Add(key, dataViewSource);
                //this.Filters.Add(dataViewSource.FilterSettings);
            }
            //if (dataViewSource.DataCategory == GeoDataCategory.EarthQuakes)
            //{
            //    //this.EarthQuakeFilter = dataViewSource.FilterSettings as EarthQuakeFilterSettings;
            //    this.Filters.EarthQuakeFilter = dataViewSource.FilterSettings as EarthQuakeFilterSettings;
            //}
            //else if (dataViewSource.DataCategory == GeoDataCategory.WorldCountries)
            //{
            //    //this.WorldCountriesFilter = dataViewSource.FilterSettings as WorldCountriesFilterSettings;
            //    this.Filters.WorldCountriesFilter = dataViewSource.FilterSettings as WorldCountriesFilterSettings;
            //}
            //else if (dataViewSource.DataCategory == GeoDataCategory.WorldCities)
            //{
            //    //this.WorldCitiesFilter = dataViewSource.FilterSettings as WorldCitiesFilterSettings;
            //    this.Filters.WorldCitiesFilter = dataViewSource.FilterSettings as WorldCitiesFilterSettings;
            //}

            //dataViewSource.
            //if (this.Filters.ContainsKey(key))
            //{
            //    this.Filters[key] = dataViewSource.FilterSettings;
            //}
            //else
            //{
            //    this.Filters.Add(key, dataViewSource.FilterSettings);
            //}
        }
        private GeoDataFilters _filters;
        /// <summary>
        /// Gets or sets Filters property 
        /// </summary>
        public GeoDataFilters Filters
        {
            get { return _filters; }
            set { if (_filters == value) return; _filters = value;

                //UpdateFilters(_filters);
                OnPropertyChanged("Filters"); }
        }


        //public void GetDataFilter(GeoDataCategory dataCategory)
        //{
        //    if (this.ContainsKey(key))
        //    {
        //        this[key] = dataViewSource;
        //    }

        //    if (dataViewSource.DataCategory == GeoDataCategory.EarthQuakes)
        //    {
        //        this.EarthQuakeFilter = dataViewSource.FilterSettings;
        //    }
        //    else if (dataViewSource.DataCategory == GeoDataCategory.WorldCountries)
        //    {
        //        this.WorldCountriesFilter = dataViewSource.FilterSettings;
        //    }
        //    else if (dataViewSource.DataCategory == GeoDataCategory.WorldCities)
        //    {
        //        this.WorldCitiesFilter = dataViewSource.FilterSettings;
        //    }
        //}

        //private EarthQuakeFilterSettings _earthQuakeFilter;
        ///// <summary>
        ///// Gets or sets EarthQuakeFilter property 
        ///// </summary>
        //public EarthQuakeFilterSettings EarthQuakeFilter
        //{
        //    get { return _earthQuakeFilter; }
        //    set { if (_earthQuakeFilter == value) return; _earthQuakeFilter = value; OnPropertyChanged("EarthQuakeFilter"); }
        //}

        //private WorldCitiesFilterSettings _worldCitiesFilter;
        ///// <summary>
        ///// Gets or sets WorldCitiesFilter property 
        ///// </summary>
        //public WorldCitiesFilterSettings WorldCitiesFilter
        //{
        //    get {  return _worldCitiesFilter; }
        //    set { if (_worldCitiesFilter == value) return; _worldCitiesFilter = value; OnPropertyChanged("WorldCitiesFilter"); }
        //}
        //private WorldCountriesFilterSettings _worldCountriesFilter;
        ///// <summary>
        ///// Gets or sets WorldCountriesFilter property 
        ///// </summary>
        //public WorldCountriesFilterSettings WorldCountriesFilter
        //{
        //    get {  return _worldCountriesFilter; }
        //    set { if (_worldCountriesFilter == value) return; _worldCountriesFilter = value; OnPropertyChanged("WorldCountriesFilter"); }
        //}

        //private List<GeoDataFilterSettings> _filters;
        ///// <summary>
        ///// Gets or sets Filters property 
        ///// </summary>
        //public List<GeoDataFilterSettings> Filters
        //{
        //    get {  return _filters; }
        //    set { if (_filters == value) return; _filters = value; OnPropertyChanged("Filters"); }
        //}


        //private Dictionary<string, GeoDataFilterSettings> _dataFilters;
        ///// <summary>
        ///// Gets or sets DataFilters property 
        ///// </summary>
        //public Dictionary<string, GeoDataFilterSettings> Filters
        //{
        //    get {  return _dataFilters; }
        //    set { if (_dataFilters == value) return; _dataFilters = value; 
        //        //OnPropertyChanged("DataFilters"); 
        //    }
        //}

        /// <summary>
        /// Raises the PropertyChanged event for provided property name
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        /// <summary>
        /// Occurs when a property value was changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
       

    }
  
    public class EarthQuakesViewSource : GeoDataViewSource
    {
        public EarthQuakesViewSource()
        {
            this.DataSource = new EarthQuakes();
            //this.DataCategory = GeoDataCategory.EarthQuakes;
            this.DataType = GeoDataType.Locations;
            this.DataWorldRect = GeoRect.WorldRect;
            this.FilterSettings = new EarthQuakeFilterSettings();
            this.SortSettings = new EarthQuakeSortSettings();
            this.PropertyChanged += OnViewSourcePropertyChanged;
        }
        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataSource")
            {
                this.FilterView.MagnitudeMin = 5; // DataSource.Magnitude.Min;
                this.FilterView.MagnitudeMax = 10; // DataSource.Magnitude.Max;
                this.FilterView.DepthMin = DataSource.Depth.Min;
                this.FilterView.DepthMax = DataSource.Depth.Max;
            }
        }
     
        public EarthQuakeFilterSettings FilterView
        {
            get { return this.FilterSettings as EarthQuakeFilterSettings; }
            set { this.FilterSettings = value; }
        }

        private EarthQuakes _dataSource;
        /// <summary>
        /// Gets or sets DataSource property 
        /// </summary>
        public EarthQuakes DataSource
        {
            get { return _dataSource; }
            set { if (_dataSource == value) return; _dataSource = value; OnPropertyChanged("DataSource"); }
        }

        // public EarthQuakes DataSource { get; set; }

    }
    public class EarthQuakeSortSettings : GeoDataSortSettings
    {
        public EarthQuakeSortSettings()
        {
            this.SortPropertyName = "Magnitude";
            this.SortDirection = ListSortDirection.Descending;
        }
    }
    public class EarthQuakeFilterSettings : GeoDataFilterSettings //ObservableModel, IGeoDataFilterSettings //GeoDataFilterSettings
    {
        public EarthQuakeFilterSettings()
        {
            this.MagnitudeMin = 0;
            this.MagnitudeMax = 10;
            this.DepthMin = 0;
            this.DepthMax = double.MaxValue; // 10000;
            this.DateMin = DateTime.MinValue;
            this.DateMax = DateTime.MaxValue;
        }
        public override bool Filter(object item) // 
        {
            var eq = item as EarthQuakeData;
            if (eq == null) return false;

            return eq.Magnitude >= this.MagnitudeMin && eq.Depth >= this.DepthMin &&
                   eq.Magnitude <= this.MagnitudeMax && eq.Depth <= this.DepthMax;
            //return eq.Magnitude >= this.MagnitudeMin && eq.Depth >= this.DepthMin && eq.Updated >= this.DateMin &&
            //       eq.Magnitude <= this.MagnitudeMax && eq.Depth <= this.DepthMax && eq.Updated <= this.DateMax;
        }
        #region Properties

        private DateTime _dateMin;
        /// <summary>
        /// Gets or sets DateMin property 
        /// </summary>
        public DateTime DateMin
        {
            get { return _dateMin; }
            set { if (_dateMin == value) return; _dateMin = value; OnPropertyChanged("DateMin"); }
        }
        private DateTime _dateMax;
        /// <summary>
        /// Gets or sets DateMax property 
        /// </summary>
        public DateTime DateMax
        {
            get { return _dateMax; }
            set { if (_dateMax == value) return; _dateMax = value; OnPropertyChanged("DateMax"); }
        }
        private double _depthMin;
        /// <summary>
        /// Gets or sets DepthMin property 
        /// </summary>
        public double DepthMin
        {
            get { return _depthMin; }
            set { if (_depthMin == value) return; _depthMin = value; OnPropertyChanged("DepthMin"); }
        }
        private double _depthMax;
        /// <summary>
        /// Gets or sets DepthMax property 
        /// </summary>
        public double DepthMax
        {
            get { return _depthMax; }
            set { if (_depthMax == value) return; _depthMax = value; OnPropertyChanged("DepthMax"); }
        }
        private double _magnitudeMin;
        /// <summary>
        /// Gets or sets MagnitudeMin property 
        /// </summary>
        public double MagnitudeMin
        {
            get { return _magnitudeMin; }
            set { if (_magnitudeMin == value) return; _magnitudeMin = value; OnPropertyChanged("MagnitudeMin"); }
        }
        private double _magnitudeMax;
        /// <summary>
        /// Gets or sets MagnitudeMax property 
        /// </summary>
        public double MagnitudeMax
        {
            get { return _magnitudeMax; }
            set { if (_magnitudeMax == value) return; _magnitudeMax = value; OnPropertyChanged("MagnitudeMin"); }
        }

        #endregion
    }

    abstract public class WorldStatsSortSettings : GeoDataSortSettings
    {
        protected WorldStatsSortSettings()
        {
            this.SortPropertyName = "Population";
            this.SortDirection = ListSortDirection.Descending;
        }
    }
    abstract public class WorldStatsFilterSettings : GeoDataFilterSettings
    {
        protected WorldStatsFilterSettings()
        {
            this.PopulationMin = 0;
            this.PopulationMax = double.MaxValue; // 10000000000;
        }
        private double _populationMin;
        /// <summary>
        /// Gets or sets PopulationMin property 
        /// </summary>
        public double PopulationMin
        {
            get { return _populationMin; }
            set { if (_populationMin == value) return; _populationMin = value; OnPropertyChanged("PopulationMin"); }
        }
        private double _populationMax;
        /// <summary>
        /// Gets or sets PopulationMax property 
        /// </summary>
        public double PopulationMax
        {
            get { return _populationMax; }
            set { if (_populationMax == value) return; _populationMax = value; OnPropertyChanged("PopulationMax"); }
        }
    }
  
    public class WorldCountriesSortSettings : WorldStatsSortSettings
    {
        public WorldCountriesSortSettings()
        {
            //this.SortPropertyName = "Population";
            //this.SortDirection = ListSortDirection.Descending;
        }
    }
    public class WorldCountriesFilterSettings : WorldStatsFilterSettings
    {
        public WorldCountriesFilterSettings()
        {
            this.PopulationMin = 0;
            this.PopulationMax = 10000000000; // double.MaxValue; // 10000000000;
            this.AreaMin = 0;
            this.AreaMax = double.MaxValue; // 1000000000;
            this.Regions = new List<string> { "Asia", "NorthAfrica", "SubSaharanAfrica", "Antarctica", 
                "Caribbean", "LatinAmerica", "Australia", "NorthAmerica", "Pacific", "Europe", };
            //this.DateMin = DateTime.MinValue;
            //this.DateMax = DateTime.MaxValue;
        }
        public override bool Filter(object item)
        {
            var wc = item as WorldCountry;
            if (wc == null) return false; 

            return wc.Population >= this.PopulationMin && wc.Area >= this.AreaMin && ContainsRegion(wc) &&
                   wc.Population <= this.PopulationMax && wc.Area <= this.AreaMax; 
        }
        private bool ContainsRegion(WorldCountry item)
        {
            foreach (var region in Regions)
            {
                var regionName = item.Region.Replace(" ", "").ToLower();
                if (regionName == region.Replace(" ", "").ToLower())
                    return true;
            }
            return false;
        }
        private double _areaMin;
        /// <summary>
        /// Gets or sets AreaMin property 
        /// </summary>
        public double AreaMin
        {
            get { return _areaMin; }
            set { if (_areaMin == value) return; _areaMin = value; OnPropertyChanged("AreaMin"); }
        }
        private double _areaMax;
        /// <summary>
        /// Gets or sets AreaMax property 
        /// </summary>
        public double AreaMax
        {
            get { return _areaMax; }
            set { if (_areaMax == value) return; _areaMax = value; OnPropertyChanged("AreaMax"); }
        }
        private List<string> _regions;
        /// <summary>
        /// Gets or sets Regions property 
        /// </summary>
        public List<string> Regions
        {
            get {  return _regions; }
            set { if (_regions == value) return; _regions = value; OnPropertyChanged("Regions"); }
        }

    }
    public class WorldCountriesViewSource : GeoDataViewSource
    {
        public WorldCountriesViewSource()
        {
            _dataSource = new WorldCountries();
            this.DataWorldRect = GeoRect.WorldRect;
            //this.DataCategory = GeoDataCategory.WorldCountries;
            this.DataType = GeoDataType.Shapes;
            this.FilterSettings = new WorldCountriesFilterSettings();
            this.SortSettings = new WorldCountriesSortSettings();

            this.PropertyChanged += OnViewSourcePropertyChanged;
        }

        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataSource")
            {
                this.FilterView.AreaMin = DataSource.Area.Min;
                this.FilterView.AreaMax = DataSource.Area.Max;
                this.FilterView.PopulationMin = DataSource.Population.Min;
                this.FilterView.PopulationMax = DataSource.Population.Max;
            }
        }
        private WorldCountries _dataSource;
        /// <summary>
        /// Gets or sets DataSource property 
        /// </summary>
        public WorldCountries DataSource
        {
            get {  return _dataSource; }
            set { if (_dataSource == value) return; _dataSource = value; OnPropertyChanged("DataSource"); }
        }

        //public WorldCountries DataSource { get; set; }
        public WorldCountriesFilterSettings FilterView
        {
            get { return this.FilterSettings as WorldCountriesFilterSettings; }
            set { this.FilterSettings = value; }
        }
    }
 
    public class WorldCitiesSortSettings : WorldStatsSortSettings
    {
        public WorldCitiesSortSettings()
        {
            //this.SortPropertyName = "Population";
            //this.SortDirection = ListSortDirection.Descending;
        }
    }
    public class WorldCitiesFilterSettings : WorldStatsFilterSettings
    {
        public WorldCitiesFilterSettings()
        {
        }
        public override bool Filter(object item)
        {
            var eq = item as WorldCity;
            if (eq == null) return false;

            var validCityCaptialMode = true; 
            if (CityCaptialMode != CityCaptialModes.AllCities)
            {
                validCityCaptialMode = CityCaptialMode == CityCaptialModes.CapitalCities ? eq.IsCapital : !eq.IsCapital;
            }

            return eq.Population >= this.PopulationMin && validCityCaptialMode &&
                   eq.Population <= this.PopulationMax; 
        }
        
        private CityCaptialModes _cityCaptialMode = CityCaptialModes.AllCities;
        /// <summary>
        /// Gets or sets CityCaptialMode property 
        /// </summary>
        public CityCaptialModes CityCaptialMode
        {
            get {  return _cityCaptialMode; }
            set { if (_cityCaptialMode == value) return; _cityCaptialMode = value; OnPropertyChanged("CityCaptialMode"); }
        }

        public enum CityCaptialModes
        {
            CapitalCities,
            NonCapitalCities,
            AllCities,
        }
    }
    public class WorldCitiesViewSource : GeoDataViewSource
    {
        public WorldCitiesViewSource()
        {
            this.DataSource = new WorldCities();
            //this.DataCategory = GeoDataCategory.WorldCities;
            this.DataWorldRect = GeoRect.WorldRect;
            this.DataType = GeoDataType.Locations;
            this.FilterSettings = new WorldCitiesFilterSettings();
            this.SortSettings = new WorldCitiesSortSettings();
            this.PropertyChanged += OnViewSourcePropertyChanged;
        }
        private void OnViewSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "DataSource")
            {
                this.FilterView.PopulationMin = DataSource.Population.Min;
                this.FilterView.PopulationMax = DataSource.Population.Max;
            }
        }
        private WorldCities _dataSource;
        /// <summary>
        /// Gets or sets DataSource property 
        /// </summary>
        public WorldCities DataSource
        {
            get { return _dataSource; }
            set { if (_dataSource == value) return; _dataSource = value; OnPropertyChanged("DataSource"); }
        }

        //public WorldCities DataSource { get; set; }
        public WorldCitiesFilterSettings FilterView
        {
            get { return this.FilterSettings as WorldCitiesFilterSettings; }
            set { this.FilterSettings = value; }
        }
    }

    //public class GeoDataFilter  
    //{
    //    public event FilterEventHandler Filter;
    //}

    //public class GeoDataSorter
    //{
    //   // public event FilterEventHandler Filter;
    //}

    internal interface IGeoDataSource : IEnumerable
    {
        GeoDataType DataType { get; set; }
    }

    public abstract class GeoDataSource : ObservableModel
    {

        protected GeoDataSource()
        {
            this.DataType = GeoDataType.Unknown;
            
        }
        
        public GeoDataType DataType { get; set; }
    }

    //public enum GeoSeriesTypes
    //{
    //    GeographicShapeSeries, 
    //    GeographicShapeControlSeries, 

    //    GeographicSymbolSeries, 
    //    GeographicProportionalSymbolSeries, 

    //    GeographicPolylineSeries,
    //    GeographicHighDensityScatterSeries, 
    //    GeographicContourLineSeries, 
    //    GeographicScatterAreaSeries 
    //}
    
    public enum GeoDataCategory
    {
        Unknown,
        TriangulationData,
        //Locations,
        EarthQuakes,
        WorldCities,
        WorldCountries,
        Weather
    }
    public enum GeoDataType
    {
        /// <summary>
        /// 
        /// </summary>
        Unknown,
        /// <summary>
        /// 
        /// </summary>
        Locations,
        /// <summary>
        /// 
        /// </summary>
        Shapes,
        /// <summary>
        /// 
        /// </summary>
        Lines,
        /// <summary>
        /// 
        /// </summary>
        Triangulation,
    }

}