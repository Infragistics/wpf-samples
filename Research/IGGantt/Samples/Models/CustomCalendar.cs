using System.ComponentModel;

namespace IGGantt.Samples.Models
{
    public class CustomCalendar : INotifyPropertyChanged
    {
        #region Private variables

        private string id;
        private string name;
        private string baseCalendarId;
        private bool isBaseCalendar;
        private string daysOfWeek;
        private string exceptions;
        private string workWeeks;

        /* This field is custom and it is not related to calendar functionality. 
         * It holds additional details about the calendar and it is used in samples to give more details about the particular calendar. */
        private string customDescription;

        #endregion // Private variables

        #region Public properties

        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value != id)
                {
                    id = value;
                    this.OnPropertyChanged("Id");
                }
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value != name)
                {
                    name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }

        #endregion // Name

        #region BaseCalendarId

        public string BaseCalendarId
        {
            get
            {
                return baseCalendarId;
            }
            set
            {
                if (value != baseCalendarId)
                {
                    baseCalendarId = value;
                    this.OnPropertyChanged("BaseCalendarId");
                }
            }
        }

        #endregion // BaseCalendarId

        #region IsBaseCalendar

        public bool IsBaseCalendar
        {
            get
            {
                return isBaseCalendar;
            }
            set
            {
                if (value != isBaseCalendar)
                {
                    isBaseCalendar = value;
                    this.OnPropertyChanged("IsBaseCalendar");
                }
            }
        }

        #endregion // IsBaseCalendar

        #region DaysOfWeek

        public string DaysOfWeek
        {
            get
            {
                return daysOfWeek;
            }
            set
            {
                if (value != daysOfWeek)
                {
                    daysOfWeek = value;
                    this.OnPropertyChanged("DaysOfWeek");
                }
            }
        }

        #endregion // DaysOfWeek

        #region Exceptions

        public string Exceptions
        {
            get
            {
                return exceptions;
            }
            set
            {
                if (value != exceptions)
                {
                    exceptions = value;
                    this.OnPropertyChanged("Exceptions");
                }
            }
        }

        public string WorkWeeks
        {
            get
            {
                return workWeeks;
            }
            set
            {
                if (value != workWeeks)
                {
                    workWeeks = value;
                    this.OnPropertyChanged("WorkWeeks");
                }
            }
        }

        public string CustomDescription
        {
            get { return customDescription; }
            set
            {
                if (value != customDescription)
                {
                    customDescription = value;
                    OnPropertyChanged("CustomDescription");
                }
            }
        }

        #endregion // Public properties

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged
    }
}