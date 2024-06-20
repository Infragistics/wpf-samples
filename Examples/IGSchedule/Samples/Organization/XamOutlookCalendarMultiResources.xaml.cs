using System.Linq;
using System.Windows;
using System.Windows.Controls;
using IGSchedule.DataSource;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Organization
{
    /// <summary>
    /// Interaction logic for XamOutlookCalendarMultiResources.xaml
    /// </summary>
    public partial class XamOutlookCalendarMultiResources : SampleContainer
    {
        ScheduleData mydata = new ScheduleData();

        public XamOutlookCalendarMultiResources()
        {
            InitializeComponent();
        }

        private void cboCalendarDisplayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            outlookView.CalendarDisplayMode = (CalendarDisplayMode)cboCalendarDisplayMode.SelectedIndex;
        }

        private void CheckBox_Clicked(object sender, RoutedEventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var curOwningResourceId = checkBox.Tag.ToString();

            if (checkBox.IsChecked == true)
            {
                if ((this.groupCheck.IsChecked == true) && (dataManager.CalendarGroups.Count() > 0))
                {
                    var calendarmy = mydata.Calendars.First(c => c.OwningResourceId == curOwningResourceId);
                    dataManager.CalendarGroups.First().Calendars.Add(calendarmy);
                }
                else
                {
                    CalendarGroup calGroup = new CalendarGroup();
                    ResourceCalendar calendarmy = mydata.Calendars.First(c => c.OwningResourceId == curOwningResourceId);
                    calGroup.Calendars.Add(calendarmy);
                    dataManager.CalendarGroups.Add(calGroup);
                }
            }
            else
            {
                var groupToRemove = dataManager.CalendarGroups.Where(cg => cg.Calendars.Where(cal => cal.OwningResourceId == curOwningResourceId).Count() > 0).FirstOrDefault();
                var calre = groupToRemove.Calendars.Where(c => c.OwningResourceId == curOwningResourceId).FirstOrDefault();

                dataManager.CalendarGroups.First(cg => cg.Calendars.Where(cal => cal.OwningResourceId == curOwningResourceId).Count() > 0).Calendars.Remove(calre);

                if (groupToRemove.Calendars.Count() == 0)
                {
                    dataManager.CalendarGroups.Remove(groupToRemove);
                }
            }
        }
    }
}
