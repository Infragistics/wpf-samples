using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Infragistics.Samples.Browser
{
    public class IgTargetAssemblies
    {
        public string VersionNumber { get; set; }
        public string Directory { get; set; }
        
        public string VersionPrefix { get; set; }
        public string VersionSuffix { get; set; }
        public IgTargetBuild Build { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
    }

    public enum IgTargetBuild
    {
        Nuget,
        Local,
        Production,
    }


    public static class ProjectScript
    {
        private static Dictionary<IgTargetBuild, IgTargetAssemblies> Config = new Dictionary<IgTargetBuild, IgTargetAssemblies>();
        private static string ProjectDirectory = "";
        private static IgTargetAssemblies Target;
        //private static string AssemblyDirectory = "C:\\WORK\\dev-tools\\XAML\\Main\\Source\\Build";
        //private static string AssemblyDirectory = "C:\\WORK\\_ASSAMBLIES\\WPF\\25.2";

        private static void Initalize()
        {

            if (string.IsNullOrEmpty(ProjectDirectory))
            {
                //ProjectDirectory = "C:\\WORK\\wpf-samples\\Research\\IGDiagram";
                ProjectDirectory = "C:\\WORK\\wpf-samples\\Research";
            }

            var version = "25.1";
            var nugetAssemblies = new IgTargetAssemblies()
            {
                VersionNumber = "25.1.117",
                VersionPrefix = ".Trial.",
                VersionSuffix = "\\lib\\net40",
                Build = IgTargetBuild.Nuget,
                Directory = "..\\packages",
                Prefix = "InfragisticsWPF",
                Suffix = ".dll",
            };

            var localAssemblies = new IgTargetAssemblies()
            {
                VersionNumber = version,
                Build = IgTargetBuild.Local,
                Directory = "C:\\WORK\\dev-tools\\XAML\\Main\\Source\\Build", 
                Prefix = "InfragisticsWPF4",
                Suffix = ".dll",
            };

            var prodAssemblies = new IgTargetAssemblies()
            {
                VersionNumber = version,
                Build = IgTargetBuild.Production,
                Directory = "C:\\WORK\\_ASSAMBLIES\\WPF\\", 
                Prefix = "InfragisticsWPF4",
                Suffix = ".dll",
            };

            Config = new Dictionary<IgTargetBuild, IgTargetAssemblies>();
            Config.Add(IgTargetBuild.Nuget, nugetAssemblies);
            Config.Add(IgTargetBuild.Local, localAssemblies);
            Config.Add(IgTargetBuild.Production, prodAssemblies);

            Target = nugetAssemblies;
            //Target = localAssemblies;
            //Target = prodAssemblies;

        }


        private static Dictionary<string, string> NugetAssembliesToDir = new Dictionary<string, string>()
        {
{ "InfragisticsWPF.Controls.Barcodes.BarcodeReader", "Infragistics.WPF.BarcodeReader" },
{ "InfragisticsWPF.Controls.Barcodes", "Infragistics.WPF.Barcodes" },
{ "InfragisticsWPF.Calculations.XamCalculationManager", "Infragistics.WPF.CalculationManager" },
{ "InfragisticsWPF.Controls.Editors.XamCalendar", "Infragistics.WPF.Calendar" },
{ "InfragisticsWPF.Controls.Charts.Olap", "Infragistics.WPF.Charts.Olap" },
{ "InfragisticsWPF.Controls.Charts.XamDataChart", "Infragistics.WPF.Charts" },
{ "InfragisticsWPF.Controls.Editors.XamColorPicker", "Infragistics.WPF.ColorPicker" },
{ "InfragisticsWPF.Controls.Grids.XGrid", "Infragistics.WPF.Controls.Grids.XamXGrid" },
{ "InfragisticsWPF.Controls.Dashboards", "Infragistics.WPF.Dashboards" },
{ "InfragisticsWPF.DataPresenter.CalculationAdapter", "Infragistics.WPF.DataGrids.Calculation" },
{ "InfragisticsWPF.DataPresenter.ExcelExporter", "Infragistics.WPF.DataGrids.Excel" },
{ "InfragisticsWPF.DataPresenter", "Infragistics.WPF.DataGrids" },
{ "InfragisticsWPF.DataPresenter.WordWriter", "Infragistics.WPF.DataGrids.Word" },
{ "InfragisticsWPF.Controls.Menus.XamDataTree", "Infragistics.WPF.DataTree" },
{ "InfragisticsWPF.Controls.Charts.XamDiagram", "Infragistics.WPF.Diagram" },
{ "InfragisticsWPF.Controls.Interactions.XamDialogWindow", "Infragistics.WPF.DialogWindow" },
{ "InfragisticsWPF.Documents.Excel", "Infragistics.WPF.Excel" },
{ "InfragisticsWPF.Controls.Charts.XamFinancialChart", "Infragistics.WPF.FinancialChart" },
{ "InfragisticsWPF.Controls.Interactions.XamFormulaEditor", "Infragistics.WPF.FormulaEditor" },
{ "InfragisticsWPF.Controls.Schedules.XamGantt", "Infragistics.WPF.Gantt" },
{ "InfragisticsWPF.Controls.Gauges", "Infragistics.WPF.Gauges" },
{ "InfragisticsWPF.Controls.Maps.XamGeographicMap", "Infragistics.WPF.GeographicMap" },
{ "InfragisticsWPF.Controls.Inputs", "Infragistics.WPF.Inputs" },
{ "InfragisticsWPF.Controls.Layouts", "Infragistics.WPF.Layouts" },
{ "InfragisticsWPF.Controls.Menus.XamMenu", "Infragistics.WPF.Menus" },
{ "InfragisticsWPF.Controls.Maps.XamNetworkNode", "Infragistics.WPF.NetworkNode" },
{ "InfragisticsWPF.Controls.Maps.XamOrgChart", "Infragistics.WPF.OrgChart" },
{ "InfragisticsWPF.Controls.Grids.XamPivotDataSlicer", "Infragistics.WPF.PivotDataSlicer" },
{ "InfragisticsWPF.Controls.Grids.XamPivotGrid", "Infragistics.WPF.PivotGrid" },
{ "InfragisticsWPF.Controls.Editors.XamPropertyGrid", "Infragistics.WPF.PropertyGrid" },
{ "InfragisticsWPF.Controls.Menus.XamRadialMenu", "Infragistics.WPF.RadialMenu" },
{ "InfragisticsWPF.Documents.RichTextDocument.Html", "Infragistics.WPF.RichTextDocument.Html" },
{ "InfragisticsWPF.Documents.RichTextDocument.Rtf", "Infragistics.WPF.RichTextDocument.Rtf" },
{ "InfragisticsWPF.Documents.RichTextDocument", "Infragistics.WPF.RichTextDocument" },
{ "InfragisticsWPF.Documents.RichTextDocument.Word", "Infragistics.WPF.RichTextDocument.Word" },
{ "InfragisticsWPF.Controls.Editors.XamRichTextEditor", "Infragistics.WPF.RichTextEditor" },
{ "InfragisticsWPF.Controls.SchedulesDialogs", "Infragistics.WPF.Schedules.Dialogs" },
{ "InfragisticsWPF.Controls.SchedulesExchangeConnector", "Infragistics.WPF.Schedules.Exchange" },
{ "InfragisticsWPF.Controls.Schedules", "Infragistics.WPF.Schedules" },
{ "InfragisticsWPF.Controls.Editors.XamSlider", "Infragistics.WPF.Slider" },
{ "InfragisticsWPF.Controls.Charts.XamSparkline", "Infragistics.WPF.Sparkline" },
{ "InfragisticsWPF.Controls.Interactions.XamSpellChecker", "Infragistics.WPF.SpellChecker" },
{ "InfragisticsWPF.Controls.Grids.XamSpreadsheet.ChartAdapter", "Infragistics.WPF.Spreadsheet.Charts" },
{ "InfragisticsWPF.Controls.Grids.XamSpreadsheet", "Infragistics.WPF.Spreadsheet" },
{ "InfragisticsWPF.Controls.Charts.XamSurfaceChart3D", "Infragistics.WPF.SurfaceChart3D" },
{ "InfragisticsWPF.Controls.Editors.XamSyntaxEditor", "Infragistics.WPF.SyntaxEditor" },
{ "InfragisticsWPF.Controls.Menus.XamTagCloud", "Infragistics.WPF.TagCloud" },
{ "InfragisticsWPF.Documents.TextDocument.CSharp", "Infragistics.WPF.TextDocument.CSharp" },
{ "InfragisticsWPF.Documents.TextDocument", "Infragistics.WPF.TextDocument" },
{ "InfragisticsWPF.Documents.TextDocument.TSql", "Infragistics.WPF.TextDocument.TSql" },
{ "InfragisticsWPF.Documents.TextDocument.VisualBasic", "Infragistics.WPF.TextDocument.VisualBasic" },
{ "InfragisticsWPF.Controls.Layouts.XamTileManager", "Infragistics.WPF.TileManager" },
{ "InfragisticsWPF.Controls.Timelines.XamTimeline", "Infragistics.WPF.Timeline" },
{ "InfragisticsWPF.Controls.Charts.XamTreemap", "Infragistics.WPF.Treemap" },
{ "InfragisticsWPF.Documents.IO", "Infragistics.WPF.Word" },
{ "InfragisticsWPF.Controls.Navigation.XamZoomSlider", "Infragistics.WPF.ZoomSlider" },
        };

        public static void MapNugetAssemblies()
        {
            var nugetDirectory = "C:\\WORK\\wpf-samples\\Research\\packages";
            var files = Directory.GetFiles(nugetDirectory, "InfragisticsWPF*.dll", SearchOption.AllDirectories).ToList();
            Debug.WriteLine("Found in " + files.Count + " project files in " + ProjectDirectory + ":");

            foreach (string path in files)
            { 
                if (path.Contains(".Design")) continue;
                if (path.Contains(".resources.")) continue;

                var assmbStart = path.LastIndexOf("Infragistics");
                var assmbEnd   = path.LastIndexOf(".dll");
                var assmb = path.Substring(assmbStart, assmbEnd - assmbStart);

                var dirStart = path.IndexOf("Infragistics");
                var dirEnd = path.LastIndexOf(".Trial");
                var dir = path.Substring(dirStart, dirEnd - dirStart);

                var assmbDir = dir.Replace("Infragistics.WPF", "InfragisticsWPF");
                if (assmbDir != assmb)
                {
                    //NugetAssembliesToDir.Add(assmb, dir);

                    Debug.WriteLine("{ \"" + assmb + "\", \"" + dir + "\" },");
                    //Debug.WriteLine(path.Replace("C:\\WORK\\wpf-samples\\Research", "") + "\t\t\t" + dir + "\t" + assmb);

                }
                //actualAssemblies.Add(path);
            }


            //Debug.WriteLine(NugetAssembliesToDir);
        }

        public static List<string> GetProjects()
        {
            Initalize();

            var files = Directory.GetFiles(ProjectDirectory, "IG*.csproj", SearchOption.AllDirectories).ToList();
            Debug.WriteLine("Found in " + files.Count + " project files in " + ProjectDirectory + ":");

            var filteredFiles = new List<string>();
            foreach (string path in files)
            {
                if (path.Contains("IgFramework.Net8")) continue;
                if (path.Contains("IgTestApp.Net4")) continue;

                filteredFiles.Add(path);
                Debug.WriteLine(path);
            } 
            return filteredFiles;
        }

        public static void List()
        {
            var files = GetProjects();
            foreach (string file in files)
            {
                Debug.WriteLine(file);
            }
        }

        public static void UpdateProjects()
        {

            var files = GetProjects();
             
            foreach (string path in files)
            {
                UpdateProjectFile(path);
            }
        }

        public static void UpdateProjectFile(string path)
        { 
            Debug.WriteLine(path);

            //var lines = File.ReadAllLines(path).ToList();
            //for (int i = 0; i < lines.Count; i++)
            //{
            //    var line = lines[i];
            //    lines[i] = UpdateProjectLine(i, line);
            //}
            //File.WriteAllLines(path, lines);

        }

        public static string UpdateProjectLine(int index, string line)
        {
            var lineID = "LINE" + (index + 1);
            if (line.Contains("InfragisticsWPF"))
            {
                if (line.Contains("Reference"))
                {
                    var assemblyEndStr = ".v";
                    if (!line.Contains(assemblyEndStr)) assemblyEndStr = "\"";

                    var assemblyStart = line.IndexOf("\"");
                    var assemblyEnd = line.LastIndexOf(assemblyEndStr);

                    var assemblyOldName = line.Substring(assemblyStart, assemblyEnd - assemblyStart);
                    var refrenceLine = GetAssemlyRefrence(assemblyOldName);

                    //Debug.WriteLine(lineID + ": \n" + line + " -> \n" + refrenceLine);
                    line = refrenceLine;
                }

                if (line.Contains("HintPath")) // <HintPath>..\packages\Infragistics.WPF.Ribbon.Trial.25.1.117\lib\net40\InfragisticsWPF.Ribbon.dll</HintPath>
                {
                    var assemblyEndStr = ".v";
                    if (!line.Contains(assemblyEndStr)) assemblyEndStr = ".dll";

                    var assemblyStartStr = "InfragisticsWPF";
                    if (!line.Contains(assemblyStartStr)) assemblyStartStr = "InfragisticsWPF4";
                    
                    var assemblyStart = line.LastIndexOf(assemblyStartStr);
                    var assemblyEnd = line.LastIndexOf(assemblyEndStr);
                    var assemblyName = line.Substring(assemblyStart, assemblyEnd - assemblyStart); 

                    var hintPath = GetAssemlyHintPath(assemblyName);

                    Debug.WriteLine(lineID + ": \n" + line + " -> \n" + hintPath);
                    line = hintPath;
                }
            }
            return line;
        }

        static string GetAssemlyRefrence(string assemblyName)
        {

            assemblyName = assemblyName.Replace("InfragisticsWPF4.", Target.Prefix);
            assemblyName = assemblyName.Replace("InfragisticsWPF.", Target.Prefix);

            if (Target.Build != IgTargetBuild.Nuget)
            {
                //<Reference Include="InfragisticsWPF.Editors.v25.1">
                assemblyName += ".v" + Target.VersionNumber;
            }
            var referencePath = "    <Reference Include=" + assemblyName + "\">";
            return referencePath;
        }

        static string GetAssemlyHintPath(string assemblyName)
        {
            assemblyName = assemblyName.Replace("InfragisticsWPF4", Target.Prefix);
            assemblyName = assemblyName.Replace("InfragisticsWPF", Target.Prefix);
            //assemblyName += 

            var hintDirectory = Target.Directory + "\\";
            if (Target.Build == IgTargetBuild.Nuget)
            {
                if (NugetAssembliesToDir.ContainsKey(assemblyName))
                {
                    // <HintPath>..\packages\Infragistics.WPF.Timeline.Trial.25.1.22\lib\net40\InfragisticsWPF.Controls.Timelines.XamTimeline.dll</HintPath>
                    hintDirectory += NugetAssembliesToDir[assemblyName];
                    hintDirectory += Target.VersionPrefix + "" + Target.VersionNumber + "" + Target.VersionSuffix;
                    hintDirectory += "\\" + assemblyName + Target.Suffix;
                }
                else
                {
                    // <HintPath>..\packages\Infragistics.WPF.DataVisualization.Trial.25.1.22\lib\net40\InfragisticsWPF.DataVisualization.dll</HintPath>
                    hintDirectory += assemblyName.Replace(Target.Prefix, "Infragistics.WPF");
                    hintDirectory += Target.VersionPrefix + "" + Target.VersionNumber + "" + Target.VersionSuffix;
                    hintDirectory += "\\" + assemblyName + Target.Suffix;
                }
                    
            }
            else if (Target.Build == IgTargetBuild.Production)
            {
                hintDirectory += Target.VersionNumber + "" + assemblyName + ".v" + Target.VersionNumber + Target.Suffix;
            }
            else
            {
                hintDirectory += assemblyName + ".v" + Target.VersionNumber + Target.Suffix;
            }

            var hintPath = "      <HintPath>" + hintDirectory + "</HintPath>";
            return hintPath;
        }
    }
}
