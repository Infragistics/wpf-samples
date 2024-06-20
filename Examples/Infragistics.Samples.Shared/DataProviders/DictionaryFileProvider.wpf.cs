using System;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class DictionaryFileProvider
    {
        public static Uri GetDictionaryLocalUri(string fileName)
        {
            return new Uri(GetDictionaryLocalPath(fileName));
        }

        public static string GetDictionaryLocalPath(string fileName)
        {
            return StorageDataProvider.GetDictionaryStoragePath(fileName);
        }
    }
}