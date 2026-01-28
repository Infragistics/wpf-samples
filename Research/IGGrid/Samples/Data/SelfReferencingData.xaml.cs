using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Data
{
    public partial class SelfReferencingData : SampleContainer
    {

        public SelfReferencingData()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Employees.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error != null) return;

            XDocument doc = e.Result;
            var result = this.GetEmployees(doc.Root);
            this.dataGrid.ItemsSource = result;

            this.ExpandRows(this.dataGrid.Rows);
        }

        private IList<Employee> GetEmployees(XElement parent)
        {
            return (from em in parent.Elements("Employees")
                    select new Employee
                    {
                        Id = em.Element("EmpID").GetString(),
                        Name = em.Element("EmpName").GetString(),
                        JobTitle = em.Element("JobTitle").GetString(),
                        EmployeeSince = em.Element("EmployeeSince").GetString(),
                        ManagerName = parent.Element("EmpName").GetString(),
                        DirectReports = this.GetEmployees(em)
                    }).ToList<Employee>();
        }

        private void ExpandRows(RowCollection rows)
        {
            foreach (Row row in rows)
            {
                row.IsExpanded = true;
                if (row.HasChildren)
                {
                    // Grab this row's RowCollection, then recursively parse this collection //
                    this.ExpandRows(row.ChildBands[0].Rows);
                }
            }
        }
    }
}
