using System.Windows;
using System.Windows.Controls;
using IGPivotGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Olap.Data;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Display
{
    public partial class CustomHeaderAndCellTemplates : SampleContainer
    {
        public CustomHeaderAndCellTemplates()
        {
            InitializeComponent();
        }

        private void HeaderTemplate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(((IMember)btn.DataContext).Caption, PivotGridStrings.XPG_HeaderClicked, MessageBoxButton.OK);
        }

        private void DataCellTemplate_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MessageBox.Show(btn.DataContext.ToString(), PivotGridStrings.XPG_DataCellClicked, MessageBoxButton.OK);
        }

        private void ApplyFromCode_Click(object sender, RoutedEventArgs e)
        {
            if (ApplyFromCodeButton.Content.ToString() == PivotGridStrings.XPG_ApplyTemplatesFromCode)
            {
                HeaderTemplate columnHeaderTemplate = new HeaderTemplate();
                columnHeaderTemplate.Template = Resources["ColumnHeaderDataTemplate3"] as DataTemplate;
                columnHeaderTemplate.Hierarchy = PivotGridStrings.XPG_Data_PropertyNames_Product; //Product
                columnHeaderTemplate.Level = 1;
                pivotGrid.ColumnHeaderTemplates.Add(columnHeaderTemplate);

                HeaderTemplate rowHeaderTemplate = new HeaderTemplate();
                rowHeaderTemplate.Template = Resources["RowHeaderDataTemplate3"] as DataTemplate;
                rowHeaderTemplate.Hierarchy = PivotGridStrings.XPG_Data_PropertyNames_City; //City
                rowHeaderTemplate.Level = 1;
                pivotGrid.RowHeaderTemplates.Add(rowHeaderTemplate);

                DataCellTemplate dataCellTemplate = new DataCellTemplate();
                dataCellTemplate.Template = Resources["DataCellDataTemplate4"] as DataTemplate;
                dataCellTemplate.ColumnHierarchy = PivotGridStrings.XPG_Data_PropertyNames_Product; //Product
                dataCellTemplate.ColumnLabel = PivotGridStrings.XPG_Clothing; //Clothing
                pivotGrid.DataCellTemplates.Add(dataCellTemplate);

                // Refresh the grid.
                pivotGrid.ArrangeLayout();

                ApplyFromCodeButton.Content = PivotGridStrings.XPG_RemoveAddedTemplates;
            }
            else
            {
                // Remove the last added templates.
                pivotGrid.ColumnHeaderTemplates.RemoveAt(pivotGrid.ColumnHeaderTemplates.Count - 1);
                pivotGrid.RowHeaderTemplates.RemoveAt(pivotGrid.RowHeaderTemplates.Count - 1);
                pivotGrid.DataCellTemplates.RemoveAt(pivotGrid.DataCellTemplates.Count - 1);
                pivotGrid.ArrangeLayout();

                ApplyFromCodeButton.Content = PivotGridStrings.XPG_ApplyTemplatesFromCode;
            }
        }
    }
}
