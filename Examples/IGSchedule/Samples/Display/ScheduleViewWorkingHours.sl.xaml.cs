using System;
using System.Windows;
using System.Windows.Controls;
using IGSchedule.DataSource;
using Infragistics;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Display
{
    public partial class ScheduleViewWorkingHours : SampleContainer
    {
        private ScheduleData Data;

        public ScheduleViewWorkingHours()
        {
            InitializeComponent();
            this.dataManager.DialogFactory = new Infragistics.Controls.Schedules.ScheduleDialogFactory();
            this.Data = new ScheduleData();
            this.Data.DataLoadingCompleted += new DataLoadingCompletedEventHandler(fdData_DataLoadingCompleted);
            this.dataManager.ColorScheme = new IGColorScheme();
        }

        public void fdData_DataLoadingCompleted(object sender, EventArgs e)
        {
            this.DataContext = Data;
            cboStart.ItemsSource = Data.GetHours(null, null);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cboEnd.SelectedItem != null && cboStart.SelectedItem != null)
            {
                if (dataManager.Settings == null)
                    dataManager.Settings = new ScheduleSettings();
                if (dataManager.Settings.WorkingHours == null)
                    dataManager.Settings.WorkingHours = new WorkingHoursCollection();

                dataManager.Settings.WorkingHours.Add(new TimeRange((TimeSpan)cboStart.SelectedItem, (TimeSpan)cboEnd.SelectedItem));
            }
            else
            {
                if (cboStart.SelectedItem == null)
                    cboStart.IsDropDownOpen = true;
                else
                    cboEnd.IsDropDownOpen = true;
            }
        }

        private void dtPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtPicker.SelectedDate.HasValue)
            {
                scheduleView.VisibleDates.Clear();
                scheduleView.VisibleDates.Add(dtPicker.SelectedDate.Value);
            }
        }

        private void cboStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cboEnd.ItemsSource = (DataContext as ScheduleData).GetHours(cboStart.SelectedItem as TimeSpan?, null);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            dataManager.Settings.WorkingHours.RemoveAt(grdHours.SelectedIndex);
        }
    }
}
