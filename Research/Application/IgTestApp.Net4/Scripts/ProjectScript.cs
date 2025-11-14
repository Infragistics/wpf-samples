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
                ProjectDirectory = "C:\\WORK\\wpf-samples\\Research\\IGDiagram";
                //ProjectDirectory = "C:\\WORK\\wpf-samples\\Research";
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

        public static List<string> GetProjects()
        {
            Initalize();

            var files = Directory.GetFiles(ProjectDirectory, "IG*.csproj", SearchOption.AllDirectories).ToList();
            Debug.WriteLine("Found in " + files .Count + " project files in " + ProjectDirectory + ":");
            //foreach (string file in files)
            //{
            //    Debug.WriteLine(file);
            //}

            return files;
        }

        public static void List()
        {

            var files = GetProjects();

            
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

            var lines = File.ReadAllLines(path).ToList();
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                lines[i] = UpdateProjectLine(i, line);
            }

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

                    var assemblyStartStr = "\\InfragisticsWPF";
                    if (!line.Contains(assemblyStartStr)) assemblyStartStr = "\\InfragisticsWPF4";
                    
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

            var hintDirectory = Target.Directory;
            if (Target.Build == IgTargetBuild.Nuget)
            {
                // <HintPath>..\packages\Infragistics.WPF.Editors.Trial.25.1.117\lib\net40\InfragisticsWPF.Editors.dll</HintPath>
                hintDirectory += assemblyName.Replace(Target.Prefix, "Infragistics.WPF");
                hintDirectory += Target.VersionPrefix + "" + Target.VersionNumber + "" + Target.VersionSuffix;
                hintDirectory += "" + assemblyName + Target.Suffix;
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
