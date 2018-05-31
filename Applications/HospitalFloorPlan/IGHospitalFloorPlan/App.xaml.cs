using System.Windows;
using IGExtensions.Framework.Controls;  // NavigationApp with error handling and culture initialization
using IGShowcase.HospitalFloorPlan.Assets.Resources;
using Infragistics.Themes; // provides ThemeManager

namespace IGShowcase.HospitalFloorPlan
{
    public partial class App : NavigationApp
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{ 
            ThemeManager.ApplicationTheme = new MetroTheme();
            // merge app resources that use colors and styles defined in the Metro Theme
            this.Resources.MergeDictionary("/InfragisticsWPF.Themes.Metro;component/themes/theme.colors.xaml"); 
            this.Resources.MergeDictionary("/IGExtensions.Common;component/Assets/Styles/CommonStyles.xaml");
            this.Resources.MergeDictionary("/IGExtensions.Common;component/Assets/Styles/GlobalStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.HospitalFloorPlan;component/Assets/Styles/AppBrushes.xaml");
            this.Resources.MergeDictionary("/IGShowcase.HospitalFloorPlan;component/Assets/Styles/AppStyles.xaml");
             
			Startup += ApplicationStartup;
			  
            this.InitializeCulture(Strings.Language);

            // NOTE: comment out the following code when testing Japanese culture
            //this.InitializeCulture(SupportedCultures.Japanese);
		}

		/// <summary>
		/// Handles the Startup event of the Application control.
		/// </summary>
		private void ApplicationStartup(object sender, StartupEventArgs e)
		{
            var mainWindow = new MainWindow();
            //mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.MainWindow = mainWindow;
            this.MainWindow.Show();
		}
 
	}
}