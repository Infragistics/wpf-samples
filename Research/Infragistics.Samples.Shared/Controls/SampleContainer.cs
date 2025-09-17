using Infragistics.Themes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;

namespace Infragistics.Samples.Framework
{
    public class SampleContainer : System.Windows.Controls.Page
    {                          
        public SampleContainer()
        {
            this.UseDefaultTheme = false;
            this.IsSampleLoaded = false;
            this.Loaded += OnSampleContainerLoaded;
            this.Unloaded += OnSampleContainerUnloaded;

            // code for WPF
            this.Initialized += OnSampleContainerInitialized;
            this.SnapsToDevicePixels = true;

            // pre-loading themes 
            ThemesInitalizer.Add(new IgTheme());
            ThemesInitalizer.Add(new MetroTheme());
            ThemesInitalizer.Add(new MetroDarkTheme());
            ThemesInitalizer.Add(new Office2010BlueTheme());
            ThemesInitalizer.Add(new Office2013Theme());
            ThemesInitalizer.Add(new RoyalDarkTheme());
            ThemesInitalizer.Add(new RoyalLightTheme()); 
        }
        public List<BuiltInThemeBase> ThemesInitalizer = new List<BuiltInThemeBase>();

        public bool UseDefaultTheme { get; set; }
        public bool IsSampleLoaded { get; private set; }
      
        #region Events
     
        /// <summary>
        /// Occurs when the sample has been removed from the visual tree.
        /// This event is also fired when switching to the XAML or code tabs.
        /// </summary>
        public event EventHandler SampleUnloaded;

        /// <summary>
        /// Occurs when the sample has been initialized.  
        /// </summary>
        public event EventHandler SampleInitialized;

        /// <summary>
        /// Occurs when the sample has been constructed and loaded to the visual tree.
        /// This event also occurs when going back to the sample from the XAML or code tabs.
        /// </summary>
        public event EventHandler SampleLoaded;

        /// <summary>
        /// Occurs when the sample is no longer needed and a new one is about to be shown.
        /// This event is not occuring when switch to the XAML tab and then back to the sample.
        /// </summary>
        public event EventHandler SampleDisposed;

        #endregion

        #region Event Handlers
        private void OnNavigationService_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.New)
            {
                this.NavigationService.Navigating -= OnNavigationService_Navigating;
                this.NavigationService.Navigated -= OnNavigationService_Navigated;
                this.OnNavigatingFrom(e);
            } 
        }

        private void OnNavigationService_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void OnSampleContainerUnloaded(object sender, RoutedEventArgs e)
        {
            OnSampleUnloaded(EventArgs.Empty);
        }

        protected void OnSampleContainerLoaded(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService != null)
            {
                this.NavigationService.Navigating += OnNavigationService_Navigating;
                this.NavigationService.Navigated += OnNavigationService_Navigated;
            }
            OnSampleLoaded(EventArgs.Empty);
        }

        //private void OnSampleClosing(EventArgs e)
        //{
        //    if (SampleClosing != null)
        //        SampleClosing(this, e);
        //}
         
        protected virtual void OnSampleUnloaded(EventArgs e)
        {
            // make sure all samples unload the same theme
            if (!this.UseDefaultTheme)
                ThemeLoader.SetTheme(this, ThemeType.Default); 

            this.IsSampleLoaded = false;
            if (SampleUnloaded != null)
                SampleUnloaded(this, e);
        }

        protected virtual void OnSampleInitialized(EventArgs e)
        {
            if (SampleInitialized != null)
                SampleInitialized(this, e);
        } 

        protected virtual void OnSampleLoaded(EventArgs e)
        {             
            // make sure all samples load the same theme
            if (!this.UseDefaultTheme)
                ThemeLoader.SetTheme(this, ThemeType.RoyalLight);

            this.IsSampleLoaded = true;
            if (SampleLoaded != null)
                SampleLoaded(this, e);
        }

        public virtual void OnSampleDisposed(EventArgs e)
        {
            if (SampleDisposed != null)
                SampleDisposed(this, e);
        }
        #endregion

        private void OnSampleContainerInitialized(object sender, EventArgs e)
        {
            OnSampleInitialized(EventArgs.Empty);
        }

        protected virtual void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            // OnSampleUnloaded(EventArgs.Empty);
            // OnSampleClosed(EventArgs.Empty);
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            OnNavigationService_Navigated(this, e);
            OnSampleInitialized(EventArgs.Empty);
        }      
    }
}