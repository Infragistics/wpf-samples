using System;
using System.Collections.Generic;
using Infragistics.Samples.Shared.Models;

namespace Infragistics.Samples.Shared.Tools
{
    public class PerformanceMonitor
    {
        public PerformanceMonitor()
        {
            this.Strings = new PerformanceStrings();
            this.IsEnabled = false;
            this.TimeStart = DateTime.Now;
            this.TimeStop = DateTime.Now;
            this.TimeSpan = TimeSpan.FromSeconds(0);

            this.MemorySizeStart = 0;
            this.MemorySizeStop = 0;
            this.MemorySizeChange = 0;

        }
        #region Events
        public event EventHandler MonitorStopped;
        public void RaiseMonitorStoppedEvent()
        {
            if (MonitorStopped != null)
            {
                MonitorStopped(this, EventArgs.Empty);
            }
        }
        public event EventHandler MonitorStarted;
        public void RaiseMonitorStartedEvent()
        {
            if (MonitorStarted != null)
            {
                MonitorStarted(this, EventArgs.Empty);
            }
        }
        #endregion
        #region Properties
        public bool IsEnabled { get; set; }

        public DateTime TimeStart { get; private set; }
        public DateTime TimeStop { get; private set; }
        public TimeSpan TimeSpan { get; private set; }

        public long MemorySizeStart { get; private set; }
        public long MemorySizeStop { get; private set; }
        public long MemorySizeChange { get; private set; }
        public bool MemoryForceCollection { get; set; }

        public PerformanceStrings Strings { get; set; }

        #endregion
        #region Methods
        public void StartMonitor()
        {
            // time 
            this.IsEnabled = true;
            this.TimeStart = DateTime.Now;
            this.TimeSpan = TimeSpan.FromSeconds(0);

            // memory 
            this.MemorySizeStart = GC.GetTotalMemory(this.MemoryForceCollection);
            this.MemorySizeChange = 0;

            RaiseMonitorStartedEvent();
        }
        public void StopMonitor()
        {
            // time 
            this.IsEnabled = false;
            this.TimeStop = DateTime.Now;
            this.TimeSpan = this.TimeStop - this.TimeStart;

            // memory 
            this.MemorySizeStop = GC.GetTotalMemory(this.MemoryForceCollection);
            this.MemorySizeChange = this.MemorySizeStop - this.MemorySizeStart;

            RaiseMonitorStoppedEvent();
        }

        public string PerformanceChangeToString()
        {
            string result = string.Empty;
            result += this.Strings.TimeSpan + String.Format("{0:0,0.00}, ", this.TimeSpan.TotalMilliseconds);
            result += this.Strings.MemorySizeChange + String.Format("{0:0,0.00}", this.MemorySizeChange);
            return result;
        }
        public string TimeSpanToString()
        {
            string result = string.Empty;
            result += String.Format("{0:0,0.00}, ", this.TimeSpan.TotalMilliseconds);
            return result;
        }
        #endregion

        public class PerformanceStrings
        {
            public PerformanceStrings()
            {
                this.TimeStart = "Time Start";
                this.TimeStop = "Time Stop";
                this.TimeSpan = "Time Span (ms)";

                this.MemorySizeStart = "Memory Size Start";
                this.MemorySizeStop = "Memory Size Stop";
                this.MemorySizeChange = "Memory Size Change";
            }
            public string MemorySizeChange { get; set; }
            public string MemorySizeStart { get; set; }
            public string MemorySizeStop { get; set; }

            public string TimeStart { get; set; }
            public string TimeStop { get; set; }
            public string TimeSpan { get; set; }


        }
    }

    public class PerformanceMonitorHolder
    {
        protected Dictionary<string, List<PerformanceMonitor>> Dictionary;

        public PerformanceMonitorHolder()
        {
            this.TimeAverageSpan = new TimeSpan();
            this.MemoryAverageSizeStart = 0;
            this.MemoryAverageSizeStop = 0;
            this.MemoryAverageSizeChange = 0;
            this.Dictionary = new Dictionary<string, List<PerformanceMonitor>>();
        }

        #region Properties

        public TimeSpan TimeAverageSpan { get; private set; }

        public long MemoryAverageSizeStart { get; private set; }
        public long MemoryAverageSizeStop { get; private set; }
        public long MemoryAverageSizeChange { get; private set; }

        #endregion

        public void AddPerformanceMonitor(string operationName, PerformanceMonitor perfMonitor)
        {
            if (Dictionary.ContainsKey(operationName))
            {
                List<PerformanceMonitor> list = Dictionary[operationName];
                list.Add(perfMonitor);

                //update the dictionary
                Dictionary.Remove(operationName);
                Dictionary.Add(operationName, list);
            }
            else
            {
                List<PerformanceMonitor> list = new List<PerformanceMonitor>();
                list.Add(perfMonitor);

                Dictionary.Add(operationName, list);
            }
        }
        public void Update()
        {
            foreach (string key in Dictionary.Keys)
            {
                TimeSpan sumTimeSpan = new TimeSpan();
                long sumMemorySizeStart = 0;
                long sumMemorySizeStop = 0;
                List<PerformanceMonitor> list = Dictionary[key];
                foreach (PerformanceMonitor pm in list)
                {
                    sumTimeSpan += pm.TimeSpan;
                    sumMemorySizeStart += pm.MemorySizeStart;
                    sumMemorySizeStop += pm.MemorySizeStop;
                }

                this.TimeAverageSpan = TimeSpan.FromMilliseconds(sumTimeSpan.TotalMilliseconds / list.Count);

                this.MemoryAverageSizeStart = sumMemorySizeStart / list.Count;
                this.MemoryAverageSizeStop = sumMemorySizeStop / list.Count;
                this.MemoryAverageSizeChange = this.MemoryAverageSizeStop - this.MemoryAverageSizeStart;
            }

        }
    }


    public class PerformanceDataPoint : ObservableModel
    {
        public PerformanceDataPoint()
        {
            _label = "0";
            _memorySizeChange = 0;
            _dateTime = DateTime.Now;
            _timeSpan = TimeSpan.FromSeconds(0);
        }
        #region Properties
        private string _label;
        public string Label
        {
            get { return _label; }
            set
            {
                if (_label == value) return;
                _label = value;
                OnPropertyChanged("Label");
            }
        }

        private DateTime _dateTime;
        public DateTime DateTime
        {
            get { return _dateTime; }
            set
            {
                if (_dateTime == value) return;
                _dateTime = value;
                OnPropertyChanged("DateTime");
            }
        }

        private TimeSpan _timeSpan;
        public TimeSpan TimeSpan
        {
            get { return _timeSpan; }
            set
            {
                if (_timeSpan == value) return;
                _timeSpan = value;
                OnPropertyChanged("TimeSpan");
            }
        }

        private long _memorySizeChange;
        public long MemorySizeChange
        {
            get { return _memorySizeChange; }
            set
            {
                if (_memorySizeChange == value) return;
                _memorySizeChange = value;
                OnPropertyChanged("MemorySizeChange");
            }
        }

        #endregion
    }
}