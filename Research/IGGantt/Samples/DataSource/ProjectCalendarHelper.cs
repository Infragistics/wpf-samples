using System;
using Infragistics;
using Infragistics.Controls.Schedules;
using IGGantt.Resources;

namespace IGGantt.Samples.DataSource
{
    public static class ProjectCalendarHelper
    {
        public static Project GenerateProjectCalendarWorkWeek(DayOfWeek dof, bool generateSampleTasks)
        {
            Project project = new Project();

            ProjectCalendar projectCalendar = new ProjectCalendar() { UniqueId = "pcww" };
            ScheduleDayOfWeek scheduledow = new ScheduleDayOfWeek();

            scheduledow.DaySettings = new DaySettings { IsWorkday = false };
            ScheduleDaysOfWeek daysOfWeek = new ScheduleDaysOfWeek();
            daysOfWeek[dof] = scheduledow;

            ProjectCalendarWorkWeek workWeek = new ProjectCalendarWorkWeek
            {
                DateRange = new DateRange(DateTime.Today, DateTime.Today.AddDays(7)),
                DaysOfWeek = daysOfWeek
            };

            projectCalendar.WorkWeeks.Add(workWeek);
            project.Calendars.Add(projectCalendar);
            project.CalendarId = "pcww";

            project.Start = DateTime.Today;

            if (generateSampleTasks)
            {
                AddSampleWorkingHours(project);
            }

            return project;
        }

        public static Project GenerateProjectCalendarException(DateTime date, bool generateSampleTasks)
        {
            Project project = new Project();
            ProjectCalendar projectCalendar = new ProjectCalendar() { UniqueId = "pce" };
            ProjectCalendarException pce = new ProjectCalendarException();
            pce.DateRange = new DateRange(date);
            pce.DaySettings = new DaySettings { IsWorkday = false };
            pce.Recurrence = new DateRecurrence { Count = 1, Frequency = DateRecurrenceFrequency.Daily };
            projectCalendar.Exceptions.Add(pce);
            project.Calendars.Add(projectCalendar);
            project.CalendarId = "pce";

            project.Start = DateTime.Today;

            if (generateSampleTasks)
            {
                AddSampleWorkingHours(project);
            }

            return project;
        }

        public static Project GenerateSampleProject()
        {
            Project project = new Project();

            project.Start = DateTime.Today;

            AddSampleWorkingHours(project);

            return project;
        }

        private static Project AddSampleWorkingHours(Project project)
        {
            ProjectTask root = new ProjectTask { TaskName = GanttStrings.Summary, IsManual = false }; // { TaskName = "Summary", IsManual = false };

            project.RootTask.Tasks.Add(root);

            for (int i = 0; i < 10; i++)
            {
                root.Tasks.Add(new ProjectTask
                {
                    TaskName = String.Format("{0} {1:00}", GanttStrings.Task_TabHeader, i.ToString()), // String.Format("Task {0:00}", i.ToString())
                    IsManual = false,
                    ManualDuration = ProjectDuration.FromFormatUnits(8.0, ProjectDurationFormat.Hours),
                    Notes = String.Format("{0} {1:00}",GanttStrings.Notes, i.ToString()) // String.Format("Notes {0:00}", i.ToString())
                });
            }

            for (int i = 1; i < 10; i++)
            {
                root.Tasks[i].Predecessors.Add(root.Tasks[i - 1]);
            }

            return project;
        }
    }
}
