using System;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Infragistics.Controls.Maps;
using IGNetworkNode.Resources;
using Infragistics.Samples.Shared.Models;

namespace IGNetworkNode.Samples.Editing
{
    public partial class Events : Infragistics.Samples.Framework.SampleContainer
    {
        private bool _keyIsDown;

        public Events()
        {
            InitializeComponent();
            _keyIsDown = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ClearOutput();
        }

        private void xnn_KeyDown(object sender, KeyEventArgs e)
        {
            if (!_keyIsDown) // prevent KeyDown spam in the event window
            {
                UpdateOutput("KeyDown: " + e.Key.ToString());
            }
            _keyIsDown = true;
        }

        private void xnn_KeyUp(object sender, KeyEventArgs e)
        {
            UpdateOutput("KeyUp: " + e.Key.ToString());
            _keyIsDown = false;
        }

        private void xnn_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateOutput(NetworkNodeStrings.Events_Loaded);
        }
        
        private void xnn_MouseEnter(object sender, MouseEventArgs e)
        {
            UpdateOutput("MouseEnter");
        }

        private void xnn_MouseLeave(object sender, MouseEventArgs e)
        {
            UpdateOutput("MouseLeave");
        }

        private void xnn_NodeControlAttachedEvent(object sender, Infragistics.Controls.Maps.NetworkNodeEventArgs e)
        {
            UpdateOutput(NetworkNodeStrings.Events_NodeAttached + ((NodeModel)e.NodeControl.Node.Data).Label);
        }

        private void xnn_NodeControlDetachedEvent(object sender, NetworkNodeEventArgs e)
        {
            UpdateOutput(NetworkNodeStrings.Events_NodeDetached);
        }

        private void xnn_NodeMouseLeftButtonDown(object sender, Infragistics.Controls.Maps.NetworkNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseLeftButtonDown: " + ((NodeModel)e.NodeControl.Node.Data).Label);
        }

        private void xnn_NodeMouseLeftButtonUp(object sender, Infragistics.Controls.Maps.NetworkNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseLeftButtonUp: " + ((NodeModel)e.NodeControl.Node.Data).Label);
        }

        private void xnn_NodeMouseRightButtonDown(object sender, Infragistics.Controls.Maps.NetworkNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseRightButtonDown: " + ((NodeModel)e.NodeControl.Node.Data).Label);
        }

        private void xnn_NodeMouseRightButtonUp(object sender, Infragistics.Controls.Maps.NetworkNodeClickEventArgs e)
        {
            UpdateOutput("NodeMouseRightButtonUp: " + ((NodeModel)e.NodeControl.Node.Data).Label);
        }
        
        private void xnn_SelectedNodesCollectionChanged(object sender, Infragistics.Controls.Maps.NetworkNodeSelectionEventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("SelectedNodesCollectionChanged:");
            foreach (NetworkNodeNode n in e.CurrentSelectedNodes)
            {
                sb.Append(" ");
                sb.Append(((NodeModel)n.Data).Label);
            }
            UpdateOutput(sb.ToString());
        }

        #region Auxiliary Methods
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

        private void xnn_NodeLayoutAssigned(object sender, NetworkNodeLayoutAssignedEventArgs e)
        {
            // Whenever an item from the data source is matched with a Node Layout object, 
            // the NodeLayoutAssigned event is raised. This allows to evaluate the item type and 
            // assign a different Node Layout object if desired.
            UpdateOutput("NodeLayoutAssigned: " + e.NodeLayout.TargetTypeName);
        }

        private void xnn_NodeMouseMove(object sender, NetworkNodeMouseEventArgs e)
        {
            UpdateOutput("NodeMouseMove: " + ((NodeModel)e.NodeControl.Node.Data).Label);
        }

        private void xnn_NodeMouseWheel(object sender, NetworkNodeMouseWheelEventArgs e)
        {
            UpdateOutput("NodeMouseWheel: " + ((NodeModel)e.NodeControl.Node.Data).Label);
        }
    }
}
