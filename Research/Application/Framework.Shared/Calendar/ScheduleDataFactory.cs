using Infragistics.Scheduler;
using Infragistics.Scheduler.Data;
using Infragistics.XamarinForms.Controls.Scheduler;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infragistics.Framework
{ 
    public class DailySchedule
    {
        public ScheduleListDataSource Data { get; set; }

        public DailySchedule() : this(30, 30)
        {
        }

        public DailySchedule(int pastDays, int futureDays)
        {
            var appointments = new List<Appointment>();

            var random = new Random();
            var maxDate = DateTime.Now.AddDays(futureDays);
            var current = DateTime.Now.AddDays(-pastDays);

            while (current.Ticks < maxDate.Ticks)
            {
                var timeStart = current.SetTime(random.Next(8, 10), 0, 0);
                var timeEnd = timeStart.AddMinutes(random.Next(1, 4) * 15);

                if (timeStart.DayOfWeek == DayOfWeek.Saturday || timeStart.DayOfWeek == DayOfWeek.Sunday)
                {
                    var item = ScheduleDataFactory.WeekendAppointment(current);
                    appointments.Add(item);
                    current = current.AddDays(1);
                }
                else
                {
                    var meetings = ScheduleDataFactory.DailyAppointments(current);
                    appointments.AddRange(meetings);

                    current = current.AddDays(1);
                }
            }
            Data = new ScheduleListDataSource();
            Data.AppointmentItemsSource = appointments;
            Data.ResourceItemsSource = ScheduleDataFactory.Resources;
        }
    }

    public class WorkSchedule  
    {
        public ScheduleListDataSource Data { get; set; }

        public WorkSchedule() : this(30, 30)
        {     
        } 
        public WorkSchedule(int pastDays, int futureDays)
        {
            var appointments = new List<Appointment>();

            var random = new Random();
            var maxDate = DateTime.Now.AddDays(futureDays);
            var current = DateTime.Now.AddDays(-pastDays);

            while (current.Ticks < maxDate.Ticks)
            {
                var timeStart = current.SetTime(random.Next(8, 10), 0, 0);
                var timeEnd = timeStart.AddMinutes(random.Next(1, 4) * 15);

                if (timeStart.DayOfWeek == DayOfWeek.Saturday || timeStart.DayOfWeek == DayOfWeek.Sunday)
                {
                    var item = ScheduleDataFactory.WeekendAppointment(timeStart);
                    appointments.Add(item);
                    current = current.AddDays(1);
                }
                else
                {
                    var meetings = random.Next(1, 4);
                    for (int i = 0; i < meetings; i++)
                    {
                        var item = ScheduleDataFactory.WorkAppointment(timeStart, timeEnd);
                        appointments.Add(item);

                        timeStart = timeEnd.AddMinutes(random.Next(1, 6) * 15);
                        timeEnd = timeStart.AddMinutes(random.Next(1, 4) * 15);
                    }
                    current = current.AddDays(1);
                }
            }

            // add some all-day appointment
            ScheduleDataFactory.CreateAllDayAppointments(DateTime.Today, appointments);

            Data = new ScheduleListDataSource();
            Data.AppointmentItemsSource = appointments;
            Data.ResourceItemsSource = ScheduleDataFactory.Resources;
        } 
    }

    public class ProjectSchedule
    {
        public ScheduleListDataSource Data { get; set; }
        
        public ProjectSchedule()
        {
            var names = AppResources.ProjectNamesList.Split(',');
            var locations = AppResources.ProjectLocationsList.Split(',');

            var appointments = new List<Appointment>();
            var random = new Random();
            var current = DateTime.Now;

            var timeStart = current.AddDays(-30).SetTime(8, 0, 0);
            var timeEnd = timeStart.AddDays(random.Next(4, 5));

            for (int i = 0; i < names.Length; i++)
            { 
                var item = new Appointment();
                item.Start = timeStart;
                item.End = timeEnd;
                item.ResourceId = ScheduleDataFactory.Resources.Random().Id;
                item.Location = locations[i];
                item.Subject = names[i];
                appointments.Add(item);

                timeStart = item.End.AddDays(1).SetTime(8, 0, 0);
                timeEnd = timeStart.AddDays(random.Next(6, 10)); 
            }            

            Data = new ScheduleListDataSource();
            Data.AppointmentItemsSource = appointments;
            Data.ResourceItemsSource = ScheduleDataFactory.Resources;
        }
    }

    public static class ScheduleDataFactory
    {
        public static string[] WorkMeetings = AppResources.WorkMeetingsList.Split(',');
        public static string[] WorkLocations = AppResources.WorkLocationsList.Split(',');
        public static string[] LifeMeetings = AppResources.LifeMeetingsList.Split(',');
        public static string[] LifeLocations = AppResources.LifeLocationsList.Split(',');
        public static string[] DailyTasks = AppResources.LifeActivityNames.Split(',');
        public static string[] DailyPlaces = AppResources.LifeActivityPlaces.Split(',');

        public static string[] Names = AppResources.WorkMeetingsList.Split(',');
        public static string[] Locations = AppResources.WorkLocationsList.Split(',');
        public static string[] ResNames = AppResources.ResourceNamesList.Split(',');

        public static List<ScheduleResource> Resources { get; internal set; }

        internal static int DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        internal static DateTime MonthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddDays(-1);
        internal static Random random = new Random();
   
        static ScheduleDataFactory()
        {
            CreateResource();
        }
        
        internal static void CreateResource()
        {
            Resources = new List<ScheduleResource>();

            var schemes = Enum.GetValues(typeof(ScheduleResourceColorScheme));
            var schemesList = schemes.Cast<ScheduleResourceColorScheme>().ToList();

            for (int i = 0; i < 3; i++)
            {
                var schemeId = ResNames[i];
                var schemeColor = schemesList[i+2];
                Resources.Add(new ScheduleResource(schemeId) { ColorScheme = schemeColor });
            }
        }

        public static List<Appointment> DailyAppointments(DateTime current)
        {
            var appointments = new List<Appointment>();
             
            var timeStart = current.SetTime(random.Next(8, 10), 0, 0);
            var timeEnd = timeStart.AddHours(random.Next(2, 4));
             
            if (timeStart.DayOfWeek == DayOfWeek.Thursday ||
                timeStart.DayOfWeek == DayOfWeek.Tuesday)
            {
                var learining = new Appointment();
                learining.Start = current.SetTime(10, 0, 0);
                learining.End = learining.Start.AddHours(random.Next(5, 7));
                learining.ResourceId = Resources.Random().Id;
                learining.Location = DailyPlaces[1];
                learining.Subject = DailyTasks[1];
                appointments.Add(learining);

                var studying = new Appointment();
                studying.Start = learining.End.AddHours(1);
                studying.End = studying.Start.AddHours(random.Next(2, 5));
                studying.ResourceId = Resources.Random().Id;
                studying.Location = DailyPlaces[2];
                studying.Subject = DailyTasks[2];
                appointments.Add(studying);
            }
            else if (timeStart.DayOfWeek == DayOfWeek.Monday ||
                     timeStart.DayOfWeek == DayOfWeek.Wednesday ||
                     timeStart.DayOfWeek == DayOfWeek.Friday)
            {
                var communting = new Appointment();
                communting.Start = current.SetTime(9, 0, 0);
                communting.End = communting.Start.AddMinutes(random.Next(20, 30));
                communting.ResourceId = Resources.Random().Id;
                communting.Location = DailyPlaces[3];
                communting.Subject = DailyTasks[3];
                appointments.Add(communting);

                var working = new Appointment();
                working.Start = current.SetTime(9, 30, 0);
                working.End = working.Start.AddHours(random.Next(8, 9));
                working.ResourceId = Resources.Random().Id;
                working.Location = DailyPlaces[4];
                working.Subject = DailyTasks[4];
                appointments.Add(working);

                var watching = new Appointment();
                watching.Start = current.SetTime(20, 0, 0);
                watching.End = watching.Start.AddHours(random.Next(2, 4));
                watching.ResourceId = Resources.Random().Id;
                watching.Location = DailyPlaces[5];
                watching.Subject = DailyTasks[5];
                appointments.Add(watching);
            }

            return appointments;
        }

        public static Appointment WorkAppointment(DateTime timeStart, DateTime timeEnd)
        {
            var item = new Appointment();
            item.Start = timeStart;
            item.End = timeEnd;
            item.ResourceId = Resources.Random().Id;
            item.Location = WorkLocations.Random();
            item.Subject = WorkMeetings.Random();
            item.Subject += " " + AppResources.WorkMeeting;
            return item;
        }

        public static Appointment WeekendAppointment(DateTime timeStart)
        {
            var item = new Appointment();
            item.Start = timeStart.SetTime(7, 0, 0);
            item.End = item.Start.AddHours(40);
            item.ResourceId = Resources.Random().Id;
            item.Location = LifeLocations.Random();
            item.Subject = LifeMeetings.Random();
            return item;
        }

        public static void CreateAllDayAppointments(DateTime date, List<Appointment> appointments)
        {
            Appointment allDay01 = new Appointment();
            allDay01.Subject = LifeMeetings.Random();
            allDay01.Location = LifeLocations.Random();
            allDay01.Start = new DateTime(date.Year, date.Month, date.Day);
            allDay01.End = new DateTime(date.Year, date.Month, date.Day);
            allDay01.IsAllDay = true;
            appointments.Add(allDay01);

            date = date.AddDays(1);

            Appointment allDay02 = new Appointment();
            allDay02.Subject = LifeMeetings.Random();
            allDay02.Location = LifeLocations.Random();
            allDay02.Start = new DateTime(date.Year, date.Month, date.Day);
            allDay02.End = new DateTime(date.Year, date.Month, date.Day);
            allDay02.IsAllDay = true;
            appointments.Add(allDay02);
        }

        public static Appointment CreateAppointment(DateTime date)
        {
            var timeStart = date.AddHours(random.Next(1, 3));
            var timeEnd = timeStart.AddMinutes(random.Next(1, 4) * 15);

            var item = new Appointment();
            item.Start = timeStart;
            item.End = timeEnd;
            item.ResourceId = Resources.Random().Id;
            item.Location = Locations.Random();
            item.Subject = Names.Random();
            item.Subject += " " + AppResources.WorkMeeting;

            return item;
        }

        public static ScheduleListDataSource CreateData(int itemsOld = 30, int itemsNew = 60)
        {
            Func<DateTime, int, int, int, DateTime> MakeDateTime = (d, h, m, s) =>
            {
                return new DateTime(d.Year, d.Month, d.Day, h, m, s);
            };

            var appointments = new List<Appointment>();
            var date = DateTime.Now.AddDays(-itemsOld);

            for (int i = 0; i < itemsOld; i++)
            {
                date = MakeDateTime(date, 8, 0, 0);
                var appt1 = CreateAppointment(date);
                appt1.Subject = AppResources.WorkDailyMeeting;
                appt1.End = appt1.Start.AddMinutes(random.Next(2, 3) * 15);
                appointments.Add(appt1);

                if (random.Next(0, 3) % 2 == 0)
                {
                    var appt2 = CreateAppointment(appt1.End);
                    appointments.Add(appt2);
                }
                date = date.AddDays(1);
            }

            date = MakeDateTime(DateTime.Now, 6, 0, 0);
            for (int i = 0; i < itemsNew; i++)
            {
                date = MakeDateTime(date, 8, 0, 0);
                var appt1 = CreateAppointment(date);
                appt1.Subject = AppResources.WorkDailyMeeting;
                appt1.End = appt1.Start.AddMinutes(random.Next(2, 3) * 15);
                appointments.Add(appt1);

                var appt2 = CreateAppointment(appt1.End);
                if (random.Next(0, 3) % 2 == 0)
                {
                    appointments.Add(appt2);
                }
                if (random.Next(0, 6) % 2 == 0)
                {
                    var appt3 = CreateAppointment(appt2.End);
                    appointments.Add(appt3);
                }
                date = date.AddDays(1);
            }

            var dataSource = new ScheduleListDataSource();
            dataSource.AppointmentItemsSource = appointments;
            dataSource.ResourceItemsSource = Resources;
            return dataSource;
        } 
    } 
}
