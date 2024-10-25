using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Markup;
using Infragistics.Samples.Core;        // provides AssemblyDesigner

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Infragistics.Samples.Assets")]
[assembly: AssemblyProduct(AssemblyDesigner.AssemblyProductPrefix + "Samples Assets" + AssemblyDesigner.AssemblyProductSuffix)]
[assembly: AssemblyDescription(AssemblyDesigner.AssemblyDescrPrefix + "Samples Assets" + AssemblyDesigner.AssemblyDescrSuffix)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(AssemblyDesigner.AssemblyCompanyName)]
[assembly: AssemblyCopyright(AssemblyDesigner.AssemblyCopyright)]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyVersion(AssemblyDesigner.AssemblyVersion)]
[assembly: AssemblyFileVersion(AssemblyDesigner.AssemblyVersion)]

[assembly: InternalsVisibleTo("Infragistics.SamplesBrowser")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("6089e6bb-61b7-4a77-b481-c7d7ee77cb1d")]

//#if WINDOWS_PHONE
[assembly: System.Resources.NeutralResourcesLanguage("en")]
//#endif

#if WINDOWS_PHONE
//[assembly: XmlnsPrefix("clr-namespace:Infragistics.Controls.Charts;assembly=" + AssemblyRef.AssemblyName, "ig")]
//[assembly: XmlnsDefinition("clr-namespace:Infragistics.Controls.Charts;assembly=" + AssemblyRef.AssemblyName, "Infragistics.Controls.Charts")]

//[assembly: XmlnsPrefix("clr-namespace:Infragistics.Samples.Data;assembly=" + "Infragistics.Samples.Data", "ig")]
//[assembly: XmlnsDefinition("clr-namespace:Infragistics.Samples.Data;assembly=" + "Infragistics.Samples.Data", "Infragistics.Samples.Data")]

//[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml/samples", "igSamples")]
//[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/samples", "Infragistics.Samples.Assets")]

#else
//[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml/samples", "igSamples")]
//[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/samples", "Infragistics.Samples.Assets")]

#endif
