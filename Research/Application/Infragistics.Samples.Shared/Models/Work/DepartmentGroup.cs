using System.Collections.ObjectModel;
 
namespace Infragistics.Samples.Shared.Models
{
    public class DepartmentGroup
    {
        /// <summary>
        /// The name of the Department Group.
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// The Departments that current Department Group has.
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }
    }
}