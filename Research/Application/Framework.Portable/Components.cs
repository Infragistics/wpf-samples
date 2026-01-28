namespace Infragistics.Framework
{
    public static class Components
    {
        /// <summary>
        /// Ensures that Infragistics.Framework;assembly is loaded by Xamarin Apps
        /// </summary>
        public static void Init()
        {
            //DataProvider.ResourceAssemblies.Add(typeof(Components).Assembly);
        }
    } 
}

namespace System.Reflection
{
    public static class AssemblyDesigner
    {
        public const string CompanyName = "Infragistics, Inc.";
        public const string CopyrightYear = "2017";
        public const string Copyright = "Copyright © " + CompanyName + " " + CopyrightYear;

        public const string TitlePrefix = "Infragistics.";
        public const string ProductPrefix = "Infragistics.";
        public const string DescrPrefix = "Infragistics ";
     
        /// <summary> The major and minor build numbers of the , e.g. 17.0 </summary>
        public const string MajorMinor = "17.0";
        /// <summary> The build number of the , e.g. 1 </summary>
        public const string Build = "1";
        /// <summary> The revision of the assembly </summary>
        public const string Revision = "1000";
        /// <summary> The full version of the . </summary>
        public const string Version = MajorMinor + "." + Build + "." + Revision;


#if DEBUG
        public const string Configuration = "Debug";
#elif TRIAL
        public const string Configuration = "Trial";
#elif BETA
        public const string Configuration = "CTP";
#else 
        public const string Configuration = "Release";
#endif

    }

}


 


