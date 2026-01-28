using System;
using System.Collections.ObjectModel;
using System.Windows;
using IGMultiColumnComboEditor.Samples.Organization;
using Infragistics;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;

namespace IGMultiColumnComboEditor.Samples.Display
{
    /// <summary>
    /// Interaction logic for XamMultiComboFeatures.xaml
    /// </summary>
    public partial class MultiColumnComboEditorFeatures : SampleContainer
    {
        ObservableCollection<Person> people;

        public MultiColumnComboEditorFeatures()
        {
            InitializeComponent();
            GetData();
            xamMultiColumnComboEditor.ItemsSource = people;
        }

        private void GetData()
        {
            people = new ObservableCollection<Person>();
            people.Add(new Person { Name = "Joe Smith", Title = "VP", IsManager = true, Level = 3, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(1)) });
            people.Add(new Person { Name = "Tom Jones", Title = "Team Leader", IsManager = true, Level = 2, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(2)) });
            people.Add(new Person { Name = "Bill Bailey", Title = "Salesperson", IsManager = false, Level = 1, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(3)) });
            people.Add(new Person { Name = "John Doe", Title = "Specialist", IsManager = false, Level = 0, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(4)) });
            people.Add(new Person { Name = "Jolene Jones", Title = "Writer", IsManager = false, Level = 0, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(5)) });
            people.Add(new Person { Name = "John Doroski", Title = "Accountant", IsManager = false, Level = 2, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(6)) });
            people.Add(new Person { Name = "Billiam Clark", Title = "Manager", IsManager = true, Level = 3, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(7)) });
            people.Add(new Person { Name = "Tomas Lundstrom", Title = "Furrier", IsManager = false, Level = 0, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(8)) });
            people.Add(new Person { Name = "Harry Carey", Title = "Plumber", IsManager = false, Level = 0, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(9)) });
            people.Add(new Person { Name = "Harry Carson", Title = "Glazier", IsManager = false, Level = 0, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(10)) });
            people.Add(new Person { Name = "Happy Rockefeller", Title = "Roofer", IsManager = false, Level = 0, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(11)) });
            people.Add(new Person { Name = "John Salesman", Title = "Salesman", IsManager = false, Level = 0, HireDate = DateTime.Now.Subtract(TimeSpan.FromDays(12)) });
        }
    }
}
