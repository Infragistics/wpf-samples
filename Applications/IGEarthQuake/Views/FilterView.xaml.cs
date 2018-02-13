using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using IGExtensions.Framework;
using IGExtensions.Common.Data;
using IGExtensions.Common.Maps.StyleSelectors;
using IGExtensions.Common.Models;
using IGShowcase.EarthQuake.ViewModels;
using IGExtensions.Framework.Controls;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Maps;

namespace IGShowcase.EarthQuake.Views
{
    public partial class FilterView : UserControl
    {
        public FilterView()
        {
            InitializeComponent();
            this.RegionsViewModel = new ShapeSelectionViewModel();
            this.Loaded += new RoutedEventHandler(FilterView_Loaded);

        }
        private void FilterView_Loaded(object sender, RoutedEventArgs e)
        {
            _mvm = DataContext as MapViewModel;
            _tvm = DataContext as TimeLineViewModel;

            if (_mvm == null && _tvm == null)
            {
                System.Diagnostics.Debug.WriteLine("WARNING: FilterView is missing DataContext!");
            }
            if (_mvm != null)
            {
                _mvm.PropertyChanged += VmPropertyChanged;
                //UpdateMagnitudeFilter();
                //UpdateRegionMap(_mvm.Regions);
            }
            if (_tvm != null)
            {
                _tvm.PropertyChanged += VmPropertyChanged;
                //UpdateMagnitudeFilter();
                //UpdateRegionMap(_tvm.Regions);
            }
            if (!NavigationApp.Current.IsInDesingMode())
            {
                var path = DataStorageProvider.ShapefilesPath;
                _shapeFileConverter = new ShapefileConverter();
                _shapeFileConverter.ImportCompleted += ShapefileConverter_ImportCompleted;
                _shapeFileConverter.ShapefileSource = new Uri(path + "world/world_countries_reg_lbl.shp", UriKind.RelativeOrAbsolute);
                _shapeFileConverter.DatabaseSource = new Uri(path + "world/world_countries_reg_lbl.dbf", UriKind.RelativeOrAbsolute);
            }
        }

        private ShapefileConverter _shapeFileConverter;
        private MapViewModel _mvm;
        private TimeLineViewModel _tvm;
     
        private void VmPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

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
            _mvm.GetRegionBounds(fi.Name, out minLon, out minLat, out maxLon, out maxLat);

            _mvm.SelectedEarthQuake = null;
            //TODO: navigate to location on geo map
            //xamMap.WindowRect = GetRecForLatitudeLongitude(minLon, maxLat, maxLon, minLat);
        }
        #region Magnitude Event Handlers
        /// <summary>
        /// Handles drag completed event of the Max magnitude thumb
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.Primitives.DragCompletedEventArgs"/> instance containing the event data.</param>
        private void MinMagnitudeDragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (_tvm != null) _tvm.MinMagnitude = ((XamSliderNumericThumb)sender).Value;
            if (_mvm == null) return;
            _mvm.MinMagnitude = ((XamSliderNumericThumb)sender).Value;
            // Clear the selected earthquake if it is outside of the magnitude range.
            if (_mvm.SelectedEarthQuake != null)
            {
                if (_mvm.SelectedEarthQuake.Magnitude < _mvm.MinMagnitude)
                {
                    _mvm.SelectedEarthQuake = null;
                }
            }
        }
        /// <summary>
        /// Handles drag completed event of the Max magnitude thumb.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Controls.Primitives.DragCompletedEventArgs"/> instance containing the event data.</param>
        private void MaxMagnitudeDragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            if (_tvm != null) _tvm.MaxMagnitude = ((XamSliderNumericThumb)sender).Value;
            if (_mvm == null) return;
            _mvm.MaxMagnitude = ((XamSliderNumericThumb)sender).Value;
            // Clear the selected earthquake if it is outside of the magnitude range.
            if (_mvm.SelectedEarthQuake != null)
            {
                if (_mvm.SelectedEarthQuake.Magnitude > _mvm.MaxMagnitude)
                {
                    _mvm.SelectedEarthQuake = null;
                }
            }
        }
        /// <summary>
        /// Sets the minimum magnitude when an arrow key is up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void MinMagnitudeKeyUp(object sender, KeyEventArgs e)
        {
            if (_tvm != null) _tvm.MinMagnitude = ((XamSliderNumericThumb)sender).Value;
            if (_mvm == null) return;
                
            _mvm.MinMagnitude = ((XamSliderNumericThumb)sender).Value;
            // Clear the selected earthquake if it is outside of the magnitude range.
            if (_mvm.SelectedEarthQuake != null)
            {
                if (_mvm.SelectedEarthQuake.Magnitude < _mvm.MinMagnitude)
                {
                    _mvm.SelectedEarthQuake = null;
                }
            }
        }
        /// <summary>
        /// Sets the maximum magnitude when an arrow key is up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void MaxMagnitudeKeyUp(object sender, KeyEventArgs e)
        {
            if (_tvm != null) _tvm.MaxMagnitude = ((XamSliderNumericThumb)sender).Value;
            if (_mvm == null) return;
            _mvm.MaxMagnitude = ((XamSliderNumericThumb)sender).Value;
            //Clear the selected earthquake if it is outside of the magnitude range.
            if (_mvm.SelectedEarthQuake != null)
            {
                if (_mvm.SelectedEarthQuake.Magnitude > _mvm.MaxMagnitude)
                {
                    _mvm.SelectedEarthQuake = null;
                }
            }
        }
        #endregion

        private void RegionMap_SeriesMouseLeftButtonDown(object sender, DataChartMouseButtonEventArgs e)
        {
            // handle section of the continent regions
            e.Handled = true;
            var item = e.Item as SelectableShapeElement;
            if (item != null)
            {
                OnShapeElementClicked(item);
            }
        }
        private void RegionMap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void OnShapeElementClicked(SelectableShapeElement shapeElement)
        {
            var isSelected = !IsSelectedRegion(shapeElement.ShapeName);
            if (_mvm != null)
            {
                foreach (var region in _mvm.Regions)
                {
                    if (region.Name == shapeElement.ShapeName)
                    {
                        region.IsSelected = isSelected;
                        //region.IsSelected = !region.IsSelected;
                    }
                }
            }
            if (_tvm != null)
            {
                foreach (var region in _tvm.Regions)
                {
                    if (region.Name == shapeElement.ShapeName)
                    {
                        region.IsSelected = isSelected;
                        //region.IsSelected = !region.IsSelected;
                    }
                }
            }
            UpdateRegionFilter();
        }
        private void UpdateMagnitudeFilter()
        {
            if (_tvm != null) this.magnitudeMinSlider.Value = _tvm.MinMagnitude;
            if (_tvm != null) this.magnitudeMaxSlider.Value = _tvm.MaxMagnitude;

            if (_mvm != null) this.magnitudeMinSlider.Value = _mvm.MinMagnitude;
            if (_mvm != null) this.magnitudeMaxSlider.Value = _mvm.MaxMagnitude;

        }
        private void UpdateRegionFilter()
        {
            if (_mvm != null)
            {
                UpdateRegionFilter(_mvm.Regions);
            }
            if (_tvm != null)
            {
                UpdateRegionFilter(_tvm.Regions);
            }
        }
        private void UpdateRegionFilter(FilterViewModel regions)
        {
            if (this.RegionMap == null || this.RegionMap.Series.Count == 0)
                return;

            var series = this.RegionMap.Series.OfType<GeographicShapeSeries>().Last();
            series.ItemsSource = null;
            foreach (var shape in this.RegionsViewModel.SelectableShapeElements)
            {
                var region = regions.First(reg => reg.Name == shape.ShapeName);
                shape.IsSelected = region.IsSelected;
            }
            series.ItemsSource = this.RegionsViewModel.SelectableShapeElements;
        }
        private bool IsSelectedRegion(string regionName)
        {
            if (_mvm != null && _mvm.Regions.Any())
            {
                foreach (var item in _mvm.Regions.Where(item => item.Name == regionName))
                {
                    return item.IsSelected;
                }
            }
            if (_tvm != null && _tvm.Regions.Any())
            {
                foreach (var item in _tvm.Regions.Where(item => item.Name == regionName))
                {
                    return item.IsSelected;
                }
            }
            return true;
        }

        public ShapeSelectionViewModel RegionsViewModel { get; set; }

        private void ShapefileConverter_ImportCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var shapePoints = GeoRegions.WorldFullRegion.ToPoints();
            var shapeOceansElement = new SelectableShapeElement
                {
                    ShapeName = "Other",
                    ShapeLabel = "Oceans",
                    IsSelected = true
                };
            shapeOceansElement.Points = new List<List<Point>> {shapePoints};
            this.RegionsViewModel.SelectableShapeElements.Add(shapeOceansElement);

            var shapefile = (ShapefileConverter) sender;
            foreach (var record in shapefile)
            {
                var shapeItem = new SelectableShapeElement();
                if (record.Fields != null)
                {
                    var regionName = record.Fields["REGION"] as string;
                    if (regionName == "NorthAfrica") regionName = "Africa";
                    if (regionName == "Sub Saharan Africa") regionName = "Africa";
                    if (regionName == "Pacific") regionName = "Other"; // "Other";
                    if (regionName == "Caribbean") regionName = "North America";
                    if (regionName == "Latin America") regionName = "South America";
                    if (regionName == null) return;

                    shapeItem.Points = record.Points;
                    shapeItem.Fields = record.Fields;
                    shapeItem.ShapeLabel = regionName == "Other" ? "Oceans" : regionName;
                    shapeItem.ShapeName = regionName;
                    shapeItem.IsSelected = IsSelectedRegion(shapeItem.ShapeName);

                    // add loaded shape records as Selectable Shape Elements to the view model
                    this.RegionsViewModel.SelectableShapeElements.Add(shapeItem);
                }
            }
            UpdateRegionFilter();

        }
    }
    /// <summary>
    /// Represents Map Selection view model with a collection of Selectable Shape Elements
    /// </summary>
    public class ShapeSelectionViewModel : ObservableModel
    {
        public ShapeSelectionViewModel()
        {
            this.SelectedShapeElements = new ObservableCollection<SelectableShapeElement>();
            this.SelectableShapeElements = new ObservableCollection<SelectableShapeElement>();
            this.SelectableShapeElements.CollectionChanged += OnShapeElementsCollectionChanged;
        }

        void OnShapeElementsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged("SelectableShapeElements");
        }
        public void AddSelectedShapeElement(SelectableShapeElement element)
        {
            this.SelectedShapeElements.Add(element);
        }
        public void RemoveSelectedShapeElement(SelectableShapeElement selectedElement)
        {
            this.SelectedShapeElements.Remove(selectedElement);
        }
        public void DeselectAllShapeElements()
        {
            this.SelectedShapeElements = new ObservableCollection<SelectableShapeElement>();
            foreach (var element in this.SelectableShapeElements)
            {
                element.IsSelected = false;
            }
        }
        public void SelectAllShapeElements()
        {
            this.SelectedShapeElements = new ObservableCollection<SelectableShapeElement>();
            foreach (var element in this.SelectableShapeElements)
            {
                element.IsSelected = true;
                _selectedShapeElements.Add(element);
            }
        }

        #region Properties
        private ObservableCollection<SelectableShapeElement> _shapeElements;
        public ObservableCollection<SelectableShapeElement> SelectableShapeElements
        {
            get { return _shapeElements; }
            set
            {
                _shapeElements = value;
                OnPropertyChanged("SelectableShapeElements");
            }
        }
        private ObservableCollection<SelectableShapeElement> _selectedShapeElements;
        public ObservableCollection<SelectableShapeElement> SelectedShapeElements
        {
            get { return _selectedShapeElements; }
            set
            {
                _selectedShapeElements = value;
                OnPropertyChanged("SelectedShapeElements");
            }
        }
        #endregion


        #region Event Handlers

        protected new void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
        }
        #endregion
    }

    /// <summary>
    /// Represents a Selectable Shape Element  
    /// </summary>
    public class SelectableShapeElement : Infragistics.Controls.Maps.ShapefileRecord, INotifyPropertyChanged
    {
        public SelectableShapeElement()
        {
            this.IsSelected = false;
        }

        public string ShapeName { get; set; }
        public string ShapeLabel { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public override string ToString()
        {
            return this.ShapeName;
        }
        public new event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(sender, e);
        }
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Rule class used for applying conditional styling for SelectableShapeElement which are selected or deselected.
    /// </summary>
    public class IsSelectedShapeStyleRule : ComparisonStyleRule
    {
        public override Style EvaluateCondition(object item)
        {
            var comparisoncompPropReflection = new FastReflectionHelper() { PropertyName = this.ValueMemberPath };
            object comparisonPropValue = comparisoncompPropReflection.GetPropertyValue(item);

            var selectionPropReflection = new FastReflectionHelper() { PropertyName = this.SelectionMemberPath };
            object selectionPropValue = selectionPropReflection.GetPropertyValue(item);
            
            if (this.EvaluateConditionAgainstPropertyValue(comparisonPropValue))
            {
                if (this.EvaluateSelectionAgainstPropertyValue(selectionPropValue))
                {
                    return this.SelectedStyle;
                }
                else
                {
                    return this.DeSelectedStyle;
                }
            }
            return null;
        }

        public Style DeSelectedStyle { get; set; }
        public Style SelectedStyle { get; set; }

        public string SelectionMemberPath { get; set; }

        public bool EvaluateSelectionAgainstPropertyValue(object value)
        {
            bool result;
            if (!bool.TryParse(value.ToString(), out result)) return false;

            return result == true;
        }
    }
    
}
