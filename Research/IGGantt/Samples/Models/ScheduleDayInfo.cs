using System;
using Infragistics;
using Infragistics.Controls.Schedules;

namespace IGGantt.Samples.Models
{
    // Helper class for making entering of working day shifts easier
    public class ScheduleDayInfo
    {
        #region Private variables

        private TimeSpan startFirstShift = new TimeSpan(8, 0, 0);
        private TimeSpan endFirstShift = new TimeSpan(12, 0, 0);
        private TimeSpan startSecondShift = new TimeSpan(13, 0, 0);
        private TimeSpan endSecondShift = new TimeSpan(17, 0, 0);

        #endregion // Private variables

        #region Constructor

        public ScheduleDayInfo(string name)
        {
            Name = name;
            IsWorkingDay = true;
        }

        #endregion // Constructor

        #region Public properties

        public ScheduleDayOfWeek ScheduleDayOfWeek
        {
            get
            {
                return CreateScheduleDayOfWeek();
            }
        }

        public string Name { get; private set; }

        public bool IsWorkingDay { get; set; }

        public TimeSpan StartFirstShift
        {
            get { return startFirstShift; }
            set
            {
                if (EndSecondShift != null && value >= EndSecondShift)
                {
                    return;
                }
                else if (EndFirstShift != null && value >= EndFirstShift)
                {
                    return;
                }

                startFirstShift = value;
            }
        }

        public TimeSpan EndFirstShift
        {
            get { return endFirstShift; }
            set
            {
                if (value < StartFirstShift)
                {
                    endFirstShift = StartFirstShift;
                }
                else if (value > StartSecondShift)
                {
                    endFirstShift = StartSecondShift;
                }
                else
                {
                    endFirstShift = value;
                }
            }
        }

        public TimeSpan StartSecondShift
        {
            get { return startSecondShift; }
            set
            {
                if (value < EndFirstShift)
                {
                    startSecondShift = EndFirstShift;
                }
                else if (value > EndSecondShift)
                {
                    startSecondShift = EndSecondShift;
                }
                else
                {
                    startSecondShift = value;
                }
            }
        }

        public TimeSpan EndSecondShift
        {
            get { return endSecondShift; }
            set
            {
                if (value < StartSecondShift)
                {
                    endSecondShift = StartSecondShift;
                }
                else
                {
                    endSecondShift = value;
                }
            }
        }

        #endregion // Public properties

        #region Private helpers

        private ScheduleDayOfWeek CreateScheduleDayOfWeek()
        {
            WorkingHoursCollection whc;
            int choice = (Convert.ToByte(StartFirstShift.Equals(EndFirstShift)) << 2) + Convert.ToByte(StartSecondShift.Equals(EndSecondShift));
            switch (choice)
            {
                case 0:
                    whc = new WorkingHoursCollection
                    {
                        new TimeRange { Start = StartFirstShift, End = EndFirstShift }, 
                        new TimeRange { Start = StartSecondShift, End = EndSecondShift }
                    };
                    break;
                case 1:
                    whc = new WorkingHoursCollection
                    {
                        new TimeRange { Start = StartFirstShift, End = EndFirstShift}
                        //new TimeRange { Start = StartSecondShift, End = EndSecondShift}
                    };
                    break;
                case 4:
                    whc = new WorkingHoursCollection
                    {
                        new TimeRange { Start = StartSecondShift, End = EndSecondShift}
                        //new TimeRange { Start = StartFirstShift, End = EndFirstShift}
                    };
                    break;
                case 5:
                default:
                    whc = new WorkingHoursCollection
                    {
                        new TimeRange { Start = StartFirstShift, End = StartFirstShift + new TimeSpan(8,0,0)}
                    };
                    break;
            }

            return new ScheduleDayOfWeek { DaySettings = new DaySettings { IsWorkday = this.IsWorkingDay, WorkingHours = whc } };
        }

        #endregion // Private helpers
    }
}
