using IgOutlook.Infrastructure;
using Infragistics.Controls.Menus;
using Infragistics.Windows.OutlookBar;

namespace IgOutlook.Modules.Contacts.Menus
{
    public partial class ContactsGroup : OutlookBarGroup, IOutlookBarGroup
    {
        INavigationItem _selectedItem;

        public ContactsGroup(ContactsGroupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        public string DefaultNavigationPath
        {
            get { return "ContactsView"; }
        }

        private void ActiveNodeChanging(object sender, ActiveNodeChangingEventArgs e)
        {
            var _selectedItem = e.NewActiveTreeNode.Data as INavigationItem;
            if (_selectedItem != null && !_selectedItem.CanNavigate)
                e.Cancel = true;
        }
    }
}
