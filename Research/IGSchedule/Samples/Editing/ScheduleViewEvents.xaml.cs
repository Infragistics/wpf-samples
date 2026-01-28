using System;
using System.Windows;
using Infragistics;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Editing
{
    /// <summary>
    /// Interaction logic for ScheduleViewEvents.xaml
    /// </summary>
    public partial class ScheduleViewEvents : SampleContainer
    {
        private int innerCounter;

        public ScheduleViewEvents()
        {
            InitializeComponent();
        }

        private void XamScheduleDataManager_ActivityDialogDisplaying(object sender, ActivityDialogDisplayingEventArgs e)
        {
            AddToLog("XamScheduleDataManager_ActivityDialogDisplaying");
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

        private void scheduleView_ActiveCalendarChanged(object sender, RoutedPropertyChangedEventArgs<ResourceCalendar> e)
        {
            AddToLog("XamScheduleView_ActiveCalendarChanged");
        }

        private void scheduleView_SelectedActivitiesChanged(object sender, SelectedActivitiesChangedEventArgs e)
        {
            AddToLog("XamScheduleView_SelectedActivitiesChanged");
        }

        void scheduleView_SelectedTimeRangeChanged(object sender, RoutedPropertyChangedEventArgs<DateRange?> e)
        {
            AddToLog("XamScheduleView_SelectedTimeRangeChanged");
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
