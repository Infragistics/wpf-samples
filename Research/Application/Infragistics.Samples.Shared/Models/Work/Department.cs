using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class Department
    {
        /// <summary>
        /// The name of the Department.
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// The Employee Positions that the current Department has.
        /// </summary>
        public ObservableCollection<EmployeePosition> EmployeePositions { get; set; }
    }
}