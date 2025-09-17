using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace IGMap.Models
{
    public class MapProjectionViewModel
    {
        public MapProjectionViewModel()
        {
            MapAdapters = new List<MapCoordinateAdapter>();

            MapViews = new ObservableCollection<MapProjectionModel>();
        }

        public List<MapCoordinateAdapter> MapAdapters { get; set; }

        public ObservableCollection<MapProjectionModel> MapViews { get; set; }


    }
}