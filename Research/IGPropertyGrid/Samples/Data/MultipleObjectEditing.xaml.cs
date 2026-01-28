using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.DataPresenter;
using System.Windows.Controls;

namespace IGPropertyGrid.Samples.Data
{
    public partial class MultipleObjectEditing : SampleContainer
    {
        public MultipleObjectEditing()
        {
            InitializeComponent();
            InitializeSampleData();
        }

        void InitializeSampleData()
        {
            EmployeeData emp = new EmployeeData();
            this.ListOfItems.ItemsSource = emp.EmployeeDataView;
        }

        private void ListOfItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            object[] objs = new object[lb.SelectedItems.Count];
            lb.SelectedItems.CopyTo(objs, 0);
            this.xamPropertyGrid1.SelectedObjects = objs;
        }
    }
}
