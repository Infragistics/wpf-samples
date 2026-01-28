using IGBusyIndicator.Resources;
using Infragistics.Controls.Schedules;
using System;

namespace IGBusyIndicator.Samples.DataProviders
{
    public class ProjectDataHelper
    {
        public static Project GenerateProjectData()
        {
            // Create a project
            var project = new Project();

            var rootTask = new ProjectTask
                                       {
                                           TaskName = BusyIndicatorStrings.MainSummaryTask,
                                           IsManual = false
                                       };

            project.RootTask.TaskName = BusyIndicatorStrings.ProjectSummaryTask;
            project.RootTask.Tasks.Add(rootTask);

            // Dates are saved in UTC in xamGantt
            DateTime startTime = DateTime.Today.ToUniversalTime();

            // Add tasks to the root task
            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.PlanningTask,
                                       IsManual = false,
                                       Start = startTime,
                                       Duration = TimeSpan.FromHours(8),
                                       Notes = BusyIndicatorStrings.PlanningNote
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.DocumentTask,
                                       IsManual = false,
                                       Start = startTime,
                                       ManualDuration = ProjectDuration.FromFormatUnits(2, ProjectDurationFormat.Days),
                                       ConstraintDate = DateTime.Today.AddDays(3).ToUniversalTime(),
                                       ConstraintType = ProjectTaskConstraintType.FinishNoLaterThan,
                                       Notes = BusyIndicatorStrings.DocumentingNote
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.CreateApplicationTask,
                                       IsManual = false,
                                       Start = startTime,
                                       ManualDuration = ProjectDuration.FromFormatUnits(3, ProjectDurationFormat.Days),
                                       ConstraintType = ProjectTaskConstraintType.MustStartOn,
                                       ConstraintDate = DateTime.Today.AddDays(2).ToUniversalTime(),
                                       IsEstimated = true,
                                       Notes = BusyIndicatorStrings.DemoNotes
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.CollectFeedbackTask,
                                       IsManual = false,
                                       Start = startTime,
                                       ManualDuration = ProjectDuration.FromFormatUnits(1, ProjectDurationFormat.Days),
                                       IsEstimated = true,
                                       Notes = BusyIndicatorStrings.CollectFeedbackNote
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.DesignTask,
                                       IsManual = false,
                                       Start = startTime,
                                       ManualDuration = ProjectDuration.FromFormatUnits(5, ProjectDurationFormat.Days),
                                       Notes = BusyIndicatorStrings.DesignNote
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.DevelopmentTask,
                                       IsManual = true,
                                       ManualStart = new ManualDateTime(BusyIndicatorStrings.FreeText_StillInDiscussion),
                                       ManualDuration = ProjectDuration.FromFormatUnits(10, ProjectDurationFormat.Days),
                                       ManualFinish = new ManualDateTime(BusyIndicatorStrings.FreeText_NotDefinedYet),
                                       Notes = BusyIndicatorStrings.DevelopmentNote
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.TestingTask,
                                       IsManual = true,
                                       Start = startTime,
                                       ManualDuration = ProjectDuration.FromFormatUnits(7, ProjectDurationFormat.Days),
                                       Notes = BusyIndicatorStrings.TestingNote
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.BugFixingTask,
                                       IsManual = true,
                                       Start = startTime,
                                       ManualDuration = ProjectDuration.FromFormatUnits(4, ProjectDurationFormat.Days),
                                       Notes = BusyIndicatorStrings.FixingNote
                                   });

            rootTask.Tasks.Add(new ProjectTask
                                   {
                                       TaskName = BusyIndicatorStrings.ProjectComplete,
                                       IsManual = false,
                                       Start = startTime,
                                       IsMilestone = true
                                   });

            return project;
        }
    }
}
