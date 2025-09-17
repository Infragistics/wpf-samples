using System.Windows;
using IGGantt.Samples.DataSource;
using IGGantt.Samples.ViewModel;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class CalendarWorkingTimes : SampleContainer
    {
        CalendarWorkingTimesViewModel cwtvm;
        public CalendarWorkingTimes()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs rea)
        {
            cwtvm = new CalendarWorkingTimesViewModel();
            cwtvm.GanttProject = ProjectCalendarHelper.GenerateSampleProject();
            xamGantt.DataContext = cwtvm.GanttProject;
            DataContext = cwtvm;
        }

        private void OnRefreshButtonClick(object sender, RoutedEventArgs rea)
        {
            DataContext = cwtvm;
            cwtvm.UpdateProjectCalendar();
        }
    }
}