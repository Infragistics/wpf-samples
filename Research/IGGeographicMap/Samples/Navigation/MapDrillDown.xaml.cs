using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using IGGeographicMap.Models;
using IGGeographicMap.Samples.Custom;                      // GeoMapAdapter
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System.Threading;
using System.Globalization;

namespace IGGeographicMap.Samples
{
    public partial class MapDrillDown : SampleContainer
    {
        public MapDrillDown()
        {
            InitializeComponent();

            this.ShapeRegionsMapElements = new List<WorldMapShapeElement>();
            this.ShapeStatesMapElements = new List<WorldMapShapeElement>();
            this.ShapeCountiesMapElements = new List<WorldMapShapeElement>();

            var path = "/Infragistics.Samples.Services;component/shapefiles/America/";
            
            CultureInfo culture = Thread.CurrentThread.CurrentCulture;

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;     

            var shapeRegions = new ShapefileConverter();
            shapeRegions.ImportCompleted += OnShapefileImportCompleted;
            shapeRegions.ShapefileSource = new Uri(path + "usa_regions_group.shp", UriKind.RelativeOrAbsolute);
            shapeRegions.DatabaseSource  = new Uri(path + "usa_regions_group.dbf", UriKind.RelativeOrAbsolute);

            var shapeStates = new ShapefileConverter();
            shapeStates.ImportCompleted += OnShapefileImportCompleted;
            shapeStates.ShapefileSource = new Uri(path + "usa_states_group.shp", UriKind.RelativeOrAbsolute);
            shapeStates.DatabaseSource  = new Uri(path + "usa_states_group.dbf", UriKind.RelativeOrAbsolute);

            var shapeCounties = new ShapefileConverter();
            shapeCounties.ImportCompleted += OnShapefileImportCompleted;
            shapeCounties.ShapefileSource = new Uri(path + "usa_counties_mod.shp", UriKind.RelativeOrAbsolute);
            shapeCounties.DatabaseSource  = new Uri(path + "usa_counties_mod.dbf", UriKind.RelativeOrAbsolute);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        protected int LoadedItemsCount = 0;
        protected int LoadedShapeFilesCount;
        protected List<WorldMapShapeElement> ShapeRegionsMapElements;
        protected List<WorldMapShapeElement> ShapeStatesMapElements;
        protected List<WorldMapShapeElement> ShapeCountiesMapElements;
        protected Rect MapWindowRect;

        public WorldDrillDownViewModel ViewModel { get; set; }
        protected Rect WorldInitialRect;

        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var shapeConverter = (ShapefileConverter) sender;
            if (shapeConverter.ShapefileSource.OriginalString.Contains("regions"))
            {
                if (this.ShapeRegionsMapElements.Count == 0)
                {
                    foreach (ShapefileRecord record in shapeConverter)
                    {
                        if (record.Fields != null)
                        {
                            var element = new WorldMapShapeElement();
                            element.ShapeRecord = record;
                            element.ShapeName = record.Fields[1].ToString(); // region name
                            this.ShapeRegionsMapElements.Add(element);
                        }
                    }
                }
            
                if (this.ShapeRegionsMapElements.Count > 0) 
                    this.LoadedShapeFilesCount++;

                this.WorldInitialRect = shapeConverter.WorldRect;
            }
            else if (shapeConverter.ShapefileSource.OriginalString.Contains("states"))
            {
                foreach (ShapefileRecord record in shapeConverter)
                {
                    if (record.Fields != null)
                    {
                        var element = new WorldMapShapeElement();
                        element.ShapeRecord = record;
                        element.ShapeName = record.Fields[0].ToString();        // shape name
                        element.ShapeRegionName = record.Fields[4].ToString();  // region name
                        element.ShapeCode = record.Fields[3].ToString();        // state code:  STATE_ABBR 
                        this.ShapeStatesMapElements.Add(element);
                    }
                }
                if (this.ShapeStatesMapElements.Count > 0) 
                    this.LoadedShapeFilesCount++;
            }
            else if (shapeConverter.ShapefileSource.OriginalString.Contains("counties"))
            {
                foreach (ShapefileRecord record in shapeConverter)
                {
                    if (record.Fields != null)
                    {
                        var element = new WorldMapShapeElement();
                        element.ShapeRecord = record;
                        element.ShapeName = record.Fields[4].ToString();    // county name
                        element.ShapeCode = record.Fields[3].ToString();    // county code:   
                        this.ShapeCountiesMapElements.Add(element);
                    }
                }
                if (this.ShapeCountiesMapElements.Count > 0) 
                    this.LoadedShapeFilesCount++;
            }

            if (this.ShapeRegionsMapElements.Count > 0 &&
                this.ShapeStatesMapElements.Count > 0 && 
                this.ShapeCountiesMapElements.Count > 0)
            {
                // bind loaded shape elements to the GeoMap
                this.geoMap.Series[0].ItemsSource = this.ShapeRegionsMapElements;
                this.geoMap.Series[1].ItemsSource = this.ShapeStatesMapElements;
                this.geoMap.Series[2].ItemsSource = this.ShapeCountiesMapElements;
                
                // initialize World Drill-Down ViewModel with the default map view of a shape file of the US regions
                var initialMapView = new WorldMapViewModel();
                initialMapView.WorldDrillDownLevel = WorldDrillDownLevel.CountryRegions;
                initialMapView.WorldRect = this.WorldInitialRect; // geoMap.WorldRect;
                this.ViewModel = new WorldDrillDownViewModel(initialMapView);
                
                this.geoMap.WorldRect = this.WorldInitialRect; 
                // show and set bounds of geo-map world 
                this.geoMap.Visibility = System.Windows.Visibility.Visible;
            }
        }

      
        private void geoMap_SeriesMouseLeftButtonDown(object sender, DataChartMouseButtonEventArgs e)
        {
            var item = (WorldMapShapeElement)e.Item;
            
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryCities)
                return;

            string seriesName = e.Series.Name;
            Rect geoWorldRect = GeoMapAdapter.GetShapeRectFromPoints(item.ShapeRecord.Points);
            // scale up the zoom rect for selected element  
            geoWorldRect = geoWorldRect.Scale(5.0);

            switch (seriesName)
            {
                #region MapDrillDown : Regions -> States
                case "regionsMapSeries":
                    // get unique identifier for selected map element 
                    string selectedRegioneName = item.ShapeName;  
                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryStates,
                        WorldIdentifier = selectedRegioneName,
                        WorldRect = geoWorldRect
                    });

                    geoMap.Series["regionsMapSeries"].Visibility = Visibility.Collapsed;
                    // Make collapsed all the states that are not in the selected region
                    foreach (WorldMapShapeElement mapElement in this.ShapeStatesMapElements)
                    {
                        string currentRegionName = mapElement.ShapeRegionName;  
                        mapElement.Visibility = currentRegionName == selectedRegioneName ? Visibility.Visible : Visibility.Collapsed;

                    }
                    geoMap.Series["statesMapSeries"].Visibility = Visibility.Visible;
                    break; 
                #endregion
                #region MapDrillDown : States -> Counties
                case "statesMapSeries":
                    // get unique identifier for selected map element 
                    string selectedStateCode = item.ShapeCode;

                    foreach (WorldMapShapeElement mapElement in this.ShapeStatesMapElements)
                    {
                        if (mapElement.GetShapeId() == item.GetShapeId() && mapElement.Visibility == Visibility.Collapsed)
                            return; // skip on hidden map element 
                    }

                    // skip drill-down for Alaska and Hawaii due to lack of counties for these states
                    if (selectedStateCode == "AK" || selectedStateCode == "HI") return;

                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryCounties,
                        WorldIdentifier = selectedStateCode,
                        WorldRect = geoWorldRect
                    });

                    geoMap.Series["statesMapSeries"].Visibility = Visibility.Collapsed;
                    // Make collapsed all states that are not selected 
                    foreach (WorldMapShapeElement mapElement in this.ShapeCountiesMapElements)
                    {
                        string currentStateCode = mapElement.ShapeCode;  
                        mapElement.Visibility = currentStateCode == selectedStateCode ? Visibility.Visible : Visibility.Collapsed;
                    }
                    geoMap.Series["countiesMapSeries"].Visibility = Visibility.Visible;

                    break; 
                #endregion
                #region MapDrillDown : Counties -> Single County
                case "countiesMapSeries":
                    // get unique identifier for selected map element 
                    string selectedCountyName = item.ShapeName;      
                    string selectedCountyState = item.ShapeCode;

                    foreach (WorldMapShapeElement mapElement in this.ShapeCountiesMapElements)
                    {
                        if (mapElement.GetShapeId() == item.GetShapeId() && mapElement.Visibility == Visibility.Collapsed)
                            return; // skip on hidden map element 
                    }

                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryCities,
                        WorldIdentifier = selectedCountyState,
                        WorldRect = geoWorldRect
                    });

                    // Make collapsed all counties that are not selected 
                    foreach (WorldMapShapeElement mapElement in this.ShapeCountiesMapElements)
                    {
                        string currentCountyName = mapElement.ShapeName;    
                        string currentCountyState = mapElement.ShapeCode;   
                        // check for counties with the same name in different states
                        bool isSameCountyName = currentCountyName == selectedCountyName;
                        bool isSameCountyState = selectedCountyState == currentCountyState;
                        mapElement.Visibility = isSameCountyName && isSameCountyState ? Visibility.Visible : Visibility.Collapsed;
                    }
                    break; 
                #endregion
            }

            

            // Zoom to selected map element
            Rect windowRect = geoMap.GetZoomFromGeographic(geoWorldRect);
            geoMap.WindowRect = windowRect;

            //ZoomMapToRegion(geoWorldRect, TimeSpan.FromSeconds(0.5));

        }
        private void geoMap_SeriesMouseRightButtonDown(object sender, DataChartMouseButtonEventArgs e)
        {
            e.Handled = true;

            MapDrillUp();
        }
        private void geoMap_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            e.Handled = true;

            MapDrillUp();
        }

        private void MapDrillUp()
        {
            // skip map drill-up operation if the map is already at lowest map drill-down level
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryRegions) return;

            this.ViewModel.DrillUp();


            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryCounties)
            {
                string selectedStateCode = this.ViewModel.CurrentDrillDownView.WorldIdentifier;
                geoMap.Series["regionsMapSeries"].Visibility = Visibility.Collapsed;
                geoMap.Series["statesMapSeries"].Visibility = Visibility.Collapsed;
                geoMap.Series["countiesMapSeries"].Visibility = Visibility.Collapsed;

                // Make visible all counties that are in the selected state of U.S. 
                foreach (MapShapeElement mapElement in this.ShapeCountiesMapElements)
                {
                    string currentStateCode = mapElement.ShapeRecord.Fields[3].ToString();
                    mapElement.Visibility = currentStateCode == selectedStateCode ? Visibility.Visible : Visibility.Collapsed;
                }

                geoMap.Series["countiesMapSeries"].Visibility = Visibility.Visible;
            }
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryStates)
            {
                string selectedRegioneName = this.ViewModel.CurrentDrillDownView.WorldIdentifier;
                geoMap.Series["regionsMapSeries"].Visibility = Visibility.Collapsed;
                geoMap.Series["statesMapSeries"].Visibility = Visibility.Collapsed;
                geoMap.Series["countiesMapSeries"].Visibility = Visibility.Collapsed;

                // Make visible all states that are in the selected region 
                foreach (MapShapeElement mapElement in this.ShapeStatesMapElements)
                {
                    string currentRegionName = mapElement.ShapeRecord.Fields[4].ToString();
                    mapElement.Visibility = currentRegionName == selectedRegioneName ? Visibility.Visible : Visibility.Collapsed;
                }
                geoMap.Series["statesMapSeries"].Visibility = Visibility.Visible;

            }
            if (this.ViewModel.CurrentDrillDownLevel == WorldDrillDownLevel.CountryRegions)
            {
                string selectedRegioneName = this.ViewModel.CurrentDrillDownView.WorldIdentifier;

                geoMap.Series["regionsMapSeries"].Visibility = Visibility.Collapsed;
                geoMap.Series["statesMapSeries"].Visibility = Visibility.Collapsed;
                geoMap.Series["countiesMapSeries"].Visibility = Visibility.Collapsed;
                // Make visible all regions of U.S. 
                foreach (MapShapeElement mapElement in this.ShapeRegionsMapElements)
                {
                    mapElement.Visibility = Visibility.Visible;
                }
                geoMap.Series["regionsMapSeries"].Visibility = Visibility.Visible;
            }

            if (this.ViewModel.CurrentDrillDownView != null)
            {
                // Zoom to parent of a map element
                Rect geoWorldRect = this.ViewModel.CurrentDrillDownView.WorldRect;
                Rect windowRect = geoMap.GetZoomFromGeographic(geoWorldRect);
                geoMap.WindowRect = windowRect;
            }
        }

        

        private void geoMap_SeriesMouseLeftButtonUp(object sender, DataChartMouseButtonEventArgs e)
        {
            var item = (WorldMapShapeElement)e.Item;

            string seriesName = e.Series.Name;
            Rect geoWorldRect = GeoMapAdapter.GetShapeRectFromPoints(item.ShapeRecord.Points);
            // scale up the zoom rect for selected element by a factor of 3.0 percents
            geoWorldRect = geoWorldRect.Scale(5.0);

            // Zoom to selected map element
            Rect windowRect = geoMap.GetZoomFromGeographic(geoWorldRect);
            geoMap.WindowRect = windowRect;

            //ZoomMapToRegion(geoWorldRect, TimeSpan.FromSeconds(0.5));

            switch (seriesName)
            {
                #region MapDrillDown : Regions -> States
                case "regionsMapSeries":
                    // get unique identifier for selected map element 
                    string selectedRegioneName = item.ShapeName; // or item.ShapeRecord.Fields[1].ToString();  
                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryStates,
                        WorldIdentifier = selectedRegioneName,
                        WorldRect = geoWorldRect
                    });

                    geoMap.Series["regionsMapSeries"].Visibility = Visibility.Collapsed;
                    // Make collapsed all the states that are not in the selected region
                    foreach (WorldMapShapeElement mapElement in this.ShapeStatesMapElements)
                    {
                        string currentRegionName = mapElement.ShapeRegionName; // or mapElement.ShapeRecord.Fields[4].ToString(); 
                        mapElement.Visibility = currentRegionName == selectedRegioneName ? Visibility.Visible : Visibility.Collapsed;

                    }
                    geoMap.Series["statesMapSeries"].Visibility = Visibility.Visible;
                    break;
                #endregion
                #region MapDrillDown : States -> Counties
                case "statesMapSeries":
                    // get unique identifier for selected map element 
                    string selectedStateCode = item.ShapeCode;  // or item.ShapeRecord.Fields[3].ToString();  
                    // skip drill-down for Alaska and Hawaii due to lack of counties for these states
                    if (selectedStateCode == "AK" || selectedStateCode == "HI") return;

                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryCounties,
                        WorldIdentifier = selectedStateCode,
                        WorldRect = geoWorldRect
                    });

                    geoMap.Series["statesMapSeries"].Visibility = Visibility.Collapsed;
                    // Make collapsed all states that are not selected 
                    foreach (WorldMapShapeElement mapElement in this.ShapeCountiesMapElements)
                    {
                        string currentStateCode = mapElement.ShapeCode; // or mapElement.ShapeRecord.Fields[3].ToString();
                        mapElement.Visibility = currentStateCode == selectedStateCode ? Visibility.Visible : Visibility.Collapsed;
                    }
                    geoMap.Series["countiesMapSeries"].Visibility = Visibility.Visible;

                    break;
                #endregion
                #region MapDrillDown : Counties -> Single County
                case "countiesMapSeries":
                    // get unique identifier for selected map element 
                    string selectedCountyName = item.ShapeName;     // or item.ShapeRecord.Fields[4].ToString();
                    string selectedCountyState = item.ShapeCode;    // or item.ShapeRecord.Fields[3].ToString();

                    // save current map view as a world drill-down view
                    this.ViewModel.DrillDown(new WorldMapViewModel()
                    {
                        WorldDrillDownLevel = WorldDrillDownLevel.CountryCities,
                        WorldIdentifier = selectedCountyState,
                        WorldRect = geoWorldRect
                    });

                    // Make collapsed all counties that are not selected 
                    foreach (WorldMapShapeElement mapElement in this.ShapeCountiesMapElements)
                    {
                        string currentCountyName = mapElement.ShapeName;    // or mapElement.ShapeRecord.Fields[4].ToString();
                        string currentCountyState = mapElement.ShapeCode;   // or mapElement.ShapeRecord.Fields[3].ToString();
                        // check for counties with the same name in different states
                        bool isSameCountyName = currentCountyName == selectedCountyName;
                        bool isSameCountyState = selectedCountyState == currentCountyState;
                        mapElement.Visibility = isSameCountyName && isSameCountyState ? Visibility.Visible : Visibility.Collapsed;
                    }
                    break;
                #endregion
            }
        }


        public void ZoomMapToRegion(Rect geoRect, TimeSpan zoomDuration)
        {
            Rect currentWindowRect = geoMap.WindowRect;
            //GeoRegion geodeticGeoRegion = region;
            Rect windowRect = geoMap.GetZoomFromGeographic(geoRect);
            //GeoRegion cartesianGeoRegion = ProjectMapRegion(geoRegion, map);
            this.MapWindowRect = windowRect; // cartesianGeoRegion.ToRect();

            //currentWindowRect.Union(MapWindowRect);
            //geoMap.WindowRect = currentWindowRect;
            var timer = new DispatcherTimer { Interval = zoomDuration };
            timer.Tick += (o, e) =>
            {
                Rect newRect = geoMap.WindowRect.ScaleDown(1.0);

                if (newRect.Width < MapWindowRect.Width || newRect.Height < MapWindowRect.Height)
                {
                    geoMap.WindowRect = MapWindowRect;
                    ((DispatcherTimer)o).Stop();
                }
                else
                {
                    geoMap.WindowRect = newRect;
                }
                //geoMap.WindowRect = MapWindowRect;
                //((DispatcherTimer)o).Stop();
            };
            timer.Start();
        }
       

       
    }
    
    public enum WorldDrillDownLevel
    {
        World,
        Continents,
        Countries,
        CountryRegions,
        CountryStates,
        CountryCounties,
        CountryCities,
    }

    public class WorldDrillDownViewModel : ObservableModel
    {
        public WorldDrillDownViewModel(WorldMapViewModel initialMapView)
        {
            this.CurrentDrillDownLevel = WorldDrillDownLevel.World;
          
            this.WorldMapViews = new List<WorldMapViewModel>();
            this.WorldMapViews.Add(initialMapView);
   
        }
        public void DrillDown(WorldMapViewModel newMapView)
        {
            this.CurrentDrillDownLevel = newMapView.WorldDrillDownLevel;
            this.WorldMapViews.Add(newMapView);
        }
        public void DrillUp()
        {
            if (this.WorldMapViews.Count() > 1)
            {
                this.WorldMapViews.RemoveAt(this.WorldMapViews.Count()-1);
                this.CurrentDrillDownLevel = this.WorldMapViews[this.WorldMapViews.Count() - 1].WorldDrillDownLevel;
                this.CurrentDrillDownView = this.WorldMapViews[this.WorldMapViews.Count() - 1];

            }
        }
        private WorldDrillDownLevel _currentDrillDownLevel;
        public WorldDrillDownLevel CurrentDrillDownLevel
        {
            get
            {
                return _currentDrillDownLevel;
            }
            private set
            {
                _currentDrillDownLevel = value;
                OnPropertyChanged("CurrentDrillDownLevel");
            }
        }
        private WorldMapViewModel _currentDrillDownView;
        public WorldMapViewModel CurrentDrillDownView
        {
            get
            {
                return _currentDrillDownView;
            }
            private set
            {
                _currentDrillDownView = value;
                OnPropertyChanged("CurrentDrillDownView");
            }
        }

        private List<WorldMapViewModel> _worldViews;
        public List<WorldMapViewModel> WorldMapViews
        {
            get
            {
                return _worldViews;
            }
            private set
            {
                _worldViews = value;
                OnPropertyChanged("WorldMapViews");
            }
        }
         
    }

    public class WorldMapViewModel : ObservableModel
    {
        private WorldDrillDownLevel _worldDrillDownLevel;
        public WorldDrillDownLevel WorldDrillDownLevel
        {
            get
            {
                return _worldDrillDownLevel;
            }
            set
            {
                _worldDrillDownLevel = value;
                OnPropertyChanged("WorldDrillDownLevel");
            }
        }


        private string _worldIdentifier;
        public string WorldIdentifier
        {
            get
            {
                return _worldIdentifier;
            }
            set
            {
                _worldIdentifier = value;
                OnPropertyChanged("WorldIdentifier");
            }
        }
        
        private Rect _worldRect;
        public Rect WorldRect
        {
            get
            {
                return _worldRect;
            }
            set
            {
                _worldRect = value;
                OnPropertyChanged("WorldRect");
            }
        }
    }
}
