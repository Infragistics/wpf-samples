using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DragIntoTileManagerDemo
{
    public class ViewModel
    {
        public List<TileItem> ListSource { get; set; }
        public ObservableCollection<TileItem> TileSource { get; set; }

        public ViewModel()
        {
            ListSource = new List<TileItem>();
            TileSource = new ObservableCollection<TileItem>();

            for(int i=0; i<10; i++) {
                TileItem tile = new TileItem() { ID = i, Content = "Tile " + i.ToString() };
                ListSource.Add(tile);
            }

            TileSource.Add(ListSource[0]);
            TileSource.Add(ListSource[1]);
        }
    }

    public class TileItem
    {
        public int ID { get; set; }
        public string Content { get; set; }
    }
}
