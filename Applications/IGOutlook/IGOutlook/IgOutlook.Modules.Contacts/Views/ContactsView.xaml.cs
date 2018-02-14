using IgOutlook.Business.Contacts;
using IgOutlook.Infrastructure;
using IgOutlook.Modules.Contacts.Menus;
using IgOutlook.Services;
using Prism.Regions;
using System.Windows.Media.Imaging;

namespace IgOutlook.Modules.Contacts.Views
{
    [RibbonTab(typeof(HomeTab))]
    public partial class ContactsView : IgOutlook.Infrastructure.ViewBase
    {
        public ContactsView(ContactsViewModel contactsViewModel, IContactService contactService)
            : base(contactsViewModel)
        {
            InitializeComponent();
        }
    }
}
