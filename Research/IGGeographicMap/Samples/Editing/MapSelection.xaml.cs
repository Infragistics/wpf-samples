using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using IGGeographicMap.Models;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples
{
    public partial class MapSelection : Infragistics.Samples.Framework.SampleContainer
    {
        public MapSelection()
        {
            InitializeComponent();

            ShapefileSource = (ShapefileConverter)this.Resources["shapeFileSource"];
            ShapefileSource.ImportCompleted += OnShapefileImportCompleted;
            
            this.MapSelectionViewModel = new MapSelectionViewModel();
            this.DataContext = MapSelectionViewModel;

            this.SampleLoaded += OnSampleLoaded;
        }
        protected ShapefileConverter ShapefileSource;
        protected MapSelectionViewModel MapSelectionViewModel;
       
        void OnSampleLoaded(object sender, System.EventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }
        private void OnShapefileImportCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var shapeFileConverter = (ShapefileConverter)sender;

            foreach (ShapefileRecord record in shapeFileConverter)
            {
                var shapeItem = new SelectableShapeElement();
                if (record.Fields != null)
                {
                    shapeItem.Points = record.Points;
                    shapeItem.Fields = record.Fields;
                    shapeItem.ShapeName = record.Fields["NAME"] as string;
                    // add loaded shape records as Selectable Shape Elements to the view model
                    this.MapSelectionViewModel.SelectableShapeElements.Add(shapeItem);
                }
            }
            if (this.MapSelectionViewModel.SelectableShapeElements.Count > 0)
                this.GeoMap.Series[0].ItemsSource = this.MapSelectionViewModel.SelectableShapeElements;
        
        }
     
        private void GeoMap_SeriesMouseLeftButtonDown(object sender, DataChartMouseButtonEventArgs e)
        {
            //e.Handled = true;
            var item = (SelectableShapeElement)e.Item;
            if (item.IsSelected)
            {
                item.IsSelected = false;
                this.MapSelectionViewModel.RemoveSelectedShapeElement(item);
             }
            else
            {
                item.IsSelected = true;
                this.MapSelectionViewModel.AddSelectedShapeElement(item);
            }
        }

        private void OnSelectAllShapesButtonClick(object sender, RoutedEventArgs e)
        {
            this.MapSelectionViewModel.SelectAllShapeElements();
        }

        private void OnDeselectAllShapesButtonClick(object sender, RoutedEventArgs e)
        {
            this.MapSelectionViewModel.DeselectAllShapeElements();
        }

    }
    /// <summary>
    /// Represents Map Selection view model with a collection of Selectable Shape Elements
    /// </summary>
    public class MapSelectionViewModel : ObservableModel
    {
        public MapSelectionViewModel()
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
    public class IsSelectedShapeStyleRule : PropertyValueConditionalStyleRule
    {

        public override Style EvaluateCondition(object item)
        {
            var frh = new FastReflectionHelper() { PropertyName = this.ValueMemberPath };
            object propertyValue = frh.GetPropertyValue(item);
            if (this.EvaluateConditionAgainstPropertyValue(propertyValue))
            {
                return this.StyleToApply;
            }
            return null;
        }
        /// <summary>
        /// Evaluates if the given value is between RangeMinValue and RangeMaxValue.
        /// </summary>
        /// <param name="value">The value to compare with the RangeMinValue and RangeMaxValue.</param>
        /// <returns>True if the value argument is between RangeMinValue and RangeMaxValue, otherwise False.</returns>
        public override bool EvaluateConditionAgainstPropertyValue(object value)
        {
            bool result;
            if (!bool.TryParse(value.ToString(), out result)) return false;
            
            return result == this.ComparisonValue;
        }

        private const string ComparisonValuePropertyName = "ComparisonValue";
        /// <summary>
        /// Identifies the ComparisonValue dependency property.
        /// </summary>
        public static readonly DependencyProperty ComparisonValueProperty = 
            DependencyProperty.Register(ComparisonValuePropertyName,
            typeof(bool), typeof(IsSelectedShapeStyleRule), new PropertyMetadata(false, (sender, e) =>
        {
            /* _ */
        }));
        /// <summary>
        /// The value to compare against when evaluating items for equality.
        /// </summary>
        public bool ComparisonValue
        {
            get
            {
                return (bool)this.GetValue(ComparisonValueProperty);
            }
            set
            {
                this.SetValue(ComparisonValueProperty, value);
            }
        }
    }

     

}
