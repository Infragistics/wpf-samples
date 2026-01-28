using IGExcel.Resources; 
using Infragistics.Controls.Charts;
using Infragistics.Documents.Excel;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models.Common;
using Infragistics.Windows.DataPresenter;
using Infragistics.Windows.DataPresenter.ExcelExporter;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization; 
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data; 

namespace IGExcel.Samples.Data
{
    /// <summary>
    /// Interaction logic for SparklinesAndDataTrends.xaml
    /// </summary>
    public partial class SparklinesAndDataTrends : SampleContainer, INotifyPropertyChanged
    {
        Workbook wb;
        Worksheet sheet1;
        WorksheetCell sparklineDestination;
        string sparklineDataStart;
        string sparklineDataEnd;
        List<Tuple<string, DataRecord>> dataRange;
        List<Tuple<string, WorksheetCell, DataRecord>> sparklineDestinations;
        SparklineType sparklineType;

        public event PropertyChangedEventHandler PropertyChanged;
        private SparklineDisplayType _sparklineDisplayType;

        public SparklineDisplayType sparklineDisplayType
        {
            get { return this._sparklineDisplayType; }
            set
            {
                if (value != this._sparklineDisplayType)
                {
                    this._sparklineDisplayType = value;
                    OnPropertyChanged("sparklineDisplayType");
                }
            }
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }    
        
        public SparklinesAndDataTrends()
        {
            wb = new Workbook(WorkbookFormat.Excel2007);
            sheet1 = wb.Worksheets.Add("Sparklines");
            dataRange = new List<Tuple<string, DataRecord>>();
            sparklineDestinations = new List<Tuple<string, WorksheetCell, DataRecord>>();
            this.DataContext = this;
            InitializeComponent();
            InitializeSample();
            InitializeExport();
            InitializeSparklines();
        }

        private void InitializeSample()
        {
            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var dt = NWindDataSource.GetTable(NWindTable.Customers, cultureInfo);
            var relations = dt.DataSet.Relations;
            dt.DataSet.Relations.RemoveAt(1);
            this.XamDataGrid.DataSource = dt.DefaultView;
        }

        private void InitializeExport()
        {
            var exporter = new DataPresenterExcelExporter();
            exporter.CellExporting += Exporter_CellExporting;
            exporter.CellExported += Exporter_CellExported;
            var exportOptions = new ExportOptions();
            exportOptions.ChildRecordCollectionSpacing = ChildRecordCollectionSpacing.None;
            exporter.Export(this.XamDataGrid, wb, exportOptions);
        }
        
        private void InitializeSparklines()
        {
            var sparklines = sparklineDestinations;
            int group = 0;
            foreach (var parentRecord in sparklines)
            {
                sparklineDataStart = parentRecord.Item1;
                var groupedFreights = dataRange.GroupBy(d => ((DataRowView)d.Item2.DataItem)["CustomerID"]);
                var start = "";
                var end = "";
                string[] endwords = null;

                var record = parentRecord.Item3;
                if (record.HasChildren.Equals(false))
                {
                    continue;
                }
                else
                {
                    for (int i = 0; i < groupedFreights.Count();)
                    {
                        var groupedData = groupedFreights as IEnumerable<IGrouping<object, Tuple<string, DataRecord>>>;

                        if (group < groupedFreights.Count())
                        {
                            var item = groupedData.ToList()[group];
                            Tuple<string, DataRecord> dataStart = item.First();
                            Tuple<string, DataRecord> dataEnd = item.Last();

                            start = dataStart.Item1.ToString();
                            start = start.Replace("$", "");

                            end = dataEnd.Item1.ToString();
                            endwords = end.Split('$');
                            sparklineDataEnd = start + ":" + endwords[1] + endwords[2];
                        }
                        group++;
                        break;
                    }

                    var sparkline = wb.Worksheets[0].
                    SparklineGroups.Add(sparklineType, sparklineDataStart, sparklineDataEnd);
                    sparkline.DisplayBlanksAs = SparklineDisplayBlanksAs.Gap;
                    sparkline.DisplayHidden = true;
                }
            }
        }

        public void ResetSparklineType(SparklineType _type)
        {
            foreach (var sp in sheet1.SparklineGroups)
            {
                sp.Type = _type;
            }
        }

        private void Exporter_CellExported(object sender, CellExportedEventArgs e)
        {
            if (e.Field.Label.ToString().Equals("Freight expenses"))
            {
                if (e.Value == null)
                {                    
                    sparklineDestination = e.CurrentWorksheet.Rows[e.CurrentRowIndex].Cells[e.CurrentColumnIndex];
                    var rawDestination = sparklineDestination.ToString().Substring(0, (int)sparklineDestination.ToString().Length);
                    sparklineDestinations.Add(new Tuple<string, WorksheetCell, DataRecord>(rawDestination, sparklineDestination, e.Record));
                }
            }

            if (e.Field.Label.ToString().Equals("Freight"))
            {
                var datapoint = e.CurrentWorksheet.Rows[e.CurrentRowIndex].Cells[e.CurrentColumnIndex];
                var rawDestination = datapoint.ToString().Substring(0, (int)datapoint.ToString().Length);
                dataRange.Add(new Tuple<string, DataRecord>(datapoint.ToString(), e.Record));                
            }
        }

        private void Exporter_CellExporting(object sender, CellExportingEventArgs e)
        {
            if (e.Field.Label.ToString().Equals("Freight expenses"))
            {
                if (e.Value != null)
                {
                    e.Value = null;
                }              
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   
            if (wb != null)
            {
                SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook (*.xlsx)|*.xlsx", DefaultExt = ".xlsx" };
                sfd.AddExtension = true;
                if (sfd.ShowDialog() == true)
                {
                    try
                    {
                        try
                        {
                            wb.Save(sfd.FileName);

                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    catch (System.IO.IOException)
                    {
                        MessageBox.Show(String.Format("{0} {1}", ExcelStrings.NewColorModel_Export_WarningMessage, sfd.FileName));
                    }
                }
            }
        }

        private void XamDataGrid_InitializeRecord(object sender, Infragistics.Windows.DataPresenter.Events.InitializeRecordEventArgs e)
        {
            XamDataGrid dataGrid = e.Source as XamDataGrid;
            
            if (e.Record is DataRecord)
            {
                //get the data record and verify it is a parent row:
                DataRecord dr = (DataRecord)e.Record;
                if (dr.HasExpandableFieldRecords)
                {                    
                    List<int> values = new List<int>();
                    foreach (ExpandableFieldRecord row in dr.ChildRecords)
                    {                        
                        RecordCollectionBase recColBase = row.ChildRecords;
                        //go through all the child records
                        foreach (Record childRec in recColBase)
                        {
                            if (childRec.FieldLayout == dataGrid.FieldLayouts[1])
                            {
                                //and build value collections with Freight expenses for each:
                                values.Add((int)double.Parse((childRec as DataRecord).Cells["Freight"].Value.ToString()));
                            }                              
                        }
                        //build nice parsable string with the values:
                        StringBuilder sb = new StringBuilder();
                        values.ForEach(delegate (int value) { sb.Append(value.ToString() + " "); });
                        // set that string as value of the parent's Chart cell

                        if (dr.FieldLayout == dataGrid.FieldLayouts[0])
                        {
                            dr.Cells["Chart"].Value = sb.ToString();
                        }
                    }
                }
            }
        }

        private void ComboBoxChartType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            var cb = this.ComboBoxChartType as ComboBox;
            if (cb == null) return;

            switch (cb.SelectedIndex)
            {
                case 0:
                    sparklineDisplayType = SparklineDisplayType.Line;
                    sparklineType = SparklineType.Line;
                    ResetSparklineType(sparklineType);
                    break;
                case 1:
                    sparklineDisplayType = SparklineDisplayType.Column;
                    sparklineType = SparklineType.Column;
                    ResetSparklineType(sparklineType);
                    break;
                case 2:
                    sparklineDisplayType = SparklineDisplayType.WinLoss;
                    sparklineType = SparklineType.Stacked;
                    ResetSparklineType(sparklineType);
                    break;
            }
        }
    }

    public class SourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DataRecord record = value as DataRecord;
            //skip the first (header) cell
            if (record != null && record.DataItemIndex != -1)
            {
                //get the value, split it and parse each to int
                string values = (string)record.Cells["Chart"].Value;
                List<int> convertedValues = new List<int>();
                List<string> separates = values.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                convertedValues = separates.ConvertAll(x => int.Parse(x));
                //return the list with int values:
                return convertedValues;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
