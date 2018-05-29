using System.Threading;

namespace IGExtensions.Common.DataProviders
{
    public class StorageDataProvider
    {
        public static string StorageLocalPath { get; set; }

        //private string _storageLocalPath;

        /// <summary>
        /// Returns localized storage path for all assets such as xml, mdb, and images files
        /// </summary>
        public static string LocalizePath(string path, bool cultureSpecific)
        {
            if (cultureSpecific && 
                (Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp" ||
                 Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja"))
            {
                path += "/ja"; //ja-jp
            }
            //else
            //{
            //    path += "/en"; // en-us
            //}
            return path;
        }
        /// <summary>
        /// Returns the localized full path for a given image asset
        /// </summary>
        public static string GetStorageImagePath(string imageName)
        {
            return StorageDataProvider.GetStorageImagePath() + imageName;
        }
        /// <summary>
        /// Returns the localized full path for all images assets
        /// <para>NOTE: By default, images are not localizable </para> 
        /// </summary>
        public static string GetStorageImagePath()
        {
            string path = StorageDataProvider.StorageLocalPath + "/images"; ;

            return LocalizePath(path, true) + "/";
        }
    }
}