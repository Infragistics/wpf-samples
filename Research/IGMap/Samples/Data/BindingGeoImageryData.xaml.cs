using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Shared.Models;
using IGMap.Models;

namespace IGMap.Samples.Data
{
    public partial class BindingGeoImageryData : Infragistics.Samples.Framework.SampleContainer
    {
        protected MapAdapter MapAdapter;
        private WorldCitiesViewModel viewModel;

        public BindingGeoImageryData()
        {
            InitializeComponent();

            this.viewModel = new WorldCitiesViewModel();

            this.DataContext = this.viewModel;
            this.MapAdapter = new MapAdapter(this.theMap);
            this.Loaded += OnSampleLoaded;


        }
        #region Event Handlers

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Set Initial Selection
            this.GeoLocationList.SelectedIndex = 0;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GeoLocation selectedLocation = this.GeoLocationList.SelectedItem as GeoLocation;
            this.MapAdapter.ZoomMapToLocation(selectedLocation);
        }

        private void theMap_Loaded(object sender, RoutedEventArgs e)
        {
            this.MapAdapter.SetMapBoundary();
        }

        #endregion Event Handlers

    }
    public class WorldCitiesViewModel : ObservableModel
    {

        public WorldCitiesViewModel()
        {
            _worldCities = GeoLocations.Cities;
        }

        private ObservableCollection<GeoLocation> _worldCities = new ObservableCollection<GeoLocation>();

        public ObservableCollection<GeoLocation> WorldCities
        {
            get
            {
                return _worldCities;
            }
            set
            {
                if (_worldCities != value)
                {
                    _worldCities = value;
                    this.OnPropertyChanged("WorldCities");
                }

            }
        }
    }
}