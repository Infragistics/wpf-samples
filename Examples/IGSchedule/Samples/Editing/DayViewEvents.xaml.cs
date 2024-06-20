using System;
using System.Windows;
using Infragistics;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Editing
{
    /// <summary>
    /// Interaction logic for DayViewEvents.xaml
    /// </summary>
    public partial class DayViewEvents : SampleContainer
    {
        private int innerCounter;

        public DayViewEvents()
        {
            InitializeComponent();
        }
        

        void XamScheduleDataManager_ActivityRemoving(object sender, ActivityRemovingEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityRemoving");
        }

        void XamScheduleDataManager_ActivityRemoved(object sender, ActivityRemovedEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityRemoved");
        }

        void XamScheduleDataManager_ActivityChanging(object sender, ActivityChangingEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityChanging");
        }

        void XamScheduleDataManager_ActivityChanged(object sender, ActivityChangedEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityChanged");
        }

        void XamScheduleDataManager_ActivityAdding(object sender, ActivityAddingEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityAdding");
        }

        void XamScheduleDataManager_ActivityAdded(object sender, ActivityAddedEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityAdded");
        }

        void XamScheduleDataManager_ActivityDialogDisplaying(object sender, ActivityDialogDisplayingEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityDialogDisplaying");
        }

        void dayView_SelectedTimeRangeChanged(object sender, RoutedPropertyChangedEventArgs<DateRange?> e)
        {
            AddToLog("XamDayView_SelectedTimeRangeChanged");
        }

        void dayView_ActiveCalendarChanged(object sender, RoutedPropertyChangedEventArgs<ResourceCalendar> e)
        {
            AddToLog("XamDayView_ActiveCalendarChanged");
        }

        void dayView_SelectedActivitiesChanged(object sender, SelectedActivitiesChangedEventArgs e)
        {
            AddToLog("XamDayView_SelectedActivitiesChanged");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            innerCounter = 0;
            txtEvents.Text = string.Empty;
        }

        private bool AddToLog(string Text)
        {
            if (txtEvents != null)
            {
                innerCounter++;
                txtEvents.Text = innerCounter + DateTime.Now.ToString(" - hh:mm:ss - ") + Text + Environment.NewLine + txtEvents.Text;
            }
            return txtEvents != null;
        }
    }
}
