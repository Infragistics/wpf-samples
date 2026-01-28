using System.Windows.Controls;
using IGMap.Models;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples
{
    public partial class World : Infragistics.Samples.Framework.SampleContainer
    {
        public World()
        {
            InitializeComponent();
        }
        private void OnWorldMapLayerImported(object sender, MapLayerImportEventArgs e)
        {
            var mapLayer = sender as MapLayer;
            if (mapLayer != null)
            {

                if (e.Action == MapLayerImportAction.End)
                {
                    MapAdapter.ZoomMapToRegion(this.Map, GeoRegions.WorldNonPolarRegion);
                }
            }
        }
     
    }
}