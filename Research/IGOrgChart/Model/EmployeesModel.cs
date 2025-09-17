using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.DataProviders;
using System;
using System.Windows.Media.Imaging;

namespace IGOrgChart.Model
{
    public class EmployeesModel : ObservableModel
    {
        public ObservableCollection<Employee> Employees { get; set; }

        public EmployeesModel()
        {
            XmlDataProvider dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Employees.xml");
        }

        /// <summary>
        /// Handles the OnGetXmlDataCompleted event and parses the downloaded XML data.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="e">A OnGetXmlDataCompleted object,</param>
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                XDocument document = e.Result;
                this.Employees = new ObservableCollection<Employee>(GetEmployees(document.Root));
            }
            else
            {
                GenerateDesignData();
            }
            OnPropertyChanged("Employees");
        }

        /// <summary>
        /// Parses the XML data.
        /// </summary>
        /// <param name="parent">The parent <see cref="XElement"/> node</param>
        /// <returns>The parsed employees for the current level.</returns>
        private IEnumerable<Employee> GetEmployees(XElement parent)
        {
            return (from employee in parent.Elements("Employees")
                select new Employee
                    {
                        Id = employee.Element("EmpID").Value,
                        FullName = employee.Element("EmpName").Value,
                        JobTitle = employee.Element("JobTitle").Value,
                        EmployeeSince = employee.Element("EmployeeSince").Value,
                        ImagePath = ImageFilePathProvider.GetImageLocalPath() + "people/" + employee.Element("ImagePath").Value,
                        Subordinates = new ObservableCollection<Employee>(GetEmployees(employee))
                    });
        }

        /// <summary>
        /// Generates sample data if the sample is in design mode or the sample data file fails to load. 
        /// </summary>
        private void GenerateDesignData()
        {
            Employee employee = new Employee()
                {
                    Id = "1000",
                    FullName = "David Green",
                    JobTitle = "Chief Technology Officer",
                    ImagePath = ImageFilePathProvider.GetImageLocalPath() + "people/GUY01.jpg",
                    EmployeeSince = "2000"
                };

            this.Employees = new ObservableCollection<Employee>() { employee };
        }
    }
}
