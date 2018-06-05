using System;
using System.Windows;
using System.Windows.Markup;
using IGExtensions.Framework.Controls; // provides NavigationApp
using IGShowcase.FinanceDashboard.Resources;
using Infragistics.Themes; // provides ThemeManager

namespace IGShowcase.FinanceDashboard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : NavigationApp
    {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            //ThemeManager.ApplicationTheme = new MetroDarkTheme();

            this.Startup += ApplicationStartup;
            this.InitializeComponent();
            this.InitializeCulture(AppStrings.AppLanguage);         
        }
       
        #endregion Constructors

        /// <summary>
        /// Handles the Startup event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.MainWindow = mainWindow;
            this.MainWindow.Show();
        }
    }
}
