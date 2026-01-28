using System;
using System.Collections.ObjectModel;
using IGGantt.Samples.Models;
using IGGantt.Resources;

namespace IGGantt.Samples.DataSource
{
    public static class CustomTaskProvider
    {
        public static ObservableCollection<CustomTask> GenerateCustomTasks()
        {
            return new ObservableCollection<CustomTask>()
			{
				new CustomTask
				{
					DataItemId = "t1",
					TaskName = String.Format("{0} 1", GanttStrings.Task_TabHeader), // "Task 1"
					Start = DateTime.Now,
					Duration = TimeSpan.FromHours(8)
				},
                new CustomTask
				{
					DataItemId = "t2",
					TaskName = String.Format("{0} 2", GanttStrings.Task_TabHeader), // "Task 2",
					Duration = TimeSpan.FromHours(9),
                    Predecessors = "t1"
				},
				new CustomTask
				{
					DataItemId = "t3",
					TaskName = String.Format("{0} 3", GanttStrings.Task_TabHeader), // "Task 3",
					Duration = new TimeSpan(8, 30, 45),
                    Predecessors = "t2"
				},
				new CustomTask
				{
					DataItemId = "t4",
					TaskName = String.Format("{0} 4", GanttStrings.Task_TabHeader), // "Task 4",
					Duration = new TimeSpan(4, 20, 0),
                    Predecessors = "t3"
				},

				new CustomTask
				{
					DataItemId = "t5",
					TaskName = String.Format("{0} 5", GanttStrings.Task_TabHeader), // "Task 5",
					Duration = new TimeSpan(8,0,0),
                    Predecessors = "t4"
				}
			};
        }
    }
}
