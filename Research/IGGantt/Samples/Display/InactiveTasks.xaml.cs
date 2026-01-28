using System;
using System.Windows;
using IGGantt.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class InactiveTasks : SampleContainer
    {
        private bool isInit = true;

        public InactiveTasks()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (isInit)
            { 
                this.AddInactiveTaskToProjectData();
                isInit = false;
            }
        }

        private void AddInactiveTaskToProjectData()
        {         
            // Add one inactive task
            this.gantt1.Project.RootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.ProjectVerification,
                IsActive = false,
                IsManual = false,
                Start = DateTime.Today.ToUniversalTime(),
                ManualDuration = ProjectDuration.FromFormatUnits(4, ProjectDurationFormat.Days),
            });
        }
    }
}
