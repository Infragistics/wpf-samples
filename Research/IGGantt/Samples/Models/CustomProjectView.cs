using System.ComponentModel;

namespace IGGantt.Samples.Models
{
    public class CustomProjectView : INotifyPropertyChanged
    {
        #region Member Vars

        private string key;
        private string sortedColumns;
        private string tableKey;
        private bool? areCriticalTasksHighlighted;
        private bool? areSummaryTasksVisible;
        private bool? isOutlineStructurePreservedWhenSorting;
        private bool? isSlackVisible;
        private string timescale;
        private string nonWorkingTimeHighlightStyle;

        private string viewName;

        #endregion // Member Vars

        #region Public Properties

        public string Key
        {
            get { return key; }
            set
            {
                if (key != value)
                {
                    key = value;
                    this.OnPropertyChanged("Key");
                }
            }
        }

        public string SortedColumns
        {
            get { return sortedColumns; }
            set
            {
                if (sortedColumns != value)
                {
                    sortedColumns = value;
                    this.OnPropertyChanged("SortedColumns");
                }
            }
        }

        public string TableKey
        {
            get { return tableKey; }
            set
            {
                if (tableKey != value)
                {
                    tableKey = value;
                    this.OnPropertyChanged("TableKey");
                }
            }
        }

        public bool? AreCriticalTasksHighlighted
        {
            get { return areCriticalTasksHighlighted; }
            set
            {
                if (areCriticalTasksHighlighted != value)
                {
                    areCriticalTasksHighlighted = value;
                    this.OnPropertyChanged("AreCriticalTasksHighlighted");
                }
            }
        }

        public bool? AreSummaryTasksVisible
        {
            get { return areSummaryTasksVisible; }
            set
            {
                if (areSummaryTasksVisible != value)
                {
                    areSummaryTasksVisible = value;
                    this.OnPropertyChanged("AreSummaryTasksVisible");
                }
            }
        }

        public string NonWorkingTimeHighlightStyle
        {
            get { return nonWorkingTimeHighlightStyle; }
            set
            {
                if (nonWorkingTimeHighlightStyle != value)
                {
                    nonWorkingTimeHighlightStyle = value;
                    this.OnPropertyChanged("NonWorkingTimeHighlightStyle");
                }
            }
        }

        public bool? IsOutlineStructurePreservedWhenSorting
        {
            get { return isOutlineStructurePreservedWhenSorting; }
            set
            {
                if (isOutlineStructurePreservedWhenSorting != value)
                {
                    isOutlineStructurePreservedWhenSorting = value;
                    this.OnPropertyChanged("IsOutlineStructurePreservedWhenSorting");
                }
            }
        }

        public bool? IsSlackVisible
        {
            get { return isSlackVisible; }
            set
            {
                if (isSlackVisible != value)
                {
                    isSlackVisible = value;
                    this.OnPropertyChanged("IsSlackVisible");
                }
            }
        }

        public string Timescale
        {
            get { return timescale; }
            set
            {
                if (timescale != value)
                {
                    timescale = value;
                    this.OnPropertyChanged("Timescale");
                }
            }
        }

        public string ViewName
        {
            get { return viewName; }
            set
            {
                if (viewName != value)
                {
                    viewName = value;
                    OnPropertyChanged("ViewName");
                }
            }
        }

        #endregion // Public Properties

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
