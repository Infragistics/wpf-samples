using Infragistics.Samples.Core;

namespace Infragistics.Samples.Shared.Core
{
    public class AssemblyProviderEx : AssemblyProvider 
    {
        /// <summary>
        /// Returns assembly base path that includes assembly pack and assembly component for a given assembly name.
        /// <para>For example: pack://application:,,,/[assemblyName];component</para>
        /// </summary>
        public static string GetAssemblyBasePath(string assemblyName)
        {
            return "pack://application:,,,/" + assemblyName + ";component/";
        }
    }
}