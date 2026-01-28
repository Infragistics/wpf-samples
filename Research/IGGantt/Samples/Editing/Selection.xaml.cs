using System;
using System.Windows;
using System.Windows.Controls;
using IGGantt.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Editing
{
    public partial class Selection : SampleContainer
    {
        public Selection()
        {
            InitializeComponent();
        }

        private void gantt_SelectedCellsChanged(object sender, GanttGridSelectionChangedEventArgs<GanttGridCell> e)
        {
            PrintLog("SelectedCellsChanged");

            var selectedCells = e.NewSelectedItems;
            PrintLog(GanttStrings.SelectedCellsCount + " " + selectedCells.Count);
            foreach (GanttGridCell cell in selectedCells)
            {
                PrintLog(GanttStrings.TaskID + " " + cell.Row.Task.Id);
            }
        }
        private void gantt_SelectedColumnsChanged(object sender, GanttGridSelectionChangedEventArgs<GanttGridColumn> e)
        {
            PrintLog("SelectedColumnsChanged");

            var selectedColumns = e.NewSelectedItems;
            PrintLog(GanttStrings.SelectedColumnsCount + " " + selectedColumns.Count);
            foreach (GanttGridColumn column in selectedColumns)
            { 
                PrintLog(GanttStrings.ColumnKey + " " + column.Key);
            }
        }

        private void gantt_SelectedRowsChanged(object sender, GanttGridSelectionChangedEventArgs<GanttGridRow> e)
        {
            PrintLog("SelectedRowsChanged");

            var selectedRows = e.NewSelectedItems;
            PrintLog(GanttStrings.SelectedRowsCount + " " + selectedRows.Count);
            foreach (GanttGridRow row in selectedRows)
            {
                PrintLog(GanttStrings.TaskName + " " + row.Task.TaskName);
            }   
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
    }
}
