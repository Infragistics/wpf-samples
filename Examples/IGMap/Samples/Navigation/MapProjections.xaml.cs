using System;
using System.Windows;
using System.Windows.Controls;
using IGMap.Models;
using Infragistics.Controls.Maps;

namespace IGMap.Samples.Navigation
{
    public partial class MapProjection : Infragistics.Samples.Framework.SampleContainer
    {
        public MapProjection()
        {
            InitializeComponent();
            ShowMapProjections();
             
        }
        private MapProjectionViewModel _viewModel;
        private readonly MapLoadingArbitter _mapLoadingArbitter = new MapLoadingArbitter();
        
        private void ShowMapProjections()
        {
            _viewModel = new MapProjectionViewModel();
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapBalthasart));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapBehrmann));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapEquirectangular));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapGallOrthographic));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapLambert));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapMercator));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapMiller37));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapMiller43));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapMiller50));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapMillerCylindrical));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapPeters));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapSphericalMercator));
            _viewModel.MapViews.Add(new MapProjectionModel(this.MapTristanEdwards));
           
            foreach (MapProjectionModel mapProjection in _viewModel.MapViews)
            {
                var button = new RadioButton();
                button.Content = mapProjection.ProjectionName;
                button.Tag = mapProjection.ToString();
                button.IsChecked = mapProjection.ProjectionMap.Visibility == Visibility.Visible;
                button.Click += OnMapProjectionChanged;
                this.MapProjectionsPanel.Children.Add(button);
            }
        }
        private void OnMapProjectionChanged(object sender, RoutedEventArgs e)
        {
            RadioButton button = (sender as RadioButton);
            if (button != null)
            {
                string tag = (string) button.Tag;

                foreach (MapProjectionModel mapProjectionModel in _viewModel.MapViews)
                {
                    if (tag.Equals(mapProjectionModel.ProjectionName))
                    {
                        mapProjectionModel.ProjectionMap.Visibility = Visibility.Visible;
                        mapProjectionModel.ProjectionMap.SetValue(Canvas.ZIndexProperty, 4);
                    }
                    else
                    {
                        mapProjectionModel.ProjectionMap.SetValue(Canvas.ZIndexProperty, 1);
                        mapProjectionModel.ProjectionMap.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
       
        private void OnWorldMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
            MapLayer mapLayer = sender as MapLayer;
            if (mapLayer != null)
            {
                string mapKey = mapLayer.Map.MapProjectionType.ToString();
                
                if (_mapLoadingArbitter.Dictionary.ContainsKey(mapKey))
                {
                    if (e.Action == MapLayerImportAction.Progress)
                    {
                        _mapLoadingArbitter.Dictionary[mapKey] = e.Progress;
                    }
                    else if (e.Action == MapLayerImportAction.End)
                    {
                        _mapLoadingArbitter.Dictionary[mapKey] = 100;
                    }
                }
                else
                {
                    _mapLoadingArbitter.Dictionary.Add(mapKey, e.Progress);
                }

                double progress = _mapLoadingArbitter.GetTotalProgress() / _viewModel.MapViews.Count;
                this.txtLoadingStatus.Text = String.Format("Loading... {0:0.00} %", progress);

                System.Diagnostics.Debug.WriteLine(String.Format("Loading... {0:0.00} %", progress));

                if (progress == 100)
                {
                    brdLoadingStatus.Visibility = Visibility.Collapsed;
                }
                    
            }
        }

    }
   

  
}
