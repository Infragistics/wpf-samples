using System;
using System.Windows;
using IGGantt.Resources;
using IGGantt.Samples.DataSource;
using IGGantt.Samples.ViewModel;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class MilestoneSummaryTasks : SampleContainer
    {
        private ProjectViewModel projectViewModel;
        private ProjectSettings settings;
        private bool isInit = true;

        public MilestoneSummaryTasks()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (isInit)
            { 
                this.projectViewModel = new ProjectViewModel();
                this.DataContext = this.projectViewModel.Project;

                this.Chb_ShowSummaries.IsChecked = true;

                isInit = false;
            }
        }

        private void Btn_CreateMilestone_Click(object sender, RoutedEventArgs e)
        {
            // Create a milestone 
            this.projectViewModel.Project.RootTask.Tasks.Add(new ProjectTask
            {
                IsActive = true,
                IsMilestone = true,
                TaskName = GanttStrings.NewMilestone,
                Start = DateTime.Today.ToUniversalTime(),
                Duration = TimeSpan.Zero
            });
        }

        private void Btn_CreateSummary_Click(object sender, RoutedEventArgs e)
        {
            ProjectTask task = new ProjectTask()
            {
                IsActive = true,
                IsManual = false,
                TaskName = GanttStrings.NewSummary,
                Start = DateTime.Today.ToUniversalTime(),
                Duration = TimeSpan.FromHours(8),
            };

            task.Tasks.Add(new ProjectTask()
            {
                TaskName = GanttStrings.NewTask,
                IsManual = true,
            });

            this.projectViewModel.Project.RootTask.Tasks.Add(task);
        }

        private void Chb_IsRootVisible_Checked(object sender, RoutedEventArgs e)
        {
            this.initProjectSettings();
            this.gantt.Project.Settings.IsRootTaskVisible = true;    
        }

        private void Chb_IsRootVisible_Unchecked(object sender, RoutedEventArgs e)
        {
            this.initProjectSettings();
            this.gantt.Project.Settings.IsRootTaskVisible = false;
        }

        private void initProjectSettings()
        {
            if (settings == null)
            {
                settings = new ProjectSettings();
                this.gantt.Project.Settings = settings;
            }
        }
    }
}
