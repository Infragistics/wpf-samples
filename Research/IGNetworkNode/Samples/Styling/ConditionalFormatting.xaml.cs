using Infragistics.Samples.Shared.Models;
namespace IGNetworkNode.Samples.Styling
{
    public partial class ConditionalFormatting : Infragistics.Samples.Framework.SampleContainer
    {
        private readonly System.Windows.Style RootNodeStyle;
        private readonly System.Windows.Style CompanyNodeStyle;
        private readonly System.Windows.Style SpeakerNodeStyle;
        private readonly System.Windows.Style AttendeeNodeStyle;

        public ConditionalFormatting()
        {
            InitializeComponent();
            RootNodeStyle = this.Resources["RootNodeStyle"] as System.Windows.Style;
            CompanyNodeStyle = this.Resources["CompanyNodeStyle"] as System.Windows.Style;
            SpeakerNodeStyle = this.Resources["SpeakerNodeStyle"] as System.Windows.Style;
            AttendeeNodeStyle = this.Resources["AttendeeNodeStyle"] as System.Windows.Style;
        }

        private void xnn_NodeControlAttachedEvent(object sender, Infragistics.Controls.Maps.NetworkNodeEventArgs e)
        {
            NodeModel n = e.NodeControl.DataContext as NodeModel;
            if (n != null)
                if (n.NodeType == NodeModel.NodeModelType.Convention)
                {
                    e.NodeControl.Style = RootNodeStyle;
                }
                else if (n.NodeType == NodeModel.NodeModelType.Company)
                {
                    e.NodeControl.Style = CompanyNodeStyle;
                }
                else if (n.NodeType == NodeModel.NodeModelType.Speaker)
                {
                    e.NodeControl.Style = SpeakerNodeStyle;
                }
                else if (n.NodeType == NodeModel.NodeModelType.Attendee)
                {
                    e.NodeControl.Style = AttendeeNodeStyle;
                }
                else { }
        }
    }
}
