using System.Collections.Specialized;
using System.Windows;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;

namespace IGGeographicMap.Samples
{
    public partial class MapSeriesVisibility : Infragistics.Samples.Framework.SampleContainer
    {
        public MapSeriesVisibility()
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // set bounds of geo-map world 
            Rect geoRect = GeoRegions.UnitedStatesLower48Region.ToGeoRect();
            this.GeoMap.WorldRect = geoRect;
        }
    }
}
