using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace IGTreeGrid.Samples.ViewModels
{
    public class WorkItemsObjectProvider : ObservableModel
    {
        private ObservableCollection<WorkItem> _workItems;

        public WorkItemsObjectProvider()
        {
            GetData();
        }

        public ObservableCollection<WorkItem> WorkItems
        {
            get { return _workItems; }
            set
            {
                if (_workItems != value)
                {
                    _workItems = value;
                    OnPropertyChanged("WorkItems");
                }
            }
        }

        private void GetData()
        {
            var dataProvider = new Infragistics.Samples.Shared.DataProviders.XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("WorkItems.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;
            XDocument doc = e.Result;
            this.WorkItems = this.GetWorkItems(doc.Root, "");
        }

        private ObservableCollection<WorkItem> GetWorkItems(XElement parent, string parentId)
        {
            return new ObservableCollection<WorkItem>(
                from d in parent.Descendants("WorkItem")
                where d.Attribute("ParentID").Value == parentId
                select new WorkItem
                {
                    ID = int.Parse(d.Attribute("ID").Value),
                    Title = d.Attribute("Title").Value,
                    Type = (WorkItemTypes)Enum.Parse(typeof(WorkItemTypes), d.Attribute("Type").Value),
                    State = (WorkItemStates)Enum.Parse(typeof(WorkItemStates), d.Attribute("State").Value),
                    AssignedTo = d.Attribute("AssignedTo").Value,
                    SubWorkItems = this.GetWorkItems(d, d.Attribute("ID").Value)
                });
        }
    }

    public enum WorkItemTypes
    {
        Task, Bug
    }

    public enum WorkItemStates
    {
        New, InReview, InProgress, InTest, Finished, Fixed, Verified
    }

    public class WorkItem
    {
        public int ID { get; set; }
        public WorkItemTypes Type { get; set; }
        public WorkItemStates State { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AssignedTo { get; set; }
        public ObservableCollection<WorkItem> SubWorkItems { get; set; }
    }
}