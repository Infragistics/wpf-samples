using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Windows;
using System.Windows.Resources;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Serialization;
using IGExtensions.Common.Models;

namespace IGExtensions.Common.Services
{
    /// <summary>
    /// Represents a service for retrieving information about latest earth quakes from USGS
    /// </summary>
    [Obsolete("This service is obsolete", true)]
    public sealed class EarthQuakesServiceObsolete : INotifyPropertyChanged
#if SILVERLIGHT
    , IApplicationService
#else // if WPF
#endif
    {
        #region Constants
        // GerRss namespace
        private const string GeoRss = "http://www.georss.org/georss";
        private const string UsGovId = "urn:earthquake-usgs-gov";
        private const string EarthquakesLink = "http://earthquake.usgs.gov/earthquakes/recenteqsww/Quakes/{0}{1}.php";
        private const string ContinentBoudariesFile = "/IGExtensions.Common.Services;component/Assets/Data/ContinentsBoundaries.xml";
        //private const string ContinentBoudariesFile = "/Assets/Data/ContinentsBoundaries.xml";

        // M 2.5+ past day (XML):
        private const string XML_M2P5_PAST_DAY = "http://earthquake.usgs.gov/earthquakes/catalogs/1day-M2.5.xml";
        // M 2.5+ past 7 days (XML):
        private const string XML_M2P5_PAST_7_DAYS = "http://earthquake.usgs.gov/earthquakes/catalogs/7day-M2.5.xml";
        // M 5+ past 7 days (XML):
        private const string XML_M5_PAST_7_DAYS = "http://earthquake.usgs.gov/earthquakes/catalogs/7day-M5.xml";
        // M 1+ past hour(CSV):
        private const string CSV_M1_PAST_HOUR = "http://earthquake.usgs.gov/earthquakes/catalogs/eqs1hour-M1.txt";
        // M 1+ past day (CSV):
        private const string CSV_M1_PAST_DAY = "http://earthquake.usgs.gov/earthquakes/catalogs/eqs1day-M1.txt";
        // M 1+ past 7 days (CSV):
        private const string CSV_M1_PAST_7_DAYS = "http://earthquake.usgs.gov/earthquakes/catalogs/eqs7day-M1.txt";

        #endregion Constants

        #region Private Member Variables
        //private static readonly Uri ServiceUrl = new Uri(CSV_M1_PAST_DAY);
        private static readonly Uri ServiceUrl = new Uri(CSV_M1_PAST_7_DAYS);
        private readonly WebClient _client;
        private bool _isBusy;
        private DispatcherTimer _timer;
        private bool _firstDataLoaded;
        private ContinentsList _continentsList;

        private double _minMagnitude;
        private double _maxMagnitude;
        private List<string> _allRegions = new List<string>();
        private List<string> _selectedRegions = new List<string>();
        private List<EarthQuakeData> _allEarthquakes;
        private List<EarthQuakeData> _earthquakes = new List<EarthQuakeData>();
        private EarthQuakeData _latestEarthquake;
        private DateTime _lastUpdated;
        #endregion Private Member Variables

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the EarthQuakesService class.
        /// </summary>
        public EarthQuakesServiceObsolete()
        {
            _minMagnitude = 0.0;
            _maxMagnitude = 10.0;

            _client = new WebClient();
            if (ServiceUrl.ToString().Contains("xml"))
            {
                _client.OpenReadCompleted += OpenReadCompletedXml;
            }
            else
            {
                _client.OpenReadCompleted += OpenReadCompletedCsv;
            }
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Implementation of IApplicationService
#if SILVERLIGHT 
        /// <summary>
        /// Called by an application in order to initialize the application extension service.
        /// </summary>
        /// <param name="context">Provides information about the application state.</param>
        public void StartService(ApplicationServiceContext context)
        {
            StartService();
        }
#endif
        internal bool IsStarted = false;
        /// <summary>
        /// Called by an application in order to initialize the application extension service.
        /// </summary>
        public void StartService()
        {
            if (IsStarted) return;

            IsStarted = true;

            _continentsList = LoadContinentsList(ContinentBoudariesFile);
            _allRegions = _continentsList.Select(x => x.Name).Concat(Enumerable.Repeat("Other", 1)).ToList();
            _selectedRegions = _allRegions;

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(5); // Refresh Data every X minutes
            _timer.Tick += (s, e) => GetEarthquakes();

            GetEarthquakes();
            _timer.Start();
        }
        /// <summary>
        /// Called by an application in order to stop the application extension service.
        /// </summary>
        public void StopService()
        {
            _timer.Stop();
            _timer = null;

            IsStarted = false;

        }
        
        #endregion

        #region Public Members
        public double MinMagnitude
        {
            get { return _minMagnitude; }
            set
            {
                if (_minMagnitude != value)
                {
                    _minMagnitude = value;
                    OnPropertyChanged("MinMagnitude");
                    if (_firstDataLoaded)
                    {
                        UpdateEarthquakes();
                    }
                }
            }
        }

        public double MaxMagnitude
        {
            get { return _maxMagnitude; }
            set
            {
                if (_maxMagnitude != value)
                {
                    _maxMagnitude = value;
                    OnPropertyChanged("MaxMagnitude");
                    if (_firstDataLoaded)
                    {
                        UpdateEarthquakes();
                    }
                }
            }
        }

        public List<string> AllRegions
        {
            get { return _allRegions; }
        }

        public List<string> SelectedRegions
        {
            get { return _selectedRegions; }
            set
            {
                if (_selectedRegions != value)
                {
                    _selectedRegions = value;
                    OnPropertyChanged("SelectedRegions");
                    if (_firstDataLoaded)
                    {
                        UpdateEarthquakes();
                    }
                }
            }
        }

        public List<EarthQuakeData> Earthquakes
        {
            get { return _earthquakes; }
            private set
            {
                if (_earthquakes != value)
                {
                    _earthquakes = value;
                    OnPropertyChanged("Earthquakes");
                }
            }
        }

        public EarthQuakeData LatestEarthquake
        {
            get { return _latestEarthquake; }
            private set
            {
                if (_latestEarthquake != value)
                {
                    _latestEarthquake = value;
                    OnPropertyChanged("LatestEarthquake");
                }
            }
        }

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

        public void GetRegionBounds(string continent, out double minLon, out double minLat, out double maxLon, out double maxLat)
        {
            if (continent != "Other")
            {
                _continentsList.First(x => x.Name == continent).GetBoundingBox(out minLon, out minLat, out maxLon, out maxLat);
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

        #region Private Methods
        private void GetEarthquakes()
        {
            if (!_isBusy)
            {
                _isBusy = true;
                _client.OpenReadAsync(ServiceUrl);
            }
        }
        private static ContinentsList LoadContinentsList(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("FileName Is Missing:" + fileName);
            }

            try
            {
                Stream fileStream;
                var uri = new Uri(fileName, UriKind.Relative);
                StreamResourceInfo info = Application.GetResourceStream(uri);
                fileStream = info.Stream;

                XmlReader xmlReader = XmlReader.Create(fileStream);
                var xmlSer = new XmlSerializer(typeof(ContinentsList));
                var list = (ContinentsList)(xmlSer.Deserialize(xmlReader));
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in serialization", ex);
            }
        }
        /// <summary>
        /// Opens the read completed for CSV file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OpenReadCompletedCsv(object sender, OpenReadCompletedEventArgs e)
        {
            _isBusy = false;

            if (!e.Cancelled && e.Error == null)
            {
                using (var reader = new StreamReader(e.Result))
                {
                    int skipFirstLines = 0; // isFirstTwoLines = true;
                    var escapes = new[] { '\"', };
                    var listEqd = new List<EarthQuakeData>();
                    while (reader.Peek() >= 0)
                    {
                        if (skipFirstLines < 2)
                        {
                            // CSV order
                            // Src,Eqid,Version,Datetime(weekday, month data, year hour utc),Lat,Lon,Magnitude,Depth,NST,Region
                            skipFirstLines++;
                            reader.ReadLine();
                        }
                        else
                        {
                            string s = reader.ReadLine();
                            string[] csv = s.Split(new[] { ',' });

                            string id = String.Format("{0}:{1}:{2}", UsGovId, csv[0], csv[1]);
                            string title = csv[11].Trim(escapes);
                            title = title.ToTitle();
                            DateTime updated = DateTime.Parse(String.Format("{0}{1}{2}",
                                csv[3].Trim(escapes), csv[4].Trim(escapes), csv[5]).Trim(escapes).Replace(" UTC", ""),
                                CultureInfo.InvariantCulture,
                                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal);
                            var link = new Uri(String.Format(EarthquakesLink, csv[0], csv[1]));
                            double latitude = Convert.ToDouble(csv[6], CultureInfo.InvariantCulture);
                            double longitude = Convert.ToDouble(csv[7], CultureInfo.InvariantCulture);
                            double depth = Convert.ToDouble(csv[9], CultureInfo.InvariantCulture) * 1000;
                            double magnitude = Convert.ToDouble(csv[8], CultureInfo.InvariantCulture);
                            var data = new EarthQuakeData(id,
                                                    title,
                                                    updated,
                                                    link,
                                                    latitude,
                                                    longitude,
                                                    depth,
                                                    magnitude);
                            listEqd.Add(data);
                        }
                    }
                    NewEarthquakesRecieved(listEqd);
                }
            }
        }

        /// <summary>
        /// Opens the read completed for XML file.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Net.OpenReadCompletedEventArgs"/> instance containing the event data.</param>
        private void OpenReadCompletedXml(object sender, OpenReadCompletedEventArgs e)
        {
            _isBusy = false;

            List<EarthQuakeData> result = null;

            if (!e.Cancelled && e.Error == null)
            {
                using (XmlReader reader = XmlReader.Create(e.Result))
                {
                    SyndicationFeed feed = SyndicationFeed.Load(reader);
                    if (feed != null)
                    {
                        result = feed.Items.Select(ExtractEarthQuake).ToList();
                    }
                }
            }

            if (result != null)
            {
                NewEarthquakesRecieved(result);
            }
        }

        private void NewEarthquakesRecieved(List<EarthQuakeData> result)
        {
            EarthQuakeData latestEarthquake = LatestEarthquake;

            if (!_firstDataLoaded)
            {
                _firstDataLoaded = true;

                if (result.Count > 0)
                {
                    //Assign a region
                    foreach (EarthQuakeData earthquake in result)
                    {
                        earthquake.Region = _continentsList.FindRegion(new Point(earthquake.Longitude, earthquake.Latitude)) ?? "Other";
                    }
                    //Order from newer to older
                    result.Sort((x, y) => -(Comparer<DateTime>.Default.Compare(x.Updated, y.Updated)));
                    latestEarthquake = result[0];
                }
                _allEarthquakes = result;
            }
            else
            {
                //Remove older entries that are not reported now
                int i = 0;
                while (i < _allEarthquakes.Count)
                {
                    if (result.Contains(_allEarthquakes[i]))
                    {
                        ++i;
                    }
                    else
                    {
                        _allEarthquakes.RemoveAt(i);
                    }
                }

                //Add newer entries that are not in the _allEarthquakes list preserving the newer to older order
                foreach (EarthQuakeData earthquake in result)
                {
                    i = 0;
                    bool isNew = true;
                    while (i < _allEarthquakes.Count)
                    {
                        if (earthquake.Updated > _allEarthquakes[i].Updated)
                        {
                            break;
                        }
                        if (earthquake.Equals(_allEarthquakes[i]))
                        {
                            isNew = false;
                            break;
                        }
                        ++i;
                    }

                    if (isNew)
                    {
                        //Assign a region
                        earthquake.Region = _continentsList.FindRegion(new Point(earthquake.Longitude, earthquake.Latitude)) ?? "Other";

                        _allEarthquakes.Insert(i, earthquake);
                        if (i == 0) latestEarthquake = earthquake;
                    }
                }
            }

            LastUpdated = DateTime.UtcNow;
            UpdateEarthquakes();
            LatestEarthquake = latestEarthquake;
        }

        private void UpdateEarthquakes()
        {
            List<EarthQuakeData> newQuakes = _allEarthquakes.
                Where(IsFilterMatch).ToList();
            if (Earthquakes == null || !Earthquakes.SequenceEqual(newQuakes))
            {
                Earthquakes = newQuakes;
            }
        }

        private bool IsFilterMatch(EarthQuakeData e)
        {
            return e.Magnitude >= MinMagnitude && e.Magnitude <= MaxMagnitude && _selectedRegions.Contains(e.Region);
        }

        /// <summary>
        /// Extracts the earthquake data.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private static EarthQuakeData ExtractEarthQuake(SyndicationItem item)
        {
            string id = item.Id;
            string title = item.Title.Text;
            double magnitude = double.Parse(title.Substring(2, title.IndexOf(',') - 2), CultureInfo.InvariantCulture);

            title = title.Substring(title.IndexOf(',') + 2);
            title = title.ToTitle();
                          
            DateTime updated = item.LastUpdatedTime.UtcDateTime;
            Uri link = null;

            if (item.Links != null && item.Links.Count > 0)
            {
                link = item.Links[0].GetAbsoluteUri();
            }

            double lat = double.NaN;
            double lon = double.NaN;
            double depth = double.NaN;
            bool pointFound = false;
            bool elevFound = false;

            foreach (var ext in item.ElementExtensions)
            {
                string value;

                if (ext.OuterNamespace == GeoRss && ext.OuterName == "point")
                {
                    value = ext.GetObject<string>();

                    lat = double.Parse(value.Substring(0, value.IndexOf(' ')), CultureInfo.InvariantCulture);
                    lon = double.Parse(value.Substring(value.IndexOf(' ') + 1), CultureInfo.InvariantCulture);

                    pointFound = true;
                }
                else if (ext.OuterNamespace == GeoRss && ext.OuterName == "elev")
                {
                    value = ext.GetObject<string>();

                    depth = -(double.Parse(value, CultureInfo.InvariantCulture));
                    elevFound = true;
                }

                if (pointFound && elevFound) break;
            }

            return new EarthQuakeData(id, title, updated, link, lat, lon, depth, magnitude);
        }
        #endregion Private Methods
    }

}