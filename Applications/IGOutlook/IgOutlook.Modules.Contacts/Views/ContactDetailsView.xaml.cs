using IgOutlook.Infrastructure;
using IgOutlook.Modules.Contacts.Menus;

namespace IgOutlook.Modules.Contacts.Views
{
    [RibbonTab(typeof(ContactHomeTab))]
    public partial class ContactDetailsView : IgOutlook.Infrastructure.ViewBase
    {
        public ContactDetailsView(ContactDetailsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
