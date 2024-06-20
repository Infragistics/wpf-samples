using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Xml.Linq;
using IGGantt.Samples.Models;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;

namespace IGGantt.Samples.ViewModel
{
    public class DataBindingUsingListBackedProjectViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<TaskModel> tasks = null;

        public DataBindingUsingListBackedProjectViewModel()
        {
            DownloadDataSource();
        }

        public ObservableCollection<TaskModel> Tasks
        {
            get
            {
                return tasks;
            }
            set
            {
                if (value != null)
                {
                    tasks = value;
                    OnPropertyChanged("Tasks");
                }
            }
        }

        private void DownloadDataSource()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("TaskData.xml");
        }

        /// <summary>
        /// Ths methods' XLinq Query uses special extention methods for converting data. 
        /// The extention methods can be found in the Infragistics.Samples.Shared.Extensions\XElementExtension class. 
        /// </summary>
        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            IEnumerable<XElement> elements = doc.Root.Elements();

            ObservableCollection<TaskModel> _tasks = new ObservableCollection<TaskModel>();

            foreach (XElement el in elements)
            {
                TaskModel task = new TaskModel();

                task.TaskID = el.Element("TaskID").GetString();
                task.Name = el.Element("Name").GetString();
                task.IsInProgress = el.Element("IsInProgress").GetBool();
                task.Start = DateTime.Today.ToUniversalTime();
                task.IsMilestone = el.Element("IsMilestone").GetBool();
                task.DurationInHours = TimeSpan.FromHours(el.Element("DurationInHours").GetDouble());
                task.IsUndetermined = el.Element("IsUndetermined").GetBool();
                task.ConstraintType = (ProjectTaskConstraintType)el.Element("ConstraintType").GetInt();

                if (el.Element("DeadlineDateInHours").GetInt() != 0)
                {
                    task.DeadlineDate = DateTime.Today.AddHours(el.Element("DeadlineDateInHours").GetInt()).ToUniversalTime();
                }

                task.DurationFormat = ProjectDurationFormat.Days;

                _tasks.Add(task);
            }
            this.Tasks = _tasks;
        }

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
