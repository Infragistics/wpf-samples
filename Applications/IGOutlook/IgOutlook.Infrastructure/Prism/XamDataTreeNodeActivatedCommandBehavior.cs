using Infragistics.Controls.Menus;
using Prism.Interactivity;
using System;

namespace IgOutlook.Infrastructure.Prism
{
    public class XamDataTreeNodeActivatedCommandBehavior : CommandBehaviorBase<XamDataTree>
    {
        public XamDataTreeNodeActivatedCommandBehavior(XamDataTree tree)
            : base(tree)
        {
            tree.ActiveNodeChanged += TreeActive_NodeChanged;
        }

        void TreeActive_NodeChanged(object sender, ActiveNodeChangedEventArgs e)
        {
            if (e.NewActiveTreeNode == null)
                return;

            var param = e.NewActiveTreeNode.Data as INavigationItem;
            if (param != null)
                CommandParameter = param.NavigationPath;
            else
                CommandParameter = String.Empty;

            ExecuteCommand(CommandParameter);
        }
    }
}
