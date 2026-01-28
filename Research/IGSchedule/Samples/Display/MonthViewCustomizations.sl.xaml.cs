using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Display
{
    public partial class MonthViewCustomizations : SampleContainer
    {
        public MonthViewCustomizations()
        {
            InitializeComponent();
            this.dataManager.ColorScheme = new IGColorScheme();
            ScheduleSettings sSettings = new ScheduleSettings();
            sSettings.AppointmentSettings = new AppointmentSettings() { IsAddViaDoubleClickEnabled = true };
            sSettings.TaskSettings = new TaskSettings() { IsAddViaDoubleClickEnabled = true };
            sSettings.JournalSettings = new JournalSettings() { IsAddViaDoubleClickEnabled = true };
            this.dataManager.Settings = sSettings;
        }

        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboCalendarDisplayMode != null)
            {
                monthView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
            }
        }

        private void LoadVisibleDates()
        {
            if (dtPicker.SelectedDates.Count > 0)
            {
                //You must clear the existing dates before setting new ones.
                monthView.VisibleDates.Clear();
                foreach (var item in dtPicker.SelectedDates)
                {
                    monthView.VisibleDates.Add(item);
                }
            }
        }

        private void dtPicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadVisibleDates();
        }

        private void rbDialogsSimple_Click(object sender, RoutedEventArgs e)
        {
            this.dataManager.DialogFactory = null;
        }

        private void rbDialogsAdvanced_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataManager.DialogFactory == null)
            {
                this.dataManager.DialogFactory = new Infragistics.Controls.Schedules.ScheduleDialogFactory();
            }
        }

        private void cbNewDialogCondition_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null && cb.IsChecked.HasValue)
            {
                if (cb.Name.Contains("App"))
                {
                    if (cb.Name.Contains("Click"))
                    {
                        this.dataManager.Settings.AppointmentSettings.IsAddViaClickToAddEnabled = cb.IsChecked.Value;
                    }
                    else if (cb.Name.Contains("Double"))
                    {
                        this.dataManager.Settings.AppointmentSettings.IsAddViaDoubleClickEnabled = cb.IsChecked.Value;
                    }
                    else if (cb.Name.Contains("Type"))
                    {
                        this.dataManager.Settings.AppointmentSettings.IsAddViaTypingEnabled = cb.IsChecked.Value;
                    }
                }
                else if (cb.Name.Contains("Task"))
                {
                    if (cb.Name.Contains("Click"))
                    {
                        this.dataManager.Settings.TaskSettings.IsAddViaClickToAddEnabled = cb.IsChecked.Value;
                    }
                    else if (cb.Name.Contains("Double"))
                    {
                        this.dataManager.Settings.TaskSettings.IsAddViaDoubleClickEnabled = cb.IsChecked.Value;
                    }
                    else if (cb.Name.Contains("Type"))
                    {
                        this.dataManager.Settings.TaskSettings.IsAddViaTypingEnabled = cb.IsChecked.Value;
                    }
                }
                else if (cb.Name.Contains("Jour"))
                {
                    if (cb.Name.Contains("Click"))
                    {
                        this.dataManager.Settings.JournalSettings.IsAddViaClickToAddEnabled = cb.IsChecked.Value;
                    }
                    else if (cb.Name.Contains("Double"))
                    {
                        this.dataManager.Settings.JournalSettings.IsAddViaDoubleClickEnabled = cb.IsChecked.Value;
                    }
                    else if (cb.Name.Contains("Type"))
                    {
                        this.dataManager.Settings.JournalSettings.IsAddViaTypingEnabled = cb.IsChecked.Value;
                    }
                }
            }
        }
    }
}
