using Infragistics.Controls.Layouts;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DragIntoTileManagerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XamTile _previousXamTile;
        int _dropIndex = 0;

        ViewModel _viewModel = new ViewModel();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = _viewModel;
        }

        private void DragSource_Drop(object sender, Infragistics.DragDrop.DropEventArgs e)
        {
            TextBlock text = e.DragSource as TextBlock;
            TileItem item = text.DataContext as TileItem;

            if (e.DropTarget is XamTile)
            {
                _viewModel.TileSource.Insert(_dropIndex, item);
            }
            else
            {
                if (_previousXamTile != null)
                {
                    _viewModel.TileSource.Insert(_dropIndex, item);
                }
                else
                {
                    _viewModel.TileSource.Add(item);
                }
            }

            ClearPrevousTile();
        }

        private void DragSource_DragOver(object sender, Infragistics.DragDrop.DragDropMoveEventArgs e)
        {
            if (e.DropTarget is XamTile tile)
            {
                if (_previousXamTile != null && _previousXamTile != e.DropTarget)
                {
                    _previousXamTile.Margin = new Thickness(0);
                }

                TileItem item = tile.Content as TileItem;

                var tileInfo = _tileManager.GetItemInfo(item);
                var rect = tileInfo.GetCurrentTargetRect();

                Point pointToTile = Mouse.GetPosition(_tileManager);
                if (rect.Contains(pointToTile))
                {
                    _dropIndex = _viewModel.TileSource.IndexOf(item);

                    var midPoint = tile.ActualWidth / 2;
                    pointToTile = Mouse.GetPosition(tile);
                    if (pointToTile.X >= midPoint)
                    {
                        tile.Margin = new Thickness(0, 0, tile.ActualWidth, 0);
                        _dropIndex = _dropIndex + 1;
                    }
                    else
                    {
                        tile.Margin = new Thickness(tile.ActualWidth, 0, 0, 0);
                    }
                }

                _previousXamTile = tile;
            }
        }

        private void DragSource_DragEnd(object sender, Infragistics.DragDrop.DragDropEventArgs e)
        {
            if (e.DropTarget == null)
            {
                ClearPrevousTile();
            }            
        }

        private void ClearPrevousTile()
        {
            if (_previousXamTile != null)
            {
                _previousXamTile.Margin = new Thickness(0);
                _previousXamTile = null;
            }
        }
    }
}
