using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;

namespace IGOrgChart.Samples.Data
{
    public partial class DataBinding : SampleContainer
    {
        public DataBinding()
        {
            InitializeComponent();

            this.Loaded += DataBinding_Loaded;
        }

        private SampleModel SampleModel = new SampleModel();

        void DataBinding_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            //System.Diagnostics.Debugger.Break();
            XmlDataProvider dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Employees.xml");
        }
        private void OnGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                XDocument document = e.Result;
                this.SampleModel.Employees = new ObservableCollection<Employee>(GetEmployees(document.Root));
                this.OrgChart.DataContext = this.SampleModel;
            }
        }

        // Parses the XML data.
        private IEnumerable<Employee> GetEmployees(XElement parent)
        {
            return (from employee in parent.Elements("Employees")
                    select new Employee
                    {
                        FullName = employee.Element("EmpName").Value,
                        JobTitle = employee.Element("JobTitle").Value,
                        ImagePath = ImageFilePathProvider.GetImageLocalPath("People\\" + employee.Element("ImagePath").Value),
                        Subordinates = new ObservableCollection<Employee>(GetEmployees(employee))
                    });
        }
    }

    public class SampleModel : ObservableModel
    {
        // The Employees.
        public ObservableCollection<Employee> Employees { get; set; }
    }
}
