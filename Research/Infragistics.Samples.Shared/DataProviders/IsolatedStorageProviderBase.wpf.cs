using System.IO.IsolatedStorage;

namespace Infragistics.Samples.Shared.DataProviders
{
    public class IsolatedStorageProviderBase 
    {
        public static IsolatedStorageFile GetIsolatedStorageFile()
        {
            return IsolatedStorageFile.GetUserStoreForAssembly();
        }
    }
}