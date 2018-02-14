using IgExcel.Infrastructure.Commands;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace IgExcel.Infrastructure.Adapters
{
    public class SpreadSelectionSheetAdapter : FrameworkElement, INotifyPropertyChanged
    {
        #region Member Variables

        //Commands
        private AdapterSetFontColorCommand setFontColorCommand;
        private AdapterSetCellFillCommand setCellFillCommand;
        private AdapterSetBorderCommand setBorderCommand;

        #endregion //Member Variables

        #region Constructor

        public SpreadSelectionSheetAdapter()
        {
            this.borderColor = new WorkbookColorInfo(Colors.Black);
            this.cellBorderLineStyle = CellBorderLineStyle.Thin;
            this.sheetCellRangeBorders = SpreadsheetCellRangeBorders.BottomBorder;
            this.lastAppliedBorderSettings = ExcelBorders.BottomBorder;
            this.FillColor = Colors.Yellow;
            this.ForegroundColor = Colors.Red;

            setBorderCommand = new AdapterSetBorderCommand(this);
            setFontColorCommand = new AdapterSetFontColorCommand(this);
            setCellFillCommand = new AdapterSetCellFillCommand(this);
        }

        #endregion //Constructor

        #region DependencyProperties

        #region SpreadSheet

        public static readonly DependencyProperty SpreadSheetProperty = DependencyProperty.Register(SpreadSheetPropertyName,
            typeof(XamSpreadsheet), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((SpreadSelectionSheetAdapter)o).OnDependencyPropertyChanged(SpreadSheetPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string SpreadSheetPropertyName = "SpreadSheet";

        public XamSpreadsheet SpreadSheet
        {
            get
            {
                return (XamSpreadsheet)this.GetValue(SpreadSelectionSheetAdapter.SpreadSheetProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.SpreadSheetProperty, value);
            }
        }

        #endregion //SpreadSheet

        #region Value

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(ValuePropertyName,
            typeof(Stream), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null,
                (o, e) =>
                {
                    ((SpreadSelectionSheetAdapter)o).OnDependencyPropertyChanged(ValuePropertyName, e.OldValue, e.NewValue);
                }));

        internal const string ValuePropertyName = "Value";

        public Stream Value
        {
            get
            {
                return (Stream)this.GetValue(SpreadSelectionSheetAdapter.ValueProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.ValueProperty, value);
            }
        }

        #endregion //Value

        #region Selection

        public static readonly DependencyProperty SelectionProperty = DependencyProperty.Register(SelectionPropertyName,
            typeof(SpreadsheetSelection), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null));

        internal const string SelectionPropertyName = "Selection";

        public SpreadsheetSelection Selection
        {
            get
            {
                return (SpreadsheetSelection)this.GetValue(SpreadSelectionSheetAdapter.SelectionProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.SelectionProperty, value);
            }
        }

        #endregion //Selection

        #region SelectedTable

        public static readonly DependencyProperty SelectedTableProperty = DependencyProperty.Register(SelectedTablePropertyName,
            typeof(WorksheetTable), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null));

        internal const string SelectedTablePropertyName = "SelectedTable";

        public WorksheetTable SelectedTable
        {
            get
            {
                return (WorksheetTable)this.GetValue(SpreadSelectionSheetAdapter.SelectedTableProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.SelectedTableProperty, value);
            }
        }

        #endregion //SelectedTable

        #region CanExecuteFormatAsTable

        public static readonly DependencyProperty CanExecuteFormatAsTableProperty = DependencyProperty.Register(CanExecuteFormatAsTablePropertyName,
            typeof(bool), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null));

        internal const string CanExecuteFormatAsTablePropertyName = "CanExecuteFormatAsTable";

        public bool CanExecuteFormatAsTable
        {
            get
            {
                return (bool)this.GetValue(SpreadSelectionSheetAdapter.CanExecuteFormatAsTableProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.CanExecuteFormatAsTableProperty, value);
            }
        }

        #endregion //CanExecuteFormatAsTable

        #region WorkbookDirtied

        public static readonly DependencyProperty WorkbookDirtiedProperty = DependencyProperty.Register(WorkbookDirtiedPropertyName,
            typeof(bool), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null));

        internal const string WorkbookDirtiedPropertyName = "WorkbookDirtied";

        public bool WorkbookDirtied
        {
            get
            {
                return (bool)this.GetValue(SpreadSelectionSheetAdapter.WorkbookDirtiedProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.WorkbookDirtiedProperty, value);
            }
        }

        #endregion //WorkbookDirtied

        #region TrackChanges

        public static readonly DependencyProperty TrackChangesProperty = DependencyProperty.Register(TrackChangesPropertyName,
            typeof(bool), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null));

        internal const string TrackChangesPropertyName = "TrackChanges";

        public bool TrackChanges
        {
            get
            {
                return (bool)this.GetValue(SpreadSelectionSheetAdapter.TrackChangesProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.TrackChangesProperty, value);
            }
        }

        #endregion //TrackChanges

        #region IsInEditModeIn

        public static readonly DependencyProperty IsInEditModeInProperty = DependencyProperty.Register(IsInEditModeInPropertyName,
            typeof(bool), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(false,
                (o, e) =>
                {
                    ((SpreadSelectionSheetAdapter)o).OnDependencyPropertyChanged(IsInEditModeInPropertyName, e.OldValue, e.NewValue);
                }));

        internal const string IsInEditModeInPropertyName = "IsInEditModeIn";

        public bool IsInEditModeIn
        {
            get
            {
                return (bool)this.GetValue(SpreadSelectionSheetAdapter.IsInEditModeInProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.IsInEditModeInProperty, value);
            }
        }

        #endregion //IsInEditModeIn

        #region IsInEditModeOut

        public static readonly DependencyProperty IsInEditModeOutProperty = DependencyProperty.Register(IsInEditModeOutPropertyName,
            typeof(bool), typeof(SpreadSelectionSheetAdapter),
            new PropertyMetadata(null));

        internal const string IsInEditModeOutPropertyName = "IsInEditModeOut";

        public bool IsInEditModeOut
        {
            get
            {
                return (bool)this.GetValue(SpreadSelectionSheetAdapter.IsInEditModeOutProperty);
            }
            set
            {
                this.SetValue(SpreadSelectionSheetAdapter.IsInEditModeOutProperty, value);
            }
        }

        #endregion //IsInEditModeOut

        #endregion //DependencyProperties

        #region Properties

        private string tableStyle;
        public string TableStyle
        {
            get { return tableStyle; }
            set
            {
                if (tableStyle != value)
                {
                    tableStyle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string TableStylePropertyName = "TableStyle";

        private CellBorderLineStyle cellBorderLineStyle;
        public CellBorderLineStyle CellBorderLineStyle
        {
            get { return cellBorderLineStyle; }
            set
            {
                if (cellBorderLineStyle != value)
                {
                    cellBorderLineStyle = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string CellBorderLineStylePropertyName = "CellBorderLineStyle";

        private SpreadsheetCellRangeBorders sheetCellRangeBorders;
        public SpreadsheetCellRangeBorders SheetCellRangeBorders
        {
            get { return sheetCellRangeBorders; }
            set
            {
                if (sheetCellRangeBorders != value)
                {
                    sheetCellRangeBorders = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string SheetCellRangeBordersPropertyName = "SheetCellRangeBorders";

        private WorkbookColorInfo borderColor;
        public WorkbookColorInfo BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string BorderColorPropertyName = "BorderColor";

        private Color fillColor;
        public Color FillColor
        {
            get { return fillColor; }
            set
            {
                if (fillColor != value)
                {
                    fillColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string FillColorPropertyName = "FillColor";

        private Color foregroundColor;
        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set
            {
                if (foregroundColor != value)
                {
                    foregroundColor = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string ForegroundColorPropertyName = "ForegroundColor";

        private ExcelBorders lastAppliedBorderSettings;
        public ExcelBorders LastAppliedBorderSettings
        {
            get { return lastAppliedBorderSettings; }
            set
            {
                if (lastAppliedBorderSettings != value)
                {
                    lastAppliedBorderSettings = value;
                    NotifyPropertyChanged();
                }
            }
        }
        internal const string LastAppliedBorderSettingsPropertyName = "LastAppliedBorderSettings";

        public List<SpreadsheetCellRangeBorders> SheetCellRangeBordersList
        {
            get
            {
                return new List<SpreadsheetCellRangeBorders> 
                { 
                    SpreadsheetCellRangeBorders.BottomBorder, 
                    SpreadsheetCellRangeBorders.LeftBorder, 
                    SpreadsheetCellRangeBorders.RightBorder, 
                    SpreadsheetCellRangeBorders.TopBorder, 
                    SpreadsheetCellRangeBorders.AllBorder, 
                    SpreadsheetCellRangeBorders.OutsideBorder, 
                };
            }
        }

        public List<string> MediumTableStyles
        {
            get
            {
                return new List<string>
                 {
                      "TableStyleMedium1",
                      "TableStyleMedium2",
                      "TableStyleMedium3",
                      "TableStyleMedium4",
                      "TableStyleMedium5",
                      "TableStyleMedium6",
                      "TableStyleMedium7",
                      "TableStyleMedium8",
                      "TableStyleMedium9",       
                      "TableStyleMedium10",
                      "TableStyleMedium11",
                      "TableStyleMedium12",
                      "TableStyleMedium13",
                      "TableStyleMedium14",
                      "TableStyleMedium15",
                      "TableStyleMedium16",
                      "TableStyleMedium17",
                      "TableStyleMedium18",
                      "TableStyleMedium19",
                      "TableStyleMedium20",
                      "TableStyleMedium21",
                      "TableStyleMedium22",
                      "TableStyleMedium23",
                      "TableStyleMedium24",
                      "TableStyleMedium25",
                      "TableStyleMedium26",
                      "TableStyleMedium27",
                      "TableStyleMedium28"
                 };
            }
        }
        public List<string> DarkTableStyles
        {
            get
            {
                return new List<string>
                {
                    "TableStyleDark1",
                    "TableStyleDark2",
                    "TableStyleDark3",
                    "TableStyleDark4",
                    "TableStyleDark5",
                    "TableStyleDark6",
                    "TableStyleDark7",
                    "TableStyleDark8",
                    "TableStyleDark9",
                    "TableStyleDark10",
                    "TableStyleDark11"
                };
            }
        }
        public List<string> LightTableStyles
        {
            get
            {
                return new List<string>
                {
                     "TableStyleLight1",
                     "TableStyleLight2",
                     "TableStyleLight3",
                     "TableStyleLight4",
                     "TableStyleLight5",
                     "TableStyleLight6",
                     "TableStyleLight7",
                     "TableStyleLight8",
                     "TableStyleLight9",
                     "TableStyleLight10",
                     "TableStyleLight11",
                     "TableStyleLight12",
                     "TableStyleLight13",
                     "TableStyleLight14",
                     "TableStyleLight15",
                     "TableStyleLight16",
                     "TableStyleLight17",
                     "TableStyleLight18",
                     "TableStyleLight19",
                     "TableStyleLight20",
                     "TableStyleLight21"
                };
            }
        }
        public List<string> AllTableStyles
        {
            get
            {
                return new List<string>
                {
                    "TableStyleMedium28",
                    "TableStyleMedium27",
                    "TableStyleMedium26",
                    "TableStyleMedium25",
                    "TableStyleMedium24",
                    "TableStyleMedium23",
                    "TableStyleMedium22",
                    "TableStyleMedium21",
                    "TableStyleMedium20",
                    "TableStyleMedium19",
                    "TableStyleMedium18",
                    "TableStyleMedium17",
                    "TableStyleMedium16",
                    "TableStyleMedium15",
                    "TableStyleMedium14",
                    "TableStyleMedium13",
                    "TableStyleMedium12",
                    "TableStyleMedium11",
                    "TableStyleMedium10",
                    "TableStyleMedium9",
                    "TableStyleMedium8",
                    "TableStyleMedium7",
                    "TableStyleMedium6",
                    "TableStyleMedium5",
                    "TableStyleMedium4",
                    "TableStyleMedium3",
                    "TableStyleMedium2",
                    "TableStyleMedium1",
                    "TableStyleLight21",
                    "TableStyleLight20",
                    "TableStyleLight19",
                    "TableStyleLight18",
                    "TableStyleLight17",
                    "TableStyleLight16",
                    "TableStyleLight15",
                    "TableStyleLight14",
                    "TableStyleLight13",
                    "TableStyleLight12",
                    "TableStyleLight11",
                    "TableStyleLight10",
                    "TableStyleLight9",
                    "TableStyleLight8",
                    "TableStyleLight7",
                    "TableStyleLight6",
                    "TableStyleLight5",
                    "TableStyleLight4",
                    "TableStyleLight3",
                    "TableStyleLight2",
                    "TableStyleLight1",

                    "TableStyleDark1",
                    "TableStyleDark2",
                    "TableStyleDark3",
                    "TableStyleDark4",
                    "TableStyleDark5",
                    "TableStyleDark6",
                    "TableStyleDark7",
                    "TableStyleDark8",
                    "TableStyleDark9",
                    "TableStyleDark10",
                    "TableStyleDark11"
                };
            }
        }

        public ICommand SetBorderCommand
        {
            get { return setBorderCommand; }
        }

        public ICommand SetFontColorCommand
        {
            get { return setFontColorCommand; }
        }

        public ICommand SetCellFillCommand
        {
            get { return setCellFillCommand; }
        }

        #endregion //Properties

        #region OnDependencyPropertyChanged

        private void OnDependencyPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            switch (propertyName)
            {
                case SpreadSheetPropertyName:
                    HookSpreadSheet((XamSpreadsheet)newValue, true);
                    break;
                case IsInEditModeInPropertyName:
                    this.IsInEditModeOut = (bool)newValue;
                    break;
            }
        }

        #endregion //OnDependencyPropertyChanged

        #region HookSpreadSheet

        private void HookSpreadSheet(XamSpreadsheet spreadsheet, bool hook)
        {
            if (spreadsheet != null)
            {
                if (hook)
                {
                    spreadsheet.SelectionChanged += OnSelectionChanged;
                    spreadsheet.IsVisibleChanged += OnIsVisibleChanged;
                    spreadsheet.Loaded += OnLoaded;
                    spreadsheet.WorkbookDirtied += OnWorkbookDirtied;
                    spreadsheet.EditModeEntered += OnEditModeEntered;
                    spreadsheet.EditModeExited += OnEditModeExited;
                }
                else
                {
                    spreadsheet.SelectionChanged -= OnSelectionChanged;
                    spreadsheet.Loaded -= OnLoaded;
                    spreadsheet.WorkbookDirtied -= OnWorkbookDirtied;
                    spreadsheet.IsVisibleChanged -= OnIsVisibleChanged;
                    spreadsheet.EditModeEntered -= OnEditModeEntered;
                    spreadsheet.EditModeExited -= OnEditModeExited;
                }
            }
        }

        #endregion //HookSpreadSheet

        #region OnEditModeExited

        private void OnEditModeExited(object sender, SpreadsheetEditModeExitedEventArgs e)
        {
            this.CanExecuteFormatAsTable = true;
        }

        #endregion //OnEditModeExited

        #region OnEditModeEntered

        private void OnEditModeEntered(object sender, SpreadsheetEditModeEnteredEventArgs e)
        {
            this.CanExecuteFormatAsTable = false;
        }

        #endregion //OnEditModeEntered

        #region OnSelectionChanged

        void OnSelectionChanged(object sender, SpreadsheetSelectionChangedEventArgs e)
        {
            this.UpdateSelectionInfo();
        }

        #endregion //OnSelectionChanged

        #region OnLoaded

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.UpdateSelectionInfo();
            this.Selection = SpreadSheet.ActiveSelection;
            this.CanExecuteFormatAsTable = true;
            this.SpreadSheet.Focus();
        }

        #endregion //OnLoaded

        #region OnIsVisibleChanged

        private void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue && this.Selection == null)
                this.Selection = SpreadSheet.ActiveSelection;
        }

        #endregion //OnIsVisibleChanged

        #region OnWorkbookDirtied

        private void OnWorkbookDirtied(object sender, SpreadsheetWorkbookDirtiedEventArgs e)
        {
            if (this.TrackChanges)
            {
                this.WorkbookDirtied = true;
            }
        }

        #endregion //OnWorkbookDirtied

        #region UpdateSelectionInfo

        private void UpdateSelectionInfo()
        {
            if (this.SpreadSheet == null || this.SpreadSheet.ActiveSelection == null)
                return;

            try
            {
                if (this.SpreadSheet.ActiveSelection.CellRanges.Count == 1 && this.SpreadSheet.ActiveWorksheet.Tables.Count > 0)
                {
                    foreach (var table in this.SpreadSheet.ActiveWorksheet.Tables)
                    {
                        TableAndSelectionLocation location = TableIntersectsWithSelectionRange(table.WholeTableRegion, this.SpreadSheet.ActiveSelection.CellRanges[0]);

                        if (location == TableAndSelectionLocation.SelectionIsInsideTable)
                        {
                            this.TableStyle = table.Style.Name;
                            this.CanExecuteFormatAsTable = true;
                            this.SelectedTable = table;
                            break;
                        }
                        else
                        {
                            this.TableStyle = string.Empty;
                            this.CanExecuteFormatAsTable = location == TableAndSelectionLocation.NoIntersection;
                            this.SelectedTable = null;

                            if (location == TableAndSelectionLocation.PartialIntersection)
                                break;
                        }

                    }
                }
                else
                {

                    this.TableStyle = string.Empty;
                    this.CanExecuteFormatAsTable = this.SpreadSheet.ActiveSelection.CellRanges.Count == 1;
                    this.SelectedTable = null;
                }

            }
            catch { }

            setBorderCommand.RaiseCanExecuteChanged();
            setFontColorCommand.RaiseCanExecuteChanged();
            setCellFillCommand.RaiseCanExecuteChanged();

            this.Selection = SpreadSheet.ActiveSelection;
        }

        #endregion //UpdateSelectionInfo

        #region Private Methods

        private enum TableAndSelectionLocation { NoIntersection, SelectionIsInsideTable, PartialIntersection }

        private TableAndSelectionLocation TableIntersectsWithSelectionRange(WorksheetRegion tableRegion, SpreadsheetCellRange selectionRegion)
        {

            Rect tableRectangle = new Rect(new Point(tableRegion.FirstRow, tableRegion.FirstColumn), new Point(tableRegion.LastRow, tableRegion.LastColumn));
            Rect selectionRectangle = new Rect(new Point(selectionRegion.FirstRow, selectionRegion.FirstColumn), new Point(selectionRegion.LastRow, selectionRegion.LastColumn));

            if (selectionRectangle.Width == 0 && selectionRectangle.Height == 0)
            {
                if (tableRectangle.Contains(selectionRectangle.X, selectionRectangle.Y))
                {
                    return TableAndSelectionLocation.SelectionIsInsideTable;
                }
                else
                {
                    return TableAndSelectionLocation.NoIntersection;
                }
            }

            if (!tableRectangle.IntersectsWith(selectionRectangle))
            {
                return TableAndSelectionLocation.NoIntersection;
            }
            else if (tableRectangle.Contains(selectionRectangle))
            {
                return TableAndSelectionLocation.SelectionIsInsideTable;
            }
            else
            {
                return TableAndSelectionLocation.PartialIntersection;
            }
        }

        private bool IntersectsWith(int rowXFirst, int columnXFirst, int rowXLast, int columnXLast, int rowYFirst, int columnYFirst, int rowYLast, int columnYLast)
        {
            Rect rectX = new Rect(new Point(rowXFirst, columnXFirst), new Point(rowXLast, columnXLast));
            Rect rectY = new Rect(new Point(rowYFirst, columnYFirst), new Point(rowYLast, columnYLast));

            return rectX.IntersectsWith(rectY);
        }
        #endregion //Private Methods

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion //INotifyPropertyChanged
    }
}
