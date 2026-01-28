using System;
using IGGantt.Samples.ViewModel;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class CriticalPath : SampleContainer
    {
        private bool isInitial = true;
        public CriticalPath()
        {
            InitializeComponent();
            bool LoadCriticalData = true;
            this.DataContext = new SimpleProjectViewModel(LoadCriticalData);

            Loaded += new System.Windows.RoutedEventHandler(CriticalPath_Loaded);
        }

        void CriticalPath_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (isInitial)
            { 
                SetDependecies();
                isInitial = false;
            }
        }

        private void SetDependecies()
        {
            ProjectTask root = this.gantt.Project.RootTask;

            root.Tasks[0].Successors.Add(root.Tasks[2]);
            root.Tasks[2].Successors.Add(root.Tasks[1]);
            root.Tasks[2].Successors.Add(root.Tasks[3]);

            root.Tasks[4].Successors.Add(root.Tasks[2]);
        }

        private void Input_CriticalLimit_ValueChanged(object sender, EventArgs e)
        {
            if (this.Input_CriticalLimit.Value == null)
            {
                return;
            }

            this.gantt.Project.Settings.CriticalSlackLimit = int.Parse(this.Input_CriticalLimit.Value.ToString());
        }

        private void Chb_CalculateMultipleCritical_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.gantt.Project.Settings.ShouldCalculateMultipleCriticalPaths = true;
        }

        private void Chb_CalculateMultipleCritical_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.gantt.Project.Settings.ShouldCalculateMultipleCriticalPaths = false;
        }
    }
}