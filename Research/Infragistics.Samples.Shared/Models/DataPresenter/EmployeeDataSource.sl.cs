using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Infragistics.Samples.Shared.DataProviders;

namespace Infragistics.Samples.Shared.Models.DataPresenter
{
    /// <summary>
    /// Class which creates mock data for this sample
    /// </summary>
    public class EmployeeData : INotifyPropertyChanged
    {
        private ObservableCollection<Employee> employeeData = null;

        public EmployeeData()
        {
        }

        public ObservableCollection<Employee> EmployeeDataCollection
        {
            get
            {
                if (this.employeeData == null)
                    this.PopulateEmployeeDataCollection();

                return this.employeeData;
            }
        }

        #region Populate Employee Collection

        private void PopulateEmployeeDataCollection()
        {
            this.employeeData = new ObservableCollection<Employee>();

            Employee employee;

            employee = new Employee("John Smith",
                                                    new DateTime(1967, 6, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy01.jpg"),
                                                    "Human Resources",
                                                    52500.00M,
                                                    "(712) 555-9837",
                                                    "(214) 555-7222",
                                                    "7655 5th Avenue",
                                                    "Apt 3B",
                                                    "Teaneck",
                                                    "NJ",
                                                    "15772",
                                                    "john@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Mary Hernandez",
                                                    new DateTime(1975, 7, 23),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl01.jpg"),
                                                    "Sales",
                                                    65100.00M,
                                                    "(612) 555-1234",
                                                    "(612) 555-8775",
                                                    "776 Maple Avenue",
                                                    "",
                                                    "Lido",
                                                    "CA",
                                                    "99837",
                                                    "MaryH@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Barbara Bailey",
                                                    new DateTime(1996, 1, 15),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl02.jpg"),
                                                    "Admin",
                                                    35750.00M,
                                                    "(512) 555-3899",
                                                    "(512) 555-2221",
                                                    "12 Larkfield Court",
                                                    "Suite 206",
                                                    "Chicago",
                                                    "IL",
                                                    "45883",
                                                    "BB@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Margaret Jones",
                                                    new DateTime(1999, 11, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl03.jpg"),
                                                    "Admin",
                                                    37750.00M,
                                                    "(512) 555-7776",
                                                    "(512) 555-2351",
                                                    "144 Main Street",
                                                    "",
                                                    "Chicago",
                                                    "IL",
                                                    "45883",
                                                    "MargeJ@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Bobby Jones",
                                                    new DateTime(2003, 9, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy02.jpg"),
                                                    "Accounting",
                                                    53250.00M,
                                                    "(212) 555-1727",
                                                    "(212) 555-9938",
                                                    "3166 72nd Street",
                                                    "Apt 229",
                                                    "New York",
                                                    "NY",
                                                    "01992",
                                                    "BobbyJ@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Jonathan Barclay",
                                                    new DateTime(1990, 4, 15),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy03.jpg"),
                                                    "Board Of Directors",
                                                    123000.00M,
                                                    "(212) 555-1199",
                                                    "(212) 555-9911",
                                                    "219 Park Avenue",
                                                    "3rd Floor",
                                                    "New York",
                                                    "NY",
                                                    "02887",
                                                    "JonathanB@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Martha Beckinsale",
                                                    new DateTime(1989, 5, 25),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl04.jpg"),
                                                    "Sales",
                                                    99700.00M,
                                                    "(213) 555-8837",
                                                    "(213) 555-1009",
                                                    "12 Sepulveda Blvd",
                                                    "Suite 5",
                                                    "Los Angeles",
                                                    "CA",
                                                    "98772",
                                                    "MarthaB@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Kate Shannon",
                                                    new DateTime(1985, 12, 21),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl20.jpg"),
                                                    "Sales",
                                                    69000.00M,
                                                    "(213) 555-8837",
                                                    "(213) 555-1009",
                                                    "53 Dark Canyon Road",
                                                    "2nd floor",
                                                    "Santa Barbara",
                                                    "CA",
                                                    "99112",
                                                    "KateS@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Maggie Smithfield",
                                                    new DateTime(1983, 10, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl06.jpg"),
                                                    "Human Resources",
                                                    41000.00M,
                                                    "(533) 555-8837",
                                                    "(533) 555-1424",
                                                    "33 Palm Court",
                                                    "",
                                                    "Saint Augustine",
                                                    "Fl",
                                                    "34211",
                                                    "MaggieS@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Bradley Swinford",
                                                    new DateTime(1991, 6, 8),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy04.jpg"),
                                                    "Development",
                                                    85500.00M,
                                                    "(516) 555-2111",
                                                    "(516) 555-9887",
                                                    "355 Old Country Road",
                                                    "Suite 6",
                                                    "Dix Hills",
                                                    "NY",
                                                    "11677",
                                                    "BradS@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Derek Hardstone",
                                                    new DateTime(1996, 2, 28),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy05.jpg"),
                                                    "Testing",
                                                    50500.00M,
                                                    "(211) 555-4983",
                                                    "(211) 555-8275",
                                                    "1199 Silver Bluff Way",
                                                    "",
                                                    "Denver",
                                                    "CO",
                                                    "76625",
                                                    "DerekH@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Melissa Allman",
                                                    new DateTime(1969, 7, 18),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl07.jpg"),
                                                    "Sales",
                                                    73600.00M,
                                                    "(545) 555-2938",
                                                    "(545) 555-6645",
                                                    "2233 Red Rock Road",
                                                    "Suite 77",
                                                    "Austin",
                                                    "TX",
                                                    "65534",
                                                    "MelissaA@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Terence Starchedpants",
                                                    new DateTime(1983, 8, 11),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy06.jpg"),
                                                    "Accounting",
                                                    53700.00M,
                                                    "(301) 555-1000",
                                                    "(301) 555-0990",
                                                    "2773 Easton Way",
                                                    "Apt 8H",
                                                    "Seattle",
                                                    "WA",
                                                    "98006",
                                                    "TerenceS@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Ashley Fabray",
                                                    new DateTime(1990, 3, 8),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl08.jpg"),
                                                    "Human Resources",
                                                    43000.00M,
                                                    "(515) 555-3321",
                                                    "(515) 555-1123",
                                                    "93888 East Main Street",
                                                    "2nd Floor",
                                                    "Racine",
                                                    "WI",
                                                    "50082",
                                                    "AshleyF@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Randall Carruthers",
                                                    new DateTime(2004, 4, 9),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy07.jpg"),
                                                    "Sales",
                                                    89700.00M,
                                                    "(201) 555-1100",
                                                    "(201) 555-8870",
                                                    "37 Fairfield Court",
                                                    "Apt 1",
                                                    "Holmdel",
                                                    "NJ",
                                                    "19882",
                                                    "RandallC@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Jake Swanson",
                                                    new DateTime(1967, 6, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy08.jpg"),
                                                    "Human Resources",
                                                    52500.00M,
                                                    "(712) 555-9837",
                                                    "(214) 555-7222",
                                                    "7655 5th Avenue",
                                                    "Apt 3B",
                                                    "Teaneck",
                                                    "NJ",
                                                    "15772",
                                                    "jakeswan@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Melissa Harvay",
                                                    new DateTime(1975, 7, 23),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl09.jpg"),
                                                    "Sales",
                                                    65100.00M,
                                                    "(612) 555-1234",
                                                    "(612) 555-8775",
                                                    "776 Maple Avenue",
                                                    "",
                                                    "Lido",
                                                    "CA",
                                                    "99837",
                                                    "MelissaH@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Elizabeth Baker",
                                                    new DateTime(1996, 1, 15),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl10.jpg"),
                                                    "Admin",
                                                    35750.00M,
                                                    "(512) 555-3899",
                                                    "(512) 555-2221",
                                                    "12 Larkfield Court",
                                                    "Suite 206",
                                                    "Chicago",
                                                    "IL",
                                                    "45883",
                                                    "LizBaker@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Alice Simpson",
                                                    new DateTime(1999, 11, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl11.jpg"),
                                                    "Admin",
                                                    37750.00M,
                                                    "(512) 555-7776",
                                                    "(512) 555-2351",
                                                    "144 Main Street",
                                                    "",
                                                    "Chicago",
                                                    "IL",
                                                    "45883",
                                                    "AliceS@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Fred Thompson",
                                                    new DateTime(2003, 9, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy09.jpg"),
                                                    "Accounting",
                                                    53250.00M,
                                                    "(212) 555-1727",
                                                    "(212) 555-9938",
                                                    "3166 72nd Street",
                                                    "Apt 229",
                                                    "New York",
                                                    "NY",
                                                    "01992",
                                                    "FreddieT@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Pete Stevens",
                                                    new DateTime(1990, 4, 15),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy10.jpg"),
                                                    "Board Of Directors",
                                                    123000.00M,
                                                    "(212) 555-1199",
                                                    "(212) 555-9911",
                                                    "219 Park Avenue",
                                                    "3rd Floor",
                                                    "New York",
                                                    "NY",
                                                    "02887",
                                                    "PeteMoss@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Jane Meadows",
                                                    new DateTime(1989, 5, 25),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl12.jpg"),
                                                    "Sales",
                                                    99700.00M,
                                                    "(213) 555-8837",
                                                    "(213) 555-1009",
                                                    "12 Sepulveda Blvd",
                                                    "Suite 5",
                                                    "Los Angeles",
                                                    "CA",
                                                    "98772",
                                                    "AudreysSister@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Sally Dougherty",
                                                    new DateTime(1985, 12, 21),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl13.jpg"),
                                                    "Sales",
                                                    69000.00M,
                                                    "(213) 555-8837",
                                                    "(213) 555-1009",
                                                    "53 Dark Canyon Road",
                                                    "2nd floor",
                                                    "Santa Barbara",
                                                    "CA",
                                                    "99112",
                                                    "SallyDoh@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Susan Springfield",
                                                    new DateTime(1983, 10, 1),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl14.jpg"),
                                                    "Human Resources",
                                                    41000.00M,
                                                    "(533) 555-8837",
                                                    "(533) 555-1424",
                                                    "33 Palm Court",
                                                    "",
                                                    "Saint Augustine",
                                                    "Fl",
                                                    "34211",
                                                    "SusieS@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Joseph Breckinrich",
                                                    new DateTime(1991, 6, 8),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy11.jpg"),
                                                    "Development",
                                                    85500.00M,
                                                    "(516) 555-2111",
                                                    "(516) 555-9887",
                                                    "355 Old Country Road",
                                                    "Suite 6",
                                                    "Dix Hills",
                                                    "NY",
                                                    "11677",
                                                    "JoeBreck@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("David Cooper",
                                                    new DateTime(1996, 2, 28),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy12.jpg"),
                                                    "Testing",
                                                    50500.00M,
                                                    "(211) 555-4983",
                                                    "(211) 555-8275",
                                                    "1199 Silver Bluff Way",
                                                    "",
                                                    "Denver",
                                                    "CO",
                                                    "76625",
                                                    "DavidCoop@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Janine Falstaff",
                                                    new DateTime(1969, 7, 18),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl15.jpg"),
                                                    "Sales",
                                                    73600.00M,
                                                    "(545) 555-2938",
                                                    "(545) 555-6645",
                                                    "2233 Red Rock Road",
                                                    "Suite 77",
                                                    "Austin",
                                                    "TX",
                                                    "65534",
                                                    "JFalstaff@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Toby Gillis",
                                                    new DateTime(1983, 8, 11),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy13.jpg"),
                                                    "Accounting",
                                                    53700.00M,
                                                    "(301) 555-1000",
                                                    "(301) 555-0990",
                                                    "2773 Easton Way",
                                                    "Apt 8H",
                                                    "Seattle",
                                                    "WA",
                                                    "98006",
                                                    "TobyNotDobie@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Margaret Douglas",
                                                    new DateTime(1990, 3, 8),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Girl16.jpg"),
                                                    "Human Resources",
                                                    43000.00M,
                                                    "(515) 555-3321",
                                                    "(515) 555-1123",
                                                    "93888 East Main Street",
                                                    "2nd Floor",
                                                    "Racine",
                                                    "WI",
                                                    "50082",
                                                    "MargieD@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

            employee = new Employee("Soupy Sales",
                                                    new DateTime(2004, 4, 9),
                                                    ImageFilePathProvider.GetImageLocalUri("people/Guy14.jpg"),
                                                    "Sales",
                                                    89700.00M,
                                                    "(201) 555-1100",
                                                    "(201) 555-8870",
                                                    "37 Fairfield Court",
                                                    "Apt 1",
                                                    "Holmdel",
                                                    "NJ",
                                                    "19882",
                                                    "SoupyS@hotmail.com");
            this.EmployeeDataCollection.Add(employee);

        }

        #endregion

        #region INotifyPropertyChanged Members

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}