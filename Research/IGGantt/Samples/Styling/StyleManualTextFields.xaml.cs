using System;
using System.Windows;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Styling
{
    public partial class StyleManualTextFields : SampleContainer
    {
        public StyleManualTextFields()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            // Set the numeric editor to the taskbar initial height
            var initHeight = this.gantt.TaskBarHeight.ToString();
            this.Input_TaskBarHeight.Text = initHeight;
        }

        private void Input_TaskBarHeight_ValueChanged(object sender, EventArgs e)
        {
            double result;

            if (this.Input_TaskBarHeight.Value == null)
            {
                return;
            }

            string inputValue = this.Input_TaskBarHeight.Value.ToString();
            
            if (double.TryParse(inputValue, out result))
            {
                this.gantt.TaskBarHeight = result;
            }
        }
    }
}
