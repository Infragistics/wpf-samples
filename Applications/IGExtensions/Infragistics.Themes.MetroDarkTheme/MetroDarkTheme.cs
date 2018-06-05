using System.Reflection;

namespace Infragistics.Themes
{
    public class MetroDarkTheme : BuiltInThemeBase
    {
        protected override void ConfigureControlMappings()
        {
            string assemblyFullName = Assembly.GetExecutingAssembly().FullName;

            #region XamComboEditors Shared
            this.Mappings.Add(ControlMappingKeys.XamComboEditor_Shared,
        BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamComboEditor.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamMultiColumnComboEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamMultiColumnCombo.xaml"));
            #endregion //XamComboEditors Shared

            #region XamMenu & XamContextMenu
            this.Mappings.Add(ControlMappingKeys.XamMenu,
        BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamMenu.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamContextMenu,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamMenu.xaml"));
            #endregion //XamMenu & XamContextMenu

            #region WpfOnly_Editors
            this.Mappings.Add(ControlMappingKeys.XamNumericEditor,
        BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamEditors.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamMaskedEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamEditors.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDateTimeEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamEditors.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamCurrencyEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamEditors.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamComboEditor_WpfOnly,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamEditors.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamTextEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamEditors.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamCheckEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamEditors.xaml"));
            #endregion //WpfOnly_Editors

            #region DataPresenter
            this.Mappings.Add(ControlMappingKeys.XamDataPresenter,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamDataPresenter.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDataGrid,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamDataPresenter.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDataCards,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamDataPresenter.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDataCarousel,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamDataPresenter.xaml"));

            // SS 01/24/16 TFS230613 - Add mapping for the XamTreeGrid
            this.Mappings.Add(ControlMappingKeys.XamTreeGrid,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamDataPresenter.xaml"));
            #endregion

            #region Gauges
            this.Mappings.Add(ControlMappingKeys.XamBulletGraph,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamGauges.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamLinearGauge,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamGauges.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamRadialGauge,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamGauges.xaml")); 
            #endregion //Gauges

            this.Mappings.Add(ControlMappingKeys.XamGrid,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamGrid.xaml"));

            this.Mappings.Add(ControlMappingKeys.MsCoreControls, BuildLocationString(assemblyFullName,
                                        @"Themes\MetroDark.MSControls.Core.Implicit.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamRibbon,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.XamRibbon.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDataChart,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDataChart.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamRadialMenu,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamRadialMenu.xaml"));


            this.Mappings.Add(ControlMappingKeys.XamCalendar,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamCalendar.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamTileManager,
               BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamTileManager.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamGantt,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamGantt.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamColorPicker,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamColorPicker.xaml"));

            #region Sliders
            this.Mappings.Add(ControlMappingKeys.XamNumericSlider,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSlider.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDateTimeSlider,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSlider.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamNumericRangeSlider,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSlider.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDateTimeRangeSlider,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSlider.xaml"));
            #endregion Sliders

            this.Mappings.Add(ControlMappingKeys.XamDialogWindow,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDialogWindow.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamSpellChecker,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSpellChecker.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDataTree,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDataTree.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamTagCloud,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamTagCloud.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamOutlookBar,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamOutlookBar.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDockManager,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDockManager.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamTabControl,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.WPF.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamPivotGrid,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamPivotGrid.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamPivotDataSlicer,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamPivotDataSlicer.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamZoombar,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.DataVisualization.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamOverviewPlusDetailPane,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.DataVisualization.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamFormulaEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamFormulaEditor.xaml"));

            #region Inputs
            this.Mappings.Add(ControlMappingKeys.XamMaskedInput,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamMaskedInput.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamNumericInput,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamMaskedInput.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamCurrencyInput,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamMaskedInput.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDateTimeInput,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDateTimeInput.xaml"));
            #endregion //Inputs

            this.Mappings.Add(ControlMappingKeys.XamRichTextEditor,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamRichTextEditor.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamMonthCalendar,
               BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamEditors.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamSyntaxEditor,
               BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSyntaxEditor.xaml"));

            #region Charts
            this.Mappings.Add(ControlMappingKeys.XamDoughnutChart,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDataChart.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamFunnelChart,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDataChart.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamPieChart,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDataChart.xaml"));
            #endregion //Charts

            this.Mappings.Add(ControlMappingKeys.XamSparkline,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSparkline.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamCarouselListBox,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.WPF.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamCarouselPanel,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.WPF.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDiagram,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamDiagram.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamSpreadsheet,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSpreadsheet.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamPropertyGrid,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamPropertyGrid.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamTimeline,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamTimeline.xaml"));

            #region Treemap
            this.Mappings.Add(ControlMappingKeys.XamTreemap,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamTreemap.xaml"));

            this.Mappings.Add(ControlMappingKeys.TreemapNode,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamTreemap.xaml"));
            #endregion //Treemap

            this.Mappings.Add(ControlMappingKeys.XamGeographicMap,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamGeographicMap.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamMap,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamMap.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamNetworkNode,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamNetworkNode.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamOrgChart,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamOrgChart.xaml"));

            #region Schedule
            this.Mappings.Add(ControlMappingKeys.XamDateNavigator,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSchedule.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamDayView,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSchedule.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamMonthView,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSchedule.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamOutlookCalendarView,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSchedule.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamScheduleView,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamSchedule.xaml"));

            #endregion // Schedule

            this.Mappings.Add(ControlMappingKeys.XamOlapPieChart,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.OlapCharts.xaml"));

            this.Mappings.Add(ControlMappingKeys.OlapXAxis,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.OlapCharts.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamBusyIndicator,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.WPF.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamScatterSurface3D,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamScatterSurface3D.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamFinancialChart,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamFinancialChart.xaml"));

            this.Mappings.Add(ControlMappingKeys.XamZoomSlider,
                BuildLocationString(assemblyFullName, @"Themes\MetroDark.xamZoomSlider.xaml"));
        }
    }
}
