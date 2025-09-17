using System;
using System.Windows;
using System.Windows.Controls;
using IGGantt.Samples.DataSource;
using Infragistics;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Organization
{
    public partial class SchedulingProjectFromStart : SampleContainer
    {
        private bool _isInit = true;
        public SchedulingProjectFromStart()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(SchedulingProjectFromStart_Loaded);
        }

        void SchedulingProjectFromStart_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isInit)
            { 
                ProjectDataHelper.GenerateTasksDependencies(this.gantt.Project.RootTask);
                _isInit = false;
            }
        }     

        private void calendar_SelectedDatesChanged(object sender, SelectedDatesChangedEventArgs e)
        {
            DateTime selectedDate = this.calendar.SelectedDate.Value;

            if (this.gantt.Project.IsScheduledFromStart)
            {
                this.gantt.Project.Start = selectedDate;
            }
            else
            {
                this.gantt.Project.Finish = selectedDate;
            }
            
            this.gantt.VisibleDateRange = new DateRange(this.gantt.Project.Start, this.gantt.Project.Finish);
        }

        
        private void Rb_Scheduling_Checked(object sender, RoutedEventArgs e)
        {
            if (!_isInit)
            { 
                string tag = (sender as RadioButton).Tag.ToString();
                if (tag == "Start")
                {
                    this.gantt.Project.IsScheduledFromStart = true;
                }

                if (tag == "Finish")
                {
                    this.gantt.Project.IsScheduledFromStart = false;
                }
            }
        }
    }
}
