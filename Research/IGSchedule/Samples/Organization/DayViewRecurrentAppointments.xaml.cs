using IGSchedule.DataSource;
using IGSchedule.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IGSchedule.Samples.Organization
{
    /// <summary>
    /// Interaction logic for DayViewRecurrentAppointments.xaml
    /// </summary>
    public partial class DayViewRecurrentAppointments : SampleContainer
    {
        ScheduleData data = null;
        public DayViewRecurrentAppointments()
        {
            InitializeComponent();

            dayView.WeekDisplayMode = WeekDisplayMode.Week;

            data = new ScheduleData();
            data.DataLoadingCompleted += new DataLoadingCompletedEventHandler(data_DataLoadingCompleted);
        }

        void data_DataLoadingCompleted(object sender, EventArgs e)
        {
            Appointment recAppointment = data.Appointments.FirstOrDefault(t => t.Id == "8");
            if (recAppointment != null)
            {
                DateRecurrence dateRecurrence = new DateRecurrence();
                var recuRules = new List<DateRecurrenceRuleBase>();
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Monday, 0));
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Wednesday, 0));
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Friday, 0));
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Sunday, 0));
                dateRecurrence.Frequency = DateRecurrenceFrequency.Weekly;
                dateRecurrence.Rules.AddRange(recuRules);
                recAppointment.Description += ScheduleStrings.Recurrent;
                recAppointment.Recurrence = dateRecurrence;
            }
            DataContext = data;
        }
    }
}
