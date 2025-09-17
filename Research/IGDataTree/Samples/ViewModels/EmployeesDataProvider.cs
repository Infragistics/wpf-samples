using System;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace IGDataTree.Samples.ViewModels
{
    public class EmployeesDataProvider : ObservableModel
    {
        public EmployeesDataProvider()
        {
            DownloadDataSource();
        }

        private ObservableCollection<Employee> _employees = null;
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return this._employees;
            }
            set
            {
                if (this._employees != value)
                {
                    this._employees = value;
                    this.OnPropertyChanged("Employees");
                }
            }
        }

        private void DownloadDataSource()
        {
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Employees.xml");
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            var doc = e.Result;
            Employees = this.GetEmployees(doc.Root);
        }

        private ObservableCollection<Employee> GetEmployees(XElement parent)
        {
            string imagePath = ImageFilePathProvider.GetImageLocalPath() + "People/";

            var data = (from em in parent.Elements("Employees")
                    select new Employee
                    {
                        Id = em.Element("EmpID").GetString(),
                        Name = em.Element("EmpName").GetString(),
                        JobTitle = em.Element("JobTitle").GetString(),
                        EmployeeSince = em.Element("EmployeeSince").GetString(),
                        ManagerName = parent.Element("EmpName").GetString(),
                        ImagePath = imagePath + em.Element("ImagePath").GetString(),
                        DirectReports = this.GetEmployees(em),
                        HasHealthInsurance = int.Parse(em.Element("EmployeeSince").GetString()) % 2 == 0
                    }).ToList<Employee>();

            return new ObservableCollection<Employee>(data);
        }
    }
}
