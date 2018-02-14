using IgOutlook.Infrastructure;
using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Services;
using Infragistics.Controls.Schedules;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace IgOutlook.Modules.Calendar.Views
{
    public class CalendarViewModel : CalendarViewBase
    {
        #region Private Fields

        private IOutlookDateNavigator dateNavigator;
        private Infragistics.DateRange? selectedTimeRange;
        private ObservableCollection<IgOutlook.Business.Calendar.ResourceCalendar> resourceCalendars;
        private CalendarClosedEvent calendarClosedEvent;
        private ViewItemsCountChangedEvent viewItemsCountChangedEvent;
        private ResourceCalendar activeCalendar;
        private OutlookDateNavigatorSelectedDatesChanged outlookDateNavigatorSelectedDatesChanged;

        #endregion //Private Fields

        #region Public Properties

        public IOutlookDateNavigator DateNavigator
        {
            get { return dateNavigator; }
            set { dateNavigator = value; OnPropertyChanged(); }
        }

        public DelegateCommand<Infragistics.DateRange?> SelectedTimeRangeChangedCommand { get; private set; }
        public DelegateCommand NewAppointmentCommand { get; private set; }
        public DelegateCommand NewMeetingCommand { get; private set; }

        public ResourceCalendar ActiveCalendar
        {
            get { return activeCalendar; }
            set
            {
                activeCalendar = value;
                if (activeCalendar != null) UpdateVisibleActivitiesCount();
                OnPropertyChanged();
            }
        }

        #endregion //Public Properties

        #region Base Class Overrides

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            Title = Resources.ResourceStrings.CalendarGroup_Header_Calendar;
            EventAggregator.GetEvent<ViewActivateEvent>().Publish(Title);

            if (!string.IsNullOrEmpty(navigationContext.Parameters.ToString()))
            {
                var initialCalendarId = string.Format("{0}[{1}]", navigationContext.Parameters[Parameters.ResourceIdKey], navigationContext.Parameters[Parameters.CalendarIdKey]);

                var calendarGroup = DataManager.CalendarGroups.FirstOrDefault(c => c.InitialSelectedCalendarId == initialCalendarId);

                if (calendarGroup == null)
                {
                    this.DataManager.CalendarGroups.Add(new CalendarGroup { InitialCalendarIds = initialCalendarId, InitialSelectedCalendarId = initialCalendarId });
                }
                else
                {
                    calendarGroup.Calendars[0].IsVisible = true;
                }
            }
        }

        #endregion //Base Class Overrides

        #region Constructor

        public CalendarViewModel(IEventAggregator eventAggragator, IDialogService dialogService, ICalendarService calendarService, ICategoryService categoryService, XamScheduleDataManager dataManager)
            : base(eventAggragator, dialogService, calendarService, categoryService, dataManager)
        {
            var unselectedCalendarChangedEvent = eventAggragator.GetEvent<UnselectedCalendarChangedEvent>();
            var selectedCalendarChangedEvent = eventAggragator.GetEvent<SelectedCalendarChangedEvent>();
            calendarClosedEvent = eventAggragator.GetEvent<CalendarClosedEvent>();
            viewItemsCountChangedEvent = EventAggregator.GetEvent<ViewItemsCountChangedEvent>();
            outlookDateNavigatorSelectedDatesChanged = EventAggregator.GetEvent<OutlookDateNavigatorSelectedDatesChanged>();

            this.DateNavigator = new OutlookDateNavigator(); 

            eventAggragator.GetEvent<DateNavigatorSelectedDatesChanged>().Subscribe(OnDateNavigatorSelectedDatesChanged);  

            this.DateNavigator.SelectedDatesChanged += DateNavigator_SelectedDatesChanged;

            this.DataManager.DialogFactory = new CustomAppointmentDialogFactory(DialogService, eventAggragator);

            resourceCalendars = calendarService.GetResourceCalendars();

            var listScheduleDataConnector = new ListScheduleDataConnector();
            listScheduleDataConnector.ResourceCalendarPropertyMappings = new ResourceCalendarPropertyMappingCollection { UseDefaultMappings = true };
            listScheduleDataConnector.ResourcePropertyMappings = new ResourcePropertyMappingCollection { UseDefaultMappings = true };
            listScheduleDataConnector.AppointmentPropertyMappings = new AppointmentPropertyMappingCollection { UseDefaultMappings = true };
            listScheduleDataConnector.ActivityCategoryPropertyMappings = new ActivityCategoryPropertyMappingCollection { UseDefaultMappings = true };
            listScheduleDataConnector.AppointmentPropertyMappings.MetadataPropertyMappings = new Infragistics.MetadataPropertyMappingCollection();
            listScheduleDataConnector.AppointmentPropertyMappings.MetadataPropertyMappings.Add("IsMeetingRequest", "IsMeetingRequest");
            listScheduleDataConnector.AppointmentPropertyMappings.MetadataPropertyMappings.Add("AppointmentIds", "AppointmentIds");
            listScheduleDataConnector.AppointmentPropertyMappings.MetadataPropertyMappings.Add("To", "To");
            listScheduleDataConnector.AppointmentPropertyMappings.MetadataPropertyMappings.Add("IsNewVariance", "IsNewVariance");

            listScheduleDataConnector.ResourceItemsSource = calendarService.GetResources();
            listScheduleDataConnector.ResourceCalendarItemsSource = resourceCalendars;
            listScheduleDataConnector.AppointmentItemsSource = calendarService.GetAppointments();
            listScheduleDataConnector.ActivityCategoryItemsSource = categoryService.GetCategories();


            this.DataManager.DataConnector = listScheduleDataConnector;
            this.DataManager.CurrentUserId = "davids";
            this.DataManager.CalendarGroups.Add(new CalendarGroup { InitialCalendarIds = "davids[davidsCalendar]", InitialSelectedCalendarId = "davids[davidsCalendar]" });

            this.DataManager.ActivityRemoved += DataManager_ActivityRemoved;
            this.DataManager.ActivityAdded += DataManager_ActivityAdded;
            this.DataManager.ActivityChanging += DataManager_ActivityChanging;
            this.DataManager.ActivityChanged += DataManager_ActivityChanged;

       //     selectedCalendarChangedEvent.Publish("Calendar");

            foreach (var calendar in resourceCalendars)
            {
                calendar.PropertyChanged += OnCalendarIsVisiblePropertyChanged;
            }

            unselectedCalendarChangedEvent.Subscribe(OnUnselectedCalendarChanged);

            SelectedTimeRangeChangedCommand = new DelegateCommand<Infragistics.DateRange?>(SelectedTimeRangeChanged);
            NewAppointmentCommand = new DelegateCommand(NewAppointment);
            NewMeetingCommand = new DelegateCommand(NewMeeting);

        }

        #endregion //Constructor

        #region Private Methods

        private void OnDateNavigatorSelectedDatesChanged(ObservableCollection<DateTime> selectedDates)
        {
            this.DateNavigator.SetSelectedDates(selectedDates.ToList());
        }

        private void UpdateVisibleActivitiesCount()
        {
            var dates = DateNavigator.GetSelectedDates();
            dates.ToList().Sort();

            if (dates.Count > 0 && ActiveCalendar != null)
            {
                if (dates[0].AddDays(dates.Count - 1).Day == dates[dates.Count - 1].Day)
                {
                    var start = new DateTime(dates[0].Year, dates[0].Month, dates[0].Day, 0, 0, 0);
                    var end = new DateTime(dates[dates.Count - 1].Year, dates[dates.Count - 1].Month, dates[dates.Count - 1].Day, 23, 59, 59);
                    var activities = DataManager.GetActivities(new ActivityQuery(ActivityTypes.All, new Infragistics.DateRange(start, end), ActiveCalendar)).Activities;
                    viewItemsCountChangedEvent.Publish(activities.Count);
                }
            }
        }

        #endregion //Private Methods

        #region Event Handlers

        private void DataManager_ActivityChanged(object sender, ActivityChangedEventArgs e)
        {
            if (e.Activity.DataItem == null) return;

            var appointment = (IgOutlook.Business.Calendar.Appointment)e.Activity.DataItem;

            //Meetings
            if (appointment.IsMeetingRequest)
            {
                //A new Variance was created 
                if (appointment.IsNewVariance)
                {
                    appointment.IsNewVariance = false;
                    CalendarService.GenerateAssociatedVarianceAppointments(appointment);
                }
                else
                {
                    CalendarService.UpdateAssociatedAppointments(appointment);
                }
            }
        }

        private void DataManager_ActivityChanging(object sender, ActivityChangingEventArgs e)
        {
            //Meetings
            if ((bool)e.Activity.Metadata["IsMeetingRequest"])
            {
                //A new Variance was created 
                if (e.OriginalActivityData != null && e.Activity.IsOccurrence && e.OriginalActivityData.IsVariance == false)
                {
                    e.Activity.Metadata["IsNewVariance"] = true;
                }
            }
        }

        private void DataManager_ActivityAdded(object sender, ActivityAddedEventArgs e)
        {
            if (e.Activity.DataItem != null)
            {
                var appointment = (IgOutlook.Business.Calendar.Appointment)e.Activity.DataItem;

                if (appointment.IsMeetingRequest)
                {
                    CalendarService.GenerateAssociatedAppointments(appointment);
                }
            }
        }

        private void DataManager_ActivityRemoved(object sender, ActivityRemovedEventArgs e)
        {
            var appointment = (IgOutlook.Business.Calendar.Appointment)e.Activity.DataItem;

            if (e.Activity.IsVariance)
            {

                if (appointment.IsMeetingRequest)
                {
                    CalendarService.GenerateAssociatedVarianceAppointments(appointment);
                }
            }
            else
            {
                if (appointment.IsMeetingRequest)
                {
                    CalendarService.DeleteAssociatedAppointments(appointment);
                }
            }
        }

        private void OnCalendarIsVisiblePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var calendar = (IgOutlook.Business.Calendar.ResourceCalendar)sender;

            if (e.PropertyName == "IsVisible")
            {
                if (calendar.IsVisible == false)
                {
                    calendarClosedEvent.Publish(calendar.Name);
                }
            }
        }

        private void OnUnselectedCalendarChanged(string calendarName)
        {
            var calendar = resourceCalendars.FirstOrDefault(r => r.Name == calendarName);

            if (calendar != null)
            {
                calendar.PropertyChanged -= OnCalendarIsVisiblePropertyChanged;
                calendar.IsVisible = false;
                calendar.PropertyChanged += OnCalendarIsVisiblePropertyChanged;
            }
        }

        void DateNavigator_SelectedDatesChanged(object sender, Infragistics.Controls.Editors.SelectedDatesChangedEventArgs e)
        {
            outlookDateNavigatorSelectedDatesChanged.Publish(new ObservableCollection<DateTime>(DateNavigator.GetSelectedDates()));
            UpdateVisibleActivitiesCount();
        }

        #endregion //Event handlers

        #region Commands

        private void SelectedTimeRangeChanged(Infragistics.DateRange? dateRange)
        {
            selectedTimeRange = dateRange;
        }

        private void NewAppointment()
        {
            if (selectedTimeRange.HasValue)
            {
                CustomAppointmentDialogFactory.IsAppointmentMeetingRequest = false;

                DateTime start, end;

                if (selectedTimeRange.Value.Start > selectedTimeRange.Value.End)
                {
                    start = selectedTimeRange.Value.End;
                    end = selectedTimeRange.Value.Start;
                }
                else
                {
                    start = selectedTimeRange.Value.Start;
                    end = selectedTimeRange.Value.End;
                }

                var duration = new TimeSpan((end - start).Ticks);
                start = DateTimeHelper.ConvertFromLocalToUtc(start);

                DataManager.DisplayActivityDialog(ActivityType.Appointment,
                    System.Windows.Application.Current.MainWindow,
                    DataManager.ResourceItems[0].PrimaryCalendar,
                    start,
                    duration,
                    false);
            }
        }

        private void NewMeeting()
        {
            if (selectedTimeRange.HasValue)
            {
                CustomAppointmentDialogFactory.IsAppointmentMeetingRequest = true;

                DateTime start, end;

                if (selectedTimeRange.Value.Start > selectedTimeRange.Value.End)
                {
                    start = selectedTimeRange.Value.End;
                    end = selectedTimeRange.Value.Start;
                }
                else
                {
                    start = selectedTimeRange.Value.Start;
                    end = selectedTimeRange.Value.End;
                }

                var duration = new TimeSpan((end - start).Ticks);
                start = DateTimeHelper.ConvertFromLocalToUtc(start);

                DataManager.DisplayActivityDialog(ActivityType.Appointment,
                    System.Windows.Application.Current.MainWindow,
                    DataManager.ResourceItems[0].PrimaryCalendar,
                    start,
                    duration,
                    false);
            }
        }

        #endregion //Commands
    }
}
