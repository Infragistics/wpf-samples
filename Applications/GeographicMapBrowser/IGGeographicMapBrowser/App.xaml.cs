using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Infragistics.Themes; // provides ThemeManager
using System.Windows.Markup;
using System.Windows.Threading;

namespace IGShowcase.GeographicMapBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            ThemeManager.ApplicationTheme = new MetroTheme();
            // merge app resources that use colors and styles defined in the Metro Theme
            this.Resources.MergeDictionary("/InfragisticsWPF.Themes.Metro;component/themes/theme.colors.xaml");
            this.Resources.MergeDictionary("/IGExtensions.Framework;component/Styles/FrameworkStyles.xaml");
            this.Resources.MergeDictionary("/IGExtensions.Common;component/Assets/Styles/CommonStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.GeographicMapBrowser;component/Styles/AppBrushes.xaml");
            this.Resources.MergeDictionary("/IGShowcase.GeographicMapBrowser;component/Styles/AppStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.GeographicMapBrowser;component/Styles/AppTheme.xaml");  
             
            this.Startup += this.OnAppStartup; 
            this.DispatcherUnhandledException += this.OnAppUnhandledException; 
        }
        
        private void OnAppStartup(object sender, StartupEventArgs e)
        {   
            this.MainWindow = new MainWindow(); 
            this.MainWindow.Show(); 
        }
  
        private void OnAppUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        { 
            System.Diagnostics.Debug.WriteLine(e.Exception.ToString());  
        }
    }
}
