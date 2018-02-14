using Infragistics.Controls.Menus;
using Prism.Interactivity;
using System.Linq;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDataTreeNodeUnselectedCommandBehavior : CommandBehaviorBase<XamDataTree>
    {
        public XamDataTreeNodeUnselectedCommandBehavior(XamDataTree tree)
            : base(tree)
        {
            tree.SelectedNodesCollectionChanged += tree_SelectedNodesCollectionChanged;
        }

        void tree_SelectedNodesCollectionChanged(object sender, NodeSelectionEventArgs e)
        {
            if (e.CurrentSelectedNodes.Count < e.OriginalSelectedNodes.Count)
            {
                var removed = e.OriginalSelectedNodes.Select(n => n.Data).Except(e.CurrentSelectedNodes.Select(n => n.Data));
                
                if (removed.Count() > 0)
                {
                    CommandParameter = removed.First();

                    ExecuteCommand(CommandParameter);
                }
            }
        }
    }
}
