using System;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class DictionaryFileProvider
    {
        /// <summary>
        /// Return Uri to dictionary file
        /// </summary>
        /// <returns></returns>
        public static Uri GetDictionaryLocalUri(string dictionaryName)
        {
            return new Uri(GetDictionaryLocalPath(dictionaryName));
        }

        public static string GetDictionaryLocalPath(string dictionaryName)
        {
            dictionaryName = dictionaryName.TrimStart(@" \/".ToCharArray()).Replace(@"\", "/");
            return StorageDataProvider.GetStorageDictionaryPath(dictionaryName);
        }

        public static string GetDictionaryLocalPath()
        {
            return StorageDataProvider.GetStorageDictionaryPath();
        }
    }
}
