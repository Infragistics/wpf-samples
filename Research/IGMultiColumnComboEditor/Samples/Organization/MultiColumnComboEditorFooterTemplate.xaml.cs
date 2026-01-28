using System;
using System.Collections.ObjectModel;
using Infragistics.Samples.Framework;

namespace IGMultiColumnComboEditor.Samples.Organization
{
    /// <summary>
    /// Interaction logic for MultiComboFooterTemplate.xaml
    /// </summary>
    public partial class MultiColumnComboEditorFooterTemplate : SampleContainer
    {
        ObservableCollection<Person> people;

        public MultiColumnComboEditorFooterTemplate()
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

    public class Person
    {
        public Person()
        {
        }
        public override string ToString()
        {
            return "Person - Name: " + Name + ", Title: " + Title + ", Level: " + Level + ", IsManager: " + IsManager.ToString();
        }
        public string Name { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public bool IsManager { get; set; }
        public DateTime HireDate { get; set; }
        private string this[string columnName]
        {
            get
            {
                return null;
            }
        }
    }

    public class FilterItem
    {
        public int? ID { get; set; }
        public string Name { get; set; }
    }
}



