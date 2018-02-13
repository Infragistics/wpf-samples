using IgWord.Infrastructure;
using IgWord.Infrastructure.Dialogs;
using Infragistics.Documents.RichText;
using Prism.Commands;
using Prism.Regions;
using System;

namespace IgWord.Dialogs
{
    public class InsertTableDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Member Variables

        private int numberOfRows;
        private int numberOfColumns;
        private double fixedColumnWidth;
        private bool? isFixedColumnWidth = false;
        private bool? isAutoFitToContents = false;
        private bool? isAutoFitToWindow = false;

        private RichTextDocument document;
        private Selection selection;

        #endregion //Member Variables

        #region Public Properties

        public int NumberOfColumns
        {
            get { return this.numberOfColumns; }
            set
            {
                if (value != this.numberOfColumns)
                {
                    this.numberOfColumns = value;

                    NotifyPropertyChanged();
                }
            }
        }
        public int NumberOfRows
        {
            get { return this.numberOfRows; }
            set
            {
                if (value != this.numberOfRows)
                {
                    this.numberOfRows = value;

                    NotifyPropertyChanged();
                }
            }
        }
        public double FixedColumnWidth
        {
            get { return this.fixedColumnWidth; }
            set
            {
                if (value != this.fixedColumnWidth)
                {
                    this.fixedColumnWidth = value;

                    NotifyPropertyChanged();
                }
            }
        }
        public bool? IsFixedColumnWidth
        {
            get { return this.isFixedColumnWidth; }
            set
            {
                if (value.Value != this.isFixedColumnWidth.Value)
                {
                    this.isFixedColumnWidth = value;

                    NotifyPropertyChanged();
                }
            }
        }
        public bool? IsAutoFitToContents
        {
            get { return this.isAutoFitToContents; }
            set
            {
                if (value.Value != this.isAutoFitToContents.Value)
                {
                    this.isAutoFitToContents = value;

                    NotifyPropertyChanged();
                }
            }
        }
        public bool? IsAutoFitToWindow
        {
            get { return this.isAutoFitToWindow; }
            set
            {
                if (value.Value != this.isAutoFitToWindow.Value)
                {
                    this.isAutoFitToWindow = value;

                    NotifyPropertyChanged();
                }
            }
        }

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion //Public Properties

        #region Constructors

        public InsertTableDialogViewModel()
        {
            this.Title = ResourceStrings.ResourceStrings.Dlg_InsertTable_Title;

            this.IconSource = "pack://application:,,,/IgWord.Infrastructure;component/Images/Table.ico";

            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            CancelCommand = new DelegateCommand(ExecuteCancel);
        }

        #endregion //Constructors

        #region Commands

        private bool CanExecuteOk()
        {
            return true;
        }

        private void ExecuteOk()
        {
            TableExtentBehavior tableExtentBehavior = TableExtentBehavior.FitColumnsToContent;
            Extent? fixedColumnWidth = null;
            if (this.IsFixedColumnWidth.HasValue && this.IsFixedColumnWidth.Value)
            {
                tableExtentBehavior = TableExtentBehavior.FixedWidthColumns;
                fixedColumnWidth = new Extent(this.FixedColumnWidth, ExtentUnitType.Inches);
            }
            else
                if (this.IsAutoFitToWindow.HasValue && this.IsAutoFitToWindow.Value)
                    tableExtentBehavior = TableExtentBehavior.FitTableToWindow;

            // Insert the table.
            string error = null;
            this.document.InsertTable(this.selection.Start, this.NumberOfColumns, this.NumberOfRows, out error, tableExtentBehavior, null, fixedColumnWidth);

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

        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            this.document = (RichTextDocument)navigationContext.Parameters[InsertTableDialogParameters.DocumentKey];
            this.selection = (Selection)navigationContext.Parameters[InsertTableDialogParameters.SelectionKey];

            NumberOfColumns = 5;
            NumberOfRows = 2;
            FixedColumnWidth = 0;
            IsFixedColumnWidth = true;
            IsAutoFitToContents = false;
            IsAutoFitToWindow = false;
        }

        #endregion //INavigationAware
    }
}
