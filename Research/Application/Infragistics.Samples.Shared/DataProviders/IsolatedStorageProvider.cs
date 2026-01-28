using System.IO.IsolatedStorage;

//TODO: add more methods for working with Isolated Storage and clean up code

namespace Infragistics.Samples.Shared.DataProviders
{
    /// <summary>
    /// Represents an utility class with useful methods when working with files in Isolated Storage 
    /// </summary>
    public class IsolatedStorageProvider : IsolatedStorageProviderBase 
    {
        /// <summary>
        /// Checks if Isolated Storage has enough space to save a file of a given size.
        /// </summary>
        /// <param name="requiredSpace">The required space of a a file in bytes</param>
        /// <returns></returns>
        public static bool IsEnoughSpaceInIsoStorage(long requiredSpace)
        {
            //using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            using (IsolatedStorageFile iso = IsolatedStorageProviderBase.GetIsolatedStorageFile())
            {
                if (iso.AvailableFreeSpace >= requiredSpace)
                {
                    System.Diagnostics.Debug.WriteLine("Isolated Storage has enough space for a file of size: " + string.Format("{0:#,###0}", requiredSpace));
                    return true;
                }
                long missingSpace = requiredSpace - iso.AvailableFreeSpace;
                System.Diagnostics.Debug.WriteLine("Isolated Storage is missing space for a file of size: " + string.Format("{0:#,###0}", missingSpace));
            }
            return false;
        }

        /// <summary>
        /// Performs an increase in space for saving files in Isolated Storage 
        /// </summary>
        /// <param name="requiredSpace">The required space of a a file in bytes</param>
        /// <returns></returns>
        public static IsolatedStorageIncreaseRespond IncreaseIsoStorage(long requiredSpace)
        {
            //using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            using (IsolatedStorageFile iso = IsolatedStorageProviderBase.GetIsolatedStorageFile())
            {
                System.Diagnostics.Debug.WriteLine("Requesting an increase in Isolated Storage: +" + string.Format("{0:#,###0}", requiredSpace));

                if (IsolatedStorageProvider.IsEnoughSpaceInIsoStorage(requiredSpace))
                {
                    System.Diagnostics.Debug.WriteLine("Requesting an increase in Isolated Storage is not needed.");
                    return IsolatedStorageIncreaseRespond.NotNeeded;
                }
                else
                {
                    // request more space for Isolated Storage  
                    if (!iso.IncreaseQuotaTo(iso.Quota + requiredSpace))
                    {
                        System.Diagnostics.Debug.WriteLine("Requesting an increase in Isolated Storage has been declined.");

                        return IsolatedStorageIncreaseRespond.Declined;
 
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Requesting an increase in Isolated Storage has been accepted.");
                        return IsolatedStorageIncreaseRespond.Accepted;
            
                    }
                }
            }
        }

       
    }
    public enum IsolatedStorageIncreaseRespond
    {
        /// <summary>
        /// Accepted respond happens when the end-user accepted an increase in IsolatedStorage
        /// </summary>
        Accepted,
        /// <summary>
        /// Declined respond happens when the end-user declined an increase in IsolatedStorage
        /// </summary>
        Declined,
        /// <summary>
        /// Not Needed respond happens when AvailableFreeSpace is greater than required space
        /// </summary>
        NotNeeded,

    }
}
   