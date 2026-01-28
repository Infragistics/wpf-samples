using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGSchedule.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Data
{
    /// <summary>
    /// Interaction logic for ScheduleViewCodeBehind.xaml
    /// </summary>
    public partial class ScheduleViewCodeBehind : SampleContainer
    {
        public ScheduleViewCodeBehind()
        {
            InitializeComponent();

            DataContext = new MyScheduleData();
            ((MyScheduleData)DataContext).DataLoadingCompleted += new DataLoadingCompletedEventHandler(ScheduleViewCodeBehind_DataLoadingCompleted);
            dtPicker.Value = DateTime.Now;
            this.dataManager.DialogFactory = new Infragistics.Controls.Schedules.ScheduleDialogFactory();
        }
        

        void ScheduleViewCodeBehind_DataLoadingCompleted(object sender, EventArgs e)
        {
            cboResources.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
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

                //determine where to add the calendar
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

        private void cboResources_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

        private void dtPicker_SelectedDateChanged(object sender, EventArgs e)
        {
            if (dtPicker.Value != null)
            {
                scheduleView.VisibleDates.Clear();
                scheduleView.VisibleDates.Add((DateTime)dtPicker.Value);
            }
        }
    }
}
