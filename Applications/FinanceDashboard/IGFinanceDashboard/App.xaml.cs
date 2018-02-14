using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using IGExtensions.Framework;
using IGExtensions.Framework.Controls;
using IGShowcase.FinanceDashboard.Resources;
using Infragistics.Themes;

namespace IGShowcase.FinanceDashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : NavigationApp // Application
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            ThemeManager.ApplicationTheme = new MetroDarkTheme();

            this.Startup += ApplicationStartup;
            this.InitializeComponent();
            this.InitializeCulture(AppStrings.AppLanguage);
         
            //SetupLocalization();
        }

       
        #endregion Constructors

        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            //MainPage mainPage = new MainPage();
            //mainPage.Language = XmlLanguage.GetLanguage(_language);
            //RootVisual = mainPage;

#if SILVERLIGHT
            var mainPage = new MainPage();
            //mainPage.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            //this.ErrorReportingElement = mainPage;
            this.RootVisual = mainPage;
#else  // WPF
            var mainWindow = new MainWindow();
            mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.MainWindow = mainWindow;
            this.MainWindow.Show();
#endif
        }
    }
}
