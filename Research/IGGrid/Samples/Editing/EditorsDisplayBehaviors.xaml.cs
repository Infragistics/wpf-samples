using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.Medical;
using Infragistics.Samples.Shared.Resources;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace IGGrid.Samples.Editing
{
    public partial class EditorsDisplayBehaviors : SampleContainer
    {
        public EditorsDisplayBehaviors()
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
            var dataProvider = new XmlDataProvider();
            dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            dataProvider.GetXmlDataAsync("Patients.xml");
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

            LB_EditorDisplayBehaviors.ItemsSource = EnumValuesProvider.GetEnumValues<EditorDisplayBehaviors>();
            LB_EditorDisplayBehaviors.SelectedIndex = 1;

            var comboColumn = this.dataGrid.Columns.OfType<ComboBoxColumn>().First();
            comboColumn.ItemsSource = SeverityLevelsCollection();
        }

        private ObservableCollection<SeverityLevel> SeverityLevelsCollection()
        {
            var collection = new ObservableCollection<SeverityLevel>();

            collection.Add(new SeverityLevel(CommonStrings.XG_Severity_Low));
            collection.Add(new SeverityLevel(CommonStrings.XG_Severity_Medium));
            collection.Add(new SeverityLevel(CommonStrings.XG_Severity_High));

            return collection;
        }

        private void LB_EditorDisplayBehaviors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            CustomDisplayEditableColumn dobEditableColumn = dataGrid.Columns.DataColumns["DOB"] as CustomDisplayEditableColumn;
            CustomDisplayEditableColumn adEditableColumn = dataGrid.Columns.DataColumns["AdmittanceDate"] as CustomDisplayEditableColumn;
            CustomDisplayEditableColumn severityEditableColumn = dataGrid.Columns.DataColumns["Severity"] as CustomDisplayEditableColumn;

            if (e.AddedItems.Count > 0)
            {
                var selectedOption = (EditorDisplayBehaviors)e.AddedItems[0];
                dobEditableColumn.EditorDisplayBehavior = selectedOption;
                adEditableColumn.EditorDisplayBehavior = selectedOption;
                severityEditableColumn.EditorDisplayBehavior = selectedOption;
            }
        }
    }  
}
