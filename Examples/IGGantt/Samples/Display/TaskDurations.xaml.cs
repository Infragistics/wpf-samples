using System.Windows;
using System.Windows.Controls;
using IGGantt.Samples.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Display
{
    public partial class TaskDurations : SampleContainer
    {
        public TaskDurations()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs rea)
        {
            LoadData(2);
        }

        private void RadioButtonOnChecked(object sender, RoutedEventArgs rea)
        {
            RadioButton rb = sender as RadioButton;
            LoadData(int.Parse(rb.Tag.ToString()));
        }

        private void LoadData(int sampleType)
        {
            DataContext = ProjectDataHelper.GenerateLinkedTasks(GetSampleTaskDurations(sampleType));
        }

        /* GenerateLinkedTasks creates sample project based on duration array, returned by the GetSampleTaskDuration sample method. */
        /* In one of the cases only normal unit strings are used, in other only elapsed unit strings are used, and in third of the */
        /* cases normal and elapsed unit strings are used. */
        private ProjectDuration[] GetSampleTaskDurations(int? sampleType)
        {
            if (sampleType == 0)
            {
                return new ProjectDuration[] 
                {
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.Days),
                    ProjectDuration.FromFormatUnits(2.0, ProjectDurationFormat.Days),
                    ProjectDuration.FromFormatUnits(6.0, ProjectDurationFormat.Hours),
                    ProjectDuration.FromFormatUnits(12.0, ProjectDurationFormat.Hours),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.Days),
                    ProjectDuration.FromFormatUnits(2.0, ProjectDurationFormat.Days),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.Weeks),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.Weeks)
                };
            }
            else if (sampleType == 1)
            {
                return new ProjectDuration[] 
                {
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.ElapsedDays),
                    ProjectDuration.FromFormatUnits(2.0, ProjectDurationFormat.ElapsedDays),
                    ProjectDuration.FromFormatUnits(6.0, ProjectDurationFormat.ElapsedHours),
                    ProjectDuration.FromFormatUnits(12.0, ProjectDurationFormat.ElapsedHours),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.ElapsedDays),
                    ProjectDuration.FromFormatUnits(2.0, ProjectDurationFormat.ElapsedDays),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.ElapsedWeeks),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.ElapsedWeeks)
                };
            }
            else
            {
                return new ProjectDuration[] 
                {
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.Days),
                    ProjectDuration.FromFormatUnits(2.0, ProjectDurationFormat.ElapsedDays),
                    ProjectDuration.FromFormatUnits(6.0, ProjectDurationFormat.Hours),
                    ProjectDuration.FromFormatUnits(12.0, ProjectDurationFormat.ElapsedHours),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.Days),
                    ProjectDuration.FromFormatUnits(2.0, ProjectDurationFormat.ElapsedDays),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.Weeks),
                    ProjectDuration.FromFormatUnits(1.0, ProjectDurationFormat.ElapsedWeeks)
                };
            }
        }
    }
}
