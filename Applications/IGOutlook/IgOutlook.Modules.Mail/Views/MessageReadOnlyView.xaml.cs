using IgOutlook.Infrastructure;
using IgOutlook.Modules.Mail.Menus;

namespace IgOutlook.Modules.Mail.Views
{
    [RibbonTab(typeof(MessageReadOnlyHomeTab))]
    public partial class MessageReadOnlyView
    {
        public MessageReadOnlyView(MessageReadOnlyViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
