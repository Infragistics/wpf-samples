using IGGeographicMap.Samples.Custom;
using Infragistics.Samples.Shared.Models;

namespace IGGeographicMap.Samples.Data
{
    public partial class BindingViewModels : Infragistics.Samples.Framework.SampleContainer
    {
        public BindingViewModels()
        {
            InitializeComponent();
            this.GeoMap.Loaded += OnGeoMapLoaded;
        }

        protected void OnGeoMapLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion); // using GeoMapAdapter class
        }
    }
}
