using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using IGGantt.Resources;
using IGGantt.Samples.Models;

namespace IGGantt.Samples.ViewModel
{
    public class ConflictResolutionViewModel : INotifyPropertyChanged
    {
        #region Private variables

        private ObservableCollection<CustomTask> tasks;

        #endregion // Private variables

        #region Public Properties

        public ObservableCollection<CustomTask> Tasks
        {
            get
            {
                if (tasks == null)
                {
                    tasks = GenerateCustomTasks();
                }
                return tasks;
            }
            set
            {
                tasks = value;
            }
        }

        #endregion // Pubilc properties

        #region Private helper methods

        private ObservableCollection<CustomTask> GenerateCustomTasks()
        {
            return new ObservableCollection<CustomTask>()
			{
				new CustomTask
				{
					DataItemId = "t1",
					TaskName = String.Format("{0} 1", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8)
				},
                new CustomTask
				{
					DataItemId = "t2",
					TaskName = String.Format("{0} 2", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t1"
				},
				new CustomTask
				{
					DataItemId = "t3",
					TaskName = String.Format("{0} 3", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t2"
				},
				new CustomTask
				{
					DataItemId = "t4",
					TaskName = String.Format("{0} 4", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t3"
				},
				new CustomTask
				{
					DataItemId = "t5",
					TaskName = String.Format("{0} 5", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t4"
				},
                new CustomTask
				{
					DataItemId = "t6",
					TaskName = String.Format("{0} 6", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t5"
				},
                new CustomTask
				{
					DataItemId = "t7",
					TaskName = String.Format("{0} 7", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t6"
				},
                new CustomTask
				{
					DataItemId = "t8",
					TaskName = String.Format("{0} 8", GanttStrings.Task_TabHeader ),
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "t7"
				}
			};
        }

        #endregion // Private helper methods

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
