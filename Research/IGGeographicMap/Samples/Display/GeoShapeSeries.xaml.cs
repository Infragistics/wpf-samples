using System.Collections.Specialized;
using IGGeographicMap.Models;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using System.Windows;

namespace IGGeographicMap.Samples
{
    public partial class GeoShapeSeries : Infragistics.Samples.Framework.SampleContainer
    {
        public GeoShapeSeries()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // zoom in geo-map a specific geographic region of the world
            this.GeoMap.NavigateTo(GeoRegions.WorldNonAntarcticRegion);
        }
    }
}
