using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.DataProviders;

namespace IGOrgChart.Samples.Display
{
    public partial class GlobalNodeLayouts : SampleContainer
    {
        public GlobalNodeLayouts()
        {
            InitializeComponent();
        }
    }

    public class SampleDepartmentModel : ObservableModel
    {
        public SampleDepartmentModel()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Employees.xml");
        }

        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error == null) 
            {
                XDocument document = e.Result; 
                this.Employees = new ObservableCollection<Employee>(GetEmployees(document.Root));

                OnPropertyChanged("Employees");
            }
        }

        // Parses the XML data.
        private IEnumerable<Employee> GetEmployees(XElement parent)
        {
            return (from employee in parent.Elements("Employees")
                    select new Employee
                    {
                        FullName = employee.Element("EmpName").Value,
                        JobTitle = employee.Element("JobTitle").Value,
                        ImagePath = employee.Element("ImagePath").Value,
                        Subordinates = new ObservableCollection<Employee>(GetEmployees(employee))
                    });
        }

        // The Employees.
        public ObservableCollection<Employee> Employees { get; set; }
    }
}
