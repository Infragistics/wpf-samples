using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using Infragistics.Samples.Core;        // provides AssemblyDesigner

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Infragistics.Samples.Shared")]
[assembly: AssemblyProduct(AssemblyDesigner.AssemblyProductPrefix + "Samples Shared" + AssemblyDesigner.AssemblyProductSuffix)]
[assembly: AssemblyDescription(AssemblyDesigner.AssemblyDescrPrefix + "Samples Shared" + AssemblyDesigner.AssemblyDescrSuffix)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(AssemblyDesigner.AssemblyCompanyName)]
[assembly: AssemblyCopyright(AssemblyDesigner.AssemblyCopyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion(AssemblyDesigner.AssemblyVersion)]
[assembly: AssemblyFileVersion(AssemblyDesigner.AssemblyVersion)]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("86e031b8-36d6-4e9c-8382-d574efb7c52d")]

//#if WINDOWS_PHONE
[assembly: System.Resources.NeutralResourcesLanguage("en")]
 
//[assembly: AllowPartiallyTrustedCallers()]
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page,
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page,
    // app, or any theme specific resource dictionaries)
)]


[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml/samples", "igSamples")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/samples", "Infragistics.Samples.Framework")]
//[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/samples", "Infragistics.Samples.Shared")]

