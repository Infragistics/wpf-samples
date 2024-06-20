using IGPropertyGrid.Resources;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace IGPropertyGrid.Samples.DataModel
{
    public class OrganizationEN
    {
        private EmployeesCollectionEN employees = new EmployeesCollectionEN();

        public OrganizationEN()
        {
            
            this.employees.Add(
                new EmployeeEN()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Age = 42,
                    Gender = Genders.M,
                    Department = Departments.Engineering,
                    Company = "Infragistics"
                });
            this.employees.Add(
                new EmployeeEN()
                {
                    FirstName = PropertyGridStrings.person2FirstName,
                    LastName = PropertyGridStrings.person2LastName,
                    Age = 24,
                    Gender = Genders.F,
                    Department = Departments.Accounting,
                    Company = "Infragistics"
                });
        }

        [TypeConverter(typeof(EmployeeCollectionCustomizer))]
        public EmployeesCollectionEN Employees
        {
            get
            {
                return this.employees;
            }
        }
    }

    public class OrganizationJA
    {
        private EmployeesCollectionJA employees = new EmployeesCollectionJA();


        public OrganizationJA()
        {
            this.employees.Add(
                new EmployeeJA()
                {
                    FirstName = PropertyGridStrings.person1FirstName,
                    LastName = PropertyGridStrings.person1LastName,
                    Age = 42,
                    Gender = Genders.M,
                    Department = Departments.Engineering,
                    Company = "Infragistics"
                });
            this.employees.Add(
                new EmployeeJA()
                {
                    FirstName = PropertyGridStrings.person2FirstName,
                    LastName = PropertyGridStrings.person2LastName,
                    Age = 24,
                    Gender = Genders.F,
                    Department = Departments.Accounting,
                    Company = "Infragistics"
                });
        }


        [TypeConverter(typeof(EmployeeCollectionCustomizer))]
        public EmployeesCollectionJA Employees
        {
            get
            {
                return this.employees;
            }
        }


    }
}
