using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Infragistics.Samples.Assets.Providers
{
    /// <summary>
    /// Represents a data provider for paths to storage assets.
    /// </summary>
    public class StorageProvider
    {
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
            path = dir.FullName + Path.DirectorySeparatorChar + "Storage" ;
            return path;
        }
        /// <summary>
        /// Returns the name of an assembly object that contains storage assets
        /// </summary>
        public static string GetStorageAssemblyName()
        {
            return StorageProvider.GetStorageAssembly().GetName().Name;
        }
        /// <summary>
        /// Returns storage absolute path for all assets such as xml, mdb, and images files based on CurrentCulture
        /// <para>Use this method to get access to assets that are copied to output directory </para> 
        /// </summary>
        public static string GetStorageAbsolutePath(bool cultureSpecific = false)
        {
            string path = GetStorageAssemblyAbsolutePath();
            return LocalizePath(path, cultureSpecific);
        }

        /// <summary>
        /// Returns storage component path for all assets such as xml, mdb, and images files based on CurrentCulture
        /// <para>Use this method to get access to assets that are built as resources in the .Assets assembly </para> 
        /// </summary>
        public static string GetStorageLocalPath(bool cultureSpecific = false, bool includeAppPack = true)
        {
            //string path = "pack://application:,,,/" + StorageProvider.GetStorageAssemblyName() + ";component/Storage";
            string path = includeAppPack ? "pack://application:,,,/" : "/"; 
            path += StorageProvider.GetStorageAssemblyName() + ";component/Storage";
            return LocalizePath(path, cultureSpecific);
        }

        /// <summary>
        /// Returns localized storage path for all assets such as xml, mdb, and images files
        /// </summary>
        public static string LocalizePath(string path, bool cultureSpecific)
        {
            if (cultureSpecific && Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                path += "/ja"; //ja-jp
            }
            else
            {
                path += "/en"; // en-us
            }
            return path ;
        }

        #region Gml Files
        /// <summary>
        /// Returns the localized full path for all gml assets
        /// </summary>
        public static string GetStorageGmlPath()
        {
            return StorageProvider.GetStorageLocalPath(true) + "/gml/";
        }
        /// <summary>
        /// Returns the localized full path for a given gml asset
        /// </summary>
        public static string GetStorageGmlPath(string gmlName)
        {
            return StorageProvider.GetStorageGmlPath() + gmlName;
        }
        #endregion

        #region Xml Files
        /// <summary>
        /// Returns the localized full path for all xml assets
        /// </summary>
        public static string GetStorageXmlPath()
        {
            return StorageProvider.GetStorageLocalPath(true) + "/xml/";
        }
        /// <summary>
        /// Returns the localized full path for a given xml asset
        /// </summary>
        public static string GetStorageXmlPath(string xmlName)
        {
            return StorageProvider.GetStorageXmlPath() + xmlName;
        }
        #endregion

        #region CSV Files
        /// <summary>
        /// Returns the localized full path for all CSV assets
        /// </summary>
        public static string GetStorageCsvPath()
        {
            return StorageProvider.GetStorageLocalPath(false) + "/csv/";
        }
        /// <summary>
        /// Returns the localized full path for a given CSV asset
        /// </summary>
        public static string GetStorageCsvPath(string csvName)
        {
            return StorageProvider.GetStorageCsvPath() + csvName;
        }
        #endregion
  
        #region Database Files
        /// <summary>
        /// Returns the localized full path for specific mdb base, Nwind.
        /// </summary>
        public static string GetStorageMdbPath(CultureInfo cultureInfo = null)
        {
            if (cultureInfo == null)
            {
                cultureInfo = Thread.CurrentThread.CurrentCulture;
            }
            return GetStorageMdbPath("Nwind.mdb", cultureInfo);
        }
        /// <summary>
        /// Returns the localized full path for a given mdb asset
        /// </summary>
        public static string GetStorageMdbPath(string dataSourceName, CultureInfo cultureInfo)
        {
            string appPath = Assembly.GetExecutingAssembly().CodeBase;
            appPath = appPath.Substring(8);
            appPath = Path.GetDirectoryName(appPath);
            DirectoryInfo dir = new DirectoryInfo(appPath);
            appPath = dir.FullName;
            string dbPath = appPath + Path.DirectorySeparatorChar + "Storage" + Path.DirectorySeparatorChar;

            if (cultureInfo.Name.ToLower() == "ja-jp")
            {
                dbPath += "ja" + Path.DirectorySeparatorChar;
            }
            else
            {
                dbPath += "en" + Path.DirectorySeparatorChar;
            }

            dbPath += "mdb" + Path.DirectorySeparatorChar + dataSourceName;
            return dbPath;
        }

        #endregion
    
        #region Dictionary Files
        /// <summary>
        /// Returns full path for a given dictionary name.
        /// <para>NOTE: By default, dictionary files are not localizable </para> 
        /// </summary>
        /// <returns></returns>
        //TODO: rename method to GetStorageDictionaryPath
        public static string GetDictionaryStoragePath(string dictionaryName)
        {
            //TODO: use GetStorageAbsolutePath(false) + "dictionaries" + Path.DirectorySeparatorChar + dictionaryName;
            string path = Assembly.GetExecutingAssembly().CodeBase;
            path = path.Substring(8);
            path = Path.GetDirectoryName(path);
            DirectoryInfo dir = new DirectoryInfo(path);
            path = dir.FullName + Path.DirectorySeparatorChar + "Storage" + Path.DirectorySeparatorChar
                + "dictionaries" + Path.DirectorySeparatorChar + dictionaryName;
            return path;
        } 
        #endregion

        #region Image Files
        /// <summary>
        /// Returns the localized full path for all images assets
        /// <para>NOTE: By default, images are not localizable </para> 
        /// </summary>
        public static string GetStorageImagePath()
        {
            //return  LocalizePath(StorageProvider.GetStorageLocalPath(),false) + "/images/";
            return StorageProvider.GetStorageLocalPath(false) + "/images/";
        }

        public static string GetStorageImagePath(string imageName)
        {
            return StorageProvider.GetStorageImagePath() + imageName;
        }

        /// <summary>
        /// Returns the localized full path for a given image asset
        /// </summary>
        public static string GetLocalizedImagePath(string imageName)
        {
            return StorageProvider.GetStorageLocalPath(true) + "/images/" + imageName;
        }
        #endregion

        #region Shape Files
        /// <summary>
        /// Returns the localized full path for all shape files (.SHP) 
        /// <para>NOTE: By default, shape files are not localizable </para> 
        /// </summary>
        public static string GetStorageShapeFilePath()
        {
            return StorageProvider.GetStorageAbsolutePath(false) + "/shapefiles/";
        }
        /// <summary>
        /// Returns the localized full path for a given shape file (.SHP) 
        /// </summary>
        public static string GetStorageShapeFilePath(string shapefileName)
        {
            return StorageProvider.GetStorageShapeFilePath() + shapefileName;
        }
        /// <summary>
        /// Returns the localized full path for all shape database files (.DBF) 
        /// <para>NOTE: By default, shape database files are not localizable </para> 
        /// </summary>
        public static string GetStorageShapeDatabasePath()
        {
            return StorageProvider.GetStorageAbsolutePath(false) + "/shapefiles/";
        }
        /// <summary>
        /// Returns the localized full path for a given shape database file (.DBF) 
        /// </summary>
        public static string GetStorageShapeDatabasePath(string shapeDatabaseName)
        {
            return StorageProvider.GetStorageShapeDatabasePath() + shapeDatabaseName;
        } 
        #endregion

        /// <summary>
        /// Returns Stream to a resource located at the specified path:
        /// <para>For example: [StorageAssemblyName].Storage.[local].xml.filename.xml</para>
        /// <para>where: [local] can be "ja-jp" or "en-us"</para>
        /// </summary>
        public static Stream GetStorageResourceStream(string resourcePath)
        {
            return StorageProvider.GetStorageAssembly().GetManifestResourceStream(resourcePath);
        }
    }
}