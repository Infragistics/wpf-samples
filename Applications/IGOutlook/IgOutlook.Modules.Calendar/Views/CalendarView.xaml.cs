using IgOutlook.Infrastructure;
using IgOutlook.Modules.Calendar.Menus;
using System.Windows.Controls;

namespace IgOutlook.Modules.Calendar.Views
{

    [RibbonTab(typeof(HomeTab))]
    public partial class CalendarView : IgOutlook.Infrastructure.ViewBase
    {
        public CalendarView(CalendarViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }

    }

}
