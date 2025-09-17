using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.DataProviders;

namespace IGOrgChart.Model
{
    public class DepartmentModel : ObservableModel
    {
        /// <summary>
        /// Creates a new <see cref="DepartmentModel"/> object and downloads sample data.
        /// </summary>
        public DepartmentModel()
        {
            XmlDataProvider dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("DepartmentGroups.xml");
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

                this.DepartmentGroups = new ObservableCollection<DepartmentGroup>
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
                 );
            }
            else
            {
                GenerateDesignData();
            }

            OnPropertyChanged("DepartmentGroups");
            OnPropertyChanged("Organizaiton");
        }

        /// <summary>
        /// Generates sample data if the sample is in design mode or the sample data file fails to load. 
        /// </summary>
        private void GenerateDesignData()
        {
            Employee employee = new Employee() { FirstName = "John", LastName = "Doe" };
            EmployeePosition employeePosition = new EmployeePosition() { PositionName = "Developer", Employees = new ObservableCollection<Employee>() { employee } };
            Department department = new Department() { DepartmentName = "Development", EmployeePositions = new ObservableCollection<EmployeePosition>() { employeePosition } };
            DepartmentGroup departmentGroup = new DepartmentGroup() { GroupName = "Engineering", Departments = new ObservableCollection<Department>() { department } };

            this.DepartmentGroups = new ObservableCollection<DepartmentGroup>() { departmentGroup };
        }

        /// <summary>
        /// The Department Groups.
        /// </summary>
        public ObservableCollection<DepartmentGroup> DepartmentGroups { get; set; }
    }
}
