using System; 
using System.Collections.Generic;  

#if PORTABLE
using Infragistics.Portable.Runtime;
#else
using System.Runtime.CompilerServices;
#endif

namespace Infragistics.Framework
{
    #region AppointmentDataGenerator class
    /// <summary>
    /// Generates an IList{AppointmentDataObject} or IEnumerable{AppointmentDataObject}
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataGenerator
    {
        private int _itemCount = 0;
        private int _resourceCount = 0;
        private DateTime _minDate;
        private DateTime _maxDate;

        private List<AppointmentDataObjectBase> _list;

        /// <summary>
        /// Creates a new instance with default values for the dates such
        /// that the entire range is constrained to one month in either direction
        /// of the current date, with start times between 8AM and 6PM.
        /// </summary>
        /// <param name="itemCount">The total number of items to be generated. Default = 1000.</param>
        /// <param name="resourceCount">The total number of resources to be generated. Default = 10.</param>
        /// <param name="implementsINotifyPropertyChanged">
        /// Specifies whether the instance returned implements the
        /// INotifyPropertyChanged interface. Useful for testing how
        /// we react to external changes to data objects.
        /// </param>
        /// <param name="hasDifferentPropertyNames">
        /// Specifies whether the AppointmentDataObjectBase instances have properties with different names
        /// than our appointment object, i.e., StartX, EndX, and SubjectX instead of Subject,
        /// Start, and End. Useful for testing custom property mappings.
        /// </param>
        public AppointmentDataGenerator(
            int itemCount = 1000,
            int resourceCount = 10,
            bool implementsINotifyPropertyChanged = true,
            bool hasDifferentPropertyNames = false,
            bool hasRecurrences = true) : this(
                DateTime.Today.AddMonths(-1),
                DateTime.Today.AddMonths(1),
                itemCount,
                resourceCount,
                implementsINotifyPropertyChanged,
                hasDifferentPropertyNames,
                hasRecurrences)

        {
        }

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="minDate">The minimum start date. No item's start date will be any less than this.</param>
        /// <param name="maxDate">The maximum start date. No item's start date will be greater than this.</param>
        /// <param name="itemCount">The total number of items to be generated. Default = 1000.</param>
        /// <param name="resourceCount">The total number of resources to be generated. Default = 10.</param>
        /// <param name="implementsINotifyPropertyChanged">
        /// Specifies whether the instance returned implements the
        /// INotifyPropertyChanged interface. Useful for testing how
        /// we react to external changes to data objects.
        /// </param>
        /// <param name="hasDifferentPropertyNames">
        /// Specifies whether the AppointmentDataObjectBase instances have properties with different names
        /// than our appointment object, i.e., StartX, EndX, and SubjectX instead of Subject,
        /// Start, and End. Useful for testing custom property mappings.
        /// </param>
        public AppointmentDataGenerator(
            DateTime minDate,
            DateTime maxDate,
            int itemCount = 1000,
            int resourceCount = 10,
            bool implementsINotifyPropertyChanged = true,
            bool hasDifferentPropertyNames = false,
            bool hasRecurrences = true)
        {
            if (resourceCount > itemCount)
                throw new ArgumentException("Can't have more resources than appointments.");

            this._minDate = minDate;
            this._maxDate = maxDate;
            this._itemCount = itemCount;
            this._resourceCount = resourceCount;

            this.ImplementsINotifyPropertyChanged = implementsINotifyPropertyChanged;
            this.HasDifferentPropertyNames = hasDifferentPropertyNames;
            this.HasRecurrences = hasRecurrences;

            this._list = new List<AppointmentDataObjectBase>(itemCount);
        }

        public bool ImplementsINotifyPropertyChanged { get; private set; }

        public bool HasDifferentPropertyNames { get; private set; }

        public bool HasRecurrences { get; private set; }

        static private TimeSpan[] Durations
        {
            get
            {
                return new TimeSpan[]
                {
                    TimeSpan.FromMinutes(10),
                    TimeSpan.FromMinutes(15),
                    TimeSpan.FromMinutes(20),
                    TimeSpan.FromMinutes(30),
                    TimeSpan.FromMinutes(45),
                    TimeSpan.FromMinutes(60),
                    TimeSpan.FromHours(12),
                    TimeSpan.FromHours(24),
                    TimeSpan.FromHours(36),
                    TimeSpan.FromHours(24),
                    TimeSpan.FromHours(48),
                    TimeSpan.FromMinutes(75),
                    TimeSpan.FromMinutes(90),
                    TimeSpan.FromMinutes(120),
                    TimeSpan.FromMinutes(30),
                    TimeSpan.FromMinutes(30),
                    TimeSpan.FromMinutes(15),
                    TimeSpan.FromMinutes(15),
                    TimeSpan.FromHours(8),
                    TimeSpan.FromMinutes(15),
                    TimeSpan.FromMinutes(60),
                    TimeSpan.FromMinutes(60),
                    TimeSpan.FromHours(24),
                    TimeSpan.FromMinutes(60),
                    TimeSpan.FromMinutes(30),
                    TimeSpan.FromHours(36),
                    TimeSpan.FromMinutes(30),
                    TimeSpan.FromHours(48),
                };
            }
        }

        static private TimeSpan[] StartTimes
        {
            get
            {
                return new TimeSpan[]
                {
                    TimeSpan.FromHours(8),
                    TimeSpan.FromHours(8.25),
                    TimeSpan.FromHours(8.5),
                    TimeSpan.FromHours(8.75),
                    TimeSpan.FromHours(9),
                    TimeSpan.FromHours(9.25),
                    TimeSpan.FromHours(9.5),
                    TimeSpan.FromHours(9.75),
                    TimeSpan.FromHours(10),
                    TimeSpan.FromHours(10.25),
                    TimeSpan.FromHours(10.5),
                    TimeSpan.FromHours(10.75),
                    TimeSpan.FromHours(11),
                    TimeSpan.FromHours(11.25),
                    TimeSpan.FromHours(11.5),
                    TimeSpan.FromHours(11.75),
                    TimeSpan.FromHours(12),
                    TimeSpan.FromHours(12.25),
                    TimeSpan.FromHours(12.5),
                    TimeSpan.FromHours(12.75),
                    TimeSpan.FromHours(13),
                    TimeSpan.FromHours(13.25),
                    TimeSpan.FromHours(13.5),
                    TimeSpan.FromHours(13.75),
                    TimeSpan.FromHours(14),
                    TimeSpan.FromHours(14.25),
                    TimeSpan.FromHours(14.5),
                    TimeSpan.FromHours(14.75),
                    TimeSpan.FromHours(15),
                    TimeSpan.FromHours(15.25),
                    TimeSpan.FromHours(15.5),
                    TimeSpan.FromHours(15.75),
                    TimeSpan.FromHours(16),
                    TimeSpan.FromHours(16.25),
                    TimeSpan.FromHours(16.5),
                    TimeSpan.FromHours(16.75),
                };
            }
        }

        static private string[] Recurrences
        {
            get
            {
                return new string[]
                {
                    "FREQ=DAILY;INTERVAL=1;WKST=MO;BYDAY=MO,TU,WE,TH,FR",
                    "FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=MO,WE,FR",
                    "FREQ=MONTHLY;INTERVAL=1;WKST=MO;BYMONTHDAY=1",
                    "FREQ=DAILY;INTERVAL=1;WKST=MO",
                    "FREQ=DAILY;INTERVAL=2;WKST=MO",
                    "FREQ=DAILY;INTERVAL=1;WKST=MO",
                    "FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=MO,WE,FR",
                    "FREQ=DAILY;INTERVAL=1;WKST=MO",
                    "FREQ=DAILY;INTERVAL=2;WKST=MO",
                    "FREQ=DAILY;INTERVAL=1;WKST=MO",
                    "FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=MO,WE,FR",
                    "FREQ=DAILY;INTERVAL=2;WKST=MO",
                    "FREQ=DAILY;INTERVAL=1;WKST=MO",
                    "FREQ=DAILY;INTERVAL=2;WKST=MO",
                    "FREQ=WEEKLY;INTERVAL=1;WKST=MO;BYDAY=MO,WE,FR",
                    "FREQ=DAILY;INTERVAL=1;WKST=MO",
                };
            }
        }

        private void GenerateList(out string[] resourceIds)
        {
            this._list.Clear();

            //  Generate resource identifiers            
            resourceIds = new string[this._resourceCount];
            for (int i = 0; i < resourceIds.Length; i++)
            {
                resourceIds[i] = Guid.NewGuid().ToString();
            }

            Random randomizeResource = new Random();

            int recurrenceCount = this._itemCount / 10;
            int nextRecurrence = recurrenceCount;

            if (this.HasRecurrences == false)
            {
                nextRecurrence = -1;
                recurrenceCount = 0;
            }

            for (int i = 0; i < this._itemCount; i++)
            {
                DateTime start = RandomizeDate(this._minDate, this._maxDate);
                TimeSpan time = RandomizeStartTime();
                TimeSpan duration = RandomizeDuration();
                start = start.AddHours(time.TotalHours);
                DateTime end = start.AddHours(duration.TotalHours);
                string subject = RandomizeString();
                string description = RandomizeString(100);
                string id = string.Format("ID_{0}", i);
                string recurrence = null;

                if (i == nextRecurrence)
                {
                    nextRecurrence += recurrenceCount;
                    recurrence = RandomizeRecurrence();
                }

                //  Generate a random integer between zero and the number of
                //  resource IDs, to randomnly assign a resource ID to each appointment.
                int resourceIdIndex = resourceIds.Length > 0 ? randomizeResource.Next(resourceIds.Length) : -1;
                string resourceId = resourceIdIndex >= 0 ? resourceIds[resourceIdIndex] : null;

                if (string.IsNullOrEmpty(recurrence) == false)
                {
                    subject += " (recurrence)";
                }

                AppointmentDataObjectBase item =
                    AppointmentDataObjectFactory.Create(
                        id,
                        resourceId,
                        start,
                        end,
                        subject,
                        this.ImplementsINotifyPropertyChanged,
                        this.HasDifferentPropertyNames);

                item.Description = description;

                if (string.IsNullOrEmpty(recurrence) == false)
                {
                    item.Recurrence = recurrence;
                }

                this._list.Add(item);
            }
        }

        //public string Dump()
        //{
        //    StringBuilder sb = new StringBuilder();

        //    for (int i = 0, count = this._list.Count; i < count; i++)
        //    {
        //        AppointmentDataObjectBase item = this._list[i];
        //        sb.AppendLine(item.ToString());
        //    }

        //    return sb.ToString();
        //}

        public IList<AppointmentDataObjectBase> Generate_IList()
        {
            string[] resourceIds = null;
            return this.Generate_IList(out resourceIds);
        }

        public IList<AppointmentDataObjectBase> Generate_IList(out string[] resourceIds)
        {
            this.GenerateList(out resourceIds);
            return this._list;
        }

        public IEnumerable<AppointmentDataObjectBase> Generate_IEnumerable()
        {
            string[] resourceIds = null;
            return this.Generate_IEnumerable(out resourceIds);
        }

        public IEnumerable<AppointmentDataObjectBase> Generate_IEnumerable(out string[] resourceIds)
        {
            this.GenerateList(out resourceIds);
            return this._list.ToArray();
        }

        public void GenerateAppointmentsAndResources(
            out IList<AppointmentDataObjectBase> appointments,
            out IList<ResourceDataObject> resources)
        {
            //  Generate appointments, and get the resulting resource IDs
            string[] resourceIds = null;
            this.GenerateList(out resourceIds);
            appointments = this._list;

            //  Generate resources, assigning the same IDs that were generated
            //  for the appointments, so that each appointment is associated
            //  with a resource in the list we'll return.
            resources = new List<ResourceDataObject>(this._resourceCount);
            foreach (string resId in resourceIds)
            {
                ResourceDataObject resource = new ResourceDataObject();
                resource.Id = resId;
                resource.DisplayName = ResourceDataObject.GenerateName();

                resources.Add(resource);
            }
        }

        static private Random _randomDate = null;
        static private DateTime RandomizeDate(DateTime min, DateTime max)
        {
            TimeSpan span = max - min;
            int minuteSpan = (int)span.TotalMinutes;

            if (_randomDate == null)
                _randomDate = new Random();

            int offset = _randomDate.Next(minuteSpan);

            DateTime date = min.AddMinutes(offset);
            return date.Date;
        }

        static private Random _randomTime = null;
        static private TimeSpan RandomizeStartTime()
        {
            TimeSpan[] spans = AppointmentDataGenerator.StartTimes;

            if (_randomTime == null)
                _randomTime = new Random();

            int index = _randomTime.Next(spans.Length);
            index = Math.Min(index, spans.Length - 1);

            return spans[index];
        }

        static private Random _randomRecurrence = null;
        static private string RandomizeRecurrence()
        {
            string[] recurrences = AppointmentDataGenerator.Recurrences;

            if (_randomRecurrence == null)
                _randomRecurrence = new Random();

            int index = _randomRecurrence.Next(recurrences.Length);
            index = Math.Min(index, recurrences.Length - 1);

            return recurrences[index];
        }

        static private TimeSpan RandomizeDuration()
        {
            TimeSpan[] spans = AppointmentDataGenerator.Durations;

            if (_randomTime == null)
                _randomTime = new Random();

            int index = _randomTime.Next(spans.Length);
            index = Math.Min(index, spans.Length - 1);

            return spans[index];
        }

        static private Random _randomString = null;
        static private string RandomizeString(int length = 10)
        {
            const int min = 97;
            const int max = 122;

            if (_randomString == null)
                _randomString = new Random();

            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                int a = _randomString.Next(max - min) + min;
                chars[i] = (char)a;
            }

            return new string(chars);
        }

    }
    #endregion AppointmentDataGenerator class

    #region ResourceDataGenerator class
#if TINYCLR
    [DontObfuscate]
#endif
    public class ResourceDataGenerator
    {
        private int _itemCount;
        private List<ResourceDataObject> _list = null;

        public ResourceDataGenerator(int itemCount = 10)
        {
            this._itemCount = itemCount;
        }

        public IEnumerable<ResourceDataObject> Generate_IEnumerable()
        {
            this.Generate_IList();
            return this._list.ToArray();
        }

        public IList<ResourceDataObject> Generate_IList()
        {
            this._list = new List<ResourceDataObject>(this._itemCount);

            for (int i = 0; i < this._itemCount; i++)
            {
                ResourceDataObject resource = new ResourceDataObject
                {
                    Id = Guid.NewGuid().ToString(),
                    DisplayName = ResourceDataObject.GenerateName(),
                };

                this._list.Add(resource);
            }

            return this._list;
        }
    }
    #endregion ResourceDataGenerator class
}

