// Infragistics
// NOTE: Modifying constants in this file will affect all IG Extensions projects.
//
namespace IGExtensions.Framework
{
    /// <summary>
    /// Provides information useful for setting up information for assemblies in Extensions projects: 
    /// <para>\Properties\AssemblyInfo.cs </para>
    /// </summary>
    public class AssemblyDesigner
    {

        #region Assembly Information

        // the Assembly Name for IG Extensions projects must be non-specific for targeted platform
#if WINDOWS_PHONE
        public const string AssemblyPlatformName = "WP7";
        public const string AssemblyNamePrefix = "Infragistics"; // + "WP";
        public const string AssemblyProductPrefix = "Infragistics ";
        public const string AssemblyProductSuffix = " for " + AssemblyPlatformName;
        public const string AssemblyDescrPrefix = "Infragistics ";
        public const string AssemblyDescrSuffix = " for " + AssemblyPlatformName;
#elif WPF
        public const string AssemblyPlatformName = "WPF";
        public const string AssemblyNamePrefix = "Infragistics"; // + "WPF";
        public const string AssemblyProductPrefix = "Infragistics ";
        public const string AssemblyProductSuffix = " for " + AssemblyPlatformName;
        public const string AssemblyDescrPrefix = "Infragistics ";
        public const string AssemblyDescrSuffix = " for " + AssemblyPlatformName;
#else
        public const string AssemblyPlatformName = "Silverlight";
        public const string AssemblyNamePrefix = "Infragistics"; // + "SL";
        public const string AssemblyProductPrefix = "Infragistics ";
        public const string AssemblyProductSuffix = " for " + AssemblyPlatformName;
        public const string AssemblyDescrPrefix = "Infragistics ";
        public const string AssemblyDescrSuffix = " for " + AssemblyPlatformName + " - " + Configuration + " Version ";
#endif

        public const string AssemblyNameSuffix = ".v" + AssemblyDesigner.AssemblyMajorMinor;

        /// <summary>
        /// The name of company
        /// </summary>
        public const string AssemblyCompanyName = "Infragistics, Inc.";
        public const string AssemblyCopyrightYear = "2018";
        public const string AssemblyCopyright = "Copyright © " + AssemblyCompanyName + " 2009 - " + AssemblyCopyrightYear;
        /// <summary>
        /// The major and minor build numbers of the assembly, e.g. 16.1
        /// </summary>
        public const string AssemblyMajorMinor = "18.1";
        /// <summary>
        /// The build number of the assembly, e.g. 20161
        /// </summary>
        public const string AssemblyBuild = "20181";
        /// <summary>
        /// The revision of the assembly.
        /// </summary>
        public const string AssemblyRevision = "1";
        /// <summary>
        /// The full version of the Assembly.
        /// </summary>
        public const string AssemblyVersion = AssemblyMajorMinor + "." + AssemblyBuild + "." + AssemblyRevision;

        #endregion

        // The current build configuration for the assembly.
#if DEBUG
        public const string Configuration = "Debug";
#elif TRIAL
    public const string Configuration = "Trial";
#elif BETA
    public const string Configuration = "CTP";
#else 
    public const string Configuration = "Release";
#endif

#if !DEBUG && !TRIAL && !Beta
    public const string ProductTitleSuffix = "";
#else
        public const string ProductTitleSuffix = " [" + AssemblyDesigner.Configuration + "]";
#endif


        public class AssemblyDesc
        {

            public const string BarcodesExtensions = AssemblyDesigner.AssemblyProductPrefix + "Barcodes" + AssemblyDesigner.AssemblyProductSuffix;
            public const string BarcodeReaderExtensions = AssemblyDesigner.AssemblyProductPrefix + "BarcodeReader" + AssemblyDesigner.AssemblyProductSuffix;

            public const string DataChartExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamDataChart" + AssemblyDesigner.AssemblyProductSuffix;
            public const string ChartExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamChart" + AssemblyDesigner.AssemblyProductSuffix;
            public const string WebChartExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamWebChart" + AssemblyDesigner.AssemblyProductSuffix;
            public const string FunnelChartExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamFunnelChart" + AssemblyDesigner.AssemblyProductSuffix;
            public const string PieChartExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamPieChart" + AssemblyDesigner.AssemblyProductSuffix;
            public const string SparklineExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamSparkline" + AssemblyDesigner.AssemblyProductSuffix;
            public const string TreemapExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamTreemap" + AssemblyDesigner.AssemblyProductSuffix;

            public const string BulletGraphExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamBulletGraph" + AssemblyDesigner.AssemblyProductSuffix;
            public const string RadialGaugeExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamRadialGauge" + AssemblyDesigner.AssemblyProductSuffix;
            public const string LinearGaugeExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamLinearGauge" + AssemblyDesigner.AssemblyProductSuffix;
            public const string SegmentedDisplayExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamSegmentedDisplay" + AssemblyDesigner.AssemblyProductSuffix;

            public const string MathExtensions = AssemblyDesigner.AssemblyProductPrefix + "Math" + AssemblyDesigner.AssemblyProductSuffix;
            public const string DragDropExtensions = AssemblyDesigner.AssemblyProductPrefix + "DragDrop" + AssemblyDesigner.AssemblyProductSuffix;
            public const string ExcelExtensions = AssemblyDesigner.AssemblyProductPrefix + "Excel" + AssemblyDesigner.AssemblyProductSuffix;
            public const string ResourceWasherExtensions = AssemblyDesigner.AssemblyProductPrefix + "ResourceWasher" + AssemblyDesigner.AssemblyProductSuffix;
            public const string PersistenceExtensions = AssemblyDesigner.AssemblyProductPrefix + "Persistence" + AssemblyDesigner.AssemblyProductSuffix;
            public const string CompressionExtensions = AssemblyDesigner.AssemblyProductPrefix + "Compression" + AssemblyDesigner.AssemblyProductSuffix;
            public const string VirtualCollectionExtensions = AssemblyDesigner.AssemblyProductPrefix + "VirtualCollection" + AssemblyDesigner.AssemblyProductSuffix;

            public const string CalendarExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamCalendar" + AssemblyDesigner.AssemblyProductSuffix;
            public const string ColorPickerExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamColorPicker" + AssemblyDesigner.AssemblyProductSuffix;
            public const string ComboEditorExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamComboEditor" + AssemblyDesigner.AssemblyProductSuffix;
            public const string MaskedInputExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamMaskedInput" + AssemblyDesigner.AssemblyProductSuffix;
            public const string CalculationManagerExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamCalculationManager" + AssemblyDesigner.AssemblyProductSuffix;
            public const string FormulaEditorExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamFormulaEditor" + AssemblyDesigner.AssemblyProductSuffix;
            public const string SpellCheckerExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamSpellChecker" + AssemblyDesigner.AssemblyProductSuffix;

            public const string MapExtensionsExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamMap" + AssemblyDesigner.AssemblyProductSuffix;
            public const string GeoMapExtensionsExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamMap" + AssemblyDesigner.AssemblyProductSuffix;
            public const string NetworkNodeExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamNetworkNode" + AssemblyDesigner.AssemblyProductSuffix;
            public const string OrgChartExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamOrgChart" + AssemblyDesigner.AssemblyProductSuffix;
            
            public const string PivotGridExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamPivotGrid" + AssemblyDesigner.AssemblyProductSuffix;
            public const string PivotDataSlicerExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamPivotDataSlicer" + AssemblyDesigner.AssemblyProductSuffix;
            public const string MultiColumnComboColumnExtensions = AssemblyDesigner.AssemblyProductPrefix + "MultiColumnComboColumn" + AssemblyDesigner.AssemblyProductSuffix;

            public const string DialogWindowExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamDialogWindow" + AssemblyDesigner.AssemblyProductSuffix;
            public const string SliderExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamSlider" + AssemblyDesigner.AssemblyProductSuffix;
            public const string HtmlViewerExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamHtmlViewer" + AssemblyDesigner.AssemblyProductSuffix;
            public const string ZoombarExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamZoombar" + AssemblyDesigner.AssemblyProductSuffix;

            public const string TileManagerExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamTileManager" + AssemblyDesigner.AssemblyProductSuffix;
            public const string DockManagerExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamDockManager" + AssemblyDesigner.AssemblyProductSuffix;

            public const string ContextMenuExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamContextMenu" + AssemblyDesigner.AssemblyProductSuffix;
            public const string DataTreeExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamDataTree" + AssemblyDesigner.AssemblyProductSuffix;
            public const string MenuExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamMenu" + AssemblyDesigner.AssemblyProductSuffix;
            public const string OutlookBarExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamOutlookBar" + AssemblyDesigner.AssemblyProductSuffix;
            public const string RibbonExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamRibbon" + AssemblyDesigner.AssemblyProductSuffix;
            public const string TagCloudExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamTagCloud" + AssemblyDesigner.AssemblyProductSuffix;
            public const string TreeExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamTree" + AssemblyDesigner.AssemblyProductSuffix;

            public const string SchedulesExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamSchedules" + AssemblyDesigner.AssemblyProductSuffix;

            public const string TimelineExtensions = AssemblyDesigner.AssemblyProductPrefix + "XamTimeline" + AssemblyDesigner.AssemblyProductSuffix;

        }


    }
 
}
