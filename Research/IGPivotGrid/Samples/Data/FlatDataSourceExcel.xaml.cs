using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Resources;
using IGPivotGrid.Resources;
using Infragistics.Olap;
using Infragistics.Olap.Excel;
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Framework;

namespace IGPivotGrid.Samples.Data
{
    public partial class FlatDataSourceExcel : SampleContainer
    {
        public FlatDataSourceExcel()
        {
            InitializeComponent();
        }

        private void LoadExcelFileData(object sender, RoutedEventArgs e)
        {
            string pathToDataFile = "/IGPivotGrid;component/Assets/EN/DataToAnalyze.xls";
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == "ja-JP")
                pathToDataFile = "/IGPivotGrid;component/Assets/JA/DataToAnalyze.xls";

            StreamResourceInfo sri = Application.GetResourceStream(new Uri(pathToDataFile, UriKind.RelativeOrAbsolute));
            FlatDataSource dataSource = CreateDataSource(sri.Stream);
            dataSource.PropertyChanged += DataSource_PropertyChanged;
            this.pivotGrid.DataSource = dataSource;
            this.dataSelector.DataSource = dataSource;
        }

        private FlatDataSource CreateDataSource(Stream fileStream)
        {
            FlatDataSource flatDataSource = new FlatDataSource();
            ExcelDataConnectionSettings excelDataSettings = new ExcelDataConnectionSettings
            {
                FileStream = fileStream,
                GeneratedTypeName = PivotGridStrings.XPG_ExcelFileDataSource_InitialCube,
                WorksheetName = PivotGridStrings.XPG_ExcelFileDataSource_WorksheetName
            };
            ExcelDataConnectionSettings.AddExcelCellFormatMapping(
                PivotGridStrings.XPG_ExcelFileDataSource_ExcelFormatSrc,
                PivotGridStrings.XPG_ExcelFileDataSource_ExcelFormatDest
            );

            flatDataSource.Cube = DataSourceBase.GenerateInitialCube(
                PivotGridStrings.XPG_ExcelFileDataSource_InitialCube
            );
            flatDataSource.Rows = DataSourceBase.GenerateInitialItems(
                PivotGridStrings.XPG_ExcelFileDataSource_InitialRows
            );
            flatDataSource.Columns = DataSourceBase.GenerateInitialItems(
                PivotGridStrings.XPG_ExcelFileDataSource_InitialColumns
            );
            flatDataSource.Measures = DataSourceBase.GenerateInitialItems(
                PivotGridStrings.XPG_ExcelFileDataSource_InitialMeasures
            );

            CubeMetadata cubeMetadata = new CubeMetadata()
            {
                DataTypeFullName = PivotGridStrings.XPG_ExcelFileDataSource_InitialCube,
                DisplayName = PivotGridStrings.XPG_ExcelFileDataSource_CubeDisplayName,
            };
            cubeMetadata.DimensionSettings.Add(new DimensionMetadata()
            {
                SourcePropertyName =
                    PivotGridStrings.XPG_ExcelFileDataSource_DimSettings_Units_SrcPropName,
                DisplayName =
                    PivotGridStrings.XPG_ExcelFileDataSource_DimSettings_DisplayName,
                DisplayFormat =
                    PivotGridStrings.XPG_ExcelFileDataSource_DimSettings_DisplayFormat
            });

            flatDataSource.ConnectionSettings = excelDataSettings;
            flatDataSource.CubesSettings.Add(cubeMetadata);

            // This will add base level for all string based dimensions
            HierarchyDescriptor stringDataDescriptor = new HierarchyDescriptor
            {
                AppliesToPropertiesOfType = typeof(string)
            };
            stringDataDescriptor.AddLevel<string>(s =>
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_All,
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_AllValues
            );
            stringDataDescriptor.AddLevel<string>(s =>
                s,
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_Members
            );

            HierarchyDescriptor dateTimeDataDescriptor = new HierarchyDescriptor
            {
                AppliesToPropertiesOfType = typeof(DateTime)
            };
            dateTimeDataDescriptor.AddLevel<DateTime>(date =>
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_AllDates,
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_AllValues
            );
            dateTimeDataDescriptor.AddLevel<DateTime>(date =>
                date.Year,
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_Years
            );
            dateTimeDataDescriptor.AddLevel<DateTime>(date =>
                date.QuarterShort(),
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_Quarters
            );
            dateTimeDataDescriptor.AddLevel<DateTime>(date =>
                date.Date,
                PivotGridStrings.XPG_ExcelFileDataSource_HierarchyDesc_Members
            );

            flatDataSource.HierarchyDescriptors.Add(stringDataDescriptor);
            flatDataSource.HierarchyDescriptors.Add(dateTimeDataDescriptor);

            return flatDataSource;
        }

        void DataSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Processing")
            {
                isBusyIndicator.Visibility = pivotGrid.DataSource.IsBusy ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
