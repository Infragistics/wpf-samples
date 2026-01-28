using System;
using System.Diagnostics;
using System.ComponentModel;

#if PORTABLE
using Infragistics.Portable.Runtime;
#else
using System.Runtime.CompilerServices;
#endif

namespace Infragistics.Framework
{
    #region AppointmentDataObjectBase class
    /// <summary>
    /// Example POCO used to test Appointment data binding.
    /// Does not implement INotifyPropertyChanged.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataObjectBase
    {
        private const string DateFormat = "MM/dd/yy";
        private const string TimeFormat = "hh:mm tt";

        #region Member variables

        private string _id = null;
        private string _resourceId = null;
        private string _location = null;
        private string _description = null;
        private string _recurrence = null;
        private string _recurrenceId = null;
        private DateTime _originalStart;
        private bool _isRemoved = false;

        /// <summary>_start</summary>
        protected DateTime _start;
        
        /// <summary>_end</summary>
        protected DateTime _end;

        /// <summary>_subject</summary>
        protected string _subject = null;

        #endregion Member variables

        #region Constructor
        /// <summary>Creates a new instance.</summary>
        internal AppointmentDataObjectBase()
        {
            //Debug.WriteLine("New AppointmentDataObject instance created.");
        }
        #endregion Constructor

        #region Initialize
        /// <summary>Initializes a new instance.</summary>
        public void Initialize(string id, string resourceId, DateTime start, DateTime end, string subject)
        {
            this._id = id;
            this._resourceId = resourceId;
            this._start = start;
            this._end = end;
            this._subject = subject;
        }
        #endregion Initialize

        #region Id
        public string Id
        {
            get { return this._id; }

            set
            {
                if (value != this._id)
                {
                    this._id = value;
                    this.OnNotifyPropertyChanged(nameof(this.Id));
                }
            }
        }
        #endregion Id

        #region ResourceId
        public string ResourceId
        {
            get { return this._resourceId; }

            set
            {
                if (value != this._resourceId)
                {
                    this._resourceId = value;
                    this.OnNotifyPropertyChanged(nameof(this.ResourceId));
                }
            }
        }
        #endregion ResourceId

        #region Location
        public string Location
        {
            get { return this._location; }

            set
            {
                if (value != this._location)
                {
                    this._location = value;
                    this.OnNotifyPropertyChanged(nameof(this.Location));
                }
            }
        }
        #endregion Location

        #region Description
        public string Description
        {
            get { return this._description; }

            set
            {
                if (value != this._description)
                {
                    this._description = value;
                    this.OnNotifyPropertyChanged(nameof(this.Description));
                }
            }
        }
        #endregion Location

        #region Recurrence
        public string Recurrence
        {
            get { return this._recurrence; }

            set
            {
                if (value != this._recurrence)
                {
                    this._recurrence = value;
                    this.OnNotifyPropertyChanged(nameof(this.Recurrence));
                }
            }
        }
        #endregion Recurrence

        #region RecurrenceId
        public string RecurrenceId
        {
            get { return this._recurrenceId; }

            set
            {
                if (value != this._recurrenceId)
                {
                    this._recurrenceId = value;
                    this.OnNotifyPropertyChanged(nameof(this.RecurrenceId));
                }
            }
        }
        #endregion RecurrenceId

        #region OriginalStart
        public DateTime OriginalStart
        {
            get { return this._originalStart; }

            set
            {
                if (value != this._originalStart)
                {
                    this._originalStart = value;
                    this.OnNotifyPropertyChanged(nameof(this.OriginalStart));
                }
            }
        }
        #endregion OriginalStart

        #region IsRemoved
        public bool IsRemoved
        {
            get { return this._isRemoved; }

            set
            {
                if (value != this._isRemoved)
                {
                    this._isRemoved = value;
                    this.OnNotifyPropertyChanged(nameof(this.IsRemoved));
                }
            }
        }
        #endregion IsRemoved

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnNotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            Debug.WriteLine(string.Format("OnNotifyPropertyChanged: {0}", propertyName));
        }

        #endregion PropertyChanged

        #region ToString
        public override string ToString()
        {
            string startDate = this._start.ToString(DateFormat);
            string startTime = this._start.ToString(TimeFormat);
            string endDate = this._end.ToString(DateFormat);
            string endTime = this._end.ToString(TimeFormat);

            return string.Format("ID='{0}' - '{1}' - ({2} {3} - {4} {5})", this._id, this._subject, startDate, startTime, endDate, endTime);
        }
        #endregion ToString

        #region GetAppointmentTimes
        public void GetAppointmentTimes(out DateTime start, out DateTime end)
        {
            start = this._start;
            end = this._end;
        }
        #endregion GetAppointmentTimes

        #region GetSubject
        public string GetSubject()
        {
            return this._subject;
        }
        #endregion GetSubject

    }
    #endregion AppointmentDataObjectBase class

    #region AppointmentDataObjectWithNotify class
    /// <summary>
    /// Example POCO used to test Appointment data binding.
    /// 
    /// Also implements INotifyPropertyChanged.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataObjectWithNotify :
        AppointmentDataObject,
        INotifyPropertyChanged
    {
    }
    #endregion AppointmentDataObjectWithNotify class

    #region AppointmentDataObject class
    /// <summary>
    /// Example POCO used to test Appointment data binding.
    /// 
    /// Does not implement INotifyPropertyChanged. There is a derived class that does.
    /// 
    /// Has Start, End, and Subject properties so use this when
    /// not using custom property mappings.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataObject : AppointmentDataObjectBase
    {
        public const string SubjectPropertyName = "Subject";
        public const string StartPropertyName = "Start";
        public const string EndPropertyName = "End";

        #region Subject
        public string Subject
        {
            get { return this._subject; }

            set
            {
                if (value != this._subject)
                {
                    this._subject = value;
                    this.OnNotifyPropertyChanged(nameof(this.Subject));
                }
            }
        }
        #endregion Subject

        #region Start
        public DateTime Start
        {
            get { return this._start; }

            set
            {
                if (value != this._start)
                {
                    this._start = value;
                    this.OnNotifyPropertyChanged(nameof(this.Start));
                }
            }
        }
        #endregion Start

        #region End
        public DateTime End
        {
            get { return this._end; }

            set
            {
                if (value != this._end)
                {
                    this._end = value;
                    this.OnNotifyPropertyChanged(nameof(this.End));
                }
            }
        }
        #endregion End
    }
    #endregion AppointmentDataObject class

    #region AppointmentDataObjectNoParameterlessCtor class
    /// <summary>
    /// Example POCO used to test Appointment data binding. Has no parameterless ctor.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataObjectNoParameterlessCtor : AppointmentDataObject
    {
        public AppointmentDataObjectNoParameterlessCtor(DateTime start, DateTime end)
        {
            this._start = start;
            this._end = end;
        }
    }
    #endregion AppointmentDataObjectNoParameterlessCtor class

    #region StringBasedAppointmentDataObject class
    /// <summary>
    /// Has Start and End properties of type string.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class StringBasedAppointmentDataObject : AppointmentDataObjectBase
    {
        public StringBasedAppointmentDataObject(string id, string resourceId, DateTime start, DateTime end, string subject)
        {
            this.Initialize(id, resourceId, start, end, subject);
        }

        #region Subject
        public string Subject
        {
            get { return this._subject; }

            set
            {
                if (value != this._subject)
                {
                    this._subject = value;
                    this.OnNotifyPropertyChanged(nameof(this.Subject));
                }
            }
        }
        #endregion Subject

        #region Start
        /// <summary>
        /// This property is of type string
        /// </summary>
        public string Start
        {
            get { return this._start.ToString("MM/dd/yyyy"); }
            set
            {
                if (value != this.Start)
                {
                    this._start = DateTime.Parse(value);
                    this.OnNotifyPropertyChanged("Start");
                }
            }
        }
        #endregion Start

        #region End
        /// <summary>
        /// This property is of type string.
        /// </summary>
        public string End
        {
            get { return this._end.ToString("MM/dd/yyyy"); }
            set
            {
                if (value != this.End)
                {
                    this._end = DateTime.Parse(value);
                    this.OnNotifyPropertyChanged("End");
                }
            }
        }
        #endregion End

    }
    #endregion StringBasedAppointmentDataObject class

    #region IntegerBasedAppointmentDataObject class
    /// <summary>
    /// Has Start and End properties of type long which won't work
    /// unless they manually convert it.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class IntegerBasedAppointmentDataObject : AppointmentDataObjectBase
    {
        public IntegerBasedAppointmentDataObject(string id, string resourceId, DateTime start, DateTime end, string subject)
        {
            this.Initialize(id, resourceId, start, end, subject);
        }

        #region Subject
        public string Subject
        {
            get { return this._subject; }

            set
            {
                if (value != this._subject)
                {
                    this._subject = value;
                    this.OnNotifyPropertyChanged(nameof(this.Subject));
                }
            }
        }
        #endregion Subject

        #region Start
        /// <summary>
        /// This property is of type long
        /// </summary>
        public long Start
        {
            get { return this._start.Ticks; }
            set
            {
                if (value != this.Start)
                {
                    this._start = new DateTime(value);
                    this.OnNotifyPropertyChanged("Start");
                }
            }
        }
        #endregion Start

        #region End
        /// <summary>
        /// This property is of type long.
        /// </summary>
        public long End
        {
            get { return this._end.Ticks; }
            set
            {
                if (value != this.End)
                {
                    this._end = new DateTime(value);
                    this.OnNotifyPropertyChanged("End");
                }
            }
        }
        #endregion End

    }
    #endregion IntegerBasedAppointmentDataObject class

    #region AppointmentDataObjectDifferentPropNamesWithNotify class
    /// <summary>
    /// Example POCO used to test Appointment data binding.
    /// 
    /// Also implements INotifyPropertyChanged.
    /// 
    /// Has StartX, EndX, and SubjectX properties so use this when
    /// testing custom property mappings.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataObjectDifferentPropNamesWithNotify :
        AppointmentDataObjectDifferentPropNames,
        INotifyPropertyChanged
    {
    }
    #endregion AppointmentDataObjectDifferentPropNamesWithNotify class

    #region AppointmentDataObjectDifferentPropNames class
    /// <summary>
    /// Example POCO used to test Appointment data binding.
    /// 
    /// Does not implement INotifyPropertyChanged.
    /// 
    /// Has StartX, EndX, and SubjectX properties so use this when
    /// using custom property mappings.
    /// </summary>
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataObjectDifferentPropNames : AppointmentDataObjectBase
    {
        public const string SubjectPropertyName = "SubjectX";
        public const string StartPropertyName = "StartX";
        public const string EndPropertyName = "EndX";

        #region SubjectX
        public string SubjectX
        {
            get { return this._subject; }

            set
            {
                if (value != this._subject)
                {
                    this._subject = value;
                    this.OnNotifyPropertyChanged(nameof(this.SubjectX));
                }
            }
        }
        #endregion SubjectX

        #region StartX
        public DateTime StartX
        {
            get { return this._start; }

            set
            {
                if (value != this._start)
                {
                    this._start = value;
                    this.OnNotifyPropertyChanged(nameof(this.StartX));
                }
            }
        }
        #endregion StartX

        #region EndX
        public DateTime EndX
        {
            get { return this._end; }

            set
            {
                if (value != this._end)
                {
                    this._end = value;
                    this.OnNotifyPropertyChanged(nameof(this.EndX));
                }
            }
        }
        #endregion EndX
    }
    #endregion AppointmentDataObjectDifferentPropNames class

    #region AppointmentDataObjectFactory class
#if TINYCLR
    [DontObfuscate]
#endif
    public class AppointmentDataObjectFactory
    {
        /// <summary>
        /// Factory method which creates an AppointmentDataObjectBase-derived instance
        /// with the specified property values.
        /// </summary>
        /// <param name="id">Appointment.Id</param>
        /// <param name="resourceId">Appointment.ResourceId</param>
        /// <param name="start">Appointment.Start</param>
        /// <param name="end">Appointment.End</param>
        /// <param name="subject">Appointment.Subject</param>
        /// <param name="implementsINotifyPropertyChanged">
        /// Specifies whether the instance returned implements the
        /// INotifyPropertyChanged interface. Useful for testing how
        /// we react to external changes to data objects.
        /// </param>
        /// <param name="hasDifferentPropertyNames">
        /// Specifies whether the instance returned has properties with different names
        /// than our appointment object, i.e., StartX, EndX, and SubjectX instead of Subject,
        /// Start, and End. Useful for testing custom property mappings.
        /// </param>
        static public AppointmentDataObjectBase Create(
            string id,
            string resourceId,
            DateTime start,
            DateTime end,
            string subject,
            bool implementsINotifyPropertyChanged = true,
            bool hasDifferentPropertyNames = false)
        {
            AppointmentDataObjectBase obj = null;

            if (implementsINotifyPropertyChanged)
            {
                if (hasDifferentPropertyNames)
                    obj = new AppointmentDataObjectDifferentPropNamesWithNotify();
                else
                    obj = new AppointmentDataObjectWithNotify();
            }
            else
            {
                if (hasDifferentPropertyNames)
                    obj = new AppointmentDataObjectDifferentPropNames();
                else
                    obj = new AppointmentDataObject();
            }

            obj.Initialize(id, resourceId, start, end, subject);

            return obj;
        }
    }
    #endregion AppointmentDataObjectFactory class

    #region ResourceDataObject class
#if TINYCLR
    [DontObfuscate]
#endif
    public class ResourceDataObject : INotifyPropertyChanged
    {
        private string _id;
        private string _displayName;
        private string _colorScheme;
        private string _daysOfWeekSettings;

        static private Random _random = new Random();

        public string Id
        {
            get { return this._id; }

            set
            {
                if (value != this._id)
                {
                    this._id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        public string DisplayName
        {
            get { return this._displayName; }

            set
            {
                if (value != this._displayName)
                {
                    this._displayName = value;
                    this.OnPropertyChanged("DisplayName");
                }
            }
        }

        public string ColorScheme
        {
            get { return this._colorScheme; }

            set
            {
                if (value != this._colorScheme)
                {
                    this._colorScheme = value;
                    this.OnPropertyChanged("ColorScheme");
                }
            }
        }

        public string DaysOfWeekSettings
        {
            get { return this._daysOfWeekSettings; }

            set
            {
                if (value != this._daysOfWeekSettings)
                {
                    this._daysOfWeekSettings = value;
                    this.OnPropertyChanged("DaysOfWeekSettings");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", this.DisplayName, this.Id);
        }

        static public string GenerateColor()
        {
            byte red = (byte)_random.Next(255);
            byte green = (byte)_random.Next(255);
            byte blue = (byte)_random.Next(255);

            //  JS chokes on hex formatting chars...
#if TINYCLR
            string rs = (string)Script.Literal("red.toString(16)");
            string gs = (string)Script.Literal("green.toString(16)");
            string bs = (string)Script.Literal("blue.toString(16)");

            if (rs.Length == 1)
                rs = "0" + rs;

            if (gs.Length == 1)
                gs = "0" + gs;

            if (bs.Length == 1)
                bs = "0" + bs;

            return "#" + rs + gs + bs;
#else
            return string.Format("#{0:x2}{1:x2}{2:x2}", red, green, blue);
#endif
        }

        static public string GenerateName()
        {
            string[] names = ResourceDataObject.Names;
            int index = _random.Next(names.Length);
            return names[index];
        }

        static private string[] Names
        {
            get
            {
                return new string[]
                {
                    "Werner Longnecker",
                    "Miguel Mcelwee",
                    "Andres Willson",
                    "Kevin Healy",
                    "Hung Mote",
                    "Bryon Miah",
                    "Percy Norsworthy",
                    "Demarcus Dupont",
                    "Salvatore Smith",
                    "Ezra Lira",
                    "Dylan Harless",
                    "Monte Mcglothlen",
                    "Cornell Pate",
                    "Marcellus Bormann",
                    "Waylon Elwell",
                    "Riley Smelser",
                    "Oliver Howells",
                    "Heriberto Bones",
                    "Laurence Trenholm",
                    "Adan Teal",
                    "Lance Giuliano",
                    "Andy Beckmann",
                    "Cleo Kendig",
                    "Erasmo Mcduff",
                    "Paris Hannaman",
                    "Whitney Pigott",
                    "Blair Dyches",
                    "Daron Smiley",
                    "Aron Holmes",
                    "Dewayne Emmanuel",
                    "Darron Backer",
                    "Saul Henderson",
                    "Earl Tjaden",
                    "Daren Gillian",
                    "German Walsh",
                    "Murray Mauffray",
                    "Josiah Slawson",
                    "Edwardo Straub",
                    "Jared Ahrens",
                    "Elliott Erne",
                    "Paul Longmire",
                    "Dan Hotz",
                    "Ferdinand Paniagua",
                    "Leland Leiker",
                    "Delmer Hessler",
                    "Rex Kimes",
                    "Jeromy Forcier",
                    "Brady Depalma",
                    "Johnnie Cass",
                    "Ezequiel Graydon",
                };
            }
        }
    }
#endregion ResourceDataObject class
}

