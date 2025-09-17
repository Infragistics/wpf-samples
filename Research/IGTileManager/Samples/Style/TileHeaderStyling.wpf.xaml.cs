using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGTileManager.Samples.Style
{
    /// <summary>
    /// Interaction logic for TileHeaderStyling.xaml
    /// </summary>
    public partial class TileHeaderStyling : SampleContainer
    {
        public TileHeaderStyling()
        {
            InitializeComponent();
            this.xamTileManager1.ItemsSource = NWindDataSource.GetTable(NWindTable.Customers, false).DefaultView;
        }
    }
}