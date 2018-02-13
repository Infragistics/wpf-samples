using IgOutlook.Infrastructure;
using IgOutlook.Modules.Mail.Resources;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace IgOutlook.Modules.Mail.Menus
{
    public class MailGroupViewModel : ViewModelBase
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

        public MailGroupViewModel()
        {
            GenerateMenu();
        }

        private void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();

            var root = new NavigationItem() { Caption = ResourceStrings.MailGroup_PersonalFolders_Text, NavigationPath = "DefaultMailView", CanNavigate = false };
            root.Items.Add(new NavigationItem() { Caption = ResourceStrings.MailGroup_Inbox_Text, NavigationPath = CreateNavigationPath(FolderParameters.Inbox) });
            root.Items.Add(new NavigationItem() { Caption = ResourceStrings.MailGroup_Drafts_Text, NavigationPath = CreateNavigationPath(FolderParameters.Drafts) });
            root.Items.Add(new NavigationItem() { Caption = ResourceStrings.MailGroup_SentItems_Text, NavigationPath = CreateNavigationPath(FolderParameters.Sent) });
            root.Items.Add(new NavigationItem() { Caption = ResourceStrings.MailGroup_DeletedItems_Text, NavigationPath = CreateNavigationPath(FolderParameters.Deleted) });

            Items.Add(root);
        }

        private string CreateNavigationPath(string folder)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FolderParameters.FolderKey, folder);
            return "MessageListView" + parameters.ToString();
        }
    }
}
