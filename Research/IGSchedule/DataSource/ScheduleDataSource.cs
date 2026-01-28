using IGSchedule.Resources;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Shared.DataProviders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Media;
using System.Xml.Linq;

namespace IGSchedule.DataSource
{
    public delegate void DataLoadingCompletedEventHandler(object sender, EventArgs e);

    public class ScheduleData
    {
        public event DataLoadingCompletedEventHandler DataLoadingCompleted;

        public ScheduleData()
        {
            XmlDataProvider _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("ScheduleData.xml"); // xml file name
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            int DaysOffset =  DateTime.Now.Subtract(new DateTime(2010, 7, 9)).Days;
            XDocument doc = e.Result;

            var resources = doc.Element("root").Elements("Resources").Where(r => r.Element("Id").Value != "own10").Select(r =>
                    new Resource
                    {
                        Id = r.Element("Id").Value,
                        Name = r.Element("Name").Value
                    }).ToList();
            LoadData(resources, _resources);

            var calendars = doc.Element("root").Elements("Calendars").Select(r =>
                new ResourceCalendar
                {
                    Id = r.Element("Id").Value,
                    Name = r.Element("Name").Value,
                    OwningResourceId = r.Element("ResourceId").Value
                }).ToList();
            LoadData(calendars, _calendars);

            var appointments = doc.Element("root").Elements("Appointments").Select(r =>
                new Appointment
                {
                    Id = r.Element("Id").Value,
                    Start = DateTime.Parse(r.Element("Start").Value).AddDays(DaysOffset).ToUniversalTime(),
                    End = DateTime.Parse(r.Element("End").Value).AddDays(DaysOffset).ToUniversalTime(),
                    OwningResourceId = r.Element("OwnerId").Value,
                    OwningCalendarId = r.Element("CalendarId").Value,
                    Subject = r.Element("Subject").Value,
                    Description = r.Element("Description").Value,
                    ReminderEnabled = bool.Parse(r.Element("Reminder").Value),
                    Reminder = new Reminder { IsSnoozed = false, SnoozeInterval = new TimeSpan(0, 0, 40), Text = r.Element("Subject").Value },

                    //Categories
                });
            LoadData(appointments, _appointments);

            var tasks = doc.Element("root").Elements("Tasks").Select(r =>
                new Task
                {
                    Id = r.Element("Id").Value,
                    Start = DateTime.Parse(r.Element("Start").Value).AddDays(DaysOffset).ToUniversalTime(),
                    End = DateTime.Parse(r.Element("End").Value).AddDays(DaysOffset).ToUniversalTime(),
                    OwningResourceId = r.Element("OwnerId").Value,
                    OwningCalendarId = r.Element("CalendarId").Value,
                    Subject = r.Element("Subject").Value,
                    Description = r.Element("Description").Value,
                    PercentComplete = int.Parse(r.Element("PercentageComplete").Value)
                });
            LoadData(tasks, _tasks);

            var journals = doc.Element("root").Elements("Journals").Select(r =>
            new Journal
            {
                Id = r.Element("Id").Value,
                Start = DateTime.Parse(r.Element("Start").Value).AddDays(DaysOffset).ToUniversalTime(),
                End = DateTime.Parse(r.Element("End").Value).AddDays(DaysOffset).ToUniversalTime(),
                OwningResourceId = r.Element("OwnerId").Value,
                OwningCalendarId = r.Element("CalendarId").Value,
                Subject = r.Element("Subject").Value,
                Description = r.Element("Description").Value
            });
            LoadData(journals, _journals);

            // invoking all the event handlers
            if (DataLoadingCompleted != null)
            {
                DataLoadingCompleted(this, null);
            }
        }

        private void LoadData<T>(IEnumerable<T> source, IList<T> destination)
        {
            foreach (T item in source)
                destination.Add(item);
        }

        private IList<Resource> _resources = new ObservableCollection<Resource>();
        public IList<Resource> Resources
        {
            get
            {
                return _resources;
            }
        }

        private IList<ResourceCalendar> _calendars = new ObservableCollection<ResourceCalendar>();
        public IList<ResourceCalendar> Calendars
        {
            get
            {
                return _calendars;
            }
        }

        private IList<Appointment> _appointments = new ObservableCollection<Appointment>();
        public IList<Appointment> Appointments
        {
            get
            {
                return _appointments;
            }
        }

        private IList<Task> _tasks = new ObservableCollection<Task>();
        public IList<Task> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        private IList<Journal> _journals = new ObservableCollection<Journal>();
        public IList<Journal> Journals
        {
            get
            {
                return _journals;
            }
        }

        private List<TimeSpan> _hours = new List<TimeSpan>();
        public List<TimeSpan> GetHours(TimeSpan? Start, TimeSpan? End)
        {
            LoadHours();
            var query = _hours.AsQueryable();
            if (Start != null)
                query = query.Where(ts => ts > Start);
            if (End != null)
                query = query.Where(ts => ts < End);
            return query.ToList();
        }
        private void LoadHours()
        {
            if (_hours.Count == 0)
            {
                for (int i = 0; i < 24; i++)
                {
                    _hours.Add(new TimeSpan(i, 0, 0));
                    _hours.Add(new TimeSpan(i, 30, 0));
                }
            }
        }
    }

    public class MyScheduleData
    {
        public event DataLoadingCompletedEventHandler DataLoadingCompleted;

        public MyScheduleData()
        {
            XmlDataProvider _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("ScheduleData.xml"); // xml file name
        }
        
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            int DaysOffset =  DateTime.Now.Subtract(new DateTime(2010, 7, 9)).Days;
            XDocument doc = e.Result;

            var resources = doc.Element("root").Elements("Resources").Where(r => r.Element("Id").Value != "own10").Select(r =>
                    new MyResource
                    {
                        Id1 = r.Element("Id").Value,
                        Name1 = r.Element("Name").Value
                    }).ToList();
            LoadData(resources, _resources);

            var calendars = doc.Element("root").Elements("Calendars").Select(r =>
                new MyCalendar
                {
                    Id1 = r.Element("Id").Value,
                    Name1 = r.Element("Name").Value,
                    ResourceId1 = r.Element("ResourceId").Value
                }).ToList();
            LoadData(calendars, _calendars);

            var appointments = doc.Element("root").Elements("Appointments").Select(r =>
                new MyAppointment
                {
                    Id1 = r.Element("Id").Value,
                    Start1 = DateTime.Parse(r.Element("Start").Value).AddDays(DaysOffset).ToUniversalTime(),
                    End1 = DateTime.Parse(r.Element("End").Value).AddDays(DaysOffset).ToUniversalTime(),
                    OwnerId1 = r.Element("OwnerId").Value,
                    CalendarId1 = r.Element("CalendarId").Value,
                    Subject1 = r.Element("Subject").Value,
                    Text1 = r.Element("Description").Value
                });
            LoadData(appointments, _appointments);

            //Move one of the appointments to recurring collection
            MyAppointment appointment8 = _appointments.FirstOrDefault(t => t.Id1 == "8");
            if (appointment8 != null)
            {
                _appointments.Remove(appointment8);

                DateRecurrence dateRecurrence = new DateRecurrence();
                var recuRules = new List<DateRecurrenceRuleBase>();
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Monday, 0));
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Wednesday, 0));
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Friday, 0));
                recuRules.Add(new DayOfWeekRecurrenceRule(DayOfWeek.Sunday, 0));
                dateRecurrence.Frequency = DateRecurrenceFrequency.Weekly;
                dateRecurrence.Rules.AddRange(recuRules);

                MyRecAppointment _newRecAppointment = new MyRecAppointment
                {
                    Id1 = appointment8.Id1,
                    Start1 = appointment8.Start1,
                    End1 = appointment8.End1,
                    OwnerId1 = appointment8.OwnerId1,
                    CalendarId1 = appointment8.CalendarId1,
                    Subject1 = appointment8.Subject1,
                    Text1 = appointment8.Text1 + ScheduleStrings.Recurrent,
                    Recurrence1 = dateRecurrence
                };
                _recAppointments.Add(_newRecAppointment);
            }

            // invoking all the event handlers
            if (DataLoadingCompleted != null)
            {
                DataLoadingCompleted(this, null);
            }
        }

        private void LoadData<T>(IEnumerable<T> source, IList<T> destination)
        {
            foreach (T item in source)
                destination.Add(item);
        }

        private IList<MyResource> _resources = new ObservableCollection<MyResource>();
        public IList<MyResource> Resources
        {
            get
            {
                return _resources;
            }
        }

        private IList<MyCalendar> _calendars = new ObservableCollection<MyCalendar>();
        public IList<MyCalendar> Calendars
        {
            get
            {
                return _calendars;
            }
        }

        private IList<MyAppointment> _appointments = new ObservableCollection<MyAppointment>();
        public IList<MyAppointment> Appointments
        {
            get
            {
                return _appointments;
            }
        }

        private IList<MyRecAppointment> _recAppointments = new ObservableCollection<MyRecAppointment>();
        public IList<MyRecAppointment> RecurringAppointments
        {
            get
            {
                return _recAppointments;
            }
        }
    }

    public class MyResource
    {
        public string Id1 { get; set; }
        public string Name1 { get; set; }
    }

    public class MyCalendar
    {
        public string Id1 { get; set; }
        public string Name1 { get; set; }
        public string ResourceId1 { get; set; }
    }

    public class MyAppointment
    {
        public string Id1 { get; set; }
        public string Text1 { get; set; }
        public string OwnerId1 { get; set; }
        public string CalendarId1 { get; set; }
        public string Subject1 { get; set; }
        public DateTime Start1 { get; set; }
        public DateTime End1 { get; set; }
    }

    public class MyRecAppointment : MyAppointment
    {
        public RecurrenceBase Recurrence1 { get; set; }
    }

    public class TZScheduleData
    {
        public event DataLoadingCompletedEventHandler DataLoadingCompleted;

        public TZScheduleData()
        {
            XmlDataProvider _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("ScheduleData.xml"); // xml file name
        }
        
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            int DaysOffset =  DateTime.Now.Subtract(new DateTime(2010, 7, 9)).Days;
            XDocument doc = e.Result;

            var resources = doc.Element("root").Elements("Resources").Where(r => r.Element("Id").Value != "own10").Select(r =>
                    new Resource
                    {
                        Id = r.Element("Id").Value,
                        Name = r.Element("Name").Value
                    }).ToList();
            LoadData(resources, _resources);

            var calendars = doc.Element("root").Elements("Calendars").Select(r =>
                new ResourceCalendar
                {
                    Id = r.Element("Id").Value,
                    Name = r.Element("Name").Value,
                    OwningResourceId = r.Element("ResourceId").Value
                }).ToList();
            LoadData(calendars, _calendars);

            var appointments = doc.Element("root").Elements("TZAppointments").Select(r =>
                new Appointment
                {
                    Id = r.Element("Id").Value,
                    Start = DateTime.Parse(r.Element("Start").Value).AddDays(DaysOffset).ToUniversalTime(),
                    End = DateTime.Parse(r.Element("End").Value).AddDays(DaysOffset).ToUniversalTime(),
                    OwningResourceId = r.Element("OwnerId").Value,
                    OwningCalendarId = r.Element("CalendarId").Value,
                    Subject = r.Element("Subject").Value,
                    Description = r.Element("Description").Value,
                    ReminderEnabled = bool.Parse(r.Element("Reminder").Value),
                    Reminder = new Reminder { IsSnoozed = false, SnoozeInterval = new TimeSpan(0, 0, 40), Text = r.Element("Subject").Value },
                    IsTimeZoneNeutral = bool.Parse(r.Element("IsTimeZoneNeutral").Value)
                });
            LoadData(appointments, _appointments);

            // invoking all the event handlers
            if (DataLoadingCompleted != null)
            {
                DataLoadingCompleted(this, null);
            }
        }

        private void LoadData<T>(IEnumerable<T> source, IList<T> destination)
        {
            foreach (T item in source)
                destination.Add(item);
        }

        private IList<Resource> _resources = new ObservableCollection<Resource>();
        public IList<Resource> Resources
        {
            get
            {
                return _resources;
            }
        }

        private IList<ResourceCalendar> _calendars = new ObservableCollection<ResourceCalendar>();
        public IList<ResourceCalendar> Calendars
        {
            get
            {
                return _calendars;
            }
        }

        private IList<Appointment> _appointments = new ObservableCollection<Appointment>();
        public IList<Appointment> Appointments
        {
            get
            {
                return _appointments;
            }
        }

        private IList<Task> _tasks = new ObservableCollection<Task>();
        public IList<Task> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        private IList<Journal> _journals = new ObservableCollection<Journal>();
        public IList<Journal> Journals
        {
            get
            {
                return _journals;
            }
        }

        private List<TimeSpan> _hours = new List<TimeSpan>();
        public List<TimeSpan> GetHours(TimeSpan? Start, TimeSpan? End)
        {
            LoadHours();
            var query = _hours.AsQueryable();
            if (Start != null)
                query = query.Where(ts => ts > Start);
            if (End != null)
                query = query.Where(ts => ts < End);
            return query.ToList();
        }

        private void LoadHours()
        {
            if (_hours.Count == 0)
            {
                for (int i = 0; i < 24; i++)
                {
                    _hours.Add(new TimeSpan(i, 0, 0));
                    _hours.Add(new TimeSpan(i, 30, 0));
                }
            }
        }
    }

    public class ScheduleDataCategorized
    {
        public ScheduleDataCategorized()
        {
            XmlDataProvider _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("ScheduleData.xml"); // xml file name
        }
        
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            int DaysOffset =  DateTime.Now.Subtract(new DateTime(2010, 7, 9)).Days;
            XDocument doc = e.Result;

            var resources = doc.Element("root").Elements("Resources").Where(r => r.Element("Id").Value != "own10").Select(r =>
                    new Resource
                    {
                        Id = r.Element("Id").Value,
                        Name = r.Element("Name").Value
                    }).ToList();
            LoadData(resources, _resources);

            var calendars = doc.Element("root").Elements("Calendars").Select(r =>
                new ResourceCalendar
                {
                    Id = r.Element("Id").Value,
                    Name = r.Element("Name").Value,
                    OwningResourceId = r.Element("ResourceId").Value
                }).ToList();
            LoadData(calendars, _calendars);

            var categories = doc.Element("root").Elements("Categories").Select(r =>
                new ActivityCategory
                {
                    CategoryName = r.Element("CategoryName").Value,
                    Description = r.Element("Description").Value,
                    Color = DecodeColor(r.Element("Color").Value)
                }).ToList();
            LoadData(categories, _categories);

            var appointments = doc.Element("root").Elements("CatAppointments").Select(r =>
                new Appointment
                {
                    Id = r.Element("Id").Value,
                    Start = DateTime.Parse(r.Element("Start").Value).AddDays(DaysOffset).ToUniversalTime(),
                    End = DateTime.Parse(r.Element("End").Value).AddDays(DaysOffset).ToUniversalTime(),
                    OwningResourceId = r.Element("OwnerId").Value,
                    OwningCalendarId = r.Element("CalendarId").Value,
                    Subject = r.Element("Subject").Value,
                    Description = r.Element("Description").Value,
                    ReminderEnabled = bool.Parse(r.Element("Reminder").Value),
                    Reminder = new Reminder { IsSnoozed = false, SnoozeInterval = new TimeSpan(0, 0, 40), Text = r.Element("Subject").Value },
                    Categories = r.Element("Categories").Value
                });
            LoadData(appointments, _appointments);
        }

        private Color DecodeColor(string ColorValues)
        {
            string[] values = ColorValues.Split(',');
            if (values.Length == 3)
            {
                byte r = byte.Parse(values[0]);
                byte g = byte.Parse(values[1]);
                byte b = byte.Parse(values[2]);
                return Color.FromArgb(255, r, g, b);
            }
            return Colors.Purple; // default color used, when no color is set for this activity category
        }

        private void LoadData<T>(IEnumerable<T> source, IList<T> destination)
        {
            foreach (T item in source)
                destination.Add(item);
        }

        private IList<Resource> _resources = new ObservableCollection<Resource>();
        public IList<Resource> Resources
        {
            get
            {
                return _resources;
            }
        }

        private IList<ResourceCalendar> _calendars = new ObservableCollection<ResourceCalendar>();
        public IList<ResourceCalendar> Calendars
        {
            get
            {
                return _calendars;
            }
        }

        private IList<Appointment> _appointments = new ObservableCollection<Appointment>();
        public IList<Appointment> Appointments
        {
            get
            {
                return _appointments;
            }
        }

        private IList<Task> _tasks = new ObservableCollection<Task>();
        public IList<Task> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        private IList<Journal> _journals = new ObservableCollection<Journal>();
        public IList<Journal> Journals
        {
            get
            {
                return _journals;
            }
        }

        private IList<ActivityCategory> _categories = new ObservableCollection<ActivityCategory>();
        public IList<ActivityCategory> Categories
        {
            get
            {
                return _categories;
            }
        }

        private List<TimeSpan> _hours = new List<TimeSpan>();
        public List<TimeSpan> GetHours(TimeSpan? Start, TimeSpan? End)
        {
            LoadHours();
            var query = _hours.AsQueryable();
            if (Start != null)
                query = query.Where(ts => ts > Start);
            if (End != null)
                query = query.Where(ts => ts < End);
            return query.ToList();
        }

        private void LoadHours()
        {
            if (_hours.Count == 0)
            {
                for (int i = 0; i < 24; i++)
                {
                    _hours.Add(new TimeSpan(i, 0, 0));
                    _hours.Add(new TimeSpan(i, 30, 0));
                }
            }
        }
    }

    public class ScheduleDataCategorizedRecurrence
    {
        public ScheduleDataCategorizedRecurrence()
        {
            XmlDataProvider _xmlDataProvider = new XmlDataProvider();
            _xmlDataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _xmlDataProvider.GetXmlDataAsync("ScheduleData.xml"); // xml file name
        }
        
        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            int mondayOffset = -1 * ((int)DateTime.Now.DayOfWeek - 1);
            int DaysOffset = DateTime.Now.AddDays(mondayOffset).Subtract(new DateTime(2010, 7, 6)).Days;
            XDocument doc = e.Result;

            var resources = doc.Element("root").Elements("Resources").Where(r => r.Element("Id").Value == "own10").Select(r =>
                    new Resource
                    {
                        Id = r.Element("Id").Value,
                        Name = r.Element("Name").Value
                    }).ToList();
            LoadData(resources, _resources);

            var calendars = doc.Element("root").Elements("Calendars").Select(r =>
                new ResourceCalendar
                {
                    Id = r.Element("Id").Value,
                    Name = r.Element("Name").Value,
                    OwningResourceId = r.Element("ResourceId").Value
                }).ToList();
            LoadData(calendars, _calendars);

            var categories = doc.Element("root").Elements("Categories").Select(r =>
                new ActivityCategory
                {
                    CategoryName = r.Element("CategoryName").Value,
                    Description = r.Element("Description").Value,
                    Color = DecodeColor(r.Element("Color").Value)
                }).ToList();
            LoadData(categories, _categories);

            var appointments = doc.Element("root").Elements("CatAppointments").Select(r =>
                new Appointment
                {
                    Id = r.Element("Id").Value,
                    Start = DateTime.Parse(r.Element("Start").Value).AddDays(DaysOffset).ToUniversalTime(),
                    End = DateTime.Parse(r.Element("End").Value).AddDays(DaysOffset).ToUniversalTime(),
                    Recurrence = bool.Parse(r.Element("Recurrent").Value) ? new DateRecurrence() : null,
                    OwningResourceId = r.Element("OwnerId").Value,
                    OwningCalendarId = r.Element("CalendarId").Value,
                    Subject = r.Element("Subject").Value,
                    Location = r.Element("Location").Value,
                    Description = r.Element("Description").Value,
                    ReminderEnabled = bool.Parse(r.Element("Reminder").Value),
                    Reminder = new Reminder { IsSnoozed = false, SnoozeInterval = new TimeSpan(0, 0, 40), Text = r.Element("Subject").Value },
                    Categories = r.Element("Categories").Value
                }).ToList();
            foreach (var appointment in appointments.Where(t => t.Recurrence != null))
            {
                DateRecurrence appDateRecurrence = (DateRecurrence)appointment.Recurrence;
                appDateRecurrence.Frequency = DateRecurrenceFrequency.Weekly;
                appDateRecurrence.Rules.Add(new DayOfWeekRecurrenceRule(appointment.Start.DayOfWeek, 0));
            }
            LoadData(appointments, _appointments);
        }

        private Color DecodeColor(string ColorValues)
        {
            string[] values = ColorValues.Split(',');
            if (values.Length == 3)
            {
                byte r = byte.Parse(values[0]);
                byte g = byte.Parse(values[1]);
                byte b = byte.Parse(values[2]);
                return Color.FromArgb(255, r, g, b);
            }
            return Colors.Purple; // default color used, when no color is set for this activity category
        }

        private void LoadData<T>(IEnumerable<T> source, IList<T> destination)
        {
            foreach (T item in source)
                destination.Add(item);
        }

        private IList<Resource> _resources = new ObservableCollection<Resource>();
        public IList<Resource> Resources
        {
            get
            {
                return _resources;
            }
        }

        private IList<ResourceCalendar> _calendars = new ObservableCollection<ResourceCalendar>();
        public IList<ResourceCalendar> Calendars
        {
            get
            {
                return _calendars;
            }
        }

        private IList<Appointment> _appointments = new ObservableCollection<Appointment>();
        public IList<Appointment> Appointments
        {
            get
            {
                return _appointments;
            }
        }

        private IList<Task> _tasks = new ObservableCollection<Task>();
        public IList<Task> Tasks
        {
            get
            {
                return _tasks;
            }
        }

        private IList<Journal> _journals = new ObservableCollection<Journal>();
        public IList<Journal> Journals
        {
            get
            {
                return _journals;
            }
        }

        private IList<ActivityCategory> _categories = new ObservableCollection<ActivityCategory>();
        public IList<ActivityCategory> Categories
        {
            get
            {
                return _categories;
            }
        }

        private List<TimeSpan> _hours = new List<TimeSpan>();
        public List<TimeSpan> GetHours(TimeSpan? Start, TimeSpan? End)
        {
            LoadHours();
            var query = _hours.AsQueryable();
            if (Start != null)
                query = query.Where(ts => ts > Start);
            if (End != null)
                query = query.Where(ts => ts < End);
            return query.ToList();
        }

        private void LoadHours()
        {
            if (_hours.Count == 0)
            {
                for (int i = 0; i < 24; i++)
                {
                    _hours.Add(new TimeSpan(i, 0, 0));
                    _hours.Add(new TimeSpan(i, 30, 0));
                }
            }
        }
    }
}
