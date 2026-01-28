using System;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Organization
{
    /// <summary>
    /// Interaction logic for ScheduleViewMultidayAppointments.xaml
    /// </summary>
    public partial class ScheduleViewMultidayAppointments : SampleContainer
    {
        public ScheduleViewMultidayAppointments()
        {
            InitializeComponent();

            DateTime dt = DateTime.Today;
            scheduleView.VisibleDates.Add(dt.AddDays(1));
            scheduleView.VisibleDates.Add(dt.AddDays(2));
        }
    }
}
