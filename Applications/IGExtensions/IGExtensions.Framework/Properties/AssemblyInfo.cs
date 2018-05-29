using System.Reflection;                // Assembly*
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;   // provides ComVisible
using System.Security;                  // provides AllowPartiallyTrustedCallers
using System.Windows;                   // provides ThemeInfo
using System.Windows.Markup;            // provides XmlnsDefinition
using IGExtensions.Framework;              // provides AssemblyDesigner

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("IGExtensions.Framework")]
[assembly: AssemblyProduct(AssemblyDesigner.AssemblyProductPrefix + "Extensions Framework" + AssemblyDesigner.AssemblyProductSuffix)]
[assembly: AssemblyDescription(AssemblyDesigner.AssemblyDescrPrefix + "Extensions Framework" + AssemblyDesigner.AssemblyDescrSuffix)]
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
[assembly: Guid("f64be29c-b94a-49c9-b1fc-ac661b3b5c48")]

[assembly: System.Resources.NeutralResourcesLanguage("en")]

#if !SILVERLIGHT
//[assembly: AllowPartiallyTrustedCallers()]
[assembly: ThemeInfo(
    ResourceDictionaryLocation.None, //where theme specific resource dictionaries are located
    //(used if a resource is not found in the page,
    // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly //where the generic resource dictionary is located
    //(used if a resource is not found in the page,
    // app, or any theme specific resource dictionaries)
)]
#endif

// XmlnsDefinition for v13.1 release
#if WINDOWS_PHONE
//[assembly: XmlnsPrefix("clr-namespace:IGExtensions.Framework;assembly=" + "IGExtensions.Framework", "igExtensions")]
//[assembly: XmlnsDefinition("clr-namespace:IGExtensions.Framework;assembly=" + "IGExtensions.Framework", "IGExtensions.Framework")]
#else
[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml/extensions", "igExtensions")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/extensions", "IGExtensions.Framework")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/extensions", "IGExtensions.Framework.Controls")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/extensions", "IGExtensions.Framework.Converters")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/extensions", "IGExtensions.Framework.Effects")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/extensions", "IGExtensions.Framework.Models")]
 
 
//[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml/IGExtensions/visuals", "vsm")]
//[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/IGExtensions/visuals", "System.Windows")]
#endif
