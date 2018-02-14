using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace IgExcel.Dialogs
{
    public enum DialogCellSelectionMode
    {
        NotInCellSelection,
        FromThird,
        FromFourth,
    }

    public class DataValidationDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Private Variables

        private IEventAggregator eventAggragator;
        private IMessageBoxService messageBoxService;
        private Worksheet activeWorksheet;
        private SpreadsheetSelection selection;
        private WorksheetReferenceCollection SelectionWithTheSameRule = null;
        private WorksheetReferenceCollection SelectionCollectionAtOpeningTime = null;
        private WorksheetCell SelectionActiveAtOpeningTime = null;

        #endregion //Private Variables

        #region Public Properties

        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }
        public DelegateCommand ClearAllCommand { get; private set; }

        private int _dialogHeight = 300;
        public int DialogHeight
        {
            get { return this._dialogHeight; }
            set
            {
                this._dialogHeight = value;
                OnPropertyChanged();
            }
        }

        private DialogCellSelectionMode _dialogInCellSelectionMode = DialogCellSelectionMode.NotInCellSelection;
        public DialogCellSelectionMode DialogInCellSelectionMode
        {
            get { return this._dialogInCellSelectionMode; }
            set
            {
                if (this._dialogInCellSelectionMode == value) return;

                if (value == DialogCellSelectionMode.NotInCellSelection)
                {
                    switch (this._dialogInCellSelectionMode)
                    {
                        case DialogCellSelectionMode.FromThird:
                            this.ThirdInputValue = this.CellSelectionTBValue;
                            break;
                        case DialogCellSelectionMode.FromFourth:
                            this.FourthInputValue = this.CellSelectionTBValue;
                            break;
                    }
                    this.DialogHeight = 300;
                    this.selection.PropertyChanged -= PropertyChangedEventHandler;
                    ((INotifyCollectionChanged)this.selection.CellRanges).CollectionChanged -= CollectionChangedEventHandler;
                }

                this._dialogInCellSelectionMode = value;
                OnPropertyChanged();

                if (value == DialogCellSelectionMode.FromThird || value == DialogCellSelectionMode.FromFourth)
                {
                    switch (value)
                    {
                        case DialogCellSelectionMode.FromThird:
                            this.CellSelectionTBValue = this.ThirdInputValue;
                            this.DialogHeight = 40;
                            break;
                        case DialogCellSelectionMode.FromFourth:
                            this.CellSelectionTBValue = this.FourthInputValue;
                            this.DialogHeight = 40;
                            break;
                    }
                    this.selection.PropertyChanged += PropertyChangedEventHandler;
                    ((INotifyCollectionChanged)this.selection.CellRanges).CollectionChanged += CollectionChangedEventHandler;
                }
            }
        }

        private int _currentTab = 0;
        public int CurrentTab
        {
            get { return this._currentTab; }
            set
            {
                int oldTab = this._currentTab;
                this._currentTab = value;
                OnPropertyChanged();
                if (oldTab == 0 && value != 0)
                {
                    string verifyError = this.VerifyDialogData();
                    if (verifyError != null)
                    {
                        this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, verifyError, MessageBoxButtons.Ok);
                        Dispatcher.CurrentDispatcher.BeginInvoke(
                            new Action(() => SwitchToFirstTab()
                        ));
                    }
                }
            }
        }



        // For the first tab (Input Message)

        private bool _cbIgnoreBlank = true;
        public bool cbIgnoreBlank
        {
            get { return this._cbIgnoreBlank; }
            set { this._cbIgnoreBlank = value; OnPropertyChanged(); }
        }

        private bool _cbIgnoreBlankEnabled;
        public bool cbIgnoreBlankEnabled
        {
            get { return this._cbIgnoreBlankEnabled; }
            private set { this._cbIgnoreBlankEnabled = value; OnPropertyChanged(); }
        }

        private bool _cbInCellDropdown = true;
        public bool cbInCellDropdown
        {
            get { return this._cbInCellDropdown; }
            set { this._cbInCellDropdown = value; OnPropertyChanged(); }
        }

        private bool _cbInCellDropdownVisible;
        public bool cbInCellDropdownVisible
        {
            get { return this._cbInCellDropdownVisible; }
            private set { this._cbInCellDropdownVisible = value; OnPropertyChanged(); }
        }

        private bool _cbApplyToAllOtherCells;
        public bool cbApplyToAllOtherCells
        {
            get { return this._cbApplyToAllOtherCells; }
            set
            {
                this._cbApplyToAllOtherCells = value;
                if (value)
                {
                    this.selection.ClearCellRanges();
                    SetSelectionFromWorksheetReferenceCollection(
                        this.SelectionWithTheSameRule, this.SelectionActiveAtOpeningTime);
                }
                else
                {
                    this.selection.ClearCellRanges();
                    SetSelectionFromWorksheetReferenceCollection(
                        this.SelectionCollectionAtOpeningTime, this.SelectionActiveAtOpeningTime);
                }
                OnPropertyChanged();
            }
        }

        private bool _cbApplyToAllOtherCellsEnabled;
        public bool cbApplyToAllOtherCellsEnabled
        {
            get { return this._cbApplyToAllOtherCellsEnabled; }
            set { this._cbApplyToAllOtherCellsEnabled = value; OnPropertyChanged(); }
        }

        private bool _SecondInputLabelEnabled;
        public bool SecondInputLabelEnabled
        {
            get { return this._SecondInputLabelEnabled; }
            private set { this._SecondInputLabelEnabled = value; OnPropertyChanged(); }
        }

        private bool _SecondInputEnabled;
        public bool SecondInputEnabled
        {
            get { return this._SecondInputEnabled; }
            private set { this._SecondInputEnabled = value; OnPropertyChanged(); }
        }

        private object _SecondInputValue;
        public object SecondInputValue
        {
            get { return this._SecondInputValue; }
            set { this._SecondInputValue = value; OnPropertyChanged(); }
        }

        private bool _ThirdInputLabelVisible;
        public bool ThirdInputLabelVisible
        {
            get { return this._ThirdInputLabelVisible; }
            private set { this._ThirdInputLabelVisible = value; OnPropertyChanged(); }
        }

        private string _ThirdInputLabelText;
        public string ThirdInputLabelText
        {
            get { return this._ThirdInputLabelText; }
            private set { this._ThirdInputLabelText = value; OnPropertyChanged(); }
        }

        private bool _ThirdInputVisible;
        public bool ThirdInputVisible
        {
            get { return this._ThirdInputVisible; }
            private set { this._ThirdInputVisible = value; OnPropertyChanged(); }
        }

        private string _ThirdInputValue;
        public string ThirdInputValue
        {
            get { return this._ThirdInputValue; }
            set { this._ThirdInputValue = value; OnPropertyChanged(); }
        }

        private bool _FourthInputLabelVisible;
        public bool FourthInputLabelVisible
        {
            get { return this._FourthInputLabelVisible; }
            private set { this._FourthInputLabelVisible = value; OnPropertyChanged(); }
        }

        private string _FourthInputLabelText;
        public string FourthInputLabelText
        {
            get { return this._FourthInputLabelText; }
            private set { this._FourthInputLabelText = value; OnPropertyChanged(); }
        }

        private bool _FourthInputVisible;
        public bool FourthInputVisible
        {
            get { return this._FourthInputVisible; }
            private set { this._FourthInputVisible = value; OnPropertyChanged(); }
        }

        private string _FourthInputValue;
        public string FourthInputValue
        {
            get { return this._FourthInputValue; }
            set { this._FourthInputValue = value; OnPropertyChanged(); }
        }

        private List<CustomComboBoxItem> _DataValidationAllowValues;
        public List<CustomComboBoxItem> DataValidationAllowValues
        {
            get { return this._DataValidationAllowValues; }
            private set { this._DataValidationAllowValues = value; OnPropertyChanged(); }
        }

        private CustomComboBoxItem _DataValidationAllowValue = null;
        public CustomComboBoxItem DataValidationAllowValue
        {
            get { return this._DataValidationAllowValue; }
            set
            {
                this._DataValidationAllowValue = value;
                OnPropertyChanged();
                this.UpdateUIAfterAllowValueChange();
            }
        }

        private List<CustomComboBoxItem> _DataValidationDataConstraints;
        public List<CustomComboBoxItem> DataValidationDataConstraints
        {
            get { return this._DataValidationDataConstraints; }
            private set { this._DataValidationDataConstraints = value; OnPropertyChanged(); }
        }

        private CustomComboBoxItem _DataValidationDataConstraint;
        public CustomComboBoxItem DataValidationDataConstraint
        {
            get { return this._DataValidationDataConstraint; }
            set
            {
                this._DataValidationDataConstraint = value;
                OnPropertyChanged();
                this.UpdateUIAfterAllowValueChange();
            }
        }

        private string _CellSelectionTBValue;
        public string CellSelectionTBValue
        {
            get { return this._CellSelectionTBValue; }
            set
            {
                this._CellSelectionTBValue = value;
                OnPropertyChanged();
            }
        }



        // For the second tab (Input Message)

        private bool _DataValidationInputMessageShow = true;
        public bool DataValidationInputMessageShow
        {
            get { return this._DataValidationInputMessageShow; }
            set { this._DataValidationInputMessageShow = value; OnPropertyChanged(); }
        }

        private string _DataValidationInputMessageTitle = string.Empty;
        public string DataValidationInputMessageTitle
        {
            get { return this._DataValidationInputMessageTitle; }
            set { this._DataValidationInputMessageTitle = value; OnPropertyChanged(); }
        }

        private string _DataValidationInputMessageContent = string.Empty;
        public string DataValidationInputMessageContent
        {
            get { return this._DataValidationInputMessageContent; }
            set { this._DataValidationInputMessageContent = value; OnPropertyChanged(); }
        }



        // For the third tab (Error Alert)

        private bool _DataValidationAlertEnabled = true;
        public bool DataValidationAlertEnabled
        {
            get { return this._DataValidationAlertEnabled; }
            set { this._DataValidationAlertEnabled = value; OnPropertyChanged(); }
        }

        private List<CustomComboBoxItem> _DataValidationAlertStyleTypes;
        public List<CustomComboBoxItem> DataValidationAlertStyleTypes
        {
            get { return this._DataValidationAlertStyleTypes; }
            private set { this._DataValidationAlertStyleTypes = value; OnPropertyChanged(); }
        }

        private CustomComboBoxItem _DataValidationAlertStyleType;
        public CustomComboBoxItem DataValidationAlertStyleType
        {
            get { return this._DataValidationAlertStyleType; }
            set { this._DataValidationAlertStyleType = value; OnPropertyChanged(); }
        }

        private string _DataValidationAlertMessageTitle = string.Empty;
        public string DataValidationAlertMessageTitle
        {
            get { return this._DataValidationAlertMessageTitle; }
            set { this._DataValidationAlertMessageTitle = value; OnPropertyChanged(); }
        }

        private string _DataValidationAlertMessageContent = string.Empty;
        public string DataValidationAlertMessageContent
        {
            get { return this._DataValidationAlertMessageContent; }
            set { this._DataValidationAlertMessageContent = value; OnPropertyChanged(); }
        }

        #endregion //Public Properties

        #region Constructor

        public DataValidationDialogViewModel(IEventAggregator eventAggragator, IMessageBoxService messageBoxService)
        {
            this.eventAggragator = eventAggragator;
            this.messageBoxService = messageBoxService;

            this.Title = ResourceStrings.ResourceStrings.Text_DataValidation;
            this.IconSource = "pack://application:,,,/IgExcel;component/Images/DataValidation_16x16.png";
            this.OkCommand = new DelegateCommand(ExecuteOk);
            this.CancelCommand = new DelegateCommand(ExecuteCancel);
            this.ClearAllCommand = new DelegateCommand(ExecuteClearAll);

            List<CustomComboBoxItem> l1 = new List<CustomComboBoxItem>();
            for (int i = 0; i < 8; i++)
            {
                l1.Add(new CustomComboBoxItem()
                {
                    ItemLabel = ResourceStrings.ResourceStrings.ResourceManager.GetString("Text_DataValidationAllowValue" + i),
                    ItemName = "AllowValue" + i
                });
            }
            DataValidationAllowValues = l1;
            DataValidationAllowValue = DataValidationAllowValues[0];

            List<CustomComboBoxItem> l2 = new List<CustomComboBoxItem>();
            for (int i = 0; i < 8; i++)
            {
                l2.Add(new CustomComboBoxItem()
                {
                    ItemLabel = ResourceStrings.ResourceStrings.ResourceManager.GetString("Text_DataValidationDataConstraint" + i),
                    ItemName = "DataConstraint" + i
                });
            }
            DataValidationDataConstraints = l2;
            DataValidationDataConstraint = DataValidationDataConstraints[0];

            List<CustomComboBoxItem> l3 = new List<CustomComboBoxItem>();
            l3.Add(new CustomComboBoxItem()
            {
                ItemLabel = ResourceStrings.ResourceStrings.Text_Stop,
                ItemName = "AlertStyleType0"
            });
            l3.Add(new CustomComboBoxItem()
            {
                ItemLabel = ResourceStrings.ResourceStrings.Text_Warning,
                ItemName = "AlertStyleType1"
            });
            l3.Add(new CustomComboBoxItem()
            {
                ItemLabel = ResourceStrings.ResourceStrings.Text_Information,
                ItemName = "AlertStyleType2"
            });
            DataValidationAlertStyleTypes = l3;
            DataValidationAlertStyleType = DataValidationAlertStyleTypes[0];
        }

        #endregion //Constructor

        #region Commands

        private void ExecuteOk()
        {
            //this.eventAggragator.GetEvent<CustomZoomLevelChangedEvent>().Publish(this.customZoomLevel);
            string verifyError = this.VerifyDialogData();
            if (verifyError == null)
            {
                if (TransferDialogDataToValidationRule())
                    RequestClose();
            }
            else
            {
                this.messageBoxService.Show(ResourceStrings.ResourceStrings.Text_ApplicationTitle, verifyError, MessageBoxButtons.Ok);
            }
        }

        private void ExecuteCancel()
        {
            RequestClose();
        }

        private void ExecuteClearAll()
        {
            // Reset dialog data

            this.cbIgnoreBlankEnabled = false;
            this.cbIgnoreBlank = true;

            this.cbInCellDropdownVisible = false;
            this.cbInCellDropdown = false;

            this.FourthInputLabelVisible = false;
            this.FourthInputVisible = false;
            this.ThirdInputLabelVisible = false;
            this.ThirdInputVisible = false;

            this.DataValidationDataConstraint = this.DataValidationDataConstraints[0]; // between
            this.DataValidationAllowValue = this.DataValidationAllowValues[0]; // any value

            this.ThirdInputValue = null;
            this.FourthInputValue = null;

            this.DataValidationInputMessageShow = true;
            this.DataValidationInputMessageTitle = string.Empty;
            this.DataValidationInputMessageContent = string.Empty;

            this.DataValidationAlertEnabled = true;
            this.DataValidationAlertMessageTitle = string.Empty;
            this.DataValidationAlertMessageContent = string.Empty;
            this.DataValidationAlertStyleType = this.DataValidationAlertStyleTypes[0]; // stop alert type
        }

        private void SwitchToFirstTab()
        {
            this._currentTab = 0;
            OnPropertyChanged("CurrentTab");
        }

        #endregion //Commands

        #region Events Handlers

        public void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            if (DialogInCellSelectionMode != DialogCellSelectionMode.NotInCellSelection)
            {
                if (e.PropertyName.Equals("ActiveCell"))
                {
                    int index = this.selection.ActiveCellRangeIndex;
                    SpreadsheetCellRange range = this.selection.CellRanges[index];
                    UpdateFormulaContentUsingCellSelection(range, false);
                }
            }
        }

        private void CollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (DialogInCellSelectionMode != DialogCellSelectionMode.NotInCellSelection)
            {
                int index = this.selection.ActiveCellRangeIndex;
                SpreadsheetCellRange range = this.selection.CellRanges[index];
                UpdateFormulaContentUsingCellSelection(range, true);
            }
        }

        private void UpdateFormulaContentUsingCellSelection(SpreadsheetCellRange range, bool replace)
        {
            string theValue = CellSelectionTBValue == null ? string.Empty : CellSelectionTBValue.Trim();
            if (theValue.Length == 0) theValue = "=";
            else
            {
                if (replace)
                {
                    for (int i = theValue.Length - 1; i >= 0; i--)
                    {
                        if (theValue[i] < '0' || theValue[i] == '=')
                        {
                            theValue = theValue.Substring(0, i + 1);
                            break;
                        }
                    }
                }
                else
                {
                    theValue += "+";
                }
            }
            theValue += range.ToString();
            CellSelectionTBValue = theValue;
        }

        #endregion Event Handlers

        #region IDialogAware Members

        public bool CanCloseDialog()
        {
            return DialogInCellSelectionMode == DialogCellSelectionMode.NotInCellSelection;
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
            this.activeWorksheet = (Worksheet)navigationContext.Parameters[DataValidationDialogViewParameters.WorksheetKey];
            this.selection = (SpreadsheetSelection)navigationContext.Parameters[DataValidationDialogViewParameters.SelectionKey];
            this.TransferValidationRuleToDialogData();
        }

        #endregion //INavigationAware

        #region Private Methods

        private void UpdateUIAfterAllowValueChange()
        {
            switch (DataValidationAllowValue.ItemName)
            {
                case "AllowValue0": // Any value
                    cbIgnoreBlankEnabled = false;
                    cbInCellDropdownVisible = false;
                    SecondInputLabelEnabled = false;
                    SecondInputEnabled = false;
                    ThirdInputLabelVisible = false;
                    ThirdInputVisible = false;
                    FourthInputLabelVisible = false;
                    FourthInputVisible = false;
                    break;
                case "AllowValue1": // Whole number
                case "AllowValue2": // Decimal
                case "AllowValue4": // Date
                case "AllowValue5": // Time
                case "AllowValue6": // Text length
                    cbIgnoreBlankEnabled = true;
                    cbInCellDropdownVisible = false;
                    SecondInputLabelEnabled = true;
                    SecondInputEnabled = true;
                    this._UpdateUIAfterConstraintChange();
                    break;
                case "AllowValue3": // List
                    cbIgnoreBlankEnabled = true;
                    cbInCellDropdownVisible = false; // will be true when implemented in the xamSpreadsheet
                    SecondInputLabelEnabled = false;
                    SecondInputEnabled = false;
                    ThirdInputLabelVisible = true;
                    ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Source;
                    ThirdInputVisible = true;
                    FourthInputLabelVisible = false;
                    FourthInputVisible = false;
                    break;
                case "AllowValue7": // Custom
                    cbIgnoreBlankEnabled = true;
                    cbInCellDropdownVisible = false;
                    SecondInputLabelEnabled = false;
                    SecondInputEnabled = false;
                    ThirdInputLabelVisible = true;
                    ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Formula;
                    ThirdInputVisible = true;
                    FourthInputLabelVisible = false;
                    FourthInputVisible = false;
                    break;
            }
        }

        private void _UpdateUIAfterConstraintChange()
        {
            ThirdInputLabelVisible = true;
            ThirdInputVisible = true;
            switch (DataValidationDataConstraint.ItemName)
            {
                case "DataConstraint0": // between
                case "DataConstraint1": // not between
                    FourthInputLabelVisible = true;
                    FourthInputVisible = true;
                    switch (DataValidationAllowValue.ItemName)
                    {
                        case "AllowValue1": // Whole number
                        case "AllowValue2": // Decimal
                        case "AllowValue6": // Text length
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Minimum;
                            FourthInputLabelText = ResourceStrings.ResourceStrings.Lbl_Maximum;
                            break;
                        case "AllowValue4": // Date
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_StartDate;
                            FourthInputLabelText = ResourceStrings.ResourceStrings.Lbl_EndDate;
                            break;
                        case "AllowValue5": // Time
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_StartTime;
                            FourthInputLabelText = ResourceStrings.ResourceStrings.Lbl_EndTime;
                            break;
                    }
                    break;
                case "DataConstraint2": // equal to
                case "DataConstraint3": // not equal to
                    FourthInputLabelVisible = false;
                    FourthInputVisible = false;
                    switch (DataValidationAllowValue.ItemName)
                    {
                        case "AllowValue1": // Whole number
                        case "AllowValue2": // Decimal
                        case "AllowValue6": // Text length
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Value;
                            break;
                        case "AllowValue4": // Date
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Date;
                            break;
                        case "AllowValue5": // Time
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Time;
                            break;
                    }
                    break;
                case "DataConstraint4": // greater than
                case "DataConstraint6": // greater than or equal to
                    FourthInputLabelVisible = false;
                    FourthInputVisible = false;
                    switch (DataValidationAllowValue.ItemName)
                    {
                        case "AllowValue1": // Whole number
                        case "AllowValue2": // Decimal
                        case "AllowValue6": // Text length
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Minimum;
                            break;
                        case "AllowValue4": // Date
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_StartDate;
                            break;
                        case "AllowValue5": // Time
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_StartTime;
                            break;
                    }
                    break;
                case "DataConstraint5": // less than
                case "DataConstraint7": // less than or equal to
                    FourthInputLabelVisible = false;
                    FourthInputVisible = false;
                    switch (DataValidationAllowValue.ItemName)
                    {
                        case "AllowValue1": // Whole number
                        case "AllowValue2": // Decimal
                        case "AllowValue6": // Text length
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_Maximum;
                            break;
                        case "AllowValue4": // Date
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_EndDate;
                            break;
                        case "AllowValue5": // Time
                            ThirdInputLabelText = ResourceStrings.ResourceStrings.Lbl_EndTime;
                            break;
                    }
                    break;
            }
        }

        private bool SetOneConstraintValue(OneConstraintDataValidationRule rule, string dataValidationAllowValue, string constraintValue)
        {
            switch (dataValidationAllowValue)
            {
                case "AllowValue1": // Whole number
                    rule.ValidationCriteria = DataValidationCriteria.WholeNumber;
                    if (constraintValue[0] == '=')
                        rule.SetConstraintFormula(constraintValue, null);
                    else
                        rule.SetConstraint(double.Parse(constraintValue));
                    break;
                case "AllowValue2": // Decimal
                    rule.ValidationCriteria = DataValidationCriteria.Decimal;
                    if (constraintValue[0] == '=')
                        rule.SetConstraintFormula(constraintValue, null);
                    else
                        rule.SetConstraint(double.Parse(constraintValue));
                    break;
                case "AllowValue4": // Date
                    rule.ValidationCriteria = DataValidationCriteria.Date;
                    DateTime date = new DateTime();
                    DateTime.TryParse(constraintValue, out date);
                    try { rule.SetConstraint(date); }
                    catch (Exception)
                    {
                        this.messageBoxService.Show(
                            ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                            string.Format(ResourceStrings.ResourceStrings.Msg_DateIsInvalid, this.ThirdInputLabelText),
                            MessageBoxButtons.Ok);
                        return false;
                    }
                    break;
                case "AllowValue5": // Time
                    rule.ValidationCriteria = DataValidationCriteria.Time;
                    TimeSpan time = new TimeSpan();
                    TimeSpan.TryParse(constraintValue, out time);
                    try { rule.SetConstraint(time); }
                    catch (Exception)
                    {
                        this.messageBoxService.Show(
                            ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                            string.Format(ResourceStrings.ResourceStrings.Msg_TimeIsInvalid, this.ThirdInputLabelText),
                            MessageBoxButtons.Ok);
                        return false;
                    }
                    break;
                case "AllowValue6": // Text length
                    rule.ValidationCriteria = DataValidationCriteria.TextLength;
                    rule.SetConstraint(double.Parse(constraintValue));
                    break;
            }
            return true;
        }

        private bool SetTwoConstraintValues(TwoConstraintDataValidationRule rule, string dataValidationAllowValue, string constraintLowerValue, string constraintUpperValue)
        {
            switch (dataValidationAllowValue)
            {
                case "AllowValue1": // Whole number
                case "AllowValue2": // Decimal
                case "AllowValue6": // Text length
                    if (dataValidationAllowValue == "AllowValue1")
                        rule.ValidationCriteria = DataValidationCriteria.WholeNumber;
                    if (dataValidationAllowValue == "AllowValue2")
                        rule.ValidationCriteria = DataValidationCriteria.Decimal;
                    if (dataValidationAllowValue == "AllowValue6")
                        rule.ValidationCriteria = DataValidationCriteria.TextLength;
                    if (constraintLowerValue[0] == '=')
                        rule.SetLowerConstraintFormula(constraintLowerValue, null);
                    else
                        rule.SetLowerConstraint(double.Parse(constraintLowerValue));
                    if (constraintUpperValue[0] == '=')
                        rule.SetUpperConstraintFormula(constraintUpperValue, null);
                    else
                        rule.SetUpperConstraint(double.Parse(constraintUpperValue));
                    break;

                case "AllowValue4": // Date
                    rule.ValidationCriteria = DataValidationCriteria.Date;
                    DateTime dateTimeLower = new DateTime();
                    DateTime.TryParse(constraintLowerValue, out dateTimeLower);
                    try { rule.SetLowerConstraint(dateTimeLower); }
                    catch (Exception)
                    {
                        this.messageBoxService.Show(
                            ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                            string.Format(ResourceStrings.ResourceStrings.Msg_DateIsInvalid, this.ThirdInputLabelText),
                            MessageBoxButtons.Ok);
                        return false;
                    }
                    DateTime dateTimeUpper = new DateTime();
                    DateTime.TryParse(constraintUpperValue, out dateTimeUpper);
                    try { rule.SetUpperConstraint(dateTimeUpper); }
                    catch (Exception)
                    {
                        this.messageBoxService.Show(
                            ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                            string.Format(ResourceStrings.ResourceStrings.Msg_DateIsInvalid, this.FourthInputLabelText),
                            MessageBoxButtons.Ok);
                        return false;
                    }
                    break;

                case "AllowValue5": // Time
                    rule.ValidationCriteria = DataValidationCriteria.Time;
                    TimeSpan timeSpanLower = new TimeSpan();
                    TimeSpan.TryParse(constraintLowerValue, out timeSpanLower);
                    try { rule.SetLowerConstraint(timeSpanLower); }
                    catch (Exception)
                    {
                        this.messageBoxService.Show(
                            ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                            string.Format(ResourceStrings.ResourceStrings.Msg_TimeIsInvalid, this.ThirdInputLabelText),
                            MessageBoxButtons.Ok);
                        return false;
                    }
                    TimeSpan timeSpanUpper = new TimeSpan();
                    TimeSpan.TryParse(constraintUpperValue, out timeSpanUpper);
                    try { rule.SetUpperConstraint(timeSpanUpper); }
                    catch (Exception)
                    {
                        this.messageBoxService.Show(
                            ResourceStrings.ResourceStrings.Text_ApplicationTitle,
                            string.Format(ResourceStrings.ResourceStrings.Msg_TimeIsInvalid, this.FourthInputLabelText),
                            MessageBoxButtons.Ok);
                        return false;
                    }
                    break;
            }
            return true;
        }

        private void SetRuleInputMessage(DataValidationRule rule)
        {
            if (rule is LimitedValueDataValidationRule)
            {
                ((LimitedValueDataValidationRule)rule).AllowNull = cbIgnoreBlank;
            }
            rule.ShowInputMessage = DataValidationInputMessageShow;
            rule.InputMessageTitle = DataValidationInputMessageTitle;
            rule.InputMessageDescription = DataValidationInputMessageContent;
        }

        private void SetRuleErrorAlert(DataValidationRule rule)
        {
            rule.ShowErrorMessageForInvalidValue = DataValidationAlertEnabled;
            switch (DataValidationAlertStyleType.ItemName)
            {
                case "AlertStyleType0":
                    rule.ErrorStyle = DataValidationErrorStyle.Stop;
                    break;
                case "AlertStyleType1":
                    rule.ErrorStyle = DataValidationErrorStyle.Warning;
                    break;
                case "AlertStyleType2":
                    rule.ErrorStyle = DataValidationErrorStyle.Information;
                    break;
            }
            rule.ErrorMessageTitle = DataValidationAlertMessageTitle;
            rule.ErrorMessageDescription = DataValidationAlertMessageContent;
        }

        private DataValidationRule AggregateDataValidationRuleFromDialog()
        {
            DataValidationRule dvr = null;

            switch (DataValidationAllowValue.ItemName)
            {
                case "AllowValue0": // Any value
                    AnyValueDataValidationRule anyValueRule = new AnyValueDataValidationRule();
                    SetRuleInputMessage(anyValueRule);
                    SetRuleErrorAlert(anyValueRule);
                    dvr = anyValueRule;
                    break;
                case "AllowValue1": // Whole number
                case "AllowValue2": // Decimal
                case "AllowValue4": // Date
                case "AllowValue5": // Time
                case "AllowValue6": // Text length
                    switch (DataValidationDataConstraint.ItemName)
                    {
                        case "DataConstraint0": // between
                            TwoConstraintDataValidationRule twoConstraintRule0 = new TwoConstraintDataValidationRule();
                            twoConstraintRule0.ValidationOperator = TwoConstraintDataValidationOperator.Between;
                            if (!SetTwoConstraintValues(
                                twoConstraintRule0,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString(),
                                FourthInputValue.ToString())) return null;
                            SetRuleInputMessage(twoConstraintRule0);
                            SetRuleErrorAlert(twoConstraintRule0);
                            dvr = twoConstraintRule0;
                            break;
                        case "DataConstraint1": // not between
                            TwoConstraintDataValidationRule twoConstraintRule1 = new TwoConstraintDataValidationRule();
                            twoConstraintRule1.ValidationOperator = TwoConstraintDataValidationOperator.NotBetween;
                            if (!SetTwoConstraintValues(
                                twoConstraintRule1,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString(),
                                FourthInputValue.ToString())) return null;
                            SetRuleInputMessage(twoConstraintRule1);
                            SetRuleErrorAlert(twoConstraintRule1);
                            dvr = twoConstraintRule1;
                            break;
                        case "DataConstraint2": // equal to
                            OneConstraintDataValidationRule oneConstraintRule0 = new OneConstraintDataValidationRule();
                            oneConstraintRule0.ValidationOperator = OneConstraintDataValidationOperator.EqualTo;
                            if (!SetOneConstraintValue(
                                oneConstraintRule0,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString())) return null;
                            SetRuleInputMessage(oneConstraintRule0);
                            SetRuleErrorAlert(oneConstraintRule0);
                            dvr = oneConstraintRule0;
                            break;
                        case "DataConstraint3": // not equal to
                            OneConstraintDataValidationRule oneConstraintRule1 = new OneConstraintDataValidationRule();
                            oneConstraintRule1.ValidationOperator = OneConstraintDataValidationOperator.NotEqualTo;
                            if (!SetOneConstraintValue(
                                oneConstraintRule1,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString())) return null;
                            SetRuleInputMessage(oneConstraintRule1);
                            SetRuleErrorAlert(oneConstraintRule1);
                            dvr = oneConstraintRule1;
                            break;
                        case "DataConstraint4": // greater than
                            OneConstraintDataValidationRule oneConstraintRule2 = new OneConstraintDataValidationRule();
                            oneConstraintRule2.ValidationOperator = OneConstraintDataValidationOperator.GreaterThan;
                            if (!SetOneConstraintValue(
                                oneConstraintRule2,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString())) return null;
                            SetRuleInputMessage(oneConstraintRule2);
                            SetRuleErrorAlert(oneConstraintRule2);
                            dvr = oneConstraintRule2;
                            break;
                        case "DataConstraint6": // greater than or equal to
                            OneConstraintDataValidationRule oneConstraintRule3 = new OneConstraintDataValidationRule();
                            oneConstraintRule3.ValidationOperator = OneConstraintDataValidationOperator.GreaterThanOrEqualTo;
                            if (!SetOneConstraintValue(
                                oneConstraintRule3,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString())) return null;
                            SetRuleInputMessage(oneConstraintRule3);
                            SetRuleErrorAlert(oneConstraintRule3);
                            dvr = oneConstraintRule3;
                            break;
                        case "DataConstraint5": // less than
                            OneConstraintDataValidationRule oneConstraintRule4 = new OneConstraintDataValidationRule();
                            oneConstraintRule4.ValidationOperator = OneConstraintDataValidationOperator.LessThan;
                            if (!SetOneConstraintValue(
                                oneConstraintRule4,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString())) return null;
                            SetRuleInputMessage(oneConstraintRule4);
                            SetRuleErrorAlert(oneConstraintRule4);
                            dvr = oneConstraintRule4;
                            break;
                        case "DataConstraint7": // less than or equal to
                            OneConstraintDataValidationRule oneConstraintRule5 = new OneConstraintDataValidationRule();
                            oneConstraintRule5.ValidationOperator = OneConstraintDataValidationOperator.LessThanOrEqualTo;
                            if (!SetOneConstraintValue(
                                oneConstraintRule5,
                                DataValidationAllowValue.ItemName,
                                ThirdInputValue.ToString())) return null;
                            SetRuleInputMessage(oneConstraintRule5);
                            SetRuleErrorAlert(oneConstraintRule5);
                            dvr = oneConstraintRule5;
                            break;
                    }
                    break;
                case "AllowValue3": // List
                    ListDataValidationRule listRule = new ListDataValidationRule();
                    char separator = ',';
                    if (Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == ",") separator = ';';
                    string[] tokens = ThirdInputValue.ToString().Split(separator);
                    object[] values = new object[tokens.Length];
                    for (int i = 0; i < tokens.Length; i++)
                    {
                        string s = tokens[i].Trim();
                        try
                        {
                            double d = double.Parse(s.Trim());
                            values[i] = d;
                        }
                        catch (Exception) { values[i] = s; }
                    }
                    listRule.SetValues(values);
                    SetRuleInputMessage(listRule);
                    SetRuleErrorAlert(listRule);
                    dvr = listRule;
                    break;
                case "AllowValue7": // Custom
                    CustomDataValidationRule customRule = new CustomDataValidationRule();
                    string formula = ThirdInputValue.ToString().Trim();
                    customRule.SetFormula(formula, null);
                    SetRuleInputMessage(customRule);
                    SetRuleErrorAlert(customRule);
                    dvr = customRule;
                    break;
            }

            return dvr;
        }

        public bool TransferDialogDataToValidationRule()
        {
            DataValidationRule newRule = this.AggregateDataValidationRuleFromDialog();
            if (newRule == null) return false;
            if (this.cbApplyToAllOtherCellsEnabled && this.cbApplyToAllOtherCells)
            {
                if (this.SelectionWithTheSameRule.CellsCount > 0)
                {
                    this.activeWorksheet.DataValidationRules.Remove(this.SelectionWithTheSameRule);
                    this.activeWorksheet.DataValidationRules.Add(newRule, this.SelectionWithTheSameRule, true);
                }
            }
            else
            {
                if (this.SelectionCollectionAtOpeningTime.CellsCount > 0)
                {
                    this.activeWorksheet.DataValidationRules.Remove(this.SelectionCollectionAtOpeningTime);
                    this.activeWorksheet.DataValidationRules.Add(newRule, this.SelectionCollectionAtOpeningTime, true);
                }
            }
            return true;
        }

        private void GetOneConstraintValue(OneConstraintDataValidationRule rule, WorksheetCell cell)
        {
            string cellAddress = WorksheetCell.GetCellAddressString(cell.Worksheet.Rows[cell.RowIndex], cell.ColumnIndex,
                CellReferenceMode.A1, false);
            switch (DataValidationAllowValue.ItemName)
            {
                case "AllowValue1": // Whole number
                case "AllowValue2": // Decimal
                case "AllowValue6": // Text length
                    double dd;
                    if (rule.TryGetConstraint(out dd))
                        ThirdInputValue = dd.ToString();
                    else
                        ThirdInputValue = rule.GetConstraintFormula(cellAddress);
                    break;
                case "AllowValue4": // Date
                    DateTime dateTime;
                    if (rule.TryGetConstraint(out dateTime))
                        ThirdInputValue = dateTime.ToString();
                    else
                        ThirdInputValue = rule.GetConstraintFormula(cellAddress);
                    break;
                case "AllowValue5": // Time
                    TimeSpan timeSpan;
                    if (rule.TryGetConstraint(out timeSpan))
                        ThirdInputValue = timeSpan.ToString();
                    else
                        ThirdInputValue = rule.GetConstraintFormula(cellAddress);
                    break;
            }
        }

        private void GetTwoConstraintValues(TwoConstraintDataValidationRule rule, WorksheetCell cell)
        {
            string cellAddress = WorksheetCell.GetCellAddressString(cell.Worksheet.Rows[cell.RowIndex], cell.ColumnIndex,
                CellReferenceMode.A1, false);
            switch (DataValidationAllowValue.ItemName)
            {
                case "AllowValue1": // Whole number
                case "AllowValue2": // Decimal
                case "AllowValue6": // Text length
                    double dd;
                    if (rule.TryGetLowerConstraint(out dd))
                        ThirdInputValue = dd.ToString();
                    else
                        ThirdInputValue = rule.GetLowerConstraintFormula(cellAddress);
                    if (rule.TryGetUpperConstraint(out dd))
                        FourthInputValue = dd.ToString();
                    else
                        FourthInputValue = rule.GetUpperConstraintFormula(cellAddress);
                    break;

                case "AllowValue4": // Date
                    DateTime dateTime;
                    string DateShortPattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
                    if (rule.TryGetLowerConstraint(out dateTime))
                        ThirdInputValue = dateTime.ToString(DateShortPattern);
                    else
                        ThirdInputValue = rule.GetLowerConstraintFormula(cellAddress);
                    if (rule.TryGetUpperConstraint(out dateTime))
                        FourthInputValue = dateTime.ToString(DateShortPattern);
                    else
                        FourthInputValue = rule.GetUpperConstraintFormula(cellAddress);
                    break;

                case "AllowValue5": // Time
                    TimeSpan timeSpan;
                    string TimeShortPattern = Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortTimePattern;
                    if (rule.TryGetLowerConstraint(out timeSpan))
                        ThirdInputValue = timeSpan.ToString(TimeShortPattern);
                    else
                        ThirdInputValue = rule.GetLowerConstraintFormula(cellAddress);
                    if (rule.TryGetUpperConstraint(out timeSpan))
                        FourthInputValue = timeSpan.ToString(TimeShortPattern);
                    else
                        FourthInputValue = rule.GetUpperConstraintFormula(cellAddress);
                    break;
            }
        }

        private void ApplyRuleValudationOperatorToDialog(ValueConstraintDataValidationRule rule, WorksheetCell cell)
        {
            if (rule is OneConstraintDataValidationRule)
            {
                OneConstraintDataValidationRule rule1 = (OneConstraintDataValidationRule)rule;
                switch (rule1.ValidationOperator)
                {
                    case OneConstraintDataValidationOperator.EqualTo:
                        DataValidationDataConstraint = DataValidationDataConstraints[2];
                        GetOneConstraintValue(rule1, cell);
                        break;
                    case OneConstraintDataValidationOperator.NotEqualTo:
                        DataValidationDataConstraint = DataValidationDataConstraints[3];
                        GetOneConstraintValue(rule1, cell);
                        break;
                    case OneConstraintDataValidationOperator.GreaterThan:
                        DataValidationDataConstraint = DataValidationDataConstraints[4];
                        GetOneConstraintValue(rule1, cell);
                        break;
                    case OneConstraintDataValidationOperator.GreaterThanOrEqualTo:
                        DataValidationDataConstraint = DataValidationDataConstraints[6];
                        GetOneConstraintValue(rule1, cell);
                        break;
                    case OneConstraintDataValidationOperator.LessThan:
                        DataValidationDataConstraint = DataValidationDataConstraints[5];
                        GetOneConstraintValue(rule1, cell);
                        break;
                    case OneConstraintDataValidationOperator.LessThanOrEqualTo:
                        DataValidationDataConstraint = DataValidationDataConstraints[7];
                        GetOneConstraintValue(rule1, cell);
                        break;
                }

            }
            else if (rule is TwoConstraintDataValidationRule)
            {
                TwoConstraintDataValidationRule rule2 = (TwoConstraintDataValidationRule)rule;
                switch (rule2.ValidationOperator)
                {
                    case TwoConstraintDataValidationOperator.Between:
                        DataValidationDataConstraint = DataValidationDataConstraints[0];
                        GetTwoConstraintValues(rule2, cell);
                        break;
                    case TwoConstraintDataValidationOperator.NotBetween:
                        DataValidationDataConstraint = DataValidationDataConstraints[1];
                        GetTwoConstraintValues(rule2, cell);
                        break;
                }
            }
        }

        public void TransferValidationRuleToDialogData()
        {
            this.cbApplyToAllOtherCellsEnabled = false;
            this.SelectionWithTheSameRule = null;

            // obtain active cell and its validation rule
            this.SelectionActiveAtOpeningTime =
                this.activeWorksheet.Rows[this.selection.ActiveCell.Row].Cells[this.selection.ActiveCell.Column];
            DataValidationRule rule = this.SelectionActiveAtOpeningTime.DataValidationRule;

            // obtain current selection
            this.SelectionCollectionAtOpeningTime = new WorksheetReferenceCollection(this.activeWorksheet);
            foreach (SpreadsheetCellRange range in this.selection.CellRanges)
            {
                this.SelectionCollectionAtOpeningTime.Add(
                    new WorksheetRegion(this.activeWorksheet, range.FirstRow, range.FirstColumn, range.LastRow, range.LastColumn));
            }

            if (rule != null)
            {
                // obtain a WorksheetReferenceCollection which contains all cells and regions associated with the same data validation rule
                this.SelectionWithTheSameRule = this.activeWorksheet.DataValidationRules.GetAllReferences(rule);

                // check if the selection of cells at dialog opening time contains a cell (or region)
                // which is not included in the WorksheetReferenceCollection created in the previous step
                // if this is the case enable the "Apply to all other cells..." checkbox
                IEnumerator iEnum2 = this.SelectionWithTheSameRule.GetEnumerator();
                while (iEnum2.MoveNext())
                {
                    if (iEnum2.Current is WorksheetCell)
                    {
                        if (!this.SelectionCollectionAtOpeningTime.Contains((WorksheetCell)iEnum2.Current))
                        {
                            this.cbApplyToAllOtherCellsEnabled = true;
                            break;
                        }
                    }
                    else if (iEnum2.Current is WorksheetRegion)
                    {
                        if (!this.SelectionCollectionAtOpeningTime.Contains((WorksheetRegion)iEnum2.Current))
                        {
                            this.cbApplyToAllOtherCellsEnabled = true;
                            break;
                        }
                    }
                }

                // "Settings" tab
                if (rule is LimitedValueDataValidationRule)
                {
                    LimitedValueDataValidationRule limRule = (LimitedValueDataValidationRule)rule;
                    cbIgnoreBlank = limRule.AllowNull;
                }
                if (rule is ListDataValidationRule)
                {
                    ListDataValidationRule listRule = (ListDataValidationRule)rule;
                    cbInCellDropdown = listRule.ShowDropdown;
                }
                if (rule is AnyValueDataValidationRule)
                {
                    DataValidationAllowValue = DataValidationAllowValues[0]; // Any value
                }
                else if (rule is ValueConstraintDataValidationRule)
                {
                    switch (((ValueConstraintDataValidationRule)rule).ValidationCriteria)
                    {
                        case DataValidationCriteria.WholeNumber:
                            DataValidationAllowValue = DataValidationAllowValues[1];
                            ApplyRuleValudationOperatorToDialog((ValueConstraintDataValidationRule)rule, SelectionActiveAtOpeningTime);
                            break;
                        case DataValidationCriteria.Decimal:
                            DataValidationAllowValue = DataValidationAllowValues[2];
                            ApplyRuleValudationOperatorToDialog((ValueConstraintDataValidationRule)rule, SelectionActiveAtOpeningTime);
                            break;
                        case DataValidationCriteria.Date:
                            DataValidationAllowValue = DataValidationAllowValues[4];
                            ApplyRuleValudationOperatorToDialog((ValueConstraintDataValidationRule)rule, SelectionActiveAtOpeningTime);
                            break;
                        case DataValidationCriteria.Time:
                            DataValidationAllowValue = DataValidationAllowValues[5];
                            ApplyRuleValudationOperatorToDialog((ValueConstraintDataValidationRule)rule, SelectionActiveAtOpeningTime);
                            break;
                        case DataValidationCriteria.TextLength:
                            DataValidationAllowValue = DataValidationAllowValues[6];
                            ApplyRuleValudationOperatorToDialog((ValueConstraintDataValidationRule)rule, SelectionActiveAtOpeningTime);
                            break;
                    }
                }
                else if (rule is ListDataValidationRule)
                {
                    DataValidationAllowValue = DataValidationAllowValues[3]; // List
                    object[] values = null;
                    if (((ListDataValidationRule)rule).TryGetValues(out values))
                    {
                        if (values != null)
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (object o in values) sb.Append(o.ToString()).Append(',');
                            sb.Remove(sb.Length - 1, 1);
                            ThirdInputValue = sb.ToString();
                        }
                    }
                }
                else if (rule is CustomDataValidationRule)
                {
                    DataValidationAllowValue = DataValidationAllowValues[7]; // Custom
                    ThirdInputValue = ((CustomDataValidationRule)rule).GetFormula(null);
                }

                // "Input Message" tab
                DataValidationInputMessageShow = rule.ShowInputMessage;
                DataValidationInputMessageTitle = rule.InputMessageTitle;
                DataValidationInputMessageContent = rule.InputMessageDescription;

                // "Error Alert" tab
                DataValidationAlertEnabled = rule.ShowErrorMessageForInvalidValue;
                switch (rule.ErrorStyle)
                {
                    case DataValidationErrorStyle.Stop:
                        DataValidationAlertStyleType = DataValidationAlertStyleTypes[0];
                        break;
                    case DataValidationErrorStyle.Warning:
                        DataValidationAlertStyleType = DataValidationAlertStyleTypes[1];
                        break;
                    case DataValidationErrorStyle.Information:
                        DataValidationAlertStyleType = DataValidationAlertStyleTypes[2];
                        break;
                }
                DataValidationAlertMessageTitle = rule.ErrorMessageTitle;
                DataValidationAlertMessageContent = rule.ErrorMessageDescription;
            }
            this.UpdateUIAfterAllowValueChange();
        }


        private bool IsDecimal(string value)
        {
            try { double.Parse(value); }
            catch (Exception) { return false; }
            return true;
        }

        public bool IsInteger(string value)
        {
            try { long.Parse(value); }
            catch (Exception) { return false; }
            return true;
        }

        public bool AreReversedNumerical(string low, string high)
        {
            try
            {
                double d1 = double.Parse(low);
                double d2 = double.Parse(high);
                if (d1 >= d2) return true;
            }
            catch (Exception) { }
            return false;
        }

        public bool AreReversedDates(string low, string high)
        {
            try
            {
                DateTime dateTimeLow;
                DateTime.TryParse(low, out dateTimeLow);
                DateTime dateTimeHigh;
                DateTime.TryParse(high, out dateTimeHigh);
                if (dateTimeLow > dateTimeHigh) return true;
            }
            catch (Exception) { }
            return false;
        }

        public bool AreReversedTimes(string low, string high)
        {
            try
            {
                TimeSpan timeSpanLow;
                TimeSpan.TryParse(low, out timeSpanLow);
                TimeSpan timeSpanHigh;
                TimeSpan.TryParse(high, out timeSpanHigh);
                if (timeSpanLow > timeSpanHigh) return true;
            }
            catch (Exception) { }
            return false;
        }

        public bool IsCorrectDate(string date)
        {
            try
            {
                DateTime result;
                if (DateTime.TryParse(date, out result)) return true;
            }
            catch (Exception) { }
            return false;
        }

        public bool IsCorrectTime(string time)
        {
            try
            {
                TimeSpan result;
                if (TimeSpan.TryParse(time, out result)) return true;
            }
            catch (Exception) { }
            return false;
        }

        private string VerifyDialogData()
        {
            string val3 = ThirdInputValue != null ? ThirdInputValue.ToString().Trim() : string.Empty;
            string val4 = FourthInputVisible ? FourthInputValue != null ? FourthInputValue.ToString().Trim() : string.Empty : string.Empty;

            if (ThirdInputVisible)
            {
                if (val3.Length == 0)
                {
                    if (FourthInputVisible)
                        return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_YouMustEnter2,
                            FourthInputLabelText, ThirdInputLabelText);
                    else
                        return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_YouMustEnter1,
                            ThirdInputLabelText);
                }
            }

            switch (DataValidationAllowValue.ItemName)
            {
                case "AllowValue1": // Whole number
                case "AllowValue2": // Decimal
                case "AllowValue6": // Text length
                    if (val3[0] != '=')
                    {
                        if (!IsDecimal(val3))
                            return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheValueMustBe,
                                ThirdInputLabelText);
                        if (DataValidationAllowValue.ItemName == "AllowValue1" || DataValidationAllowValue.ItemName == "AllowValue6")
                            if (IsDecimal(val3) && !IsInteger(val3))
                                return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_DecimalValuesCannotBeUsed,
                                    ResourceStrings.ResourceStrings.Text_DataValidationAllowValue1);
                    }
                    if (FourthInputVisible)
                    {
                        if (val4.Length == 0)
                            return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_YouMustEnter2,
                                FourthInputLabelText, ThirdInputLabelText);
                        if (val4[0] != '=')
                        {
                            if (!IsDecimal(val4))
                                return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheValueMustBe,
                                    FourthInputLabelText);
                            if (DataValidationAllowValue.ItemName == "AllowValue1" || DataValidationAllowValue.ItemName == "AllowValue6")
                                if (IsDecimal(val4) && !IsInteger(val4))
                                    return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_DecimalValuesCannotBeUsed,
                                        ResourceStrings.ResourceStrings.Text_DataValidationAllowValue1);
                            if (AreReversedNumerical(val3, val4))
                                return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheValueMustBeGreaterThan,
                                    FourthInputLabelText, ThirdInputLabelText);
                        }
                    }
                    break;

                case "AllowValue4": // Date
                    if (val3[0] != '=' && !IsCorrectDate(val3))
                    {
                        return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheDateYouEntered, ThirdInputLabelText);
                    }
                    if (FourthInputVisible)
                    {
                        if (val4.Length == 0)
                            return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_YouMustEnter2,
                                FourthInputLabelText, ThirdInputLabelText);
                        if (val4[0] != '=')
                        {
                            if (!IsCorrectDate(val4))
                            {
                                return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheDateYouEntered, FourthInputLabelText);
                            }
                            if (AreReversedDates(val3, val4))
                                return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheValueMustBeGreaterThan,
                                    FourthInputLabelText, ThirdInputLabelText);
                        }
                    }
                    break;

                case "AllowValue5": // Time
                    if (val3[0] != '=' && !IsCorrectTime(val3))
                    {
                        return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheTimeYouEntered, ThirdInputLabelText);
                    }
                    if (FourthInputVisible)
                    {
                        if (val4.Length == 0)
                            return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_YouMustEnter2,
                                FourthInputLabelText, ThirdInputLabelText);
                        if (val4[0] != '=')
                        {
                            if (!IsCorrectTime(val4))
                            {
                                return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheTimeYouEntered, FourthInputLabelText);
                            }
                            if (AreReversedTimes(val3, val4))
                                return string.Format(ResourceStrings.ResourceStrings.Msg_DataValidation_TheValueMustBeGreaterThan,
                                    FourthInputLabelText, ThirdInputLabelText);
                        }
                    }
                    break;

                case "AllowValue3": // List
                    break;
                case "AllowValue7": // Custom
                    break;
            }

            return null;
        }

        private void SetSelectionFromWorksheetReferenceCollection(WorksheetReferenceCollection wrc, WorksheetCell activeCell)
        {
            SpreadsheetCellRange scr;
            WorksheetRegion wr;
            WorksheetCell wc;
            foreach (var obj in wrc)
            {
                if (obj is WorksheetRegion)
                {
                    wr = (WorksheetRegion)obj;
                    scr = new SpreadsheetCellRange(wr.FirstRow, wr.FirstColumn, wr.LastRow, wr.LastColumn);
                }
                else
                {
                    wc = (WorksheetCell)obj;
                    scr = new SpreadsheetCellRange(wc.RowIndex, wc.ColumnIndex);
                }
                if (scr.Contains(activeCell.RowIndex, activeCell.ColumnIndex))
                {
                    this.selection.AddActiveCellRange(scr, new SpreadsheetCell(activeCell.RowIndex, activeCell.ColumnIndex));
                }
                else
                {
                    this.selection.AddCellRange(scr);
                }
            }
        }

        #endregion //Private Methods
    }

    public class CustomComboBoxItem
    {
        public string ItemLabel { get; set; }
        public string ItemName { get; set; }
    }
}
