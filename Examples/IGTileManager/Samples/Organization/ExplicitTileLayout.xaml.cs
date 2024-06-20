using System.Windows.Controls;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGTileManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ExplicitTileLayout.xaml
    /// </summary>
    public partial class ExplicitTileLayout : SampleContainer
    {
        public ExplicitTileLayout()
        {
            InitializeComponent();
            this.Loaded += new System.Windows.RoutedEventHandler(ExplicitTileLayout_Loaded);
        }

        void ExplicitTileLayout_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.LayoutOrderCombo.ItemsSource = EnumValuesProvider.GetEnumValues<TileLayoutOrder>();
            this.LayoutOrderCombo.SelectedItem = this.xamTileManager1.NormalModeSettings.TileLayoutOrder;
            this.LayoutOrderCombo.SelectionChanged += new SelectionChangedEventHandler(LayoutOrderCombo_SelectionChanged);
        }

        void LayoutOrderCombo_SelectionChanged(object sender, SelectionChangedEventArgs scea)
        {
            if (scea.AddedItems.Count > 0 && scea.AddedItems[0] is TileLayoutOrder)
            {
                this.xamTileManager1.NormalModeSettings.TileLayoutOrder = (TileLayoutOrder)scea.AddedItems[0];
            }
        }
    }
}