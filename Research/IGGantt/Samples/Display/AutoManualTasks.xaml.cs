using System.Windows;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class AutoManualTasks : SampleContainer
    {
        private ProjectSettings settings;
        public AutoManualTasks()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.RBtn_ManualNewTask.IsChecked = true;
        }

        private void RBtn_ManualNewTask_Checked(object sender, RoutedEventArgs e)
        {
            if (this.gantt.Project != null)
            { 
                this.InitializeProjectSettings();
                this.gantt.Project.Settings.NewTasksAreManual = true;
            }
        }

        private void RBtn_AutoNewTask_Checked(object sender, RoutedEventArgs e)
        {
            this.InitializeProjectSettings();
            this.gantt.Project.Settings.NewTasksAreManual = false;          
        }

        private void InitializeProjectSettings()
        {
            if (settings == null)
            {
                settings = new ProjectSettings();
                this.gantt.Project.Settings = settings;
            }
        }
    }
}
