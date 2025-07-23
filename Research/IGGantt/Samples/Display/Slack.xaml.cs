using System;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class Slack : SampleContainer
    {
        public Slack()
        {
            InitializeComponent();
        }

        private void gantt_ActiveCellChanged(object sender, EventArgs e)
        {
            if (this.gantt.ActiveRow != null)
            { 
                txt_TaskName.Text = this.gantt.ActiveRow.Value.Task.TaskName;

                txt_StartSlack.Text = " " + this.gantt.ActiveRow.Value.Task.StartSlack.ToString();
                txt_FinishSlack.Text = " " + this.gantt.ActiveRow.Value.Task.FinishSlack.ToString();
                txt_FreeSlack.Text = " " + this.gantt.ActiveRow.Value.Task.FreeSlack.ToString();
                txt_TotalSlack.Text = " " + this.gantt.ActiveRow.Value.Task.TotalSlack.ToString();
            }
        }
    }
}
