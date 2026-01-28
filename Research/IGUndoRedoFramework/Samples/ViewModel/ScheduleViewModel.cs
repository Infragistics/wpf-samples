using System.Collections.ObjectModel;
using IGUndoRedoFramework.Data;
using Infragistics.Samples.Shared.Models;
using Infragistics.Undo;

namespace IGUndoRedoFramework.ViewModel
{
    public class ScheduleViewModel : ObservableModel
    {
        private UndoManager _undoManager;
        private ObservableCollection<ResourceItem> _resources;
        private ObservableCollection<ResourceCalendarItem> _calendars;
        private ObservableCollection<AppointmentItem> _appointments;
        private ObservableCollection<JournalItem> _journals;
        private ObservableCollection<TaskItem> _tasks;
        private string _currentUserId;

        public ScheduleViewModel(UndoManager undoManager)
        {
            _resources = new ObservableCollection<ResourceItem>();
            _calendars = new ObservableCollection<ResourceCalendarItem>();
            _appointments = new ObservableCollection<AppointmentItem>();
            _journals = new ObservableCollection<JournalItem>();
            _tasks = new ObservableCollection<TaskItem>();
            _undoManager = undoManager;
        }

        public UndoManager UndoManager
        {
            get { return _undoManager; }
        }

        public string CurrentUserId
        {
            get { return _currentUserId; }
            internal set
            {
                _currentUserId = value;
                this.OnPropertyChanged("CurrentUserId");
            }
        }

        public ObservableCollection<ResourceItem> Resources { get { return _resources; } }
        public ObservableCollection<ResourceCalendarItem> Calendars { get { return _calendars; } }
        public ObservableCollection<AppointmentItem> Appointments { get { return _appointments; } }
        public ObservableCollection<JournalItem> Journals { get { return _journals; } }
        public ObservableCollection<TaskItem> Tasks { get { return _tasks; } }

    }
}
