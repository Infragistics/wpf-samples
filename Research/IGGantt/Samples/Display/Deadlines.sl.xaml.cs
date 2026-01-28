using System;
using System.Windows;
using IGGantt.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class Deadlines : SampleContainer
    {
        private ProjectTask taskWithDeadline;
        public Deadlines()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(Deadlines_Loaded);
        }

        void Deadlines_Loaded(object sender, RoutedEventArgs e)
        {
            if (taskWithDeadline == null)
            { 
            // Add a task with a deadline date specified
                taskWithDeadline = new ProjectTask();
                taskWithDeadline.TaskName = GanttStrings.MeetingsTask;
                taskWithDeadline.IsManual = true;
                taskWithDeadline.Start = DateTime.Today.ToUniversalTime();
                taskWithDeadline.Duration = TimeSpan.FromHours(2 * 8);
                taskWithDeadline.Deadline = DateTime.Today.AddHours(2 * 8).ToUniversalTime();

                this.gantt.Project.RootTask.Tasks.Add(taskWithDeadline);
            }
        }

        private void Btn_SetDeadline_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate != null)
            {
                if (this.gantt.ActiveRow.HasValue)
                {
                    GanttGridRow row = this.gantt.ActiveRow.Value;
                    row.Task.Deadline = datePicker.SelectedDate;
                }
            }
            else
            {
                MessageBox.Show(GanttStrings.DateIsNotSelected);
                this.datePicker.Focus();
            }
        }
    }
}
