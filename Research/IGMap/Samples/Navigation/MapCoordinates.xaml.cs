using IGMap.Models;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Shared.Models;

namespace IGMap.Samples.Navigation
{
    public partial class MapCoordinates : Infragistics.Samples.Framework.SampleContainer
    {
        public MapCoordinates()
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
                    MapCoordinateAdapter.AddCoordinateGrid(this.Map.Layers[1]);
                    MapAdapter.ZoomMapToRegion(this.Map, GeoRegions.WorldNonPolarRegion);
                }
            }
        }

    }
   

  
}
