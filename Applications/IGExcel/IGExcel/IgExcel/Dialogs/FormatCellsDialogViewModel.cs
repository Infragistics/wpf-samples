using IgExcel.Business;
using IgExcel.Infrastructure;
using IgExcel.Infrastructure.Dialogs;
using IgExcel.Services;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace IgExcel.Dialogs
{
    public class FormatCellsDialogViewModel : ViewModelBase, IDialogAware, INavigationAware
    {
        #region Private Memebers

        private IEventAggregator eventAggragator;
        private IFontService fontService;
        private IFormatsService formatsService;

        private SpreadsheetSelection activeSelection;
        private Worksheet activeWorksheet;

        private string selectedItemName;
        private SpreadsheetCellRangeFormat cellFormat;

        private bool synchronizeFormattings;
        private List<NumberFormatInfo> numberFormats;
        private FormatInfo selectedFormat;
        private NumberFormatInfo selectedNumberFormatCategory;
        private object activeCellValue;
        private List<string> modifiedProperties;
        private bool trackChanges;

        #endregion //Private Memebers

        #region Public Properties

        public List<int> FontSizes { get; private set; }
        public List<string> FontNames { get; private set; }

        public object ActiveCellValue
        {
            get { return activeCellValue; }
            set { SetProperty<object>(ref activeCellValue, value); }
        }

        public string SelectedItemName
        {
            get { return selectedItemName; }
            set { SetProperty<string>(ref selectedItemName, value); }
        }
        public SpreadsheetCellRangeFormat CellFormat
        {
            get { return cellFormat; }
            set { SetProperty<SpreadsheetCellRangeFormat>(ref cellFormat, value); }
        }

        public bool SynchronizeFormattings
        {
            get { return synchronizeFormattings; }
            set { SetProperty<bool>(ref synchronizeFormattings, value); }
        }

        public FormatInfo SelectedFormat
        {
            get { return selectedFormat; }
            set { SetProperty<FormatInfo>(ref selectedFormat, value); }
        }
        internal const string SelectedFormatPropertyName = "SelectedFormat";

        public NumberFormatInfo SelectedNumberFormatCategory
        {
            get { return selectedNumberFormatCategory; }
            set { SetProperty<NumberFormatInfo>(ref selectedNumberFormatCategory, value); }
        }
        internal const string SelectedNumberFormatCategoryPropertyName = "SelectedNumberFormatCategory";

        public List<NumberFormatInfo> NumberFormats
        {
            get { return numberFormats; }
            set { SetProperty<List<NumberFormatInfo>>(ref numberFormats, value); }
        }

        private int? fontSize;
        public int? FontSize
        {
            get { return fontSize; }
            set { SetProperty<int?>(ref fontSize, value); }
        }
        internal const string FontSizePropertyName = "FontSize";

        private string fontName;
        public string FontName
        {
            get { return fontName; }
            set { SetProperty<string>(ref fontName, value); }
        }
        internal const string FontNamePropertyName = "FontName";

        private bool isNormalFont;
        public bool IsNormalFont
        {
            get { return isNormalFont; }
            set { SetProperty<bool>(ref isNormalFont, value); }
        }
        internal const string IsNormalFontPropertyName = "IsNormalFont";

        private Color fontColor;
        public Color FontColor
        {
            get { return fontColor; }
            set { SetProperty<Color>(ref fontColor, value); }
        }
        internal const string FontColorPropertyName = "FontColor";

        private FontStylesCustom? fontStyle;
        public FontStylesCustom? FontStyle
        {
            get { return fontStyle; }
            set { SetProperty<FontStylesCustom?>(ref fontStyle, value); }
        }
        internal const string FontStylePropertyName = "FontStyle";

        private bool? subscript;
        public bool? Subscript
        {
            get { return subscript; }
            set { SetProperty<bool?>(ref subscript, value); }
        }
        internal const string SubscriptPropertyName = "Subscript";

        private bool? superscript;
        public bool? Superscript
        {
            get { return superscript; }
            set { SetProperty<bool?>(ref superscript, value); }
        }
        internal const string SuperscriptPropertyName = "Superscript";

        private bool? strikethrough;
        public bool? Strikethrough
        {
            get { return strikethrough; }
            set { SetProperty<bool?>(ref strikethrough, value); }
        }
        internal const string StrikethroughPropertyName = "Strikethrough";

        private HorizontalCellAlignment? horizontalCellAlignment;
        public HorizontalCellAlignment? HorizontalCellAlignment
        {
            get { return horizontalCellAlignment; }
            set { SetProperty<HorizontalCellAlignment?>(ref horizontalCellAlignment, value); }
        }
        internal const string HorizontalCellAlignmentPropertyName = "HorizontalCellAlignment";

        private VerticalCellAlignment? verticalCellAlignment;
        public VerticalCellAlignment? VerticalCellAlignment
        {
            get { return verticalCellAlignment; }
            set { SetProperty<VerticalCellAlignment?>(ref verticalCellAlignment, value); }
        }
        internal const string VerticalCellAlignmentPropertyName = "VerticalCellAlignment";


        private FillPatternStyle fillPatternStyle;
        public FillPatternStyle FillPatternStyle
        {
            get { return fillPatternStyle; }
            set { SetProperty<FillPatternStyle>(ref fillPatternStyle, value); }
        }
        internal const string FillPatternStylePropertyName = "FillPatternStyle";

        private Color? backgroundColor;
        public Color? BackgroundColor
        {
            get { return backgroundColor; }
            set { SetProperty<Color?>(ref backgroundColor, value); }
        }
        internal const string BackgroundColorPropertyName = "BackgroundColor";

        private Color? patternColor;
        public Color? PatternColor
        {
            get { return patternColor; }
            set { SetProperty<Color?>(ref patternColor, value); }
        }
        internal const string PatternColorPropertyName = "PatternColor";


        private CellFill fill;
        public CellFill Fill
        {
            get { return fill; }
            set { SetProperty<CellFill>(ref fill, value); }
        }
        internal const string FillPropertyName = "Fill";


        private bool? wrapText;
        public bool? WrapText
        {
            get { return wrapText; }
            set { SetProperty<bool?>(ref wrapText, value); }
        }
        internal const string WrapTextPropertyName = "WrapText";

        private bool? shrinkToFit;
        public bool? ShrinkToFit
        {
            get { return shrinkToFit; }
            set { SetProperty<bool?>(ref shrinkToFit, value); }
        }
        internal const string ShrinkToFitPropertyName = "ShrinkToFit";

        private int? indent;
        public int? Indent
        {
            get { return indent; }
            set { SetProperty<int?>(ref indent, value); }
        }
        internal const string IndentPropertyName = "Indent";


        private CellBorderLineStyle? topBorder;
        public CellBorderLineStyle? TopBorder
        {
            get { return topBorder; }
            set { SetProperty<CellBorderLineStyle?>(ref topBorder, value); }
        }
        internal const string TopBorderPropertyName = "TopBorder";

        private CellBorderLineStyle? bottomBorder;
        public CellBorderLineStyle? BottomBorder
        {
            get { return bottomBorder; }
            set { SetProperty<CellBorderLineStyle?>(ref bottomBorder, value); }
        }
        internal const string BottomBorderPropertyName = "BottomBorder";

        private CellBorderLineStyle? leftBorder;
        public CellBorderLineStyle? LeftBorder
        {
            get { return leftBorder; }
            set { SetProperty<CellBorderLineStyle?>(ref leftBorder, value); }
        }

        private CellBorderLineStyle? rightBorder;
        public CellBorderLineStyle? RightBorder
        {
            get { return rightBorder; }
            set { SetProperty<CellBorderLineStyle?>(ref rightBorder, value); }
        }

        private CellBorderLineStyle? diagonalDownBorder;
        public CellBorderLineStyle? DiagonalDownBorder
        {
            get { return diagonalDownBorder; }
            set { SetProperty<CellBorderLineStyle?>(ref diagonalDownBorder, value); }
        }

        private CellBorderLineStyle? diagonalUpBorder;
        public CellBorderLineStyle? DiagonalUpBorder
        {
            get { return diagonalUpBorder; }
            set { SetProperty<CellBorderLineStyle?>(ref diagonalUpBorder, value); }
        }

        public Array HorizontalCellAlignments
        {
            get
            {
                return Enum.GetValues(typeof(HorizontalCellAlignment));
            }
        }
        public Array VerticalCellAlignments
        {
            get
            {
                return Enum.GetValues(typeof(VerticalCellAlignment));
            }
        }
        public Array FontStyles
        {
            get
            {
                return Enum.GetValues(typeof(FontStylesCustom));
            }
        }
        public Array FillPatternStyles
        {
            get
            {
                return Enum.GetValues(typeof(FillPatternStyle));
            }
        }
        public Array CellBorderLineStyles
        {
            get
            {
                return Enum.GetValues(typeof(CellBorderLineStyle));
            }
        }

        public DelegateCommand<bool?> ResetFontSettingsCommand { get; private set; }
        public DelegateCommand OkCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        #endregion //Public Properties

        #region Constructor

        public FormatCellsDialogViewModel(IEventAggregator eventAggragator, IFontService fontService, IFormatsService formatsService)
        {
            this.Title = ResourceStrings.ResourceStrings.Text_FormatCells;
            this.IconSource = "pack://application:,,,/IgExcel.Infrastructure;component/Images/FormatCells.ico";

            this.eventAggragator = eventAggragator;
            this.fontService = fontService;
            this.formatsService = formatsService;

            this.FontNames = fontService.GetFontNames();
            this.FontSizes = fontService.GetFontSizes();
            this.NumberFormats = formatsService.GetNumberFormats();

            this.SelectedNumberFormatCategory = this.NumberFormats.FirstOrDefault();
            this.modifiedProperties = new List<string>();

            HookPropertyChanged(true);

            //Commands
            OkCommand = new DelegateCommand(ExecuteOk, CanExecuteOk);
            CancelCommand = new DelegateCommand(ExecuteCancel);
            ResetFontSettingsCommand = new DelegateCommand<bool?>(ExecuteResetFontSettings);
        }

        #endregion //Constructor

        #region Commands

        private bool CanExecuteOk()
        {
            return true;
        }

        private void ExecuteOk()
        {
            ApplyFormattings();
            RequestClose();
        }

        private void ExecuteCancel()
        {
            RequestClose();
        }

        private void ExecuteResetFontSettings(bool? execute)
        {
            if (execute == true)
            {
                this.FontName = "Calibri";
                this.FontSize = 11;
                this.FontStyle = FontStylesCustom.Regular;
            }
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
            this.SelectedItemName = (string)navigationContext.Parameters[FormatCellsDialogParameters.InitialTabNameKey];
            this.activeSelection = (SpreadsheetSelection)navigationContext.Parameters[FormatCellsDialogParameters.SelectionKey];
            this.activeWorksheet = (Worksheet)navigationContext.Parameters[FormatCellsDialogParameters.WorksheetKey];
            this.CellFormat = (SpreadsheetCellRangeFormat)navigationContext.Parameters[FormatCellsDialogParameters.SelectionFormatsKey];

            ResolveCellFormats(this.activeSelection, this.activeWorksheet);

            this.trackChanges = true;
        }

        #endregion //INavigationAware

        #region Private Methods

        private FontStylesCustom? ResolveFontStyle(ExcelDefaultableBoolean? bold, ExcelDefaultableBoolean? italic)
        {
            if (bold == null || italic == null)
            {
                return null;
            }

            if (bold == ExcelDefaultableBoolean.False && italic == ExcelDefaultableBoolean.False)
            {
                return FontStylesCustom.Regular;
            }
            else if (bold == ExcelDefaultableBoolean.True && italic == ExcelDefaultableBoolean.False)
            {
                return FontStylesCustom.Bold;
            }
            else if (bold == ExcelDefaultableBoolean.False && italic == ExcelDefaultableBoolean.True)
            {
                return FontStylesCustom.Italic;
            }
            else
            {
                return FontStylesCustom.BoldItalic;
            }
        }

        private void ResolveCellFormats(SpreadsheetSelection selection, Worksheet worksheet)
        {
            List<IWorksheetCellFormat> cellFormats = new List<IWorksheetCellFormat>();

            //Take all ResolvedCellFormats 
            foreach (var range in selection.CellRanges)
            {
                for (int i = range.FirstRow; i <= range.LastRow; i++)
                {
                    var row = worksheet.Rows[i];

                    for (int j = range.FirstColumn; j <= range.LastColumn; j++)
                    {
                        cellFormats.Add(row.GetResolvedCellFormat(j));
                    }
                }
            }


            this.FontName = cellFormats[0].Font.Name;
            this.FontSize = cellFormats[0].Font.Height;
            this.FontColor = cellFormats[0].Font.ColorInfo.GetResolvedColor(this.activeWorksheet.Workbook);
            this.Superscript = IsSuperscript(cellFormats[0].Font.SuperscriptSubscriptStyle);
            this.Subscript = IsSubscript(cellFormats[0].Font.SuperscriptSubscriptStyle);
            this.HorizontalCellAlignment = cellFormats[0].Alignment;
            this.VerticalCellAlignment = cellFormats[0].VerticalAlignment;
            this.Indent = cellFormats[0].Indent;
            this.Fill = cellFormats[0].Fill;

            ExcelDefaultableBoolean? strikeout = cellFormats[0].Font.Strikeout;
            ExcelDefaultableBoolean? bold = cellFormats[0].Font.Bold;
            ExcelDefaultableBoolean? italic = cellFormats[0].Font.Italic;
            ExcelDefaultableBoolean? wrapTextLocal = cellFormats[0].WrapText;
            ExcelDefaultableBoolean? shrinkToFitLocal = cellFormats[0].ShrinkToFit;

            CellBorderLineStyle? topBorderStyle = cellFormats[0].TopBorderStyle;


            string formatString = cellFormats[0].FormatString;


            for (int i = 1; i < cellFormats.Count; i++)
            {
                if (formatString != cellFormats[i].FormatString && formatString != null)
                {
                    formatString = null;
                }

                if (this.FontName != cellFormats[i].Font.Name && this.FontName != null)
                {
                    this.FontName = null;
                }

                if (this.FontSize != cellFormats[i].Font.Height && this.FontSize != null)
                {
                    this.FontSize = null;
                }

                if (this.FontColor != cellFormats[i].Font.ColorInfo.GetResolvedColor(this.activeWorksheet.Workbook) && this.FontColor != Colors.Transparent)
                {
                    this.FontColor = Colors.Transparent;
                }

                if (strikeout != cellFormats[i].Font.Strikeout && strikeout != null)
                {
                    strikeout = null;
                }

                if (bold != cellFormats[i].Font.Bold && bold != null)
                {
                    bold = null;
                }

                if (italic != cellFormats[i].Font.Italic && italic != null)
                {
                    italic = null;
                }

                if (this.superscript != IsSuperscript(cellFormats[i].Font.SuperscriptSubscriptStyle) && this.Superscript != null)
                {
                    this.Superscript = null;
                }

                if (this.subscript != IsSubscript(cellFormats[i].Font.SuperscriptSubscriptStyle) && this.Subscript != null)
                {
                    this.Subscript = null;
                }

                if (this.horizontalCellAlignment != cellFormats[i].Alignment && this.horizontalCellAlignment != null)
                {
                    this.HorizontalCellAlignment = null;
                }

                if (this.verticalCellAlignment != cellFormats[i].VerticalAlignment && this.VerticalCellAlignment != null)
                {
                    this.VerticalCellAlignment = null;
                }

                if (this.indent != cellFormats[i].Indent && this.Indent != null)
                {
                    this.Indent = null;
                }

                if (wrapTextLocal != cellFormats[i].WrapText && wrapTextLocal != null)
                {
                    wrapTextLocal = null;
                }

                if (shrinkToFitLocal != cellFormats[i].ShrinkToFit && shrinkToFitLocal != null)
                {
                    shrinkToFitLocal = null;
                }

                if (this.fill != cellFormats[i].Fill && this.fill != null)
                {
                    this.Fill = null;
                }
            }

            this.FontStyle = ResolveFontStyle(bold, italic);
            this.Strikethrough = ExcelDefaultableBooleanToNullableBool(strikeout);
            this.WrapText = ExcelDefaultableBooleanToNullableBool(wrapTextLocal);
            this.ShrinkToFit = ExcelDefaultableBooleanToNullableBool(shrinkToFitLocal);

            InitFormatString(formatString);
            InitFillTab();

            this.FontSize = this.FontSize / 20;
        }

        private void InitFillTab()
        {
            if (this.fill == null)
                return;

            if (this.Fill is CellFillPattern)
            {
                var cellFillPattern = ((CellFillPattern)this.Fill);
                this.BackgroundColor = cellFillPattern.BackgroundColorInfo.Color;
                this.FillPatternStyle = cellFillPattern.PatternStyle;

                if (cellFillPattern.PatternColorInfo == WorkbookColorInfo.Automatic)
                    this.PatternColor = Colors.Transparent;
                else
                    this.PatternColor = cellFillPattern.PatternColorInfo.Color;
            }
        }

        private void InitFormatString(string formatString)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                this.SelectedFormat = null;
                this.SelectedNumberFormatCategory = null;
            }
            else
            {
                FormatInfo formatInfo = null;

                foreach (var numberFormat in this.NumberFormats)
                {
                    formatInfo = numberFormat.FindFormat(formatString);

                    if (formatInfo != null)
                    {
                        this.SelectedNumberFormatCategory = numberFormat;
                        this.SelectedFormat = formatInfo;
                        break;
                    }
                }

                if (formatInfo == null)
                {
                    var customGroup = this.NumberFormats.First(x => x.IsCustom);
                    this.SelectedNumberFormatCategory = customGroup;
                    this.SelectedFormat = customGroup.AddFormatInfo(formatString);
                }

                this.ActiveCellValue = this.activeWorksheet.Rows[this.activeSelection.ActiveCell.Row].Cells[this.activeSelection.ActiveCell.Column].Value;
            }
        }

        private bool IsSuperscript(FontSuperscriptSubscriptStyle style)
        {
            return style == FontSuperscriptSubscriptStyle.Superscript;
        }

        private bool IsSubscript(FontSuperscriptSubscriptStyle style)
        {
            return style == FontSuperscriptSubscriptStyle.Subscript;
        }

        private void ApplySuperscriptSubscriptStyle(SpreadsheetCellRangeFormat cellFormat)
        {

            if (this.Subscript.HasValue && this.Subscript == true)
            {
                cellFormat.Font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.Subscript;
            }
            else if (this.Superscript == true)
            {
                cellFormat.Font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.Superscript;
            }
            else
            {
                cellFormat.Font.SuperscriptSubscriptStyle = FontSuperscriptSubscriptStyle.None;
            }

        }

        private bool? ExcelDefaultableBooleanToNullableBool(ExcelDefaultableBoolean? value)
        {
            if (value == null)
            {
                return null;
            }
            else
            {
                return value.Value == ExcelDefaultableBoolean.True;
            }
        }

        private void EvaluateIsNormalFont()
        {
            IsNormalFont = fontSize == 11 && fontName == "Calibri" && fontStyle == FontStylesCustom.Regular;
        }

        private void ApplyFormattings()
        {
            foreach (var prop in this.modifiedProperties)
            {
                switch (prop)
                {
                    case HorizontalCellAlignmentPropertyName:
                        CellFormat.Alignment = HorizontalCellAlignment.Value;
                        break;
                    case VerticalCellAlignmentPropertyName:
                        CellFormat.VerticalAlignment = VerticalCellAlignment.Value;
                        break;
                    case IndentPropertyName:
                        if (Indent != null && Indent.HasValue)
                            CellFormat.Indent = Indent.Value;
                        break;
                    case WrapTextPropertyName:
                        CellFormat.WrapText = WrapText.Value ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                        break;
                    case ShrinkToFitPropertyName:
                        CellFormat.ShrinkToFit = ShrinkToFit.Value ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                        break;
                    case FontNamePropertyName:
                        if (!string.IsNullOrEmpty(FontName))
                            CellFormat.Font.Name = FontName;
                        break;
                    case FontSizePropertyName:
                        CellFormat.Font.Height = FontSize.Value * 20;
                        break;
                    case FontColorPropertyName:
                        CellFormat.Font.ColorInfo = new WorkbookColorInfo(FontColor);
                        break;
                    case StrikethroughPropertyName:
                        CellFormat.Font.Strikeout = this.Strikethrough.Value ? ExcelDefaultableBoolean.True : ExcelDefaultableBoolean.False;
                        break;
                    case SubscriptPropertyName:
                    case SuperscriptPropertyName:
                        ApplySuperscriptSubscriptStyle(CellFormat);
                        break;
                    case FontStylePropertyName:
                        switch (FontStyle)
                        {
                            case FontStylesCustom.Regular:
                                CellFormat.Font.Bold = ExcelDefaultableBoolean.False;
                                CellFormat.Font.Italic = ExcelDefaultableBoolean.False;
                                break;
                            case FontStylesCustom.Italic:
                                CellFormat.Font.Bold = ExcelDefaultableBoolean.False;
                                CellFormat.Font.Italic = ExcelDefaultableBoolean.True;
                                break;
                            case FontStylesCustom.Bold:
                                CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                                CellFormat.Font.Italic = ExcelDefaultableBoolean.False;
                                break;
                            case FontStylesCustom.BoldItalic:
                                CellFormat.Font.Bold = ExcelDefaultableBoolean.True;
                                CellFormat.Font.Italic = ExcelDefaultableBoolean.True;
                                break;
                        }
                        break;
                    case SelectedFormatPropertyName:
                        CellFormat.FormatString = SelectedFormat.Mask;
                        break;
                    case FillPropertyName:
                    case PatternColorPropertyName:
                    case FillPatternStylePropertyName:
                    case BackgroundColorPropertyName:
                        CellFormat.Fill = this.Fill;
                        break;
                    default:
                        break;
                }
            }
        }

        private void SetFill()
        {
            WorkbookColorInfo patternColor = WorkbookColorInfo.Automatic;
            WorkbookColorInfo backgroundColor = WorkbookColorInfo.Automatic;

            if (this.BackgroundColor.HasValue && this.BackgroundColor.Value != Colors.Transparent)
            {
                backgroundColor = new WorkbookColorInfo(this.BackgroundColor.Value);
            }

            if (this.PatternColor != null && this.PatternColor != Colors.Transparent)
            {
                patternColor = new WorkbookColorInfo(this.PatternColor.Value);
            }

            this.Fill = new CellFillPattern(backgroundColor, patternColor, this.FillPatternStyle);
        }

        private void ResolveBorders(SpreadsheetSelection selection, Worksheet worksheet)
        {
            if (selection.CellRanges[0].IsSingleCell)
            {
                var range = selection.CellRanges[0];
                var cellFormat = worksheet.Rows[range.FirstRow].Cells[range.FirstColumn].CellFormat;
                this.TopBorder = cellFormat.TopBorderStyle;
                this.BottomBorder = cellFormat.BottomBorderStyle;
                this.LeftBorder = cellFormat.LeftBorderStyle;
                this.RightBorder = cellFormat.RightBorderStyle;
                //this.DiagonalUpBorder = cellFormat.DiagonalBorders == DiagonalBorders.
                //this.RightBorder = cellFormat.RightBorderStyle;
            }
            foreach (var range in selection.CellRanges)
            {
                if (range.IsSingleCell)
                {
                }

                for (int i = range.FirstRow; i <= range.LastRow; i++)
                {
                    var row = worksheet.Rows[i];

                    for (int j = range.FirstColumn; j <= range.LastColumn; j++)
                    {
                        if (i == range.FirstRow)
                        {

                        }
                    }
                }
            }
        }

        private void HookPropertyChanged(bool hook)
        {
            if (hook)
            {
                this.PropertyChanged += OnPropertyChanged;
            }
            else
            {
                this.PropertyChanged -= OnPropertyChanged;
            }
        }

        #endregion //Private Methods

        #region OnPropertyChanged

        private void OnPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            HookPropertyChanged(false);

            try
            {
                if (this.trackChanges && !this.modifiedProperties.Contains(e.PropertyName))
                {
                    this.modifiedProperties.Add(e.PropertyName);
                }

                switch (e.PropertyName)
                {
                    case FontSizePropertyName:
                    case FontNamePropertyName:
                    case FontStylePropertyName:
                        EvaluateIsNormalFont();
                        break;
                    case SelectedNumberFormatCategoryPropertyName:
                        if (SelectedNumberFormatCategory != null)
                        {
                            this.SelectedFormat = this.SelectedNumberFormatCategory.Formats.FirstOrDefault();
                        }
                        break;

                    case BackgroundColorPropertyName:
                        if (this.BackgroundColor != null && this.BackgroundColor != Colors.Transparent)
                        {
                            if (this.FillPatternStyle == FillPatternStyle.None)
                                this.FillPatternStyle = FillPatternStyle.Solid;
                        }
                        else
                        {
                            this.FillPatternStyle = FillPatternStyle.None;
                        }
                        SetFill();
                        break;
                    case PatternColorPropertyName:
                    case FillPatternStylePropertyName:
                        SetFill();
                        break;

                }

            }
            catch { }

            HookPropertyChanged(true);
        }


        #endregion //OnPropertyChanged
    }
}
