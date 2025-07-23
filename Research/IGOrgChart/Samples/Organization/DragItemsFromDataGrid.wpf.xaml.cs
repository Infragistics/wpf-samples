using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Maps;
using Infragistics.DragDrop;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using IGOrgChart.Resources;

namespace IGOrgChart.Samples.Organization
{
    public partial class DragItemsFromDataGrid : SampleContainer
    {
        //An instance of the SampleEmployeeModel.
        private SampleEmployeeModel _employeeModel;

        //The style applied to the node drop targets.
        private System.Windows.Style _dropTargetStyle;

        public DragItemsFromDataGrid()
        {
            InitializeComponent();
            
            //Get the EmployeeModel.
            _employeeModel = OrgChart.DataContext as SampleEmployeeModel;

            //Get the drop target style from the resources.
            _dropTargetStyle = this.Resources["DropTargetStyle"] as System.Windows.Style;
        }

        //This method is called whenever a node is brought into view. It makes the node a drop target.
        private void OrgChart_NodeControlAttachedEvent(object sender, OrgChartNodeEventArgs e)
        {
            //Make the node a drop target.
            DragDropManager.SetDropTarget(e.Node, new DropTarget()
                {
                    IsDropTarget = true,
                    DropTargetStyle = _dropTargetStyle
                });
        }

        private void ButtonReset_Click(object sender, RoutedEventArgs e)
        {
            //Reset the assigned employees.
            _employeeModel.ResetAssignedEmployees();
        }

        private void DataGridRow_DragStart(object sender, DragDropStartEventArgs e)
        {
            //Assing the DataItem of the selected row as data context for the Dragged Item.
            //This is needed for the DragTemplate.
            e.Data = e.DragSource.GetValue(DataContextProperty);
        }

        // Perform a drop and update the underlying data source.
        private void DataGridRow_Drop(object sender, DropEventArgs e)
        {
            Employee dragSource = e.Data as Employee;

            if (e.DropTarget is OrgChartNodeControl)
            {
                Employee dropTarget = ((OrgChartNodeControl)e.DropTarget).Node.Data as Employee;

                dropTarget.Subordinates.Add(dragSource);
            }
            else //if the Drop Target is the XamOrgChart
            {
                _employeeModel.AssignedEmployees.Add(dragSource);
            }

            //Remove the dropped Employee from the grid.
            _employeeModel.UnassignedEmployees.Remove((Employee)dragSource);
        }
    }

    public class SampleEmployeeModel : ObservableModel
    {
        // A collection with all Employees.
        private ObservableCollection<Employee> Employees { get; set; }

        // The Employees that are assigned.
        public ObservableCollection<Employee> AssignedEmployees { get; set; }

        // The Employees that are awating assignment.
        public ObservableCollection<Employee> UnassignedEmployees { get; set; }

        public SampleEmployeeModel()
        {
            //Create sample Employee objects.
            this.Employees = new ObservableCollection<Employee>
                (
                Enumerable
                    .Range(1, 20)
                    .Select(index => new Employee() {FullName = OrgChartStrings.OrgChart_Employee + " " + index})
                );

            this.Employees[0].Subordinates.Add(this.Employees[1]);
            this.Employees[0].Subordinates.Add(this.Employees[2]);
            this.Employees[1].Subordinates.Add(this.Employees[3]);
            this.Employees[1].Subordinates.Add(this.Employees[4]);

            this.AssignedEmployees = new ObservableCollection<Employee>() {this.Employees[0]};
            this.UnassignedEmployees = new ObservableCollection<Employee>(this.Employees.Skip(5));
        }

        // Clear all Assigned Employees.
        public void ResetAssignedEmployees()
        {
            //Clear all Subordinates.
            foreach (Employee employee in this.Employees)
            {
                employee.Subordinates.Clear();
            }

            //Restore the unassigned Employees collection.
            this.UnassignedEmployees = new ObservableCollection<Employee>(this.Employees);

            //Clear the assigned Employees collection.
            this.AssignedEmployees.Clear();

            OnPropertyChanged("AssignedEmployees");
            OnPropertyChanged("UnassignedEmployees");
        }
    }
}
