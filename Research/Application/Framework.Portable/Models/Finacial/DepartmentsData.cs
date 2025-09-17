using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq; 

namespace Infragistics.Framework 
{ 
    public class DepartmentHierarchy : DepartmentViewModel
    {
        public DepartmentHierarchy()
        {
            Departments = new ObservableCollection<Department>();

            var market = new Department() { Name = "Marketing", Budget = 50 };
            market.SubDepartments.Add(new Department() { Name = "Strategic", Budget = 25 });
            market.SubDepartments.Add(new Department() { Name = "Operational", Budget = 25 });

            var it = new Department() { Name = "IT", Budget = 30 };
            it.SubDepartments.Add(new Department() { Name = "Infrastructure", Budget = 22 });
            it.SubDepartments.Add(new Department() { Name = "Support", Budget = 8 });

            var support = new Department() { Name = "Support", Budget = 60 };
            support.SubDepartments.Add(new Department() { Name = "Chat Support", Budget = 20 });
            support.SubDepartments.Add(new Department() { Name = "Mail Support", Budget = 20 });
            support.SubDepartments.Add(new Department() { Name = "Phone Support", Budget = 20 });

            var dev = new Department() { Name = "Development", Budget = 75 };
            dev.SubDepartments.Add(new Department() { Name = "Research", Budget = 50 });
            dev.SubDepartments.Add(new Department() { Name = "Quality Assurance", Budget = 25 });

            Departments.Add(market);
            Departments.Add(it);
            Departments.Add(support);
            Departments.Add(dev);
        }
    }

    public class DepartmentViewModel
    {
        public ObservableCollection<Department> Departments { get; set; }

        string[] depts = "Marketing;IT;Development;Customer Support;Administration;Sales".Split(';');

        Random r = new Random();

        public DepartmentViewModel()
        {
            Departments = new ObservableCollection<Department>();

            for (int i = 0; i < depts.Length; i++)
            {
                Departments.Add(new Department
                {
                    Name = depts[i],
                    Budget = r.Next(25, 30)
                });
            }
        }
    }

    public class DepartmentsData : List<Department>
    {
        internal static Random Random = new Random();

        public DepartmentsData()
        {
            var labels = new[] { "IT", "Marketing", "Management", "Sales", "Development", "Support" };
             
            for (var i = 0; i < labels.Length; i++)
            {
                var item = new Department();
                item.Spending = (Random.NextDouble() * (100 - 20)) + 20;
                item.Budget = (Random.NextDouble() * (100 - 40)) + 40;
                item.Name = labels[i];
                this.Add(item);
            }
        } 
    }
     
    public class Department : ObservableModel
    { 
        /// <summary> Gets or sets Balance </summary>
        public double Balance { get { return Budget - Spending; } }

        private double _Spending;
        /// <summary> Gets or sets Spending </summary>
        public double Spending
        {
            get { return _Spending; }
            set { if (_Spending == value) return; _Spending = value; OnPropertyChanged("Spending"); }
        }
          
        private double _Budget;
        /// <summary> Gets or sets Budget </summary>
        public double Budget
        {
            get { return _Budget; }
            set { if (_Budget == value) return; _Budget = value; OnPropertyChanged("Budget"); }
        } 

        private string _Name;
        /// <summary> Gets or sets Name </summary>
        public string Name
        {
            get { return _Name; }
            set { if (_Name == value) return; _Name = value; OnPropertyChanged("Name"); }
        } 
          
        private ObservableCollection<Department> _SubDepartments;
        /// <summary> Gets or sets SubDepartments </summary>
        public ObservableCollection<Department> SubDepartments
        {
            get { return _SubDepartments; }
            set { if (_SubDepartments == value) return; _SubDepartments = value; OnPropertyChanged("SubDepartments"); }
        }
         
        public Department()
        {
            SubDepartments = new ObservableCollection<Department>();
        }
    }
}
