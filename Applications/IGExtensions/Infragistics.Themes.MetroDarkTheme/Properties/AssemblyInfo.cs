using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using Infragistics;

#pragma warning disable 436

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(AssemblyRef.AssemblyName + AssemblyVersion.ProductTitleSuffix)]
[assembly: AssemblyDescription(AssemblyRef.AssemblyDescriptionBase + " - " + AssemblyVersion.Configuration + " Version")]
[assembly: AssemblyConfiguration(AssemblyVersion.Configuration)]
[assembly: AssemblyCompany(AssemblyVersion.CompanyName)]
[assembly: AssemblyProduct(AssemblyRef.AssemblyProduct + AssemblyVersion.ProductTitleSuffix)]
[assembly: AssemblyCopyright("Copyright © Infragistics, Inc. 2008 - " + AssemblyVersion.EndCopyrightYear)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly:Guid("59275BFD-B979-4078-AA82-0C723F529C4C")]


[assembly: AssemblyVersion(AssemblyVersion.Version)]
[assembly: AssemblyFileVersion(AssemblyVersion.FileVersion)]
[assembly: SatelliteContractVersion(AssemblyVersion.SatelliteContractVersion)]

[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml", "ig")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml", "Infragistics.Themes")]
//In order to begin building localizable applications, set 
//<UICulture>CultureYouAreCodingWith</UICulture> in your .csproj file
//inside a <PropertyGroup>.  For example, if you are using US english
//in your source files, set the <UICulture> to en-US.  Then uncomment
//the NeutralResourceLanguage attribute below.  Update the "en-US" in
//the line below to match the UICulture setting in the project file.

//[assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)]

#if !DEBUG
[assembly: System.Security.AllowPartiallyTrustedCallers()]
#endif


[assembly:ThemeInfo(
    ResourceDictionaryLocation.SourceAssembly, //where theme specific resource dictionaries are located
                             //(used if a resource is not found in the page, 
                             // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
                                      //(used if a resource is not found in the page, 
                                      // app, or any theme specific resource dictionaries)
)]

class AssemblyRef
{
    internal const string AssemblyName = AssemblyVersion.AssemblyNamePrefix + ".Themes.MetroDark" + AssemblyVersion.AssemblyNameSuffix;
    internal const string AssemblyProduct = AssemblyVersion.Product;
    internal const string AssemblyDescriptionBase = "MetroDarkTheme themes assembly for " + AssemblyVersion.Platform;

}
#pragma warning restore 436