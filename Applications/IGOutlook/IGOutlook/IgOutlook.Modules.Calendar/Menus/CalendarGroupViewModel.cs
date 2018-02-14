using IgOutlook.Infrastructure;
using IgOutlook.Services;
using Infragistics.Controls.Schedules;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace IgOutlook.Modules.Calendar.Menus
{
    public class CalendarGroupViewModel : ViewModelBase
    {
        #region Protected Members

        protected ICalendarService CalendarService { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }

        #endregion //Protected Members

        #region Private Fields

        private object selectedDates;
        private object unselectedCalendar;
        private object selectedCalendar;
        private bool canExecuteDateNavigatorSelectedDatesCommand;
        private XamScheduleDataManager dataManager;
        private ObservableCollection<NavigationItem> _items;
        private ObservableCollection<object> selectedDataItems;

        private UnselectedCalendarChangedEvent unselectedCalendarChangedEvent;
        private SelectedCalendarChangedEvent selectedCalendarChangedEvent;
        private CalendarClosedEvent calendarClosedEvent;
        private DateNavigatorSelectedDatesChanged dateNavigatorSelectedDatesChanged;
        private OutlookDateNavigatorSelectedDatesChanged outlookDateNavigatorSelectedDatesChanged;

        #endregion //Private Fields

        #region Public Properties

        public DelegateCommand<object> CalendarUnselectedComman { get; set; }
        public DelegateCommand<ObservableCollection<DateTime>> DateNavigatorSelectedDatesCommand { get; set; }

        public object SelectedDates
        {
            get { return selectedDates; }
            set { selectedDates = value; OnPropertyChanged(); }
        }
        public object UnselectedCalendar
        {
            get { return unselectedCalendar; }
            set { unselectedCalendar = value; OnPropertyChanged(); }
        }
        public object SelectedCalendar
        {
            get { return selectedCalendar; }
            set { selectedCalendar = value; OnPropertyChanged(); }
        }
        public XamScheduleDataManager DataManager
        {
            get { return dataManager; }
            set { dataManager = value; OnPropertyChanged(); }
        }
        public ObservableCollection<NavigationItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }
        public ObservableCollection<object> SelectedDataItems
        {
            get { return selectedDataItems; }
            set { selectedDataItems = value; OnPropertyChanged(); }
        }

        #endregion //Public Properties

        #region Constructor

        public CalendarGroupViewModel(XamScheduleDataManager dataManager, ICalendarService calendarService, IEventAggregator eventAggragator)
        {
            CalendarUnselectedComman = new DelegateCommand<object>(CalendarUnselectedCommandExecuted);
            DateNavigatorSelectedDatesCommand = new DelegateCommand<ObservableCollection<DateTime>>(OnDateNavigatorSelectedDates);

            CalendarService = calendarService;
            EventAggregator = eventAggragator;

            DataManager = dataManager;

            GenerateMenu();

            unselectedCalendarChangedEvent = EventAggregator.GetEvent<UnselectedCalendarChangedEvent>();
            selectedCalendarChangedEvent = eventAggragator.GetEvent<SelectedCalendarChangedEvent>();
            selectedCalendarChangedEvent.Subscribe(OnSelectedCalendarChanged);

            calendarClosedEvent = EventAggregator.GetEvent<CalendarClosedEvent>();
            calendarClosedEvent.Subscribe(OnCalendarClosed);

            dateNavigatorSelectedDatesChanged = EventAggregator.GetEvent<DateNavigatorSelectedDatesChanged>();

            outlookDateNavigatorSelectedDatesChanged = EventAggregator.GetEvent<OutlookDateNavigatorSelectedDatesChanged>();
            outlookDateNavigatorSelectedDatesChanged.Subscribe(OnOutlookDateNavigatorSelectedDatesChanged);
            canExecuteDateNavigatorSelectedDatesCommand = true;
        }

        #endregion //Constructor

        #region Commands

        private void CalendarUnselectedCommandExecuted(object obj)
        {
            if (obj == null) return;

            unselectedCalendarChangedEvent.Publish(((NavigationItem)obj).Caption);
        }

        private void OnDateNavigatorSelectedDates(ObservableCollection<DateTime> selectedDates)
        {
            if (canExecuteDateNavigatorSelectedDatesCommand)
                dateNavigatorSelectedDatesChanged.Publish(selectedDates);
        }

        #endregion //Commands

        #region Private Methods

        private void OnOutlookDateNavigatorSelectedDatesChanged(ObservableCollection<DateTime> selectedDates)
        {
            canExecuteDateNavigatorSelectedDatesCommand = false;
            SelectedDates = selectedDates;
            canExecuteDateNavigatorSelectedDatesCommand = true;
        }

        private void OnCalendarClosed(string obj)
        {
            var item = FindNavigationItem(Items, obj);

            if (item != null)
                UnselectedCalendar = item;
        }

        private void OnSelectedCalendarChanged(string obj)
        {
            var item = FindNavigationItem(Items, obj);

            if (item != null)
                SelectedCalendar = item;
        }

        private void GenerateMenu()
        {
            Items = new ObservableCollection<NavigationItem>();

            var root1 = new NavigationItem() { Caption = Resources.ResourceStrings.CalendarGroup_MyCalendars_Text, NavigationPath = "CalendarView", CanNavigate = false };

            foreach (var resourceCalendars in CalendarService.GetUserResourceCalendars("davids"))
            {
                root1.Items.Add(new NavigationItem() { Caption = resourceCalendars.Name, DataItem = resourceCalendars, NavigationPath = CreateNavigationPath(resourceCalendars.OwningResourceId, resourceCalendars.Id) });
            }

            var root2 = new NavigationItem() { Caption = Resources.ResourceStrings.CalendarGroup_SharedCalendars_Text, NavigationPath = "CalendarView", CanNavigate = false };

            foreach (var resourceCalendars in CalendarService.GetSharedResourceCalendars())
            {
                root2.Items.Add(new NavigationItem() { Caption = resourceCalendars.Name, DataItem = resourceCalendars, NavigationPath = CreateNavigationPath(resourceCalendars.OwningResourceId, resourceCalendars.Id) });
            }

            var root3 = new NavigationItem() { Caption = Resources.ResourceStrings.CalendarGroup_TeamCalendars_Text, NavigationPath = "CalendarView", CanNavigate = false };

            foreach (var resourceCalendars in CalendarService.GetTeamResourceCalendars("davids"))
            {
                root3.Items.Add(new NavigationItem() { Caption = resourceCalendars.Name, DataItem = resourceCalendars, NavigationPath = CreateNavigationPath(resourceCalendars.OwningResourceId, resourceCalendars.Id) });
            }

            Items.Add(root1);
            Items.Add(root2);
            Items.Add(root3);
        }

        private static NavigationItem FindNavigationItem(ObservableCollection<NavigationItem> items, string caption)
        {
            NavigationItem target = null;

            System.Action<NavigationItem, object> find = (n, dItem) =>
            {
                if (n.Items.Count > 0)
                {
                    foreach (var item in n.Items)
                    {
                        if (target != null) return;

                        if (item.Caption == caption)
                        {
                            target = item;
                        }
                    }
                }
            };

            foreach (var ni in items)
            {
                if (ni.Caption == caption)
                {
                    return ni;
                }
            }

            foreach (var ni in items)
            {
                if (target == null)
                    find(ni, caption);
            }

            return target;
        }

        private string CreateNavigationPath(string reourceId, string calendarId)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(Parameters.ResourceIdKey, reourceId);
            parameters.Add(Parameters.CalendarIdKey, calendarId);
            return "CalendarView" + parameters.ToString();
        }

        #endregion //Private Methods
    }
}
