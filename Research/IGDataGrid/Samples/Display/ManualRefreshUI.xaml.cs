using IGDataGrid.DataSources;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DataPresenter;
using System;
using System.Windows;

namespace IGDataGrid.Samples.Display
{
    public partial class ManualRefreshUI : SampleContainer
    {
        private PlainObjects plainDataSource;

        public ManualRefreshUI()
        {
            InitializeComponent();
            this.plainDataSource = new PlainObjects();
            this.SampleLoaded += ManualRefreshUI_SampleLoaded;
            this.SampleUnloaded += ManualRefreshUI_SampleUnloaded;
            this.dataGrid.DataSource = this.plainDataSource.Books;
        }

        void ManualRefreshUI_SampleLoaded(object sender, EventArgs e)
        {
            this.plainDataSource.StartUpdateThread();
        }

        void ManualRefreshUI_SampleUnloaded(object sender, EventArgs e)
        {
            this.plainDataSource.StopUpdateThread();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Record record in this.dataGrid.Records)
            {
                if (record is DataRecord)
                {
                    ((DataRecord)record).RefreshCellValues();

                    // refresh a cell by providing the related field
                    //var field = this.dataGrid.FieldLayouts[0].Fields[0];
                    //((DataRecord)record).RefreshCellValue(field);

                    // refresh all cells on a record
                    //foreach (Cell cell in ((DataRecord)record).Cells) {
                    //    cell.RefreshValue();
                    //}
                }
            }
        }
    }
}
