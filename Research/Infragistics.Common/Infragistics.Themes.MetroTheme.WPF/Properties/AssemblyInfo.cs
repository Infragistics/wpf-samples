using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;

[assembly: AssemblyTitle("Infragistics.Themes.MetroTheme")]
[assembly: AssemblyDescription("Infragistics Metro Theme package for WPF controls ")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Infragistics")]
[assembly: AssemblyProduct("Infragistics.Themes.MetroTheme")]
[assembly: AssemblyCopyright("Copyright © Infragistics 2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]
 
//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]

[assembly: AssemblyVersion("13.2.1000.1")]
[assembly: AssemblyFileVersion("13.2.1000.1")]

#if !SILVERLIGHT
[assembly: ThemeInfo(
ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page, 
    // or application resource dictionaries)
ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page, 
    // app, or any theme specific resource dictionaries)
)]
#endif