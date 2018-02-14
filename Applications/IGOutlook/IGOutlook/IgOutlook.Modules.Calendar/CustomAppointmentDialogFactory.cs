using IgOutlook.Infrastructure.Dialogs;
using Infragistics.Controls.Schedules;
using Prism.Events;
using Prism.Regions;
using System.Windows;

namespace IgOutlook.Modules.Calendar
{
    public class CustomAppointmentDialogFactory : ScheduleDialogFactoryBase
    {
        public static bool IsAppointmentMeetingRequest { get; set; }

        protected IDialogService DialogService { get; private set; }
        protected IEventAggregator EventAggragator { get; private set; }
        public override ActivityTypes SupportedActivityDialogTypes
        {
            get { return ActivityTypes.Appointment; }
        }

        public CustomAppointmentDialogFactory(IDialogService dialogService, IEventAggregator eventAggragator)
        {
            DialogService = dialogService;
            EventAggragator = eventAggragator;
        }

        public override FrameworkElement CreateActivityDialog(FrameworkElement container, XamScheduleDataManager dataManager, ActivityBase activity, bool allowModifications, bool allowRemove)
        {
            string view = "AppointmentView";

            if (activity.DataItem != null)
            {
                if (activity.Metadata["IsMeetingRequest"] != null && (bool)activity.Metadata["IsMeetingRequest"] == true)
                    view = "MeetingView";
            }
            else
            {
                if (IsAppointmentMeetingRequest)
                {
                    activity.Metadata["IsMeetingRequest"] = true;
                    view = "MeetingView";
                }
            }

            IsAppointmentMeetingRequest = false;

            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(Parameters.ActivityIdKey, activity.Id ?? "1");

            switch (activity.ActivityType)
            {
                case ActivityType.Appointment:
                    {
                        var evn = EventAggragator.GetEvent<ActivityChangedEvent>();
                        DialogService.ShowRibbonDialog(view, parameters);
                        evn.Publish(activity);

                        return DialogService.GetDialogInstance();
                    }
                default:
                    {
                        return null;
                    }
            }
        }
    }
}
