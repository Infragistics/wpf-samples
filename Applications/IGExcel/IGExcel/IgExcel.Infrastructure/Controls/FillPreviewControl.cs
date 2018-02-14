using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using System.Windows;
using System.Windows.Media;


namespace IgExcel.Infrastructure.Controls
{
    public class FillPreviewControl : XamSpreadsheet
    {
        #region Constructor

        public FillPreviewControl()
        {
            //Hide the UI elements 
            IsFormulaBarVisible = false;
            Workbook.WindowOptions.TabBarVisible = false;
            Workbook.WindowOptions.ScrollBars = ScrollBars.None;

            this.SizeChanged += (s, a) =>
            {
                Workbook.Worksheets[0].Rows[0].Height = (int)this.ActualHeight * 20;
                Workbook.Worksheets[0].SetDefaultColumnWidth(this.ActualWidth, WorksheetColumnWidthUnit.Pixel);
            };

            this.Loaded += (s, a) =>
            {
                AreGridlinesVisible = false;
                AreHeadersVisible = false;

                if (ActiveSelection != null)
                    ActiveSelection.SetActiveCell(new SpreadsheetCell(5, 5));
            };
        }


        #endregion //Constructor

        #region PreviewFill

        public static readonly DependencyProperty PreviewFillProperty = DependencyProperty.Register(PreviewFillPropertyName, typeof(CellFill), typeof(FillPreviewControl), new PropertyMetadata(null, OnPreviewFillPropertyChanged));

        private static void OnPreviewFillPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FillPreviewControl formatPreviewControl = (FillPreviewControl)d;
            formatPreviewControl.Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.Fill = (CellFill)e.NewValue;
        }

        internal const string PreviewFillPropertyName = "PreviewFill";

        public CellFill PreviewFill
        {
            get { return (CellFill)this.GetValue(PreviewFillProperty); }
            set { this.SetValue(PreviewFillProperty, value); }
        }

        #endregion // PreviewFill

    }
}
