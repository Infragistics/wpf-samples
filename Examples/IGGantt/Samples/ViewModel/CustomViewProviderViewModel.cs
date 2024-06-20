using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using IGGantt.Resources;
using IGGantt.Samples.Models;

namespace IGGantt.Samples.ViewModel
{
    public class CustomViewProviderViewModel : INotifyPropertyChanged
    {
        #region Private variables

        private ObservableCollection<CustomProjectView> views;
        private ObservableCollection<CustomProjectTable> tables;
        private ObservableCollection<CustomProjectColumn> columns;

        private ObservableCollection<CustomTask> tasks;

        private CustomProjectView selectedView;

        #endregion // Private variables

        #region Constructor

        public CustomViewProviderViewModel()
        {
            SelectedView = Views[0];
        }

        #endregion // Constructor

        #region Public properties

        public CustomProjectView SelectedView
        {
            get
            {
                return selectedView;
            }
            set
            {
                if (value != selectedView)
                {
                    selectedView = value;
                    OnPropertyChanged("SelectedView");
                }
            }
        }

        public ObservableCollection<CustomProjectColumn> Columns
        {
            get
            {
                if (columns == null)
                {
                    columns = GenerateColumns();
                }
                return columns;
            }
        }

        public ObservableCollection<CustomProjectTable> Tables
        {
            get
            {
                if (tables == null)
                {
                    tables = GenerateTables();
                }
                return tables;
            }
            set
            {
                if (tables != value)
                {
                    tables = value;
                }
            }
        }

        public ObservableCollection<CustomProjectView> Views
        {
            get
            {
                if (views == null)
                {
                    views = GenerateViews();
                }
                return views;
            }
            set
            {
                if (views != value)
                {
                    views = value;
                    OnPropertyChanged("CustomViews");
                }
            }
        }

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

        public DateTime Today
        {
            get { return DateTime.Today; }
        }

        #endregion // Public properties

        #region Private helpers

        private ObservableCollection<CustomProjectColumn> GenerateColumns()
        {
            return new ObservableCollection<CustomProjectColumn>()
            {
                new CustomProjectColumn
                {
                    Id = "idTaskName",
                    Key = "TaskName",
                    HeaderText = GanttStrings.ResourceName // "Name"
                },
                new CustomProjectColumn
                {
                    Id = "idStart",
                    Key = "ManualStart",
                    HeaderText = GanttStrings.Start, //"Start",
                    HeaderTextHorizontalAlignment = "Center"
                },
                new CustomProjectColumn
                {
                    Id = "idFinish",
                    Key = "ManualFinish",
                    HeaderText = GanttStrings.Finish, // "Finish",
                    HeaderTextHorizontalAlignment = "Center"
                },
                new CustomProjectColumn
                {
                    Id = "idDuration",
                    Key = "ManualDuration",
                    HeaderText = GanttStrings.Duration //"Duration"
                },
                new CustomProjectColumn
                {
                    Id = "idPredecessors",
                    Key = "PredecessorsIdText",
                    HeaderText = GanttStrings.Predecessors // "Predecessors"
                },
                new CustomProjectColumn
                {
                    Id="idIsCritical",
                    Key = "IsCritical",
                    HeaderText = GanttStrings.IsCritical // "Is Critical"
                }
            };
        }

        private ObservableCollection<CustomProjectTable> GenerateTables()
        {
            return new ObservableCollection<CustomProjectTable>()
            {
                new CustomProjectTable
                {
                    Key = "Table01",
                    // Comma separated column ids, which are provided by the ProjectColumnProperty.DataItemId property mapping
                    ColumnIds = "idTaskName, idStart, idDuration, idFinish, idPredecessors, idIsCritical",
                    ShowInMenu = true
                },
                new CustomProjectTable
                {
                    Key = "Table02",
                    ColumnIds = "idTaskName, idStart, idFinish, idIsCritical",
                    ShowInMenu = true
                },
                new CustomProjectTable
                {
                    Key = "Table03",
                    ColumnIds = "idTaskName, idDuration, idPredecessors",
                    ShowInMenu = true
                }
            };
        }

        private ObservableCollection<CustomProjectView> GenerateViews()
        {
            return new ObservableCollection<CustomProjectView>()
            {
                new CustomProjectView
                {
                    Key = "View01",
                    AreSummaryTasksVisible = true,
                    AreCriticalTasksHighlighted = true,
                    // Comma separated column keys, where each key can optionally be followed by 'Ascending' or 'Descending' word to indicate 
                    // that the column should be sorted ascending or descending
                    SortedColumns = "ManualStart, ManualFinish:Descending",
                    IsOutlineStructurePreservedWhenSorting = true,
                    TableKey = "Table01",
                    NonWorkingTimeHighlightStyle = "ActualNonWorkingHours",
                    ViewName = String.Format("{0} 1", GanttStrings.View_TabHeader)
                },
                new CustomProjectView
                {
                    Key = "View02",
                    TableKey = "Table02",
                    AreCriticalTasksHighlighted = false,
                    AreSummaryTasksVisible = false,
                    ViewName = String.Format("{0} 2", GanttStrings.View_TabHeader)
                },
                new CustomProjectView
                {
                    Key = "View03",
                    TableKey = "Table03",
                    AreCriticalTasksHighlighted = true,
                    ViewName = String.Format("{0} 3", GanttStrings.View_TabHeader)
                }
            };
        }

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

        #endregion // Private helpers

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