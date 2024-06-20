using System;
using System.Collections.ObjectModel;
using System.Windows;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using IGMap.Models;

namespace IGMap.Samples.Data
{
    public partial class BindingViewModels : Infragistics.Samples.Framework.SampleContainer
    {
        protected MapAdapter MapAdapter;
        protected MapDataViewModel MapViewModel;

        public BindingViewModels()
        {
            InitializeComponent();

            this.MapAdapter = new MapAdapter(this.shapeMap);
            this.MapAdapter.SetMapBoundary();

            // create data view model with location of world cities
            this.MapViewModel = new MapDataViewModel();
            this.MapViewModel.Locations = GeoLocations.Cities;
           
        }

        #region Event Handlers
         
        private void OnMapLoaded(object sender, RoutedEventArgs e)
        {
           var converter = new Infragistics.DataMapping.Converter();
            
            // set world rectangle on map layer to world rectangle of the map control
            this.shapeMap.Layers[0].WorldRect = this.shapeMap.WorldRect;
            
            // add map element to the map layer
            foreach (GeoLocation location in this.MapViewModel.Locations)
            {
                AddMapLocation(location);
            }
         }

        #endregion Event Handlers

        private void AddMapLocation(GeoLocation worldLocation)
        {
             
            // get mapLocation using a projection from Geodetic to Cartesian coordinates 
            Point mapLocation = this.shapeMap.MapProjection.ProjectToMap(worldLocation.ToPoint());

            // create Symbol Element
            SymbolElement mapElement = new SymbolElement
            {
                SymbolOrigin = mapLocation,
                SymbolType = MapSymbolType.None,
                ToolTip=worldLocation.Name
            };

            // add the Symbol Element to the map control
            this.shapeMap.Layers[0].Elements.Add(mapElement);

        }

    }
    public class MapDataViewModel : ObservableModel
    {

        private ObservableCollection<GeoLocation> _locations = new ObservableCollection<GeoLocation>();
        public ObservableCollection<GeoLocation> Locations
        {
            get
            {
                return _locations;
            }
            set
            {
                if (_locations != value)
                {
                    _locations = value;
                    this.OnPropertyChanged("Locations");
                }

            }
        }
    }
}