using System.Threading;
using System.Windows;
using Infragistics.Samples.Framework;

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents a data provider for paths to storage assets.
    /// </summary>
    public static class StorageDataProvider
    {
        /// <summary>
        /// Returns storage path for all assets such as xml, mdb, and images files based on CurrentCulture
        /// </summary>
        public static string GetStorageLocalPath()
        {
            return SampleContainer.StorageLocalPath;
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
            return path;
        }

        #region GML Files
        /// <summary>
        /// Returns the localized full path for all GML assets
        /// </summary>
        public static string GetStorageGmlPath()
        {
            return LocalizePath(StorageDataProvider.GetStorageLocalPath(), true) + "/gml/";
        }
        /// <summary>
        /// Returns the localized full path for a given GML asset
        /// </summary>
        public static string GetStorageGmlPath(string gmlName)
        {
            return StorageDataProvider.GetStorageGmlPath() + gmlName;
        } 
        #endregion

        #region XML Files
        /// <summary>
        /// Returns the localized full path for all XML assets
        /// </summary>
        public static string GetStorageXmlPath()
        {
            return LocalizePath(StorageDataProvider.GetStorageLocalPath(), true) + "/xml/";
        }
        /// <summary>
        /// Returns the localized full path for a given xml asset
        /// </summary>
        public static string GetStorageXmlPath(string xmlName)
        {
            return StorageDataProvider.GetStorageXmlPath() + xmlName;
        }
        #endregion
        #region CSV Files
        /// <summary>
        /// Returns the localized full path for all CSV assets
        /// </summary>
        public static string GetStorageCsvPath()
        {
            return LocalizePath(StorageDataProvider.GetStorageLocalPath(), false) + "/csv/";
        }
        /// <summary>
        /// Returns the localized full path for a given CSV asset
        /// </summary>
        public static string GetStorageCsvPath(string csvName)
        {
            return StorageDataProvider.GetStorageCsvPath() + csvName;
        }
        #endregion
  
 
        /// <summary>
        /// Returns the localized full path for all images assets
        /// <para>NOTE: By default, images are not localizable </para> 
        /// </summary>
        public static string GetStorageImagePath()
        {
            return LocalizePath(StorageDataProvider.GetStorageLocalPath(), true) + "/images/";
        }

        /// <summary>
        /// Returns the localized full path for a given dictionary file
        /// </summary>
        public static string GetStorageDictionaryPath(string dictionaryName)
        {
            return StorageDataProvider.GetStorageDictionaryPath() + dictionaryName;
        }

        /// <summary>
        /// Returns the non-localized full path for all dictionary files.
        ///<para>Dictionary files are not localizable</para>
        /// </summary>
        public static string GetStorageDictionaryPath()
        {
            return StorageDataProvider.GetStorageLocalPath() + "/dictionaries/";
        }

        /// <summary>
        /// Returns localized full path for provided sample page name.
        /// </summary>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public static string GetStorageSamplePagePath(string pageName)
        {
            return LocalizePath(StorageDataProvider.GetStorageLocalPath(), true) + "/SamplePages/" + pageName;
        }

        /// <summary>
        /// Returns the localized full path for a given image asset
        /// </summary>
        public static string GetStorageImagePath(string imageName)
        {
            return StorageDataProvider.GetStorageImagePath() + imageName;
        }
        ///// <summary>
        ///// Returns localized Uri for to a given asset
        ///// </summary>
        ///// <param name="fileName"></param>
        ///// <returns></returns>
        //public static Uri GetStorageLocalUri(string fileName)
        //{
        //    string path = StorageDataPathProvider.GetStorageLocalPath();
        //    return new Uri(path + "/" + fileName);
        //}

        /// <summary>
        /// Returns the localized full path to the storage directory.
        /// </summary>
        public static string GetLocalizedStoragePath()
        {
            return LocalizePath(GetStorageLocalPath(), true);
        }

        #region Shape Files
        /// <summary>
        /// Returns the localized full path for all shape files (.SHP) 
        /// <para>NOTE: By default, shape files are not localizable </para> 
        /// </summary>
        public static string GetStorageShapeFilePath()
        {
            return LocalizePath(StorageDataProvider.GetStorageLocalPath(), false) + "/shapefiles/";
        }
        /// <summary>
        /// Returns the localized full path for a given shape file (.SHP) 
        /// </summary>
        public static string GetStorageShapeFilePath(string shapefileName)
        {
            return StorageDataProvider.GetStorageShapeFilePath() + shapefileName;
        }
        /// <summary>
        /// Returns the localized full path for all shape database files (.DBF) 
        /// <para>NOTE: By default, shape database files are not localizable </para> 
        /// </summary>
        public static string GetStorageShapeDatabasePath()
        {
            return LocalizePath(StorageDataProvider.GetStorageLocalPath(), false) + "/shapefiles/";
        }
        /// <summary>
        /// Returns the localized full path for a given shape database file (.DBF) 
        /// </summary>
        public static string GetStorageShapeDatabasePath(string shapeDatabaseName)
        {
            return StorageDataProvider.GetStorageShapeDatabasePath() + shapeDatabaseName;
        }
        #endregion

    }
}