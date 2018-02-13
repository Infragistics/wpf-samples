using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using IGExtensions.Common.Maps;
using IGExtensions.Common.Maps.DataSelectors;
using IGExtensions.Common.Maps.StyleSelectors;
using IGExtensions.Common.Models;
using IGExtensions.Framework;
using IGShowcase.GeographicMapBrowser.Models;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;

namespace IGShowcase.GeographicMapBrowser.ViewModels
{
    public class MapViewModel : ObservableObject // : ObservableModel
    {
        public MapViewModel()
        {
            //this.MapZoomScale = 0.9;
            //this.MapLocationPane = new MapLocationViewModel() { Opacity = 0.75 };


            this.DataViewModel = new DataViewModel(); //new List<GeoDataViewSource>();
            this.DataViewModel.LoadDataCompleted += OnLoadDataCompleted;
          
            //_MapImageryBackgroundLayer = new MapBackgroundLayer();
            //_MapImageryBackgroundLayer.PropertyChanged += OnMapImageryBackgroundLayerChanged;
            //this.MapStartupMode = MapStartupMode.CustomMapMode;

            this.MapLayers = new GeoMapDataLayers();
            //this.MapImageryBackgroundLayer = new MapBackgroundLayer();
            this.MapImageryPreviewMapLayer = new GeoImageryPreviewMapLayer();
          
            //this.MapImageryBackgroundLayer.PropertyChanged += OnMapImageryBackgroundLayerChanged;
            //MapLocationPane = new MapElement();
            //MapNavigationPane = new MapElement();

            //this.MapLayers = new GeoMapDataLayers();

            this.PropertyChanged += OnMapViewModelPropertyChanged;
            //this.MapLayers.CollectionChanged += OnMapLayersChanged;
         
        }

        public bool IsMapInitialized { get; set; }
        public void InitializeData()
        {
            this.DataViewModel.LoadWorldShapefiles();
            //this.DataViewModel.LoadWorldAirports();
        }
        public void InitializeMap(XamGeographicMap geoMap)
        {
            this.Map = geoMap;
            this.Map.BackgroundContent.Opacity = 0;
            this.Map.IncreaseImageryZoomMaxLevel();
            this.Map.NavigateTo(GeoRegions.WorldNonAntarcticRegion);
            this.Map.Series.Clear();
            foreach (var mapLayer in MapLayers)
            {
                AddGeoMapLayer(mapLayer);
            }
            this.IsMapInitialized = true;
        }
        #region Event Handlers
        void OnMapViewModelPropertyChanged(object sender, PropertyChangedEventArgs ea)
        {
            switch (ea.PropertyName)
            {
                case "MapWindowRect":
                    {
                        this.LogPropertyChange(ea);

                       // this.Map.WindowRect = this.MapWindowRect;
                        break;
                    }
            }
        }
         
        /// <summary>
        /// Occurs when a new data source is loaded
        /// </summary>
        public event EventHandler<PropertyChangedEventArgs> LoadDataCompleted;
        private void OnLoadDataCompleted(object sender, PropertyChangedEventArgs e)
        {
            if (LoadDataCompleted != null)
                LoadDataCompleted(this, new PropertyChangedEventArgs(e.PropertyName));
        }

        private void OnMapImageryBackgroundLayerChanged(object sender, PropertyChangedEventArgs ea)
        {
            
            //OnPropertyChanged("MapImageryBackgroundLayer");
            //OnPropertyChanged("MapImageryBackgroundLayer." + ea.PropertyName);

            switch (ea.PropertyName)
            {
                case "ImageryViewModel":  
                    {
                        this.Map.LoadGeoImagery(this.MapImageryBackgroundLayer.ImageryViewModel);
                        this.LogPropertyChange(ea);
                        break;
                    }
                case "Opacity": 
                    {
                        this.Map.BackgroundContent.Opacity = this.MapImageryBackgroundLayer.Opacity;
                        break;
                    }
                case "Visibility":  
                    {
                        this.Map.BackgroundContent.Visibility = this.MapImageryBackgroundLayer.Visibility;
                        break;
                    }
            }
        }
        private void OnMapImageryPreviewMapLayerChanged(object sender, PropertyChangedEventArgs ea)
        {

            //OnPropertyChanged("MapImageryBackgroundLayer");
            //OnPropertyChanged("MapImageryBackgroundLayer." + ea.PropertyName);

            switch (ea.PropertyName)
            {
                case "ImageryViewModel":
                    {
                        var series = this.Map.Series.OfType<GeoImageryPreviewSeries>().FirstOrDefault();
                        if (series != null)
                            series.LoadGeoImagery(this.MapImageryPreviewMapLayer.ImageryViewModel);
                        //this.Map.LoadGeoImagery(this.MapImageryBackgroundLayer.ImageryViewModel);
                        this.LogPropertyChange(ea);
                        break;
                    }
                case "TileImageryExtentWidth":
                    {
                        var series = this.Map.Series.OfType<GeoImageryPreviewSeries>().FirstOrDefault();
                        if (series != null)
                            series.TileImageryExtentWidth = this.MapImageryPreviewMapLayer.TileImageryExtentWidth;
                        //this.Map.BackgroundContent.Opacity = this.MapImageryBackgroundLayer.Opacity;
                        break;
                    }
                case "TileImageryExtentHeight":
                    {
                        var series = this.Map.Series.OfType<GeoImageryPreviewSeries>().FirstOrDefault();
                        if (series != null)
                            series.TileImageryExtentHeight = this.MapImageryPreviewMapLayer.TileImageryExtentHeight;
                        //this.Map.BackgroundContent.Opacity = this.MapImageryBackgroundLayer.Opacity;
                        break;
                    }
                case "Opacity":
                    {
                        var series = this.Map.Series.OfType<GeoImageryPreviewSeries>().FirstOrDefault();
                        if (series != null)
                            series.Opacity = this.MapImageryPreviewMapLayer.Opacity;
                        //this.Map.BackgroundContent.Opacity = this.MapImageryBackgroundLayer.Opacity;
                        break;
                    }
                case "Visibility":
                    {
                        var series = this.Map.Series.OfType<GeoImageryPreviewSeries>().FirstOrDefault();
                        if (series != null)
                            series.Visibility = this.MapImageryPreviewMapLayer.Visibility;
                        //this.Map.BackgroundContent.Visibility = this.MapImageryBackgroundLayer.Visibility;
                        break;
                    }
            }
        }
        private void OnMapLayersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (this.Map == null) return;
            
            OnPropertyChanged("MapLayers");
            //e.Action =
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                AddGeoMapLayer(e.NewItems[0] as GeoMapDataLayer);
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                this.Map.Series.Clear();
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                //this.Map.Series.RemoveAt();
            }
            else if (e.Action == NotifyCollectionChangedAction.Replace)
            {
                //this.Map.Series[1] = new series
            }
        }
        private void OnMapSeriesMouseDoubleClick(object sender, DataChartMouseButtonEventArgs e)
        {
            var seriesName = e.Series.Name;
            var mousePoint = e.GetPosition(sender as UIElement);
             
            var seriesItem = e.Item as IGeoNavigational;
            if (seriesItem != null)
            {
                var geoPoint = this.Map.GetGeographicPoint(mousePoint);
                var geoRect = seriesItem.NavigationRect(geoPoint);
                if (geoRect == Rect.Empty) return;

               //TODO this.Map.ZoomMapToRegion(new GeoRect(geoRect), TimeSpan.FromSeconds(3));
                //var rect = this.Map.GetZoomFromGeographic(geoRect);
                //this.Map.WindowRect = rect;
            }
           
        }
        private void OnMapLayerChanged(object sender, PropertyChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("MapLayer.PropertyChanged " + e.PropertyName);
            if (e.PropertyName == "ImageryViewModel" ||
                e.PropertyName == "VisibilityProxy" ||
                e.PropertyName == "Visibility")
            {
                UpdateDataSourceTrademarks();
            }
        }
        #endregion

        #region Methods
        private void UpdateDataSourceTrademarks()
        {
            var trademarkList = string.Empty;
            var mapLayerList = this.MapLayers.ToList();
            mapLayerList.Reverse();

            foreach (var mapLayer in mapLayerList)
            {
                //if (mapLayer.SeriesView.Visibility == Visibility.Collapsed) continue;
                //if (mapLayer.Visibility == Visibility.Collapsed) continue;
                var trademark = mapLayer.DataSourceTrademark;
                if (trademark == string.Empty) continue;
                trademarkList += trademark;
                if (mapLayer != mapLayerList.Last()) trademarkList += ", ";
            }
            this.MapDataSourceTrademarks = trademarkList;
        }
        protected void AddGeoMapLayer(GeoDataViewSource dataViewSource, GeoSeriesType seriesType)
        {
            if (dataViewSource.DataType == GeoDataType.Shapes && seriesType == GeoSeriesType.GeographicShapeSeries)
            {
                // AddGeoMapLayer(MapLayer as GeoShapeMapLayer);
            }
        }
        protected void AddGeoMapLayer(GeoMapDataLayer mapLayer)
        {
            //mapLayer.PropertyChanged += OnMapLayerChanged;
            //mapLayer.SetSeriesName();
            mapLayer.InitializeMap(this.Map);
            mapLayer.InitializeSeries();

            DebugManager.LogTrace("MapViewModel.AddGeoMapLayer[" + mapLayer.SeriesIndex + "]: " + mapLayer.SeriesName);
                // MapLayer with type of GeoDataViewSource );
            if (mapLayer.SeriesType == GeoSeriesType.GeographicShapeSeries)
            {
                AddGeoMapLayer(mapLayer as GeoShapeMapLayer);
            }
            else if (mapLayer.SeriesType == GeoSeriesType.GeographicProportionalSymbolSeries)
            {
                AddGeoMapLayer(mapLayer as GeoSymbolProportionalMapLayer);
            }
            else if (mapLayer.SeriesType == GeoSeriesType.GeographicSymbolSeries)
            {
                AddGeoMapLayer(mapLayer as GeoSymbolMapLayer);
            }
            else if (mapLayer.SeriesType == GeoSeriesType.GeographicTileImagerySeries)
            {
                AddGeoMapLayer(mapLayer as GeoTileImageryMapLayer);
            }
            else if (mapLayer.SeriesType == GeoSeriesType.GeographicHighDensityScatterSeries)
            {
                AddGeoMapLayer(mapLayer as GeoHighDensityScatterMapLayer);
            }
            else if (mapLayer.SeriesType == GeoSeriesType.GeographicScatterAreaSeries)
            {
                AddGeoMapLayer(mapLayer as GeoScatterAreaMapLayer);
            }
            else if (mapLayer.SeriesType == GeoSeriesType.GeographicMotionSymbolSeries)
            {
                AddGeoMapLayer(mapLayer as GeoMotionMapLayer);
            } 
          
            UpdateDataSourceTrademarks();
        }
        protected void AddGeoMapLayer(GeoTileImageryMapLayer mapLayer)
        {
            mapLayer.PropertyChanged += OnMapLayerChanged;
            //mapLayer.ImageryViewModel.PropertyChanged += OnImageryMapLayerChanged;
            //mapLayer.SeriesView.ItemsSource = dataSource.View; //WorldCountries.View;
            //var windowRect = this.Map.GetZoomFromGeographic(GeoRect.WorldRect.ToRect());
            //this.Map.WindowRect = windowRect;
            this.Map.Series.Add(mapLayer.SeriesView);
        }
        protected void AddGeoMapLayer(GeoScatterAreaMapLayer mapLayer)
        {
            mapLayer.PropertyChanged += OnMapLayerChanged;
            var dataSourceKey = mapLayer.DataSourceKey;
            var triangulationSource = this.DataViewModel.DataSources[dataSourceKey] as TriangulationDataViewSource;
            if (triangulationSource != null)
            {
                //var triangulationSource = triangulationSource.DataSource;
                //mapLayer.SeriesView.Title = mapLayer.Title;
                 //TODO use ColorValueScale
                //if (mapLayer.ColorScale.Palette.Count >= 2)
                //{
                //    var min = mapLayer.HeatScale.Brushes.First() as SolidColorBrush;
                //    var max = mapLayer.HeatScale.Brushes.Last() as SolidColorBrush;

                //    if (min != null && max != null)
                //    {
                //        mapLayer.ActualSeriesView.HeatMinimumColor = min.Color;
                //        mapLayer.ActualSeriesView.HeatMaximumColor = max.Color;
                //    }
                //}
                //mapLayer.ActualSeriesView.HeatMinimum = mapLayer.HeatScale.MinimumValue;
                //mapLayer.ActualSeriesView.HeatMaximum = mapLayer.HeatScale.MaximumValue;
                mapLayer.ActualSeriesView.ItemsSource = triangulationSource.DataSource.Points;
                mapLayer.ActualSeriesView.TrianglesSource = triangulationSource.DataSource.Triangles; 

                mapLayer.ActualSeriesView.ToolTip = mapLayer.ToolTip;

                var windowRect = this.Map.GetZoomFromGeographic(triangulationSource.DataWorldRect.ToRect());
                this.Map.WindowRect = windowRect;

                this.Map.Series.Add(mapLayer.ActualSeriesView);

            }
        }
        protected void AddGeoMapLayer(GeoHighDensityScatterMapLayer mapLayer)
        {
            mapLayer.PropertyChanged += OnMapLayerChanged;
            var dataSourceKey = mapLayer.DataSourceKey;
            var dataSource = this.DataViewModel.DataSources[dataSourceKey];
            if (dataSource != null)
            {

                //mapLayer.SeriesView.Title = mapLayer.Title;
                 //TODO use ColorValueScale
                if (mapLayer.HeatScale.Brushes.Count >= 2)
                {
                    var min = mapLayer.HeatScale.Brushes.First() as SolidColorBrush;
                    var max = mapLayer.HeatScale.Brushes.Last() as SolidColorBrush;

                    if (min != null && max != null)
                    {
                        mapLayer.ActualSeriesView.HeatMinimumColor = min.Color;
                        mapLayer.ActualSeriesView.HeatMaximumColor = max.Color;
                    }
                }
                mapLayer.ActualSeriesView.HeatMinimum = mapLayer.HeatScale.MinimumValue;
                mapLayer.ActualSeriesView.HeatMaximum = mapLayer.HeatScale.MaximumValue;
                mapLayer.ActualSeriesView.ItemsSource = dataSource.View; //WorldCountries.View;
                if (mapLayer.LabelMemberPath != string.Empty)
                {
                    mapLayer.ActualSeriesView.ToolTip = mapLayer.ToolTip;
                }

                var windowRect = this.Map.GetZoomFromGeographic(dataSource.DataWorldRect.ToRect());
                this.Map.WindowRect = windowRect;

                this.Map.Series.Add(mapLayer.SeriesView);

            }
        }
        protected void AddGeoMapLayer(GeoShapeMapLayer mapLayer)
        {
            mapLayer.PropertyChanged += OnMapLayerChanged;
            
            var dataSourceKey = mapLayer.DataSourceKey;
            var dataSource = this.DataViewModel.DataSources[dataSourceKey];
            //
            if (dataSource != null)
            {
                dataSource.FilterSettings.ItemsSourceKey = dataSourceKey;
                mapLayer.DataViewSource = dataSource;

                //MapLayer.SeriesView.Visibility = MapLayer.Visibility;
                //MapLayer.SeriesView.Opacity = MapLayer.Opacity;

                 //MapLayer.SeriesView.ShapeStyleSelector = MapLayer.ShapeStyleSettings.ShapeStyleSelector;

                //mapLayer.SeriesView.Title = mapLayer.Title;

                //MapLayer.SeriesView.Brush = NamedColors.Orange.ToBrush();

                mapLayer.ShapeStyleSettings.ShapeStyleSelectorType = StyleSelectorType.SingleShapeStyle;
                mapLayer.ShapeStyleSettings.ShapeFill = NamedColors.GreenYellow.BrushOpacity(0.5);
                //var style = new Style(typeof(Path));
                //style.Setters.Add(new Setter(Path.FillProperty, NamedColors.DodgerBlue.ToBrush()));
                //style.Setters.Add(new Setter(Path.StrokeProperty, NamedColors.White.ToBrush()));
                //style.Setters.Add(new Setter(Path.StrokeThicknessProperty, 0.75));
                //MapLayer.SeriesView.ShapeStyle = style;

                mapLayer.ActualSeriesView.ShapeStyle = mapLayer.ShapeStyleSettings.GetShapeStyle();
                //mapLayer.SeriesView.ShapeStyle = mapLayer.ShapeStyleSettings.GetShapeStyle();

                //MapLayer.SeriesView.ShapeMemberPath = MapLayer.ShapeMemberPath;
               //mapLayer.SeriesView.Opacity = mapLayer.Opacity;

                //mapLayer.SeriesView.ItemsSource = dataSource.View; //WorldCountries.View;
                mapLayer.SeriesView.ItemsSource = dataSource.View; //WorldCountries.View;
                mapLayer.ActualSeriesView.ToolTip = mapLayer.ToolTip;

                //TODO recalculate based on points of shapes
                //var windowRect = this.Map.GetZoomFromGeographic(dataSource.DataWorldRect.ToRect());
                //this.Map.WindowRect = windowRect;
                this.Map.Series.Add(mapLayer.SeriesView);

            }
            //var series = new GeographicShapeSeries();
            //series.Name = "WorldCountries";
            //if (this.DataSources.ContainsKey(dataSourceKey))
            //{
            //    MapLayer.SeriesView.ItemsSource = this.DataSources[dataSourceKey].View; //WorldCountries.View;

            //}

            //series.ShapeMemberPath = MapLayer.SeriesShapeMemberPath;
            //series.Brush = new SolidColorBrush(Colors.Red);
            //series.Opacity = 0.6;
            //series.ShapeFilterResolution = 10;
            //MapLayer.SeriesView = series;

            //this.MapLayers.Add(MapLayer);
        }
        protected void AddGeoMapLayer(GeoSymbolMapLayer mapLayer)
        {
            mapLayer.PropertyChanged += OnMapLayerChanged;
            var dataSourceKey = mapLayer.DataSourceKey;
            if (dataSourceKey == DataViewModel.UnitedStatesAirlineTrafficKey)
            {
                var dataSource = this.DataViewModel.DataSources[dataSourceKey] as AirlineTrafficDataViewSource;
                //dataSource.PropertyChanged += OnAirlineTrafficDataViewSourceChanged;
              

                //var dataSource = this.DataViewModel.UnitedStatesAirlineTraffic;
                mapLayer.DataViewSource = dataSource;
                //mapLayer.SeriesView.ItemsSource = dataSource.ActualFlights;
                mapLayer.SeriesView.PropertyChanged += new PropertyChangedEventHandler(SeriesView_PropertyChanged);
                mapLayer.ActualSeriesView.PropertyUpdated += new PropertyUpdatedEventHandler(ActualSeriesView_PropertyUpdated);
                //TODO add airports - prop series
                //TODO add flights - symbol series
                var tooltipLabel = new TextBlock { Margin = new Thickness(5) };
                tooltipLabel.SetBinding(TextBlock.TextProperty, new Binding("Item.Label"));
                var tooltip = new Border { Opacity = 0.8 };
                tooltip.Background = NamedColors.White.ToBrush();
                tooltip.Child = tooltipLabel;
                //mapLayer.SeriesView.ToolTip = tooltip;

                mapLayer.ActualSeriesView.ItemsSource = dataSource.ActualAirports;
                //mapLayer.SeriesView.ItemsSource = dataSource.ActualAirports;
                //mapLayer.SeriesView.ItemsSource = dataSource.AirportsDataSource;
                this.Map.Series.Add(mapLayer.ActualSeriesView);
            }
        }
        protected void AddGeoMapLayer(GeoMotionMapLayer mapLayer)
        {
            mapLayer.PropertyChanged += OnMapLayerChanged;
            var dataSourceKey = mapLayer.DataSourceKey;
            if (dataSourceKey == DataViewModel.UnitedStatesAirlineTrafficKey)
            {
                var dataSource = this.DataViewModel.DataSources[dataSourceKey] as AirlineTrafficDataViewSource;
                if (dataSource == null) return;
                
                //var dataSource = this.DataViewModel.UnitedStatesAirlineTraffic;
                mapLayer.DataViewSource = dataSource;
              
                //TODO add airports - prop series
                //TODO add flights - symbol series
            
                //mapLayer.AirportsView.ActualSeriesView.FillScale = mapLayer.AirportsView.FillValueScale;
                
                //mapLayer.AirportsView.ActualSeriesView.ToolTip = mapLayer.AirportsView.ToolTip;
                mapLayer.AirportsView.ActualSeriesView.ItemsSource = dataSource.ActualAirports;

                //mapLayer.AirplanesView.ActualSeriesView.ToolTip = mapLayer.AirplanesView.ToolTip;
                mapLayer.AirplanesView.ActualSeriesView.ItemsSource = dataSource.ActualFlights;
                mapLayer.AirplanesView.ActualSeriesView.MaximumMarkers = dataSource.ActualFlights.Count;
                mapLayer.AirplanesView.ActualSeriesView.TransitionDuration = TimeSpan.FromSeconds(0.25);

                if (mapLayer.AirplanesView.LabelMemberPath != string.Empty)
                {
                    mapLayer.AirplanesView.ActualSeriesView.ToolTip = mapLayer.AirplanesView.ToolTip;
                }
                if (mapLayer.AirportsView.LabelMemberPath != string.Empty)
                {
                    mapLayer.AirportsView.ActualSeriesView.ToolTip = mapLayer.AirportsView.ToolTip;
                }

                var windowRect = this.Map.GetZoomFromGeographic(dataSource.DataWorldRect.ToRect());
                this.Map.WindowRect = windowRect;
                
                this.Map.Series.Add(mapLayer.AirportsView.ActualSeriesView);
                this.Map.Series.Add(mapLayer.AirplanesView.ActualSeriesView);
            }
        }

        //TODO move to map multi layer
        private void OnAirlineTrafficDataViewSourceChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ActualAirports")
            {
                foreach (var mapLayer in MapLayers)
                {
                    if (mapLayer.DataSourceKey == DataViewModel.UnitedStatesAirlineTrafficKey)
                    {
                        System.Diagnostics.Debug.WriteLine("US AirlineTraffic Layer.ItemsSource changing... ");
                        var dataSource = mapLayer.DataViewSource as AirlineTrafficDataViewSource;
                        //mapLayer.SeriesView.ItemsSource = dataSource.ActualAirports;
                        break;
                    }
                }

            }
        }

        void ActualSeriesView_PropertyUpdated(object sender, PropertyUpdatedEventArgs e)
        {
            if (e.PropertyName == "ItemsSource")
            {
                var series = sender as Series;
                var items = series.ItemsSource as ObservableCollection<Airport>;
                if (items != null)
                    System.Diagnostics.Debug.WriteLine("US AirlineTraffic Layer.ItemsSource " + items.Count);

                //var stop = true;
            }
        }

        void SeriesView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ItemsSource")
            {
                //var stop = true;
            }
        }

        protected void AddGeoMapLayer(GeoSymbolProportionalMapLayer mapLayer)
        {
            mapLayer.PropertyChanged += OnMapLayerChanged;

            var dataSourceKey = mapLayer.DataSourceKey;
              
               //var dataSource = this.DataViewModel.DataSources[dataSourceKey];
            GeoDataViewSource dataSource = null;
            if (dataSourceKey == DataViewModel.WorldCitiesKey)
                dataSource = this.DataViewModel.WorldCities;
            else if (dataSourceKey == DataViewModel.WorldCountriesKey)
                dataSource = this.DataViewModel.WorldCountries;
            else if (dataSourceKey == DataViewModel.WorldWeatherKey)
                dataSource = this.DataViewModel.WorldWeather;
            else if (dataSourceKey == DataViewModel.WorldEarthQuakesKey)
                dataSource = this.DataViewModel.WorldEarthQuakes;
            //else if (dataSourceKey == DataViewModel.UnitedStatesAirlineTrafficKey)
            //    dataSource = this.DataViewModel.UnitedStatesAirlineTraffic;

            if (dataSource != null)
            {
                mapLayer.DataViewSource = dataSource;
             
                mapLayer.SeriesView.Visibility = mapLayer.Visibility;
                mapLayer.SeriesView.Title = mapLayer.Title;
                mapLayer.SeriesView.Opacity = mapLayer.Opacity;
                mapLayer.SeriesView.ItemsSource = mapLayer.DataViewSource.View; //WorldCountries.View;
                //mapLayer.SeriesView.ItemsSource = dataSource.View; //WorldCountries.View;
                 
                //mapLayer.ActualSeriesView.MaximumMarkers = dataSource.DataItemsCount;
                mapLayer.ActualSeriesView.MaximumMarkers = mapLayer.DataViewSource.DataItemsCount;
                //mapLayer.ActualSeriesView.MarkerCollisionAvoidance = mapLayer.MarkersLayout;
                //mapLayer.ActualSeriesView.MarkerType = mapLayer.MarkerType;

                //mapLayer.ActualSeriesView.LongitudeMemberPath = mapLayer.LongitudeMemberPath;
                //mapLayer.ActualSeriesView.LatitudeMemberPath = mapLayer.LatitudeMemberPath;
                mapLayer.ActualSeriesView.RadiusMemberPath = mapLayer.RadiusMemberPath;
                mapLayer.ActualSeriesView.FillMemberPath = mapLayer.FillMemberPath;
                //mapLayer.ActualSeriesView.FillScale.Brushes = mapLayer.FillValueScale.Brushes;
                //mapLayer.ActualSeriesView.FillScale. = mapLayer.FillValueScale;
                mapLayer.ActualSeriesView.FillScale = mapLayer.FillValueScale;


                //MapLayer.SeriesView.RadiusScale = MapLayer.RadiusScale;
                //MapLayer.SeriesView.FillScale = MapLayer.FillScale;

                if (mapLayer.LabelMemberPath != string.Empty)
                {
                    mapLayer.SeriesView.ToolTip = mapLayer.ToolTip;
                }

                //var windowRect = this.Map.GetZoomFromGeographic(mapLayer.DataViewSource.DataWorldRect.ToRect());
                //this.Map.WindowRect = windowRect;
                this.Map.Series.Add(mapLayer.SeriesView);
            }
        }
       
        #endregion
         
        #region Properties
        public DataViewModel DataViewModel { get; set; }

        protected XamGeographicMap Map { get; set; }

        //public XamGeographicMap Map { get; set; }
        //private XamGeographicMap _geoMap;
        ///// <summary>
        ///// Gets or sets Geographic Map control 
        ///// </summary>
        //public XamGeographicMap Map
        //{
        //    get { return _geoMap; }
        //    set
        //    {
        //        if (_geoMap == value) return; _geoMap = value;
        //        GeoMapAdapter.SeriesMouseDoubleClick += OnMapSeriesMouseDoubleClick;
        //        this.Map.EnableSeriesMouseDoubleClick(true);
        //        //OnPropertyChanged("Map"); 
        //    }
        //}
        private Rect _mapWindowRect = new Rect();
        /// <summary>
        /// Gets or sets MapWindowRect property 
        /// </summary>
        public Rect MapWindowRect
        {
            //get {  return _mapWindowRect; }
            //set { if (_mapWindowRect == value) return; _mapWindowRect = value; OnPropertyChanged("MapWindowRect"); }
            get
            {
                if(this.Map != null)
                {
                    _mapWindowRect = this.Map.ActualWindowRect;
                }
                return _mapWindowRect;
            }
            set 
            { 
                if (_mapWindowRect == value) return; 
                _mapWindowRect = value;
                if (this.Map != null) this.Map.WindowRect = _mapWindowRect;
                OnPropertyChanged("MapWindowRect"); 
            }
        }

        private bool _isMapZoomable = true;
        /// <summary>
        /// Gets or sets MapDataSourceInfo property 
        /// </summary>
        public bool IsMapZoomable
        {
            get { return _isMapZoomable; }
            set { if (_isMapZoomable == value) return; _isMapZoomable = value; OnPropertyChanged("IsMapZoomable"); }
        }

        private string _mapDataSourceTrademarks;
        /// <summary>
        /// Gets or sets MapDataSourceInfo property 
        /// </summary>
        public string MapDataSourceTrademarks
        {
            get { return _mapDataSourceTrademarks; }
            set { if (_mapDataSourceTrademarks == value) return; _mapDataSourceTrademarks = value; OnPropertyChanged("MapDataSourceTrademarks"); }
        }
        private GeoMapDataLayers _mapLayers;
        /// <summary>
        /// Gets or sets MapLayers property 
        /// </summary>
        public GeoMapDataLayers MapLayers
        {
            get { return _mapLayers; }
            set
            {
                if (_mapLayers == value) return;
                if (_mapLayers != null) _mapLayers.CollectionChanged -= OnMapLayersCollectionChanged;
                _mapLayers = value;
                if (_mapLayers != null) _mapLayers.CollectionChanged += OnMapLayersCollectionChanged;
                OnPropertyChanged("MapLayers");
            }
        }

        private MapBackgroundLayer _mapImageryBackgroundLayer;
        public MapBackgroundLayer MapImageryBackgroundLayer
        {
            get { return _mapImageryBackgroundLayer; }
            set
            {
                if (value == null) return;
                if (_mapImageryBackgroundLayer == value) return;
                if (_mapImageryBackgroundLayer != null) _mapImageryBackgroundLayer.PropertyChanged -= OnMapImageryBackgroundLayerChanged;
                _mapImageryBackgroundLayer = value;
                if (_mapImageryBackgroundLayer != null) _mapImageryBackgroundLayer.PropertyChanged += OnMapImageryBackgroundLayerChanged;
                OnPropertyChanged("MapImageryBackgroundLayer");
            }
        }
     
        private GeoImageryPreviewMapLayer _mapImageryPreviewMapLayer;
        public GeoImageryPreviewMapLayer MapImageryPreviewMapLayer
        {
            get { return _mapImageryPreviewMapLayer; }
            set
            {
                if (_mapImageryPreviewMapLayer == value) return;
                if (_mapImageryPreviewMapLayer != null) _mapImageryPreviewMapLayer.PropertyChanged -= OnMapImageryPreviewMapLayerChanged;
                _mapImageryPreviewMapLayer = value;
                if (_mapImageryPreviewMapLayer != null) _mapImageryPreviewMapLayer.PropertyChanged += OnMapImageryPreviewMapLayerChanged;
                OnPropertyChanged("MapImageryPreviewMapLayer");
            }
        }
        
        //private GeoMapLayers _MapLayers;
        //public GeoMapLayers MapLayers
        //{
        //    get { return _MapLayers; }
        //    set { if (_MapLayers == value) return; _MapLayers = value; OnPropertyChanged("MapLayers"); }
        //} 

        //private MapStartupMode _mapStartupMode;
        ///// <summary>
        ///// Gets or sets MapStartupMode property 
        ///// </summary>
        //public MapStartupMode MapStartupMode
        //{
        //    get { return _mapStartupMode; }
        //    set { if (_mapStartupMode == value) return; _mapStartupMode = value; OnPropertyChanged("MapStartupMode"); }
        //}
        #endregion

        //public MapElement MapLocationPane { get; set; }
        //public MapElement MapNavigationPane { get; set; }


        //public double MapZoomScale { get; set; }

        //public MapLocationViewModel MapLocationPane { get; set; }



        //public const string ImageryStylePropertyName = "ImageryStyle";
        ///// <summary>
        ///// <para> Gets or sets a map style of the Bing Maps imagery tiles. </para>
        ///// </summary>
        //public BingMapsImageryStyle ImageryStyle
        //{
        //    get { return (BingMapsImageryStyle)GetValue(ImageryStyleProperty); }
        //    set { SetValue(ImageryStyleProperty, value); }
        //}
        //public static readonly DependencyProperty ImageryStyleProperty =
        //    DependencyProperty.Register(ImageryStylePropertyName, typeof(BingMapsImageryStyle), typeof(MapViewModel),
        //    new PropertyMetadata(BingMapsImageryStyle.StreetMapStyle, (o, e) =>
        //        (o as MapViewModel).OnImageryStylePropertyChanged((BingMapsImageryStyle)e.OldValue, (BingMapsImageryStyle)e.NewValue)));

        //private void OnImageryStylePropertyChanged(BingMapsImageryStyle oldValue, BingMapsImageryStyle newValue)
        //{
        // //   this.Validate();
        //}

    }
    /// <summary>
    /// Represents starting mode of the map browser
    /// </summary>
    public enum MapStartupMode
    {
        ///// <summary>
        ///// Specify limitless map mode which allows map editing with limitless map settings
        ///// </summary>
        //LimitlessMode,
        /// <summary>
        /// Specify custom map mode which allows map editing with custom map settings
        /// </summary>
        CustomMapMode,
        /// <summary>
        /// Specify ESRI map mode which limits editing of the map to BingMaps imagery settings
        /// </summary>
        BingMapsMode,
        /// <summary>
        /// Specify ESRI map mode which limits editing of the map to OpenStreetMap imagery settings
        /// </summary>
        OpenStreetMapMode,
        /// <summary>
        /// Specify ESRI map mode which limits editing of the map to CloudMadeMap imagery settings
        /// </summary>
        CloudMadeMapMode,
        /// <summary>
        /// Specify ESRI map mode which limits editing of the map to ESRI imagery settings
        /// </summary>
        EsriMapMode,
    }
}