using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup;
using Infragistics.Samples.Core;        // provides AssemblyDesigner

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Infragistics.Samples.Framework")]
[assembly: AssemblyProduct(AssemblyDesigner.AssemblyProductPrefix + "Samples Framework" + AssemblyDesigner.AssemblyProductSuffix)]
[assembly: AssemblyDescription(AssemblyDesigner.AssemblyDescrPrefix + "Samples Framework" + AssemblyDesigner.AssemblyDescrSuffix)]
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
[assembly: Guid("67450b00-cca4-46e2-8235-97d4b764d294")]
 

[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml/samples", "igSamples")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/samples", "Infragistics.Samples.Framework")]
//[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/samples", "Infragistics.Samples.Framework.Controls")]
 
     
//[assembly: AllowPartiallyTrustedCallers()]
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page,
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page,
    // app, or any theme specific resource dictionaries)
)] 
