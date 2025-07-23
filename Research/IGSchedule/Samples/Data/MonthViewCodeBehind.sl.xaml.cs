using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGSchedule.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGSchedule.Samples.Data
{
    public partial class MonthViewCodeBehind : SampleContainer
    {
        private MyScheduleData Data;

        public MonthViewCodeBehind()
        {
            InitializeComponent();
            this.dataManager.DialogFactory = new ScheduleDialogFactory();
            this.Data = new MyScheduleData();
            this.Data.DataLoadingCompleted += new DataLoadingCompletedEventHandler(cbcData_DataLoadingCompleted);
            this.dataManager.ColorScheme = new IGColorScheme();
        }

        public void cbcData_DataLoadingCompleted(object sender, EventArgs e)
        {
            this.DataContext = this.Data;
            cboResources.SelectedIndex = 0;
        }

        public void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MyResource selectedResource = cboResources.SelectedItem as MyResource;
            if (selectedResource != null)
            {
                CalendarGroup calGroup;
                var resource = dataManager.ResourceItems.GetResourceFromId(selectedResource.Id1);
                ResourceCalendar calendar = resource.Calendars.First(c => c.Id == cboResourceCalendars.SelectedItem.ToString());

                // in case the user added it to another group, remove it first 
                CalendarGroup groupWithCalendarAdded = dataManager.CalendarGroups.FirstOrDefault(cg => cg.Calendars.Contains(calendar));
                if (groupWithCalendarAdded != null)
                {
                    groupWithCalendarAdded.Calendars.Remove(calendar);
                    if (groupWithCalendarAdded.Calendars.Count == 0)
                        dataManager.CalendarGroups.Remove(groupWithCalendarAdded);
                }

                // determine where to add the calendar
                if (chkAddingMode.IsChecked == true && cboGroups.SelectedIndex != -1)
                {
                    int index = cboGroups.SelectedIndex;
                    calGroup = dataManager.CalendarGroups[index];
                    calGroup.Calendars.Add(calendar);
                }
                else
                {
                    calGroup = new CalendarGroup();
                    calGroup.Calendars.Add(calendar);
                    dataManager.CalendarGroups.Add(calGroup);
                }
            }
        }

        public void cboResources_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyResource selectedResource = cboResources.SelectedItem as MyResource;
            if (selectedResource != null)
            {
                MyScheduleData myData = (MyScheduleData)DataContext;
                var calendarIds = myData.Appointments.Where(t => t.OwnerId1 == selectedResource.Id1).Select(t => t.CalendarId1).Distinct().ToList();
                cboResourceCalendars.ItemsSource = calendarIds;
                cboResourceCalendars.SelectedIndex = 0;
            }
        }

        private void dtPicker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            monthView.VisibleDates.Clear();
            foreach (var item in dtPicker.SelectedDates)
            {
                monthView.VisibleDates.Add(item);
            }
        }
    }
}
