using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Display
{
    /// <summary>
    /// Interaction logic for DayViewGroupsResizing.xaml
    /// </summary>
    public partial class DayViewGroupsResizing : SampleContainer
    {
        public DayViewGroupsResizing()
        {
            InitializeComponent();
        }
        
        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dayView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
        }
    }
}
