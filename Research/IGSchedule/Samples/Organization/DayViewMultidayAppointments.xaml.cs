using System;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Organization
{
    /// <summary>
    /// Interaction logic for DayViewMultidayAppointments.xaml
    /// </summary>
    public partial class DayViewMultidayAppointments : SampleContainer
    {
        public DayViewMultidayAppointments()
        {
            InitializeComponent();

            DateTime dt = DateTime.Today;
            dayView.VisibleDates.Add(dt.AddDays(1));
            dayView.VisibleDates.Add(dt.AddDays(2));
        }
    }
}
