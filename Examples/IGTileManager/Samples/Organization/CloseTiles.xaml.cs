using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGTileManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CloseTiles.xaml
    /// </summary>
    public partial class CloseTiles : SampleContainer
    {
        public CloseTiles()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(CloseTiles_Loaded);
        }

        void CloseTiles_Loaded(object sender, RoutedEventArgs e)
        {
            this.CloseActionCombo.ItemsSource = EnumValuesProvider.GetEnumValues<TileCloseAction>();
            this.CloseActionCombo.SelectedItem = this.xamTileManager1.TileCloseAction;
            this.CloseActionCombo.SelectionChanged += new SelectionChangedEventHandler(CloseActionCombo_SelectionChanged);
        }

        void CloseActionCombo_SelectionChanged(object sender, SelectionChangedEventArgs scea)
        {
            if (scea.AddedItems.Count > 0 && scea.AddedItems[0] is TileCloseAction)
            {
                this.xamTileManager1.TileCloseAction = (TileCloseAction)scea.AddedItems[0];
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in this.xamTileManager1.Items)
            {
                ItemTileInfo info = this.xamTileManager1.GetItemInfo(item);
                if (info.IsClosed)
                {
                    info.IsClosed = false;
                }
            }
        }
    }
}
