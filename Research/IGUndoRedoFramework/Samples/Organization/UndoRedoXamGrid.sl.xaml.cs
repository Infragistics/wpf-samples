using System.Threading;
using System.Windows;
using System.Windows.Threading;
using IGUndoRedoFramework.ViewModel;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using Infragistics.Undo;

namespace IGUndoRedoFramework.Samples.Organization
{
    public partial class UndoRedoXamGrid : SampleContainer
    {      
        public UndoRedoXamGrid()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        ButtonTool btnUndo;
        ButtonTool btnRedo;

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = new CustomersViewModel();
            UndoManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UndoManager_PropertyChanged);
            btnUndo = xamRibbon.FindToolById("UNDO_TOOL") as ButtonTool;
            btnRedo = xamRibbon.FindToolById("REDO_TOOL") as ButtonTool;
        }

        void UndoManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (btnUndo != null)
                btnUndo.IsEnabled = UndoManager.CanUndo;
            if (btnRedo != null)
                btnRedo.IsEnabled = UndoManager.CanRedo;
        }

        private UndoManager UndoManager
        {
            get 
            { 
                return ((CustomersViewModel)this.DataContext).UndoManager; 
            }
        }

        private void AddContact_Click(object sender, RibbonToolEventArgs e)
        {
            this.dataGrid.AddNewRowSettings.AllowAddNewRow = AddNewRowLocation.Top;
        }

        private void DeleteContact_Click(object sender, RibbonToolEventArgs e)
        {
            // Get the selected row for deletion
            Row selectedRow = this.dataGrid.SelectionSettings.SelectedRows[0];

            if (selectedRow == null)
                return;

            // Delete selected row
            this.dataGrid.Rows.Remove(selectedRow);
        }

        private void dataGrid_RowAdding(object sender, Infragistics.Controls.Grids.CancellableRowAddingEventArgs e)
        {
            var transaction = this.UndoManager.StartTransaction("Add Row", "Add Row");

            new DispatcherSynchronizationContext().Post(new SendOrPostCallback((o) =>
            {
                if (!transaction.IsClosed)
                    transaction.Commit();
            }), transaction);
        }
    }
}
