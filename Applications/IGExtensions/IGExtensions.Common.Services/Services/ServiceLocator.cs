using System.Linq;
using System.Windows;

namespace IGExtensions.Common.Services
{
    /// <summary>
    /// Represents XAML wrapper class for locating common services provided by IGExtensions library
    /// </summary>
    public static class ServiceLocator
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetInstance<T>() where T : class
        {
            T instance = null;
#if SILVERLIGHT
            instance = Application.Current.Resources.Values.OfType<T>().FirstOrDefault();

            //var lifetimeObjects = Application.Current.ApplicationLifetimeObjects;
            //for (int i = 0; i < lifetimeObjects.Count; i++)
            //{
            //    instance = lifetimeObjects[i] as T;
            //    if (instance != null)
            //    {
            //        break;
            //        //return instance;
            //    }
            //}
#else  // WPF
           instance = Application.Current.Resources.Values.OfType<T>().FirstOrDefault();

#endif
            return instance;
        }
    }
}