using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using IGGantt.Resources;
using IGGantt.Samples.DataSource;
using IGGantt.Samples.Models;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;

namespace IGGantt.Samples.Organization
{
    public partial class Resources : SampleContainer
    {
        bool _isInitial = true;
        private ProjectSettings settings;
        private ObservableCollection<HumanResource> resources = null;

        public Resources()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (_isInitial)
            {
                if (this.Lb_Resources.HasItems)
                {
                    this.Lb_Resources.Items.Clear();
                }

                settings = new ProjectSettings();
                this.gantt.Project.Settings = settings;

                // Add available resources to the project
                resources = ProjectDataHelper.GenerateHumanResources();
                foreach (HumanResource hr in resources)
                {
                    ProjectResource pr = new ProjectResource();
                    pr.DisplayName = hr.Name;
                    pr.UniqueId = hr.PersonalID;

                    this.gantt.Project.ResourceItems.Add(pr);

                    // Fill the listbox 
                    AddListResourceItem(hr.Name, hr.PersonalID);
                }

                gantt.Project.ResourceItems.CollectionChanged += new NotifyCollectionChangedEventHandler(ResourceItems_CollectionChanged);

                this.Chb_AddNewResource.IsChecked = true;

                _isInitial = false;
            }
        }

        private void AddListResourceItem(string content, string tag)
        {
            // Load a list of available resources
            ListBoxItem item = new ListBoxItem();
            item.Content = content;
            item.Tag = tag;
            item.MouseDoubleClick += new MouseButtonEventHandler(item_MouseDoubleClick);

            Lb_Resources.Items.Add(item);
        }

        private void ResourceItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems;
            foreach (ProjectResource resource in newItems)
            {
                AddListResourceItem(resource.DisplayName, resource.UniqueId);
            }
        }

        private void item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string resourceId = (sender as ListBoxItem).Tag.ToString();
            
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
    }
}
