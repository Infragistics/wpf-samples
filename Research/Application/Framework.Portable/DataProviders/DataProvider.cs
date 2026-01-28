using Infragistics.Framework.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Infragistics.Framework
{
    public static class DataProvider  
    {
        static DataProvider()
        {
            Resources = new Dictionary<string, Assembly>();
            Assemblies = new List<Assembly>();
            
            AddAssembly(typeof(DataProvider).GetTypeInfo().Assembly);
        }
        /// <summary>
        /// Gets or sets Dictionary of Resource Name and Assemblies
        /// </summary>
        public static Dictionary<string, Assembly> Resources { get; private set; }

        /// <summary>
        /// Gets or sets Resource Assemblies
        /// </summary>
        public static List<Assembly> Assemblies { get; private set; }
        
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
            else 
            {
                var msg = "Could not find '" + resourceName + "' file in any resource assemblies";
                if (assembly != null)
                    msg += " including " + assembly.FullName + " assembly";

                var allResources = "";
                foreach (var resource in Resources.Keys)
                {
                    allResources += "- " + resource + "\n";
                }
                Logs.Message("App has these embedded resources: \n" + allResources);

                Logs.Error(msg);
                
                throw new FileNotFoundException(msg);
            }
        }
        public static List<string> GetResourceKeys()
        {
            return Resources.Keys.ToList();
        }
        public static string GetResourceString()
        {
            var ret = "";
            foreach (var resource in Resources.Keys)
            {
                ret += "- " + resource + "\n";
            }
            return ret;
        }
        /// <summary>
        /// Gets string of source code files for specified type and code file
        /// </summary>
        public static string GetCodeFile(Type sampleType, CodeFile codeType)
        {
            var fileName = sampleType.FullName + codeType.GetFileExt();
            if (CodeFiles.ContainsKey(fileName))
            {
                return CodeFiles[fileName];
            }
            else
            {
                Logs.Trace("DataProvider loading code file: " + fileName); 
                return GetCodeFileAsync(fileName).Result;
            }
        }

        /// <summary>
        /// Cache all code files for re-use and speed up consecutive calls that access these files 
        /// </summary>
        private static Dictionary<string, string> CodeFiles = new Dictionary<string, string>();

        /// <summary>
        /// Gets string of source code files embedded in specified assembly or already added assemblies
        /// </summary>
        /// <param name="fileName">File name with .xaml, .xaml.cs, .cs extensions </param> 
        /// <returns></returns>
        public static async Task<string> GetCodeFileAsync(string fileName, Assembly assembly = null)
        {
            var stream = GetResource(fileName, assembly);
            using (var reader = new StreamReader(stream))
            {
                var code = await reader.ReadToEndAsync();
                if (!CodeFiles.ContainsKey(fileName))
                     CodeFiles.Add(fileName, code);
                return code;
            }
        }
       
        public static async Task<Dictionary<string, string>> GetCodeFilesAsync(List<string> fileNames)
        { 
            foreach (var name in fileNames)
            { 
                var code = await GetCodeFileAsync(name);
            }
            
            return CodeFiles;
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
                var csv = line.Split(',').ToList();
                result.Add(csv);
            }
            return result;
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