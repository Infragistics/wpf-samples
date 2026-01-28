using System.Windows;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGDataTree.Samples.Editing
{
    public partial class SelectedDataItems : SampleContainer
    {
        public SelectedDataItems()
        {
            InitializeComponent();
        }

        private void DataTree_OnNodeLayoutAssigned(object sender, NodeLayoutAssignedEventArgs e)
        {
            if (e.Level == 0)
            {
                e.NodeLayout.Tree.Nodes[0].IsSelected = true;
            }
        }

        private void CheckBox_Insurance_OnChecked(object sender, RoutedEventArgs e)
        {
            var employeeLayout = DataTree.NodeLayouts["EmployeeLayout"];

            if (employeeLayout != null)
            {
                employeeLayout.IsSelectedMemberPath = "HasHealthInsurance";
                foreach (var node in employeeLayout.Tree.Nodes)
                {
                    node.IsExpanded = true;
                }
            }
        }

        private void CheckBox_Insurance_OnUnchecked(object sender, RoutedEventArgs e)
        {
            DataTree.NodeLayouts["EmployeeLayout"].IsSelectedMemberPath = null;
            DataTree.SelectedDataItems = null;
        }
    }
}
