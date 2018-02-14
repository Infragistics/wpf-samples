using IgOutlook.Business.Calendar;
using System.Collections.ObjectModel;

namespace IgOutlook.Services
{
    public interface ICalendarService
    {
        ObservableCollection<Resource> GetResources();
        ObservableCollection<ResourceCalendar> GetResourceCalendars();
        ObservableCollection<Appointment> GetAppointments();
        ObservableCollection<ResourceCalendar> GetSharedResourceCalendars();
        ObservableCollection<ResourceCalendar> GetUserResourceCalendars(string userId);
        ObservableCollection<ResourceCalendar> GetTeamResourceCalendars(string userId);

        Appointment GetAppointment(string id);

        void GenerateAssociatedAppointments(Appointment appointment);
        void GenerateAssociatedVarianceAppointments(Appointment appointment);
        void UpdateAssociatedAppointments(Appointment appointment);
        void DeleteAssociatedAppointments(Appointment appointment);

    }
}
