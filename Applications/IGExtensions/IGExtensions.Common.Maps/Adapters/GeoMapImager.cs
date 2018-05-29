using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;
using System.Xml.Linq;
using IGExtensions.Common.DataProviders;
using IGExtensions.Common.Maps.Assets.Resources;
using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Framework;
using IGExtensions.Framework.Controls;

namespace Infragistics.Controls.Maps  
{
    /// <summary>
    /// Represents geo-imagery provider based on configuration of the XamGeographicMap control
    /// </summary>
    public static class GeoMapImagryProvider
    {
        static GeoMapImagryProvider()
        {
            MapConfigFile = "IGMapConfig.xml";
            // comment out to disable overriding of map configuration file
            //GeoMapConfigOverride.OverrideMapImageryConfigChanges();
            OpenStreetMapImageryConfig = GeoMapImageryConfig.OpenStreetMapImageryConfig;
            BingMapImageryConfig = GeoMapImageryConfig.BingMapImageryConfig;
            EsriMapImageryConfig = GeoMapImageryConfig.EsriMapImageryConfig;            
            MapQuestImageryConfig = GeoMapImageryConfig.MapQuestImageryConfig;
        }
        public static GeoMapImageryConfig OpenStreetMapImageryConfig { get; private set; }
        public static GeoMapImageryConfig BingMapImageryConfig { get; private set; }
        public static GeoMapImageryConfig EsriMapImageryConfig { get; private set; }        
        public static GeoMapImageryConfig MapQuestImageryConfig { get; private set; }    

        public static GeoImageryGroupList GetGeographicMapImageryGroups()
        {
            var groups = new GeoImageryGroupList();

            if (OpenStreetMapImageryConfig.ImageryIsEnabled)
                groups.Add(new GeoImageryGroup { Name = ImageryStrings.Tiles_OpenStreetMap, ImageryViews = GeoImageryViews.OpenStreetMapViews });

            if (BingMapImageryConfig.ImageryIsEnabled)
                groups.Add(new GeoImageryGroup { Name = ImageryStrings.Tiles_BingMap, ImageryViews = GeoImageryViews.BingMapViews });

            if (EsriMapImageryConfig.ImageryIsEnabled)
            {
                groups.Add(new GeoImageryGroup { Name = ImageryStrings.Tiles_ESRI_World_Map, ImageryViews = GeoImageryViews.EsriWorldMapViews });
                groups.Add(new GeoImageryGroup { Name = ImageryStrings.Tiles_ESRI_World_Overlay, ImageryViews = GeoImageryViews.EsriWorldOverlayViews });
                groups.Add(new GeoImageryGroup { Name = ImageryStrings.Tiles_ESRI_USA_Overlay, ImageryViews = GeoImageryViews.EsriUsaOverlayViews });
            }            

            if (MapQuestImageryConfig.ImageryIsEnabled)
                groups.Add(new GeoImageryGroup { Name = ImageryStrings.Tiles_MapQuest, ImageryViews = GeoImageryViews.MapQuestViews });

            return groups;
        }
        #region Methods
        /// <summary>
        /// Creates <see cref="GeographicMapImagery"/> from specified imagery view model, <see cref="GeoImageryViews"/>
        /// </summary>
        public static GeographicMapImagery GetGeographicMapImagery(GeoImageryViewModel imageryViewModel)
        {
            GeographicMapImagery geoImagery = new OpenStreetMapImagery();
            #region Open Street Map Imagery
            if (imageryViewModel.ImagerySource == GeoImagerySource.OpenStreetMapImagery && OpenStreetMapImageryConfig.ImageryIsEnabled)
            {
                geoImagery = new OpenStreetMapImagery();
            }
            #endregion     
            #region ESRI Map Imagery
            else if (imageryViewModel.ImagerySource == GeoImagerySource.EsriMapImagery && EsriMapImageryConfig.ImageryIsEnabled)
            {
                var vm = (EsriMapImageryView)imageryViewModel;
                geoImagery = new ArcGISOnlineMapImagery { MapServerUri = vm.ImageryServer };
            }
            #endregion
            #region Map Quest Imagery
            else if (imageryViewModel.ImagerySource == GeoImagerySource.MapQuestImagery && MapQuestImageryConfig.ImageryIsEnabled)
            {
                var vm = (MapQuestImageryView)imageryViewModel;
                if (vm.ImageryStyle == MapQuestImageryStyle.StreetMapStyle)
                    geoImagery = new MapQuestStreetImagery();
                else if (vm.ImageryStyle == MapQuestImageryStyle.SatelliteMapStyle)
                    geoImagery = new MapQuestSatelliteImagery();
            }
            #endregion
            #region Bing Map Imagery
            else if (imageryViewModel.ImagerySource == GeoImagerySource.BingMapsImagery && BingMapImageryConfig.ImageryIsEnabled)
            {
                var vm = (BingMapsImageryView)imageryViewModel;
                var geoImageryStyle = (Infragistics.Controls.Maps.BingMapsImageryStyle)vm.ImageryStyle;

                if (vm.ImageryKey == string.Empty) vm.ImageryKey = BingMapImageryConfig.ImageryKey;
                if (vm.IsImageryKeyValid())
                {
                    geoImagery = new BingMapsMapImagery { ImageryStyle = geoImageryStyle, ApiKey = vm.ImageryKey };
                }
                else
                {
                    DebugManager.LogWarning("Geographic Imagery view model is missing key for Bing Map!");
                }
                //var bingMapsConnector = new BingMapsConnector();
                //    //bingMapsConnector.ImageryInitialized += (o, e) =>
                //    //{
                //    //    var connector = (BingMapsConnector)o;
                //    //    geoMap.BackgroundContent =
                //    //        new BingMapsMapImagery()
                //    //        {
                //    //            TilePath = connector.TilePath,
                //    //            SubDomains = connector.SubDomains
                //    //        };
                //    //};
                //bingMapsConnector.ImageryInitialized += OnImageryInitialized;
                //bingMapsConnector.ImageryStyle = vm.ImageryStyle;
                //bingMapsConnector.ApiKey = vm.ImageryKey;
                //bingMapsConnector.Initialize();
            }
            #endregion
            return geoImagery;
        }
        /// <summary>
        /// Creates <see cref="GeographicMapImagery"/> for default imagery view based on configuration file
        /// </summary>
        public static GeographicMapImagery GetGeographicMapImagery()
        {
            return GetGeographicMapImagery(GetGeographicMapImageryView());
        }
        /// <summary>
        /// Gets <see cref="GeographicMapImagery"/> from specified imagery source, <see cref="GeoImagerySource"/>
        /// </summary>
        public static GeographicMapImagery GetGeographicMapImagery(GeoImagerySource imagerySource)
        {
            GeoImageryViewModel imageryView = GeoImageryViews.OpenStreetMap.DefaultImageryView;
        
            if (imagerySource == GeoImagerySource.BingMapsImagery)
                imageryView = GeoImageryViews.BingMaps.StreetImageryView;            

            else if (imagerySource == GeoImagerySource.MapQuestImagery)
                imageryView = GeoImageryViews.MapQuest.StreetImageryView;

            else if (imagerySource == GeoImagerySource.EsriMapImagery)
                imageryView = GeoImageryViews.Esri.WorldTopographicMap;

            else if (imagerySource == GeoImagerySource.OpenStreetMapImagery)
                imageryView = GeoImageryViews.OpenStreetMap.DefaultImageryView;
            
            return GetGeographicMapImagery(imageryView);
        }  
        /// <summary>
        /// Gets default geo-imagery view from configuration file
        /// </summary>
        public static GeoImageryViewModel GetGeographicMapImageryView()
        {
            if (BingMapImageryConfig.ImageryIsDefault && BingMapImageryConfig.ImageryIsValid)
                return GeoImageryViews.BingMaps.StreetImageryView;
            
            if (EsriMapImageryConfig.ImageryIsDefault && EsriMapImageryConfig.ImageryIsValid)
                return GeoImageryViews.Esri.WorldTopographicMap;                       

            if (MapQuestImageryConfig.ImageryIsDefault && MapQuestImageryConfig.ImageryIsValid)
                return GeoImageryViews.MapQuest.StreetImageryView;
            
            if (OpenStreetMapImageryConfig.ImageryIsDefault && OpenStreetMapImageryConfig.ImageryIsValid)
                return GeoImageryViews.OpenStreetMap.DefaultImageryView;

            return GeoImageryViews.OpenStreetMap.DefaultImageryView;
        }

        #endregion
        #region Event Handlers
        /// <summary>
        /// Occurs when loading of map keys has been completed
        /// </summary>
        public static event EventHandler<ResultEventArgs> LoadMapConfigurationCompleted;

        public static string MapConfigFile { get; set; }
        //const string MapConfigFile = "IGMapConfig.xml";
        /// <summary>
        /// Loads configuration file with map keys for geo-imagery sources
        /// </summary>
        public static void LoadMapConfiguration()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.LoadXmlDataCompleted += OnLoadXmlDataCompleted;
            dataProvider.LoadXmlDataAsync(MapConfigFile);
        }
        public static void LoadMapConfiguration(Application app)
        {
            LoadMapConfiguration();
         
            //if (!app.HasElevatedPermissions)
            //{
            //   LoadMapConfiguration();
            //}
            //else
            //{
            //    OnLoadMapConfigurationCompleted(new ResultEventArgs("Loaded map settings for: "));
            //    string path = Environment.CurrentDirectory;
            //    //TODO load map config from local file
            //    //if (System.IO.File.Exists(path + MapConfigFile))
            //    //{
            //    //    var stream = System.IO.File.OpenText(path + MapConfigFile);
            //    //    var text = stream.ReadToEnd();
            //    //}
            //    //var uri = new Uri(path + "\\" + MapConfigFile, UriKind.RelativeOrAbsolute);
            //    ////var dataProvider = new XmlDataProvider();
            //    ////dataProvider.LoadXmlDataCompleted += OnLoadXmlDataCompleted;
            //    ////dataProvider.LoadXmlDataAsync(MapConfigFile);
            //}
        }

        static void OnLoadMapConfigurationCompleted(ResultEventArgs eventArgs)
        {
            if (LoadMapConfigurationCompleted != null)
            {
                LoadMapConfigurationCompleted(null, eventArgs);
            }
        }
        static void OnLoadXmlDataCompleted(object sender, LoadXmlDataCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                var error = new Exception("Missing Infragistics Map configuration file - will use default settings.", e.Error);
                DebugManager.LogError(error);
                OnLoadMapConfigurationCompleted(new ResultEventArgs(error));
                return;
            }
            try
            {
                var xml = e.XmlDocument;
                var xmlMapConfiguration = xml.Element("Configuration");
                if (xmlMapConfiguration != null)
                {
                    var xmlMapImagerySources = xmlMapConfiguration.Elements("MapImagerySources");
                    var xmlMapImageryElements = xmlMapImagerySources.Elements();
                    //TODO-MT add MapStyle attribute to map.config

                    var loadedMapSources = string.Empty;
                    foreach (var element in xmlMapImageryElements)
                    {
                        var mapSource = element.Attribute("MapSource").GetString();
                        if (String.IsNullOrEmpty(mapSource)) continue;

                        var mapKey = element.Attribute("MapKey").GetString();
                        var mapIsEnabled = element.Attribute("IsEnabled").GetString().ToLower() != "false";
                        var mapIsDefault = element.Attribute("IsDefault").GetString().ToLower() != "false";
                        
                        if (mapSource == "BingMapImagery")
                        {
                            BingMapImageryConfig.ImageryIsDefault = mapIsDefault;
                            BingMapImageryConfig.ImageryIsEnabled = (mapIsEnabled || mapIsDefault) && mapKey != string.Empty;
                            BingMapImageryConfig.ImageryKey = mapKey;
                        }
                        else if (mapSource == "EsriMapImagery")
                        {
                            EsriMapImageryConfig.ImageryIsDefault = mapIsDefault;
                            EsriMapImageryConfig.ImageryIsEnabled = mapIsEnabled || mapIsDefault;
                        }
                        else if (mapSource == "OpenStreetMapImagery")
                        {
                            OpenStreetMapImageryConfig.ImageryIsDefault = mapIsDefault;
                            OpenStreetMapImageryConfig.ImageryIsEnabled = mapIsEnabled || mapIsDefault;
                        }                        
                        else if (mapSource == "MapQuestImagery")
                        {
                            MapQuestImageryConfig.ImageryIsDefault = mapIsDefault;
                            MapQuestImageryConfig.ImageryIsEnabled = mapIsEnabled || mapIsDefault;
                        }
                        loadedMapSources += mapSource + ", ";
                    }
                    OnLoadMapConfigurationCompleted(new ResultEventArgs("Loaded map settings for: " + loadedMapSources));
                }
            }
            catch (Exception ex)
            {
                DebugManager.LogWarning("Error occurred while parsing IG Map configuration file: \n" + ex);
            }

        }

        #endregion
    }
    /// <summary>
    /// Provides extension methods for the XamGeographicMap control and working with geo-imagery models
    /// </summary>
    public static class GeoMapImager
    {
        public static double ImageryZoomMaxLevel = 0.00001;
        public static double ImageryZoomFactor = 0.000005;

        static GeoMapImager()
        {
            //GeoMapImagryProvider.LoadMapKeys();
        }
    
        public static void IncreaseImageryZoomMaxLevel(this XamGeographicMap geoMap)
        {
            geoMap.WindowRectMinWidth = GeoMapImager.ImageryZoomMaxLevel;
        }
       
        #region Imagery-Loading Methods
        /// <summary>
        /// Loads geo-imagery from default imagery style of OpenStreetMap 
        /// </summary>
        public static void LoadGeoImagery(this XamGeographicMap geoMap)
        {
            geoMap.LoadGeoImagery(new OpenStreetMapImageryView());
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery source
        /// </summary>
        public static void LoadGeoImagery(this XamGeographicMap geoMap, GeoImagerySource imagerySource)
        {
            if (imagerySource == GeoImagerySource.BingMapsImagery)
                geoMap.LoadGeoImagery(IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.StreetMapStyle);            
            else if (imagerySource == GeoImagerySource.MapQuestImagery)
                geoMap.LoadGeoImagery(MapQuestImageryStyle.StreetMapStyle);
            else if (imagerySource == GeoImagerySource.EsriMapImagery)
                geoMap.LoadGeoImagery(EsriMapImageryStyle.WorldTopographicMap);
            else if (imagerySource == GeoImagerySource.OpenStreetMapImagery)
                geoMap.LoadGeoImagery(new OpenStreetMapImageryView());
            else
            {
                System.Diagnostics.Debug.WriteLine("WARNING: Imagery source " + imagerySource + " is not supported by IGExtensions library.");
            }
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery style of BingMaps
        /// </summary>
        public static void LoadGeoImagery(this XamGeographicMap geoMap, IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle imageryStyle)
        {
            geoMap.LoadGeoImagery(new BingMapsImageryView(imageryStyle));
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery style of BingMaps
        /// </summary>
        public static void LoadGeoImagery(this XamGeographicMap geoMap, Infragistics.Controls.Maps.BingMapsImageryStyle imageryStyle)
        {
            var geoImageryStyle = (IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle) imageryStyle;
            geoMap.LoadGeoImagery(new BingMapsImageryView(geoImageryStyle));
        }     
        /// <summary>
        /// Loads geo-imagery from specified imagery style of MapQuest
        /// </summary>
        public static void LoadGeoImagery(this XamGeographicMap geoMap, MapQuestImageryStyle imageryStyle)
        {
            geoMap.LoadGeoImagery(new MapQuestImageryView(imageryStyle));
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery style of ESRI
        /// </summary>
        public static void LoadGeoImagery(this XamGeographicMap geoMap, EsriMapImageryStyle imageryStyle)
        {
            geoMap.LoadGeoImagery(new EsriMapImageryView(imageryStyle));
        }
        /// <summary>
        /// Loads geo-imagery from specified imagery view and preserves settings of XamGeographicMap's BackgroundContent
        /// <remarks>Supported imagery views: <see cref="OpenStreetMapImageryView"/>, <see cref="BingMapsImageryView"/>, 
        /// <see cref="EsriMapImageryView"/>, and <see cref="MapQuestImageryView"/></remarks>
        /// </summary>
        public static void LoadGeoImagery(this XamGeographicMap geoMap, GeoImageryViewModel imageryViewModel)
        {
            if (geoMap == null)
            {
                DebugManager.LogWarning("GeoMapImager cannot load geo-imagery when XamGeographicMap is null"); return;
            }
            GeoMapImager.GeoMap = geoMap;
            
            geoMap.SaveMapImagerySettings();
            geoMap.BackgroundContent = imageryViewModel.GetGeographicMapImagery();
            geoMap.LoadMapImagerySettings();
        }
        ///// <summary>
        ///// Loads geo-imagery from specified imagery view and preserves settings of GeographicTileSeries' TileImagery
        ///// <remarks>Supported imagery views: <see cref="OpenStreetMapImageryView"/>, <see cref="BingMapsImageryView"/>, 
        ///// <see cref="EsriMapImageryView"/>, and <see cref="MapQuestImageryView"/></remarks>
        ///// </summary>
        //public static void LoadGeoImagery(this GeographicTileSeries geoTileSeries, GeoImageryViewModel imageryViewModel)
        //{
        //    if (geoTileSeries == null)
        //    {
        //        DebugManager.LogWarning("GeoMapImager cannot load geo-imagery when GeographicTileSeries is null"); return;
        //    }
            
        //    geoTileSeries.TileImagery.SaveMapImagerySettings();
        //    geoTileSeries.TileImagery = imageryViewModel.GetGeographicMapImagery();
        //    geoTileSeries.TileImagery.LoadMapImagerySettings();
        //}
        //private static bool IsFirstKeyReservedWarning = true;
        public static void LoadGeoImagery22(this XamGeographicMap geoMap, GeoImageryViewModel imageryViewModel)
        {
            if (geoMap == null)
            {
                DebugManager.LogWarning("22 XamGeographicMap cannot be null"); return;
            }

            GeoMapImager.GeoMap = geoMap;
            if (geoMap.BackgroundContent is GeographicMapImagery)
               (geoMap.BackgroundContent as GeographicMapImagery).SaveMapImagerySettings();         
        }
      

        internal static XamGeographicMap GeoMap;
       
        private static void OnImageryInitialized(object sender, EventArgs e)
        {
            #if SILVERLIGHT
            //Dispatcher.BeginInvoke(() => UpdateBingMaps(sender));
            Deployment.Current.Dispatcher.BeginInvoke(() => UpdateBingMaps(sender)); // use in library

            // Dispatcher.BeginInvoke(() => UpdateBingMaps(sender)); // use in application (on UI page)
            #else // if WPF
    
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
               new System.Threading.ThreadStart(() => UpdateBingMaps(sender))); // use in library/application
             
            #endif
            //Deployment.Current.Dispatcher.BeginInvoke(new PropertyChangedDelegate(OnPropertyChanged), this, propertyName);
          
        }
        private static void UpdateBingMaps(object sender)
        {
            var opacity = GeoMap.BackgroundContent.Opacity;
            var visibility = GeoMap.BackgroundContent.Visibility;
            // display geo-imagery from Bing Maps on a XamGeographicMap control
            var connector = (BingMapsConnector)sender;

            GeoMap.SaveMapImagerySettings();
            GeoMap.BackgroundContent =
                new BingMapsMapImagery()
                {
                    TilePath = connector.TilePath,
                    SubDomains = connector.SubDomains,
                };
            GeoMap.LoadMapImagerySettings();
            DebugManager.LogWarning("GeoMapImager loading BingMaps imagery"); return;
        }
        #endregion
        
        #region Imagery-Info Methods
        public static GeoImagerySource GetGeoImagerySource(this XamGeographicMap geoMap)
        {
            var geoImagerySource = GeoImagerySource.OpenStreetMapImagery;

            var geoMapImagery = geoMap.BackgroundContent as GeographicMapImagery;
            
            if (geoMapImagery is MapQuestStreetImagery || geoMapImagery is MapQuestSatelliteImagery)
                geoImagerySource = GeoImagerySource.MapQuestImagery;
            else if (geoMapImagery is BingMapsMapImagery)
                geoImagerySource = GeoImagerySource.BingMapsImagery;
            else if (geoMapImagery is ArcGISOnlineMapImagery)
                geoImagerySource = GeoImagerySource.EsriMapImagery;
            else //if (geoMapImagery is OpenStreetMapImagery)
                geoImagerySource = GeoImagerySource.OpenStreetMapImagery;
        
            return geoImagerySource;
        }
        public static GeoImageryViewModel GetGeoImageryViewModel(this XamGeographicMap geoMap)
        {
            var geoImagerySource = geoMap.GetGeoImagerySource();
            GeoImageryViewModel geoImageryView = new OpenStreetMapImageryView();

            if (geoImagerySource == GeoImagerySource.OpenStreetMapImagery)
            {
                geoImageryView = new OpenStreetMapImageryView();
            }         
            //TODO: might require refactoring based on how MapQuestImagery is implemented in XamGeoMap
            else if (geoImagerySource == GeoImagerySource.MapQuestImagery)
            {
                if (geoMap.BackgroundContent is MapQuestStreetImagery)
                    geoImageryView = new MapQuestStreetImageryView();
                else // if(geoMap.BackgroundContent is MapQuestStreetImagery)
                    geoImageryView = new MapQuestSatelliteImageryView();
            }
            else if (geoImagerySource == GeoImagerySource.BingMapsImagery)
            {
                var geoImagery = geoMap.BackgroundContent as BingMapsMapImagery;
                var style = geoImagery.GetImageryStyle();
                geoImageryView = new BingMapsImageryView(style);
            }
            else if (geoImagerySource == GeoImagerySource.EsriMapImagery)
            {
                var geoImagery = geoMap.BackgroundContent as ArcGISOnlineMapImagery;
                //var style = geoImagery.GetImageryStyle();
                //geoImageryView = new BingMapsImageryView(style);
                //TODO: add ESRI   
            }
            return geoImageryView;

        }
               
        public static IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle GetImageryStyle(this BingMapsMapImagery geoImagery)
        {
            var style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.StreetMapStyle;
            // TODO refractor using list of BingMapsImageryViews
            if (geoImagery.ImageryStyle == BingMapsImageryStyle.Road)
            {
                style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.StreetMapStyle;
            }
            else if (geoImagery.ImageryStyle == BingMapsImageryStyle.AerialWithLabels)
            {
                style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.SatelliteMapStyle;
            }
            else if (geoImagery.ImageryStyle == BingMapsImageryStyle.Aerial)
            {
                style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.SatelliteNoLabelsMapStyle;
            }
            return style;
        }
        public static IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle GetImageryStyle2(this BingMapsMapImagery geoImagery)
        {
            var style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.StreetMapStyle;
            if (geoImagery.TilePath.Contains("tiles/r"))
            {
                style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.StreetMapStyle;
            }
            else if (geoImagery.TilePath.Contains("tiles/h"))
            {
                style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.SatelliteMapStyle;
            }
            else if (geoImagery.TilePath.Contains("tiles/a"))
            {
                style = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle.SatelliteNoLabelsMapStyle;
            }
            return style;
        }
        #endregion

        #region Internal Methods
        internal static GeoMapImagerySettings MapImagerySettings = new GeoMapImagerySettings();
        internal static void SaveMapImagerySettings(this XamGeographicMap geoMap)
        {
            if (geoMap.BackgroundContent is GeographicMapImagery)
               (geoMap.BackgroundContent as GeographicMapImagery).SaveMapImagerySettings();
               // MapImagerySettings = new GeoMapImagerySettings(geoMap.BackgroundContent as GeographicMapImagery);
        }
        internal static void LoadMapImagerySettings(this XamGeographicMap geoMap)
        {
            if (geoMap.BackgroundContent is GeographicMapImagery)
               (geoMap.BackgroundContent as GeographicMapImagery).LoadMapImagerySettings();
            // MapImagerySettings = new GeoMapImagerySettings(geoMap.BackgroundContent as GeographicMapImagery);
        }
        internal static void SaveMapImagerySettings(this GeographicMapImagery geoImagery)
        {
           // if (geoImagery is GeographicMapImagery)
                MapImagerySettings = new GeoMapImagerySettings(geoImagery);
        }
        internal static void LoadMapImagerySettings(this GeographicMapImagery geoImagery)
        {
            geoImagery.Opacity = MapImagerySettings.Opacity;
            geoImagery.Visibility = MapImagerySettings.Visibility;
        } 
        #endregion
    }

    internal class GeoMapImagerySettings
    {
        public GeoMapImagerySettings()
        {
            
        }
        public GeoMapImagerySettings(GeographicMapImagery geoImagery)
        {
            this.Opacity = geoImagery.Opacity;
            this.Visibility = geoImagery.Visibility;
        }

        public Visibility Visibility { get; set; }
        public double Opacity { get; set; }
    }
}


