using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using IGGrid.Resources;
using Infragistics.Controls.Grids;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Extensions;
using Infragistics.Samples.Shared.Models;

namespace IGGrid.Samples.Editing
{
    public partial class CopyPasteHelperMethods : SampleContainer
    {
        public CopyPasteHelperMethods()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.DownloadDataSource();
        }

        private void DownloadDataSource()
        {
            var _dataProvider = new XmlDataProvider();
            _dataProvider.GetXmlDataCompleted += OnDataProviderGetXmlDataCompleted;
            _dataProvider.GetXmlDataAsync("Products.xml");
        }

        private void OnDataProviderGetXmlDataCompleted(object sender, GetXmlDataCompletedEventArgs e)
        {
            XDocument doc = e.Result;
            var dataSource = (from d in doc.Descendants("Products")
                              select new Product
                              {
                                  Name = d.Element("ProductName").GetString(),
                                  Category = d.Element("Category").GetString(),
                                  UnitPrice = d.Element("UnitPrice").GetDouble(),
                                  UnitsInStock = d.Element("UnitsInStock").GetInt(),
                              });

            this.igGridSource.ItemsSource = dataSource.ToList();
        }

        private List<CellBase> previouslySelectedCells = null;
        private void igGridSource_ClipboardCopying(object sender, ClipboardCopyingEventArgs e)
        {
            // Get selected cells
            List<CellBase> selectedCells = new List<CellBase>(e.SelectedItems);

            // Clear the style of the previously selected cells
            if (previouslySelectedCells!= null && previouslySelectedCells.Count > 0)
            {
                this.SetCellStyle(previouslySelectedCells, null);
            }

            this.PrintLog("ClipboardCopying");

            // Check if the selected region of cells is valid for pasting
            bool IsValidSelectedRec = e.ValidateSelectedRectangle();
            
            this.PrintLog(GridStrings.XG_Paste_ValidateSelectedRectangle + " " + IsValidSelectedRec);

            if (IsValidSelectedRec)
            {
                // Color in blue the valid cell selection
                System.Windows.Style CellStyleResource = this.Resources["CopiedCellStyle"] as System.Windows.Style;
                this.SetCellStyle(selectedCells, CellStyleResource);
            }
            else
            {
                // Color in red the invalid cell selection
                System.Windows.Style CellStyleResource = this.Resources["InvalidSelectionCellStyle"] as System.Windows.Style;
                this.SetCellStyle(selectedCells, CellStyleResource);

                MessageBox.Show(GridStrings.XG_CopyPasteHelperMethods_Message1);

                // Cancel the copying event if the selected region of cells is not rectangular 
                e.Cancel = true;
            }

            previouslySelectedCells = selectedCells;
        }

        private void SetCellStyle(List<CellBase> cells, System.Windows.Style cellsStyle)
        {
            foreach (CellBase cell in cells)
            {
                // Set a new style to the copied cells
                cell.Style = cellsStyle;
            }
        }

        private void igGridSource_ClipboardPasting(object sender, ClipboardPastingEventArgs e)
        {
            this.PrintLog("ClipboardPasting");

            // Paste a rectangular selection of cells in the xamGrid
            e.PasteAsExcel();   
        }

        private void igGridSource_ClipboardPasteError(object sender, ClipboardPasteErrorEventArgs e)
        {
            this.PrintLog("ClipboardPasteError");
            
            // Get the type of the paste error
            string strErrorType = e.ErrorType.ToString();
            // Check if the pasting can continue after the error
            bool isRecoverableError = e.IsRecoverable;

            // Display the type of the error that occurs while pasting the data
            this.PrintLog(GridStrings.XG_Paste_ErrorType + " " + strErrorType);
            this.PrintLog(GridStrings.XG_CopyPasteHelperMethods_IsItRecoverable + " " + isRecoverableError);

            if (isRecoverableError)
            {
                MessageBoxButton button = MessageBoxButton.OKCancel;

                string errorMsg = string.Format(GridStrings.XG_CopyPasteHelperMethods_ErrorMessage, strErrorType);
                string questionMsg = GridStrings.XG_CopyPasteHelperMethods_Question;

                MessageBoxResult result = MessageBox.Show(errorMsg + "\n" + questionMsg, GridStrings.XG_CopyPasteHelperMethods_ErrorDialog, button);

                switch (result)
                { 
                    case MessageBoxResult.OK:
                        e.AttemptRecover = true;
                        break;
                    case MessageBoxResult.Cancel:
                        e.AttemptRecover = false;
                        break;
                }
            }
        }

        private void PrintLog(string msg)
        {
            string logMsg = msg + "\n";

            ListBoxItem lstBoxItem = new ListBoxItem
            {
                Content = logMsg,
                FontSize = 10,
                Height = 20
            };

            listBox.Items.Insert(0, lstBoxItem);
        }
        private void Btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            if (this.listBox.Items.Count > 0)
            {
                this.listBox.Items.Clear();
            }
        }
    }
}
