using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using IGGantt.Resources;
using IGGantt.Samples.Models;
using Infragistics;
using Infragistics.Controls.Schedules;

namespace IGGantt.Samples.ViewModel
{
    public class CalendarWorkingTimesViewModel : INotifyPropertyChanged
    {
        #region Private variables

        private DateTime? selectedStartDate;
        private DateTime? selectedEndDate;
        private ObservableCollection<ScheduleDayInfo> days;
        private ScheduleDayInfo selectedDayInfo;

        #endregion // Private variables

        #region Constructor

        public CalendarWorkingTimesViewModel()
        {
            days = new ObservableCollection<ScheduleDayInfo>(
                new ScheduleDayInfo[] {
                    new ScheduleDayInfo(GanttStrings.Monday),
                    new ScheduleDayInfo(GanttStrings.Tuesday),
                    new ScheduleDayInfo(GanttStrings.Wednesday), 
                    new ScheduleDayInfo(GanttStrings.Thursday), 
                    new ScheduleDayInfo(GanttStrings.Friday), 
                    new ScheduleDayInfo(GanttStrings.Saturday) {IsWorkingDay = false},
                    new ScheduleDayInfo(GanttStrings.Sunday){IsWorkingDay = false}}
                );

            SelectedDayInfo = Days[0];
            SelectedStartDate = DateTime.Today;
            SelectedEndDate = DateTime.Today.AddDays(7);
        }

        #endregion // Constructor

        #region Public methods

        public void UpdateProjectCalendar()
        {
            ProjectCalendar projectCalendar = new ProjectCalendar() { UniqueId = "TwoShiftCalendar" };

            ScheduleDaysOfWeek scheduleWeek = new ScheduleDaysOfWeek
            {
                Monday = Days[0].ScheduleDayOfWeek,
                Tuesday = Days[1].ScheduleDayOfWeek,
                Wednesday = Days[2].ScheduleDayOfWeek,
                Thursday = Days[3].ScheduleDayOfWeek,
                Friday = Days[4].ScheduleDayOfWeek,
                Saturday = Days[5].ScheduleDayOfWeek,
                Sunday = Days[6].ScheduleDayOfWeek
            };
            ProjectCalendarWorkWeek projectWorkWeek = new ProjectCalendarWorkWeek
            {
                DateRange = new DateRange(SelectedStartDate, SelectedEndDate),
                DaysOfWeek = scheduleWeek
            };

            projectCalendar.WorkWeeks.Add(projectWorkWeek);
            if (GanttProject.Calendars.Count > 0)
            {
                GanttProject.Calendars[0] = projectCalendar;
            }
            else
            {
                GanttProject.Calendars.Add(projectCalendar);
            }

            GanttProject.CalendarId = "TwoShiftCalendar";
        }

        #endregion // Public methods

        #region Public properties

        public Project GanttProject { get; set; }

        public DateTime SelectedStartDate
        {
            get { return (DateTime)selectedStartDate; }
            set
            {
                if (value > selectedEndDate)
                {
                    selectedStartDate = selectedEndDate;
                }
                else
                {
                    selectedStartDate = value;
                }
                OnPropertyChanged("SelectedStartDate");
            }
        }

        public DateTime SelectedEndDate
        {
            get { return (DateTime)selectedEndDate; }
            set
            {
                if (value < selectedStartDate)
                {
                    selectedEndDate = selectedStartDate;
                }
                else
                {
                    selectedEndDate = value;
                }
                OnPropertyChanged("SelectedEndDate");
            }
        }

        public ScheduleDayInfo SelectedDayInfo
        {
            get
            {
                return selectedDayInfo;
            }
            set
            {
                if (value != selectedDayInfo)
                {
                    selectedDayInfo = value;
                    OnPropertyChanged("SelectedDayInfo");
                    OnPropertyChanged("IsWorkingDay");
                    OnPropertyChanged("FirstShiftStart");
                    OnPropertyChanged("FirstShiftEnd");
                    OnPropertyChanged("SecondShiftStart");
                    OnPropertyChanged("SecondShiftEnd");
                }
            }
        }

        public bool? IsWorkingDay
        {
            get { return (bool?)SelectedDayInfo.IsWorkingDay; }
            set
            {
                if (value.HasValue)
                {
                    SelectedDayInfo.IsWorkingDay = value.Value;
                }

                OnPropertyChanged("IsWorkingDay");
            }
        }

        public DateTime? FirstShiftStart
        {
            get { return DateTime.Today.Add(SelectedDayInfo.StartFirstShift); }
            set
            {
                if (value != null)
                {
                    DateTime dt = (DateTime)value;
                    SelectedDayInfo.StartFirstShift = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                }

                OnPropertyChanged("FirstShiftStart");
                OnPropertyChanged("FirstShiftEnd");
                OnPropertyChanged("SecondShiftStart");
                OnPropertyChanged("SecondShiftEnd");
            }
        }

        public DateTime? FirstShiftEnd
        {
            get { return DateTime.Today.Add(SelectedDayInfo.EndFirstShift); }
            set
            {
                if (value != null)
                {
                    DateTime dt = (DateTime)value;
                    SelectedDayInfo.EndFirstShift = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                }

                OnPropertyChanged("FirstShiftStart");
                OnPropertyChanged("FirstShiftEnd");
                OnPropertyChanged("SecondShiftStart");
                OnPropertyChanged("SecondShiftEnd");
            }
        }

        public DateTime? SecondShiftStart
        {
            get { return DateTime.Today.Add(SelectedDayInfo.StartSecondShift); }
            set
            {
                if (value != null)
                {
                    DateTime dt = (DateTime)value;
                    SelectedDayInfo.StartSecondShift = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                }

                OnPropertyChanged("FirstShiftStart");
                OnPropertyChanged("FirstShiftEnd");
                OnPropertyChanged("SecondShiftStart");
                OnPropertyChanged("SecondShiftEnd");
            }
        }

        public DateTime? SecondShiftEnd
        {
            get { return DateTime.Today.Add(SelectedDayInfo.EndSecondShift); }
            set
            {
                if (value != null)
                {
                    DateTime dt = (DateTime)value;
                    SelectedDayInfo.EndSecondShift = new TimeSpan(dt.Hour, dt.Minute, dt.Second);
                }

                OnPropertyChanged("FirstShiftStart");
                OnPropertyChanged("FirstShiftEnd");
                OnPropertyChanged("SecondShiftStart");
                OnPropertyChanged("SecondShiftEnd");
            }
        }

        public ObservableCollection<ScheduleDayInfo> Days
        {
            get { return days; }
            set 
            {
                if (value != null)
                {
                    days = value;
                    OnPropertyChanged("Days");
                }
            }
        }

        #endregion // Public properties

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion // INotifyPropertyChanged
    }
}
