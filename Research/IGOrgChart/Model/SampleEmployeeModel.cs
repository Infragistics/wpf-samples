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
using System.Collections.ObjectModel;
using IGOrgChart.Resources;
using System.Linq;

namespace IGOrgChart.Model
{
    public class SampleEmployeeModel : ObservableModel
    {
        // A collection with all Employees.
        public ObservableCollection<Employee> Employees { get; set; }

        public SampleEmployeeModel()
        {
            this.Employees = new ObservableCollection<Employee> { CreateTree(5, 2) };
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
