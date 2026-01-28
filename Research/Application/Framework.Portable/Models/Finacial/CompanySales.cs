using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Infragistics.Framework
{
    public class CompanySales : ObservableCollection<Sale>
    {
        public CompanySales()
        {
            var sales = CompanyGenerator.GetSales();
            foreach (var sale in sales)
            {
                this.Add(sale);
            }
        }
    }

    public class CompanyTeam : ObservableCollection<Employee>
    {
        public CompanyTeam() : this(50)
        {
        }
        public CompanyTeam(int count)
        {
            var list = CompanyGenerator.GetEmployees(count);
          
            for (int i = 0; i < list.Count; i++)
            { 
                list[i].Index = i;
                this.Add(list[i]);
            }
        }
    }
    public class SocialNetwork : ObservableCollection<Person>
    {
        public SocialNetwork() : this(100)
        {
        }
        public SocialNetwork(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var person = CompanyGenerator.GetRandomPerson();
                person.Index = i;
                this.Add(person); 
            }
        }
    }
    public class CompanyGenerator
    {
        private static readonly Random Random = new Random();

        private static string[] _MaleNames;
        protected static string[] MaleNames
        {
            get
            {
                if (_MaleNames == null)
                {
                    _MaleNames = new string[] {
                        "Kyle",
                        "Oscar",
                        "Ralph",
                        "Torrey",
                        "Bill",
                        "Frank",
                        "Howard",
                        "Jack",
                        "Larry",
                        "Pete",
                        "Steve",
                        "Vince",
                        "Mark",
                        "Alex",
                        "Max",
                        "Brian",
                        "Chris",
                        "Andrew",
                        "Martin",
                        "Mike",
                        "Steven",
                        "Glenn",
                        "Bruce",
                    };
                }
                return _MaleNames;
            }
        }

        private static string[] _GrilNames;
        protected static string[] GrilNames
        {
            get
            {
                if (_GrilNames == null)
                {
                    _GrilNames = new string[] {
                        "Gina",
                        "Irene",
                        "Katie",
                        "Daniel",
                        "Brenda",
                        "Casey",
                        "Fiona",
                        "Holly",
                        "Kate",
                        "Liz",
                        "Pamela",
                        "Nelly",
                        "Marisa",
                        "Monica",
                        "Anna",
                        "Jessica",
                        "Sofia",
                        "Isabela",
                        "Margo",
                        "Jane",
                        "Audrey",
                        "Sally",
                        "Melanie",
                        "Greta",
                        "Aurora",
                        "Sally",
                    };
                }
                return _GrilNames;
            }
        }

        private static string[] _LastNames;
        protected static string[] LastNames
        {
            get
            {
                if (_LastNames == null)
                {
                    _LastNames = new string[] {
                        "Adams",
                        "Crowley",
                        "Ellis",
                        "Martinez",
                        "Irvine",
                        "Maxwell",
                        "Clark",
                        "Owens",
                        "Rooney",
                        "Lincoln",
                        "Thomas",
                        "Spacey",
                        "Betts",
                        "King",
                        "Newton",
                        "Fitzgerald",
                        "Holmes",
                        "Jefferson",
                        "Landry",
                        "Newberry",
                        "Perez",
                        "Spencer",
                        "Starr",
                        "Carter",
                        "Edwards",
                        "Stark",
                        "Johnson",
                        "Fitz",
                        "Chief",
                        "Blanc",
                        "Perry",
                        "Stone",
                        "Williams",
                        "Lane",
                        "Jobs",
                        "Adama",
                        "Power",
                        "Tesla",
                        "Newman",
                        "Morgan",
                        "Polanski",
                        "Phoenix",
                        "Electra",
                        "Bond",
                        "Gretzky",
                        "Costa",
                        "Talon",
                        "Stark",
                        "Connery",
                        "Connery",
                        "Forrest",
                        "Hepburn",
                        "Nowak",
                    };
                }
                return _LastNames;
            }
        }

        private static string[] _Genders;
        protected static string[] Genders
        {
            get
            {
                if (_Genders == null)
                {
                    _Genders = new string[] {
                        "male",
                        "female",
                        "female",
                        "female",
                        "male",
                        "male",
                        "male",
                        "male",
                        "male",
                        "male",
                        "male",
                        "male",
                        "female",
                        "female",
                        "female",
                        "male",
                        "male",
                        "male",
                        "female",
                        "female",
                        "female",
                        "male",
                        "male",
                        "male",
                        "male"
                    };
                }
                return _Genders;
            }
        }

        private static string[] _countries;
        protected static string[] Countries
        {
            get
            {
                if (_countries == null)
                {
                    _countries = new string[] {
                        "Australia",
                        "Canada",
                        "Egypt",
                        "Greece",
                        "Italy",
                        "Kenya",
                        "Mexico",
                        "Oman",
                        "Qatar",
                        "Sweden",
                        "Uruguay",
                        "Yemen",
                        "Bulgaria",
                        "Denmark",
                        "France",
                        "Hungary",
                        "Japan",
                        "Germany",
                        "Netherlands",
                        "Portugal",
                        "Russia",
                        "Turkey",
                        "Venezuela",
                        "Poland"
                    };
                }
                return _countries;
            }
        }

        private static string[] _cities;
        protected static string[] Cities
        {
            get
            {
                if (_cities == null)
                {
                    _cities = new string[] {
                        "Sydney",
                        "Cairo",
                        "Hong Kong",
                        "Rome",
                        "Paris",
                        "Tokyo",
                        "Seattle",
                        "Berlin",
                        "New York",
                        "London",
                        "Warsaw"
                    };
                }
                return _cities;
            }
        }

        private static string[] _products;
        protected static string[] Products
        {
            get
            {
                if (_products == null)
                {
                    _products = new string[] {
                        "Accessories",
                        "Bikes",
                        "Clothing",
                        "Components",
                        "Cars",
                    };
                }
                return _products;
            }
        }

        public static List<Employee> GetEmployees(int peopleCount = 50)
        {
            var sellers = new List<Employee>();
            for (var i = 0; i < peopleCount; i++)
            {
                var seller = GetRandomEmployee();
                seller.ID = i;
                sellers.Add(seller);
            }
            return sellers;
        }

        public static List<Sale> GetSales(int salesCount = 500)
        {
            var sales = new List<Sale>();
            for (var i = 0; i < salesCount; i++)
            {
                var sale = new Sale
                {
                    Quarter = "Q" + i,
                    Value = GetRandomPrice(),
                    Date = GetRandomDate(),
                    Product = GetRandomProduct(),
                    Units = GetRandomNumUnits(),
                    Employee = GetRandomEmployee()
                };
                sales.Add(sale);
            }
            return sales;
        }

        #region Get Random Objects
        public static Person GetRandomPerson()
        {
            var person = new Person
            {
                City = GetRandomCity(),
                Gender = GetRandomGender(),
                Birthday = GetRandomBirthday(),
                LastName = GetRandomLastName(),
            };
            person.Firends = Random.Next(50, 1500);
            person.Name = GetRandomFirstName(person.Gender); 

            return person;
        }
        public static Employee GetRandomEmployee()
        {
            var person = new Employee
            {
                City = GetRandomCity(),
                Gender = GetRandomGender(),
                Birthday = GetRandomBirthday(),
                LastName = GetRandomLastName(),
            };
            var photo = GetRandomPhoto(person.Gender); 
            person.Name = GetRandomFirstName(person.Gender);
            person.Photo = "People/" + photo;
            person.PhotoEmbedded = "Samples.Browser.Images.People." + photo;

            person.Sales = GetRandomSales();
            person.KPI = GetRandomPerformance();
            person.Salary = GetRandomSalary();
            person.HireDate = GetRandomHireDate();
            
            person.GenderImage  = "genders_" + person.Gender + ".png";
            person.GenderEmbedded = "Samples.Browser.Images." + person.GenderImage;

            person.Promotion = GetPromotionDate(person);
             
            for (int j = 1; j <= 5; j++)
            {
                person.SalesTrend.Add(new SalesTrend { Value = Random.Next(-10, 10), Label = "L" + j }); 
            }
            return person;
        }

        public static string GetRandomFirstName(string gender)
        {
            var names = gender == "female" ? GrilNames : MaleNames;
            return names[Random.Next(names.Length)];
        }
        public static string GetRandomLastName()
        {
            return LastNames[Random.Next(LastNames.Length)];
        }
        public static DateTime GetRandomBirthday()
        {
            var year = Random.Next(1970, 1990);
            var month = Random.Next(1, 12);
            var day = Random.Next(1, 28);
            return new DateTime(year, month, day);
        }
        public static DateTime GetRandomHireDate()
        {
            var year = Random.Next(2010, 2015);
            var month = Random.Next(1, 12);
            var day = Random.Next(1, 28);
            return new DateTime(year, month, day);
        }
        public static DateTime GetPromotionDate(Employee employee)
        {
            var year = Random.Next(employee.HireDate.Year, DateTime.Now.Year);
            var month = Random.Next(1, 12);
            var day = Random.Next(1, 28);
            return new DateTime(year, month, day);
        }
        public static DateTime GetRandomDate()
        {
            int year = Random.Next(DateTime.Today.Year - 5, DateTime.Today.Year);
            int month = Random.Next(1, 12);
            int day = Random.Next(1, 28);
            return new DateTime(year, month, day);
        }
        public static string GetRandomPhoto(string gender)
        {
            int id = Random.Next(1, 24);
            var photo = gender;
            if (id < 10) photo += "0";
            photo += id + ".png";

            return photo;
        }
        public static string GetEmbeddedPhoto(string gender)
        {
            int id = Random.Next(1, 24);
            var photo = gender;
            if (id < 10) photo += "0";
            photo += id + ".png";
            photo = "Samples.Browser.Images.People." + photo;

            return photo;
        }
        public static string GetRandomPhone()
        {
            var digits1 = Random.Next(200, 999);
            var digits2 = Random.Next(100, 999);
            var digits3 = Random.Next(1000, 9999); 
            return digits1 + "-" + digits2 + "-" + digits3;
        }
        public static string GetRandomGender()
        {
            var gender = Random.NextDouble();
            return gender < .55 ? "female" : "male";
        }
        public static string GetRandomCity()
        {
            return Cities[Random.Next(Cities.Length)];
        }
        private static int GetRandomNumUnits()
        {
            return Random.Next(1, 500);
        }
        public static int GetRandomSales()
        {
            return Random.Next(100, 600);
        }
        public static int GetRandomPerformance()
        {
            var kpi = Random.NextDouble();
            return kpi < .6 ? Random.Next(70, 90) : Random.Next(40, 70);
        }
        public static int GetRandomSalary()
        {
            return Random.Next(65, 500) * 1000;
        }
        private static Product GetRandomProduct()
        {
            return new Product
            {
                Name = GetRandomProductName(),
                UnitPrice = GetRandomPrice()
            };
        }
        private static double GetRandomPrice()
        {
            return Random.NextDouble() * 100;
        }
        private static string GetRandomProductName()
        {
            return Products[Random.Next(Products.Length)];
        }
        #endregion 
    }

    public class Sale
    {
        public Sale()
        {
            this.Product = new Product();
            this.Employee = new Employee();
        }
        public Product Product { get; set; }
        public Employee Employee { get; set; }
        public DateTime Date { get; set; }

        public string Quarter { get; set; }
        public string City
        {
            get { return this.Employee.City; }
            set { this.Employee.City = value; }
        }
        public int Units { get; set; }
        public double Value
        {
            get { return this.UnitPrice * this.Units; }
            set { this.Product.UnitPrice = value / this.Units; }
        }
        public double UnitPrice
        {
            get { return this.Product.UnitPrice; }
            set { this.Product.UnitPrice = value; }
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public int Age { get { return Calendar.GetAge(Birthday); } }
        public int Firends { get; set; }
        public int Index { get; set; }
    }

    public class Employee : Person
    {
        public Employee()
        {
            SalesTrend = new List<SalesTrend>();
        }
        public int ID { get; set; }
        public int Sales { get; set; }
        public string Photo { get; set; }
        public string PhotoEmbedded { get; set; }
        // Key Performance Indicator
        public int KPI { get; set; }
        public int Salary { get; set; }

        public DateTime HireDate { get; set; }
        public DateTime Promotion { get; set; }

        /// <summary>
        /// Gets or sets name of resource image local to platform app
        /// </summary>
        public string GenderImage { get; set; }
        /// <summary>
        /// Gets or sets name of embedded image in cross-platform assembly
        /// </summary>
        public string GenderEmbedded { get; set; }
        public List<SalesTrend> SalesTrend { get; internal set; }

        public override string ToString()
        {
            return Name + " " + Salary + "" + Photo;
        }
        protected static Random Random = new Random();
    }

    public class SalesTrend
    {
        public string Label { get; set; }
        public double Value { get; set; }
    }

    public static class Calendar {
        
        public static int GetAge(DateTime Birthday)
        {
            return (int)(DateTime.Now.Subtract(Birthday).TotalDays / 365);
        }
    }
    public class Product
    {
        public Product() { }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public int Sales { get; set; }

    }
}
