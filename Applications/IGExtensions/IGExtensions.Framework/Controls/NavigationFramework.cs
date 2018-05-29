using System;
#if SILVERLIGHT
using System.Windows;
using System.Windows.Browser;
#else // WPF
using System.Diagnostics;
#endif

namespace IGExtensions.Framework.Controls
{
    /// <summary>
    /// Represents navigation framework that gets notification of navigation changes from the <see cref="NavigationPage"/>
    /// </summary>
    public static class NavigationFramework
    {
        static NavigationFramework()
        {
            CurrentNavigationPage = null;
            CurrentNavigationAddress = GetNavigationAddress();
            CurrentNavigationContext = string.Empty;
#if SILVERLIGHT
            CurrentNavigationHostSource = Application.Current.Host.Source;
            CurrentNavigationHostAddress = GetNavigationHostAddress();
            CurrentNavigationHostRoot = GetNavigationHostRoot();
#else // WPF
            // 
#endif
          }
        /// <summary>
        /// Gets URI of the host website of the current XAML application 
        /// </summary>
        public static Uri CurrentNavigationHostSource { get; set; }
        /// <summary>
        /// Gets address of the root website of the current XAML application
        /// </summary>
        public static string CurrentNavigationHostAddress { get; set; }
        public static string CurrentNavigationHostRoot { get; set; }
        private static string GetNavigationHostRoot()
        {
            string path = CurrentNavigationHostSource.OriginalString;
            var indexLast = path.LastIndexOf("/");
            var indexRoot = path.Substring(0, indexLast).LastIndexOf("/");
            path = path.Substring(0, indexRoot);
            return path;
        }
        private static string GetNavigationHostAddress()
        {
            string path = CurrentNavigationHostSource.OriginalString;
            var indexLast = path.LastIndexOf("/");
            path = path.Substring(0, indexLast);
            return path;
        }
        ///// <summary>
        ///// Gets the CurrentHost   
        ///// </summary>
        //public static string CurrentNavigationHost
        //{
        //    get
        //    {
        //        string path = Application.Current.Host.Source.OriginalString;
        //        var indexLast = path.LastIndexOf("/");
        //        var indexRoot = path.Substring(0, indexLast).LastIndexOf("/");
        //        path = path.Substring(0, indexRoot) + "/Data/xml";

        //        return path;
        //    }
        //}

        /// <summary>
        /// Gets the navigation address of the latest page. 
        /// </summary>
        public static string GetNavigationAddress()
        {
#if SILVERLIGHT
            return HtmlPage.Document.DocumentUri.ToString();
#else // WPF
            return string.Empty;
#endif
        }
        #region Properties
        /// <summary>
        /// Gets the navigation link of the current page. 
        /// <remarks>
        /// This includes address and navigation context if the current page 
        /// overrides <see cref="NavigationPage.GetNavigationContext"/> method.</remarks>
        /// </summary>
        public static string CurrentNavigationLink
        {
            get
            {
                var link = CurrentNavigationAddress;
                if (CurrentNavigationPage != null)
                {
                    link += CurrentNavigationPage.GetNavigationContext();
                }
                else
                {
                    link += CurrentNavigationContext;
                }
                return link;
            }
        }

        public static void Navigate(Uri navigateToUri)
        {
            Navigate(navigateToUri, "_blank");
        }
        public static void Navigate(Uri navigateToUri, string target = "_blank")
        {
            //return HtmlPage.Window.Navigate(navigateToUri, target);

            var website = navigateToUri.ToString(); //.ToLower();
            if (website == string.Empty) return;

            if (!website.StartsWith("http")) website = "http://" + website;
            //if (!website.StartsWith("http://")) website = "http://" + website;

            DebugManager.Log("NavigationFramework.Navigating to: " + website);
#if SILVERLIGHT
            HtmlPage.Window.Navigate(new Uri(website), target);
#else // WPF
            Process.Start(website);
#endif
        }

    
        private static string _navCurrentNavigationContext;
        /// <summary>
        /// Gets the navigation context of the current page. 
        /// </summary>
        public static string CurrentNavigationContext
        {
            get { return _navCurrentNavigationContext; }
            set { _navCurrentNavigationContext = value; OnPageContextChanged(); }
        }

        private static string _navCurrentNavigationAddress;
        /// <summary>
        /// Gets the navigation address of the current page. 
        /// </summary>
        public static string CurrentNavigationAddress
        {
            get { return _navCurrentNavigationAddress; }
            set { _navCurrentNavigationAddress = value; OnPageAddressChanged(); }
        }
        private static NavigationPage _navCurrentPage;
        /// <summary>
        /// Gets or sets the current navigation page. 
        /// </summary>
        public static NavigationPage CurrentNavigationPage
        {
            get { return _navCurrentPage; }
            set { _navCurrentPage = value; OnPageChanged(); }
        } 
        #endregion
        #region Events
        /// <summary>
        /// Occurs when the navigation address of the current page has been changed
        /// </summary>
        public static event EventHandler PageChanged;
        /// <summary>
        /// Occurs when the navigation address of the current page has been changed
        /// </summary>
        public static event EventHandler PageAddressChanged;
        /// <summary>
        /// Occurs when the navigation context of the current page has been changed
        /// </summary>
        public static event EventHandler PageContextChanged;
        public static void OnPageAddressChanged()
        {
            if (PageAddressChanged != null)
                PageAddressChanged(null, EventArgs.Empty);
        }
        public static void OnPageContextChanged()
        {
            if (PageContextChanged != null)
                PageContextChanged(null, EventArgs.Empty);
        }
        public static void OnPageChanged()
        {
            if (PageChanged != null)
                PageChanged(null, EventArgs.Empty);
        }

        #endregion
    }
   
}