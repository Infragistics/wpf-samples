using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using IGShowcase.GeographicMapBrowser.Models;
using IGShowcase.GeographicMapBrowser.ViewModels;

namespace IGShowcase.GeographicMapBrowser.Views
{
    public partial class MapSeriesEditor : UserControl
    {
        public MapSeriesEditor()
        {
            InitializeComponent();
            this.Loaded += MapSeriesEditor_Loaded;
        }

        void MapSeriesEditor_Loaded(object sender, RoutedEventArgs e)
        {
            this.MapSeriesListBox.SelectedIndex = 0;
             
        }

        #region Properties
        public const string MapLayersPropertyName = "MapLayers";
        public static readonly DependencyProperty MapLayersProperty = DependencyProperty.Register(
            MapLayersPropertyName, typeof(GeoMapDataLayers), typeof(MapSeriesEditor), new PropertyMetadata(null, (sender, e) =>
        {
            (sender as MapSeriesEditor).OnPropertyChanged(new PropertyChangedEventArgs(MapLayersPropertyName));
        }));
        /// <summary>
        /// Gets or sets the MapLayers property 
        /// </summary>
        public GeoMapDataLayers MapLayers
        {
            get { return (GeoMapDataLayers)GetValue(MapLayersProperty); }
            set { SetValue(MapLayersProperty, value); }
        } 
        #endregion
        private void OnPropertyChanged(PropertyChangedEventArgs ea)
        {
            if (ea.PropertyName == "MapLayers")
            { 
                this.MapLayers.CollectionChanged += OnMapLayersCollectionChanged;
            }
        }

        private void OnMapLayersCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {  
                if (this.MapSeriesListBox.SelectedIndex == 0)
                    this.MapSeriesListBox.SelectedIndex = this.MapLayers.Count - 1;

                this.MapSeriesListBox.SelectedIndex = 0; 
            }
        }
         
        protected int CurrentIndex = -1;

        private void MapSeriesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.MapSeriesListBox.SelectedIndex == -1) return;
           
            this.MapLayerControls.Visibility = Visibility.Collapsed;
            var mapLayer = e.AddedItems[0];
            var mapLayerBinding = new Binding { Source = mapLayer, Mode = BindingMode.OneWay };
            GeoMapLayerEditor mapLayerEditor;
           
            if (mapLayer is GeoSymbolProportionalMapLayer)
                ShowMapEditor(mapLayer as GeoSymbolProportionalMapLayer);

            else if (mapLayer is GeoShapeMapLayer)
                ShowMapEditor(mapLayer as GeoShapeMapLayer);

            else if (mapLayer is GeoTileImageryMapLayer)
                ShowMapEditor(mapLayer as GeoTileImageryMapLayer);
            
            else if (mapLayer is GeoHighDensityScatterMapLayer)
                ShowMapEditor(mapLayer as GeoHighDensityScatterMapLayer);

            else if (mapLayer is GeoScatterAreaMapLayer)
            {
                mapLayerEditor = new GeoScatterAreaLayerEditor();
                mapLayerEditor.SetBinding(GeoMapLayerEditor.MapLayerProperty, mapLayerBinding);
                this.MapLayerControls.Child = mapLayerEditor;
                this.MapLayerControls.Visibility = Visibility.Visible;
            }
            else if (mapLayer is GeoMotionMapLayer)
            {
                mapLayerEditor = new GeoMotionFrameworkLayerEditor();
                mapLayerEditor.SetBinding(GeoMapLayerEditor.MapLayerProperty, mapLayerBinding);
                this.MapLayerControls.Child = mapLayerEditor;
                this.MapLayerControls.Visibility = Visibility.Visible;
            }

            else if (mapLayer is GeoSymbolMapLayer)
            {
                ShowMapEditor(mapLayer as GeoSymbolMapLayer); 
            } 
        }

        private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }
        private void ShowMapEditor(GeoMapLayer mapLayer)
        {
            var mapLayerBinding = new Binding()
            {
                Source = mapLayer,
                Mode = BindingMode.OneWay
            };
            var mapEditor = new GeoImagerLayerEditor();
            mapEditor.SetBinding(GeoMapLayerEditor.MapLayerProperty, mapLayerBinding);

            this.MapLayerControls.Child = mapEditor;
            this.MapLayerControls.Visibility = Visibility.Visible;
        }
        private void ShowMapEditor(GeoShapeMapLayer mapLayer)
        {
            var mapLayerBinding = new Binding()
            {
                Source = mapLayer,
                Mode = BindingMode.OneWay
            };
            var mapEditor = new GeoShapeLayerEditor();
            mapEditor.SetBinding(GeoMapLayerEditor.MapLayerProperty, mapLayerBinding);

            this.MapLayerControls.Child = mapEditor;
            this.MapLayerControls.Visibility = Visibility.Visible;
        }
        private void ShowMapEditor(GeoHighDensityScatterMapLayer mapLayer)
        {
            var mapLayerBinding = new Binding { Source = mapLayer, Mode = BindingMode.OneWay };
            var mapEditor = new GeoHighDensityLayerEditor();
            mapEditor.SetBinding(GeoMapLayerEditor.MapLayerProperty, mapLayerBinding);

            this.MapLayerControls.Child = mapEditor;
            this.MapLayerControls.Visibility = Visibility.Visible;
        }
        private void ShowMapEditor(GeoSymbolProportionalMapLayer mapLayer)
        {
            var mapLayerBinding = new Binding()
            {
                Source = mapLayer,
                Mode = BindingMode.OneWay
            };
            var mapEditor = new GeoSymbolLayerEditor();
            mapEditor.SetBinding(GeoMapLayerEditor.MapLayerProperty, mapLayerBinding);

            this.MapLayerControls.Child = mapEditor;
            this.MapLayerControls.Visibility = Visibility.Visible;
        }
        private void ShowMapEditor(GeoSymbolMapLayer mapLayer)
        {
            if (mapLayer.DataViewSource is AirlineTrafficDataViewSource)
            {
                var mapLayerBinding = new Binding { Source = mapLayer, Mode = BindingMode.OneWay };
                var mapLayerEditor = new GeoMotionFrameworkLayerEditor();
                mapLayerEditor.SetBinding(GeoMapLayerEditor.MapLayerProperty, mapLayerBinding);
                this.MapLayerControls.Child = mapLayerEditor;
                this.MapLayerControls.Visibility = Visibility.Visible;
            } 
        }

        public void UpdateSelection()
        {
            if (this.MapSeriesListBox.SelectedIndex == 0)
                this.MapSeriesListBox.SelectedIndex = -1;  

            this.MapSeriesListBox.SelectedIndex = 0;

        }
    }
}
