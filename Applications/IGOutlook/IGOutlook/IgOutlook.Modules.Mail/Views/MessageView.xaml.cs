using IgOutlook.Infrastructure;
using IgOutlook.Modules.Mail.Menus;

namespace IgOutlook.Modules.Mail.Views
{
    [RibbonTab(typeof(MessageHomeTab))]
    public partial class MessageView : ISupportRichText
    {
        public Infragistics.Controls.Editors.XamRichTextEditor RichTextEditor
        {
            get { return _richTextEditor; }
            set { throw new System.NotSupportedException(); }
        }

        public MessageView(MessageViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
