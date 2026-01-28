using IGDiagram.Resources;
using Infragistics.Samples.Framework;
using System.Windows;
using Infragistics.Controls.Charts;

namespace IGDiagram.Samples.Data
{
    public partial class DiagramCommands : SampleContainer
    {
        public DiagramCommands()
        {
            InitializeComponent();
            this.Loaded += (s, e) => Diagram.ScaleToFit();
        }

        private void ToolsRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.Diagram != null)
            {
                if (sender.ToString() == DiagramStrings.DiagramCommands_Pointer)
                {
                    this.Diagram.Tool = DiagramTool.Pointer;
                }
                else
                {
                    this.Diagram.Tool = DiagramTool.Connector;
                }
            }
        }

        private void ConnectionsDisplayModeRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.Diagram != null)
            {
                if (sender.ToString() == DiagramStrings.DiagramCommands_Always)
                {
                    this.Diagram.ConnectionPointsDisplayMode = DiagramConnectionPointsDisplayMode.Always;
                }
                else if (sender.ToString() == DiagramStrings.DiagramCommands_Auto)
                {
                    this.Diagram.ConnectionPointsDisplayMode = DiagramConnectionPointsDisplayMode.Auto;
                }
                else if (sender.ToString() == DiagramStrings.DiagramCommands_MouseOver)
                {
                    this.Diagram.ConnectionPointsDisplayMode = DiagramConnectionPointsDisplayMode.MouseOver;
                }
                else if (sender.ToString() == DiagramStrings.DiagramCommands_Never)
                {
                    this.Diagram.ConnectionPointsDisplayMode = DiagramConnectionPointsDisplayMode.Never;
                }
            }
        }

        private void DragInteractionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (this.Diagram != null)
            {
                if (sender.ToString() == DiagramStrings.DiagramCommands_None)
                {
                    this.Diagram.DefaultDragInteraction = DiagramDragInteraction.None;
                }
                else if (sender.ToString() == DiagramStrings.DiagramCommands_Pan)
                {
                    this.Diagram.DefaultDragInteraction = DiagramDragInteraction.Pan;
                }
                else if (sender.ToString() == DiagramStrings.DiagramCommands_Select)
                {
                    this.Diagram.DefaultDragInteraction = DiagramDragInteraction.Select;
                }
                else if (sender.ToString() == DiagramStrings.DiagramCommands_Zoom)
                {
                    this.Diagram.DefaultDragInteraction = DiagramDragInteraction.Zoom;
                }
            }
        }
    }
}
