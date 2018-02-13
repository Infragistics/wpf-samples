using Infragistics.Controls.Menus;
using Prism.Interactivity;
using System.Linq;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDataTreeNodeSelectedCommandBehavior : CommandBehaviorBase<XamDataTree>
    {
        public XamDataTreeNodeSelectedCommandBehavior(XamDataTree tree)
            : base(tree)
        {
            tree.SelectedNodesCollectionChanged += tree_SelectedNodesCollectionChanged;
        }

        void tree_SelectedNodesCollectionChanged(object sender, NodeSelectionEventArgs e)
        {
            if (e.CurrentSelectedNodes.Count > e.OriginalSelectedNodes.Count)
            {
                var added = e.CurrentSelectedNodes.Select(n => n.Data).Except(e.OriginalSelectedNodes.Select(n => n.Data));

                if (added.Count() > 0)
                {
                    CommandParameter = added.First();

                    ExecuteCommand(CommandParameter);
                }
            }
        }
    }
}
