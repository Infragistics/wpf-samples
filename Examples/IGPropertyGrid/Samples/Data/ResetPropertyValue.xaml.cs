using IGPropertyGrid.Resources;
using IGPropertyGrid.Samples.DataModel;
using Infragistics.Samples.Framework;
using System.Collections;
using System.Threading;

namespace IGPropertyGrid.Samples.Data
{
    public partial class ResetPropertyValue : SampleContainer
    {
        public ResetPropertyValue()
        {
            InitializeComponent();
            InitializeSampleData();
        }

        void InitializeSampleData()
        {
            ArrayList objectsList = new ArrayList();

            if (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                objectsList.Add(new EmployeeJA()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Age = 31,
                    Gender = Genders.M
                });

                objectsList.Add(new EmployeeMethodsJA()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Age = 31,
                    Gender = Genders.M
                });
            }
            else
            {
                objectsList.Add(new EmployeeEN()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Age = 31,
                    Gender = Genders.M
                });

                objectsList.Add(new EmployeeMethodsEN()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Age = 31,
                    Gender = Genders.M
                });
            }

            this.ListOfObjects.ItemsSource = objectsList;
            this.ListOfObjects.SelectedIndex = 0;
        }
    }
}
