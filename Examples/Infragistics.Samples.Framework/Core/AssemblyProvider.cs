using System.Reflection;       // provides Assembly

namespace Infragistics.Samples.Core
{
    /// <summary>
    /// Provides useful methods for working with assembly objects
    /// </summary>
    public class AssemblyProvider 
    {
        //static AssemblyProvider()
        //{
        //    Info = new AssemblyInfo(Assembly.GetCallingAssembly());
        //}
        ///// <summary>
        ///// Gets info about the calling assembly object
        ///// </summary>
        //public static AssemblyInfo Info { get; private set; }

        /// <summary>
        /// Returns info about a given assembly object
        /// </summary>
        public static AssemblyInfo GetAssemblyInfo(Assembly assembly)
        {
            return new AssemblyInfo(assembly);
        }
        /// <summary>
        /// Returns path to location of a given assembly
        /// </summary>
        public static string GetAssemblyPath(Assembly assembly)
        {
            string path = assembly.CodeBase;
            return path.Replace("file:///", ""); //.Substring(8);  //remove the file:///
        }
        /// <summary>
        /// Returns assembly pack for a given assembly.
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// <para>where [assemblyName] is a name of the assembly</para>
        /// </summary>
        public static string GetAssemblyPack(Assembly assembly)
        {
            string assemblyName = AssemblyProvider.GetAssemblyName(assembly);
            return AssemblyProvider.GetAssemblyPack(assemblyName);
        }
        /// <summary>
        /// Returns assembly pack for a given assembly name.
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// </summary>
        public static string GetAssemblyPack(string assemblyName)
        {
            return AssemblyProvider.AssemblyPack + "/" + assemblyName + ";component";
        }
        /// <summary>
        /// Returns the name of a given assembly
        /// </summary>
        public static string GetAssemblyName(Assembly assembly)
        {
            // in SL4: 
            // assemblyName = assembly.GetName().Name;

            string assemblyName = assembly.FullName;
            assemblyName  = assemblyName.Substring(0, assemblyName .IndexOf(','));

            return assemblyName;

        }

        /// <summary>
        /// Gets the application pack string: "pack://application:,,,"
        /// </summary>
        public static string AssemblyPack = "pack://application:,,,";

        /// <summary>
        /// Returns the component path for a given assembly name
        /// </summary>
        public static string GetAssemblyComponent(string assemblyName)
        {
            return "/" + assemblyName + ";component";
        }
    }

    /// <summary>
    /// Represents a class that contains info about an assembly
    /// </summary>
    public class AssemblyInfo
    {
        /// <summary>
        /// Constructor for AssemblyInfo object
        /// </summary>
        /// <param name="assembly"></param>
        public AssemblyInfo(Assembly assembly)
        {
            this.Assembly = assembly;
            //this.AssemblyNameShort = assembly.GetName().Name;
            this.AssemblyNameShort = AssemblyProvider.GetAssemblyName(assembly);
            this.AssemblyNameFull = assembly.FullName;

            this.AssemblyPath = AssemblyProvider.GetAssemblyPath(assembly);
            this.AssemblyPack = AssemblyProvider.GetAssemblyPack(assembly);

            // verify: 
            //this.AssemblyNamespace = assembly.GetName().Name;
            this.AssemblyNamespace = AssemblyProvider.GetAssemblyName(assembly);
            this.AssemblyComponent = this.AssemblyNamespace + ";component";
        }
        /// <summary>
        /// Gets the Assembly object
        /// </summary>
        public Assembly Assembly { get; private set; }
        /// <summary>
        /// Gets assembly path for this assembly
        /// </summary>
        public string AssemblyPath { get; private set; }
        /// <summary>
        /// Gets assembly pack for this assembly
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// </summary>
        public string AssemblyPack { get; private set; }
        /// <summary>
        /// Gets assembly short name for this assembly
        /// </summary>
        public string AssemblyNameShort { get; private set; }
        /// <summary>
        /// Gets assembly full name for this assembly
        /// </summary>
        public string AssemblyNameFull { get; private set; }
        /// <summary>
        /// Gets assembly default namespace for this assembly
        /// </summary>
        public string AssemblyNamespace { get; private set; }
        /// <summary>
        /// Gets assembly component path for this assembly
        /// <para>For example: "/" + [assemblyName] + ";component"</para>
        /// </summary>
        public string AssemblyComponent { get; private set; }
    }
}