using System.Linq;
using System.Windows;
using System.Xml.Linq;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Editing
{
    public partial class Activation : SampleContainer
    {

        public Activation()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Patients.xml");
        }

        void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("PatientAdmittance")
                              select new PatientAdmittance
                              {
                                  AdmittanceId = d.Attribute("AdmittanceId").GetString(),
                                  FirstName = d.Attribute("FirstName").GetString(),
                                  LastName = d.Attribute("LastName").GetString(),
                                  DOB = d.Attribute("DOB").GetDateTime(),
                                  Gender = d.Attribute("Gender").GetString(),
                                  AdmittanceDate = d.Attribute("AdmittanceDate").GetDateTime(),
                                  Location = d.Attribute("Location").GetString(),
                                  Severity = d.Attribute("Severity").GetString()
                              });

            this.dataGrid.ItemsSource = dataSource.ToList();

            if (this.dataGrid.Rows.Count > 0)
            {
                this.dataGrid.ActiveCell = this.dataGrid.Rows[0].Cells["FirstName"];
                this.DisplayActiveCellValue();
            }
        }

        private void dataGrid_ActiveCellChanged(object sender, System.EventArgs e)
        {
            this.DisplayActiveCellValue();
        }

        private void DisplayActiveCellValue()
        {
            if (this.dataGrid.ActiveCell != null)
            {
                this.ActiveCellValue.Text = string.Format(System.Globalization.CultureInfo.CurrentCulture, "{0:d}", this.dataGrid.ActiveCell.Value);
            }
        }

    }
}
