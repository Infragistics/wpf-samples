using IgOutlook.Business.Calendar;
using IgOutlook.Services.Properties;
using IgOutlook.Services.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace IgOutlook.Services
{
    public class CalendarService : ICalendarService
    {
        public ObservableCollection<ResourceCalendar> GetSharedResourceCalendars()
        {
            return new ObservableCollection<ResourceCalendar>(ResourceCalendars.Where(rc => rc.OwningResourceId.Contains("conferenceRoom")));
        }

        public ObservableCollection<ResourceCalendar> GetUserResourceCalendars(string userId)
        {
            return new ObservableCollection<ResourceCalendar>(ResourceCalendars.Where(rc => rc.OwningResourceId == userId));
        }

        public ObservableCollection<ResourceCalendar> GetTeamResourceCalendars(string userId)
        {
            return new ObservableCollection<ResourceCalendar>(ResourceCalendars.Where(rc => rc.OwningResourceId != userId && !rc.OwningResourceId.StartsWith("conferenceRoom")));
        }

        public ObservableCollection<Resource> GetResources()
        {
            return Resources;
        }

        public ObservableCollection<ResourceCalendar> GetResourceCalendars()
        {
            return ResourceCalendars;
        }

        public ObservableCollection<Appointment> GetAppointments()
        {
            return Appointments;
        }

        static bool wasDataInitilized = false;

        public CalendarService()
        {
            if (!wasDataInitilized)
            {
                InitializeData();
                wasDataInitilized = true;
            }
        }

        #region Data

        private static ObservableCollection<Resource> Resources = new ObservableCollection<Resource>();
        private static ObservableCollection<ResourceCalendar> ResourceCalendars = new ObservableCollection<ResourceCalendar>();
        private static ObservableCollection<Appointment> Appointments = new ObservableCollection<Appointment>();

        private void InitializeData()
        {
            #region Initialize users

            Resource davids = new Resource { Name = "David Smith", EmailAddress = "davids@demo.infragistics.com", Id = "davids" };
            Resource bobbyj = new Resource { Name = "Bobby Jones", EmailAddress = "bobbyj@demo.infragistics.com", Id = "bobbyj" };
            Resource jonathanb = new Resource { Name = "Jonathan Barclay", EmailAddress = "jonathanb@demo.infragistics.com", Id = "jonathanb" };
            Resource barbarab = new Resource { Name = "Barbara Bailey", EmailAddress = "barbarab@demo.infragistics.com", Id = "barbarab" };
            Resource maryh = new Resource { Name = "Mary Hernandez", EmailAddress = "maryh@demo.infragistics.com", Id = "maryh" };
            Resource conferenceRoomA = new Resource { Name = ResourceStrings.ConferenceRoomA_Text, EmailAddress = "conferencerooma@demo.infragistics.com", Id = "conferenceRoomA" };
            Resource conferenceRoomB = new Resource { Name = ResourceStrings.ConferenceRoomB_Text, EmailAddress = "conferenceroomb@demo.infragistics.com", Id = "conferenceRoomB" };

            Resources.Add(davids);
            Resources.Add(bobbyj);
            Resources.Add(jonathanb);
            Resources.Add(barbarab);
            Resources.Add(maryh);
            Resources.Add(conferenceRoomA);
            Resources.Add(conferenceRoomB);

            #endregion // Initialize users

            #region Initialize calendars

            ResourceCalendar davidsCalendar = new ResourceCalendar { Name = ResourceStrings.Calendar_Text, OwningResourceId = "davids", Id = "davidsCalendar" };
            ResourceCalendar bobbyjCalendar = new ResourceCalendar { Name = "Bobby Jones", OwningResourceId = "bobbyj", Id = "bobbyjCalendar" };
            ResourceCalendar jonathanbCalendar = new ResourceCalendar { Name = "Jonathan Barclay", OwningResourceId = "jonathanb", Id = "jonathanbCalendar" };
            ResourceCalendar barbarabCalendar = new ResourceCalendar { Name = "Barbara Bailey", OwningResourceId = "barbarab", Id = "barbarabCalendar" };
            ResourceCalendar maryhCalendar = new ResourceCalendar { Name = "Mary Hernandez", OwningResourceId = "maryh", Id = "maryhCalendar" };

            ResourceCalendar conferenceRoomCalendar1 = new ResourceCalendar { Name = ResourceStrings.ConferenceRoomA_Text, Description = "A", OwningResourceId = "conferenceRoomA", Id = "confRoomACalendar" };
            ResourceCalendar conferenceRoomCalendar2 = new ResourceCalendar { Name = ResourceStrings.ConferenceRoomB_Text, Description = "B", OwningResourceId = "conferenceRoomB", Id = "confRoomBCalendar" };

            ResourceCalendars.Add(davidsCalendar);
            ResourceCalendars.Add(bobbyjCalendar);
            ResourceCalendars.Add(jonathanbCalendar);
            ResourceCalendars.Add(barbarabCalendar);
            ResourceCalendars.Add(maryhCalendar);
            ResourceCalendars.Add(conferenceRoomCalendar1);
            ResourceCalendars.Add(conferenceRoomCalendar2);

            var dateCreator = new DateCreator(DateTime.Now);

            #endregion //Initialize calendars

            Appointment davidsProductIncrementReview = new Appointment()
            {
                Id = "ProductIncrementReview",
                IsMeetingRequest = true,
                Location = ServiceResources.Appt_DavidsProductIncrementReview_Location,
                OwningResourceId = "davids",
                OwningCalendarId = "davidsCalendar",
                Subject = ServiceResources.Appt_DavidsProductIncrementReview_Subject,
            };
            davidsProductIncrementReview.To = new ObservableCollection<string> { "maryh@demo.infragistics.com", "jonathanb@demo.infragistics.com", "conferencerooma@demo.infragistics.com" };

            davidsProductIncrementReview.Description = IgOutlook.Services.Properties.ServiceResources.ProductIncrementReviewDescription;
            davidsProductIncrementReview.Start = dateCreator.CreateDate(0, 9, 0);
            davidsProductIncrementReview.End = dateCreator.CreateDate(0, 10, 30);
            Appointments.Add(davidsProductIncrementReview);

            GenerateAssociatedAppointments(davidsProductIncrementReview);

            Appointment davidsLuchWithSara = new Appointment()
            {
                Id = "davidsLuchWithSara",
                IsMeetingRequest = false,
                OwningResourceId = "davids",
                OwningCalendarId = "davidsCalendar",
                Subject = ServiceResources.Appt_DavidsLuchWithSara_Subject
            };
            davidsLuchWithSara.Start = dateCreator.CreateDate(1, 12, 0);
            davidsLuchWithSara.End = dateCreator.CreateDate(1, 13, 0);

            Appointments.Add(davidsLuchWithSara);


            Appointment davidsCallJohn = new Appointment()
            {
                Id = "davidsCallJohn",
                IsMeetingRequest = false,
                OwningResourceId = "davids",
                OwningCalendarId = "davidsCalendar",
                Subject = ServiceResources.Appt_DavidsCallJohn_Subject
            };
            davidsCallJohn.Start = dateCreator.CreateDate(2, 15, 0);
            davidsCallJohn.End = dateCreator.CreateDate(2, 15, 30);

            Appointments.Add(davidsCallJohn);

            foreach (Appointment appt in Appointments)
            {
                appt.StartTimeZoneId = TimeZoneInfo.Local.Id;
                appt.EndTimeZoneId = TimeZoneInfo.Local.Id;
            }

        }

        #endregion //Data

        public Appointment GetAppointment(string id)
        {
            return Appointments.FirstOrDefault(a => a.Id == id);
        }

        public void GenerateAssociatedAppointments(Appointment appointment)
        {
            if (appointment.To == null)
                return;

            appointment.AppointmentIds = new ObservableCollection<string>();

            var organizer = Resources.FirstOrDefault(r => r.Id == appointment.OwningResourceId);

            if (organizer == null) return;

            List<Appointment> generatedAppointments = new List<Appointment>();

            foreach (var email in appointment.To)
            {
                var resource = Resources.FirstOrDefault(r => r.EmailAddress == email);

                if (resource == null) continue;

                var calendar = ResourceCalendars.FirstOrDefault(c => c.OwningResourceId == resource.Id);

                if (calendar == null) continue;

                Appointment appt = new Appointment();
                appointment.CopyPropertiesTo(appt, "Id", "To", "OwningResourceId", "OwningCalendarId", "AppointmentIds");

                appt.Id = appointment.Id + resource.Id;
                appt.OwningResourceId = resource.Id;
                appt.OwningCalendarId = calendar.Id;

                appt.To = new ObservableCollection<string>(appointment.To);
                appt.To.Remove(resource.EmailAddress);
                appt.To.Add(organizer.EmailAddress);

                appointment.AppointmentIds.Add(appt.Id);
                Appointments.Add(appt);
                generatedAppointments.Add(appt);
            }

            foreach (var appt in generatedAppointments)
            {
                appt.AppointmentIds = new ObservableCollection<string>(appointment.AppointmentIds);
                appt.AppointmentIds.Remove(appt.Id);
                appt.AppointmentIds.Add(appointment.Id);
            }

        }

        public void UpdateAssociatedAppointments(Appointment appointment)
        {
            if (appointment.To == null)
                return;

            var organizer = Resources.FirstOrDefault(r => r.Id == appointment.OwningResourceId);

            if (organizer == null) return;

            foreach (var id in appointment.AppointmentIds)
            {
                var appt = Appointments.FirstOrDefault(a => a.Id == id);

                if (appt == null) continue;

                appointment.CopyPropertiesTo(appt, "Id", "To", "RootActivityId", "OwningResourceId", "OwningCalendarId", "AppointmentIds");
            }
        }

        public void DeleteAssociatedAppointments(Appointment appointment)
        {
            if (appointment.To == null)
                return;

            foreach (var id in appointment.AppointmentIds)
            {
                var appt = Appointments.FirstOrDefault(a => a.Id == id);
                if (appt == null) continue;
                Appointments.Remove(appt);
            }
        }

        public void GenerateAssociatedVarianceAppointments(Appointment appointment)
        {
            if (appointment.To == null)
                return;

            var organizer = Resources.FirstOrDefault(r => r.Id == appointment.OwningResourceId);

            if (organizer == null) return;

            var originalAppointmentIds = new ObservableCollection<string>(appointment.AppointmentIds);
            appointment.AppointmentIds = new ObservableCollection<string>();

            List<Appointment> generatedAppointments = new List<Appointment>();

            foreach (var id in originalAppointmentIds)
            {
                var appt = Appointments.FirstOrDefault(a => a.Id == id);

                if (appt == null) continue;

                Appointment apptNew = new Appointment();
                appointment.CopyPropertiesTo(apptNew, "Id", "RootActivityId", "Recurrence", "RecurrenceVersion");

                if (appt.RootActivityId == null)
                    apptNew.Id = appt.Id + appointment.Id.Replace(appointment.RootActivityId, string.Empty);
                else
                    apptNew.Id = appt.Id;

                apptNew.RootActivityId = id;
                apptNew.OwningCalendarId = appt.OwningCalendarId;
                apptNew.OwningResourceId = appt.OwningResourceId;

                Appointments.Add(apptNew);
                generatedAppointments.Add(apptNew);
                appointment.AppointmentIds.Add(apptNew.Id);
            }

            foreach (var appt in generatedAppointments)
            {
                appt.AppointmentIds = new ObservableCollection<string>(appointment.AppointmentIds);
                appt.AppointmentIds.Remove(appt.Id);
                appt.AppointmentIds.Add(appointment.Id);
            }
        }
    }

    public class DateCreator
    {
        public DateTime InitialDate
        {
            get;
            set;
        }

        public DateCreator(DateTime initialDate)
        {
            this.InitialDate = initialDate;
        }

        public DateTime CreateDate(int dayOffset, int hours, int minutes)
        {
            DateTime result = new DateTime(InitialDate.Year, InitialDate.Month, InitialDate.Day, hours, minutes, 0);
            if (dayOffset != 0)
                result = result.AddDays(dayOffset);
            return result.ToUniversalTime();
        }
    }
}
