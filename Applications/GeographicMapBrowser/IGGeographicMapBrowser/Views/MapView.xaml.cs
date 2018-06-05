using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using IGExtensions.Common.Assets.Resources;
using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Framework.Controls;
using IGShowcase.GeographicMapBrowser.Models;
using IGShowcase.GeographicMapBrowser.ViewModels;
using IGShowcase.GeographicMapBrowser.Assets.Resources;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Interactions;
using Infragistics.Controls.Maps;
using WindowState = Infragistics.Controls.Interactions.WindowState;

namespace IGShowcase.GeographicMapBrowser
{
    public partial class MapView : UserControl
    {
        public MapView()
        {
            var rd = App.Current.Resources;

            InitializeComponent();

            this.ViewModel = new MapViewModel(); 

            this.DataContext = ViewModel;

            this.Loaded += OnMapViewLoaded;
        }

        void OnMapViewLoaded(object sender, RoutedEventArgs e)
        {
            if (NavigationApp.Current.IsInDesingMode()) return;
            
            // load settings from map configuration file 
            GeoMapImagryProvider.MapConfigFile = "IGMap.config";
            GeoMapImagryProvider.LoadMapConfigurationCompleted += OnLoadMapConfigurationCompleted;
            GeoMapImagryProvider.LoadMapConfiguration(Application.Current);

            if (!this.ViewModel.IsMapInitialized)
                 this.ViewModel.InitializeMap(this.GeoMap);

            this.ViewModel.LoadDataCompleted += OnLoadDataCompleted;

            MapLayersWindow.MinimizedWidth = MapLayersWindow.ActualWidth;
            MapSourcesWindow.MinimizedWidth = MapSourcesWindow.ActualWidth;
        }
        private void OnLoadDataCompleted(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DataViewModel.WorldCountriesKey)
            {
                if (this.ViewModel.IsMapInitialized) AddWorldCountriesLayer();
            }
            if (e.PropertyName == DataViewModel.WorldCitiesKey)
            {
                if (this.ViewModel.IsMapInitialized) AddWorldCitiesLayer();
            }
            if (e.PropertyName == DataViewModel.WorldEarthQuakesKey)
            {
                if (this.ViewModel.IsMapInitialized) AddWorldEarthquakesLayer();
            }
            if (e.PropertyName == DataViewModel.AustralianSitesKey)
            {
                if (this.ViewModel.IsMapInitialized) AddAustralianSitesLayer();
            }
            if (e.PropertyName == DataViewModel.UnitedStatesPrecipitationKey)
            {
                if (this.ViewModel.IsMapInitialized) AddUnitedStatesPrecipitationLayer();
            }
            if (e.PropertyName == DataViewModel.UnitedStatesAirlineTrafficKey)
            {
                if (this.ViewModel.IsMapInitialized) AddUnitedStatesAirlineTrafficLayer();
            }

            this.MapSeriesEditor.UpdateSelection();
        }
         

        protected MapViewModel ViewModel;

        #region Map Events
        private void OnMapAreaRectChanged(object sender, RectChangedEventArgs e)
        { 

        }
        private void OnLoadMapConfigurationCompleted(object sender, ResultEventArgs e)
        {
            var mapLayer = new GeoTileImageryMapLayer
                {
                    ImageryViewModel = GeoMapImagryProvider.GetGeographicMapImageryView(),
                    Title = AppStrings.MapBaseLayer
                };
            this.ViewModel.MapLayers.Add(mapLayer);

            if (!this.ViewModel.IsMapInitialized)
                 this.ViewModel.InitializeMap(this.GeoMap); 
        }

        private void OnMapSeriesMouseRightButtonUp(object sender, DataChartMouseButtonEventArgs e)
        { 
        }

        private void OnMapSeriesMouseRightButtonDown(object sender, DataChartMouseButtonEventArgs e)
        { 
        }

        private void OnMapSeriesMouseMove(object sender, ChartMouseEventArgs e)
        { 
        } 
        #endregion

        private void OnAddWorldCountriesButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DataViewModel.LoadWorldCountriesShapes();
        }
        private void AddWorldCountriesLayer()
        {
            this.AddWorldCountriesShapesButton.IsEnabled = false;

            var mapLayer = new GeoShapeMapLayer();
            mapLayer.DataSourceKey = DataViewModel.WorldCountriesKey;
            mapLayer.DataSourceTrademark = CommonStrings.SourceData_USNA;
            mapLayer.Title = AppStrings.MapLayer_WorldCountries;
            mapLayer.LabelMemberPath = "Label"; 
            mapLayer.ActualSeriesView.ShapeMemberPath = "ShapePoints"; 
            mapLayer.Opacity = 0.75;

            this.ViewModel.MapLayers.Insert(0, mapLayer);
        }
        private void OnAddWorldCitiesButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DataViewModel.LoadWorldCitiesLocations();
        }
        private void AddWorldCitiesLayer()
        {
            this.AddWorldCitiesButton.IsEnabled = false;

            var mapLayer = new GeoSymbolProportionalMapLayer();
            mapLayer.DataSourceKey = DataViewModel.WorldCitiesKey;
            mapLayer.DataSourceTrademark = CommonStrings.SourceData_GeoCommons;
            mapLayer.Title = AppStrings.MapLayer_WorldCities;
            mapLayer.LabelMemberPath = "Label";
             
            mapLayer.RadiusMemberPath = "Population";
            mapLayer.FillMemberPath = "Population";
            mapLayer.Opacity = 0.75;
            var fillScale = new ValueBrushScaleView();
            fillScale.Brushes.Add(NamedColors.LightGreen.BrushOpacity(0.5));
            fillScale.Brushes.Add(NamedColors.Green.BrushOpacity(0.5));
            fillScale.MinimumValue = this.ViewModel.DataViewModel.WorldCities.DataSource.Population.Min;
            fillScale.MaximumValue = this.ViewModel.DataViewModel.WorldCities.DataSource.Population.Max;
            fillScale.MinimumRange = this.ViewModel.DataViewModel.WorldCities.DataSource.Population.Min;
            fillScale.MaximumRange = this.ViewModel.DataViewModel.WorldCities.DataSource.Population.Max;
            fillScale.ValueStringFormat = "#,##0,,.0 M";
            mapLayer.FillValueScale = fillScale; 
            mapLayer.RadiusScale.MaximumValue = 75;
            mapLayer.RadiusScale.MinimumValue = 10;
            this.ViewModel.MapLayers.Insert(0, mapLayer);
        }
        private void OnAddWorldEarthquakesButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DataViewModel.LoadWorldEarthquakesLocations();
        }
        private void AddWorldEarthquakesLayer()
        {
            this.AddWorldEarthquakesButton.IsEnabled = false;

            var mapLayer = new GeoSymbolProportionalMapLayer();
            mapLayer.DataSourceKey = DataViewModel.WorldEarthQuakesKey;
            mapLayer.DataSourceTrademark = CommonStrings.SourceData_USGS;
            mapLayer.Title = AppStrings.MapLayer_WorldEarthquakes;
            mapLayer.LabelMemberPath = "Label"; 
            mapLayer.FillMemberPath = "Magnitude";
            mapLayer.RadiusMemberPath = "Magnitude";
            mapLayer.Opacity = 0.7;

            var fillScale = new ValueBrushScaleView();
            fillScale.Brushes.Add(NamedColors.Yellow.BrushOpacity(0.5));
            fillScale.Brushes.Add(NamedColors.Red.BrushOpacity(0.5)); 
            fillScale.MinimumValue = this.ViewModel.DataViewModel.WorldEarthQuakes.FilterView.MagnitudeMin;
            fillScale.MaximumValue = this.ViewModel.DataViewModel.WorldEarthQuakes.FilterView.MagnitudeMax;
            fillScale.MinimumRange = 0;
            fillScale.MaximumRange = 10;
            mapLayer.FillValueScale = fillScale;

            mapLayer.RadiusScale.MaximumValue = 50;
            mapLayer.RadiusScale.MinimumValue = 10;

            this.ViewModel.MapLayers.Insert(0, mapLayer); 
        }
      
        private void OnAddAustralianSitesButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DataViewModel.LoadAustralianSites();
        }
        private void AddAustralianSitesLayer()
        {
            this.AddAustralianSitesButton.IsEnabled = false;

            var mapLayer = new GeoHighDensityScatterMapLayer();
            mapLayer.DataSourceKey = DataViewModel.AustralianSitesKey;
            mapLayer.DataSourceTrademark = CommonStrings.SourceData_GeoCommons;
            mapLayer.Title = AppStrings.MapLayer_AustralianSites;
            mapLayer.LabelMemberPath = "Name"; 
            var fillScale = new ValueBrushScaleView();
            fillScale.Brushes.Add(NamedColors.DodgerBlue.BrushOpacity(0.75));
            fillScale.Brushes.Add(NamedColors.Red.BrushOpacity(0.75));
            fillScale.MinimumValue = 0;
            fillScale.MaximumValue = 50;
            fillScale.MinimumRange = 0;
            fillScale.MaximumRange = 200;
            mapLayer.HeatScale = fillScale;
            mapLayer.ActualSeriesView.MouseOverEnabled = true;
            mapLayer.ActualSeriesView.LatitudeMemberPath = "Latitude";
            mapLayer.ActualSeriesView.LongitudeMemberPath = "Longitude";
            mapLayer.Opacity = 0.7;
            this.ViewModel.MapLayers.Insert(0, mapLayer);
        }
     

        private void OnAddUnitedStatesPrecipitationButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DataViewModel.LoadUnitedStatesPrecipitation();
        }
        private void AddUnitedStatesPrecipitationLayer()
        {
            this.AddUnitedStatesPrecipitationButton.IsEnabled = false;

            var mapLayer = new GeoScatterAreaMapLayer();
            mapLayer.DataSourceKey = DataViewModel.UnitedStatesPrecipitationKey;
            mapLayer.DataSourceTrademark = CommonStrings.SourceData_NOAA;
            mapLayer.Title = AppStrings.MapLayer_US_Precipitation;
            mapLayer.LabelMemberPath = "Value";
            mapLayer.ColorScale.MinimumValue = 0.15; // 0.05
            mapLayer.ColorScale.MaximumValue = 1.3; // 1.8;
            mapLayer.ColorScale.ValueStringFormat = "0.00";
            mapLayer.ColorScale.MinimumRange = 0; //0.15; //  0;
            mapLayer.ColorScale.MaximumRange = 2.5;
            mapLayer.ColorScale.Palette = new ObservableCollection<Color> { NamedColors.DodgerBlue.Color, NamedColors.LimeGreen.Color, NamedColors.Orange.Color, NamedColors.Maroon.Color };
            mapLayer.Opacity = 0.7;
            this.ViewModel.MapLayers.Insert(0, mapLayer);
        }
      
        private void OnAddUnitedStatesAirlineTrafficButtonClick(object sender, RoutedEventArgs e)
        {
            this.ViewModel.DataViewModel.LoadUnitedStatesAirlineTraffic();

        }
        private void AddUnitedStatesAirlineTrafficLayer()
        {
            this.AddUnitedStatesAirlineTrafficButton.IsEnabled = false;

            var mapLayer = new GeoMotionMapLayer();
            mapLayer.Opacity = 0.7;
            mapLayer.Title = AppStrings.MapLayer_US_AirlineTraffic;
            mapLayer.DataSourceKey = DataViewModel.UnitedStatesAirlineTrafficKey;
            mapLayer.DataSourceTrademark = CommonStrings.SourceData_USAT;

            mapLayer.AirportsView.LabelMemberPath = "Name";
            mapLayer.AirportsView.ActualSeriesView.LongitudeMemberPath = "Longitude";
            mapLayer.AirportsView.ActualSeriesView.LatitudeMemberPath = "Latitude";
            mapLayer.AirportsView.ActualSeriesView.MarkerBrush = NamedColors.Red.BrushOpacity(0.5);

            var airportToolTip = Application.Current.Resources["AirportToolTipContent"] as ContentControl;

            var airportMarker = Application.Current.Resources["AirportMarker2"] as DataTemplate;
            if (airportMarker != null)
                mapLayer.AirportsView.ActualSeriesView.MarkerTemplate = airportMarker;

            mapLayer.AirplanesView.LabelMemberPath = "Flight.FlightCode";
            mapLayer.AirplanesView.ActualSeriesView.LongitudeMemberPath = "Longitude";
            mapLayer.AirplanesView.ActualSeriesView.LatitudeMemberPath = "Latitude";
            mapLayer.AirplanesView.ActualSeriesView.MarkerBrush = NamedColors.Red.BrushOpacity(0.75);
          
            var airplaneMarker = Application.Current.Resources["AirplaneMarker"] as DataTemplate;
            if (airplaneMarker != null)
                mapLayer.AirplanesView.ActualSeriesView.MarkerTemplate = airplaneMarker;

            var airplaneToolTip = Application.Current.Resources["AirplanesToolTipContent"] as ContentControl;
            if (airplaneToolTip != null)
            {
                //mapLayer.AirplanesView.ActualSeriesView.ToolTip = airplaneToolTip;
                //mapLayer.AirplanesView.ActualSeriesView.ToolTip
            }

            this.ViewModel.MapLayers.Insert(0, mapLayer);
        }

        private void OnAddImageryLayerButtonClick(object sender, RoutedEventArgs e)
        {
            AddImageryLayerButton.IsEnabled = false;

            var mapLayer = new GeoTileImageryMapLayer();
            mapLayer.ImageryViewModel = GeoImageryViews.Esri.WorldOceansMap;
            mapLayer.Opacity = 1;
            mapLayer.Title = AppStrings.MapLayer_MapImageryOverlay;
            this.ViewModel.MapLayers.Insert(0, mapLayer);

            this.MapSeriesEditor.UpdateSelection();
        }

        private void OnMapLayersHeaderWindowClick(object sender, MouseEventArgs e)
        {
            ToggleWindow(MapLayersWindow);
        }
        private void OnMapSourcesHeaderWindowClick(object sender, MouseEventArgs e)
        {
            ToggleWindow(MapSourcesWindow);
        }
        private void ToggleWindow(XamDialogWindow dialog)
        { 
            if (dialog == null) return;
            switch (dialog.WindowState)
            {
                case WindowState.Maximized:
                case WindowState.Normal:
                    dialog.WindowState = WindowState.Minimized;
                    break;
                case WindowState.Minimized:
                    dialog.WindowState = WindowState.Normal;
                    break;
            }

        }
    }
}
