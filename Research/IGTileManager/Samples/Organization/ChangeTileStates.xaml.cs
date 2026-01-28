using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;

namespace IGTileManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ChangeTileStates.xaml
    /// </summary>
    public partial class ChangeTileStates : SampleContainer
    {
        public ChangeTileStates()
        {
            InitializeComponent();
            Infragistics.CommandSourceManager.RegisterCommandTarget(this.xamTileManager1.Items[0] as XamTile);
            Infragistics.CommandSourceManager.RegisterCommandTarget(this.xamTileManager1.Items[1] as XamTile);
        }
    }
}
