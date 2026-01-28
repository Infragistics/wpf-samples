using System.Collections.Specialized;
using IGGeographicMap.Models;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class GeoShapeControlSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoShapeControlSeries()
        {
            InitializeComponent();
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
        }
        private void OnShapefileImportCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class

            this.GeoMap.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void OnShapefileCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.LoadedItemsCount += e.NewItems.Count;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadingItem + " " + this.LoadedItemsCount + "...";
        }
        protected int LoadedItemsCount;

    }
}
