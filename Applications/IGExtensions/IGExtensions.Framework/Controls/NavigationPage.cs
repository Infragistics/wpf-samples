using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace IGExtensions.Framework.Controls
{
    /// <summary>
    /// Represents common (SL and WPF) wrapper for <see cref="System.Windows.Controls.Page"/> 
    /// with additional navigation features such as notification of 
    /// the <see cref="NavigationFramework"/> about navigation changes.
    /// </summary>
    public class NavigationPage : Page
    {
        /// <summary>
        /// Constructs an instance of the <see cref="NavigationPage"/> object
        /// </summary>
        public NavigationPage()
        {
            this.IsPageLoaded = false;
            this.IsPageNavigated = false;
            this.Loaded += OnPageLoaded;
            this.Unloaded += OnPageUnloaded;

            if (this.NavigationService != null)
            {
                this.NavigationService.Navigating += OnNavigationService_Navigating;
                this.NavigationService.Navigated += OnNavigationService_Navigated;
            }
        }

        /// <summary>
        /// Gets whether the current page has been loaded
        /// </summary>
        public bool IsPageLoaded { get; private set; }
        /// <summary>
        /// Gets whether the current page has been navigated
        /// </summary>
        public bool IsPageNavigated { get; private set; }
        #region Events
        /// <summary>
        /// Occurs when the current page has been removed from the visual tree.
        /// </summary>
        public event EventHandler PageUnloaded;
        /// <summary>
        /// Occurs when the current page has been constructed and loaded to the visual tree.
        /// </summary>
        public event EventHandler PageLoaded;
        /// <summary>
        /// Occurs when the current page has been navigated to.
        /// </summary>
        public event EventHandler PageNavigated;
        /// <summary>
        /// Occurs when the current page has been started to navigating to.
        /// </summary>
        public event EventHandler PageNavigating;

        #endregion
        #region Event Handlers
        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            //if (this.NavigationService != null)
            //{
            //    this.NavigationService.Navigating += OnNavigationService_Navigating;
            //    this.NavigationService.Navigated += OnNavigationService_Navigated;
            //}
            OnPageLoaded(e);
        }
        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            OnPageUnloaded(e);
        }

        private void OnNavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
             OnPageNavigating(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                this.NavigationService.Navigating -= OnNavigationService_Navigating;

                //this.OnNavigatingFrom(e);
            }
        }
        private void OnNavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            DebugManager.LogTrace("" + e.Uri.ToString());
            OnPageNavigated(e);

        }

        protected virtual void OnPageLoaded(RoutedEventArgs e)
        {
            this.IsPageLoaded = true;
            if (PageLoaded != null)
                PageLoaded(this, e);
        }
        protected virtual void OnPageUnloaded(EventArgs e)
        {
            this.IsPageLoaded = false;
            if (PageUnloaded != null)
                PageUnloaded(this, e);
        }
        protected virtual void OnPageNavigating(NavigatingCancelEventArgs e)
        {
            this.IsPageNavigated = false;
            if (PageNavigating != null)
                PageNavigating(this, e);
        }
        /// <summary>
        /// Gets the navigation context of the current page. 
        /// <remarks>
        /// Derived objects should overrides this method and provides actual navigation context 
        /// or this method will return empty string and <see cref="NavigationFramework"/> will not include
        /// navigation context in <see cref="NavigationFramework.CurrentNavigationLink"/> property.</remarks>
        /// <example>www.localhost:1905/#Map + [?]NavigationContext, ?regions=[AU,NA,EU] </example>
        /// </summary>
        public virtual string GetNavigationContext()
        {
            return string.Empty; // + "?" + "CurrentNavigationContext";
        }
        /// <summary>
        /// Handles the <see cref="PageNavigated"/> event and notifies the <see cref="NavigationFramework"/> about navigation changes.
        /// <remarks>
        /// Overrides of this methods should still call this method in order to synchronize the <see cref="NavigationFramework"/>.</remarks>
        /// </summary>
        protected virtual void OnPageNavigated(NavigationEventArgs e)
        {
            DebugManager.LogTrace("OnPageNavigated-> " + e.Uri);
            // update NavigationFramework with navigation changes of the current page
            NavigationFramework.CurrentNavigationPage = this;
            NavigationFramework.CurrentNavigationAddress = NavigationFramework.GetNavigationAddress();
            NavigationFramework.CurrentNavigationContext = GetNavigationContext();
            this.IsPageNavigated = true;
            if (PageNavigated != null)
                PageNavigated(this, e);
        }

        //protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        //{
        //    OnPageUnloaded(EventArgs.Empty);
        //    base.OnNavigatingFrom(e);
        //    //OnPageClosed(EventArgs.Empty);
        //}
        protected virtual void OnPageNavigatedTo(NavigationEventArgs e)
        {
            //OnNavigationService_Navigated(this, e);
            OnPageNavigated(e);
     
            //base.OnNavigatedTo(e);
            //OnPageInitialized(EventArgs.Empty);
        }

#if SILVERLIGHT
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            OnPageNavigated(e);
        }
#endif

        #endregion
    }
}