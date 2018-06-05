using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace IGExtensions.Common.Data
{
    public static class DataStorageProvider
    {

        //public const string ComponentsPath = "/IGExtensions.Common.Data;component/";
        internal const string ComponentName = "/IGExtensions.Common.Data;component/";

        public static string ShapefilesPath { get { return DataStorageProvider.GetStorageShapefilesPath(); } }
        public static string ImagesPath { get { return DataStorageProvider.GetStorageImagePath(); } }
        //public const string ShapeFilesPath = ComponentName + "/storage/en/shapefiles/";
        //public const string CsvFilesPath = ComponentName + "csv/";
        //public const string XmlFilesPath = ComponentName + "xml/";

        internal static bool IsCsvCultureSpecific = false;
        internal static bool IsXmlCultureSpecific = false;
        internal static bool IsShapefileCultureSpecific = false;
        internal static bool IsImagePathCultureSpecific = false;
     

        //public const string ImagesPath = ComponentPath + "images/";

        #region Assembly Path
        /// <summary>
        /// Returns the assembly object that contains storage assets
        /// </summary>
        public static Assembly GetStorageAssembly()
        {
            return Assembly.GetExecutingAssembly();
        }
        public static string GetStorageAssemblyAbsolutePath()
        {
            string path = Assembly.GetExecutingAssembly().CodeBase;
            path = path.Substring(8);
            path = Path.GetDirectoryName(path);
            var dir = new DirectoryInfo(path);
            path = dir.FullName + Path.DirectorySeparatorChar + "storage";
            return path;
        }
        /// <summary>
        /// Returns the name of an assembly object that contains storage assets
        /// </summary>
        public static string GetStorageAssemblyName()
        {
            var assembly = GetStorageAssembly();
            return assembly.GetAssemblyName();
        }
        /// <summary>
        /// Returns the name of an assembly object that contains storage assets
        /// </summary>
        public static string GetStorageAssemblyComponent()
        {
            var assembly = GetStorageAssembly();
            return assembly.GetAssemblyComponent();
        }
        /// <summary>
        /// Returns storage absolute path for all assets such as xml, mdb, and images files based on CurrentCulture
        /// <para>Use this method to get access to assets that are copied to output directory </para> 
        /// </summary>
        internal static string GetStorageAbsolutePath(bool cultureSpecific = false)
        {
            string path = GetStorageAssemblyAbsolutePath();
            return LocalizePath(path, cultureSpecific);
        }
        /// <summary>
        /// Returns localized storage path for all assets such as xml, mdb, and images files
        /// </summary>
        internal static string LocalizePath(string path, bool cultureSpecific)
        {
            if (cultureSpecific && Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                path += "/ja"; //ja-jp
            }
            else
            {
                path += "/en"; // en-us
            }
            return path;
        }
        /// <summary>
        /// Returns storage component path for all assets such as xml, mdb, and images files based on CurrentCulture
        /// <para>Use this method to get access to assets that are built as resources in the .Assets assembly </para> 
        /// </summary>
        public static string GetStorageLocalPath(bool cultureSpecific = false, bool includeAppPack = true)
        {
            //string path = "pack://application:,,,/" + StorageProvider.GetStorageAssemblyName() + ";component/Storage";
            string path = includeAppPack ? "pack://application:,,,/" : "/";
            path += GetStorageAssemblyName() + ";component/storage";
            return LocalizePath(path, cultureSpecific);
        } 
        #endregion

        public static string GetStorageImagePath()
        {
            var path = GetStorageAssemblyComponent() +"/storage";
            return LocalizePath(path, IsImagePathCultureSpecific) + "/images";
        }
        /// <summary>
        /// Returns the localized full path for all shapefile assets
        /// </summary>
        public static string GetStorageShapefilesPath()
        {
            var path = GetStorageAssemblyComponent() + "/storage";
            return LocalizePath(path, IsShapefileCultureSpecific) + "/shapefiles/";
        }
        #region CSV Path
        
        /// <summary>
        /// Returns the localized full path for all CSV assets
        /// </summary>
        public static string GetStorageCsvPath()
        {
            return GetStorageLocalPath(IsCsvCultureSpecific) + "/csv/";
        }
        /// <summary>
        /// Returns the localized full path for a given CSV asset
        /// </summary>
        public static string GetStorageCsvPath(string csvName)
        {
            return GetStorageCsvPath() + csvName;
        }
        #endregion
    }
}