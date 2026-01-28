using System.Collections.ObjectModel;
using System.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using IGOrgChart.Resources;

namespace IGOrgChart.Samples.Performance
{
    public partial class LargeData : SampleContainer
    {
        public LargeData()
        {
            InitializeComponent();
            OrgChart.ItemsSource = new SampleEmployeeModel().Employees;
        }
    }

    public class SampleEmployeeModel : ObservableModel
    {
        // A collection with all Employees.
        public ObservableCollection<Employee> Employees { get; set; }

        public SampleEmployeeModel()
        {
            this.Employees = new ObservableCollection<Employee> { CreateTree(5, 5) };
        }

        private Employee CreateTree(int depth, int nodesPerLevel)
        {
            Employee employee = new Employee();
            employee.FullName = OrgChartStrings.OrgChart_Employee;

            if (depth > 0)
            {
                employee.Subordinates = new ObservableCollection<Employee>
                    (from index in Enumerable.Range(0, nodesPerLevel)
                     select CreateTree(depth - 1, nodesPerLevel));
            }

            return employee;
        }
    }
}
