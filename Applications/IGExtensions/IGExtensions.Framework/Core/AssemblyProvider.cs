using System;
using System.Reflection;
using IGExtensions.Framework;
using System.IO;
using System.Collections.Generic;
using System.Linq;

// provides Assembly

namespace IGExtensions.Framework 
{
    /// <summary>
    /// Provides useful methods for working with assembly objects
    /// </summary>
    public static class AssemblyProvider 
    { 
        public static List<Type> GetTypesList(this Assembly assembly)
        {
            var types = assembly.GetTypes().ToList();
            return types;
        }

        /// <summary>
        /// Gets stream of embedded resource file in specified assembly or 
        /// </summary> 
        public static Stream GetResourceStream(this Assembly assembly, string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
                throw new ArgumentException("resourceName");

            resourceName = resourceName.ToLower();
            var resurces = assembly.GetManifestResourceNames();
            foreach (var name in resurces)
            {
                if (name.ToLower().EndsWith(resourceName))
                    return assembly.GetManifestResourceStream(name);
            }
            return null;
        }

        /// <summary>
        /// Returns info about a given assembly object
        /// </summary>
        public static AssemblyInfo GetAssemblyInfo(this Assembly assembly)
        {
            return new AssemblyInfo(assembly);
        }
        /// <summary>
        /// Returns path to location of a given assembly
        /// </summary>
        public static string GetAssemblyPath(Assembly assembly)
        {
            return assembly.GetAssemblyPath();
        }
        /// <summary>
        /// Returns assembly pack for a given assembly.
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// <para>where [assemblyName] is a name of the assembly</para>
        /// </summary>
        public static string GetAssemblyPack(Assembly assembly)
        {
            return assembly.GetAssemblyPack();
        }
        /// <summary>
        /// Returns assembly pack for a given assembly name.
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// </summary>
        public static string GetAssemblyPack(string assemblyName)
        {
            return AssemblyEx.GetAssemblyPack(assemblyName);
        }
        /// <summary>
        /// Returns the name of a given assembly
        /// </summary>
        public static string GetAssemblyName(Assembly assembly)
        {
            return assembly.GetAssemblyName();
        }
        /// <summary>
        /// Returns the component path for a given assembly
        /// </summary>
        public static string GetAssemblyComponent(Assembly assembly)
        {
            return assembly.GetAssemblyComponent();
        }
        /// <summary>
        /// Returns the component path for a given assembly name
        /// </summary>
        public static string GetAssemblyComponent(string assemblyName)
        {
            return AssemblyEx.GetAssemblyComponent(assemblyName);
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

            AssemblyTitle = assembly.GetAssemblyAttribute(AssemblyAttributes.Title);
            AssemblyProduct = assembly.GetAssemblyAttribute(AssemblyAttributes.Product);
            AssemblyDescription = assembly.GetAssemblyAttribute(AssemblyAttributes.Description);
            AssemblyConfiguration = assembly.GetAssemblyAttribute(AssemblyAttributes.Configuration);
            AssemblyCompany = assembly.GetAssemblyAttribute(AssemblyAttributes.Company);
            AssemblyCopyright = assembly.GetAssemblyAttribute(AssemblyAttributes.Copyright);
            AssemblyTrademark = assembly.GetAssemblyAttribute(AssemblyAttributes.Trademark);
            AssemblyCulture = assembly.GetAssemblyAttribute(AssemblyAttributes.Culture);
            AssemblyVersion = assembly.GetAssemblyAttribute(AssemblyAttributes.Version);
        }
        #region Properties
        /// <summary>
        /// Gets the Assembly object
        /// </summary>
        public Assembly Assembly { get; private set; }
        /// <summary>
        /// Gets assembly Title for this assembly
        /// </summary>
        public string AssemblyTitle { get; private set; }
        /// <summary>
        /// Gets assembly Product for this assembly
        /// </summary>
        public string AssemblyProduct { get; private set; }
        /// <summary>
        /// Gets assembly Description for this assembly
        /// </summary>
        public string AssemblyDescription { get; private set; }
        /// <summary>
        /// Gets assembly Configuration for this assembly
        /// </summary>
        public string AssemblyConfiguration { get; private set; }
        /// <summary>
        /// Gets assembly Company for this assembly
        /// </summary>
        public string AssemblyCompany { get; private set; }
        /// <summary>
        /// Gets assembly Copyright for this assembly
        /// </summary>
        public string AssemblyCopyright { get; private set; }
        /// <summary>
        /// Gets assembly Trademark for this assembly
        /// </summary>
        public string AssemblyTrademark { get; private set; }
        /// <summary>
        /// Gets assembly Culture for this assembly
        /// </summary>
        public string AssemblyCulture { get; private set; }
        /// <summary>
        /// Gets assembly Version for this assembly
        /// </summary>
        public string AssemblyVersion { get; private set; }
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
        #endregion
    }
}

namespace System.Reflection
{
    public static class AssemblyEx
    {
        /// <summary>
        /// Gets the application pack string: "pack://application:,,,"
        /// </summary>
        public const string AssemblyPack = "pack://application:,,,";
        /// <summary>
        /// Returns the pack for a given assembly.
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// <para>where [assemblyName] is a name of the assembly</para>
        /// </summary>
        public static string GetAssemblyPack(this Assembly assembly)
        {
            string assemblyName = assembly.GetAssemblyName();
            return GetAssemblyPack(assemblyName);
        }
        /// <summary>
        /// Returns assembly pack for a given assembly name.
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// </summary>
        public static string GetAssemblyPack(string assemblyName)
        {
            return AssemblyPack + "/" + assemblyName + ";component";
        }
        /// <summary>
        /// Returns the name of a given assembly
        /// </summary>
        public static string GetAssemblyName(this Assembly assembly)
        {
            // in SL4: 
            // assemblyName = assembly.GetName().Name;
            var assemblyName = assembly.FullName;
            assemblyName = assemblyName.Substring(0, assemblyName.IndexOf(','));
            return assemblyName; 
        }
        /// <summary>
        /// Returns the component of a given assembly
        /// <para>For example: /[assemblyName];component</para>
        /// <para>where [assemblyName] is a name of the assembly</para>
        /// </summary>
        public static string GetAssemblyComponent(this Assembly assembly)
        {
            var assemblyName = assembly.GetAssemblyName();
            return GetAssemblyComponent(assemblyName);
        }
        /// <summary>
        /// Returns the component path for a given assembly name
        /// </summary>
        public static string GetAssemblyComponent(string assemblyName)
        {
            return "/" + assemblyName + ";component";
        }
        /// <summary>
        /// Returns path to location of a given assembly
        /// </summary>
        public static string GetAssemblyPath(this Assembly assembly)
        {
            var path = assembly.CodeBase;
            return path.Replace("file:///", ""); 
        }
        
        /// <summary>
        /// Returns a custom attribute for the currant assembly.
        /// </summary>
        public static Attribute GetAssemblyAttribute(this Assembly assembly, Type attributeType)
        {
            var attr = Attribute.GetCustomAttribute(assembly, attributeType, false);
            return attr;
        }
        /// <summary>
        /// Returns a value of custom attribute for the currant assembly.
        /// </summary>
        public static string GetAssemblyAttribute(this Assembly assembly, AssemblyAttributes attributeType)
        {
            var value = string.Empty;
            try
            {
                if (attributeType == AssemblyAttributes.Title)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyTitleAttribute));
                    value = ((AssemblyTitleAttribute)attr).Title;
                }
                else if (attributeType == AssemblyAttributes.Product)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyProductAttribute));
                    value = ((AssemblyProductAttribute)attr).Product;
                }
                else if (attributeType == AssemblyAttributes.Description)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyDescriptionAttribute));
                    value = ((AssemblyDescriptionAttribute)attr).Description;
                }
                else if (attributeType == AssemblyAttributes.Configuration)
                { 
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyConfigurationAttribute));
                    value = ((AssemblyConfigurationAttribute)attr).Configuration;
                }
                else if (attributeType == AssemblyAttributes.Company)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyCompanyAttribute));
                    value = ((AssemblyCompanyAttribute)attr).Company;
                }
                else if (attributeType == AssemblyAttributes.Copyright)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyCopyrightAttribute));
                    value = ((AssemblyCopyrightAttribute)attr).Copyright;
                }
                else if (attributeType == AssemblyAttributes.Trademark)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyTrademarkAttribute));
                    value = ((AssemblyTrademarkAttribute)attr).Trademark;
                }
                else if (attributeType == AssemblyAttributes.Culture)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyCultureAttribute));
                    value = ((AssemblyCultureAttribute)attr).Culture;
                }
                else if (attributeType == AssemblyAttributes.Version)
                {
                    var attr = assembly.GetAssemblyAttribute(typeof(AssemblyFileVersionAttribute));
                    value = ((AssemblyFileVersionAttribute)attr).Version;
                }
            }
            
            catch (Exception)
            {
            }
            return value;
        }

    }
    public enum AssemblyAttributes
    {
        Title,
        Product,
        Description,
        Configuration,
        Company,
        Copyright,
        Trademark,
        Culture,
        Version,  
    }
      
}