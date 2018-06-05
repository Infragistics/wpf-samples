using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using IGExtensions.Common.Models;
using IGExtensions.Framework;

namespace IGExtensions.Common.Services
{
    public enum EarthQuakeRequestMode
    {
        Default,
        LastHour,
        LastDay,
        LastWeek,
        LastMonth,
        //TODO load from shapefile/server
        //LastQuarter,
        //LastYear,
    }

    public class EarthQuakesChangedEventArgs : EventArgs
    {
        public EarthQuakesChangedEventArgs()  
        {
            Result = new List<EarthQuakeData>();
        }
        public EarthQuakesChangedEventArgs(List<EarthQuakeData> data)
        {
            Result = data;
        }
        public List<EarthQuakeData> Result { get; set; }
    }
    
    /// <summary>
    /// Represents a service for retrieving information about latest earth quakes from USGS
    /// </summary>
    public sealed class EarthQuakesService : ObservableModel
    {
        #region Constants

       
        /// <summary>
        /// Refer to http://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
        /// </summary>
        private const string UsgsEarthQuakesFormat = UsgsEarthQuakesFeed + "{0}_{1}.geojson";
        
        private const string UsgsEarthQuakesFeed = "http://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/";
        private const string UsgsEarthQuakesInLastHour = UsgsEarthQuakesFeed + "all_hour.geojson";
        private const string UsgsEarthQuakesInLastDay = UsgsEarthQuakesFeed + "all_day.geojson";
        private const string UsgsEarthQuakesInLastWeek = UsgsEarthQuakesFeed + "all_week.geojson";
        private const string UsgsEarthQuakesInLastMonth = UsgsEarthQuakesFeed + "all_month.geojson";

        internal string UsgsEarthQuakesLink = UsgsEarthQuakesInLastWeek;
       
        private const string ContinentBoudariesFile = "/IGExtensions.Common.Services;component/Assets/Data/ContinentsBoundaries.xml";
      
        #endregion
        #region Fields

        internal ContinentsList EartQuakeRegions;
        internal Dictionary<string, EarthQuakeData> EarthQuakeDataCache;
        internal DispatcherTimer Timer = new DispatcherTimer();
       
        internal WebClient UsgsWebClient;

        internal DateTime ProcessStartTime;
        internal DateTime ProcessStopTime;
        internal bool HasNewEarthQuakes = false;
        internal bool ForceEarthQuakeUpdate = false;
     
        public List<string> EartQuakeRegionNames = new List<string>();

        #endregion

        #region Properties

        private DateTime _lastUpdated;
        public DateTime LastUpdated
        {
            get { return _lastUpdated; }
            private set
            {
                if (_lastUpdated != value)
                {
                    _lastUpdated = value;
                    OnPropertyChanged("LastUpdated");
                }
            }
        }

        private EarthQuakeData _latestEarthquake = null;
        public EarthQuakeData LatestEarthQuake
        {
            get { return _latestEarthquake; }
            private set
            {
                if (_latestEarthquake != value)
                {
                    _latestEarthquake = value;
                    OnPropertyChanged("LatestEarthQuake");
                }
            }
        }
        private List<EarthQuakeData> _earthQuakes = new List<EarthQuakeData>();
        public List<EarthQuakeData> EarthQuakes
        {
            get { return _earthQuakes; }
            private set
            {
                if (_earthQuakes != value)
                {
                    _earthQuakes = value;
                    OnPropertyChanged("EarthQuakes");
                }
            }
        }
        public bool IsStarted { get; private set; }
        public bool IsInitialized { get; private set; }
        
        private List<string> _selectedRegions = new List<string>();
        public List<string> SelectedRegions
        {
            get { return _selectedRegions; }
            set
            {
                if (_selectedRegions != value)
                {
                    _selectedRegions = value;
                    OnPropertyChanged("SelectedRegions");
                    UpdateEarthQuakes(true);
                }
            }
        }

        private TimeSpan _requestInterval = TimeSpan.FromSeconds(120); // 2 minutes
        public TimeSpan RequestInterval
        {
            get { return _requestInterval; }
            set
            {
                if (_requestInterval != value && value.TotalSeconds >= 30)
                {
                    _requestInterval = value;
                    Timer.Interval = _requestInterval;
                    OnPropertyChanged("RequestInterval");
                }
            }
        }
        private EarthQuakeRequestMode _requestMode = EarthQuakeRequestMode.LastDay;
        public EarthQuakeRequestMode RequestMode
        {
            get { return _requestMode; }
            set
            {
                if (_requestMode != value)
                {
                    _requestMode = value;
                    TimeFilter.IsCustom = false;
                    OnPropertyChanged("RequestMode");
                    UpdateServiceUrl();
                }
            }
        }
        private void UpdateServiceUrl()
        {
            string time = "week";
            //if (TimeFilter.IsCustom)
            //{
            //    var timeSpan = TimeFilter.Max.Subtract(TimeFilter.Min);
            //    if (TimeFilter.Max > DateTime.Now)
            //    {
            //        timeSpan = DateTime.Now.Subtract(TimeFilter.Min);
            //    }
            //    if (timeSpan.Days > 7) time = "month";
            //    else if (timeSpan.Days > 1) time = "week";
            //    else if (timeSpan.Hours > 1) time = "day";
            //    else if (timeSpan.Hours <= 1) time = "hour";
            //}
            //else
            //{
                switch (this.RequestMode)
                {
                    case EarthQuakeRequestMode.LastHour: time = "hour"; break;
                    case EarthQuakeRequestMode.LastDay: time = "day"; break;
                    case EarthQuakeRequestMode.LastWeek: time = "week"; break;
                    case EarthQuakeRequestMode.LastMonth: time = "month"; break;
                    //case EarthQuakeRequestMode.LastYear: time = "month"; break;
                    default: time = "week"; break;
                }
            //}
            string magnitude;
            if (MagnitudeFilter.Min >= 4.5) magnitude = "4.5";
            else if (MagnitudeFilter.Min >= 2.5) magnitude = "2.5";
            else if (MagnitudeFilter.Min >= 1.0) magnitude = "1.0";
            else //if (MagnitudeMin < 1.0) 
                magnitude = "all";

            UsgsEarthQuakesLink = string.Format(UsgsEarthQuakesFormat, magnitude, time);
        }

        private DateTime GetActualTimeMin()
        {
            var timeMin = DateTime.MinValue;
            switch (RequestMode)
            {
                case EarthQuakeRequestMode.LastHour: timeMin = DateTime.Now.AddHours(-1); break;
                case EarthQuakeRequestMode.LastDay: timeMin = DateTime.Now.AddDays(-1); break;
                case EarthQuakeRequestMode.LastWeek: timeMin = DateTime.Now.AddDays(-7); break;
                case EarthQuakeRequestMode.LastMonth: timeMin = DateTime.Now.AddMonths(-1); break;
                //case EarthQuakeRequestMode.LastYear: timeMin = DateTime.Now.AddYears(-1); break;
                default: timeMin = DateTime.Now.AddDays(-7); break;
            }
            return timeMin;
        }

        private TimeFilter _timeFilter = new TimeFilter(DateTime.MinValue, DateTime.MaxValue);
        public TimeFilter TimeFilter
        {
            get { return _timeFilter; }
            set
            {
                if (_timeFilter != value)
                {
                   _timeFilter = value;
                    _timeFilter.PropertyChanged += OnFilterChanged;
                    OnPropertyChanged("TimeFilter");
                    UpdateServiceUrl();
                    UpdateEarthQuakes();
                }
            }
        }

        private DoubleFilter _magnitudeFilter = new DoubleFilter(4.5, 10);
        public DoubleFilter MagnitudeFilter
        {
            get { return _magnitudeFilter; }
            set
            {
                if (_magnitudeFilter != value)
                {
                    _magnitudeFilter = value;
                    _magnitudeFilter.PropertyChanged += OnFilterChanged;
                    OnPropertyChanged("MagnitudeFilter");
                    UpdateEarthQuakes();
                }
            }
        }

        private DoubleFilter _depthFilter = new DoubleFilter(0, 100000);
        public DoubleFilter DepthFilter
        {
            get { return _depthFilter; }
            set
            {
                if (_depthFilter != value)
                {
                    _depthFilter = value;
                    _depthFilter.PropertyChanged += OnFilterChanged;
                    OnPropertyChanged("DepthFilter");
                    UpdateEarthQuakes();
                }
            }
        }

        private DoubleFilter _longitudeFilter = new DoubleFilter(-180, 180);
        public DoubleFilter LongitudeFilter
        {
            get { return _longitudeFilter; }
            set
            {
                if (_longitudeFilter != value)
                {
                    _longitudeFilter = value;
                    _longitudeFilter.PropertyChanged += OnFilterChanged;
                    OnPropertyChanged("LongitudeFilter");
                    UpdateEarthQuakes();
                }
            }
        }
   
        private DoubleFilter _latitudeFilter = new DoubleFilter(-90, 90);
        public DoubleFilter LatitudeFilter
        {
            get { return _latitudeFilter; }
            set
            {
                if (_latitudeFilter != value)
                {
                    _latitudeFilter = value;
                    _latitudeFilter.PropertyChanged += OnFilterChanged;
                    OnPropertyChanged("LatitudeFilter");
                    UpdateEarthQuakes();
                }
            }
        }

        void OnFilterChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateServiceUrl();
            UpdateEarthQuakes(true);
        }

        #endregion

        public EarthQuakesService()
        {
            UsgsEarthQuakesLink = UsgsEarthQuakesInLastWeek;

            DebugManager.LoggingLevel = DebugLogLevel.All;

            EarthQuakeDataCache = new Dictionary<string, EarthQuakeData>();

            UsgsWebClient = new WebClient();
            UsgsWebClient.OpenReadCompleted += OnEarthQuakesRequested;
            
            Timer.Interval = RequestInterval; 
            Timer.Tick += OnTimerTick;

            Initialize();
        }

        #region Events

        private void OnTimerTick(object sender, EventArgs e)
        {
            RequestEarthQuakes();
        }

        /// <summary>
        /// Occurs when the list of earthquakes is changed
        /// </summary>
        public event EventHandler<EarthQuakesChangedEventArgs> EarthQuakesChanged;
        //public event EventHandler EarthQuakesChanged;
        internal void OnEarthQuakesChanged(object sender, EarthQuakesChangedEventArgs e)
        {
            var handler = this.EarthQuakesChanged;
            if (handler != null)
                handler(sender, e);
        }
         
        /// <summary>
        /// Called when [RequestEarthQuakes is completed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OnEarthQuakesRequested(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Cancelled || e.Error != null)
            {
                DebugManager.LogWarning("EarthQuakeService->Failed on request of earthquakes: \n" + e.Error);
                return;
            }

            ProcessStopTime = DateTime.Now;
            var time = String.Format("{0} seconds", ProcessStopTime.Subtract(ProcessStartTime).TotalSeconds);
            DebugManager.LogTrace("EarthQuakeService->Requesting earthquakes completed in " + time);
          
            var result = ProcessEarthQuakes(e.Result);
            if (e.UserState != null)
            {
                var callback = (Action<IList<EarthQuakeSummary>>)e.UserState;
                SynchronizationContext.Current.Post(x => callback((IList<EarthQuakeSummary>)x), result);
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Called by an application in order to initialize the application extension service.
        /// </summary>
        public void StartService()
        {
            if (IsStarted)
            {
                StopService();
            }
            else
            {
                DebugManager.LogTrace("EarthQuakeService->Starting earthquakes service... ");
                IsStarted = true;
                Initialize();
                Timer.Start();

                RequestEarthQuakes();
            }
        }

        public void StopService()
        {
            IsStarted = false;
            Timer.Stop();
            DebugManager.LogTrace("EarthQuakeService->Stopping earthquakes service... ");
        }

        /// <summary>
        /// Request earthquakes in last hour
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RequestEarthQuakesInLastHour(Action<IList<EarthQuakeData>> callback = null)
        {
            RequestEarthQuakes(UsgsEarthQuakesInLastHour, callback);
        }
        /// <summary>
        /// Request earthquakes in last day 
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RequestEarthQuakesInLastDay(Action<IList<EarthQuakeData>> callback = null)
        {
            RequestEarthQuakes(UsgsEarthQuakesInLastDay, callback);
        }
        /// <summary>
        /// Request earthquakes in last week
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RequestEarthQuakesInLastWeek(Action<IList<EarthQuakeData>> callback = null)
        {
            RequestEarthQuakes(UsgsEarthQuakesInLastWeek, callback);
        }
        /// <summary>
        /// Request earthquakes in last month
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RequestEarthQuakesInLastMonth(Action<IList<EarthQuakeData>> callback = null)
        {
            RequestEarthQuakes(UsgsEarthQuakesInLastMonth, callback);
        }
        /// <summary>
        /// Request earthquakes in last week
        /// </summary>
        /// <param name="callback">The callback.</param>
        public void RequestEarthQuakes(Action<IList<EarthQuakeData>> callback = null)
        {
            RequestEarthQuakes(UsgsEarthQuakesLink, callback);
        }

        internal string EarthQuakesLink = string.Empty;
        /// <summary>
        /// Request earthquakes with no default callback method
        /// </summary>
        private void RequestEarthQuakes(string url, Action<IList<EarthQuakeData>> callback = null)
        {
            if (!IsInitialized) return;
            
            HasNewEarthQuakes = false;
           
            //Initialize();

            if (EarthQuakesLink != url)
            {
                EarthQuakesLink = url;
                ForceEarthQuakeUpdate = true;

            }
            
            var summary = EarthQuakesLink.Substring(EarthQuakesLink.LastIndexOf('/'));
            summary = summary.Replace(".geojson", "").Replace("/", "").Replace("_", " ");
            DebugManager.LogTrace("EarthQuakeService->Requesting earthquakes for " + summary);
        
            ProcessStartTime = DateTime.Now;
            var uri = new Uri(EarthQuakesLink);
            UsgsWebClient.OpenReadAsync(uri, callback);
        }
       

        #endregion

       
        #region Private Methods
        private void Initialize()
        {
            if (EartQuakeRegions == null)
            {
                IsInitialized = true;

                DebugManager.Log("EarthQuakeService->Initializing earthquake regions...");
                EartQuakeRegions = LoadEartQuakeRegions(ContinentBoudariesFile);
                var regions = EartQuakeRegions.Select(item => item.Name.Replace("_", " ")).ToList();
                regions.Add("Other");
                EartQuakeRegionNames = regions;
                SelectedRegions = EartQuakeRegionNames;

                _latitudeFilter.PropertyChanged += OnFilterChanged;
                _longitudeFilter.PropertyChanged += OnFilterChanged;
                _depthFilter.PropertyChanged += OnFilterChanged;
                _magnitudeFilter.PropertyChanged += OnFilterChanged;
                _timeFilter.PropertyChanged += OnFilterChanged;
            }
        }

        private static ContinentsList LoadEartQuakeRegions(string fileName)
        {
            var list = new ContinentsList();
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName Is Missing:" + fileName);
            }
            try
            {
                var uri = new Uri(fileName, UriKind.Relative);
                var info = Application.GetResourceStream(uri);
                if (info != null)
                {
                    var stream = info.Stream;
                    var reader = XmlReader.Create(stream);
                    var serializer = new XmlSerializer(typeof(ContinentsList));
                    list = (ContinentsList)(serializer.Deserialize(reader));
                    foreach (var item in list)
                    {
                        item.Name = item.Name.Replace("_", " ");
                    }
                }
            }
            catch (Exception ex)
            {
                var error = "EarthQuakeService->Failed on loading world regions. \n" + ex;
                DebugManager.LogError(error);
            }
            return list;
     
        }
        
        public bool IsFirstProcessError = true;

        /// <summary>
        /// Processes the stock details.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="forceUpdate">The data.</param>
        /// <returns></returns>
        public IList<EarthQuakeData> ProcessEarthQuakes(Stream data, bool forceUpdate = false)
        {
            
            ProcessStartTime = DateTime.Now;
            DebugManager.LogTrace("EarthQuakeService->Processing earthquakes... ");
        
            var result = new List<EarthQuakeData>();
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(EarthQuakeDataRoot));
                var dataRoot = (EarthQuakeDataRoot)serializer.ReadObject(data);

                foreach (var summary in dataRoot.EarthQuakes)
                {
                    var properties = summary.Properties;
                    var earthquake = new EarthQuakeData();
                    earthquake.Title = properties.Place.ToTitle();
                    if (earthquake.Title.Contains("km"))
                    {
                        var words = earthquake.Title.Split(' ').ToList();
                        if (words.Count > 2) words[1] = words[1].ToUpper();
                        earthquake.Title = words.ToSentence();
                    }
                    earthquake.Code = properties.Code;
                    if (properties.Updated.HasValue)
                        earthquake.Updated =properties.Updated.Value.ToLocalTime();
                   
                    //DebugManager.LogData("EarthQuakeService->Processing earthquake: " + earthquake.Title);
                    if (IsNewEarthQuake(earthquake))
                    {
                        if (properties.Time.HasValue)
                            earthquake.Time = properties.Time.Value.ToLocalTime();
                        if (properties.Magnitude.HasValue)
                            earthquake.Magnitude = properties.Magnitude.Value;

                        earthquake.EventCodes = properties.EventCodes;
                        earthquake.Significance = properties.Significance.ToInteger();
                        earthquake.Stations = properties.Stations.ToInteger();
                        earthquake.Reports = properties.Reports.ToInteger();
                        earthquake.Intensity = properties.Intensity.ToInteger();
                        earthquake.Tsunami = properties.Tsunami != null && properties.Tsunami != "null";
                        earthquake.Sources = properties.Sources;
                        earthquake.Network = properties.Network;
                        earthquake.Link = new Uri(properties.Url);
                        earthquake.Details = new Uri(properties.Detail);
                        
                        var geometry = summary.Geometry;
                        if (geometry != null && geometry.Coordinates != null)
                        {
                            var coordinates = summary.Geometry.Coordinates;
                            if (coordinates.Count > 0) earthquake.Longitude = coordinates[0];
                            if (coordinates.Count > 1) earthquake.Latitude = coordinates[1];
                            if (coordinates.Count > 2) earthquake.Depth = Math.Abs(coordinates[2]);
                            earthquake.Region = EartQuakeRegions.FindRegion(earthquake.ToPoint()) ?? "Other";
                        }
                        
                        UpdateEarthQuakeCache(earthquake);
                    }
                }

                ProcessStopTime = DateTime.Now;
                var time = String.Format("{0} seconds", ProcessStopTime.Subtract(ProcessStartTime).TotalSeconds);
                DebugManager.LogTrace("EarthQuakeService->Processing earthquakes completed in " + time);

                UpdateEarthQuakes(forceUpdate);

                result = this.EarthQuakes;
            }
            catch (Exception ex)
            {
                var error = "EarthQuakeService->Failed on processing earthquake JSON object. \n" + ex;
                DebugManager.LogError(error);
                if (IsFirstProcessError)
                {
                    IsFirstProcessError = false;
                    //throw new Exception(error);
                }
            }

            return result;
        }

        private bool IsNewEarthQuake(EarthQuakeData earthquake)
        {
            var key = earthquake.Code; //earthquake.Updated + earthquake.Code;
            if (!EarthQuakeDataCache.ContainsKey(key) ||
                 EarthQuakeDataCache[key].Updated < earthquake.Updated)
                return true;

            return false;
        }

        public bool HasAnyEarthQuakes { get { return EarthQuakeDataCache.Values.Any(); } }
        
        private void UpdateEarthQuakeCache(EarthQuakeData earthquake)
        {
            var key = earthquake.Code; // earthquake.Updated + earthquake.Code;
            if (EarthQuakeDataCache.ContainsKey(key))
            {
                if (EarthQuakeDataCache[key].Updated < earthquake.Updated)
                {
                    //EarthQuakesList.Add(earthquake);
                    DebugManager.LogData("EarthQuakeService->Updating earthquake: " + earthquake.ToString());
                    EarthQuakeDataCache[key] = earthquake;
                    HasNewEarthQuakes = true;
                }
            }
            else
            {
                EarthQuakeDataCache.Add(key, earthquake);
                HasNewEarthQuakes = true;
                //EarthQuakesList.Add(earthquake);
                //DebugManager.LogData("EarthQuakeService->Adding earthquake: " + earthquake.ToString());
            }
        }

        private void UpdateEarthQuakes(bool forceUpdate = false)
        {
            if (HasNewEarthQuakes || ForceEarthQuakeUpdate || forceUpdate)
            {
                HasNewEarthQuakes = false;
                ForceEarthQuakeUpdate = false;

                ProcessStartTime = DateTime.Now;
                DebugManager.LogTrace("EarthQuakeService->Updating earthquakes... ");
                var matchEarthQuakes = EarthQuakeDataCache.Values.Where(IsFilterMatch).ToList();
                
                if (Debugger.IsAttached)
                {
                    var otherEarthQuakes = new List<EarthQuakeData>();
                    foreach (var value in EarthQuakeDataCache.Values)
                    {
                        if (!matchEarthQuakes.Contains(value))
                        {
                            otherEarthQuakes.Add(value);
                        }
                    }
                }
               
                if (matchEarthQuakes.Count > 0)
                {
                    // order from older to newer
                    matchEarthQuakes.Sort((x, y) => +(Comparer<DateTime>.Default.Compare(x.Updated, y.Updated)));
                    LatestEarthQuake = matchEarthQuakes.Last();
                }
                DebugManager.LogTrace("EarthQuakeService->Updating earthquakes: " + EarthQuakes.Count + "->" + matchEarthQuakes.Count);

                EarthQuakes = matchEarthQuakes;
                OnEarthQuakesChanged(this, new EarthQuakesChangedEventArgs(matchEarthQuakes));
                LastUpdated = DateTime.UtcNow;
      
                ProcessStopTime = DateTime.Now;
                var time = String.Format("{0} seconds", ProcessStopTime.Subtract(ProcessStartTime).TotalSeconds);
                DebugManager.LogTrace("EarthQuakeService->Updating earthquakes completed in " + time);
            }
        }

       

        private bool IsFilterMatch(EarthQuakeData e)
        {
            var matchMagnitude = MagnitudeFilter.Contains(e.Magnitude);
            var matchDepth = DepthFilter.Contains(e.Depth);
            var matchLatitude  = LatitudeFilter.Contains(e.Latitude);
            var matchLongitude = LongitudeFilter.Contains(e.Longitude);

            var matchRegion = SelectedRegions.Contains(e.Region);
            bool matchTime;
            //if (TimeFilter.IsCustom)
            //{
            //    matchTime = TimeFilter.Contains(e.Updated);
            //}
            //else
            //{
                matchTime = e.Updated >= GetActualTimeMin();
            //}
            return matchMagnitude && matchDepth && matchTime &&
                   matchLatitude && matchLongitude && matchRegion;
        }

        public void GetRegionBounds(string region, out double minLon, out double minLat, out double maxLon, out double maxLat)
        {
            if (region != "Other")
            {
                EartQuakeRegions.First(x => x.Name == region).GetBoundingBox(out minLon, out minLat, out maxLon, out maxLat);
            }
            else
            {
                minLon = -180.0;
                minLat = -80.0;
                maxLon = 180.0;
                maxLat = 80.0;
            }
        }
        #endregion
    }

    //{"type":"Feature","properties":{"mag":5,"place":"33km S of Taron, Papua New Guinea",
    //"time":1378155655940,"updated":1378157118838,"tz":600,
    //"url":"http://earthquake.usgs.gov/earthquakes/eventpage/usb000jfdl",
    //"detail":"http://earthquake.usgs.gov/earthquakes/feed/v1.0/detail/usb000jfdl.geojson",
    //"felt":0,"cdi":1,"mmi":null,"alert":null,"status":"REVIEWED","tsunami":null,"sig":385,
    //"net":"us","code":"b000jfdl","ids":",usb000jfdl,","sources":",us,",
    //"types":",cap,dyfi,general-link,geoserve,nearby-cities,origin,p-wave-travel-times,phase-data,tectonic-summary,",
    //"nst":45,"dmin":1.08,"rms":0.89,"gap":72,"magType":"mb","type":"earthquake"},
    //"geometry":{"type":"Point","coordinates":[153.082,-4.7627,50.69]},"id":"usb000jfdl"},
    
     
    /// <summary>
    /// 
    /// <para> description: http://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php </para>
    /// <para> auto-generated: http://json2csharp.com/ </para>
    /// </summary>
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WebRole")]
    public class EarthQuakeDataRoot
    {
        public EarthQuakeDataRoot()
        {
            EarthQuakes = new List<EarthQuakeSummary>();
            GeodeticRange = new List<double>();
            Metadata = new EarthQuakeMetadata();
        }
        [DataMember(Name = "bbox")]
        public List<double> GeodeticRange { get; set; }
        //[DataMember(Name = "metadata", EmitDefaultValue = false)]

        [DataMember(Name = "features")]
        public List<EarthQuakeSummary> EarthQuakes { get; set; }

        [DataMember(Name = "metadata")]
        public EarthQuakeMetadata Metadata { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WebRole")]
    public class EarthQuakeMetadata
    {
        [DataMember(Name = "generated")]
        public long Generated { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "status")]
        public int Status { get; set; }
        [DataMember(Name = "api")]
        public string Api { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }

        public new string ToString()
        {
            var result = new StringBuilder();
            result.Append("title " + this.Title + ", ");
            result.Append("api " + this.Api + ", ");
            result.Append("# " + this.Count + ", ");

            result.Append("generated " + this.Generated + ", ");
            return result.ToString();
        }
    }

    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WebRole")]
    public class EarthQuakeSummary
    {
        public EarthQuakeSummary()
        {
            Geometry = new EarthQuakeGeometry();
            Properties = new EarthQuakeProperties();
        }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "properties")]
        public EarthQuakeProperties Properties { get; set; }
        [DataMember(Name = "geometry")]
        public EarthQuakeGeometry Geometry { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }

        public new string ToString()
        {
            var result = new StringBuilder();
            //result.Append("type " + this.Type + ", ");
            //result.Append("id " + this.Id + ", ");
            //result.Append("geometry " + this.Geometry.ToString() + ", ");
            result.Append("properties " + this.Properties.ToString() + ", ");
            return result.ToString();
        }
    }
    
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WebRole")]
    public class EarthQuakeGeometry
    {
        public EarthQuakeGeometry()
        {
            Coordinates = new List<double>();
        }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "coordinates")]
        public List<double> Coordinates { get; set; }

        public new string ToString()
        {
            var result = new StringBuilder();
            result.Append("type " + this.Type + ", ");
            result.Append("coordinates " + this.Coordinates + ", ");

            return result.ToString();
        }
    }
   
    /// <summary>
    /// Represents properties of an earthquake
    /// <para>description: http://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php </para>
    /// </summary>
    [DataContract(Namespace = "http://schemas.datacontract.org/2004/07/WebRole")]
    public class EarthQuakeProperties
    {
        /// <summary>
        /// Gets or sets Magnitude
        /// </summary>
        [DataMember(Name = "mag")]
        public double? Magnitude { get; set; }
        /// <summary>
        /// Gets or sets Magnitude Type
        /// </summary>
        [DataMember(Name = "magType")]
        public string MagnitudeType { get; set; }

        /// <summary>
        /// Gets or sets Reports
        /// </summary>
        [DataMember(Name = "felt")]
        public string Reports { get; set; }
        
        [DataMember(Name = "types")]
        public string DataTypes { get; set; }
      
        /// <summary>
        /// Gets or sets Intensity
        /// </summary>
        [DataMember(Name = "cdi")]
        public string Intensity { get; set; }
       
        [DataMember(Name = "mmi")]
        public double? IntensityEstimate { get; set; }
        /// <summary>
        /// Gets or sets Significance
        /// </summary>
        [DataMember(Name = "sig")]
        public string Significance { get; set; }
        [DataMember(Name = "nst")]
        public string Stations { get; set; }

        [DataMember(Name = "place")]
        public string Place { get; set; }
        /// <summary>
        /// Gets or sets Time
        /// </summary>
        [DataMember(Name = "time")]
        public long? Time { get; set; }
        [DataMember(Name = "updated")]
        public long? Updated { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "tz")]
        public string TimeZone { get; set; }

        [DataMember(Name = "detail")]
        public string Detail { get; set; }

        [DataMember(Name = "alert")]
        public string Alert { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "tsunami")]
        public string Tsunami { get; set; }
        [DataMember(Name = "net")]
        public string Network { get; set; }

        [DataMember(Name = "code")]
        public string Code { get; set; }
        [DataMember(Name = "ids")]
        public string EventCodes { get; set; }

        [DataMember(Name = "sources")]
        public string Sources { get; set; }
        [DataMember(Name = "dmin")]
        public double? Dmin { get; set; }
        [DataMember(Name = "rms")]
        public double? Rms { get; set; }
        [DataMember(Name = "gap")]
        public string Gap { get; set; }
        [DataMember(Name = "type")]
        public string EventType { get; set; }

        [IgnoreDataMember]
        public double? Longitude { get; set; }
        [IgnoreDataMember]
        public double? Latitude { get; set; }
        [IgnoreDataMember]
        public double? Depth { get; set; }
        [IgnoreDataMember]
        public string Title { get; set; }
        [IgnoreDataMember]
        public string Region { get; set; }

        public new string ToString()
        {
            var result = new StringBuilder();
            result.Append("time " + this.Updated + ", ");
            result.Append("tz " + this.TimeZone + ", ");
            result.Append("place " + this.Title + ", ");
            result.Append("mag " + this.Magnitude + ", ");
            result.Append("geo {" + this.Longitude + ", " + this.Latitude + "}, ");
            result.Append("dep " + this.Depth + ", ");

            result.Append("reg " + this.Region + ", ");
            result.Append("rep " + this.Reports + ", ");
            result.Append("int " + this.Intensity + ", ");
            result.Append("sta " + this.Stations + ", ");
            result.Append("tsunami " + this.Tsunami + ", ");
            result.Append("net " + this.Network + ", ");
          
            return result.ToString();
        }
    }

 
}