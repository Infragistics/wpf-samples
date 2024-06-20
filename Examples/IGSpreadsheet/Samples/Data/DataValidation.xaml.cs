using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using System.Windows;
using IGSpreadsheet.Resources;
using IGSpreadsheet.Shared;
using System.Threading;

namespace IGSpreadsheet.Samples.Data
{
    public partial class DataValidation : SampleContainer
    {
        public DataValidation()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            Utils.SetDefaultDialogTitle(this.xamSpreadsheet1);
  
            // overriding default theme
                 
        }

        public void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.xamSpreadsheet1.Workbook.Worksheets[0].Columns[0].SetWidth(25, WorksheetColumnWidthUnit.Character);
            this.xamSpreadsheet1.Workbook.Worksheets[0].Columns[1].SetWidth(15, WorksheetColumnWidthUnit.Character);

            // this validation rule has only input message set
            AnyValueDataValidationRule valRule1 = new AnyValueDataValidationRule();
            valRule1.InputMessageTitle = SpreadsheetStrings.formTitle;
            valRule1.InputMessageDescription = SpreadsheetStrings.formDescription;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[1].Cells[0].DataValidationRule = valRule1;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[1].Cells[0].Value = SpreadsheetStrings.formTitle;

            // this validation rule has a two constraint validation set
            TwoConstraintDataValidationRule valRule2 = new TwoConstraintDataValidationRule();
            valRule2.ValidationOperator = TwoConstraintDataValidationOperator.Between;
            valRule2.SetLowerConstraint(1);
            valRule2.SetUpperConstraint(4);
            valRule2.InputMessageTitle = SpreadsheetStrings.adultsInputTitle;
            valRule2.InputMessageDescription = SpreadsheetStrings.adultsInputDescription;
            valRule2.ErrorMessageTitle = SpreadsheetStrings.errorMessage1Title;
            valRule2.ErrorMessageDescription = SpreadsheetStrings.errorMessage1Description;
            valRule2.ErrorStyle = DataValidationErrorStyle.Information;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[3].Cells[1].DataValidationRule = valRule2;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[3].Cells[1].Value = 1;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[3].Cells[0].Value = SpreadsheetStrings.adultsInputTitle;

            // this validation rule has a custom formula validation set
            CustomDataValidationRule valRule3 = new CustomDataValidationRule();
            string separator = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator == "." ? "," : ";";
            valRule3.SetFormula(string.Format("=AND((B4+B5)<5 {0} (B4+B5)>0)", separator), null);
            valRule3.InputMessageTitle = SpreadsheetStrings.childrenInputTitle;
            valRule3.InputMessageDescription = SpreadsheetStrings.childrenInputDescription;
            valRule3.ErrorMessageTitle = SpreadsheetStrings.errorMessage1Title;
            valRule3.ErrorMessageDescription = SpreadsheetStrings.errorMessage1Description;
            valRule3.ErrorStyle = DataValidationErrorStyle.Warning;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[4].Cells[1].DataValidationRule = valRule3;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[4].Cells[1].Value = 0;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[4].Cells[0].Value = SpreadsheetStrings.childrenInputTitle;

            // this validation rule has a list of accepted choices validation set
            ListDataValidationRule valRule4 = new ListDataValidationRule();
            valRule4.SetValues(new object[] { "FB", "HB", "BB" });
            valRule4.InputMessageTitle = SpreadsheetStrings.servicingTitle;
            valRule4.InputMessageDescription = SpreadsheetStrings.servicingDescription;
            valRule4.ErrorMessageTitle = SpreadsheetStrings.servicingErrorTitle;
            valRule4.ErrorMessageDescription = SpreadsheetStrings.servicingErrorDescription;
            valRule4.ErrorStyle = DataValidationErrorStyle.Stop;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[5].Cells[1].DataValidationRule = valRule4;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[5].Cells[1].Value = "FB";
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[5].Cells[0].Value = SpreadsheetStrings.servicingTitle;

            // this validation rule has a single constraint validation set
            OneConstraintDataValidationRule valRule5 = new OneConstraintDataValidationRule();
            valRule5.InputMessageTitle = SpreadsheetStrings.workTimeTitle;
            valRule5.InputMessageDescription = SpreadsheetStrings.workTimeDescription;
            valRule5.ValidationOperator = OneConstraintDataValidationOperator.GreaterThanOrEqualTo;
            valRule5.SetConstraint(new System.DateTime(2015, 4, 1));
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[6].Cells[1].DataValidationRule = valRule5;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[6].Cells[1].Value = new System.DateTime(2015, 4, 1);
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[6].Cells[0].Value = SpreadsheetStrings.timeStart;

            // this validation rule has a single constraint validation set
            OneConstraintDataValidationRule valRule6 = new OneConstraintDataValidationRule();
            valRule6.InputMessageTitle = SpreadsheetStrings.workTimeTitle2;
            valRule6.InputMessageDescription = SpreadsheetStrings.workTimeDescription2;
            valRule6.ValidationOperator = OneConstraintDataValidationOperator.GreaterThan;
            valRule6.SetConstraintFormula("=B7", null);
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[7].Cells[1].DataValidationRule = valRule6;
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[7].Cells[1].Value = new System.DateTime(2015, 10, 31);
            this.xamSpreadsheet1.Workbook.Worksheets[0].Rows[7].Cells[0].Value = SpreadsheetStrings.timeEnd;
        }
    }
}
