using IGPropertyGrid.Resources;
using IGPropertyGrid.Samples.DataModel;
using Infragistics.Samples.Framework;
using System.Threading;
using System.Windows;

namespace IGPropertyGrid.Samples.Editing
{
    public partial class ChangeDefaultEditor : SampleContainer
    {
        public ChangeDefaultEditor()
        {
            InitializeComponent();
            InitializeSampleData();
        }

        void InitializeSampleData()
        {
            if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                this.xamPropertyGrid1.SelectedObject = new EmployeeJA()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Department = Departments.Engineering,
                    Company = PropertyGridStrings.companyName,
                    Age = 31
                };
            }
            else
            {
                this.xamPropertyGrid1.SelectedObject = new EmployeeEN()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Department = Departments.Engineering,
                    Company = PropertyGridStrings.companyName,
                    Age = 31
                };
            }
        }
    }
}
