# .NET8.0 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET8.0 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET8.0 upgrade.
3. Upgrade Infragistics.Samples.Framework\Infragistics.Samples.Framework.WPF.csproj
4. Upgrade Infragistics.Samples.Assets\Infragistics.Samples.Assets.WPF.csproj
5. Upgrade Infragistics.Samples.Shared\Infragistics.Samples.Shared.WPF.csproj
6. Upgrade Infragistics.Samples.Services\Infragistics.Samples.Services.WPF.csproj
7. Upgrade IGSpreadsheet\IGSpreadsheet.WPF.csproj
8. Upgrade IGSparkline\IGSparkline.WPF.csproj
9. Upgrade IGZoombar\IGZoombar.WPF.csproj
10. Upgrade IGWord\IGWord.WPF.csproj
11. Upgrade IGUndoRedoFramework\IGUndoRedoFramework.WPF.csproj
12. Upgrade IGTreemap\IGTreemap.WPF.csproj
13. Upgrade IGTreeGrid\IGTreeGrid.WPF.csproj
14. Upgrade IGToolbar\IGToolbar.WPF.csproj
15. Upgrade IGTimeline\IGTimeline.WPF.csproj
16. Upgrade IGTileManager\IGTileManager.WPF.csproj
17. Upgrade IGThemeManager\IGThemeManager.WPF.csproj
18. Upgrade IGTagCloud\IGTagCloud.WPF.csproj
19. Upgrade IGTabControl\IGTabControl.WPF.csproj
20. Upgrade IGSyntaxParsingEngine\IGSyntaxParsingEngine.WPF.csproj
21. Upgrade IGSyntaxEditor\IGSyntaxEditor.WPF.csproj
22. Upgrade IGSurfaceChart\IGSurfaceChart.WPF.csproj
23. Upgrade IGSpellChecker\IGSpellChecker.WPF.csproj
24. Upgrade IGSlider\IGSlider.WPF.csproj
25. Upgrade IGShapeChart\IGShapeChart.csproj
26. Upgrade IGSchedule\IGSchedule.WPF.csproj
27. Upgrade IGRichTextEditor\IGRichTextEditor.WPF.csproj
28. Upgrade IGRibbon\IGRibbon.WPF.csproj
29. Upgrade IGResourceWasher\IGResourceWasher.WPF.csproj
30. Upgrade IGReporting\IGReporting.WPF.csproj
31. Upgrade IGRadialMenu\IGRadialMenu.WPF.csproj
32. Upgrade IGRadialGauge\IGRadialGauge.WPF.csproj
33. Upgrade IGPropertyGrid\IGPropertyGrid.WPF.csproj
34. Upgrade IGPivotGrid\IGPivotGrid.WPF.csproj
35. Upgrade IGPieChart\IGPieChart.WPF.csproj
36. Upgrade IGPersistenceFramework\IGPersistenceFramework.WPF.csproj
37. Upgrade IGOutlookBar\IGOutlookBar.WPF.csproj
38. Upgrade IGOrgChart\IGOrgChart.WPF.csproj
39. Upgrade IGBulletGraph\IGBulletGraph.WPF.csproj
40. Upgrade IGNetworkNode\IGNetworkNode.WPF.csproj
41. Upgrade IGMultiColumnComboEditor\IGMultiColumnComboEditor.WPF.csproj
42. Upgrade IGMonthCalendar\IGMonthCalendar.WPF.csproj
43. Upgrade IGMenu\IGMenu.WPF.csproj
44. Upgrade IGMath\IGMath.WPF.csproj
45. Upgrade IGMap\IGMap.WPF.csproj
46. Upgrade IGLinearGauge\IGLinearGauge.WPF.csproj
47. Upgrade IGGeographicMap\IGGeographicMap.WPF.csproj
48. Upgrade IGGantt\IGGantt.WPF.csproj
49. Upgrade IGFunnelChart\IGFunnelChart.WPF.csproj
50. Upgrade IGFormulaEditor\IGFormulaEditor.WPF.csproj
51. Upgrade IGFinancialChart\IGFinancialChart.csproj
52. Upgrade IGExcel\IGExcel.WPF.csproj
53. Upgrade IGEditors\IGEditors.WPF.csproj
54. Upgrade IGDragDropFramework\IGDragDropFramework.WPF.csproj
55. Upgrade IGDoughnutChart\IGDoughnutChart.WPF.csproj
56. Upgrade IGDockManager\IGDockManager.WPF.csproj
57. Upgrade IGDialogWindow\IGDialogWindow.WPF.csproj
58. Upgrade IGDiagram\IGDiagram.WPF.csproj
59. Upgrade IGDataTree\IGDataTree.WPF.csproj
60. Upgrade IGDataPresenter\IGDataPresenter.WPF.csproj
61. Upgrade IGDataGrid\IGDataGrid.WPF.csproj
62. Upgrade IGDataChart\IGDataChart.WPF.csproj
63. Upgrade IGDataCarousel\IGDataCarousel.WPF.csproj
64. Upgrade IGDataCards\IGDataCards.WPF.csproj
65. Upgrade IGDashboardTile\IGDashboardTile.csproj
66. Upgrade IGContextMenu\IGContextMenu.WPF.csproj
67. Upgrade IGComboEditor\IGComboEditor.WPF.csproj
68. Upgrade IGColorPicker\IGColorPicker.WPF.csproj
69. Upgrade IGCarouselPanel\IGCarouselPanel.WPF.csproj
70. Upgrade IGCarouselListBox\IGCarouselListBox.WPF.csproj
71. Upgrade IGCalendar\IGCalendar.WPF.csproj
72. Upgrade IGDialogWindow\IGDialogWindow.WPF.csproj
73. Upgrade IGPieChart\IGPieChart.WPF.csproj
74. Upgrade IGBarcode\IGBarcode.WPF.csproj
75. Upgrade IGBarcodeReader\IGBarcodeReader.WPF.csproj
76. Upgrade IGDataPieChart\IGDataPieChart.WPF.csproj
77. Upgrade Infragistics.SamplesBrowser.WPF\Infragistics.SamplesBrowser.WPF.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name | Description |
|:------------|:-----------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name | Current Version | New Version | Description |
|:-------------|:---------------:|:-----------:|:------------|
| Infragistics.WPF.Trial |25.1.117 | | No supported version for .NET8.0; remove |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### Project modifications (applies to all listed projects)

Project properties changes:
 - Convert project file to SDK-style
 - Target framework should be changed from `net47` to `net8.0-windows`

NuGet packages changes:
 - Keep existing Infragistics assemblies in place; do not change assembly names
 - Remove `Infragistics.WPF.Trial` meta-package

Other changes:
 - Ensure `Microsoft.NET.Sdk.WindowsDesktop` and `UseWPF` are present in each WPF project
 - Preserve assembly names and root namespaces
