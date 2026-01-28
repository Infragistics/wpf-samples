using System;
using System.Windows;
using System.Windows.Controls;
using IGGantt.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Editing
{
    public partial class Events : SampleContainer
    {
        public Events()
        {
            InitializeComponent();
        }

        private void PrintLog(string msg)
        {
            string logMsg = "[" + DateTime.Now.ToString("HH:mm") + "] " + msg + "\n";

            ListBoxItem lstBoxItem = new ListBoxItem
            {
                Content = logMsg,
                FontSize = 10,
                Height = 20,
                FontWeight = FontWeights.Bold
            };

            ListBox_Output.Items.Insert(0, lstBoxItem);
        }

        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Output.Items.Count > 0)
            {
                ListBox_Output.Items.Clear();
            }
        }

        private void dataProvider_TaskExpanded(object sender, ProjectTaskExpandedEventArgs e)
        {
            this.PrintLog("TaskExpanded: " + e.Task.TaskName);
        }
        private void dataProvider_TaskCollapsed(object sender, ProjectTaskCollapsedEventArgs e)
        {
            this.PrintLog("TaskCollapsed: " + e.Task.TaskName);
        }

        private void dataProvider_TaskDeleting(object sender, ProjectTaskDeletingEventArgs e)
        {
            string msg = String.Format(GanttStrings.DeleteTaskQuestion, e.Task.TaskName);
            MessageBoxResult result = MessageBox.Show(msg, GanttStrings.DeletionConfirmation, MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.Cancel)
            {
                // Cancel task deletion
                e.Cancel = true;
            }
        }
        private void dataProvider_TaskDeleted(object sender, ProjectTaskDeletedEventArgs e)
        {
            this.PrintLog("TaskDeleted: " + e.Task.TaskName);
        }

        private void dataProvider_TaskIndented(object sender, ProjectTaskIndentedEventArgs e)
        {
            this.PrintLog("TaskIndented: " + e.Task.TaskName);
        }
        private void dataProvider_TaskOutdented(object sender, ProjectTaskOutdentedEventArgs e)
        {
            this.PrintLog("TaskOutdented: " + e.Task.TaskName);
        }
        
        private void dataProvider_TaskInserted(object sender, ProjectTaskInsertedEventArgs e)
        {
            string msg = string.Format(GanttStrings.InsertTaskString, e.Task.TaskName, e.RelativePosition, e.RelativeTask.TaskName);
            this.PrintLog("TaskInserted: " + msg);
        }

        private void gantt_ColumnVisibilityChanged(object sender, GanttGridColumnVisibilityChangedEventArgs e)
        {
            string msg = string.Format(GanttStrings.ForColumnString, e.ColumnDefinition.DisplayName);
            this.PrintLog("ColumnVisibilityChanged: " + msg);           
        }

        private void gantt_TaskBarDragCompleted(object sender, GanttTaskBarDragCompletedEventArgs e)
        {
            string msg = string.Format(GanttStrings.NewCalculatedDateString, e.Task.TaskName, e.Task.CalculatedFinish);
            this.PrintLog("TaskBarDragCompleted: " + msg);
        }
    }
}
