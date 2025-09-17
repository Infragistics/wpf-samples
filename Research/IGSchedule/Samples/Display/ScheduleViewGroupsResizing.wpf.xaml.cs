using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Display
{
    /// <summary>
    /// Interaction logic for ScheduleViewGroupsResizing.xaml
    /// </summary>
    public partial class ScheduleViewGroupsResizing : SampleContainer
    {
        public ScheduleViewGroupsResizing()
        {
            InitializeComponent();
        }
        
        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            scheduleView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
        }
    }
}
