using System.Windows.Controls;
using IGMap.Models;                         // MapAdapter
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples.Navigation
{
    public partial class MapNavigationPane : Infragistics.Samples.Framework.SampleContainer
    {
        public MapNavigationPane()
        {
            InitializeComponent();
            InitializeMap();
        }

        private void InitializeMap()
        {
            // initialize map boundary to specific map region
            MapAdapter.SetMapBoundary(theMap, GeoRegions.WorldFullRegion);
            MapAdapter.ZoomMapToRegion(theMap, GeoRegions.WorldNonPolarRegion);
        }           
    }
}
