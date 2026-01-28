using System.Threading;
using System.Windows;
using System.Windows.Threading;
using IGUndoRedoFramework.Resources;
using IGUndoRedoFramework.ViewModel;
using Infragistics.Samples.Framework;
using Infragistics.Undo;
using Infragistics.Windows.DataPresenter;

namespace IGUndoRedoFramework.Samples.Organization
{
    public partial class UndoRedoXamDataGrid : SampleContainer
    {
        public UndoRedoXamDataGrid()
        {
            InitializeComponent();
            this.DataContext = new CustomersViewModel();
        }

        private UndoManager UndoManager
        {
            get { return ((CustomersViewModel)this.DataContext).UndoManager; }
        }

        private void DataGrid_RecordAdding(object sender, Infragistics.Windows.DataPresenter.Events.RecordAddingEventArgs e)
        {
            var transaction = this.UndoManager.StartTransaction(UndoRedoStrings.UndoRedo_AddRow, UndoRedoStrings.UndoRedo_AddRow_DetailedDesc);

            new DispatcherSynchronizationContext().Post(new SendOrPostCallback((o) =>
            {
                if (!transaction.IsClosed)
                    transaction.Commit();
            }), transaction);
        }

        private void DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            if (this.dataGrid.SelectedItems.Records.Count != 0)
            {
                this.dataGrid.ExecuteCommand(DataPresenterCommands.DeleteSelectedDataRecords);
            }                              
        }
    }
}
