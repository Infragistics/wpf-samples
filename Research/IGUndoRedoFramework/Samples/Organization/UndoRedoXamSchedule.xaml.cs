using System;
using System.Windows;
using IGUndoRedoFramework.Data;
using IGUndoRedoFramework.UndoUnits;
using IGUndoRedoFramework.ViewModel;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Undo;

namespace IGUndoRedoFramework.Samples.Organization
{
    public partial class UndoRedoXamSchedule : SampleContainer
    {
        private UndoManager _undoManager;
        private ScheduleViewModel _viewModel;

        public UndoRedoXamSchedule()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            _undoManager = new UndoManager();
            var viewmodel = new ScheduleViewModel(_undoManager);

            viewmodel.Resources.Add(new ResourceItem { Id = "0", Name = "Zero" });
            viewmodel.Calendars.Add(new ResourceCalendarItem { Id = "0", Name = "Main", OwningResourceId = "0" });

            DateTime now = DateTime.Now.ToUniversalTime();
            viewmodel.Appointments.Add(new AppointmentItem { Id = "0", OwningResourceId = "0", OwningCalendarId = "0", Description = "This is when the app started", Start = now, End = now.AddMinutes(30), Subject = "Now" });

            viewmodel.CurrentUserId = "0";

            string tzId = TimeZoneInfoProvider.DefaultProvider.LocalTimeZoneIdResolved;

            foreach (var activity in viewmodel.Appointments)
            {
                activity.StartTimeZoneId = tzId;
                activity.EndTimeZoneId = tzId;
            }

            _viewModel = viewmodel;
            this.DataContext = viewmodel;            
        }

        private void dataManager_ActivityChanging(object sender, ActivityChangingEventArgs e)
        {
            ScheduleDataManagerUndoHelper.AddActivityModifiedChanged(_undoManager, sender as XamScheduleDataManager, e.Activity, e.OriginalActivityData);
        }

        private void dataManager_ActivityAdded(object sender, ActivityAddedEventArgs e)
        {
            ScheduleDataManagerUndoHelper.AddActivityAddChange(_undoManager, sender as XamScheduleDataManager, e.Activity);
        }

        private void dataManager_ActivityRemoved(object sender, ActivityRemovedEventArgs e)
        {
            ScheduleDataManagerUndoHelper.AddActivityRemoveChange(_undoManager, sender as XamScheduleDataManager, e.Activity);
        }
    }
}
