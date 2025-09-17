using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Display
{
    public partial class DayViewGroupsResizing : SampleContainer
    {
        public DayViewGroupsResizing()
        {
            InitializeComponent();
            this.dataManager.ColorScheme = new IGColorScheme();
        }

        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dayView != null)
            {
                dayView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
            }
        }
    }
}
