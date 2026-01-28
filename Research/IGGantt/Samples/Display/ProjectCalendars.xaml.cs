using System;
using System.Windows;
using IGGantt.Samples.DataSource;
using Infragistics.Samples.Framework;
using System.Windows.Controls;

namespace IGGantt.Samples.Display
{
    public partial class ProjectCalendars : SampleContainer
    {
        private DateTime selectedDate = DateTime.Today;
        private bool useCalendarException = true;
        private bool generateSampleTasks = true;

        public ProjectCalendars()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        #region Event Handlers

        void OnSampleLoaded(object sender, RoutedEventArgs rea)
        {
            GetProjectData();
            xdti.Value = selectedDate;
        }

        private void OnButtonClick(object sender, RoutedEventArgs rea)
        {
            GetProjectData();
        }

        private void Xdti_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (xdti.Value != null)
            {
                selectedDate = (DateTime)xdti.Value;
            }
        }

        private void OnRadioButtonChecked(object sender, RoutedEventArgs rea)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.IsChecked.HasValue)
            {
                useCalendarException = rb.IsChecked.Value;
            }
        }

        private void OnCheckBoxChanged(object sender, RoutedEventArgs rea)
        {
            CheckBox cb = sender as CheckBox;

            if (cb.IsChecked.HasValue)
            {
                generateSampleTasks = cb.IsChecked.Value;
            }
        }

        #endregion // Event Handlers

        private void GetProjectData()
        {
            if (useCalendarException)
            {
                xamGantt.DataContext = ProjectCalendarHelper.GenerateProjectCalendarException(selectedDate, generateSampleTasks);
            }
            else
            {
                xamGantt.DataContext = ProjectCalendarHelper.GenerateProjectCalendarWorkWeek(selectedDate.DayOfWeek, generateSampleTasks);
            }
        }
    }
}
