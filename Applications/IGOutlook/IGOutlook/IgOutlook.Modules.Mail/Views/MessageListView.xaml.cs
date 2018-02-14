using IgOutlook.Infrastructure;
using IgOutlook.Modules.Mail.Menus;

namespace IgOutlook.Modules.Mail.Views
{
    [RibbonTab(typeof(HomeTab))]
    public partial class MessageListView
    {
        public MessageListView(MessageListViewModel viewModel)
            : base (viewModel)
        {
            InitializeComponent();
        }      
    }
}
