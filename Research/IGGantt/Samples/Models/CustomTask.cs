using System;
using System.ComponentModel;

namespace IGGantt.Samples.Models
{
    public class CustomTask : INotifyPropertyChanged
    {
        #region Private variables

        private string dataItemId;
        private DateTime? start;
        private string taskName;
        private TimeSpan? duration;
        private string tasks;
        private string predecesors;

        private DateTime? constraintDate;
        private string constraintType;
        private string durationFormat;

        private string resources;

        #endregion // Private variables

        #region Public Properties

        public string DataItemId
        {
            get { return dataItemId; }
            set
            {
                if (value != dataItemId)
                {
                    dataItemId = value;
                    OnPropertyChanged("DataItemId");
                }
            }
        }

        public DateTime? Start
        {
            get { return start; }
            set
            {
                if (value != start)
                {
                    start = value;
                    OnPropertyChanged("Start");
                }
            }
        }

        public string TaskName
        {
            get { return taskName; }
            set
            {
                if (value != taskName)
                {
                    taskName = value;
                    OnPropertyChanged("TaskName");
                }
            }
        }

        public TimeSpan? Duration
        {
            get { return duration; }
            set
            {
                if (duration != value)
                {
                    duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }

        public string Tasks
        {
            get { return tasks; }
            set
            {
                if (value != tasks)
                {
                    tasks = value;
                    OnPropertyChanged("Tasks");
                }
            }
        }

        public string Predecessors
        {
            get { return predecesors; }
            set
            {
                if (value != predecesors)
                {
                    predecesors = value;
                    OnPropertyChanged("Predecessors");
                }
            }
        }

        public DateTime? ConstraintDate
        {
            get { return constraintDate; }
            set
            {
                if (value != constraintDate)
                {
                    constraintDate = value;
                    OnPropertyChanged("ConstraintDate");
                }
            }
        }

        public string ConstraintType
        {
            get { return constraintType; }
            set
            {
                if (value != constraintType)
                {
                    constraintType = value;
                    OnPropertyChanged("ConstraintType");
                }
            }
        }

        public string DurationFormat
        {
            get { return durationFormat; }
            set
            {
                if (value != durationFormat)
                {
                    durationFormat = value;
                    OnPropertyChanged("DurationFormat");
                }
            }
        }


        public string Resources
        {
            get { return resources; }
            set
            {
                if (value != resources)
                {
                    resources = value;
                    OnPropertyChanged("Resources");
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
