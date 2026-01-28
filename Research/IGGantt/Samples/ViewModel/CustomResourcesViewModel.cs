using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using IGGantt.Resources;
using IGGantt.Samples.Models;

namespace IGGantt.Samples.ViewModel
{
    public class CustomResourcesViewModel: INotifyPropertyChanged
    {
        #region Private variables

        private ObservableCollection<CustomTask> taskAndResources;
        private ObservableCollection<CustomResource> resources;

        #endregion Private variables

        #region Public Properties

        public ObservableCollection<CustomTask> Tasks
        {
            get 
            {
                if (taskAndResources == null)
                {
                    taskAndResources = GenerateTasks();
                }

                return taskAndResources; 
            }
            set
            {
                if (value != taskAndResources)
                {
                    taskAndResources = value;
                    OnPropertyChanged("Tasks");
                }
            }
        }

        public ObservableCollection<CustomResource> Resources
        {
            get
            {
                if (resources == null)
                {
                    resources = GenerateResources();
                }
                return resources;
            }
        }

        #endregion Public Properties

        #region Private helpers

        private ObservableCollection<CustomResource> GenerateResources()
        {
            return new ObservableCollection<CustomResource>()
            {
                new CustomResource { Id = "res01", Name = GanttStrings.pdhLindaHamilton},
                new CustomResource { Id = "res02", Name = GanttStrings.pdhJohnSmith},
                new CustomResource { Id = "res03", Name = GanttStrings.pdhPeterGreen},
                new CustomResource { Id = "res04", Name = GanttStrings.pdhJuliePeterson},
                new CustomResource { Id = "res05", Name = GanttStrings.pdhAlexPetrov}
            };
        }

        private ObservableCollection<CustomTask> GenerateTasks()
        {
            return new ObservableCollection<CustomTask>()
			{
				new CustomTask
				{
					DataItemId = "planning",
					TaskName = GanttStrings.PlanningTask,
					Start = DateTime.Now,
					Duration = TimeSpan.FromHours(8),
                    Resources = "res02, res03, res05"
				},
                new CustomTask
				{
					DataItemId = "development",
					TaskName = GanttStrings.DevelopmentTask, 
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "planning",
                    Resources = "res02, res03"
				},
				new CustomTask
				{
					DataItemId = "createapplication",
					TaskName = GanttStrings.CreateApplicationTask,
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "planning",
                    Resources = "res03"
				},
				new CustomTask
				{
					DataItemId = "document",
					TaskName = GanttStrings.DocumentTask,
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "development",
                    Resources = "res04"
				},
                new CustomTask
				{
					DataItemId = "testing",
					TaskName = GanttStrings.TestingTask,
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "development"
				},
				new CustomTask
				{
					DataItemId = "design",
					TaskName = GanttStrings.DesignTask,
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "createapplication",
                    Resources = "res05"
				},
                new CustomTask
				{
					DataItemId = "collectfeedback",
					TaskName = GanttStrings.CollectFeedbackTask,
					Duration = TimeSpan.FromHours(8),
                    Predecessors = "design",
                    Resources = "res01"
				}
			};
        }

        #endregion // Private helpers

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
