using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using System.Windows;


namespace IgExcel.Infrastructure.Controls
{
    public class FormatPreviewControl : XamSpreadsheet
    {
        #region Constructor

        public FormatPreviewControl()
        {
            //Hide the UI elements 
            IsFormulaBarVisible = false;
            Workbook.WindowOptions.TabBarVisible = false;
            Workbook.WindowOptions.ScrollBars = ScrollBars.None;
            Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.VerticalAlignment = VerticalCellAlignment.Center;

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

        #region PreviewFormatMask

        public static readonly DependencyProperty PreviewFormatMaskProperty = DependencyProperty.Register(PreviewFormatMaskPropertyName, typeof(string), typeof(FormatPreviewControl), new PropertyMetadata(null, OnPreviewFormatMaskPropertyChanged));

        private static void OnPreviewFormatMaskPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FormatPreviewControl formatPreviewControl = (FormatPreviewControl)d;

            formatPreviewControl.Workbook.Worksheets[0].Rows[0].Cells[0].CellFormat.FormatString = (string)e.NewValue;
        }


        internal const string PreviewFormatMaskPropertyName = "PreviewFormatMask";

        public string PreviewFormatMask
        {
            get { return (string)this.GetValue(PreviewFormatMaskProperty); }
            set { this.SetValue(PreviewFormatMaskProperty, value); }
        }

        #endregion // PreviewFormatMask

        #region PreviewValue

        public static readonly DependencyProperty PreviewValueProperty = DependencyProperty.Register(PreviewValuePropertyName, typeof(object), typeof(FormatPreviewControl), new PropertyMetadata(null, OnPreviewValuePropertyChanged));

        private static void OnPreviewValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FormatPreviewControl formatPreviewControl = (FormatPreviewControl)d;

            formatPreviewControl.Workbook.Worksheets[0].Rows[0].Cells[0].Value = e.NewValue;
        }


        internal const string PreviewValuePropertyName = "PreviewValue";

        public object PreviewValue
        {
            get { return (object)this.GetValue(PreviewValueProperty); }
            set { this.SetValue(PreviewValueProperty, value); }
        }

        #endregion // PreviewValue
    }
}
