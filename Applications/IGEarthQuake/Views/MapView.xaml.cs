using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;
using IGExtensions.Common.Maps;
using IGExtensions.Common.Maps.Imagery;
using IGExtensions.Common.Models;
using IGExtensions.Common.Services;
using IGExtensions.Framework.Extensions;
using IGShowcase.EarthQuake.ViewModels;
using IGExtensions.Framework.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Maps;
using BingMapsImageryStyle = IGExtensions.Common.Maps.Imagery.BingMapsImageryStyle;

namespace IGShowcase.EarthQuake.Views
{
    public partial class MapView : IGExtensions.Framework.Controls.NavigationPage
    {
        public MapView()
        {
            InitializeComponent();
            
            _isSelectedEarthquakeChanging = false;
            this.PageNavigated += MapView_PageNavigated;
          
            this.GeoMap.LoadGeoImagery(EsriMapImageryStyle.WorldOceansMap);
            //this.GeoMap.NavigateTo(GeoRegions.WorldNonPolarRegion);
            this.GeoMap.WindowRectChanged += GeoMap_WindowRectChanged;
             
            _vm = (MapViewModel)DataContext;
            _vm.PropertyChanged += VmPropertyChanged;

            // Attached properties to storyboards
            _rightPanelExpanded = (Storyboard)this.Resources["RightPanelExpanded"];
            _rightPanelCollapsed = (Storyboard)this.Resources["RightPanelCollapsed"];
            Storyboard.SetTargetProperty(_rightPanelExpanded, new PropertyPath(GridExtension.ColumnWidthProperty));
            Storyboard.SetTargetProperty(_rightPanelCollapsed, new PropertyPath(GridExtension.ColumnWidthProperty));
        }
        private readonly Storyboard _rightPanelExpanded;
        private readonly Storyboard _rightPanelCollapsed;
        private readonly bool _isSelectedEarthquakeChanging;

        private readonly MapViewModel _vm;
        private EarthQuakeViewModel _selectedEarthQuake;

        private void GeoMap_SeriesMouseLeftButtonUp(object sender, DataChartMouseButtonEventArgs e)
        {
            _selectedEarthQuake = e.Item as EarthQuakeViewModel;
            _vm.SelectedEarthQuake = null;

            ShowUpDetailsContorol();
        }

        /// <summary>
        /// Shows up details control.
        /// </summary>
        private void ShowUpDetailsContorol()
        {
            if (_selectedEarthQuake != null)
            {
                UpdateDetailsControlPosition();
                _vm.SelectedEarthQuake = _selectedEarthQuake; 
            }
        }

        /// <summary>
        /// Calculates the details control position.
        /// </summary>
        private void UpdateDetailsControlPosition()
        {
            if (_selectedEarthQuake != null && detailsControl != null)
            {
                try
                {
                    Point p = _selectedEarthQuake.GeoLocation;
                    p = this.GeoMap.GetMapPosition(p);
                    
                    double left = p.X + 6;
                    double top = p.Y + 6;
                    double maxWidth = canvas.ActualWidth;
                    double maxHeight = canvas.ActualHeight;

                    double width = detailsControl.Width;
                    double height = detailsControl.Height;

                    if (left + width > maxWidth)
                    {
                        left = left - width - 24;
                        if (left < 0) left = 0;
                    }

                    if (top + height > maxHeight)
                    {
                        top = top - height - 24;
                        if (top < 0) top = 0;
                    }

                    detailsControl.SetValue(Canvas.LeftProperty, Math.Round(left));
                    detailsControl.SetValue(Canvas.TopProperty, Math.Round(top));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        void GeoMap_WindowRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            var wrect = this.GeoMap.WindowRect;
            var grect = this.GeoMap.WorldRect;
            var arect = this.GeoMap.ActualWorldRect;
            //System.Diagnostics.Debug.WriteLine("wrect : " + wrect.ToString());
            System.Diagnostics.Debug.WriteLine("arect : " + arect.ToString());

            //System.Diagnostics.Debug.WriteLine("wrect : " + wrect.ToString() + ", " + "grect : " + grect.ToString() + ", " + "arect : " + arect.ToString() + ", ");
        }

        void MapView_PageNavigated(object sender, EventArgs e)
        {
            var rect = GeoRegions.UnitedStatesRegion.ToRect();
            //this.GeoMap.WorldRect = rect;
        }
        
        private void OnImageryInitialized(object sender, EventArgs e)
        {

#if SILVERLIGHT
            Dispatcher.BeginInvoke(() => UpdateBingMaps(sender));
#else // if WPF
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background,
               (Action)(() => UpdateBingMaps(sender)));
#endif
        }
        private void UpdateBingMaps(object sender)
        {
            // display geo-imagery from Bing Maps
            var connector = (BingMapsConnector)sender;
            this.GeoMap.BackgroundContent =
                new BingMapsMapImagery()
                {
                    TilePath = connector.TilePath,
                    SubDomains = connector.SubDomains
                };
        }
        /// <summary>
        /// Gets the navigation context of the current page. 
        /// </summary>
        public override string GetNavigationContext()
        {
            var context = base.GetNavigationContext();
            //TODO: add logic for creating navigation context
            context = "?" + "regions=[AU,NA,EU]&minValue=[1.0]";
            return context;
        }
        // Executes when the user navigates to this page.
        protected override void OnPageNavigated(NavigationEventArgs e)
        {
            base.OnPageNavigated(e);

            this.GeoMap.NavigateTo(GeoRegions.WorldNonPolarRegion);
        }
        private void VmPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == "EarthQuakes")
            //{
            //    AddMapElements();
            //}
            //else if (e.PropertyName == "SelectedEarthQuake")
            //{
            //    _isSelectedEarthquakeChanging = true;
            //    listEarthquakes.SelectedItem = _vm.SelectedEarthQuake;

            //    _isSelectedEarthquakeChanging = false;
            //}

        }
         
        /// <summary>
        /// Handles the Click event of the HyperlinkButton control.
        /// Fly to selected region (continent).
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void EarthQuakeListItemButton_Click(object sender, RoutedEventArgs e)
        {
            double minLon;
            double minLat;
            double maxLon;
            double maxLat;
            var fi = (FilterViewModel.FilterItem)((NavigationButton)sender).DataContext;
            _vm.GetRegionBounds(fi.Name, out minLon, out minLat, out maxLon, out maxLat);

            _vm.SelectedEarthQuake = null;
            //TODO: navigate to location on geo map
            //xamMap.WindowRect = GetRecForLatitudeLongitude(minLon, maxLat, maxLon, minLat);
        }
         
        /// <summary>
        /// Handles the Click event of the PointToMyLocation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PointToMyLocation_Click(object sender, RoutedEventArgs e)
        {
            FlyToMyLocation();
        }
        /// <summary>
        /// Flies to my location.
        /// </summary>
        private void FlyToMyLocation()
        {
            _vm.SelectedEarthQuake = null;
             
            //if (_myLocation.X != 0 && _myLocation.Y != 0)
            //{
            //    xamMap.WindowRect = GetRecForLatitudeLongitude(_myLocation.X - 20, _myLocation.Y + 20, _myLocation.X + 20, _myLocation.Y - 20);
            //}
            //else
            //{
            //    xamMap.WindowRect = GetRecForLatitudeLongitude(-180, 70, 180, -70);
            //}
        }
        /// <summary>
        /// Lists the earthquakes selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void EarthQuakeListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_isSelectedEarthquakeChanging && e.AddedItems != null && e.AddedItems.Count > 0)
            {
                MoveMapToLocation((EarthQuakeViewModel)(e.AddedItems[0]));
            }
        }
        /// <summary>
        /// Moves the map to earthquake location with animation.
        /// </summary>
        /// <param name="location">The location.</param>
        private void MoveMapToLocation(EarthQuakeViewModel location)
        {
            _vm.SelectedEarthQuake = null;
            //TODO: 
            // _currentElement = xamMap.Layers["symbolLayer"].Elements.FirstOrDefault(x => x.ToolTip == location);

            //Rect currentWindowRect = xamMap.WindowRect;
            //_nextWindowRect = GetRecForLatitudeLongitude(location.Longitude - 4,
            //                                                location.Latitude + 4,
            //                                                location.Longitude + 4,
            //                                                location.Latitude - 4);

            //currentWindowRect.Union(_nextWindowRect);
            //xamMap.WindowRect = currentWindowRect;
            //// Timer for zoom OUT animation
            //DispatcherTimer timer = new DispatcherTimer { Interval = _mapAnimationDiration };
            //// Timer for zoom IN animation
            //DispatcherTimer timer2 = new DispatcherTimer { Interval = _mapAnimationDiration };
            //timer.Tick += (o, e) =>
            //{
            //    xamMap.WindowRect = _nextWindowRect;
            //    timer.Stop();
            //    timer2.Tick += (o2, e2) =>
            //    {
            //        timer2.Stop();
            //        // After zoom IN animation ends show the Details Control
            //        ShowUpDetailsContorol();
            //    };
            //    timer2.Start();
            //};
            //timer.Start();
        }

        private bool _isExpandedRightPanel = true;
        private void RightPanelToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isExpandedRightPanel)
                _rightPanelCollapsed.Begin();
            else
                _rightPanelExpanded.Begin();

            _isExpandedRightPanel = !_isExpandedRightPanel;
        }


        private void ColorRange_OnThumbValueChanged(object sender, ThumbValueChangedEventArgs<double> e)
        {
            

        }

        private void ColorMinSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_vm != null && GeoMap.Series.Count > 0)
            {
                var series = GeoMap.Series.OfType<GeographicProportionalSymbolSeries>().First();
                var scale = series.FillScale as ValueBrushScale;
                if (scale != null) scale.MinimumValue = e.NewValue;
            }
        }
        private void ColorMaxSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_vm != null && GeoMap.Series.Count > 0)
            {
                var series = GeoMap.Series.OfType<GeographicProportionalSymbolSeries>().First();
                var scale = series.FillScale as ValueBrushScale;
                if (scale != null) scale.MaximumValue = e.NewValue;
            }
        }

        private void RadiusMinSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_vm != null && GeoMap.Series.Count > 0)
            {
                var series = GeoMap.Series.OfType<GeographicProportionalSymbolSeries>().First();
                var scale = series.RadiusScale;
                scale.MinimumValue = e.NewValue;
            }
        }

        private void RadiusMaxSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_vm != null && GeoMap.Series.Count > 0)
            {
                var series = GeoMap.Series.OfType<GeographicProportionalSymbolSeries>().First();
                var scale = series.RadiusScale;
                scale.MaximumValue = e.NewValue;
            }
        }

        private void XamLinearGauge_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
