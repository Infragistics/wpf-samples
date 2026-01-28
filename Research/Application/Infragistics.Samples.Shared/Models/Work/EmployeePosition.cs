using System.Collections.ObjectModel;

namespace Infragistics.Samples.Shared.Models
{
    public class EmployeePosition
    {
        /// <summary>
        /// The name of the Employee Position.
        /// </summary>
        public string PositionName { get; set; }

        /// <summary>
        /// The Employees that are on the current Employee Position.
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
    }
}