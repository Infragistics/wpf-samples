using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;

namespace IGGantt.Samples.Display
{
    public partial class ControllingUInteractions : SampleContainer
    {
        private bool _isInit = true;
        public ControllingUInteractions()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            { 
                this.Cmb_AllowDragSummary.ItemsSource = EnumValuesProvider.GetEnumValues<AllowDragSummaryTask>();
                this.Cmb_AllowResizeSummary.ItemsSource = EnumValuesProvider.GetEnumValues<AllowDragSummaryTask>();

                this.Cmb_AllowDragSummary.SelectedIndex = (int)this.gantt.Project.TaskSettings.AllowDragSummary.Value;
                this.Cmb_AllowResizeSummary.SelectedIndex = (int)this.gantt.Project.TaskSettings.AllowResizeSummary.Value;

                _isInit = false;
            }
        }

        private void Cmb_AllowDragSummary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.gantt.Project.TaskSettings.AllowDragSummary = (AllowDragSummaryTask)this.Cmb_AllowDragSummary.SelectedIndex;
        }

        private void Cmb_AllowResizeSummary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.gantt.Project.TaskSettings.AllowResizeSummary = (AllowDragSummaryTask)this.Cmb_AllowResizeSummary.SelectedIndex;
        }
    }
}
