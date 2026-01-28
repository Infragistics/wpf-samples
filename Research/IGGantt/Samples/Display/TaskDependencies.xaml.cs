using System.Windows;
using System.Windows.Controls;
using IGGantt.Samples.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics;

namespace IGGantt.Samples.Display
{
    public partial class TaskDependencies : SampleContainer
    {
        bool isInitial = true;
        public TaskDependencies()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (isInitial)
            { 
                // Generate tasks dependencies
                ProjectDataHelper.GenerateTasksDependencies(this.gantt.Project.RootTask);

                Cmb_DepdLine.ItemsSource = EnumValuesProvider.GetEnumValues<ProjectTaskDependencyLineType>();
                Cmb_DepdLine.SelectedIndex = 2;
                Cmb_DepdLine.SelectionChanged += new SelectionChangedEventHandler(Cmb_DepdLine_SelectionChanged);

                isInitial = false;
            }
        }
        
        private void Btn_ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            this.gantt.ViewSettings.Timescale.Scale = 100;

            this.Btn_ZoomOut.IsEnabled = false;
            this.Btn_ZoomIn.IsEnabled = true;

            this.gantt.VisibleDateRange = new DateRange(this.gantt.Project.Start, this.gantt.Project.Finish);
        }

        private void Btn_ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            this.gantt.ViewSettings.Timescale.Scale = 200;

            this.Btn_ZoomOut.IsEnabled = true;
            this.Btn_ZoomIn.IsEnabled = false;

            this.gantt.VisibleDateRange = new DateRange(this.gantt.Project.Start, this.gantt.Project.Finish);

        }

        private void Cmb_DepdLine_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            this.gantt.TaskDependencyLineType = (ProjectTaskDependencyLineType)Cmb_DepdLine.SelectedIndex;
        }
    }
}
