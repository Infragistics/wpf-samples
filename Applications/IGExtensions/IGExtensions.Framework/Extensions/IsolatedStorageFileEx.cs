using IGExtensions.Framework;

namespace System.IO.IsolatedStorage
{
    public static class IsolatedStorageFileEx
    {
        /// <summary>
        /// Checks if Isolated Storage has enough space to save a file of a given size.
        /// </summary>
        /// <param name="iso"></param>
        /// <param name="requiredSpace">The required space of a a file in bytes</param>
        /// <returns></returns>
        public static bool IsEnoughSpaceInIsoStorage(this IsolatedStorageFile iso, long requiredSpace)
        {
    
            if (iso.AvailableFreeSpace >= requiredSpace)
            {
                DebugManager.LogData("Isolated Storage has enough space for a file of size: " + string.Format("{0:#,###0}", requiredSpace));
                return true;
            }
            long missingSpace = requiredSpace - iso.AvailableFreeSpace;
            DebugManager.LogData("Isolated Storage is missing space for a file of size: " + string.Format("{0:#,###0}", missingSpace));
             
            return false;
        }
        /// <summary>
        /// Performs an increase in space for saving files in Isolated Storage 
        /// </summary>
        /// <param name="iso"></param>
        /// <param name="requiredSpace">The required space of a a file in bytes</param>
        /// <returns></returns>
        public static IsolatedStorageIncreaseRespond IncreaseIsoStorage(this IsolatedStorageFile iso, long requiredSpace)
        {
            IsolatedStorageIncreaseRespond respond;
            DebugManager.LogData("Requesting an increase in Isolated Storage: +" + string.Format("{0:#,###0}", requiredSpace));
                
            if (iso.IsEnoughSpaceInIsoStorage(requiredSpace))
            {
                DebugManager.LogTrace("Request for increase in Isolated Storage is not needed.");
                respond = IsolatedStorageIncreaseRespond.NotNeeded;
            }
            else
            {
                // request more space for Isolated Storage  
                if (!iso.IncreaseQuotaTo(iso.Quota + requiredSpace))
                {
                    DebugManager.LogTrace("Request for increase in Isolated Storage has been declined by the user.");
                    respond = IsolatedStorageIncreaseRespond.Declined;
                }
                else
                {
                    DebugManager.LogTrace("Request for increase in Isolated Storage has been accepted by the user.");
                    respond = IsolatedStorageIncreaseRespond.Accepted;
                }
            }
            return respond;
        }
        
    }
    /// <summary>
    /// Represents an Toolity class with useful methods when working with files in Isolated Storage 
    /// </summary>
    public class IsolatedStorageProvider 
    {
        /// <summary>
        /// Checks if Isolated Storage has enough space to save a file of a given size.
        /// </summary>
        /// <param name="requiredSpace">The required space of a a file in bytes</param>
        /// <returns></returns>
        public static bool IsEnoughSpaceInIsoStorage(long requiredSpace)
        {
            using (IsolatedStorageFile iso = GetIsolatedStorageFile())
            {
                return iso.IsEnoughSpaceInIsoStorage(requiredSpace);
            }
        }
        /// <summary>
        /// Performs an increase in space for saving files in Isolated Storage 
        /// </summary>
        /// <param name="requiredSpace">The required space of a a file in bytes</param>
        /// <returns></returns>
        public static IsolatedStorageIncreaseRespond IncreaseIsoStorage(long requiredSpace)
        {
            //using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
            using (IsolatedStorageFile iso = GetIsolatedStorageFile())
            {
                return iso.IncreaseIsoStorage(requiredSpace);
            }
        }
        public static IsolatedStorageFile GetIsolatedStorageFile()
        {
#if SILVERLIGHT
            return IsolatedStorageFile.GetUserStoreForApplication();
#else
            return IsolatedStorageFile.GetUserStoreForAssembly();
#endif
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