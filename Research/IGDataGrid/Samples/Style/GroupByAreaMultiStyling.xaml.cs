using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using IGDataGrid.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;

namespace IGDataGrid.Samples.Style
{
    /// <summary>
    /// Interaction logic for GroupByAreaMultiStyling.xaml
    /// </summary>
    public partial class GroupByAreaMultiStyling : SampleContainer
    {
        public GroupByAreaMultiStyling()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(GroupByAreaMultiStyling_Loaded);
        }

        private void GroupByAreaMultiStyling_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = NWindDataSource.GetTable(NWindTable.Customers);
            this.XamDataGrid1.DataSource = dt.DefaultView;

            if (this.cboDashStyles.Items.Count == 0)
            {
                this.cboDashStyles.Items.Add(DataGridStrings.ConfigArea_DashStyle_Dash);
                this.cboDashStyles.Items.Add(DataGridStrings.ConfigArea_DashStyle_DashDot);
                this.cboDashStyles.Items.Add(DataGridStrings.ConfigArea_DashStyle_DashDotDot);
                this.cboDashStyles.Items.Add(DataGridStrings.ConfigArea_DashStyle_Dot);
                this.cboDashStyles.Items.Add(DataGridStrings.ConfigArea_DashStyle_Solid);
                this.cboDashStyles.SelectedIndex = 4;
            }
        }

        private void Compact_Checked(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.GroupByAreaMode = GroupByAreaMode.MultipleFieldLayoutsCompact;
        }

        private void Compact_Unchecked(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.GroupByAreaMode = GroupByAreaMode.MultipleFieldLayoutsFull;
        }

        private void CustomFieldLayoutDescriptionTemplate_Checked(object sender, RoutedEventArgs e)
        {
            DataTemplate myFieldLayoutDescriptionTemplate = this.FindResource("myFieldLayoutDescriptionTemplate") as DataTemplate;
            if (myFieldLayoutDescriptionTemplate != null)
                this.XamDataGrid1.GroupByAreaMulti.FieldLayoutDescriptionTemplate = myFieldLayoutDescriptionTemplate;
        }

        private void CustomFieldLayoutDescriptionTemplate_Unchecked(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.GroupByAreaMulti.ClearValue(GroupByAreaMulti.FieldLayoutDescriptionTemplateProperty);
        }

        private void CustomPromptsTemplate_Checked(object sender, RoutedEventArgs e)
        {
            DataTemplate myPrompt1Template = this.FindResource("myPrompt1Template") as DataTemplate;
            if (myPrompt1Template != null)
                this.XamDataGrid1.GroupByAreaMulti.Prompt1Template = myPrompt1Template;

            DataTemplate myPrompt2Template = this.FindResource("myPrompt2Template") as DataTemplate;
            if (myPrompt2Template != null)
                this.XamDataGrid1.GroupByAreaMulti.Prompt2Template = myPrompt2Template;
        }

        private void CustomPromptsTemplate_Unchecked(object sender, RoutedEventArgs e)
        {
            this.XamDataGrid1.GroupByAreaMulti.ClearValue(GroupByAreaMulti.Prompt1TemplateProperty);
            this.XamDataGrid1.GroupByAreaMulti.ClearValue(GroupByAreaMulti.Prompt2TemplateProperty);
        }

        private void cboDashStyles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cboDashStyles.SelectedIndex == 0)
                this.XamDataGrid1.GroupByAreaMulti.ConnectorLinePen.DashStyle = DashStyles.Dash;
            else
                if (this.cboDashStyles.SelectedIndex == 1)
                    this.XamDataGrid1.GroupByAreaMulti.ConnectorLinePen.DashStyle = DashStyles.DashDot;
                else
                    if (this.cboDashStyles.SelectedIndex == 2)
                        this.XamDataGrid1.GroupByAreaMulti.ConnectorLinePen.DashStyle = DashStyles.DashDotDot;
                    else
                        if (this.cboDashStyles.SelectedIndex == 3)
                            this.XamDataGrid1.GroupByAreaMulti.ConnectorLinePen.DashStyle = DashStyles.Dot;
                        else
                            this.XamDataGrid1.GroupByAreaMulti.ConnectorLinePen.DashStyle = DashStyles.Solid;
        }
    }
}
