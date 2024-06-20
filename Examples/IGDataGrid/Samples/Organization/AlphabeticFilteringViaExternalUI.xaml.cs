using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for AlphabeticFilteringViaExternalUI.xaml
    /// </summary>
    public partial class AlphabeticFilteringViaExternalUI : SampleContainer
    {
        private Dictionary<string, bool> filterCharacters = new Dictionary<string, bool>(26);

        public AlphabeticFilteringViaExternalUI()
        {
            InitializeComponent();

            // Setup and attach the data source to the XamDataGrid.
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.XamDataGrid1.DataSource = dt.DefaultView;

            // Intialize the dictionary containing the characters and their selected status.
            string[] characters = { 
                                      "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P",
                                      "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            for (int i = 0; i < 26; i++)
            {
                this.filterCharacters.Add(characters[i], false);
            }
        }

        private void OnAlphaCharacterClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button == null)
                return;

            // Establish the new selected status of the filter character by toggling the previous value stored in our dictionary.
            this.filterCharacters[(string)button.Content] = !this.filterCharacters[(string)button.Content];

            // Change the opacity of the button depending on whether its filter character is selected.
            button.Opacity = this.filterCharacters[(string)button.Content] ? 1.0 : .3;

            // Construct/Reconstruct the record filter base on the current selected state of the characters.
            Infragistics.Windows.DataPresenter.FieldLayout fieldLayout = this.XamDataGrid1.DefaultFieldLayout;
            Infragistics.Windows.DataPresenter.RecordFilterCollection recordFilterCollection = fieldLayout.RecordFilters;

            Infragistics.Windows.DataPresenter.RecordFilter filter = new Infragistics.Windows.DataPresenter.RecordFilter(fieldLayout.Fields["CustomerID"]);
            filter.Conditions.LogicalOperator = Infragistics.Windows.Controls.LogicalOperator.Or;

            foreach (KeyValuePair<string, bool> entry in this.filterCharacters)
            {
                // Only process the character if it is selected
                if (entry.Value == true)
                {
                    // Use both upper and lowercase versions of the character.
                    filter.Conditions.Add(new Infragistics.Windows.Controls.ComparisonCondition(
                        Infragistics.Windows.Controls.ComparisonOperator.StartsWith, entry.Key));
                    filter.Conditions.Add(new Infragistics.Windows.Controls.ComparisonCondition(
                        Infragistics.Windows.Controls.ComparisonOperator.StartsWith, entry.Key.ToLower()));
                }
            }

            recordFilterCollection.Clear();
            recordFilterCollection.Add(filter);
        }
    }
}
