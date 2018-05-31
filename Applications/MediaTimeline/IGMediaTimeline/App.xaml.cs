using System.Windows;
using System.Windows.Markup;
using IGExtensions.Framework.Controls;
using MediaTimeline.Assets.Resources;
using Infragistics.Themes; // provides ThemeManager

namespace MediaTimeline
{
    public partial class App : NavigationApp
	{
		
		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{
			InitializeComponent();

            ThemeManager.ApplicationTheme = new MetroTheme();
            // merge app resources that use colors and styles defined in the Metro Theme
            this.Resources.MergeDictionary("/InfragisticsWPF.Themes.Metro;component/themes/theme.colors.xaml"); 
            this.Resources.MergeDictionary("/IGExtensions.Common;component/Assets/Styles/CommonStyles.xaml"); 
            this.Resources.MergeDictionary("/IGShowcase.MediaTimeline;component/Assets/Styles/AppBrushes.xaml");
            this.Resources.MergeDictionary("/IGShowcase.MediaTimeline;component/Assets/Styles/AppStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.MediaTimeline;component/Styles.xaml");
             
			Startup += Application_Startup;
             
            this.InitializeCulture(AppStrings.Language);
        }
		#endregion Constructor
        
		#region Event Handlers
		/// <summary>
		/// Handles the Startup event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
		private void Application_Startup(object sender, StartupEventArgs e)
		{
            var mainWindow = new MainWindow();
            mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.MainWindow = mainWindow;
            this.MainWindow.Show();
		}
    
		#endregion Event Handlers
	}
}