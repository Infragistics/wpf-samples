using System.Collections.ObjectModel;
using IGOutlookBar.Resources;
using Infragistics.Samples.Framework;

namespace IGOutlookBar.Samples.Data
{
    public partial class DataBindingGroupsSource : SampleContainer
    {
        // Member variable that stores a reference to a collection of TaskGroup objects.
        private ObservableCollection<TaskGroup> _taskGroups;

        public DataBindingGroupsSource()
        {
            InitializeComponent();

            // Set the DataContext of the page to the collection of TaskGroups so we can easily bind elements to the data in our XAML.
            this.DataContext = this.TaskGroups;
        }
        // Property that returns a collection Of TaskGroup objects
        public ObservableCollection<TaskGroup> TaskGroups
        {
            get
            {
                // If we haven't yet created and populated the collection of TaskGroup objects, create and populate it now.
                if (this._taskGroups == null)
                {
                    this._taskGroups = new ObservableCollection<TaskGroup>();

                    TaskGroup taskgroup1 = new TaskGroup(OutlookBarStrings.DataBindingGroupsSource_TaskGroup1,
                        @"/IGOutlookBar;component/Images/OBarTeamTasks.png");
                    taskgroup1.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task1,
                        @"/IGOutlookBar;component/Images/OBarCreateNew.png"));
                    taskgroup1.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task2,
                        @"/IGOutlookBar;component/Images/OBarUpdateTime.png"));
                    taskgroup1.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task3,
                        @"/IGOutlookBar;component/Images/OBarSelfEv.png"));
                    taskgroup1.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task4,
                        @"/IGOutlookBar;component/Images/OBarDiscuss.png"));

                    this._taskGroups.Add(taskgroup1);

                    TaskGroup taskgroup2 = new TaskGroup(OutlookBarStrings.DataBindingGroupsSource_TaskGroup2,
                        @"/IGOutlookBar;component/Images/OBarMyTasks.png");
                    taskgroup2.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task5,
                        @"/IGOutlookBar;component/Images/OBarCall.png"));
                    taskgroup2.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task6,
                        @"/IGOutlookBar;component/Images/OBarSend.png"));
                    taskgroup2.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task7,
                        @"/IGOutlookBar;component/Images/OBarCall.png"));
                    taskgroup2.Tasks.Add(new Task(OutlookBarStrings.DataBindingGroupsSource_Task8,
                        @"/IGOutlookBar;component/Images/OBarAsk.png"));

                    this._taskGroups.Add(taskgroup2);
                }
                return this._taskGroups;
            }
        }
    }

    // The TaskGroup class definition.
    public class TaskGroup
    {
        // Member variable that stores a reference to a collection of TaskGroup objects.
        private ObservableCollection<Task> _tasks;

        public TaskGroup(string title, string image)
        {
            this.Title = title;
            this.Image = image;
        }

        // Properties that return the Title and Image associated with the TaskGroup.
        public string Title { get; set; }
        public string Image { get; set; }


        // Property that returns a collection of Task objects
        public ObservableCollection<Task> Tasks
        {
            get
            {
                // If we haven't yet created the collection of Task objects, create it now.
                if (this._tasks == null)
                    this._tasks = new ObservableCollection<Task>();

                return this._tasks;
            }
        }
    }

    // The Task class definition.
    public class Task
    {
        public Task(string text, string image)
        {
            this.Text = text;
            this.Image = image;
        }

        // Properties that return the Text and Image associated with the TaskGroup.
        public string Text { get; set; }
        public string Image { get; set; }
    }
}
