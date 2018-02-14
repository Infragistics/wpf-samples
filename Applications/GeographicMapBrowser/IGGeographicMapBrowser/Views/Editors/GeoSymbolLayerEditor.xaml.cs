using System.ComponentModel;
using System.Windows;
using IGExtensions.Common.Maps.DataSelectors;

namespace IGShowcase.GeographicMapBrowser.Views
{
    public partial class GeoSymbolLayerEditor : GeoMapLayerEditor 
    {
        public GeoSymbolLayerEditor()
        {
            InitializeComponent(); 
        }
          
        public override void OnPropertyChanged(PropertyChangedEventArgs ea)
        { 
            if (ea.PropertyName == MapLayerPropertyName)
            {
                if (this.MapLayer.DataViewSource is WorldCitiesViewSource)
                    this.WorldCitiesPopulationFilter.Visibility = Visibility.Visible;
                else
                    this.WorldCitiesPopulationFilter.Visibility = Visibility.Collapsed;

                if (this.MapLayer.DataViewSource is EarthQuakesViewSource)
                    this.WorldEarthQuakesMagnitudeFilter.Visibility = Visibility.Visible;
                else
                    this.WorldEarthQuakesMagnitudeFilter.Visibility = Visibility.Collapsed; 
            }
            base.OnPropertyChanged(ea);
        }

    }
}
