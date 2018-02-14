using IgOutlook.Infrastructure;
using IgOutlook.Modules.Calendar.Menus;
using System.Windows.Controls;

namespace IgOutlook.Modules.Calendar.Views
{
    [RibbonTab(typeof(MeetingHomeTab))]
    [RibbonTab(typeof(FormatTextTab))]
    public partial class MeetingView : IgOutlook.Infrastructure.ViewBase, ISupportRichText
    {
        public MeetingView(MeetingViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

        public Infragistics.Controls.Editors.XamRichTextEditor RichTextEditor
        {
            get { return _richTextEditor; }
            set { throw new System.NotSupportedException(); }
        }
    }
}
