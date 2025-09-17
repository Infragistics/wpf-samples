using System;
using System.Collections.ObjectModel;
using IGGantt.Resources;
using IGGantt.Samples.Models;
using Infragistics.Controls.Schedules;

namespace IGGantt.Samples.DataSource
{
    public class ProjectDataHelper
    {
        public static Project GenerateProjectData()
        {
            // Create a project
            Project project = new Project();

            ProjectTask rootTask = new ProjectTask
            {
                TaskName = GanttStrings.MainSummaryTask,
                IsManual = false
            };

            project.RootTask.TaskName = GanttStrings.ProjectSummaryTask;
            project.RootTask.Tasks.Add(rootTask);

            // Dates are saved in UTC in xamGantt
            DateTime startTime = DateTime.Today.ToUniversalTime();
            
            // Add tasks to the root task
            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.PlanningTask,
                IsManual = false,
                Start = startTime,
                Duration = TimeSpan.FromHours(8),
                Notes = GanttStrings.PlanningNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.DocumentTask,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(2,ProjectDurationFormat.Days),      
                ConstraintDate = DateTime.Today.AddDays(3).ToUniversalTime(),
                ConstraintType = ProjectTaskConstraintType.FinishNoLaterThan,
                Notes = GanttStrings.DocumentingNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.CreateApplicationTask,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(3, ProjectDurationFormat.Days),
                ConstraintType = ProjectTaskConstraintType.MustStartOn,
                ConstraintDate = DateTime.Today.AddDays(2).ToUniversalTime(),
                IsEstimated = true,
                Notes = GanttStrings.DemoNotes
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.CollectFeedbackTask,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(1, ProjectDurationFormat.Days),
                IsEstimated = true,
                Notes = GanttStrings.CollectFeedbackNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.DesignTask,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(5, ProjectDurationFormat.Days),
                Notes = GanttStrings.DesignNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.DevelopmentTask,
                IsManual = true,
                ManualStart = new ManualDateTime(GanttStrings.FreeText_StillInDiscussion),              
                ManualDuration = ProjectDuration.FromFormatUnits(10, ProjectDurationFormat.Days),
                ManualFinish = new ManualDateTime(GanttStrings.FreeText_NotDefinedYet),
                Notes = GanttStrings.DevelopmentNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.TestingTask,
                IsManual = true,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(7, ProjectDurationFormat.Days),
                Notes = GanttStrings.TestingNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.BugFixingTask,
                IsManual = true,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(4, ProjectDurationFormat.Days),
                Notes = GanttStrings.FixingNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.ProjectComplete,
                IsManual = false,
                Start = startTime,
                IsMilestone = true
            });
            
            return project;
        }
        public static Project GenerateProjectDataWithoutConstraints()
        {
            // Create a project
            Project project = new Project();

            ProjectTask rootTask = new ProjectTask
            {
                TaskName = GanttStrings.ProjectSummaryTask,
                IsManual = false
            };

            project.RootTask.Tasks.Add(rootTask);

            DateTime startTime = DateTime.Today.ToUniversalTime();

            // Add tasks to the root task
            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.PlanningTask,
                IsManual = false,
                Start = startTime,
                Duration = TimeSpan.FromHours(8),
                Notes = GanttStrings.PlanningNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.DocumentTask,
                IsManual = false,
                ManualDuration = ProjectDuration.FromFormatUnits(2, ProjectDurationFormat.Days),
                Notes = GanttStrings.DocumentingNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.CreateApplicationTask,
                IsManual = false,
                ManualDuration = ProjectDuration.FromFormatUnits(3, ProjectDurationFormat.Days),
                IsEstimated = true,
                Notes = GanttStrings.DemoNotes
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.CollectFeedbackTask,
                IsManual = false,
                ManualDuration = ProjectDuration.FromFormatUnits(1, ProjectDurationFormat.Days),
                IsEstimated = true,
                Notes = GanttStrings.CollectFeedbackNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.DesignTask,
                IsManual = false,
                ManualDuration = ProjectDuration.FromFormatUnits(5, ProjectDurationFormat.Days),
                Notes = GanttStrings.DesignNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.DevelopmentTask,
                IsManual = false,
                ManualDuration = ProjectDuration.FromFormatUnits(10, ProjectDurationFormat.Days),
                Notes = GanttStrings.DevelopmentNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.TestingTask,
                IsManual = false,
                ManualDuration = ProjectDuration.FromFormatUnits(7, ProjectDurationFormat.Days),
                Notes = GanttStrings.TestingNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.BugFixingTask,
                IsManual = false,
                ManualDuration = ProjectDuration.FromFormatUnits(4, ProjectDurationFormat.Days),
                Notes = GanttStrings.FixingNote
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.ProjectComplete,
                IsManual = false,
                IsMilestone = true
            });

            return project;
        }
        public static void GenerateTasksDependencies(ProjectTask rootTask)
        {
            ProjectTask summaryTask = rootTask.Tasks[0];

            // Set dependencies
            // Setting dependency task id
            summaryTask.Tasks[0].SuccessorsIdText = summaryTask.Tasks[1].Id.ToString();
            summaryTask.Tasks[1].SuccessorsIdText = summaryTask.Tasks[2].Id.ToString();

            // Setting text for dependency 
            summaryTask.Tasks[3].PredecessorsIdText = "4FS-2 days";
            summaryTask.Tasks[4].PredecessorsIdText = "5SS";

            // Adding dependency in the Predecessors collection
            summaryTask.Tasks[5].Predecessors.Add(summaryTask.Tasks[4], ProjectTaskLinkType.StartToStart, ProjectDuration.FromFormatUnits(2, ProjectDurationFormat.Days));
            summaryTask.Tasks[6].Predecessors.Add(summaryTask.Tasks[5], ProjectTaskLinkType.FinishToStart, ProjectDuration.FromFormatUnits(-5, ProjectDurationFormat.Days));
            summaryTask.Tasks[7].Predecessors.Add(summaryTask.Tasks[6], ProjectTaskLinkType.StartToStart, ProjectDuration.FromFormatUnits(1, ProjectDurationFormat.Days));
            summaryTask.Tasks[8].Predecessors.Add(summaryTask.Tasks[7], ProjectTaskLinkType.FinishToStart, ProjectDuration.FromFormatUnits(0, ProjectDurationFormat.Days));
        }

        public static ObservableCollection<HumanResource> GenerateHumanResources()
        {
            ObservableCollection<HumanResource> hrCollection = new ObservableCollection<HumanResource>();

            hrCollection.Add(new HumanResource { Name = GanttStrings.pdhLindaHamilton, PersonalID = "lhamilton-sales" });
            hrCollection.Add(new HumanResource { Name = GanttStrings.pdhJohnSmith, PersonalID = "jsmith-dev" });
            hrCollection.Add(new HumanResource { Name = GanttStrings.pdhPeterGreen, PersonalID = "pgreen-dev" });
            hrCollection.Add(new HumanResource { Name = GanttStrings.pdhJuliePeterson, PersonalID = "jpeterson-marketing" });
            hrCollection.Add(new HumanResource { Name = GanttStrings.pdhAlexPetrov, PersonalID = "alexpetrov-design" });

            return hrCollection;
        }

        public static Project GenerateLinkedTasks(ProjectDuration[] durations)
        {
            Project project = new Project();
            ProjectTask root = new ProjectTask { TaskName = GanttStrings.Summary, IsManual = false };

            project.RootTask.Tasks.Add(root);
            DateTime startTime = DateTime.Today.ToUniversalTime();

            // Creating the tasks, setting their duration according the provided sample array
            for (int i = 0; i < durations.Length; i++)
            {
                root.Tasks.Add(new ProjectTask
                {
                    TaskName = String.Format("{0} {1:00}", GanttStrings.Task_TabHeader, i.ToString()),
                    IsManual = false,
                    ManualDuration = durations[i]
                });
            }

            // Making links between some of tasks.
            for (int i = 1; i < durations.Length; i++)
            {
                root.Tasks[i].Predecessors.Add(root.Tasks[i - 1]);
            }

            return project;
        }

        public static Project GenerateCriticalData()
        {
            Project project = new Project();

            ProjectSettings settings = new ProjectSettings();

            project.Settings = settings;

            ProjectTask rootTask = project.RootTask;
            rootTask.TaskName = GanttStrings.ProjectSummaryTask;

            // Dates are saved in UTC in xamGantt
            DateTime startTime = DateTime.Today.ToUniversalTime();

            // Add tasks to the root task
            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.task1,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(4, ProjectDurationFormat.Days)
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.task2,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(2, ProjectDurationFormat.Days),
                IsEstimated = true
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.task3,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(4, ProjectDurationFormat.Days),
            });


            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.task4,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(4, ProjectDurationFormat.Days),
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.task5,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(2, ProjectDurationFormat.Days),
                IsEstimated = true
            });

            rootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.task6,
                IsManual = false,
                Start = startTime,
                ManualDuration = ProjectDuration.FromFormatUnits(10, ProjectDurationFormat.Days),
                IsEstimated = true
            });

            return project;
        }
    }
}