using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class TaskProgress : SampleContainer
    {
        public TaskProgress()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton button = sender as RadioButton;
            int percent =  int.Parse(button.Tag.ToString());

            if (this.gantt.ActiveRow.HasValue)
            {
                this.gantt.ActiveRow.Value.Task.PercentComplete = percent; 
            }
        }
    }
}
