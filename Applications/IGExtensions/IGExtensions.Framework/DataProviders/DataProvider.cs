using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using IGExtensions.Framework;
using System.Text.RegularExpressions;

namespace Infragistics.Framework
{
    public static class DataProvider  
    {
        static DataProvider()
        {
            Resources = new Dictionary<string, Assembly>();
            Assemblies = new List<Assembly>();
            
            AddAssembly(typeof(DataProvider).Assembly);
        }
        /// <summary>
        /// Gets or sets Dictionary of Resource Name and Assemblies
        /// </summary>
        private static Dictionary<string, Assembly> Resources { get; set; }

        /// <summary>
        /// Gets or sets Resource Assemblies
        /// </summary>
        private static List<Assembly> Assemblies { get; set; }
        
        /// <summary>
        /// Gets Resource Names
        /// </summary>
        public static List<string> AssemblyNames { get { return Assemblies.Select(i => i.FullName).ToList(); } }

        /// <summary>
        /// Gets Resource Names
        /// </summary>
        public static List<string> ResourceNames { get { return Resources.Keys.ToList(); } }
        
        /// <summary>
        /// Checks if embedded resource exists with a name that ends with specified file name 
        /// </summary> 
        public static bool ContainsFile(string fileName, out string fileKey)
        {
            fileName = fileName.ToLower();
            foreach (var key in Resources.Keys)
            {
                if (key.EndsWith(fileName))
                {
                    fileKey = key;
                    return true;
                }
            }
            fileKey = string.Empty;
            return false;
        }

        /// <summary>
        /// Checks if specified assembly and its embedded resources were registered
        /// </summary> 
        public static bool ContainsAssembly(Assembly assembly)
        {
            return Assemblies.Contains(assembly);
        }

        /// <summary>
        /// Adds specified assembly and its embedded resources to registry
        /// </summary> 
        public static void AddAssembly(Assembly assembly)
        {
            if (!Assemblies.Contains(assembly))
            {
                Assemblies.Add(assembly);

                var resurces = assembly.GetManifestResourceNames();
                foreach (var resource in resurces)
                {
                    var resourceName = resource.ToLower();
                    if (!Resources.ContainsKey(resourceName))
                         Resources.Add(resourceName, assembly);
                }
            }
        }
        
        /// <summary>
        /// Gets stream of embedded resource file in specified assembly or already added assemblies
        /// </summary> 
        public static Stream GetResource(string resourceName, Assembly assembly = null)
        {
            if (assembly != null)
            {
                if (!Assemblies.Contains(assembly))
                     AddAssembly(assembly); 
            }
            var key = string.Empty;
            if (ContainsFile(resourceName, out key))
            {
                return Resources[key].GetResourceStream(resourceName);
            }
            else //if (!ResourceFiles.ContainsFile(resourceName, out key))
            {
                var msg = "Could not find '" + resourceName + "' file in any resource assemblies";
                if (assembly != null)
                    msg += " including " + assembly.FullName + " assembly";

                Logs.Error(msg);
                throw new FileNotFoundException(msg);
            }
        }

        /// <summary>
        /// Gets streams of source code files (/xaml and/or .cs) embedded in specified assembly or already added assemblies
        /// </summary> 
        public static string GetCodeFile(string resourceName, Assembly assembly = null)
        {
            var stream = GetResource(resourceName, assembly);
            using (var reader = new StreamReader(stream))
            {
                var code = reader.ReadToEnd();
                return code;
            }
        }

        /// <summary>
        /// Gets content of text file as list of strings
        /// </summary> 
        public static List<string> GetTextFile(string resourceName, Assembly assembly = null)
        {
            var stream = GetResource(resourceName, assembly);
            return GetTextFile(stream);
        }

        public static List<string> GetTextFile(Stream stream)
        {
            var result = new List<string>();
            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line != null)
                        result.Add(line);
                }
                //do
                //{
                //    var line = sr.ReadLine();
                //    if (line != null)
                //    {
                //        result.Add(line);
                //    }
                //} while (sr.Peek() != -1);
            }
            return result;
        }

        /// <summary>
        /// Gets content of xml file as <see cref="XDocument"/>
        /// </summary> 
        public static XDocument GetXmlFile(string resourceName, Assembly assembly = null)
        {
            var stream = GetResource(resourceName, assembly);
            var result = XDocument.Load(stream);
            return result;
        }
        public static XDocument GetXmlFile(Stream stream)
        {
            return XDocument.Load(stream); 
        }
        /// <summary>
        /// Gets content of csv file as list of list of strings
        /// </summary> 
        public static List<List<string>> GetCsvFile(string fileName, 
            Assembly fileAssembly = null)
        {
            var data = GetResource(fileName, fileAssembly);
            
            return GetCsvFile(data);
        }

        public static List<List<string>> GetCsvFile(Stream data)
        {
            var result = new List<List<string>>();
            var fileLines = GetTextFile(data);
            if (fileLines.Count == 0)
            {
                Logs.Warning("Found no lines in csv data");
            }

            foreach (var line in fileLines)
            {
                //var csv = SplitCSV(line);
                var csv = line.Split(',').ToList(); 
                result.Add(csv);
            }
            return result;
        }

        public static List<string> SplitCSV(string input)
        {
            var csvSplit = new Regex("(?:^|,)(\"(?:[^\"]+|\"\")*\"|[^,]*)", RegexOptions.Compiled);
            var csv = new List<string>();
            string curr = null;
            foreach (Match match in csvSplit.Matches(input))
            {
                curr = match.Value;
                if (0 == curr.Length)
                {
                    csv.Add("");
                }
                curr = curr.Replace("\"", ""); 
                csv.Add(curr.TrimStart(','));
            }

            return csv;
        }


        /// <summary>
        /// Gets content of csv file as <see cref="CsvDataTable"/> object
        /// </summary> 
        public static CsvDataTable GetCsvTable(string fileName,
            Assembly fileAssembly = null, char fileSeperator = ',')
        {
            var csvStrings = GetCsvFile(fileName, fileAssembly);
            var csv = new CsvDataTable(csvStrings);
            return csv;
        }

    }
}