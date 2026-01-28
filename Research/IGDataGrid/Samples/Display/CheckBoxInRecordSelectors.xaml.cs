using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;

namespace IGDataGrid.Samples.Display
{
    /// <summary>
    /// Interaction logic for CheckBoxInRecordSelectors.xaml
    /// </summary>
    public partial class CheckBoxInRecordSelectors : SampleContainer
    {
        public CheckBoxInRecordSelectors()
        {
            InitializeComponent();

            List<PersonViewModel> members =
                (from p in Person.GetPeople()
                 select new PersonViewModel(p))
                .ToList();

            base.DataContext = new CommunityViewModel(members);
        }
        void OnShowRecords(object sender, RoutedEventArgs e)
        {
            List<string> checkedNames = new List<string>();
            List<string> uncheckedNames = new List<string>();

            var community = base.DataContext as CommunityViewModel;
            foreach (PersonViewModel p in community.Members)
            {
                if (p.IsChecked)
                    checkedNames.Add(p.LastName);
                else
                    uncheckedNames.Add(p.LastName);
            }

            checkedNames.Sort();
            uncheckedNames.Sort();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(DataGridStrings.CheckBoxInRecordSelectors_MessageBox_Checked);
            foreach (string s in checkedNames)
                sb.AppendLine(s);

            sb.AppendLine();

            sb.AppendLine(DataGridStrings.CheckBoxInRecordSelectors_MessageBox_Unchecked);
            foreach (string s in uncheckedNames)
                sb.AppendLine(s);

            string msg = sb.ToString();
            string caption = DataGridStrings.CheckBoxInRecordSelectors_MessageBox_Caption;
            MessageBox.Show(msg, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #region Person [nested class]

        /// <summary>
        /// A simple data object that stores information about a person.
        /// </summary>
        public class Person
        {
            public static Person[] GetPeople()
            {
                return new Person[]
            {
                new Person("Jane", "Smith", 23),
                new Person("Mike", "Wheeler", 45),
                new Person("Ned", "Ableton", 67),
                new Person("Patrick", "McCort", 12),
                new Person("Ravi", "Patel", 28),
                new Person("Wallace", "Barrington", 42),
            };
            }

            public Person(string firstName, string lastName, int age)
            {
                this.FirstName = firstName;
                this.LastName = lastName;
                this.Age = age;
            }

            public int Age { get; private set; }
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
        }

        #endregion // Person [nested class]

        #region PersonViewModel [nested class]

        /// <summary>
        /// A presentation-friendly wrapper for the Person class, which has support for being 'checked.'
        /// </summary>
        public class PersonViewModel : INotifyPropertyChanged
        {
            readonly Person _person;
            bool _isChecked;

            public PersonViewModel(Person person)
            {
                _person = person;
            }

            public bool IsChecked
            {
                get { return _isChecked; }
                set
                {
                    if (value == _isChecked)
                        return;

                    _isChecked = value;

                    this.OnPropertyChanged("IsChecked");
                }
            }

            public int Age { get { return _person.Age; } }
            public string FirstName { get { return _person.FirstName; } }
            public string LastName { get { return _person.LastName; } }

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged(string prop)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }

            #endregion // INotifyPropertyChanged Members
        }

        #endregion // PersonViewModel [nested class]

        #region CommunityViewModel [nested class]

        /// <summary>
        /// A presentation-friendly class that contains a list of PersonViewModel objects
        /// and manages an aggregated check state for the group, via the get/set property
        /// called AllMembersAreChecked.
        /// </summary>
        public class CommunityViewModel : INotifyPropertyChanged
        {
            public CommunityViewModel(List<PersonViewModel> members)
            {
                this.Members = members;

                foreach (PersonViewModel member in members)
                    member.PropertyChanged += (sender, e) =>
                    {
                        // Raising PropertyChanged for the AllMembersAreChecked
                        // property causes the data binding system to query its
                        // getter for the new value.
                        if (e.PropertyName == "IsChecked")
                            this.OnPropertyChanged("AllMembersAreChecked");
                    };
            }

            public List<PersonViewModel> Members { get; private set; }

            public bool? AllMembersAreChecked
            {
                get
                {
                    // Determine if all members have the same 
                    // value for the IsChecked property.
                    bool? value = null;
                    for (int i = 0; i < this.Members.Count; ++i)
                    {
                        if (i == 0)
                        {
                            value = this.Members[0].IsChecked;
                        }
                        else if (value != this.Members[i].IsChecked)
                        {
                            value = null;
                            break;
                        }
                    }

                    return value;
                }
                set
                {
                    if (value == null)
                        return;

                    foreach (PersonViewModel member in this.Members)
                        member.IsChecked = value.Value;
                }
            }

            #region INotifyPropertyChanged Members

            public event PropertyChangedEventHandler PropertyChanged;

            void OnPropertyChanged(string prop)
            {
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }

            #endregion // INotifyPropertyChanged Members
        }

        #endregion // CommunityViewModel [nested class]
    }
}
