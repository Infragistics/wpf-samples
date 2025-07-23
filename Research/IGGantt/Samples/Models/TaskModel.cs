using System;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Shared.Models;

namespace IGGantt.Samples.Models
{
    public class TaskModel : ObservableModel
    {
        private string _taskId;
        public string TaskID
        {
            get
            {
                return _taskId;
            }
            set
            {
                if (_taskId != value)
                {
                    _taskId = value;
                    this.OnPropertyChanged("TaskID");
                }
            }
        }        
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    this.OnPropertyChanged("Name");
                }
            }
        }
        private TimeSpan? _durationInHours;
        public TimeSpan? DurationInHours
        {
            get
            {
                return _durationInHours;
            }
            set
            {
                if (_durationInHours != value)
                {
                    _durationInHours = value;
                    this.OnPropertyChanged("DurationInHours");
                }
            }
        }
        private DateTime _start;
        public DateTime Start
        {
            get
            {
                return _start;
            }
            set
            {
                if (_start != value)
                {
                    _start = value;
                    this.OnPropertyChanged("Start");
                }
            }
        }
        private bool _isMilestone = false;
        public bool IsMilestone
        {
            get
            {
                return _isMilestone;
            }
            set
            {
                if (_isMilestone != value)
                {
                    _isMilestone = value;
                    this.OnPropertyChanged("IsMilestone");
                }
            }
        }
        private ProjectTaskConstraintType _constraintType;
        public ProjectTaskConstraintType ConstraintType
        {
            get
            {
                return _constraintType;
            }
            set
            {
                if (_constraintType != value)
                {
                    _constraintType = value;
                    this.OnPropertyChanged("ConstraintType");
                }
            }
        }
        private DateTime? _constraintDate;
        public DateTime? ConstraintDate
        {
            get
            {
                return _constraintDate;
            }
            set
            {
                if (_constraintDate != value)
                {
                    _constraintDate = value;
                    this.OnPropertyChanged("ConstraintDate");
                }
            }
        }
        private ProjectDurationFormat _durationFormat;
        public ProjectDurationFormat DurationFormat
        {
            get
            {
                return _durationFormat;
            }
            set
            {
                if (_durationFormat != value)
                {
                    _durationFormat = value;
                    this.OnPropertyChanged("DurationFormat");
                }
            }
        }
        private bool _isInProgress = true;
        public bool IsInProgress
        {
            get
            {
                return _isInProgress;
            }
            set
            {
                if (_isInProgress != value)
                {
                    _isInProgress = value;
                    this.OnPropertyChanged("IsInProgress");
                }
            }

        }
        private bool _isUndetermined = false;
        public bool IsUndetermined
        {
            get
            {
                return _isUndetermined;
            }
            set
            {
                if (_isUndetermined != value)
                {
                    _isUndetermined = value;
                    this.OnPropertyChanged("IsUndetermined");
                }
            }
        }
        private DateTime? _deadlineDate;
        public DateTime? DeadlineDate
        {
            get
            {
                return _deadlineDate;
            }
            set
            {
                if (_deadlineDate != value)
                {
                    _deadlineDate = value;
                    this.OnPropertyChanged("DeadlineDate");
                }
            }
        }
        private string _tasks;
        public string Tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                if (_tasks != value)
                {
                    _tasks = value;
                    this.OnPropertyChanged("Tasks");
                }
            }
        }

        private decimal _percentComplete;
        public decimal PercentComplete
        {
            get
            {
                return _percentComplete;
            }
            set
            {
                if (_percentComplete != value)
                {
                    _percentComplete = value;
                    this.OnPropertyChanged("PercentComplete");
                }
            }
        }

        private DateTime? _actualStart;
        public DateTime? ActualStart
        {
            get
            {
                return _actualStart;
            }
            set
            {
                if (_actualStart != value)
                {
                    _actualStart = value;
                    this.OnPropertyChanged("ActualStart");
                }
            }
        }
    }
}
