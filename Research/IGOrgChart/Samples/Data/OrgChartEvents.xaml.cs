using System;
using System.Windows;
using IGOrgChart.Converters;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;

namespace IGOrgChart.Samples.Data
{
    public partial class OrgChartEvents : SampleContainer
    {
        private NodeDepartmentDataToDescriptionConverter converter = new NodeDepartmentDataToDescriptionConverter();

        public OrgChartEvents()
        {
            InitializeComponent();

            // This will prevent the Silverlight context menu from appearing when we do Right-Mouse down on a node.
            //Application.Current.RootVisual.MouseRightButtonDown += (s, eventArgs) => { eventArgs.Handled = true; };
        }

        private void OrgChart_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateOutput("Loaded.");
        }

        private void OrgChart_NodeControlAttachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            UpdateOutput("NodeControlAttached: " + converter.Convert(e.Node.Node.Data, null, null, null));
        }

        private void OrgChart_NodeLayoutAssigned(object sender, NodeLayoutAssignedEventArgs e)
        {
            UpdateOutput("NodeLayoutAssigned: " + e.NodeLayout.TargetTypeName);
        }

        private void OrgChart_NodeMouseLeftButtonDown(object sender, OrgChartNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseLeftButtonDown: " + converter.Convert(e.Node.Node.Data, null, null, null));
        }

        private void OrgChart_NodeMouseLeftButtonUp(object sender, OrgChartNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseLeftButtonUp: " + converter.Convert(e.Node.Node.Data, null, null, null));
        }

        private void OrgChart_NodeMouseMove(object sender, OrgChartNodeMouseEventArgs e)
        {
            UpdateOutput("NodeMouseMove: " + converter.Convert(e.Node.Node.Data, null, null, null));
        }

        private void OrgChart_NodeMouseRightButtonDown(object sender, OrgChartNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseRightButtonDown: " + converter.Convert(e.Node.Node.Data, null, null, null));           
        }

        private void OrgChart_NodeMouseRightButtonUp(object sender, OrgChartNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseRightButtonUp: " + converter.Convert(e.Node.Node.Data, null, null, null));
        }

        private void OrgChart_NodeMouseWheel(object sender, OrgChartNodeMouseWheelEventArgs e)
        {
            UpdateOutput("NodeMouseWheel: " + converter.Convert(e.Node.Node.Data, null, null, null));
        }

        private void OrgChart_SelectedNodesCollectionChanged(object sender, OrgChartNodeSelectionEventArgs e)
        {
            UpdateOutput("SelectedNodesCollectionChanged: " + e.CurrentSelectedNodes.Count);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }

        #region AuxiliaryMethods

        private void UpdateOutput(string text)
        {
            TextBlockOutput.Text += text + Environment.NewLine;
            ScrollViewerOutput.UpdateLayout();
            ScrollViewerOutput.ScrollToVerticalOffset(Double.MaxValue);
        }

        private void ClearOutput()
        {
            TextBlockOutput.Text = string.Empty;
        }

        #endregion
    }
}
