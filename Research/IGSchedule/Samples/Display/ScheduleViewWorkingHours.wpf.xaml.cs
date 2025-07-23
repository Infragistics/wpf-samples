using System;
using System.Windows;
using System.Windows.Controls;
using IGSchedule.DataSource;
using Infragistics;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Display
{
    /// <summary>
    /// Interaction logic for ScheduleViewWorkingHours.xaml
    /// </summary>
    public partial class ScheduleViewWorkingHours : SampleContainer
    {
        public ScheduleViewWorkingHours()
        {
            InitializeComponent();

            this.dataManager.DialogFactory = new Infragistics.Controls.Schedules.ScheduleDialogFactory();
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

        private void dtPicker_SelectedDateChanged(object sender, EventArgs e)
        {
            if (dtPicker.Value != null)
            {
                scheduleView.VisibleDates.Clear();
                scheduleView.VisibleDates.Add((DateTime)dtPicker.Value);
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
