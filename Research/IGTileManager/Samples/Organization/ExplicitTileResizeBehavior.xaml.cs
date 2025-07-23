using System.Windows.Controls;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGTileManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ExplicitTileResizeBehavior.xaml
    /// </summary>
    public partial class ExplicitTileResizeBehavior : SampleContainer
    {
        public ExplicitTileResizeBehavior()
        {
            InitializeComponent();
            this.Loaded += new System.Windows.RoutedEventHandler(ExplicitTileResizeBehavior_Loaded);
        }

        void ExplicitTileResizeBehavior_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.LayoutOrderCombo.ItemsSource = EnumValuesProvider.GetEnumValues<TileLayoutOrder>();
            this.LayoutOrderCombo.SelectedItem = this.xamTileManager1.NormalModeSettings.TileLayoutOrder;
            this.LayoutOrderCombo.SelectionChanged += new SelectionChangedEventHandler(LayoutOrderCombo_SelectionChanged);

            this.ExplicitLayoutCombo.ItemsSource = EnumValuesProvider.GetEnumValues<ExplicitLayoutTileSizeBehavior>();
            this.ExplicitLayoutCombo.SelectedItem = this.xamTileManager1.NormalModeSettings.ExplicitLayoutTileSizeBehavior;
            this.ExplicitLayoutCombo.SelectionChanged += new SelectionChangedEventHandler(ExplicitLayoutCombo_SelectionChanged);
        }

        void LayoutOrderCombo_SelectionChanged(object sender, SelectionChangedEventArgs scea)
        {
            if (scea.AddedItems.Count > 0 && scea.AddedItems[0] is TileLayoutOrder)
            {
                this.xamTileManager1.NormalModeSettings.TileLayoutOrder = (TileLayoutOrder)scea.AddedItems[0];
            }
        }

        void ExplicitLayoutCombo_SelectionChanged(object sender, SelectionChangedEventArgs scea)
        {
            if (scea.AddedItems.Count > 0 && scea.AddedItems[0] is ExplicitLayoutTileSizeBehavior)
            {
                this.xamTileManager1.NormalModeSettings.ExplicitLayoutTileSizeBehavior = (ExplicitLayoutTileSizeBehavior)scea.AddedItems[0];
            }
        }
    }
}
