using System.Collections.Generic;
using System.Data;
using System.Windows;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.Events;

namespace IGDataGrid.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CustomFilterDropDownItems.xaml
    /// </summary>
    public partial class CustomFilterDropDownItems : SampleContainer
    {
        public CustomFilterDropDownItems()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Orders);
            this.XamDataGrid1.DataSource = dt.DefaultView;
        }

        private void XamDataGrid1_RecordFilterDropDownPopulating(object sender, RecordFilterDropDownPopulatingEventArgs e)
        {
            if (e.Field.EditAsTypeResolved == typeof(string))
            {
                // At this point, e.DropDownItems will be pre-populated with special operands, like
                // (Custom), (Blanks), (NonBlanks) etc... You can remove them if you don't want to
                // display those options.
                // 
                // Remove all but (All) and (Custom) items from the drop-down.
                // 
                for (int i = e.DropDownItems.Count - 1; i >= 0; i--)
                {
                    // Remove all but (All) and (Custom) items from the list.
                    // 
                    if (e.DropDownItems[i].DisplayText != "(All)"
                        && e.DropDownItems[i].DisplayText != "(Custom)")
                    {
                        e.DropDownItems.RemoveAt(i);
                    }
                }

                // After this event is raised, data presenter will populate the drop-down with
                // field values. If you don't want to display field values in the filter drop-down
                // then set IncludeUniqueValues to false, which will prevent the data presenter
                // from adding field values to the drop-down.
                // 
                e.IncludeUniqueValues = false;

                // Add custom items. Items can be ICondition derived class (here we are using built-in
                // ComparisonCondition), or a SpecialFilterBase derived class, or a ICommand (which
                // lets you to take an action, like show a dialog). You can even implement your own 
                // ICondition to provide completely custom logic.
                // 

                if (System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName == "ja")
                {
                    //Since the Japanese data contains English characters for the CustomerID field,
                    //I added the English FilterDropDownItems to that drop-down list
                    if (e.Field.Name == "CustomerID")
                    {
                        AddFilterDropDownItems(e.DropDownItems);
                    }
                    else
                    {
                        FilterDropDownItem item;
                        // This item uses the standard unicode range of hiragana characters
                        item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                            Infragistics.Windows.Controls.ComparisonOperator.Match, "^[\u3040-\u309F]"), "ひらがな");
                        e.DropDownItems.Add(item);

                        // This item uses the standard unicode range of katakana characters
                        item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                            Infragistics.Windows.Controls.ComparisonOperator.Match, "^[\u30A0-\u30FF]"), "カタカナ");
                        e.DropDownItems.Add(item);

                        // This item uses the standard unicode range of kanji characters
                        item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                            Infragistics.Windows.Controls.ComparisonOperator.Match, "^[\u4E00-\u9FBF]"), "漢字");
                        e.DropDownItems.Add(item);

                        // This item uses the standard unicode range of roman characters
                        item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                            Infragistics.Windows.Controls.ComparisonOperator.Match, "^[A-Z]"), "ローマ字");
                        e.DropDownItems.Add(item);
                    }
                }
                else
                {
                    //FilterDropDownItems for English data
                    AddFilterDropDownItems(e.DropDownItems);
                }
            }
        }

        private void AddFilterDropDownItems(IList<FilterDropDownItem> filterList)
        {
            FilterDropDownItem item;
            item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                Infragistics.Windows.Controls.ComparisonOperator.Match, "^[A-F]"), "[A-F]");
            filterList.Add(item);

            item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                Infragistics.Windows.Controls.ComparisonOperator.Match, "^[G-K]"), "[G-K]");
            filterList.Add(item);

            item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                Infragistics.Windows.Controls.ComparisonOperator.Match, "^[L-P]"), "[L-P]");
            filterList.Add(item);

            item = new FilterDropDownItem(new Infragistics.Windows.Controls.ComparisonCondition(
                Infragistics.Windows.Controls.ComparisonOperator.Match, "^[Q-Z]"), "[Q-Z]");
            filterList.Add(item);
        }
    }
}