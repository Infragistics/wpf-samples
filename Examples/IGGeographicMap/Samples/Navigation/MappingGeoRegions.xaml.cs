using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using IGGeographicMap.Models;
using IGGeographicMap.Resources;
using Infragistics.Samples.Shared.Extensions;   // FormatRect() method
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class MappingGeoRegions : Infragistics.Samples.Framework.SampleContainer
    {
        public MappingGeoRegions()
        {
            InitializeComponent();
            this.FavouriteViews = new ObservableCollection<MapView>();
            InitializeMap();
          
        }
        public ObservableCollection<MapView> FavouriteViews { get; private set; }
                       
        #region Methods
        private string GetMapViewDescription(DateTime mapViewTime)
        {
            return MapStrings.XWGM_MappingGeoRegions_MapView + " (" + mapViewTime.Hour + ":" + mapViewTime.Minute + ":" + mapViewTime.Second + ")";
        }
        private string GetMapViewDescription(Rect mapViewRect)
        {
            return MapStrings.XWGM_MappingGeoRegions_MapView + " (" + mapViewRect.FormatRect() + ")";
        }
        private void InitializeMap()
        {
      
           FavouriteViews.Add(new MapView { WindowRect = new Rect(-180.00, -85.05, 360.00, 170.10), Description = ModelStrings.XWM_Region_World }); 
           FavouriteViews.Add(new MapView { WindowRect = new Rect(-153.17, 1.80, 117.00, 73.86), Description = ModelStrings.XWM_Region_NorthAmerica });    
           FavouriteViews.Add(new MapView { WindowRect = new Rect(-96.26, -57.90, 88.16, 74.45), Description = ModelStrings.XWM_Region_SouthAmerica });    
           FavouriteViews.Add(new MapView { WindowRect = new Rect(-21.70, -38.04, 83.01, 76.58), Description = ModelStrings.XWM_Region_Africa });    
           FavouriteViews.Add(new MapView { WindowRect = new Rect(-16.12, 32.04, 67.50, 38.61), Description = ModelStrings.XWM_Region_Europe });    
           FavouriteViews.Add(new MapView { WindowRect = new Rect(31.38, 0.95, 119.08, 75.02), Description = ModelStrings.XWM_Region_Asia });    
           FavouriteViews.Add(new MapView { WindowRect = new Rect(107.91, -46.30, 45.93, 39.88), Description = ModelStrings.XWM_Region_Australia });   

      
            this.FavouriteViewsListBox.ItemsSource = this.FavouriteViews;
            this.FavouriteViewsListBox.SelectionChanged += OnFavouriteViewsListBoxChanged;
        }

        private void AddFavourite(object sender, RoutedEventArgs e)
        {
            Rect geoRect = this.GeoMap.GetGeographicFromZoom(this.GeoMap.WindowRect);
            var view = new MapView { WindowRect = geoRect, Description = GetMapViewDescription(DateTime.Now) };
            this.FavouriteViews.Add(view);

            if (this.FavouriteViewsListBox.Items.Contains(view))
            {
                this.FavouriteViewsListBox.SelectedItem = view;
            }
        }

        private void RemoveFavourite(object sender, RoutedEventArgs e)
        {
            if (this.FavouriteViewsListBox.SelectedItem != null)
            {
                var view = this.FavouriteViewsListBox.SelectedItem as MapView;
                this.FavouriteViews.Remove(view);
            }
        } 
        #endregion

        #region Event Handlers
        private void OnFavouriteViewsListBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count < 1)
            {
                return;
            }

            var view = e.AddedItems[0] as MapView;
            if (view != null)
            {
                _viewRect = view.WindowRect;
                Rect geoRect = view.WindowRect;
                this.GeoMap.WindowRect = this.GeoMap.GetZoomFromGeographic(geoRect);
            }
        }

        private Rect _viewRect = new Rect();

        private void OnGeoMapWindowRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            Rect geoRect = this.GeoMap.GetGeographicFromZoom(this.GeoMap.WindowRect);
            this.MapViewTextBlock.Text = geoRect.FormatRect();
            System.Diagnostics.Debug.WriteLine("  ");
            System.Diagnostics.Debug.WriteLine("geoRect    " + geoRect.FormatRect());
            System.Diagnostics.Debug.WriteLine("viewRect   " + _viewRect.FormatRect());
            System.Diagnostics.Debug.WriteLine("WorldRect  " + this.GeoMap.WorldRect.FormatRect());
            System.Diagnostics.Debug.WriteLine("WindowRect  " + this.GeoMap.ActualWindowRect.FormatRect());
         
        } 
        #endregion
    }

  
}
