using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Common.Data;
using IGExtensions.Common.Maps.DataSelectors;
using IGExtensions.Common.Models;
using IGExtensions.Common.Services;
using IGExtensions.Framework;
//using IGShowcase.GeographicMapBrowser.Samples;
//using IGShowcase.GeographicMapBrowser.WeatherServiceReference;
using Infragistics.Controls.Maps;

namespace IGShowcase.GeographicMapBrowser.ViewModels
{
    public class DataViewModel : ObservableModel
    {

        #region Properties
        public GeoDataViewSourceDictionary DataSources { get; set; }

        private EarthQuakesViewSource _earthQuakes;
        /// <summary>
        /// Gets or sets EarthQuakes data source of earthquake locations around the world 
        /// </summary>
        public EarthQuakesViewSource WorldEarthQuakes
        {
            get {  return _earthQuakes; }
            set { if (_earthQuakes == value) return; _earthQuakes = value; OnPropertyChanged(WorldEarthQuakesKey); }
        }
        private WorldCountriesViewSource _worldCountries;
        /// <summary>
        /// Gets or sets WorldCountries data source of countries around the world 
        /// </summary>
        public WorldCountriesViewSource WorldCountries
        {
            get {  return _worldCountries; }
            set { if (_worldCountries == value) return; _worldCountries = value; OnPropertyChanged(WorldCountriesKey); }
        }
        private WorldCitiesViewSource _worldCities;
        /// <summary>
        /// Gets or sets WorldCities data source of biggest cities around the world 
        /// </summary>
        public WorldCitiesViewSource WorldCities
        {
            get {  return _worldCities; }
            set { if (_worldCities == value) return; _worldCities = value; OnPropertyChanged(WorldCitiesKey); }
        }
        private ShapefileConverter _worldShapes;
        /// <summary>
        /// Gets or sets WorldShape data source of shapes of the world 
        /// </summary>
        public ShapefileConverter WorldShapes
        {
            get { return _worldShapes; }
            set { if (_worldShapes == value) return; _worldShapes = value; OnPropertyChanged(WorldShapesKey); }
        }
        private List<Airport> _worldAirports = new List<Airport>();
        /// <summary>
        /// Gets or sets WorldAirports data source of airports around the world
        /// </summary>
        public List<Airport> WorldAirports
        {
            get { return _worldAirports; }
            set { if (_worldAirports == value) return; _worldAirports = value; OnPropertyChanged(WorldAirportsKey); }
        }
        private WeatherDataViewSource _worldWeather;
        /// <summary>
        /// Gets or sets WorldWeather data source of locations around the world with live weather data
        /// </summary>
        public WeatherDataViewSource WorldWeather
        {
            get { return _worldWeather; }
            set { if (_worldWeather == value) return; _worldWeather = value; OnPropertyChanged(WorldWeatherKey); }
        }

        public int WorldWeatherStationsMax { get; set; }
         
        private bool _isLoadingData;
        /// <summary>
        /// Gets or sets IsLoadingData property 
        /// </summary>
        public bool IsLoadingData
        {
            get { return _isLoadingData; }
            set { if (_isLoadingData == value) return; _isLoadingData = value; OnPropertyChanged("IsLoadingData"); }
        }

        public const string WorldShapesKey = "WorldShapes";
        public const string WorldCountriesKey = "WorldCountries";
        public const string WorldCitiesKey = "WorldCities";
        public const string WorldAirportsKey = "WorldAirports";
        public const string WorldEarthQuakesKey = "WorldEarthQuakes";
        public const string WorldWeatherKey = "WorldWeather";

        public const string AustralianSitesKey = "AustralianSites";
        public const string UnitedStatesPrecipitationKey = "UnitedStatesPrecipitation";
        public const string UnitedStatesAirlineTrafficKey = "UnitedStatesAirlineTraffic";
   
        private GeoSiteDataViewSource _australianSites;
        /// <summary>
        /// Gets or sets data view source of location sites for Australia
        /// </summary>
        public GeoSiteDataViewSource AustralianSites
        {
            get { return _australianSites; }
            set { if (_australianSites == value) return; _australianSites = value; OnPropertyChanged(AustralianSitesKey); }
        }
        private TriangulationDataViewSource _unitedStatesPrecipitation;
        /// <summary>
        /// Gets or sets data view source of precipitation for the United States
        /// </summary>
        public TriangulationDataViewSource UnitedStatesPrecipitation
        {
            get { return _unitedStatesPrecipitation; }
            set { if (_unitedStatesPrecipitation == value) return; _unitedStatesPrecipitation = value; OnPropertyChanged(UnitedStatesPrecipitationKey); }
        }

        private AirlineTrafficDataViewSource _unitedStatesAirlineTraffic;
        /// <summary>
        /// Gets or sets data view source of airline traffic for the United States
        /// </summary>
        public AirlineTrafficDataViewSource UnitedStatesAirlineTraffic
        {
            get { return _unitedStatesAirlineTraffic; }
            set { if (_unitedStatesAirlineTraffic == value) return; _unitedStatesAirlineTraffic = value; OnPropertyChanged(UnitedStatesAirlineTrafficKey); }
        }
        #endregion 
       
        //TODO-MT WeatherNoaaService     
        //protected WeatherNoaaServiceSoapClient WeatherService;
        public DataViewModel()
        {
            var airportItems = new List<Airport>();

            //TODO initialize DataSources with empty data sources
            this.DataSources = new GeoDataViewSourceDictionary(); //new List<GeoDataViewSource>();

            //TODO-MT WeatherNoaaService     
            //var weatherItems = new List<WeatherNoaaData>();
            //this.DataSources.Update(WorldWeatherKey, new WeatherDataViewSource
            //{
            //    Source = weatherItems,
            //    DataSource = weatherItems
            //});
            //this.WorldWeatherStationsMax = 50;
            //this.WorldWeather = this.DataSources[WorldWeatherKey] as WeatherDataViewSource;

            //TODO-MT WeatherNoaaService     
            //this.WeatherService = new WeatherNoaaServiceSoapClient();
            //this.WeatherService.GetWeatherNoaaDataListCompleted += OnLoadWeatherDataListCompleted;
            //this.WeatherService.GetWeatherNoaaDataCompleted += OnLoadWeatherDataItemCompleted;

            this.PropertyChanged += OnLoadDataCompleted;
        }

        /// <summary>
        /// Occurs when a new data source is loaded
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> LoadDataCompleted;
        private void OnLoadDataCompleted(object sender, PropertyChangedEventArgs e)
        {
            if (LoadDataCompleted != null)
                LoadDataCompleted(this, new PropertyChangedEventArgs(e.PropertyName));

            this.IsLoadingData = false;
        }
        private void OnLoadDataCompleted(string dataSouceName)
        {
            OnLoadDataCompleted(this, new PropertyChangedEventArgs(dataSouceName));
        }
        
        protected TaskPerfomanceTimer LoadingTimer =new TaskPerfomanceTimer();
        
        #region Methods
        protected string ShapefilePath = DataStorageProvider.ShapefilesPath;
        public void LoadWorldShapefiles()
        {
            // pre-loading data
            LoadWorldCountriesShapes();

            LoadWorldCitiesLocations();

            LoadWorldEarthquakesLocations();
  
            // on-demand loading data
            //LoadAustralianSites();
        }
   
        public void LoadWorldCountriesShapes()
        {
            if (this.WorldCountries != null && this.WorldCountries.DataSource.Count > 0) 
            {
                OnLoadDataCompleted(WorldCountriesKey); return;
            }
            IsLoadingData = true;
            LoadingTimer.StartTask("LoadWorldCountriesShapes");
            var worldCountryShapes = new ShapefileConverter();
            worldCountryShapes.ImportCompleted += OnLoadWorldCountriesShapesCompleted;
            worldCountryShapes.ShapefileSource = new Uri(ShapefilePath + "world/world_countries_reg.shp", UriKind.RelativeOrAbsolute);
            worldCountryShapes.DatabaseSource =  new Uri(ShapefilePath + "world/world_countries_reg.dbf", UriKind.RelativeOrAbsolute);
        }
        public void LoadWorldCitiesLocations()
        {
            if (this.WorldCities != null && this.WorldCities.DataSource.Count > 0) 
            {
                OnLoadDataCompleted(WorldCitiesKey); return;
            }
            IsLoadingData = true;
            LoadingTimer.StartTask("LoadWorldCitiesLocations");
            var worldCitiesShapes = new ShapefileConverter();
            worldCitiesShapes.ImportCompleted += OnLoadWorldCitiesLocationsCompleted;
            worldCitiesShapes.ShapefileSource = new Uri(ShapefilePath + "world/places/cities_population.shp", UriKind.RelativeOrAbsolute);
            worldCitiesShapes.DatabaseSource =  new Uri(ShapefilePath + "world/places/cities_population.dbf", UriKind.RelativeOrAbsolute);
        }
        public void LoadWorldEarthquakesLocations()
        {
            if (this.WorldEarthQuakes != null && this.WorldEarthQuakes.DataSource.Count > 0)  
            {
                OnLoadDataCompleted(WorldEarthQuakesKey); return;
            }
           
            IsLoadingData = true;
            LoadingTimer.StartTask("LoadWorldEarthquakesLocations");
            var worldEarthquakes = new ShapefileConverter();
            worldEarthquakes.ImportCompleted += OnLoadWorldEarthquakesLocationsCompleted;
            worldEarthquakes.ShapefileSource = new Uri(ShapefilePath + "world/events/earthquakes_usgs2010.shp", UriKind.RelativeOrAbsolute);
            worldEarthquakes.DatabaseSource =  new Uri(ShapefilePath + "world/events/earthquakes_usgs2010.dbf", UriKind.RelativeOrAbsolute);
        }
        public void LoadUnitedStatesPrecipitation()
        {
            if (this.UnitedStatesPrecipitation != null && 
                this.UnitedStatesPrecipitation.DataSource != null && 
                this.UnitedStatesPrecipitation.DataSource.Points.Count > 0)
            {
                OnLoadDataCompleted(UnitedStatesPrecipitationKey); return;
            }
            IsLoadingData = true;
            LoadingTimer.StartTask("LoadUnitedStatesPrecipitation");
            var triangultionDataConverter = new ItfConverter();
            triangultionDataConverter.ImportCompleted += OnLoadUnitedStatesPrecipitationCompleted;
            //triangultionDataConverter.Source = new Uri(ShapefilePath + "weather/precipitation/precip_1day_observed_20110831.itf", UriKind.RelativeOrAbsolute);
            triangultionDataConverter.Source = new Uri(ShapefilePath + "weather/precipitation/precip_1hour_observed_2011091820.itf", UriKind.RelativeOrAbsolute);
            
        }
        public void LoadUnitedStatesAirlineTraffic()
        {
            if (this.UnitedStatesAirlineTraffic != null &&
                this.UnitedStatesAirlineTraffic.FlightsDataSource != null &&
                this.UnitedStatesAirlineTraffic.FlightsDataSource.Count > 0)
            {
                OnLoadDataCompleted(UnitedStatesAirlineTrafficKey); return;
            }

            IsLoadingData = true;
            LoadingTimer.StartTask("LoadUnitedStatesAirlineTraffic");
            var dataProvider = new AirlinesDataProvider();
            dataProvider.LoadDataCompleted += OnLoadUnitedStatesAirlineTrafficCompleted;
            dataProvider.LoadAmericanFlightsAsync();
        }
        public void LoadWorldAirports()
        {
            if (this.WorldAirports != null && this.WorldAirports.Count > 0)  
            {
                OnLoadDataCompleted(WorldAirportsKey); return;
            }
           
            IsLoadingData = true;
            LoadingTimer.StartTask("LoadWorldAirports");
            var airportMajorCodesProvider = new AirlinesDataProvider();
            airportMajorCodesProvider.LoadDataCompleted += OnLoadWorldAirportsCodesCompleted;
            airportMajorCodesProvider.LoadWorldsAirportsAsync();
        }
        public void LoadWorldWeather()
        {
            //TODO-MT implement
            //if (this.WorldWeather != null && this.WorldWeather.DataSource.Count > 0)
            //{
            //    OnLoadDataCompleted(WorldWeatherKey); return;
            //}
           
            //IsLoadingData = true;
            //LoadingTimer.StartTask("LoadWorldWeather");
            //int counter = 0;
            //var codes = new ArrayOfString();
            //TODO-MT add logic for excluding airports that are close too previous airports based on location
            //foreach (var airport in this.WorldAirports)
            //{
            //    if (counter < WorldWeatherStationsMax)
            //        codes.Add(airport.CodeICAO);
            //    counter++;
            //}
            //this.WeatherService.GetWeatherNoaaDataListAsync(codes);
        }
        public void LoadAustralianSites()
        {
            if (this.AustralianSites != null && this.AustralianSites.DataSource.Count > 0)
            {
                OnLoadDataCompleted(AustralianSitesKey); return;
            }
            IsLoadingData = true;
            LoadingTimer.StartTask("LoadAustralianSites");
            //var airportMajorCodesProvider = new AirlinesDataProvider();
            //airportMajorCodesProvider.LoadDataCompleted += OnWorldAirportsMajorCodesLoaded;
            //airportMajorCodesProvider.LoadWorldsAirportsAsync();
            var csvDataProvider = new CsvDataProvider();
            csvDataProvider.GetDataCompleted += OnLoadAustralianSitesCompleted;
            csvDataProvider.GetDataAsync("australian_sites.csv");
        }

        #endregion
        #region Event Handlers
        private void OnLoadUnitedStatesAirlineTrafficCompleted(object sender, AirlinesDataCompletedEventArgs e)
        {
            var dataProvider = sender as AirlinesDataProvider;
            if (dataProvider != null)
            {
                //var minTime = dataProvider.AmericanFlightTimeMin.Subtract(TimeSpan.FromMinutes(1));
                //var maxTime = dataProvider.AmericanFlightTimeMax.Add(TimeSpan.FromMinutes(1));
                var minTime = DateTime.MaxValue;
                var maxTime = DateTime.MinValue;

                var airports = dataProvider.Airports;
                var flights = new List<AirlineFlight>(); // dataProvider.Flights;
                var flightFilter = 4;
                foreach (var flight in dataProvider.Flights)
                {
                    if (flightFilter % 4 == 0)
                    {
                        flights.Add(flight);
                        minTime = new DateTime(System.Math.Min(minTime.Ticks, flight.DepartureTime.Ticks));
                        maxTime = new DateTime(System.Math.Max(maxTime.Ticks, flight.ArrivalTime.Ticks));
                    }
                    flightFilter++;
                }
                minTime = minTime.Subtract(TimeSpan.FromMinutes(1));
                maxTime = maxTime.Add(TimeSpan.FromMinutes(1));

                var dataViewSource = new AirlineTrafficDataViewSource
                {
                    Source = airports,
                    AirportsDataSource = airports,
                    FlightsDataSource = flights,
                    DataSourceTrademark = CommonStrings.SourceData_USAT,
                };
                dataViewSource.DataMotionFramework.MotionSlider.MinValue = minTime;
                dataViewSource.DataMotionFramework.MotionSlider.MaxValue = maxTime;
                dataViewSource.DataMotionFramework.MotionSlider.Value = minTime;
                dataViewSource.DataMotionFramework.MotionSlider.Interval = TimeSpan.FromMinutes(15);
                dataViewSource.DataMotionFramework.MotionUpdateInterval = TimeSpan.FromSeconds(0.25);
                //dataViewSource.DataMotionFramework.MotionSlider.Value = maxTime;)

                this.DataSources.Update(UnitedStatesAirlineTrafficKey, dataViewSource);

                _unitedStatesAirlineTraffic = this.DataSources[UnitedStatesAirlineTrafficKey] as AirlineTrafficDataViewSource;
                OnLoadDataCompleted(UnitedStatesAirlineTrafficKey);  
                //this.UnitedStatesAirlineTraffic = this.DataSources[UnitedStatesAirlineTrafficKey] as AirlineTrafficDataViewSource;

                LoadingTimer.StopTask("LoadUnitedStatesAirlineTraffic");
            }
        }
        private void OnLoadUnitedStatesPrecipitationCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var triangultionConverter = sender as ItfConverter;
            if (triangultionConverter != null)
            {
                var items = triangultionConverter.TriangulationSource;

                this.DataSources.Update(UnitedStatesPrecipitationKey, new TriangulationDataViewSource { Source = items.Points, DataSource = items });
                this.UnitedStatesPrecipitation = this.DataSources[UnitedStatesPrecipitationKey] as TriangulationDataViewSource;
                LoadingTimer.StopTask("LoadUnitedStatesPrecipitation");
            }
        }
        private void OnLoadAustralianSitesCompleted(object sender, CsvDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                DebugManager.LogError(e.Error); return;
            }

            var geoSites = new List<GeoSite>();
            try
            {
                foreach (var line in e.Result)
                {
                    var vals = line.Split(',');
                    if (vals.Length >= 3)
                    {
                        var name = vals[0];
                        var latitude = double.Parse(vals[1]);
                        var longitude = double.Parse(vals[2]);
                        var place = new GeoSite { Name = name, Longitude = longitude, Latitude = latitude };
                        geoSites.Add(place);
                    }
                }
                //var dataSourceView = new GeoSiteDataViewSource {Source = geoSites, DataSource = geoSites};
                //this.DataSources.Update(AustralianSitesKey, dataSourceView);
                //this.AustralianSites = dataSourceView;

                this.DataSources.Update(AustralianSitesKey, new GeoSiteDataViewSource { Source = geoSites, DataSource = geoSites });
                this.AustralianSites = this.DataSources[AustralianSitesKey] as GeoSiteDataViewSource;

                LoadingTimer.StopTask("LoadAustralianSites");
            }
            catch (Exception ex)
            {
                DebugManager.LogError(ex);  
            }
        }

        private void OnLoadWorldAirportsCodesCompleted(object sender, AirlinesDataCompletedEventArgs e)
        {
            this.WorldAirports = e.Airports;
            // LoadWorldWeather();
            LoadingTimer.StopTask("LoadWorldAirports");
        }
        //TODO-MT WeatherNoaaService     
        //private void OnLoadWeatherDataListCompleted(object sender, GetWeatherNoaaDataListCompletedEventArgs e)
        //{
        //    var items = new List<WeatherNoaaData>();
        //    foreach (var location in e.Result)
        //    {
        //        if (!double.IsNaN(location.Temperature))
        //            items.Add(location);
        //        //Debug.WriteLine(ToString(location));
        //        //Debug.WriteLine(location.ToString());
        //    }
        //    this.DataSources.Update(WorldWeatherKey, new WeatherDataViewSource { Source = items, DataSource = items });
        //    this.WorldWeather = this.DataSources[WorldWeatherKey] as WeatherDataViewSource;

        //    LoadingTimer.StopTask("LoadWorldWeather");
        //}
        //private void OnLoadWeatherDataItemCompleted(object sender, GetWeatherNoaaDataCompletedEventArgs e)
        //{
        //    //TODO add updating weather data with individual data items
        //    //_worldWeather = new List<WeatherStation>();
        //    var weatherStations = e.Result;
        //}

        private void OnLoadWorldCountriesShapesCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var shapes = sender as ShapefileConverter;
            var items = GeoDataParser.ProcessWorldCountries(shapes);
          
            //var dataSource = new WorldCountriesViewSource {Source = items, DataSource = items};
            //dataSource.ItemsSourceKey = "WorldCountriesShapefile";
            //dataSource.FilterSettings.ItemsSourceKey = "WorldCountriesShapefile";
            //this.MapViewModel.DataSources.Update(dataSource.ItemsSourceKey, dataSource);
            //this.MapViewModel.WorldCountriesFilter = dataSource.FilterSettings as WorldCountriesFilterSettings;

            var dataViewSource = new WorldCountriesViewSource { Source = items, DataSource = items, DataSourceTrademark = CommonStrings.SourceData_USNA};
            this.DataSources.Update(WorldCountriesKey, dataViewSource);
            //this.DataSources.Update(WorldCountriesKey, new WorldCountriesViewSource { Source = items, DataSource = items });

            this.WorldCountries = this.DataSources[WorldCountriesKey] as WorldCountriesViewSource;
            this.WorldShapes = shapes;

            LoadingTimer.StopTask("LoadWorldCountriesShapes");
        }
        private void OnLoadWorldCitiesLocationsCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var shapes = sender as ShapefileConverter;
            var items = GeoDataParser.ProcessWorldCities(shapes);

            var dataViewSource = new WorldCitiesViewSource { Source = items, DataSource = items, DataSourceTrademark = CommonStrings.SourceData_GeoCommons };
            this.DataSources.Update(WorldCitiesKey, dataViewSource);
            //this.DataSources.Update(WorldCitiesKey, new WorldCitiesViewSource { Source = items, DataSource = items });
            this.WorldCities = this.DataSources[WorldCitiesKey] as WorldCitiesViewSource;

            LoadingTimer.StopTask("LoadWorldCitiesLocations");
        }
        private void OnLoadWorldEarthquakesLocationsCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var shapes = sender as ShapefileConverter;
            var items = GeoDataParser.ProcessEarthQuakes(shapes);

            this.DataSources.Update(WorldEarthQuakesKey, new EarthQuakesViewSource { Source = items, DataSource = items });
            this.WorldEarthQuakes = this.DataSources[WorldEarthQuakesKey] as EarthQuakesViewSource;
         
            LoadingTimer.StopTask("LoadWorldEarthquakesLocations");
        } 


        #endregion
    }
    public class TaskPerfomanceTimer 
    {
        //protected DateTime TaskStartTime;
        //protected DateTime TaskStopTime;
        protected Dictionary<string, TaskPerfomanceStatus> TaskDictionary;
        //protected TaskPerfomanceStatus TaskCurrent;
        public TaskPerfomanceStatus TaskCurrent { get; private set; }

        public TaskPerfomanceTimer()
        {
            TaskDictionary = new Dictionary<string, TaskPerfomanceStatus>();
            TaskCurrent = new TaskPerfomanceStatus("Current");
            //TaskStartTime = DateTime.Now;
            //TaskStopTime = TaskStartTime;
        }

        public void StartCurrent()
        {
            StartTask("Current");
        }
        public void StopCurrent()
        {
            StartTask("Current");
        }

        public void StartTask(string taskName)
        {
            if (TaskDictionary.ContainsKey(taskName))
            {
                TaskDictionary[taskName].Start();
            }
            else
            {
                var task = new TaskPerfomanceStatus(taskName);
                task.Start();
                TaskDictionary.Add(taskName, task);
                TaskCurrent = task;
            }
        }
        public void StopTask(string taskName)
        {
            if (TaskDictionary.ContainsKey(taskName))
            {
                TaskDictionary[taskName].Stop();
                var duration = TaskDictionary[taskName].Duration;
                Debug.WriteLine("Task-> " + taskName + " completed in: " + duration.TotalSeconds.ToString("0.000 second(s)"));
            }
            else
            {
                var task = new TaskPerfomanceStatus(taskName);
                TaskDictionary.Add(taskName, task);
                TaskCurrent = task;
            }
        }
       
        public void Update(string taskKey, TaskPerfomanceStatus taskStatus)
        {
            if (TaskDictionary.ContainsKey(taskKey))
            {
                TaskDictionary[taskKey] = taskStatus;
            }
            else
            {
                TaskDictionary.Add(taskKey, taskStatus);
            }
        }
    }
    public class TaskPerfomanceStatus
    {
        //protected string TaskName;

        public string TaskName { get; private set; }

        protected DateTime TaskStartTime;
        protected DateTime TaskStopTime;
        public TimeSpan Duration { get { return TaskStopTime.Subtract(TaskStartTime); } }
        public TaskPerfomanceStatus()
        {
            TaskStartTime = DateTime.Now;
            TaskStopTime = TaskStartTime;
        }
        public TaskPerfomanceStatus(string taskName)
        {
            TaskName = taskName;
            TaskStartTime = DateTime.Now;
            TaskStopTime = TaskStartTime;
        }
        public void Start()
        {
            TaskStartTime = DateTime.Now;
            TaskStopTime = TaskStartTime;
        }
        public void Stop()
        {
            TaskStopTime = DateTime.Now;
        }

    }
    //public class LoadDataSourceCompletedEventArgs : EventArgs
    //{
    //    public LoadDataSourceCompletedEventArgs(string dataSourceName)
    //    {
    //        this.DataSourceName = dataSourceName;
    //    }

    //    public string DataSourceName { get; set; }
    //}
}