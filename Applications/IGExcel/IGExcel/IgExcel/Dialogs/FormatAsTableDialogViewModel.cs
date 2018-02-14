using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;

namespace IgExcel.Dialogs
{
    public class FormatAsTableDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Private Variables

        private IEventAggregator eventAggragator;

        private SpreadsheetSelection selection;
        private Worksheet activeWorksheet;
        private Workbook workbook;
        private string tableStyle;

        #endregion //Private Variables

        #region  Public Properties

        private bool tableHasHeaders;
        public bool TableHasHeaders
        {
            get { return tableHasHeaders; }
            set { SetProperty<bool>(ref tableHasHeaders, value); }
        }

        private string rangeAddress;
        public string RangeAddress
        {
            get { return rangeAddress; }
            set { SetProperty<string>(ref rangeAddress, value); }
        }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion // Public Properties

        #region Constructor

        public FormatAsTableDialogViewModel(IEventAggregator eventAggragator)
        {
            this.Title = ResourceStrings.ResourceStrings.Text_FormatAsTable;
            this.IconSource = "pack://application:,,,/IgExcel.Infrastructure;component/Images/FormatAsTable.ico";

            this.eventAggragator = eventAggragator;

            OkCommand = new DelegateCommand(ExecuteOk);
            CancelCommand = new DelegateCommand(ExecuteCancel);
        }

        #endregion //Constructor

        #region Commands

        private bool CanExecuteOk()
        {
            return true;
        }

        private void ExecuteOk()
        {
            var range = selection.CellRanges[0].ToString(this.workbook.CellReferenceMode);
            var table = this.activeWorksheet.Tables.Add(range, this.tableHasHeaders, this.workbook.StandardTableStyles[this.tableStyle]);
            eventAggragator.GetEvent<TableAddedEvent>().Publish(table);

            RequestClose();
        }

        private void ExecuteCancel()
        {
            RequestClose();
        }

        #endregion //Commands

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            return true;
        }

        public event Action RequestClose;

        void CloseDialog()
        {
            if (RequestClose != null)
            {
                RequestClose();
            }
        }

        #endregion //IDialogAware Members

        #region INavigationAware

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.selection = (SpreadsheetSelection)navigationContext.Parameters[FormatAsTableDialogParameters.SelectionKey];
            this.activeWorksheet = (Worksheet)navigationContext.Parameters[FormatAsTableDialogParameters.WorksheetKey];
            this.workbook = (Workbook)navigationContext.Parameters[FormatAsTableDialogParameters.WorkbookKey];
            this.tableStyle = (string)navigationContext.Parameters[FormatAsTableDialogParameters.TableStyleKey];

            this.RangeAddress = selection.CellRanges[0].ToString(this.workbook.CellReferenceMode);
        }

        #endregion //INavigationAware
    }
}
