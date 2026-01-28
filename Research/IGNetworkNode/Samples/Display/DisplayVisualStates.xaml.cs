using System; 
using System.Windows;
using System.Windows.Controls;
using IGNetworkNode.Resources;
using Infragistics.Controls.Maps;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models; 
using Infragistics.Themes;

namespace IGNetworkNode.Samples.Display
{
    public partial class DisplayVisualStates : SampleContainer
    {
        private NodeModel nm;       
        public DisplayVisualStates()
        {
            InitializeComponent(); 
        }

        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        // Disabled
        private void cb_NormalState_Checked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                node.IsEnabled = false;
                this.cb_FocusState.IsChecked = false;
                this.cb_SelectState.IsChecked = false;
                this.cb_EditState.IsChecked = false;
            }
        }

        // Enable
        private void cb_NormalState_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                node.IsEnabled = true;
            }
        }

        // Focused
        private void cb_FocusState_Checked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                if (node.IsEnabled)
                {
                    nm = node.Data as NodeModel;
                    if (nm.Label == "RM")
                    {
                        node.Control.Focus();
                    }
                }
                else
                {
                    MessageBox.Show(NetworkNodeStrings.XNN_CannotGetFocus);
                    this.cb_FocusState.IsChecked = false;
                    break;
                }
            }
        }

        // Unfocused
        private void cb_FocusState_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                nm = node.Data as NodeModel;
                if (nm.Label == "RM")
                {
                    xnn.SelectedNodes.Clear();
                    xnn.ActiveNode = null;
                }
            }
        }

        // Selected
        private void cb_SelectState_Checked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                nm = node.Data as NodeModel;
                if (nm.Label == "RM")
                {
                    node.IsSelected = true;
                }
            }
        }

        // Unselected
        private void cb_SelectState_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                nm = node.Data as NodeModel;
                if (nm.Label == "RM")
                {
                    node.IsSelected = false;
                }
            }
        }

        // Editing
        private void cb_EditState_Checked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                nm = node.Data as NodeModel;
                if (nm.Label == "RM")
                {
                    node.IsEditing = true;
                }
            }
        }

        // Not Editing
        private void cb_EditState_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                nm = node.Data as NodeModel;
                if (nm.Label == "RM")
                {
                    node.IsEditing = false;
                }
            }
        }

        // Select All nodes
        private void btn_Select_All_Click(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                xnn.SelectedNodes.Add(node);
            }
        }

        // Editting All nodes
        private void btn_Edit_All_Click(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                node.IsEditing = true;
            }
        }

        // Clear All selections
        private void btn_ClearSelection_Click(object sender, RoutedEventArgs e)
        {
            foreach (NetworkNodeNode node in xnn.Nodes)
            {
                node.IsEditing = false;
                node.IsSelected = false;
                this.cb_FocusState.IsChecked = false;
                this.cb_SelectState.IsChecked = false;
                this.cb_EditState.IsChecked = false;
            }
        }

    }
}
