using System.Collections.Specialized;
using IGGeographicMap.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class MapZoomBar : Infragistics.Samples.Framework.SampleContainer
    {
        public MapZoomBar()
        {
            InitializeComponent();
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
        }
        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadedItems + " " + this.LoadedItemsCount;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
             
            // zoom in geo-map to map view without polar regions
            this.GeoMap.WindowRect = MapViews.WorldNonPolarMapView.WindowRect;
            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
        }
        private void OnShapefileCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.LoadedItemsCount += e.NewItems.Count;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadingItem + " " + this.LoadedItemsCount;
        }
        protected int LoadedItemsCount;
    }
}
