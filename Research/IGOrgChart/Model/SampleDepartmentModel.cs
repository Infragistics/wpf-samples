using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.DataProviders;
using System.Xml.Linq;
using System.Linq;
using System.Collections.ObjectModel;

namespace IGOrgChart.Model
{
    public class SampleDepartmentModel : ObservableModel
    {
        public SampleDepartmentModel()
        {
            XmlDataProvider dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("DepartmentGroups.xml");
        }

        //Parses the XML data.
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                XDocument document = e.Result;

                this.DepartmentGroups = new ObservableCollection<object>
                (
                    document
                        .Element("DepartmentGroups")
                        .Elements("DepartmentGroup")
                        .Select
                        (
                            departmentGroup => new DepartmentGroup()
                            {
                                GroupName = departmentGroup.Attribute("GroupName").Value,
                                Departments = new ObservableCollection<Department>
                                (
                                    departmentGroup
                                    .Elements("Department")
                                    .Select
                                    (
                                        department => new Department()
                                        {
                                            DepartmentName = department.Attribute("DepartmentName").Value,
                                            EmployeePositions = new ObservableCollection<EmployeePosition>
                                            (
                                                department
                                                .Elements("EmployeePosition")
                                                .Select
                                                (
                                                    employeePosition => new EmployeePosition()
                                                    {
                                                        PositionName = employeePosition.Attribute("PositionName").Value,
                                                        Employees = new ObservableCollection<Employee>
                                                        (
                                                            employeePosition
                                                            .Elements("Employee")
                                                            .Select
                                                            (
                                                                employee => new Employee()
                                                                {
                                                                    FirstName = employee.Attribute("FirstName").Value,
                                                                    LastName = employee.Attribute("LastName").Value,
                                                                    ImagePath = ImageFilePathProvider.GetImageLocalPath() + "people/" + employee.Attribute("ImagePath").Value
                                                                }
                                                            )
                                                        )
                                                    }
                                                )
                                            )
                                        }
                                    )
                                )
                            }
                        )
                        .Cast<object>()
                 );

                OnPropertyChanged("DepartmentGroups");
            }
        }

        // The Department Groups.
        public ObservableCollection<object> DepartmentGroups { get; set; }
    }

}
