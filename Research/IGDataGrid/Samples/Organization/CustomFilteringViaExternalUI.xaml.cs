using System;
using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CustomFilteringViaExternalUI.xaml
    /// </summary>
    public partial class CustomFilteringViaExternalUI : SampleContainer
    {
        public CustomFilteringViaExternalUI()
        {
            InitializeComponent();

            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);

            this.XamDataGrid1.DataSource = dt.DefaultView;

            // Create a list of fields whose data the user will be allowed to search in 
            // the filtering UI.
            // 
            string[] searchFieldNames = new string[]
            {
                "CustomerID",
                "ContactName",
                "Country",
                "City",
                "Address",
                "PostalCode",
                "CompanyName",
                "Phone",
                "Fax"
            };

            // Create a list of operators the user will be allowed to use.
            // 
            Infragistics.Windows.Controls.ComparisonOperator[] searchOperators = new Infragistics.Windows.Controls.ComparisonOperator[]
            {
                Infragistics.Windows.Controls.ComparisonOperator.StartsWith,
                Infragistics.Windows.Controls.ComparisonOperator.Contains,
                Infragistics.Windows.Controls.ComparisonOperator.Like, // Wildcard 
                Infragistics.Windows.Controls.ComparisonOperator.Match, // Regex
                Infragistics.Windows.Controls.ComparisonOperator.Equals
            };

            // In XAML, we are setting ItemsSource property of _fieldComboEditor 
            // and _operatorComboEditor to above two lists respectively using
            // dynamic resource.
            // 
            this.Resources.Add("SearchFieldNames", searchFieldNames);
            this.Resources.Add("SearchOperators", searchOperators);
        }

        private void OnFieldComboEditor_ValueChanged(object sender, RoutedEventArgs e)
        {
            this.OnSearchCriteriaChanged();
        }

        private void OnOperatorComboEditor_ValueChanged(object sender, RoutedEventArgs e)
        {
            this.OnSearchCriteriaChanged();
        }

        private void OnOperandTextEditor_ValueChanged(object sender, RoutedEventArgs e)
        {
            this.OnSearchCriteriaChanged();
        }

        private void OnSearchCriteriaChanged()
        {
            // Don't process change notifications until all the controls have been loaded.
            if (!this.IsInitialized)
                return;

            // Get the field whose data will be searched.
            string searchFieldName = _fieldComboEditor.Text;

            // Get the comparison operator (like Equals, StartsWith, Contains etc...) to use.
            // Note that _operatorComboEditor's Value will be of type ComparisonOperator 
            // because we set ValueType on the editor to ComparisonOperator type in XAML 
            // where we defined this control. 
            Infragistics.Windows.Controls.ComparisonOperator searchOperator = (
                Infragistics.Windows.Controls.ComparisonOperator)_operatorComboEditor.Value;
            string searchValue = _operandTextEditor.Text;

            // Get the record filter collection from the data grid.
            Infragistics.Windows.DataPresenter.RecordFilterCollection recordFilterCollection = this.XamDataGrid1.FieldLayouts[0].RecordFilters;

            // Clear the collection, in case we've added any record filters previously.
            recordFilterCollection.Clear();

            if (!string.IsNullOrEmpty(searchValue))
            {
                try
                {
                    // Create a new RecordFilter object based on the search criteria.
                    RecordFilter recordFilter = new RecordFilter();
                    recordFilter.FieldName = searchFieldName;
                    recordFilter.Conditions.Add(new Infragistics.Windows.Controls.ComparisonCondition(searchOperator,
                                                                                                      searchValue));

                    // Add the new record filter.
                    recordFilterCollection.Add(recordFilter);
                }
                catch (Exception e)
                {
                    MessageBoxResult result = MessageBox.Show(e.Message);
                }
            }
        }
    }
}