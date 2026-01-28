using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using IGMap.Models;
using IGMap.Resources;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples.Navigation
{
    /// <summary>
    /// Interaction logic for FavoriteMapViews.xaml
    /// </summary>
    public partial class FavoriteMapViews : Infragistics.Samples.Framework.SampleContainer
    {
        public FavoriteMapViews()
        {
            InitializeComponent();
            this.FavouriteViews = new ObservableCollection<MapView>();
            InitializeMap();
        }
    
        public ObservableCollection<MapView> FavouriteViews { get; private set; }

        private string GetMapViewDescription(DateTime mapViewTime)
        {
            return MapStrings.XWM_FavoriteViews_MapView + " (" + mapViewTime + ")";
        }
        private string GetMapViewDescription(Rect mapViewRect)
        {
            return MapStrings.XWM_FavoriteViews_MapView + " (" + mapViewRect.FormatRect() + ")";
        }
        private void InitializeMap()
        {
            List<GeoRegion> geoMapRegions = new List<GeoRegion>
                {
                    GeoRegions.WorldNonPolarRegion, GeoRegions.NorthAmericaRegion, GeoRegions.SouthAmericaRegion, 
                    GeoRegions.AfricaRegion, GeoRegions.EuropeRegion, GeoRegions.AsiaRegion, GeoRegions.OceaniaRegion
                };

            DateTime today = DateTime.Now;
            DateTime time = new DateTime(today.Year, today.Month, today.Day).Subtract(TimeSpan.FromDays(GeoRegions.KnownRegions.Count));
            foreach (GeoRegion geoRegion in geoMapRegions)
            {
                Rect windowRect = MapAdapter.ProjectMapRegion(geoRegion, theMap).ToRect();
                MapView view = new MapView { WindowRect = windowRect, Description = GetMapViewDescription(geoRegion.ToRect()) };
                // MapView view = new MapView { WindowRect = windowRect, Description = GetMapViewDescription(time) };
                time = time.Add(TimeSpan.FromHours(25));
                this.FavouriteViews.Add(view);
            }

            this.FavouriteViewsListBox.ItemsSource = this.FavouriteViews;
            this.FavouriteViewsListBox.SelectionChanged += OnFavouriteViewsListBoxChanged;
            this.FavouriteViewsListBox.SelectedIndex = 0;
        }

        private void AddFavourite(object sender, RoutedEventArgs e)
        {
            MapView view = new MapView { WindowRect = theMap.WindowRect, Description = GetMapViewDescription(DateTime.Now) };

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
                MapView view = this.FavouriteViewsListBox.SelectedItem as MapView;
                this.FavouriteViews.Remove(view);
            }
        }
        private void OnFavouriteViewsListBoxChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count < 1)
            {
                return;
            }

            MapView view = e.AddedItems[0] as MapView;
            if (view != null)
            {
                theMap.WindowRect = view.WindowRect;
            }
        }

        private void OnGeoMapWindowRectChanged(object sender, Infragistics.Controls.Maps.MapWindowRectChangedEventArgs e)
        {
            Rect windowRect = theMap.WindowRect;
            Point origin = theMap.MapProjection.UnprojectFromMap(new Point(windowRect.X, windowRect.Y));
            Point ending = theMap.MapProjection.UnprojectFromMap(new Point(windowRect.X + windowRect.Width, windowRect.Y - windowRect.Height));
            double width = System.Math.Abs(origin.X - ending.X);
            double height = System.Math.Abs(ending.Y + origin.Y);
            Rect geoRect = new Rect(origin.X, origin.Y, width, height);
            this.MapViewTextBlock.Text = geoRect.FormatRect();
        } 
    }

 
}
