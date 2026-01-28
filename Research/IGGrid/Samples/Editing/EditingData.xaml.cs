using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Editing
{
    public partial class EditingData : SampleContainer
    {

        public EditingData()
        {
            InitializeComponent();
            Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.editingTypeCombo.ItemsSource = EnumValuesProvider.GetEnumValues<EditingType>();
            this.editingTypeCombo.SelectedIndex = (int)this.dataGrid.EditingSettings.AllowEditing;

            this.rowHoverCombo.ItemsSource = EnumValuesProvider.GetEnumValues<RowHoverType>();
            this.rowHoverCombo.SelectedIndex = (int)this.dataGrid.RowHover;

            this.mouseActionTypeCombo.ItemsSource = EnumValuesProvider.GetEnumValues<MouseEditingAction>();
            this.mouseActionTypeCombo.SelectedIndex = (int)this.dataGrid.EditingSettings.IsMouseActionEditingEnabled;

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
        }

        private void editingTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (EditingType)e.AddedItems[0];
            this.dataGrid.EditingSettings.AllowEditing = selectedItem;

            // Toggle IsEnabled State Based on Selected Allow Editing Value
            if (this.dataGrid.EditingSettings.AllowEditing == EditingType.None)
            {
                this.EnableEnterKeyEditing.IsEnabled = this.EnableF2Editing.IsEnabled = this.EnableOnCellActiveEditing.IsEnabled = this.mouseActionTypeCombo.IsEnabled = false;
            }
            else
            {
                this.EnableEnterKeyEditing.IsEnabled = this.EnableF2Editing.IsEnabled = this.EnableOnCellActiveEditing.IsEnabled = this.mouseActionTypeCombo.IsEnabled = true;
            }
        }

        private void rowHoverCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (RowHoverType)e.AddedItems[0];
            // The "RowHover" property specify whether a single cell or the whole row will go in edit mode when hovered
            // this works only if you set the "AllowEditing" property of the EditingSettings to "Hover".
            // Look at the previous method ("editingTypeCombo_SelectionChanged") to see how to set the "AllowEditing" property.
            this.dataGrid.RowHover = selectedItem;
        }

        private void mouseActionTypeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (MouseEditingAction)e.AddedItems[0];
            this.dataGrid.EditingSettings.IsMouseActionEditingEnabled = selectedItem;
        }

        private void EnableF2Editing_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.EditingSettings.IsF2EditingEnabled = (bool)this.EnableF2Editing.IsChecked;
        }

        private void EnableEnterKeyEditing_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.EditingSettings.IsEnterKeyEditingEnabled = (bool)this.EnableEnterKeyEditing.IsChecked;
        }

        private void EnableOnCellActiveEditing_Click(object sender, RoutedEventArgs e)
        {
            this.dataGrid.EditingSettings.IsOnCellActiveEditingEnabled = (bool)this.EnableOnCellActiveEditing.IsChecked;
        }
    }
}
