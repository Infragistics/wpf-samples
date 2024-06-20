using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Organization
{
    /// <summary>
    /// Interaction logic for SchoolScheduleUsingDayView.xaml
    /// </summary>
    public partial class SchoolScheduleUsingDayView : SampleContainer
    {
        public SchoolScheduleUsingDayView()
        {
            InitializeComponent();

            dayView.WeekDisplayMode = WeekDisplayMode.WorkWeek;
        }
    }
}
