using System.Windows;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGTree.Samples.Organization
{
    public partial class XamlBasedTree : SampleContainer
    {
        public XamlBasedTree()
        {
            InitializeComponent();
        }

        // Expand all child nodes OnTreeLoaded
        void OnTreeLoaded(object sender, RoutedEventArgs e)
        {
            XamTree tree = (XamTree)sender;
            this.SetNodeExpandedState(tree.XamTreeItems, true);
        }

        private void SetNodeExpandedState(XamTreeItemsCollection nodes, bool expandNode)
        {
            foreach (XamTreeItem item in nodes)
            {
                item.IsExpanded = expandNode;
                this.SetNodeExpandedState(item.XamTreeItems, expandNode);
            }
        }
    }
}