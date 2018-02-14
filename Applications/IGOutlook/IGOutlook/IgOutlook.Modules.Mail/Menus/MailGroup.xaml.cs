using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Prism;
using IgOutlook.Modules.Mail.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Windows.OutlookBar;
using System;
using System.Linq;

namespace IgOutlook.Modules.Mail.Menus
{
    public partial class MailGroup : OutlookBarGroup, IOutlookBarGroup
    {
        INavigationItem _selectedItem;

        public MailGroup(MailGroupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            _xamDataTree.Loaded += _xamDataTree_Loaded;
        }

        void _xamDataTree_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _xamDataTree.Loaded -= _xamDataTree_Loaded;

            var node = XamDataTreeSelectedUnselectedItemBehavior.FindTreeNodeFromDataItem(_xamDataTree, _selectedItem);

            if (node != null)
            {
                node.IsSelected = true;
            }
        }

        public string DefaultNavigationPath
        {
            get
            {
                var item = _xamDataTree.SelectionSettings.SelectedNodes[0] as XamDataTreeNode;

                if (item != null)
                    return ((INavigationItem)item.Data).NavigationPath;
                else
                {
                    var inboxItem = (DataContext as MailGroupViewModel).Items.SelectMany(x => x.Items).FirstOrDefault(x => x.Caption == ResourceStrings.MailGroup_Inbox_Text);
                    _selectedItem = inboxItem;
                    return inboxItem != null ? inboxItem.NavigationPath : String.Empty;

                }
            }
        }

        private void ActiveNodeChanging(object sender, ActiveNodeChangingEventArgs e)
        {
            _selectedItem = e.NewActiveTreeNode.Data as INavigationItem;
            if (_selectedItem != null && !_selectedItem.CanNavigate)
                e.Cancel = true;
        }
    }
}

