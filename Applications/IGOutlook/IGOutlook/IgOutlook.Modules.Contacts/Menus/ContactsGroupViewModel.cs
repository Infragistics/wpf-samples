using IgOutlook.Infrastructure;
using IgOutlook.Modules.Contacts.Resources;
using System.Collections.ObjectModel;

namespace IgOutlook.Modules.Contacts.Menus
{
    public class ContactsGroupViewModel : ViewModelBase
    {
        private ObservableCollection<NavigationItem> _items;
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public ContactsGroupViewModel()
        {
            GenerateMenu();
        }

        private void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();

            var root = new NavigationItem() { Caption = ResourceStrings.ContactsGroup_MyContacts_Text, NavigationPath = "ContactsView", CanNavigate = false };
            root.Items.Add(new NavigationItem() { Caption = ResourceStrings.ContactsGroup_Contacts_Text, NavigationPath = "ContactsView" });

            Items.Add(root);
        }

    }
}
