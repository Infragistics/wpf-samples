﻿using System.Reflection;                // Assembly*
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;   // provides ComVisible
using System.Security;                  // provides AllowPartiallyTrustedCallers
using System.Windows;                   // provides ThemeInfo
using System.Windows.Markup;            // provides XmlnsDefinition
using IGExtensions.Framework;
using AssemblyDesigner = IGExtensions.Framework.AssemblyDesigner;

// provides AssemblyDesigner

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("IGExtensions.Common.Services")]
[assembly: AssemblyProduct(AssemblyDesigner.AssemblyProductPrefix + " Common Service Extensions" + AssemblyDesigner.AssemblyProductSuffix)]
[assembly: AssemblyDescription(AssemblyDesigner.AssemblyDescrPrefix + "Common Service Extensions" + AssemblyDesigner.AssemblyDescrSuffix)]
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
[assembly: Guid("7c021514-db92-4936-9ea8-6dad439962c1")]

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
//[assembly: XmlnsPrefix("clr-namespace:IGExtensions.Common;assembly=" + "IGExtensions.Common", "igExtensions")]
//[assembly: XmlnsDefinition("clr-namespace:IGExtensions.Common;assembly=" + "IGExtensions.Common", "IGExtensions.Common")]
#else
[assembly: XmlnsPrefix("http://schemas.infragistics.com/xaml/extensions", "igExtensions")]
[assembly: XmlnsDefinition("http://schemas.infragistics.com/xaml/extensions", "IGExtensions.Common.Data")]

#endif