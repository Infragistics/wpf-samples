using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using IGGantt.Resources;
using IGGantt.Samples.DataSource;
using IGGantt.Samples.Models;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Organization
{
    public partial class Resources : SampleContainer
    {
        private ProjectSettings settings;
        private ObservableCollection<HumanResource> resources = null;

        public Resources()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            settings = new ProjectSettings();
            this.gantt.Project.Settings = settings;

            // Add resources to the project
            resources = ProjectDataHelper.GenerateHumanResources();
            foreach (HumanResource hr in resources)
            {
                ProjectResource pr = new ProjectResource();
                pr.DisplayName = hr.Name;
                pr.UniqueId = hr.PersonalID;

                this.gantt.Project.ResourceItems.Add(pr);
            }

            this.gantt.Project.ResourceItems.CollectionChanged += new NotifyCollectionChangedEventHandler(ResourceItems_CollectionChanged);

            this.Chb_AddNewResource.IsChecked = true;

            this.Cmb_Resources.ItemsSource = resources;
            this.Cmb_Resources.DisplayMemberPath = "Name";
            this.Cmb_Resources.SelectedValuePath = "PersonalID";
        }

        private void ResourceItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems;
            foreach (ProjectResource resource in newItems)
            {
                resources.Add(new HumanResource { Name = resource.DisplayName, PersonalID = resource.UniqueId });
            }
        }       

        private void Chb_AddNewResource_Checked(object sender, RoutedEventArgs e)
        {
            // Enable entering new resources for the project
            settings.AutoAddNewResources = true;
        }

        private void Chb_AddNewResource_Unchecked(object sender, RoutedEventArgs e)
        {
            // Entering resources only from the available resource collection
            settings.AutoAddNewResources = false;
        }

        private void Btn_AddResource_Click(object sender, RoutedEventArgs e)
        {
            string resourceId = Cmb_Resources.SelectedValue.ToString();

            if (this.gantt.ActiveCell.HasValue)
            {
                ProjectTaskResource resource = new ProjectTaskResource(resourceId);
                try
                {
                    this.gantt.ActiveCell.Value.Row.Task.Resources.Add(resource);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else
            {
                MessageBox.Show(GanttStrings.TaskNotSelectedMsg);
            }
        }
    }
}
