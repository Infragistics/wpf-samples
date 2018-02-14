using IgOutlook.Infrastructure.Dialogs;
using IgOutlook.Services;
using Infragistics.Controls.Schedules;
using Prism.Commands;
using System.Linq;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Windows;
using IgOutlook.Modules.Calendar.Resources;
using System.Collections.Generic;

namespace IgOutlook.Modules.Calendar.Views
{
    public class MeetingViewModel : AppointmentViewModel
    {
        #region Public Properties

        private ObservableCollection<IgOutlook.Business.Contacts.Contact> contacts;
        public ObservableCollection<IgOutlook.Business.Contacts.Contact> Contacts
        {
            get { return contacts; }
            set { contacts = value; OnPropertyChanged(); }
        }

        public ObservableCollection<IgOutlook.Business.Calendar.ResourceCalendar> LocationsList { get; set; }
        public ObservableCollection<IgOutlook.Business.Calendar.Resource> ResourcesList { get; set; }

        public Visibility UpdateMeetingTextVisibility { get; private set; }
        public DelegateCommand CancelMeetingCommand { get; private set; }

        private List<string> locationsEmails;

        #endregion //Public Properties

        #region Constructor

        public MeetingViewModel(IEventAggregator eventAggragator, IDialogService dialogService, ICalendarService calendarService, ICategoryService categoryService, IContactService contactService, IMessageBoxService messageBoxService, XamScheduleDataManager dataManager)
            : base(eventAggragator, dialogService, calendarService, categoryService, messageBoxService, dataManager)
        {
            LocationsList = CalendarService.GetSharedResourceCalendars();
            ResourcesList = CalendarService.GetResources();
            Contacts = new ObservableCollection<Business.Contacts.Contact>(contactService.GetContacts());

            locationsEmails = new List<string>();

            foreach (var locationContact in contactService.GetLocationContacts())
            {
                Contacts.Add(locationContact);
                locationsEmails.Add(locationContact.Email);
            }

            CancelMeetingCommand = new DelegateCommand(CancelMeeting);
        }

        #endregion //Constructor

        #region BaseClassOverrides

        public override void OnActivityChanged(ActivityBase act)
        {
            base.OnActivityChanged(act);

            Activity.PropertyChanged += Activity_LocationChanged;

            AppointmetTypeName = ResourceStrings.Meeting_Text;
        }

        protected override void CloseDialog()
        {
            if (Activity != null)
                Activity.PropertyChanged -= Activity_LocationChanged;

            base.CloseDialog();
        }

        #endregion //BaseClassOverrides

        #region Private Methods

        private string GetLocationEmail(string locationName)
        {
            var location = LocationsList.FirstOrDefault(l => l.Name == locationName);
            if (location == null) return string.Empty;
            var resource = ResourcesList.FirstOrDefault(r => r.Id == location.OwningResourceId);

            if (resource != null)
                return resource.EmailAddress;

            return string.Empty;
        }

        private void Activity_LocationChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Location") return;

            if (!string.IsNullOrEmpty(((Appointment)Activity).Location))
            {
                var email = GetLocationEmail(((Appointment)Activity).Location);

                if (email == string.Empty) return;

                if (Activity.Metadata["To"] == null)
                {
                    Activity.Metadata["To"] = new ObservableCollection<string>() { email };
                    return;
                }

                var newTo = new ObservableCollection<string>();

                foreach (var to in (ObservableCollection<string>)Activity.Metadata["To"])
                {
                    if (!locationsEmails.Contains(to))
                        newTo.Add(to);
                }

                newTo.Add(email);

                Activity.Metadata["To"] = newTo;
            }
        }
        #endregion //Private Methods

        #region Commands

        private void CancelMeeting()
        {
            var activitiesToDelete = DataManager.GetActivities(new ActivityQuery(ActivityTypes.Appointment, new Infragistics.DateRange(Activity.Start, Activity.End), (ResourceCalendar)null));

            while (activitiesToDelete.Activities.Count > 0)
            {
                DataManager.Remove(activitiesToDelete.Activities[0]);
            }

            CloseTheDialog();
        }

        #endregion //Commands
    }

}
