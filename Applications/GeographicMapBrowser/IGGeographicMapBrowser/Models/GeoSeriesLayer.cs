using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using IGExtensions.Common.Maps.DataSelectors;
using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Common.Maps.StyleSelectors;
using IGExtensions.Common.Models;
using IGExtensions.Common.Scales;
using IGExtensions.Framework;
using IGShowcase.GeographicMapBrowser.ViewModels;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using SizeScale = Infragistics.Controls.Charts.SizeScale;

namespace IGShowcase.GeographicMapBrowser.Models
{
    internal interface IGeoDataSource //: IEnumerable
    {
        GeoDataType DataType { get; set; }
    }
    /// <summary>
    /// Represents a base map layer with a data source view for Geographic Series 
    /// </summary>
    abstract public class GeoMapDataLayer : ObservableModel 
    {

        public void InitializeMap(XamGeographicMap map)
        {
            this.MapView = map;
            //SetSeriesName();
        }

        #region Common Properties for all Series Views
        protected readonly string IsVisiblePropertyName = "IsVisible";
        /// <summary>
        /// Gets or sets IsVisible property 
        /// </summary>
        public bool IsVisible
        {
            get { return this.Visibility == Visibility.Visible; }
            set
            {
                if (this.IsVisible == value) return;
                this.Visibility = value ? Visibility.Visible : Visibility.Collapsed; OnPropertyChanged(IsVisiblePropertyName);
            }
        }
        protected readonly string TitlePropertyName = "Title";
        private string _title;
        /// <summary>
        /// Gets or sets Title property 
        /// </summary>
        public string Title
        {
            get {  return _title; }
            set { if (_title == value) return; _title = value; OnPropertyChanged(TitlePropertyName); }
        }

        protected readonly string VisibilityPropertyName = "Visibility";
        private Visibility _visibility = Visibility.Visible;
        /// <summary>
        /// Gets or sets Visibility of the Geographic series
        /// </summary>
        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                if (_visibility == value) return; _visibility = value;
                OnPropertyChanged(VisibilityPropertyName); OnPropertyChanged(IsVisiblePropertyName);
            }
        }
        protected readonly string OpacityPropertyName = "Opacity";
        private double _opacity = 1.0;
        /// <summary>
        /// Gets or sets Opacity of the Geographic series
        /// </summary>
        public double Opacity
        {
            get { return _opacity; }
            set { if (_opacity == value) return; _opacity = value; OnPropertyChanged(OpacityPropertyName); }
        }
       
        #endregion
        protected static int SeriesIndexer = 0;
        public void InitializeSeries()
        {
            this.SeriesIndex = SeriesIndexer;
            this.SeriesIndtifier = this.SeriesType + "-" + this.SeriesIndex + "-" + DataSourceKey;
            this.SeriesName = this.SeriesIndtifier;
            //this.SeriesName = this.SeriesType + this.SeriesIndex.ToString();
            SeriesIndexer++;
            //SeriesIndtifier++;
        }
        public int SeriesIndex { get; protected set; }
        public string SeriesIndtifier { get; protected set; }

        //public object SeriesTitle { get; set; }

        /// <summary>
        /// Gets or sets Name of the Geographic series
        /// </summary>
        public string SeriesName
        {
            //get { return this.SeriesView.Name; }
            //protected set { if (this.SeriesView.Name == value) return; this.SeriesView.Name = value; OnPropertyChanged("SeriesName"); }
            get { return _seriesName; }
            protected set { if (_seriesName == value) return; _seriesName = value; OnPropertyChanged("SeriesName"); }
        }
        private string _seriesName = string.Empty;
        #region Properties
        //TODO changed to layer type
        private GeoSeriesType _seriesType;
        /// <summary>
        /// Gets a SeriesType of a Geographic series
        /// </summary>
        public GeoSeriesType SeriesType
        {
            get { return _seriesType; }
            protected set
            {
                if (_seriesType == value) return; _seriesType = value;
                //this.IsMapLayerEditable = SeriesType != GeoSeriesType.NoneSelected;
                OnPropertyChanged("SeriesType");
            }
        }
       
        public XamGeographicMap MapView { get; set; }
        private GeoDataViewSource _dataViewSource;
        /// <summary>
        /// Gets or sets SeriesView property 
        /// </summary>
        public GeoDataViewSource DataViewSource
        {
            get { return _dataViewSource; }
            set { if (_dataViewSource == value) return; _dataViewSource = value; OnPropertyChanged("DataViewSource"); }
        }

        private string _dataSourceKey;
        /// <summary>
        /// Gets or sets key used to identify data source associated with the current map layer
        /// </summary>
        public string DataSourceKey
        {
            get { return _dataSourceKey; }
            set { if (_dataSourceKey == value) return; _dataSourceKey = value; OnPropertyChanged("DataSourceKey"); }
        }

        private string _dataSourceTrademark;
        /// <summary>
        /// Gets or sets trademark associated with data source of current map layer
        /// </summary>
        public string DataSourceTrademark
        {
            get { return _dataSourceTrademark; }
            set { if (_dataSourceTrademark == value) return; _dataSourceTrademark = value; OnPropertyChanged("DataSourceTrademark"); }
        } 
        #endregion
    }
    /// <summary>
    /// Represents a base map layer with a view of multi Geographic Series 
    /// </summary>
    abstract public class GeoMultiMapLayer : GeoMapDataLayer 
    {
        protected GeoMultiMapLayer()
        {
            
        }
        private List<GeoMapLayer> _views = new List<GeoMapLayer>();
        /// <summary>
        /// Gets or sets Views property 
        /// </summary>
        public List<GeoMapLayer> Views
        {
            get { return _views; }
            set
            {
                if (_views == value) return;
                _views = value;
                OnPropertyChanged("Views");
            }
        }
    }
    public class GeoMotionMapLayer : GeoMapDataLayer // GeoMapLayer // GeoMapDataLayer GeoMultiMapLayer
    {
        public GeoMotionMapLayer()
        {
            this.SeriesType = GeoSeriesType.GeographicMotionSymbolSeries;

            _airportsView = new GeoSymbolMapLayer();
            _airplanesView = new GeoSymbolMapLayer();

            //_airportsView.LongitudeMemberPath = "Longitude";
            //_airportsView.LatitudeMemberPath = "Latitude";
            //_airportsView.RadiusScale = new SizeScale { MinimumValue = 5, MaximumValue = 50};


            this.PropertyChanged += OnMapLayerPropertyChanged;
            //this.DataViewSource.PropertyChanged += OnDataViewSourceChanged;
        }
        //public GeographicProportionalSymbolSeries AiportsSeriesView { get { return SeriesView as GeographicProportionalSymbolSeries; } }
        void OnMapLayerPropertyChanged(object sender, PropertyChangedEventArgs ea)
        {
            //TODO update series view based on changes from base class:
            if (ea.PropertyName == base.VisibilityPropertyName)
            {
                this.AirportsView.Visibility = this.Visibility;
                this.AirplanesView.Visibility = this.Visibility;
            }
            if (ea.PropertyName == base.OpacityPropertyName)
            {
                this.AirportsView.Opacity = this.Opacity;
                this.AirplanesView.Opacity = this.Opacity;
            }
            if (ea.PropertyName == base.TitlePropertyName)
            {
                this.AirportsView.Title = this.Title;
                this.AirplanesView.Title = this.Title;
            }
            // this.LogPropertyChange(ea);
        }
        //private GeographicProportionalSymbolSeries _airportsView;
        ///// <summary>
        ///// Gets or sets property property 
        ///// </summary>
        //public GeographicProportionalSymbolSeries AirportsView
        //{
        //    get { return _airportsView; }
        //    set { if (_airportsView == value) return; _airportsView = value; OnPropertyChanged("AirportsView"); }
        //}
        private GeoSymbolMapLayer _airportsView;
        /// <summary>
        /// Gets or sets property property 
        /// </summary>
        public GeoSymbolMapLayer AirportsView
        {
            get { return _airportsView; }
            set { if (_airportsView == value) return; _airportsView = value; OnPropertyChanged("AirportsView"); }
        }

        //private GeoSymbolMapLayer _aiportsView;
        ///// <summary>
        ///// Gets or sets property property 
        ///// </summary>
        //public GeoSymbolMapLayer AiportsView
        //{
        //    get { return _aiportsView; }
        //    set { if (_aiportsView == value) return; _aiportsView = value; OnPropertyChanged("AiportsView"); }
        //}
        private GeoSymbolMapLayer _airplanesView;
        /// <summary>
        /// Gets or sets property property 
        /// </summary>
        public GeoSymbolMapLayer AirplanesView
        {
            get { return _airplanesView; }
            set { if (_airplanesView == value) return; _airplanesView = value; OnPropertyChanged("AirplanesView"); }
        }


        void OnDataViewSourceChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.DataViewSource is AirlineTrafficDataViewSource &&
                e.PropertyName == "ActualAirports")
            {
                //foreach (var mapSeries in this.MapView.Series)
                //{
                //    if (mapSeries.Name == this.DataSourceKey == DataViewModel.UnitedStatesAirlineTrafficKey)
                //    {
                //        System.Diagnostics.Debug.WriteLine("US AirlineTraffic Layer.ItemsSource changing... ");
                //        var dataSource = mapLayer.DataViewSource as AirlineTrafficDataViewSource;
                //        //mapLayer.SeriesView.ItemsSource = dataSource.ActualAirports;
                //        break;
                //    }
                //}
            }
        }
    }

    /// <summary>
    /// Represents a base map layer with a view for single Geographic Series 
    /// </summary>
    abstract public class GeoMapLayer : GeoMapDataLayer 
    {
        protected GeoMapLayer()
        {
            //SeriesIndtifier++;
            //this.SeriesIndex = SeriesIndtifier;
            this.SeriesType = GeoSeriesType.NoneSelected;


            this.PropertyChanged += OnMapLayerPropertyChanged;

        }
        protected GeoMapLayer(Series series)
        {
            this.SeriesType = GeoSeriesType.NoneSelected;
            this.SeriesView = series;
       
            this.PropertyChanged += OnMapLayerPropertyChanged;
        }

        void OnMapLayerPropertyChanged(object sender, PropertyChangedEventArgs ea)
        {
            //TODO update series view based on changes from base class:
            if (ea.PropertyName == base.VisibilityPropertyName)
            {
                this.SeriesView.Visibility = this.Visibility;
            }
            if (ea.PropertyName == base.OpacityPropertyName)
            {
                this.SeriesView.Opacity = this.Opacity;
            }
            if (ea.PropertyName == base.TitlePropertyName)
            {
                this.SeriesView.Title = this.Title;
            }
           // this.LogPropertyChange(ea);
        }
        //private GeoDataFilterSettings _dataView;
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public GeoDataFilterSettings DataView
        //{
        //    get { return _dataView; }
        //    set { if (_dataView == value) return; _dataView = value; OnPropertyChanged("DataView"); }
        //}

        public void SyncSeriesViewPropertyChanges()
        {
            //if (this.SeriesView != null) this.SeriesView.PropertyUpdated -= OnSeriesViewPropertyUpdated;
            //if (this.SeriesView != null) this.SeriesView.PropertyChanged -= OnSeriesViewPropertyChanged;
            //PropertyChangedEventHandler handler = this.SeriesView.PropertyChanged;
            //if (handler != null)
            if (this.SeriesView != null) this.SeriesView.PropertyUpdated += OnSeriesViewPropertyUpdated;
            if (this.SeriesView != null) this.SeriesView.PropertyChanged += OnSeriesViewPropertyChanged;  
            
            //this.SeriesView.PropertyChanged += OnSeriesViewPropertyChanged;
        }
      
        ///// <summary>
        ///// Gets or sets name/identifier of a Geographic series 
        ///// </summary>
        //public string SeriesName { get; protected set; }
       
        //public string SeriesIdentifier
        //{
        //    get { return _seriesIdentifier; }
        //    protected set { if (_seriesIdentifier == value) return; _seriesIdentifier = value; OnPropertyChanged("SeriesIdentifier"); }
        //}

        //public abstract Series SeriesView { get; set; }
        private Series _seriesView = new ScatterSeries();
        /// <summary>
        /// Gets or sets SeriesView property 
        /// </summary>
        public Series SeriesView
        {
            get { return _seriesView; }
            set
            {
                if (_seriesView == value) return;
                //if (_seriesView != null) _seriesView.PropertyUpdated -= OnSeriesViewPropertyUpdated;
                //if (_seriesView != null) _seriesView.PropertyChanged -= OnSeriesViewPropertyChanged;
                _seriesView = value;
                //if (_seriesView != null) _seriesView.PropertyUpdated += OnSeriesViewPropertyUpdated;
                //if (_seriesView != null) _seriesView.PropertyChanged += OnSeriesViewPropertyChanged;
                OnPropertyChanged("SeriesView");
            }
        }

        public void OnSeriesViewPropertyUpdated(object sender, PropertyUpdatedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
        public void OnSeriesViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e.PropertyName);
        }
     
        #region Series Properties

        public string LabelMemberPath { get; set; }
        public Border ToolTip 
        { 
            get
            {
                if (LabelMemberPath == string.Empty) return null; // new ToolTip { Content = "LabelMemberPath" };
                var tooltipLabel = new TextBlock { Margin = new Thickness(5) };
                tooltipLabel.SetBinding(TextBlock.TextProperty, new Binding("Item." + LabelMemberPath));
                var tooltipBorder = new Border { Opacity = 0.8 };
                tooltipBorder.Background = NamedColors.White.ToBrush();
                tooltipBorder.Child = tooltipLabel;
                return tooltipBorder;
            } 
        }

        ///// <summary>
        ///// Gets or sets Title of the Geographic series
        ///// </summary>
        //public object Title
        //{
        //    get { return this.SeriesView.Title; }
        //    set { if (this.SeriesView.Title == value) return; this.SeriesView.Title = value; OnPropertyChanged("Title"); }
        //}
      
        ///// <summary>
        ///// Gets or sets Visibility of the Geographic series
        ///// </summary>
        //public Visibility Visibility
        //{
        //    get { return this.SeriesView.Visibility; }
        //    set
        //    {
        //        if (this.SeriesView.Visibility == value) return; this.SeriesView.Visibility = value;
        //        OnPropertyChanged("Visibility"); OnPropertyChanged("IsVisible");
        //    }
        //}
       
        //abstract public Series SeriesView { get; set; }

        //private Brush _brush = NamedColors.DodgerBlue.ToBrush();
        ///// <summary>
        ///// Gets or sets Brush property 
        ///// </summary>
        //public Brush Brush
        //{
        //    get {  return _brush; }
        //    set { if (_brush == value) return; _brush = value; OnPropertyChanged("Brush"); }
        //}
        #endregion

      //  public new bool IsMapLayerEditable { get { return SeriesType != GeoSeriesType.NoneSelected; } }
        //protected static int SeriesIndtifier = 0;
      
    }
    /// <summary>
    /// Represents a collection of map base layers that contain views of Geographic Series and their data source
    /// </summary>
    public class GeoMapDataLayers : ObservableCollection<GeoMapDataLayer>
    {
    }

    public class GeoTileImageryMapLayer : GeoMapLayer
    {
        public GeoTileImageryMapLayer()
            : base(new GeographicTileSeries { TileImagery = new OpenStreetMapImagery() })
        {
            this.SeriesType = GeoSeriesType.GeographicTileImagerySeries;
            //this.SeriesView = new GeographicTileSeries { TileImagery = new OpenStreetMapImagery() };
            SyncSeriesViewPropertyChanges();    

            this.ImageryViewModel = GeoImageryViews.OpenStreetMap.DefaultImageryView;
        }

        #region Properties
        public GeoImagerySource ImagerySource { get { return ImageryViewModel.ImagerySource; } }

        private GeoImageryViewModel _imageryViewModel;
        public GeoImageryViewModel ImageryViewModel
        {
            get { return _imageryViewModel; }
            set
            {
                if (_imageryViewModel == value) return;
                if (_imageryViewModel != null) _imageryViewModel.PropertyChanged -= OnImageryViewModelChanged;
                _imageryViewModel = value;
                if (_imageryViewModel != null) _imageryViewModel.PropertyChanged += OnImageryViewModelChanged;
                UpdateTileImagery();
                OnPropertyChanged("ImageryViewModel");
            }
        }
        #endregion
        private void UpdateTileImagery()
        {
            var tileSeries = this.SeriesView as GeographicTileSeries;
            if (tileSeries != null)
            {
                var imagery = ImageryViewModel.GetGeographicMapImagery();
                tileSeries.TileImagery = imagery;
                tileSeries.Title = "Imagery Map Layer";
               
                this.DataSourceTrademark = ImageryViewModel.ImagerySourceTrademark;
               // tileSeries.Title = ImageryViewModel.ImageryDisplayName + " Layer";
            }
                
        }
        private void OnImageryViewModelChanged(object sender, PropertyChangedEventArgs ea)
        {
        }
    }

    //abstract public class GeoMarkerMapLayer : GeoMapLayer
    //{
    //    protected GeoMarkerMapLayer()
    //    {
            
    //    }
    //}
    /// <summary>
    /// Represents a map MapLayer with a view for the <see cref="GeographicSymbolSeries"/>
    /// </summary>
    public class GeoSymbolMapLayer : GeoMapLayer
    {
        public GeoSymbolMapLayer()
            : base(new GeographicSymbolSeries())
        {
            this.SeriesType = GeoSeriesType.GeographicSymbolSeries;
            SyncSeriesViewPropertyChanges();

            this.ActualSeriesView.LongitudeMemberPath = "Longitude";
            this.ActualSeriesView.LatitudeMemberPath = "Latitude";
            this.ActualSeriesView.MarkerType = MarkerType.Circle;
        }
        //public GeoSymbolMapLayer()
        //    : base()
        //{
        //}
        public GeoSymbolMapLayer(GeographicSymbolSeries series)
            : base(series)
        {
            this.SeriesType = GeoSeriesType.GeographicSymbolSeries;
            //this.SeriesLongitudeMemberPath = "Longitude";
            //this.SeriesLatitudeMemberPath = "Latitude";

            //this.SeriesView = new GeographicSymbolSeries();
            SyncSeriesViewPropertyChanges();

            this.ActualSeriesView.LongitudeMemberPath = "Longitude";
            this.ActualSeriesView.LatitudeMemberPath = "Latitude";
            this.ActualSeriesView.MarkerType = MarkerType.Circle;
            //this.MarkersLayout = CollisionAvoidanceType.None;
            //_markersView = new GeoMarkersSymbolSettings(this.SeriesView);
        }
        //public GeographicSymbolSeries ActualSeriesView { get { return base.SeriesView as GeographicSymbolSeries; } }
        public GeographicSymbolSeries ActualSeriesView { get { return SeriesView as GeographicSymbolSeries; } }
      
        //GeoMarkersSymbolSettings

        //private GeoMarkersSymbolSettings _markersView;
        ///// <summary>
        ///// Gets or sets MarkersView property 
        ///// </summary>
        //public GeoMarkersSymbolSettings MarkersView
        //{
        //    get {  return _markersView; }
        //    set { if (_markersView == value) return; _markersView = value; OnPropertyChanged("MarkersView"); }
        //}

        #region Data-Binding Properties
        
        ///// <summary>
        ///// Gets or sets LongitudeMemberPath property 
        ///// </summary>
        //public string LongitudeMemberPath
        //{
        //    get { return this.ActualSeriesView.LongitudeMemberPath; }
        //    set { if (this.ActualSeriesView.LongitudeMemberPath == value) return; this.ActualSeriesView.LongitudeMemberPath = value; OnPropertyChanged("LongitudeMemberPath"); }
        //}
        ///// <summary>
        ///// Gets or sets LatitudeMemberPath property 
        ///// </summary>
        //public string LatitudeMemberPath
        //{
        //    get { return this.ActualSeriesView.LatitudeMemberPath; }
        //    set { if (this.ActualSeriesView.LatitudeMemberPath == value) return; this.ActualSeriesView.LatitudeMemberPath = value; OnPropertyChanged("LatitudeMemberPath"); }
        //} 
        #endregion

        #region Markers Propereties
        ///// <summary>
        ///// Gets or sets CollisionAvoidanceType property 
        ///// </summary>
        //public CollisionAvoidanceType CollisionAvoidanceType
        //{
            //  get { return this.SeriesView.MarkerCollisionAvoidance; }
            //set { if (this.SeriesView.MarkerCollisionAvoidance == value) return; this.SeriesView.MarkerCollisionAvoidance = value; OnPropertyChanged("MarkerCollisionAvoidance"); }
        //}
        ///// <summary>
        ///// Gets or sets MarkersLayout property 
        ///// </summary>
        //public CollisionAvoidanceType MarkersLayout
        //{
        //    get { return this.ActualSeriesView.MarkerCollisionAvoidance; }
        //    set { if (this.ActualSeriesView.MarkerCollisionAvoidance == value) return; this.ActualSeriesView.MarkerCollisionAvoidance = value; OnPropertyChanged("MarkersLayout"); }
        //}
        ///// <summary>
        ///// Gets or sets MarkerType property 
        ///// </summary>
        //public MarkerType MarkerType
        //{
        //    get { return this.ActualSeriesView.MarkerType; }
        //    set { if (this.ActualSeriesView.MarkerType == value) return; this.ActualSeriesView.MarkerType = value; OnPropertyChanged("MarkerType"); }
        //}
        ///// <summary>
        ///// Gets or sets MarkerBrush property 
        ///// </summary>
        //public Brush MarkerBrush
        //{
        //    get { return this.ActualSeriesView.MarkerBrush; }
        //    set { if (this.ActualSeriesView.MarkerBrush == value) return; this.ActualSeriesView.MarkerBrush = value; OnPropertyChanged("MarkerBrush"); }
        //}
        ///// <summary>
        ///// Gets or sets MarkerOutline property 
        ///// </summary>
        //public Brush MarkerOutline
        //{
        //    get { return this.ActualSeriesView.MarkerOutline; }
        //    set { if (this.ActualSeriesView.MarkerOutline == value) return; this.ActualSeriesView.MarkerOutline = value; OnPropertyChanged("MarkerOutline"); }
        //}
        ///// <summary>
        ///// Gets or sets MaximumMarkers property 
        ///// </summary>
        //public int MarkersMaximum
        //{
        //    get { return this.ActualSeriesView.MaximumMarkers; }
        //    set { if (this.ActualSeriesView.MaximumMarkers == value) return; this.ActualSeriesView.MaximumMarkers = value; OnPropertyChanged("MaximumMarkers"); }
        //}
        #endregion
        
        //private GeographicSymbolSeries _seriesView;
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public new GeographicSymbolSeries SeriesView
        //{
        //    get { return _seriesView; }
        //    set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        //}
    }
    /// <summary>
    /// Represents a map MapLayer with a view for the <see cref="GeographicProportionalSymbolSeries"/>
    /// </summary>
    public class GeoSymbolProportionalMapLayer : GeoMapLayer //: GeoSymbolMapLayer
    {
        public GeoSymbolProportionalMapLayer()
            : base(new GeographicProportionalSymbolSeries())
        {
            this.SeriesType = GeoSeriesType.GeographicProportionalSymbolSeries;
            //this.SeriesFillMemberPath = "Magnitude";
            //this.SeriesRadiusMemberPath = "Magnitude";

            //this.SeriesView = new GeographicProportionalSymbolSeries();
            SyncSeriesViewPropertyChanges();
            this.ActualSeriesView.MarkerType = MarkerType.Circle;
            this.ActualSeriesView.LongitudeMemberPath = "Longitude";
            this.ActualSeriesView.LatitudeMemberPath = "Latitude";
            this.ActualSeriesView.FillMemberPath = "Magnitude";
            this.ActualSeriesView.RadiusMemberPath = "Magnitude";

            if (this.ActualSeriesView.MarkerOutline == null)
                this.ActualSeriesView.MarkerOutline = NamedColors.White.Brush;

            //this.FillScaleFilterOutBrush = new SolidColorBrush(Colors.Transparent);
            this.FillScaleFilterOutBrush = NamedColors.DimGray.BrushOpacity(0.5);

            if (this.RadiusScale == null)
            {
                this.RadiusScale = new SizeScale();
                this.RadiusScale.MaximumValue = 50;
                this.RadiusScale.MinimumValue = 5;
            }
            //if (this.FillScale == null)
            //{
            //    this.FillScale = new ValueBrushScale();
            //    this.FillScale.Brushes = NamedColors.Collections.BlueColors.Brushes.ToBrushCollection();
            //    //this.SeriesView.FillScale.MaximumValue = 50;
            //    //this.SeriesView.FillScale.MinimumValue = 5;
            //}

            this.SeriesView.PropertyChanged += OnSeriesViewPropertyChanged;

            this.PropertyChanged += GeoMapLayer_PropertyChanged;
        }
        public GeographicProportionalSymbolSeries ActualSeriesView { get { return SeriesView as GeographicProportionalSymbolSeries; } }

        private void GeoMapLayer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs ea)
        {
            this.LogPropertyChange(ea);
        }

        private new void OnSeriesViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs ea)
        {
            //this.LogPropertyChange(sender, ea);
            var seriesName = sender.GetType().ToString().Split('.').ToList().LastItem();
            this.LogPropertyChange("SeriesView(" + seriesName + ")." + ea.PropertyName);
           
            //SeriesView.Title = "Cities' Population";
            //SeriesView.MarkerType = MarkerType.Circle;
            //SeriesView.LongitudeMemberPath = "Longitude";
            //SeriesView.LatitudeMemberPath = "Latitude";
            //SeriesView.RadiusMemberPath = "Population";
            //SeriesView.FillMemberPath = "Population";
            //SeriesView.Opacity = 0.75;
            ////MapLayer2.SeriesView.RadiusScale = new SizeScale();
            //var fillScale = new ValueBrushScale();
            ////fillScale.Brushes.Add(new SolidColorBrush(Colors.White));
            //fillScale.Brushes.AddRange(NamedColors.Collections.BlueColors.Brushes);


            ////fillScale.Brushes.Add(new SolidColorBrush().FromHex("#FF1AA1E2"));
            ////fillScale.Brushes.Add(new SolidColorBrush().FromHex("#FFFFD034"));
            ////fillScale.Brushes.Add(new SolidColorBrush().FromHex("#FF7D120D"));
            ////fillScale.Brushes.Add(new SolidColorBrush(Colors.LightGray));
            ////fillScale.Brushes.Add(new SolidColorBrush(Colors.DarkGray));
            ////fillScale.Brushes.Add(new SolidColorBrush(Colors.Black));
            //fillScale.MinimumValue = WorldCities.DataSource.Population.Min;
            //fillScale.MaximumValue = WorldCities.DataSource.Population.Max;
            //SeriesView.FillScale = fillScale;
            //SeriesView.RadiusScale.MaximumValue = 60;
            //SeriesView.RadiusScale.MinimumValue = 10;

        }


        //private GeographicProportionalSymbolSeries _seriesView;
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public new GeographicProportionalSymbolSeries SeriesView
        //{
        //    get { return _seriesView; }
        //    set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        //}
        #region Properties
        ///// <summary>
        ///// Gets or sets LabelMemberPath property 
        ///// </summary>
        //public string LabelMemberPath
        //{
        //    get { return this.ActualSeriesView.LabelMemberPath; }
        //    set { if (this.ActualSeriesView.LabelMemberPath == value) return; this.ActualSeriesView.LabelMemberPath = value; OnPropertyChanged("LabelMemberPath"); }
        //}
        /// <summary>
        /// Gets or sets RadiusScale property 
        /// </summary>
        public SizeScale RadiusScale
        {
            get { return this.ActualSeriesView.RadiusScale; }
            set { if (this.ActualSeriesView.RadiusScale == value) return; this.ActualSeriesView.RadiusScale = value; OnPropertyChanged("RadiusScale"); }
        }
        ///// <summary>
        ///// Gets or sets FillScale property 
        ///// </summary>
        //public BrushScale FillScale
        //{
        //    get { return this.ActualSeriesView.FillScale; }
        //    set { if (this.ActualSeriesView.FillScale == value) return; this.ActualSeriesView.FillScale = value; OnPropertyChanged("FillScale"); }
        //}
        ValueBrushScaleView _valueFillScale = new ValueBrushScaleView(); 
        /// <summary>
        /// Gets or sets FillScale property 
        /// </summary>
        public ValueBrushScaleView FillValueScale
        {
            get { return _valueFillScale; }
            set { if (_valueFillScale == value) return; _valueFillScale = value; OnPropertyChanged("FillValueScale"); }
        }
        
        private double _fillScaleAbsoluteMin = 0;
        /// <summary>
        /// Gets or sets FillScaleAbsoluteMin property 
        /// </summary>
        public double FillScaleAbsoluteMin
        {
            get {  return _fillScaleAbsoluteMin; }
            set { if (_fillScaleAbsoluteMin == value) return; _fillScaleAbsoluteMin = value; OnPropertyChanged("FillScaleAbsoluteMin"); }
        }
        private double _fillScaleAbsoluteMax = 100;
        /// <summary>
        /// Gets or sets FillScaleAbsoluteMin property 
        /// </summary>
        public double FillScaleAbsoluteMax
        {
            get { return _fillScaleAbsoluteMax; }
            set { if (_fillScaleAbsoluteMax == value) return; _fillScaleAbsoluteMax = value; OnPropertyChanged("FillScaleAbsoluteMax"); }
        }

        //private string _fillScaleValueFormat = "{}{0:#,0.###} M";
        private string _fillScaleValueFormat = "#,#0";
       // private string _fillScaleValueFormat = "#,##0.##,,M";
        /// <summary>
        /// Gets or sets FillScaleValueFormat property 
        /// </summary>
        public string FillScaleValueFormat
        {
            get { return _fillScaleValueFormat; }
            set { if (_fillScaleValueFormat == value) return; _fillScaleValueFormat = value; OnPropertyChanged("FillScaleValueFormat"); }
        }
        private Brush _fillScaleFilterBrush = new SolidColorBrush(Colors.Transparent);
        /// <summary>
        /// Gets or sets FillScaleFilterOutBrush property 
        /// </summary>
        public Brush FillScaleFilterOutBrush
        {
            get { return this.ActualSeriesView.MarkerBrush; }
            set { if (this.ActualSeriesView.MarkerBrush == value) return; this.ActualSeriesView.MarkerBrush = value; OnPropertyChanged("FillScaleFilterOutBrush"); }
        }
        public Brush FillScaleOutlineBrush
        {
            get { return this.ActualSeriesView.MarkerOutline; }
            set { if (this.ActualSeriesView.MarkerBrush == value) return; this.ActualSeriesView.MarkerOutline = value; OnPropertyChanged("FillScaleOutlineBrush"); }
        }
        /// <summary>
        /// Gets or sets FillMemberPath property 
        /// </summary>
        public string FillMemberPath
        {
            get { return this.ActualSeriesView.FillMemberPath; }
            set { if (this.ActualSeriesView.FillMemberPath == value) return; this.ActualSeriesView.FillMemberPath = value; OnPropertyChanged("FillMemberPath"); }
        }

        //private string _radiusMemberPath;
        /// <summary>
        /// Gets or sets RadiusMemberPath property 
        /// </summary>
        public string RadiusMemberPath
        {
            get { return this.ActualSeriesView.RadiusMemberPath; }
            set { if (this.ActualSeriesView.RadiusMemberPath == value) return; this.ActualSeriesView.RadiusMemberPath = value; OnPropertyChanged("RadiusMemberPath"); }
        }

        #endregion
         
    }
    /// <summary>
    /// Represents a base map MapLayer with a view for all Geographic Shape Series
    /// </summary>
    abstract public class GeoShapeSeriesBaseMapLayer : GeoMapLayer
    {
        protected GeoShapeSeriesBaseMapLayer() : base()
        {
            
        }
        protected GeoShapeSeriesBaseMapLayer(GeographicShapeSeriesBase series)
            : base(series)
        {
            //SeriesView = series;
        }

        //private GeographicShapeSeriesBase _seriesView;
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public new GeographicShapeSeriesBase SeriesView
        //{
        //    get { return _seriesView; }
        //    set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        //}
        ///// <summary>
        ///// Gets or sets VisibleFromScale of the Geographic series
        ///// </summary>
        //public double VisibleFromScale
        //{
        //    get { return this.SeriesView.VisibleFromScale; }
        //    set { if (this.SeriesView.VisibleFromScale == value) return; this.SeriesView.VisibleFromScale = value; OnPropertyChanged("VisibleFromScale"); }
        //}
      
    }
    /// <summary>
    /// Represents a map MapLayer with a view for the <see cref="GeographicShapeSeries"/>
    /// </summary>
    public class GeoShapeMapLayer : GeoShapeSeriesBaseMapLayer
    {
        public GeoShapeMapLayer()
            : base(new GeographicShapeSeries())
        {
            this.SeriesType = GeoSeriesType.GeographicShapeSeries;
           // this.SeriesView = new GeographicShapeSeries();
            //this.SeriesView = base.SeriesView;
            this.SeriesView.PropertyUpdated += OnSeriesViewPropertyUpdated;
            this.SeriesView.PropertyChanged += OnSeriesViewPropertyChanged;
            //this.SeriesName = this.SeriesType + this.SeriesIndex.ToString();

            this.ShapeStyleSettings = new ShapeStyleSettings();
            this.ShapeStyleSettings.PropertyChanged += OnShapeStyleSettingsChanged;
            this.ActualSeriesView.ShapeMemberPath = "ShapePoints";
            //this.MarkerType = MarkerType.None;
            //this.MarkersLayout = CollisionAvoidanceType.None;
            //TODO
            //this.SeriesView.ShapeFilterResolution
            //this.SeriesView.ShapeStyle
            //    this.SeriesView.ShapeStyleSelector
             
        }

        void OnShapeStyleSettingsChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //this.SeriesView.ItemsSource = null;
            //this.SeriesView.ShapeStyleSelector = null;
            //this.SeriesView.Brush = null;
            //if (this.ShapeStyleSettings.ShapeStyleSelectorType == StyleSelectorType.SingleShapeStyle)
            //{
                
            //}
            //this.SeriesView.ShapeStyle = null;
            //this.SeriesView.ShapeStyle = this.ShapeStyleSettings.GetShapeStyle();
            this.ActualSeriesView.ShapeStyle = this.ShapeStyleSettings.GetShapeStyle();

            //this.SeriesView.ShapeStyleSelector = null;
            //this.SeriesView.ShapeFilterResolution = 40;
            //this.SeriesView.ItemsSource = this.DataSourceView.View;
            //this.SeriesView.ShapeStyleSelector = this.ShapeStyleSettings.GetShapeStyleSelector();

            //this.SeriesView.ItemsSource = this.DataSourceView.View;
           
        }

       // public Visibility ActualVisibility { get { return this.SeriesView.Visibility;  } }
        public GeographicShapeSeries ActualSeriesView { get { return base.SeriesView as GeographicShapeSeries; } }

        //private GeographicShapeSeries _seriesView = new GeographicShapeSeries();
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public new GeographicShapeSeries SeriesView
        //{
        //    //get { return base.SeriesView as GeographicShapeSeries; }
        //    get { return _seriesView; }
        //    set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        //}
        #region Shape Properties

        private ShapeStyleSettings _shapeStyle;
        /// <summary>
        /// Gets or sets ShapeStyle property 
        /// </summary>
        public ShapeStyleSettings ShapeStyleSettings
        {
            get {  return _shapeStyle; }
            set { if (_shapeStyle == value) return; _shapeStyle = value; OnPropertyChanged("ShapeStyleSettings"); }
        }
        ///// <summary>
        ///// Gets or sets ShapeMemberPath property 
        ///// </summary>
        //public string ShapeMemberPath
        //{
        //    get { return this.SeriesView.ShapeMemberPath; }
        //    set { if (this.SeriesView.ShapeMemberPath == value) return; this.SeriesView.ShapeMemberPath = value; OnPropertyChanged("ShapeMemberPath"); }
        //}
        ///// <summary>
        ///// Gets or sets ShapeMemberPath property 
        ///// </summary>
        //public double ShapeFilterResolution
        //{
        //    get { return this.SeriesView.ShapeFilterResolution; }
        //    set { if (this.SeriesView.ShapeFilterResolution == value) return; this.SeriesView.ShapeFilterResolution = value; OnPropertyChanged("ShapeFilterResolution"); }
        //}
        //TODO: add
        ///// <summary>
        ///// Gets or sets ShapeStyleSelector property 
        ///// </summary>
        //public StyleSelector ShapeStyleSelector
        //{
        //    get { return this.SeriesView.ShapeStyleSelector; }
        //    set { if (this.SeriesView.ShapeStyleSelector == value) return; this.SeriesView.ShapeStyleSelector = value; OnPropertyChanged("ShapeStyleSelector"); }
        //}
        ///// <summary>
        ///// Gets or sets ShapeStyle property 
        ///// </summary>
        //public Style ShapeStyle
        //{
        //    get { return this.SeriesView.ShapeStyle; }
        //    set { if (this.SeriesView.ShapeStyle == value) return; this.SeriesView.ShapeStyle = value; OnPropertyChanged("ShapeStyle"); }
        //}

        #endregion

        #region Markers Propereties
        ///// <summary>
        ///// Gets or sets CollisionAvoidanceType property 
        ///// </summary>
        //public CollisionAvoidanceType CollisionAvoidanceType
        //{
        //  get { return this.SeriesView.MarkerCollisionAvoidance; }
        //set { if (this.SeriesView.MarkerCollisionAvoidance == value) return; this.SeriesView.MarkerCollisionAvoidance = value; OnPropertyChanged("MarkerCollisionAvoidance"); }
        //}


        ///// <summary>
        ///// Gets or sets MarkersLayout property 
        ///// </summary>
        //public CollisionAvoidanceType MarkersLayout
        //{
        //    get { return this.SeriesView.MarkerCollisionAvoidance; }
        //    set { if (this.SeriesView.MarkerCollisionAvoidance == value) return; this.SeriesView.MarkerCollisionAvoidance = value; OnPropertyChanged("MarkersLayout"); }
        //}
        ///// <summary>
        ///// Gets or sets MarkerType property 
        ///// </summary>
        //public MarkerType MarkerType
        //{
        //    get { return this.SeriesView.MarkerType; }
        //    set { if (this.SeriesView.MarkerType == value) return; this.SeriesView.MarkerType = value; OnPropertyChanged("MarkerType"); }
        //}
        ///// <summary>
        ///// Gets or sets MarkerBrush property 
        ///// </summary>
        //public Brush MarkerBrush
        //{
        //    get { return this.SeriesView.MarkerBrush; }
        //    set { if (this.SeriesView.MarkerBrush == value) return; this.SeriesView.MarkerBrush = value; OnPropertyChanged("MarkerBrush"); }
        //}
        ///// <summary>
        ///// Gets or sets MarkerOutline property 
        ///// </summary>
        //public Brush MarkerOutline
        //{
        //    get { return this.SeriesView.MarkerOutline; }
        //    set { if (this.SeriesView.MarkerOutline == value) return; this.SeriesView.MarkerOutline = value; OnPropertyChanged("MarkerOutline"); }
        //}

        ///// <summary>
        ///// Gets or sets MaximumMarkers property 
        ///// </summary>
        //public int MarkersMaximum
        //{
        //    get { return this.SeriesView.MaximumMarkers; }
        //    set { if (this.SeriesView.MaximumMarkers == value) return; this.SeriesView.MaximumMarkers = value; OnPropertyChanged("MaximumMarkers"); }
        //}
        #endregion

    }
    /// <summary>
    /// Represents a map MapLayer with a view for the <see cref="GeographicShapeControlSeries"/>
    /// </summary>
    public class GeoShapeControlMapLayer : GeoShapeSeriesBaseMapLayer
    {
        public GeoShapeControlMapLayer()
        {
            this.SeriesType = GeoSeriesType.GeographicShapeControlSeries;
            this.SeriesView = new GeographicShapeControlSeries();
            SyncSeriesViewPropertyChanges();
            this.SeriesView.PropertyUpdated += OnSeriesViewPropertyUpdated;
            this.SeriesView.PropertyChanged += OnSeriesViewPropertyChanged;
        
            this.ShapeMemberPath = "ShapePoints";
            //this.MarkerType = MarkerType.None;
            //this.MarkersLayout = CollisionAvoidanceType.None;
            //TODO
            //this.SeriesView.ShapeFilterResolution
            //this.SeriesView.ShapeStyle
            //    this.SeriesView.ShapeStyleSelector
        }
        private GeographicShapeControlSeries _seriesView = new GeographicShapeControlSeries();
        /// <summary>
        /// Gets or sets SeriesView property 
        /// </summary>
        public new GeographicShapeControlSeries SeriesView
        {
            get { return _seriesView; }
            set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        }
        #region Shape Properties
        /// <summary>
        /// Gets or sets ShapeMemberPath property 
        /// </summary>
        public string ShapeMemberPath
        {
            get { return this.SeriesView.ShapeMemberPath; }
            set { if (this.SeriesView.ShapeMemberPath == value) return; this.SeriesView.ShapeMemberPath = value; OnPropertyChanged("ShapeMemberPath"); }
        }
        /// <summary>
        /// Gets or sets ShapeMemberPath property 
        /// </summary>
        public double ShapeFilterResolution
        {
            get { return this.SeriesView.ShapeFilterResolution; }
            set { if (this.SeriesView.ShapeFilterResolution == value) return; this.SeriesView.ShapeFilterResolution = value; OnPropertyChanged("ShapeFilterResolution"); }
        }
        //TODO: add
        ///// <summary>
        ///// Gets or sets ShapeStyleSelector property 
        ///// </summary>
        //public StyleSelector ShapeStyleSelector
        //{
        //    get { return this.SeriesView.ShapeStyleSelector; }
        //    set { if (this.SeriesView.ShapeStyleSelector == value) return; this.SeriesView.ShapeStyleSelector = value; OnPropertyChanged("ShapeStyleSelector"); }
        //}
        ///// <summary>
        ///// Gets or sets ShapeStyle property 
        ///// </summary>
        //public Style ShapeStyle
        //{
        //    get { return this.SeriesView.ShapeStyle; }
        //    set { if (this.SeriesView.ShapeStyle == value) return; this.SeriesView.ShapeStyle = value; OnPropertyChanged("ShapeStyle"); }
        //}

        #endregion

    }
    /// <summary>
    /// Represents a map MapLayer with a view for the <see cref="GeographicPolylineSeries"/>
    /// </summary>
    public class GeoPolylineMapLayer : GeoShapeSeriesBaseMapLayer
    {
        public GeoPolylineMapLayer()
            : base(new GeographicPolylineSeries())
        {
            this.SeriesType = GeoSeriesType.GeographicPolylineSeries;
            //this.SeriesView = new GeographicPolylineSeries();
            SyncSeriesViewPropertyChanges();    
  
            this.ShapeMemberPath = "ShapePoints";
            //this.MarkerType = MarkerType.None;
            //this.MarkersLayout = CollisionAvoidanceType.None;
            //TODO
            //this.SeriesView.ShapeFilterResolution
            //this.SeriesView.ShapeStyle
            //    this.SeriesView.ShapeStyleSelector
        }
        private GeographicPolylineSeries _seriesView = new GeographicPolylineSeries();
        /// <summary>
        /// Gets or sets SeriesView property 
        /// </summary>
        public new GeographicPolylineSeries SeriesView
        {
            get { return _seriesView; }
            set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        }
        #region Shape Properties
        /// <summary>
        /// Gets or sets ShapeMemberPath property 
        /// </summary>
        public string ShapeMemberPath
        {
            get { return this.SeriesView.ShapeMemberPath; }
            set { if (this.SeriesView.ShapeMemberPath == value) return; this.SeriesView.ShapeMemberPath = value; OnPropertyChanged("ShapeMemberPath"); }
        }
        /// <summary>
        /// Gets or sets ShapeMemberPath property 
        /// </summary>
        public double ShapeFilterResolution
        {
            get { return this.SeriesView.ShapeFilterResolution; }
            set { if (this.SeriesView.ShapeFilterResolution == value) return; this.SeriesView.ShapeFilterResolution = value; OnPropertyChanged("ShapeFilterResolution"); }
        }
        //TODO: add
        ///// <summary>
        ///// Gets or sets ShapeStyleSelector property 
        ///// </summary>
        //public StyleSelector ShapeStyleSelector
        //{
        //    get { return this.SeriesView.ShapeStyleSelector; }
        //    set { if (this.SeriesView.ShapeStyleSelector == value) return; this.SeriesView.ShapeStyleSelector = value; OnPropertyChanged("ShapeStyleSelector"); }
        //}
        ///// <summary>
        ///// Gets or sets ShapeStyle property 
        ///// </summary>
        //public Style ShapeStyle
        //{
        //    get { return this.SeriesView.ShapeStyle; }
        //    set { if (this.SeriesView.ShapeStyle == value) return; this.SeriesView.ShapeStyle = value; OnPropertyChanged("ShapeStyle"); }
        //}

        #endregion

    }

    /// <summary>
    /// Represents a base map MapLayer with a view for all Geographic Series with triangulation source
    /// </summary>
    abstract public class GeoTriangulationMapLayer : GeoMapLayer
    {
        protected GeoTriangulationMapLayer(GeographicXYTriangulatingSeries series)
            : base(series)
        {
            
        }
        //private GeographicXYTriangulatingSeries _seriesView;
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public new GeographicXYTriangulatingSeries SeriesView
        //{
        //    get { return _seriesView; }
        //    set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        //}
        #region Properties
        //private string _seriesTrianglesSourceKey;
        ///// <summary>
        ///// Gets or sets dataSourceKey property 
        ///// </summary>
        //public string SeriesTrianglesSourceKey
        //{
        //    get { return _seriesTrianglesSourceKey; }
        //    set { if (_seriesTrianglesSourceKey == value) return; _seriesTrianglesSourceKey = value; OnPropertyChanged("SeriesTrianglesSourceKey"); }
        //}
        ///// <summary>
        ///// Gets or sets TriangleVertexMemberPath1 property 
        ///// </summary>
        //public string TriangleVertexMemberPath1
        //{
        //    get { return this.SeriesView.TriangleVertexMemberPath1; }
        //    set { if (this.SeriesView.TriangleVertexMemberPath1 == value) return; this.SeriesView.TriangleVertexMemberPath1 = value; OnPropertyChanged("TriangleVertexMemberPath1"); }
        //}
        ///// <summary>
        ///// Gets or sets TriangleVertexMemberPath2 property 
        ///// </summary>
        //public string TriangleVertexMemberPath2
        //{
        //    get { return this.SeriesView.TriangleVertexMemberPath2; }
        //    set { if (this.SeriesView.TriangleVertexMemberPath2 == value) return; this.SeriesView.TriangleVertexMemberPath2 = value; OnPropertyChanged("TriangleVertexMemberPath2"); }
        //}
        ///// <summary>
        ///// Gets or sets TriangleVertexMemberPath3 property 
        ///// </summary>
        //public string TriangleVertexMemberPath3
        //{
        //    get { return this.SeriesView.TriangleVertexMemberPath3; }
        //    set { if (this.SeriesView.TriangleVertexMemberPath3 == value) return; this.SeriesView.TriangleVertexMemberPath3 = value; OnPropertyChanged("TriangleVertexMemberPath3"); }
        //}
        
        ///// <summary>
        ///// Gets or sets LongitudeMemberPath property 
        ///// </summary>
        //public string LongitudeMemberPath
        //{
        //    get { return this.SeriesView.LongitudeMemberPath; }
        //    set { if (this.SeriesView.LongitudeMemberPath == value) return; this.SeriesView.LongitudeMemberPath = value; OnPropertyChanged("LongitudeMemberPath"); }
        //}
        ///// <summary>
        ///// Gets or sets LatitudeMemberPath property 
        ///// </summary>
        //public string LatitudeMemberPath
        //{
        //    get { return this.SeriesView.LatitudeMemberPath; }
        //    set { if (this.SeriesView.LatitudeMemberPath == value) return; this.SeriesView.LatitudeMemberPath = value; OnPropertyChanged("LatitudeMemberPath"); }
        //}  
        #endregion
    }
    /// <summary>
    /// Represents a map MapLayer with a view for the  <see cref="GeographicScatterAreaSeries"/>
    /// </summary>
    public class GeoScatterAreaMapLayer : GeoTriangulationMapLayer
    {
        public GeoScatterAreaMapLayer()
            : base(new GeographicScatterAreaSeries())
        {
            this.SeriesType = GeoSeriesType.GeographicScatterAreaSeries;
            //this.SeriesView = new GeographicScatterAreaSeries();
            SyncSeriesViewPropertyChanges();    

            //if (this.ColorScale == null)
            //    this.ColorScale = new CustomPaletteColorScaleView();

            this.ActualSeriesView.LatitudeMemberPath = "Point.Y";
            this.ActualSeriesView.LongitudeMemberPath = "Point.X";

            this.ActualSeriesView.TriangleVertexMemberPath1 = "V1";
            this.ActualSeriesView.TriangleVertexMemberPath2 = "V2";
            this.ActualSeriesView.TriangleVertexMemberPath3 = "V3";

            this.ActualSeriesView.ColorScale = this.ColorScale;
            //TODO: add mapping to triangles
            //this.TriangleVertexMemberPath1 = "Point[0]";
            //this.TriangleVertexMemberPath2 = "Point[1]";
            //this.TriangleVertexMemberPath3 = "Point[2]";
        }
        public GeographicScatterAreaSeries ActualSeriesView { get { return SeriesView as GeographicScatterAreaSeries; } }
       
        private CustomPaletteColorScaleView _colorScale = new CustomPaletteColorScaleView();
        /// <summary>
        /// Gets or sets ColorScale property 
        /// </summary>
        public CustomPaletteColorScaleView ColorScale
        {
            get {  return _colorScale; }
            set { if (_colorScale == value) return; _colorScale = value; OnPropertyChanged("ColorScale"); }
        }

        //private GeographicScatterAreaSeries _seriesView = new GeographicScatterAreaSeries();
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public new GeographicScatterAreaSeries SeriesView
        //{
        //    get { return _seriesView; }
        //    set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        //}

        ///// <summary>
        ///// Gets or sets ColorMemberPath property 
        ///// </summary>
        //public string ColorMemberPath
        //{
        //    get { return this.SeriesView.ColorMemberPath; }
        //    set { if (this.SeriesView.ColorMemberPath == value) return; this.SeriesView.ColorMemberPath = value; OnPropertyChanged("ColorMemberPath"); }
        //}
        ///// <summary>
        ///// Gets or sets ColorScale property 
        ///// </summary>
        //public ColorScale ColorScale
        //{
        //    get { return this.ActualSeriesView.ColorScale; }
        //    set { if (this.ActualSeriesView.ColorScale == value) return; this.ActualSeriesView.ColorScale = value; OnPropertyChanged("ColorScale"); }
        //}

    }
    /// <summary>
    /// Represents a map MapLayer with a view for the <see cref="GeographicContourLineSeries"/>
    /// </summary>
    public class GeoContourLineMapLayer : GeoTriangulationMapLayer
    {
        public GeoContourLineMapLayer()
            : base(new GeographicContourLineSeries())
        {
            this.SeriesType = GeoSeriesType.GeographicContourLineSeries;
           // this.SeriesView = new GeographicContourLineSeries();
            SyncSeriesViewPropertyChanges();    

            if (this.FillScale == null) 
                this.FillScale = new ValueBrushScale();

            this.ValueMemberPath = "Value";

            //TODO: add 
            //this.ValueResolver = new ContourValueResolver();
            
        }
        private GeographicContourLineSeries _seriesView = new GeographicContourLineSeries();
        /// <summary>
        /// Gets or sets SeriesView property 
        /// </summary>
        public new GeographicContourLineSeries SeriesView
        {
            get { return _seriesView; }
            set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        }

        /// <summary>
        /// Gets or sets FillScale property 
        /// </summary>
        public ValueBrushScale FillScale
        {
            get { return this.SeriesView.FillScale; }
            set { if (this.SeriesView.FillScale == value) return; this.SeriesView.FillScale = value; OnPropertyChanged("FillScale"); }
        }
        /// <summary>
        /// Gets or sets ValueMemberPath property 
        /// </summary>
        public string ValueMemberPath
        {
            get { return this.SeriesView.ValueMemberPath; }
            set { if (this.SeriesView.ValueMemberPath == value) return; this.SeriesView.ValueMemberPath = value; OnPropertyChanged("ValueMemberPath"); }
        }
        /// <summary>
        /// Gets or sets ValueResolver property 
        /// </summary>
        public ContourValueResolver ValueResolver
        {
            get { return this.SeriesView.ValueResolver; }
            set { if (this.SeriesView.ValueResolver == value) return; this.SeriesView.ValueResolver = value; OnPropertyChanged("ValueResolver"); }
        }
    }
    
    /// <summary>
    /// Represents a map MapLayer with a view for the  <see cref="GeographicHighDensityScatterSeries"/>
    /// </summary>
    public class GeoHighDensityScatterMapLayer : GeoMapLayer
    {
        public GeoHighDensityScatterMapLayer()
            : base(new GeographicHighDensityScatterSeries())
        {
            this.SeriesType = GeoSeriesType.GeographicHighDensityScatterSeries;
            //this.SeriesView = new GeographicHighDensityScatterSeries();
            SyncSeriesViewPropertyChanges();    
        }
        //private GeographicHighDensityScatterSeries _seriesView = new GeographicHighDensityScatterSeries();
        ///// <summary>
        ///// Gets or sets SeriesView property 
        ///// </summary>
        //public new GeographicHighDensityScatterSeries SeriesView
        //{
        //    get { return _seriesView; }
        //    set { if (_seriesView == value) return; _seriesView = value; OnPropertyChanged("SeriesView"); }
        //}
        public GeographicHighDensityScatterSeries ActualSeriesView { get { return SeriesView as GeographicHighDensityScatterSeries; } }

        #region Properties
        ///// <summary>
        ///// Gets or sets MouseOverEnabled property 
        ///// </summary>
        //public bool MouseOverEnabled
        //{
        //    get { return this.ActualSeriesView.MouseOverEnabled; }
        //    set { if (this.ActualSeriesView.MouseOverEnabled == value) return; this.ActualSeriesView.MouseOverEnabled = value; OnPropertyChanged("MouseOverEnabled"); }
        //}
        ///// <summary>
        ///// Gets or sets UseBruteForce property 
        ///// </summary>
        //public bool UseBruteForce
        //{
        //    get { return this.ActualSeriesView.UseBruteForce; }
        //    set { if (this.ActualSeriesView.UseBruteForce == value) return; this.ActualSeriesView.UseBruteForce = value; OnPropertyChanged("UseBruteForce"); }
        //}
        ///// <summary>
        ///// Gets or sets UseSquareCutoffStyle property 
        ///// </summary>
        //public bool UseSquareCutoffStyle
        //{
        //    get { return this.SeriesView.UseSquareCutoffStyle; }
        //   set { if (this.SeriesView.UseSquareCutoffStyle == value) return; this.SeriesView.UseSquareCutoffStyle = value; OnPropertyChanged("UseSquareCutoffStyle"); }
        //} 
        ///// <summary>
        ///// Gets or sets PointExtent property 
        ///// </summary>
        //public int PointExtent
        //{
        //    get { return this.ActualSeriesView.PointExtent; }
        //    set { if (this.ActualSeriesView.PointExtent == value) return; this.ActualSeriesView.PointExtent = value; OnPropertyChanged("PointExtent"); }
        //}
        //private ValueScale _heatScale = new ValueScale();
        ///// <summary>
        ///// Gets or sets HeatScale property 
        ///// </summary>
        //public ValueScale HeatScale
        //{
        //    get {  return _heatScale; }
        //    set { if (_heatScale == value) return; _heatScale = value; OnPropertyChanged("HeatScale"); }
        //}
        private ValueBrushScaleView _heatScale = new ValueBrushScaleView();
        /// <summary>
        /// Gets or sets HeatScale property 
        /// </summary>
        public ValueBrushScaleView HeatScale
        {
            get { return _heatScale; }
            set { if (_heatScale == value) return; _heatScale = value; OnPropertyChanged("HeatScale"); }
        }
        ///// <summary>
        ///// Gets or sets HeatMaximum property 
        ///// </summary>
        //public double HeatMaximum
        //{
        //    get { return this.ActualSeriesView.HeatMaximum; }
        //    set { if (this.ActualSeriesView.HeatMaximum == value) return; this.ActualSeriesView.HeatMaximum = value; OnPropertyChanged("HeatMaximum"); }
        //}
        ///// <summary>
        ///// Gets or sets HeatMinimum property 
        ///// </summary>
        //public double HeatMinimum
        //{
        //    get { return this.ActualSeriesView.HeatMinimum; }
        //    set { if (this.ActualSeriesView.HeatMinimum == value) return; this.ActualSeriesView.HeatMinimum = value; OnPropertyChanged("HeatMinimum"); }
        //}
        ///// <summary>
        ///// Gets or sets HeatMaximumColor property 
        ///// </summary>
        //public Color HeatMaximumColor
        //{
        //    get { return this.ActualSeriesView.HeatMaximumColor; }
        //    set { if (this.ActualSeriesView.HeatMaximumColor == value) return; this.ActualSeriesView.HeatMaximumColor = value; OnPropertyChanged("HeatMaximumColor"); }
        //}
        ///// <summary>
        ///// Gets or sets HeatMinimumColor property 
        ///// </summary>
        //public Color HeatMinimumColor
        //{
        //    get { return this.ActualSeriesView.HeatMinimumColor; }
        //    set { if (this.ActualSeriesView.HeatMinimumColor == value) return; this.ActualSeriesView.HeatMinimumColor = value; OnPropertyChanged("HeatMinimumColor"); }
        //}
        ///// <summary>
        ///// Gets or sets LongitudeMemberPath property 
        ///// </summary>
        //public string LongitudeMemberPath
        //{
        //    get { return this.ActualSeriesView.LongitudeMemberPath; }
        //    set { if (this.ActualSeriesView.LongitudeMemberPath == value) return; this.ActualSeriesView.LongitudeMemberPath = value; OnPropertyChanged("LongitudeMemberPath"); }
        //}
        ///// <summary>
        ///// Gets or sets LatitudeMemberPath property 
        ///// </summary>
        //public string LatitudeMemberPath
        //{
        //    get { return this.ActualSeriesView.LatitudeMemberPath; }
        //    set { if (this.ActualSeriesView.LatitudeMemberPath == value) return; this.ActualSeriesView.LatitudeMemberPath = value; OnPropertyChanged("LatitudeMemberPath"); }
        //}  

        
        #endregion
    }
    

    /// <summary>
    /// Represents a type of Geographic Series 
    /// </summary>
    public enum GeoSeriesType
    {
        NoneSelected,
        GeographicMotionSymbolSeries,
       // GeographicMultiSeries,

        GeographicTileImagerySeries,

        GeographicContourLineSeries,
        GeographicScatterAreaSeries,

        GeographicHighDensityScatterSeries,
    
        GeographicPolylineSeries,
        GeographicShapeControlSeries,
        GeographicShapeSeries,

        GeographicSymbolSeries,
        GeographicProportionalSymbolSeries, 
    }


   
}