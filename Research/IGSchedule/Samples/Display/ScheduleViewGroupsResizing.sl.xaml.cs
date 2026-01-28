using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Display
{
    public partial class ScheduleViewGroupsResizing : SampleContainer
    {
        public ScheduleViewGroupsResizing()
        {
            InitializeComponent();
            this.dataManager.ColorScheme = new IGColorScheme();
            this.Loaded += new System.Windows.RoutedEventHandler(ScheduleHeaderArea_Loaded);
        }

        void ScheduleHeaderArea_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            scheduleView.CalendarDisplayMode = CalendarDisplayMode.Overlay;
        }

        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (scheduleView != null)
            {
                scheduleView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
            }
        }
    }
}
