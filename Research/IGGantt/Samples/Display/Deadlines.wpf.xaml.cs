using System;
using System.Windows;
using IGGantt.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class Deadlines : SampleContainer
    {
        private bool isInitial = true;
        public Deadlines()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (isInitial == true) 
            {
                this.AddDeadlineToProjectData();
                isInitial = false;
            }
              
        }

        private void AddDeadlineToProjectData()
        {
            // Add a task with a deadline date specified
            this.gantt.Project.RootTask.Tasks.Add(new ProjectTask
            {
                TaskName = GanttStrings.MeetingsTask,
                IsManual = true,
                Start = DateTime.Today.ToUniversalTime(),
                Duration = TimeSpan.FromHours(2 * 8),
                Deadline = DateTime.Today.AddHours(2 * 8).ToUniversalTime()
            });
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
