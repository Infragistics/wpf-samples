using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using IgExcel.Services;
using Infragistics;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Infragistics.Documents.Core;
using Infragistics.Documents.Excel;
using Infragistics.Documents.Excel.ConditionalFormatting;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace IgExcel
{
    public class ShellViewModel : ViewModelBase, IDialogAware, IViewModel
    {
        #region Private Memebers
        private IDialogService dialogService;
        private IMessageBoxService messageBoxService;
        private IEventAggregator eventAggragator;
        private IRecentFilesService recentFilesService;
        private IStyleService styleService;

        private IgExcelDocumentStatus documentStatus;
        private SpreadsheetSelection selection;
        private Worksheet activeWorksheet;
        private Workbook workbook;
        private WorksheetTable selectedTable;

        private FileStream lockFileStream;

        private string fileName;
        private string fileNameWithoutExtension;
        private string selectedBackstageMenuItem;
        private string selectedTemplateName;

        private bool isNewTemplateRequested;
        private bool closeRequested;
        private bool isSpreadSheetVisible;
        private bool isBackstageOpened;
        private bool isUiPartEnabled;
        private bool canExecuteFormatAsTable;
        private bool workbookDirtied;
        private bool trackChanges;
        private bool isInEditMode;

        private int zoomLevel;

        #endregion //Private Memebers

        #region Public Properties

        public List<int> FontSizes { get; private set; }
        public List<string> FontNames { get; private set; }
        public List<string> TableStyles { get; private set; }
        public List<string> CellStyles { get; private set; }
        public List<string> GoodBadAndNeutralCellStyles { get; private set; }
        public List<string> DataAndModelCellStyles { get; private set; }
        public List<string> TitlesAndHeadingsCellStyles { get; private set; }
        public List<string> NumberFormatCellStyles { get; private set; }
        public List<string> ThemedCellStyles { get; private set; }

        public string FileName
        {
            get { return fileName; }
            set { SetProperty<string>(ref fileName, value); }
        }
        public string FileNameWithoutExtension
        {
            get { return fileNameWithoutExtension; }
            set { SetProperty<string>(ref fileNameWithoutExtension, value); }
        }
        public string SelectedBackstageMenuItem
        {
            get { return selectedBackstageMenuItem; }
            set
            {
                selectedBackstageMenuItem = value;
                //Use the OnPropertyChanged to fire notification even if the value is the same
                base.OnPropertyChanged("SelectedBackstageMenuItem");
            }
        }

        public IgExcelDocumentStatus DocumentStatus
        {
            get { return documentStatus; }
            set { SetProperty<IgExcelDocumentStatus>(ref documentStatus, value); }
        }

        public int ZoomLevel
        {
            get { return zoomLevel; }
            set { SetProperty<int>(ref zoomLevel, value); }
        }

        public bool IsBackstageOpened
        {
            get { return isBackstageOpened; }
            set { SetProperty<bool>(ref isBackstageOpened, value); }
        }
        public bool IsSpreadSheetVisible
        {
            get { return isSpreadSheetVisible; }
            set { SetProperty<bool>(ref isSpreadSheetVisible, value); }
        }
        public bool IsUiPartEnabled
        {
            get { return isUiPartEnabled; }
            set { SetProperty<bool>(ref isUiPartEnabled, value); }
        }
        public bool CanExecuteFormatAsTable
        {
            get { return canExecuteFormatAsTable; }
            set { SetProperty<bool>(ref canExecuteFormatAsTable, value); }
        }
        public bool WorkbookDirtied
        {
            get { return workbookDirtied; }
            set
            {
                SetProperty<bool>(ref workbookDirtied, value);
                MarkDocumentAsModified();
            }
        }
        public bool TrackChanges
        {
            get { return trackChanges; }
            set { SetProperty<bool>(ref trackChanges, value); }
        }

        public Worksheet ActiveWorksheet
        {
            get { return activeWorksheet; }
            set { SetProperty<Worksheet>(ref activeWorksheet, value); }
        }
        public Workbook Workbook
        {
            get { return workbook; }
            set
            {
                if (this.workbook != value)
                {
                    var command = new SpreadsheetCommand(SpreadsheetCommandType.ExitEditModeAndDiscardChanges);
                    this.InvokeIGCommand("spreadSheet", command);
                    SetProperty<Workbook>(ref workbook, value);
                }
            }
        }
        public SpreadsheetSelection Selection
        {
            get { return selection; }
            set { SetProperty<SpreadsheetSelection>(ref selection, value); }
        }
        public WorksheetTable SelectedTable
        {
            get { return selectedTable; }
            set { SetProperty<WorksheetTable>(ref selectedTable, value); }
        }
        public bool IsInEditMode
        {
            get { return isInEditMode; }
            set { SetProperty<bool>(ref isInEditMode, value); }
        }

        public DelegateCommand<object> ShowNumberSettingsDialogCommand { get; private set; }
        public DelegateCommand<object> ShowFontSettingsDialogCommand { get; private set; }
        public DelegateCommand<object> ShowFillSettingsDialogCommand { get; private set; }
        public DelegateCommand<object> ShowAlignmnetSettingsDialogCommand { get; private set; }
        public DelegateCommand<DimensionDialogMode?> ShowDimensionDialogCommand { get; private set; }
        public DelegateCommand<string> FormatAsTableCommand { get; private set; }
        public DelegateCommand CloseDocumentCommand { get; private set; }
        public DelegateCommand SaveDocumentCommand { get; private set; }
        public DelegateCommand<string> CreateNewDocumentCommand { get; private set; }
        public DelegateCommand ShowZoomDialogCommand { get; private set; }
        public DelegateCommand ShowDataValidationDialogCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public ShellViewModel(IEventAggregator eventAggragator, IDialogService dialogService, IMessageBoxService messageBoxService, IFontService fontService, IStyleService styleService, IRecentFilesService recentFilesService)
        {
            this.eventAggragator = eventAggragator;
            this.dialogService = dialogService;
            this.messageBoxService = messageBoxService;
            this.recentFilesService = recentFilesService;
            this.styleService = styleService;

            this.FontSizes = fontService.GetFontSizes();
            this.FontNames = fontService.GetFontNames();
            this.TableStyles = styleService.GetAllTableStyles();
            this.CellStyles = styleService.GetAllCellStyles();
            this.GoodBadAndNeutralCellStyles = styleService.GetGoodBadAndNeutralCellStyles();
            this.DataAndModelCellStyles = styleService.GetDataAndModelCellStyles();
            this.TitlesAndHeadingsCellStyles = styleService.GetTitlesAndHeadingsCellStyles();
            this.NumberFormatCellStyles = styleService.GetNumberFormatCellStyles();
            this.ThemedCellStyles = styleService.GetThemedCellStyles();

            this.IsUiPartEnabled = false;
            this.SetTitle();

            this.SelectedBackstageMenuItem = ShellParameters.NewTabName;

			// TFS 191000 - Workaround: do not initialize the value of the IsBackstageOpen property to true before the ribbon is loaded
			//this.IsBackstageOpened = true;
            
			this.ZoomLevel = 100;

            this.recentFilesService.SetRepositories(Properties.Settings.Default, p => Properties.Settings.Default.RecentFilesRepo, p => Properties.Settings.Default.RecentFoldersRepo);

            this.eventAggragator.GetEvent<FileOpenedEvent>().Subscribe(OnFileLoaded);
            this.eventAggragator.GetEvent<FileSavedEvent>().Subscribe(OnFileSaved);
            this.eventAggragator.GetEvent<NewDocumentEvent>().Subscribe(OnNewDocumentEvent);
            this.eventAggragator.GetEvent<TableAddedEvent>().Subscribe(OnTableAddedEvent);
            this.eventAggragator.GetEvent<CustomZoomLevelChangedEvent>().Subscribe(OnCustomZoomLevelChangedEvent);
            this.eventAggragator.GetEvent<PasswordToOpenEnteredEvent>().Subscribe(OnPasswordToOpenEnteredEvent);
            this.eventAggragator.GetEvent<PasswordToModifyEnteredEvent>().Subscribe(OnPasswordToModifyEnteredEvent);

            //Commands
            this.ShowNumberSettingsDialogCommand = new DelegateCommand<object>(ExecuteShowNumberSettingsDialog);
            this.ShowFontSettingsDialogCommand = new DelegateCommand<object>(ExecuteShowFontSettingsDialog);
            this.ShowFillSettingsDialogCommand = new DelegateCommand<object>(ExecuteShowFillSettingsDialog);
            this.ShowAlignmnetSettingsDialogCommand = new DelegateCommand<object>(ExecuteShowAlignmnetSettingsDialog);
            this.ShowDimensionDialogCommand = new DelegateCommand<DimensionDialogMode?>(ExecuteShowDimensionDialog);
            this.CloseDocumentCommand = new DelegateCommand(ExecuteCloseDocument);
            this.FormatAsTableCommand = new DelegateCommand<string>(ExecuteFormatAsTable);
            this.SaveDocumentCommand = new DelegateCommand(ExecuteSaveDocument);
            this.CreateNewDocumentCommand = new DelegateCommand<string>(CreateNewDocument);
            this.ShowZoomDialogCommand = new DelegateCommand(ExecuteShowZoomDialog);
            this.ShowDataValidationDialogCommand = new DelegateCommand(ExecuteShowDataValidationDialog);

            this.PropertyChanged += OnPropertyChanged;
        }

        #endregion //Constructor

        #region Commnds

        private void ExecuteShowDimensionDialog(DimensionDialogMode? mode)
        {
            if (mode.HasValue)
            {
                NavigationParameters parameters = new NavigationParameters();
                parameters.Add(DimensionDialogViewParameters.SelectionKey, this.selection);
                parameters.Add(DimensionDialogViewParameters.WorksheetKey, this.activeWorksheet);
                parameters.Add(DimensionDialogViewParameters.DialogModeKey, mode);

                this.dialogService.ShowIgDialog(DimensionDialogViewParameters.ViewName, parameters);
            }
        }

        private void ExecuteShowAlignmnetSettingsDialog(object parameter)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FormatCellsDialogParameters.InitialTabNameKey, FormatCellsDialogParameters.AlignmentTabKey);
            parameters.Add(FormatCellsDialogParameters.SelectionKey, this.selection);
            parameters.Add(FormatCellsDialogParameters.WorksheetKey, this.activeWorksheet);
            parameters.Add(FormatCellsDialogParameters.SelectionFormatsKey, parameter);

            this.dialogService.ShowIgDialog(FormatCellsDialogParameters.ViewName, parameters);
        }

        private void ExecuteShowFillSettingsDialog(object parameter)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FormatCellsDialogParameters.InitialTabNameKey, FormatCellsDialogParameters.FillTabKey);
            parameters.Add(FormatCellsDialogParameters.SelectionKey, this.selection);
            parameters.Add(FormatCellsDialogParameters.WorksheetKey, this.activeWorksheet);
            parameters.Add(FormatCellsDialogParameters.SelectionFormatsKey, parameter);

            this.dialogService.ShowIgDialog(FormatCellsDialogParameters.ViewName, parameters);
        }

        private void ExecuteShowFontSettingsDialog(object parameter)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FormatCellsDialogParameters.InitialTabNameKey, FormatCellsDialogParameters.FontTabKey);
            parameters.Add(FormatCellsDialogParameters.SelectionKey, this.selection);
            parameters.Add(FormatCellsDialogParameters.WorksheetKey, this.activeWorksheet);
            parameters.Add(FormatCellsDialogParameters.SelectionFormatsKey, parameter);

            this.dialogService.ShowIgDialog(FormatCellsDialogParameters.ViewName, parameters);
        }

        private void ExecuteShowNumberSettingsDialog(object parameter)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(FormatCellsDialogParameters.InitialTabNameKey, FormatCellsDialogParameters.NumberTabKey);
            parameters.Add(FormatCellsDialogParameters.SelectionKey, this.selection);
            parameters.Add(FormatCellsDialogParameters.WorksheetKey, this.activeWorksheet);
            parameters.Add(FormatCellsDialogParameters.SelectionFormatsKey, parameter);

            this.dialogService.ShowIgDialog(FormatCellsDialogParameters.ViewName, parameters);
        }

        private void ExecuteShowZoomDialog()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(ZoomDialogViewParameters.ZoomLevelKey, this.zoomLevel);
            this.dialogService.ShowIgDialog(ZoomDialogViewParameters.ViewName, parameters);
        }

        private void ExecuteShowDataValidationDialog()
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add(DataValidationDialogViewParameters.SelectionKey, this.selection);
            parameters.Add(DataValidationDialogViewParameters.WorksheetKey, this.activeWorksheet);
            this.dialogService.ShowIgDialog(DataValidationDialogViewParameters.ViewName, parameters, true);
        }

        private void ExecuteCloseDocument()
        {
            if (CanCloseDocument(true))
            {
                this.DocumentStatus = IgExcelDocumentStatus.NoDocumentLoaded;
                this.IsSpreadSheetVisible = false;
                this.IsUiPartEnabled = false;
                UnlockFile();
                SetTitle();
                CreateNewBlankWorkbook();
            }
        }

        private void ExecuteSaveDocument()
        {
            if (DocumentStatus == IgExcelDocumentStatus.BlankModified
                || DocumentStatus == IgExcelDocumentStatus.BlankNotModified
                || DocumentStatus == IgExcelDocumentStatus.TemplateNotModified)
            {

                if (this.workbook.IsFileWriteProtected)
                {
                    messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, string.Format(ResourceStrings.ResourceStrings.Msg_CannotSaveReadOnlyFile, this.fileNameWithoutExtension, Environment.NewLine), MessageBoxButtons.Ok);
                }

                if (!IsBackstageOpened)
                    IsBackstageOpened = true;

                SelectedBackstageMenuItem = ShellParameters.SaveAsTabName;
            }
            else if (DocumentStatus != IgExcelDocumentStatus.NoDocumentLoaded)
            {
                PushChangesToFile(FileName);

                if (this.isNewTemplateRequested)
                {
                    var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);

                    LoadTemplate(this.selectedTemplateName);
                    SetTitle(tempDocumentName);

                    this.FileNameWithoutExtension = tempDocumentName;

                    this.isNewTemplateRequested = false;
                    this.selectedTemplateName = string.Empty;
                }
                else
                {
                    DocumentStatus = IgExcelDocumentStatus.ExistingNotModified;
                    UpdateTitle(false);
                }

                WorkbookDirtied = false;
                IsBackstageOpened = false;
            }
        }

        private void ExecuteFormatAsTable(string tableStyle)
        {
            if (this.canExecuteFormatAsTable)
            {
                if (this.SelectedTable == null)
                {
                    NavigationParameters parameters = new NavigationParameters();
                    parameters.Add(FormatAsTableDialogParameters.SelectionKey, this.selection);
                    parameters.Add(FormatAsTableDialogParameters.WorksheetKey, this.activeWorksheet);
                    parameters.Add(FormatAsTableDialogParameters.WorkbookKey, this.workbook);
                    parameters.Add(FormatAsTableDialogParameters.TableStyleKey, tableStyle);
                    this.dialogService.ShowIgDialog(FormatAsTableDialogParameters.ViewName, parameters);

                }
                else
                {
                    this.SelectedTable.Style = this.Workbook.StandardTableStyles[tableStyle];
                }
            }
        }

        #endregion //Commnds

        #region Events

        private void OnFileSaved(string filePath)
        {
            if (this.fileName == null)
                this.FileName = string.Empty;

            if (this.fileName.ToLower() == filePath.ToLower() && this.workbook.IsFileWriteProtected)
            {
                this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, ResourceStrings.ResourceStrings.Msg_DocumentOpenedAsReadOnly, MessageBoxButtons.Ok);
                return;
            }

            this.FileName = filePath;
            this.DocumentStatus = IgExcelDocumentStatus.ExistingNotModified;
            this.recentFilesService.AddFile(filePath, true);

            PushChangesToFile(filePath);

            if (this.isNewTemplateRequested)
            {
                var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);

                LoadTemplate(this.selectedTemplateName);
                SetTitle(tempDocumentName);

                this.FileNameWithoutExtension = tempDocumentName;

                this.isNewTemplateRequested = false;
                this.selectedTemplateName = string.Empty;
            }
            else
            {
                this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);

                SetTitle(this.FileNameWithoutExtension);
            }

            if (IsBackstageOpened)
                IsBackstageOpened = false;

            if (closeRequested)
                CloseDialog();
        }

        private void OnFileLoaded(string filePath)
        {
            try
            {
                this.TrackChanges = false;

                if (DocumentStatus == IgExcelDocumentStatus.BlankModified || DocumentStatus == IgExcelDocumentStatus.ExistingModified)
                {
                    var result = this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChangesLong, this.FileNameWithoutExtension), MessageBoxButtons.YesNoCancel);

                    if (result == InteractionResult.Yes)
                    {
                        ExecuteSaveDocument();
                        return;
                    }
                    else if (result == InteractionResult.Cancel)
                    {
                        this.IsBackstageOpened = false;
                        return;
                    }
                }

                this.FileName = filePath;
                var extention = Path.GetExtension(filePath);

                switch (extention)
                {
                    case ".xls":
                    case ".xlt":
                    case ".xlsx":
                    case ".xlsm":
                    case ".xltm":
                    case ".xltx":
                        if (Workbook.IsWorkbookEncrypted(filePath))
                        {
                            //The document is encrypted and must be opened with a password.

                            NavigationParameters parameters = new NavigationParameters();
                            parameters.Add(PasswordDialogViewParameters.DialogModeKey, PasswordDialogMode.OpenPassword);
                            parameters.Add(PasswordDialogViewParameters.WorkbookKey, this.workbook);
                            parameters.Add(PasswordDialogViewParameters.FileNameKey, Path.GetFileNameWithoutExtension(filePath));

                            this.dialogService.ShowIgDialog(PasswordDialogViewParameters.ViewName, parameters);
                        }
                        else
                        {
                            UnlockFile();
                            this.Workbook = Workbook.Load(filePath);
                            this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
                            this.IsSpreadSheetVisible = true;
                            this.DocumentStatus = IgExcelDocumentStatus.ExistingNotModified;
                            SetTitle(this.FileNameWithoutExtension);
                            LockFile(this.FileName);
                            this.IsUiPartEnabled = true;
                        }
                        break;
                    default: messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Warning, ResourceStrings.ResourceStrings.Msg_NotSuppportedFileFormat, MessageBoxButtons.Ok); break;
                }
                this.TrackChanges = true;
            }
            catch (Exception ex)
            {
                messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Exception, ex.Message, MessageBoxButtons.Ok);
            }
            
            if (IsBackstageOpened)
                IsBackstageOpened = false;
        }

        private void LockFile(string fn)
        {
            FileInfo lockFileInfo = new FileInfo(fn);
            this.lockFileStream = lockFileInfo.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
        }

        private void UnlockFile()
        {
            if (this.lockFileStream != null)
            {
                this.lockFileStream.Close();
                this.lockFileStream.Dispose();
                this.lockFileStream = null;
            }
        }

        private void OnPasswordToModifyEnteredEvent(WriteProtectedFileMode mode)
        {
            if (mode == WriteProtectedFileMode.ReadOnly)
            {
                //This will prevent the user from saving changes to the existing file.
                this.DocumentStatus = IgExcelDocumentStatus.BlankModified;
            }
            else
            {
                this.DocumentStatus = IgExcelDocumentStatus.ExistingNotModified;
            }

            this.IsSpreadSheetVisible = true;
            this.IsUiPartEnabled = true;

            if (IsBackstageOpened)
                IsBackstageOpened = false;
        }

        private void OnPasswordToOpenEnteredEvent(string password)
        {
            try
            {
                UnlockFile();
                this.Workbook = Workbook.Load(this.FileName, new WorkbookLoadOptions(password));

                if (this.Workbook.IsFileWriteProtected)
                {
                    NavigationParameters parameters = new NavigationParameters();
                    parameters.Add(PasswordDialogViewParameters.DialogModeKey, PasswordDialogMode.ModifyPassword);
                    parameters.Add(PasswordDialogViewParameters.WorkbookKey, this.workbook);
                    parameters.Add(PasswordDialogViewParameters.FileNameKey, Path.GetFileNameWithoutExtension(this.fileName));

                    this.dialogService.ShowIgDialog(PasswordDialogViewParameters.ViewName, parameters);
                }
                else
                {
                    this.IsUiPartEnabled = true;
                    this.IsSpreadSheetVisible = true;
                    this.DocumentStatus = IgExcelDocumentStatus.ExistingNotModified;
                }

                this.FileNameWithoutExtension = Path.GetFileNameWithoutExtension(FileName);
                SetTitle(this.FileNameWithoutExtension);
                LockFile(this.FileName);
            }
            catch (InvalidPasswordException)
            {
                this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, ResourceStrings.ResourceStrings.Msg_InvalidPasswordLong, MessageBoxButtons.Ok);
            }
            catch (Exception ex)
            {
                this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, ex.Message, MessageBoxButtons.Ok);
            }
        }

        private void OnNewDocumentEvent(string templateName)
        {
            CreateNewDocument(templateName);
        }

        private void OnTableAddedEvent(WorksheetTable table)
        {
            this.SelectedTable = table;
        }

        private void OnCustomZoomLevelChangedEvent(int zoom)
        {
            this.ZoomLevel = zoom;
        }

        void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBackstageOpened")
            {
                if (this.isBackstageOpened == false)
                    this.isNewTemplateRequested = false;
            }
        }

        #endregion //Events

        #region Private Methods

        private void PushChangesToFile(string filePath)
        {
            try
            {
                var extention = Path.GetExtension(filePath);
                var selctedWorkbookFormat = WorkbookFormatFromFileExtension(extention);

                if (this.workbook.CurrentFormat != selctedWorkbookFormat)
                {
                    this.workbook.SetCurrentFormat(selctedWorkbookFormat);
                }

                UnlockFile();
                this.workbook.Save(filePath);
                LockFile(filePath);
            }
            catch (Exception ex)
            {
                messageBoxService.Show(ResourceStrings.ResourceStrings.Text_Exception, ex.Message, MessageBoxButtons.Ok);
            }
        }

        private void CreateNewDocument(string templateName)
        {
            var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);

            if (DocumentStatus == IgExcelDocumentStatus.NoDocumentLoaded
               || DocumentStatus == IgExcelDocumentStatus.ExistingNotModified
               || DocumentStatus == IgExcelDocumentStatus.BlankNotModified
               || DocumentStatus == IgExcelDocumentStatus.TemplateNotModified)
            {
                LoadTemplate(templateName);
                SetTitle(tempDocumentName);

                if(templateName == "ProjectBudget" && this.Workbook != null)
                {
                    Worksheet sheet = this.Workbook.Worksheets[2];

                    OperatorConditionalFormat format1 = sheet.ConditionalFormats.AddOperatorCondition("C5:C28", Infragistics.Documents.Excel.ConditionalFormatting.FormatConditionOperator.GreaterEqual);
                    format1.SetOperand1(2500);
                    format1.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Red);

                    OperatorConditionalFormat format2 = sheet.ConditionalFormats.AddOperatorCondition("C5:C28", Infragistics.Documents.Excel.ConditionalFormatting.FormatConditionOperator.Less);
                    format2.SetOperand1(2500);
                    format2.CellFormat.Font.ColorInfo = new WorkbookColorInfo(System.Drawing.Color.Green);
                }

                this.IsBackstageOpened = false;
                this.IsSpreadSheetVisible = true;
                this.FileNameWithoutExtension = tempDocumentName;
            }
            else
            {
                var result = this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChanges, tempDocumentName), MessageBoxButtons.YesNoCancel);

                if (result == InteractionResult.Yes)
                {
                    this.isNewTemplateRequested = true;
                    this.selectedTemplateName = templateName;

                    ExecuteSaveDocument();
                }
                else if (result == InteractionResult.No)
                {
                    LoadTemplate(templateName);
                    SetTitle(tempDocumentName);

                    this.FileNameWithoutExtension = tempDocumentName;
                    this.IsBackstageOpened = false;

                }
                else
                {
                    this.IsBackstageOpened = false;
                }
            }

            this.IsUiPartEnabled = true;
        }

        private void LoadTemplate(string templateName)
        {
            if (!string.IsNullOrEmpty(templateName))
            {
                Stream stream = new MemoryStream((byte[])ResourceStrings.ResourceStrings.ResourceManager.GetObject(templateName));
                this.Workbook = Workbook.Load(stream);
                this.DocumentStatus = IgExcelDocumentStatus.TemplateNotModified;
            }
            else
            {
                CreateNewBlankWorkbook();
                this.DocumentStatus = IgExcelDocumentStatus.BlankNotModified;
            }
        }

        private void CreateNewBlankWorkbook()
        {
            this.TrackChanges = false;
            this.Workbook = new Workbook(WorkbookFormat.Excel2007);
            this.ActiveWorksheet = this.Workbook.Worksheets.Add(ResourceStrings.ResourceStrings.Text_Sheet1);
            this.WorkbookDirtied = false;
            this.TrackChanges = true;
        }

        private bool CanCloseDocument(bool askForSaveLocation)
        {
            if (DocumentStatus == IgExcelDocumentStatus.NoDocumentLoaded
                || DocumentStatus == IgExcelDocumentStatus.BlankNotModified
                || DocumentStatus == IgExcelDocumentStatus.TemplateNotModified
                || DocumentStatus == IgExcelDocumentStatus.ExistingNotModified)

                return true;

            InteractionResult msgCloseResult = InteractionResult.Cancel;

            if (DocumentStatus == IgExcelDocumentStatus.BlankModified || DocumentStatus == IgExcelDocumentStatus.ExistingModified)
            {
                var tempDocumentName = string.Format(ResourceStrings.ResourceStrings.Text_TempDocumentName, 1);
                msgCloseResult = this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChanges, tempDocumentName), MessageBoxButtons.YesNoCancel);
            }
            else
            {
                msgCloseResult = this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, string.Format(ResourceStrings.ResourceStrings.Msg_WantToSaveChanges, fileName), MessageBoxButtons.YesNoCancel);
            }

            if (msgCloseResult == InteractionResult.Cancel)
            {
                return false;
            }

            if (msgCloseResult == InteractionResult.No)
            {
                ClearDataProperties();
                return true;
            }

            if (msgCloseResult == InteractionResult.Yes)
            {
                if (askForSaveLocation)
                {
                    string fileNameTemp;
                    var filters = ShellParameters.FileDialogFilter;
                    dialogService.ShowSaveFileDialog(string.Empty, out fileNameTemp, filters);
                    ClearDataProperties();
                    return true;
                }
                else
                {
                    ExecuteSaveDocument();
                    closeRequested = true;
                    return false;
                }
            }

            return true;
        }

        private void ClearDataProperties()
        {
            this.Workbook = null;
            this.ActiveWorksheet = null;
        }

        private void MarkDocumentAsModified()
        {
            if (this.isSpreadSheetVisible && this.workbookDirtied && this.trackChanges)
            {
                if (documentStatus != IgExcelDocumentStatus.BlankModified && documentStatus != IgExcelDocumentStatus.ExistingModified)
                {
                    if (documentStatus == IgExcelDocumentStatus.BlankNotModified || documentStatus == IgExcelDocumentStatus.TemplateNotModified)
                        DocumentStatus = IgExcelDocumentStatus.BlankModified;
                    else
                        DocumentStatus = IgExcelDocumentStatus.ExistingModified;

                    UpdateTitle(true);
                }
            }
        }

        private void UpdateTitle(bool addStart)
        {
            var findText = " - " + ResourceStrings.ResourceStrings.Text_ApplicationTitle;

            if (addStart)
            {
                Title = Title.Replace(findText, "*" + findText);
            }
            else
            {
                Title = Title.Replace("*" + findText, findText);
            }
        }

        private void SetTitle(string title = null)
        {
            if (string.IsNullOrEmpty(title))
            {
                this.Title = ResourceStrings.ResourceStrings.Text_ApplicationTitle;
            }
            else
            {
                this.Title = title + " - " + ResourceStrings.ResourceStrings.Text_ApplicationTitle;
            }
        }

        private WorkbookFormat WorkbookFormatFromFileExtension(string extension)
        {
            switch (extension)
            {
                case ".xls": return WorkbookFormat.Excel97To2003;
                case ".xlt": return WorkbookFormat.Excel97To2003Template;
                case ".xlsx": return WorkbookFormat.Excel2007;
                case ".xlsm": return WorkbookFormat.Excel2007MacroEnabled;
                case ".xltm": return WorkbookFormat.Excel2007MacroEnabledTemplate;
                case ".xltx": return WorkbookFormat.Excel2007Template;

                default: return WorkbookFormat.Excel2007;
            }
        }

        private void InvokeIGCommand(string targetName, CommandBase cmd)
        {
            Collection<ICommandTarget> targets = CommandSourceManager.GetTargets(cmd);
            foreach (ICommandTarget target in targets)
            {
                if (target is FrameworkElement)
                    if (((FrameworkElement)target).Name.Equals(targetName))
                        cmd.Execute(target);
            }
        }

        #endregion //Private Methods

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            if (closeRequested)
            {
                closeRequested = false;
                return true;
            }

            return CanCloseDocument(false);
        }

        public event Action RequestClose;

        void CloseDialog()
        {
            if (RequestClose != null)
            {
                closeRequested = true;
                RequestClose();
            }
        }

        #endregion //IDialogAware Members
    }
}
