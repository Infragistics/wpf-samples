using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGTree.Samples.Style
{
    public partial class CustomizingTheLayout : SampleContainer
    {
        public CustomizingTheLayout()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            DownloadDataSource();
        }


        void OnDirectReportsAreaLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // Hide the area that shows direct reports if the employee has none.
            FrameworkElement elem = sender as FrameworkElement;
            if (elem == null)
            {
                return;
            }

            Employee emp = elem.DataContext as Employee;
            if (emp == null)
            {
                return;
            }

            if (emp.DirectReports.Count == 0)
            {
                elem.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private XmlDataProvider xmlDataProvider;
        private void DownloadDataSource()
        {
            xmlDataProvider = new XmlDataProvider();
            xmlDataProvider.GetXmlDataCompleted += new EventHandler<GetXmlDataCompletedEventArgs>(xmlDataProvider_GetXmlDataCompleted);
            xmlDataProvider.GetXmlDataAsync("Employees.xml");
        }

        void xmlDataProvider_GetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs gxdcea)
        {
            XDocument doc = gxdcea.Result;

            if (doc != null)
            {
                var result = GetEmployees(doc.Root);
                dataTree.ItemsSource = result;
                dataTree.XamTreeItems[0].IsExpanded = true;
            }
        }

        private IList<Employee> GetEmployees(XElement parent)
        {
            return (from em in parent.Elements("Employees")
                    select new Employee
                    {
                        Id = em.Element("EmpID").Value,
                        Name = em.Element("EmpName").Value,
                        JobTitle = em.Element("JobTitle").GetString(),
                        EmployeeSince = em.Element("EmployeeSince").Value,
                        ImagePath = ImageFilePathProvider.GetImageLocalPath(String.Format("{0}/{1}", "People", em.Element("ImagePath").Value)),
                        ManagerName = parent.Element("EmpName").GetString(),
                        DirectReports = this.GetEmployees(em)
                    }).ToList<Employee>();
        }
    }
}