using System;
using System.ComponentModel;
using System.Windows;
using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Common.Models;

namespace IGShowcase.GeographicMapBrowser.Models
{
    //TODO make abstract
    public class MapBackgroundLayer : MapLayer //ObservableObject //MapLayer
    {
        public MapBackgroundLayer()
        {

               
            //ImageryViewModel = new OpenStreetMapImageryView();
            this.ImageryViewModel = GeoImageryViews.OpenStreetMap.DefaultImageryView;
            //ImageryViewModel = GeoImageryViews.MapQuest.SatelliteImageryView;

            this.Title = "Map Imagery";
            //this.IsOrderEditable = false;
        }
        //public GeoImagerySource ImagerySource { get; set; }
        //public GeoImageryViewModel ImageryViewModel { get; set; }

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
                OnPropertyChanged("ImageryViewModel");
            }
        } 
        #endregion

        //public const string ImageryViewModelPropertyName = "ImageryViewModel";
        //public static readonly DependencyProperty ImageryViewModelProperty =
        //    DependencyProperty.Register(ImageryViewModelPropertyName, typeof(GeoImageryViewModel), typeof(MapBackgroundLayer),
        //                                new PropertyMetadata(null, (sender, e) =>
        //   {
        //        (sender as MapBackgroundLayer).OnPropertyChanged(new PropertyChangedEventArgs(ImageryViewModelPropertyName));
        //   })); 

        ///// <summary>
        ///// Gets or sets the ImageryViewModel property 
        ///// </summary>
        //public GeoImageryViewModel ImageryViewModel
        //{
        //    get { return (GeoImageryViewModel)GetValue(ImageryViewModelProperty); }
        //    set { SetValue(ImageryViewModelProperty, value); }
        //}


        void OnImageryViewModelChanged(object sender, PropertyChangedEventArgs ea)
        {
            this.Title = "Map Imagery - " + this.ImagerySource.ToString().Replace("Imagery", "");
        
            //switch (ea.PropertyName)
            //{
            //    case "MapImageryBackgroundLayer": //"ImageryViewModel":
            //        {
            //            this.GeoMap.LoadGeoImagery(this.MapViewModel.MapImageryBackgroundLayer.ImageryViewModel);

            //            break;

            //        }
            //}

            OnPropertyChanged("ImageryViewModel");
        }

       
       
    }

    public class GeoImageryPreviewMapLayer : MapBackgroundLayer //ObservableObject //MapLayer
    {
        public GeoImageryPreviewMapLayer() : base()
        {
            TileImageryExtentWidth = 300;
            TileImageryExtentHeight = 200;
        }
        private double _tileImageryWidthExtent;
        public double TileImageryExtentWidth
        {
            get { return _tileImageryWidthExtent; }
            set { if (_tileImageryWidthExtent == value) return; _tileImageryWidthExtent = value; OnPropertyChanged("TileImageryExtentWidth"); }
        }

        private double _tileImageryHeightExtent;
        public double TileImageryExtentHeight
        {
            get { return _tileImageryHeightExtent; }
            set { if (_tileImageryHeightExtent == value) return; _tileImageryHeightExtent = value; OnPropertyChanged("TileImageryExtentHeight"); }
        }

        private GeoRegion _tileImageryRegion;
        public GeoRegion TileImageryRegion
        {
            get { return _tileImageryRegion; }
            set { if (_tileImageryRegion == value) return; _tileImageryRegion = value; OnPropertyChanged("TileImageryRegion"); }
        }

    }
}
