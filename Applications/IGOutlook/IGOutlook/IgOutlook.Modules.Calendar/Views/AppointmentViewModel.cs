using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Modules.Calendar.Converters;
using IgOutlook.Modules.Calendar.Resources;
using IgOutlook.Services;
using Infragistics;
using Infragistics.Controls.Schedules;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Windows;

namespace IgOutlook.Modules.Calendar.Views
{
    public class AppointmentViewModel : CalendarViewBase, IDialogAware
    {
        #region Protected Members

        protected string AppointmetTypeName;

        #endregion //Protected Members

        #region Private Fields


        private IMessageBoxService MessageBoxService;

        private List<DateTime> startTimeIntervals;
        private List<DateTime> endTimeIntervals;
        private ActivityBase originalActivity;
        private Visibility timeZoneChoosersVisibility;

        #endregion //Private Fields

        #region Public Properties

        public DelegateCommand SaveAndCloseCommand { get; private set; }
        public DelegateCommand DeleteAndCloseCommand { get; private set; }
        public DelegateCommand ToggleTimeZoneChoosersVisibilityCommand { get; private set; }

        public List<DateTime> EndTimeIntervals
        {
            get { return endTimeIntervals; }
            set { endTimeIntervals = value; OnPropertyChanged(); }
        }
        public List<DateTime> StartTimeIntervals
        {
            get { return startTimeIntervals; }
            set { startTimeIntervals = value; OnPropertyChanged(); }
        }
        public Visibility TimeZoneChoosersVisibility
        {
            get { return timeZoneChoosersVisibility; }
            set { timeZoneChoosersVisibility = value; OnPropertyChanged(); }
        }
        public bool IsNewActivity { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public AppointmentViewModel(IEventAggregator eventAggragator, IDialogService dialogService, ICalendarService calendarService, ICategoryService categoryService, IMessageBoxService messageBoxService, XamScheduleDataManager dataManager)
            : base(eventAggragator, dialogService, calendarService, categoryService, dataManager)
        {
            MessageBoxService = messageBoxService;
            var activityAddedEvent = eventAggragator.GetEvent<ActivityChangedEvent>();
            var converter = new ActivityUtcToLocalTimeConverter();

            TimeZoneChoosersVisibility = Visibility.Collapsed;

            activityAddedEvent.Subscribe(OnActivityChanged);

            SaveAndCloseCommand = new DelegateCommand(SaveAndClose);
            DeleteAndCloseCommand = new DelegateCommand(DeleteAndClose);
            ToggleTimeZoneChoosersVisibilityCommand = new DelegateCommand(ToggleTimeZoneChoosersVisibility);

            AppointmetTypeName = ResourceStrings.Appointment_Text;
        }

        #endregion //Constructor

        #region Commmands

        private void DeleteAndClose()
        {
            if (!IsNewActivity)
                base.DeleteCommand.Execute();

            RequestClose();
        }

        private void SaveAndClose()
        {
            SaveChanges();

            closeRequested = true;

            if (RequestClose != null)
            {
                RequestClose();
            }
        }

        private void ToggleTimeZoneChoosersVisibility()
        {
            TimeZoneChoosersVisibility = TimeZoneChoosersVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        #endregion //Commmands

        #region Private Methods

        protected void CloseTheDialog()
        {
            RequestClose();
        }

        private void SaveChanges()
        {
            //Handle selection of reminder if any
            if (ReminderInterval != null && ReminderInterval.TimeInterval.HasValue)
            {
                Activity.Reminder = new Reminder();
                Activity.ReminderEnabled = true;
                Activity.ReminderInterval = ReminderInterval.TimeInterval.Value;
            }
            else
            {
                Activity.Reminder = null;
                Activity.ReminderEnabled = false;
                Activity.ReminderInterval = new TimeSpan();
            }

            DataManager.EndEdit(Activity);

        }

        private List<DateTime> GenerateStartTimeIntervals(DateTime startTime)
        {
            var intervalStep = new TimeSpan(0, 30, 0);
            var startTimeLocal = new DateTime(startTime.Year, startTime.Month, startTime.Day, startTime.Hour, startTime.Minute, startTime.Second, DateTimeKind.Utc).ToLocalTime();
            var startInterval = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0);
            var day = startTimeLocal.Day;

            var intervals = new List<DateTime>();

            while (startInterval.Day == day)
            {
                intervals.Add(startInterval);
                startInterval = startInterval.Add(intervalStep);
            }

            if (!intervals.Contains(startTimeLocal))
                intervals.Add(startTimeLocal);

            intervals.Sort();

            return intervals;
        }

        private List<DateTime> GenerateEndTimeIntervals(DateTime endTime)
        {
            var intervalStep = new TimeSpan(0, 30, 0);
            var endTimeLocal = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endTime.Minute, endTime.Second, DateTimeKind.Utc).ToLocalTime();
            var roundedStart = Round(endTimeLocal.TimeOfDay, new TimeSpan(0, 30, 0), MidpointRounding.AwayFromZero);
            var startInterval = new DateTime(endTime.Year, endTime.Month, endTime.Day, roundedStart.Hours, roundedStart.Minutes, roundedStart.Seconds);
            var duration = new TimeSpan();

            var intervals = new List<DateTime>();

            while (duration.Hours < 23)
            {
                duration = duration.Add(intervalStep);
                intervals.Add(startInterval);
                startInterval = startInterval.Add(intervalStep);
            }

            if (!intervals.Contains(endTimeLocal))
                intervals.Add(endTimeLocal);

            intervals.Sort();

            return intervals;
        }

        public TimeSpan Round(TimeSpan time, TimeSpan roundingInterval, MidpointRounding roundingType)
        {
            return new TimeSpan(
                Convert.ToInt64(Math.Round(
                    time.Ticks / (decimal)roundingInterval.Ticks,
                    roundingType
                )) * roundingInterval.Ticks
            );
        }

        public virtual void OnActivityChanged(ActivityBase act)
        {

            if (originalActivity != null)
                return;

            originalActivity = act;
            DataErrorInfo dataErrorInfo;
            Activity = DataManager.BeginEditWithCopy(originalActivity, true, out dataErrorInfo);

            //Generate Time Intervals
            StartTimeIntervals = GenerateStartTimeIntervals(Activity.Start);
            EndTimeIntervals = GenerateEndTimeIntervals(Activity.End);

            base.UpdateTitleOnPropertyChanged(Activity, "Subject", " - " + AppointmetTypeName, ResourceStrings.Untitled_Text);

            //Handle IsTimeZoneNeutral
            base.HookOnPropertyChanged(Activity, "IsTimeZoneNeutral", () =>
            {
                if (Activity.IsTimeZoneNeutral)
                {
                    Activity.Start = new DateTime(Activity.Start.Year, Activity.Start.Month, Activity.Start.Day, 0, 0, 0);
                    Activity.End = new DateTime(Activity.Start.Year, Activity.Start.Month, Activity.Start.Day, 0, 0, 0).AddDays(1);
                }
                else
                {
                    if (originalActivity.IsTimeZoneNeutral)
                    {
                        var workingHours = DataManager.GetWorkingHours(Activity.OwningResource, Activity.Start);

                        if (workingHours.Count > 0)
                        {
                            var start = new DateTime(Activity.Start.Year, Activity.Start.Month, Activity.Start.Day, workingHours[0].Start.Hours, workingHours[0].Start.Minutes, workingHours[0].Start.Seconds);
                            var end = new DateTime(Activity.Start.Year, Activity.Start.Month, Activity.Start.Day, workingHours[0].Start.Hours, workingHours[0].Start.Minutes + 30, workingHours[0].Start.Seconds);

                            StartTimeIntervals = GenerateStartTimeIntervals(start);
                            EndTimeIntervals = GenerateEndTimeIntervals(end);

                            Activity.Start = start;
                            Activity.End = end;
                        }
                    }
                    else
                    {
                        Activity.Start = originalActivity.Start;
                        Activity.End = originalActivity.End;
                    }
                }
            });

            bool wasDescriptionDeafultRtfLoaded = false;

            IsNewActivity = Activity.DataItem == null;
            OnPropertyChanged("IsNewActivity");

            Activity.PropertyChanged += (s, a) =>
            {
                if (a.PropertyName == "Description" && wasDescriptionDeafultRtfLoaded == false)
                {
                    if (IsNewActivity)
                        Title = ResourceStrings.Untitled_Text + " - " + AppointmetTypeName;
                    else
                        Title = Activity.Subject + " - " + AppointmetTypeName;

                    wasDescriptionDeafultRtfLoaded = true;
                    return;
                }

                isDirty = true;
            };
        }

        #endregion //Private Methods

        #region Base Class Overrides

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            var activityId = navigationContext.Parameters[Parameters.ActivityIdKey];
        }

        #endregion //Base Class Overrides

        #region IDialogAware

        private bool closeRequested;
        private bool isDirty;

        public bool CanCloseDialog()
        {
            if (closeRequested)
                return true;
            if (!isDirty)
                return true;

            var interactionResult = MessageBoxService.Show("IG Outlook", ResourceStrings.SaveChangesMessage_Text, MessageBoxButtons.YesNoCancel);

            if (interactionResult == InteractionResult.Cancel)
            {
                return false;
            }
            else if (interactionResult == InteractionResult.No)
            {
                DataErrorInfo dataErrorInfo;
                DataManager.CancelEdit(Activity, out dataErrorInfo);
                return true;
            }
            else
            {
                SaveChanges();
                return true;
            }
        }

        public event Action RequestClose;

        protected virtual void CloseDialog()
        {
            if (RequestClose != null)
            {
                closeRequested = true;
                RequestClose();
            }
        }

        #endregion //IDialogAware
    }

}
