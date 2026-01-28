using System.Windows;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingTriangulatedFiles : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingTriangulatedFiles()
        {
            InitializeComponent();
            this.GeoMap.Visibility = System.Windows.Visibility.Collapsed;
            this.MapLoadingContainer.Visibility = System.Windows.Visibility.Visible;
            this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_Loading;
        }

        private void OnTriangulatedProviderPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("TriangulationSource"))
            {
                this.MapLoadingStatus.Text = CommonStrings.XW_SampleStatus_LoadedItems + " " + this.GetMapLoadedItemsCount();
                this.MapLoadingContainer.Visibility = System.Windows.Visibility.Collapsed;
                
                // show and set bounds of geo-map world 
                var mapRegion = new GeoRegion(new GeoLocation(-110, 50),
                                              new GeoLocation(-80, 15));
                Rect geoRect = mapRegion.ToGeoRect();
                this.GeoMap.WorldRect = geoRect;
                this.GeoMap.Visibility = System.Windows.Visibility.Visible;
            }
        }
        private int GetMapLoadedItemsCount()
        {
            ItfConverter itfProvider = this.LayoutRoot.Resources["itfConverter"] as ItfConverter;
            if (itfProvider == null) return 0;

            return itfProvider.TriangulationSource != null ? itfProvider.TriangulationSource.Triangles.Count : 0;
        }


    }
}
