using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Display
{
    public partial class MonthViewGroupsResizing : SampleContainer
    {
        public MonthViewGroupsResizing()
        {
            InitializeComponent();
            this.dataManager.ColorScheme = new IGColorScheme();
        }

        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (monthView != null)
            {
                monthView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
            }
        }
    }
}
