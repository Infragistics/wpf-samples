using System;
using System.Windows;
using IGGantt.Resources;
using IGGantt.Samples.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGGantt.Samples.Editing
{
    public partial class TaskConstraints : SampleContainer
    {
        private bool isInit = true;
        public TaskConstraints()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;          
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (isInit)
            { 
                // Set task dependencies
                ProjectDataHelper.GenerateTasksDependencies(this.gantt.Project.RootTask); 

                // Initialize Project settings
                ProjectSettings settings = new ProjectSettings();

                // Tasks constraints are consider prior to tasks dependencies
                settings.AlwaysHonorTaskConstraintDates = true;

                this.gantt.Project.Settings = settings;     

                this.Cmb_ConstraintTypes.ItemsSource = EnumValuesProvider.GetEnumValues<ProjectTaskConstraintType>();

                isInit = false;
            }
        }    

        private void Btn_SetConstraint_Click(object sender, RoutedEventArgs e)
        {
            if (this.gantt.ActiveRow.HasValue)
            {
                var activeTask = this.gantt.ActiveRow.Value.Task;
         
                    if (!this.calendar.SelectedDate.HasValue)
                    {
                        MessageBox.Show(GanttStrings.ConstraintDateNotSelectedMsg);
                    }
                    else if (this.Cmb_ConstraintTypes.SelectedIndex == -1)
                    {
                        MessageBox.Show(GanttStrings.ConstraintNotSelectedMsg);
                    }
                    else
                    {
                        try
                        {
                            activeTask.ConstraintDate = this.calendar.SelectedDate.Value.ToUniversalTime();
                            activeTask.ConstraintType = (ProjectTaskConstraintType)this.Cmb_ConstraintTypes.SelectedIndex;
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show(exc.Message);
                        }
                    }
                }
            else
            {
                MessageBox.Show(GanttStrings.TaskNotSelectedMsg);
            }
        }
    }
}
