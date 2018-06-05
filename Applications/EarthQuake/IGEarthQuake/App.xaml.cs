using System.Windows;
using System.Windows.Markup;
using IGShowcase.EarthQuake.Resources;
using IGExtensions.Framework.Controls; // provides NavigationApp
using IGExtensions.Common.Services;
using Infragistics.Themes; // provides ThemeManager

namespace IGShowcase.EarthQuake
{
    public partial class App : NavigationApp
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{
            this.Startup += ApplicationStartup;
            this.InitializeCulture(AppStrings.AppLanguage);
            
            // NOTE: comment out the following code when testing Japanese culture
            //this.InitializeCulture(SupportedCultures.ja);

            InitializeComponent();
             
            ThemeManager.ApplicationTheme = new MetroTheme();
            // merge app resources that use colors and styles defined in the Metro Theme
            this.Resources.MergeDictionary("/IGExtensions.Common;component/Assets/Styles/CommonStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.EarthQuake;component/Assets/Styles/AppBrushes.xaml");
            this.Resources.MergeDictionary("/IGShowcase.EarthQuake;component/Assets/Styles/AppStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.EarthQuake;component/Assets/Styles/DetailsStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.EarthQuake;component/Assets/Styles/GeoMapStyles.xaml");
		}
		#endregion Constructors
		#region Methods
		/// <summary>
		/// Handles the Startup event of the Application control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.StartupEventArgs"/> instance containing the event data.</param>
		private void ApplicationStartup(object sender, StartupEventArgs e)
		{
            var service = ServiceLocator.GetInstance<EarthQuakesService>();
            service.MagnitudeFilter.Min = GlobalSettings.EarthquakeMinMagnitude;
            service.MagnitudeFilter.Max = GlobalSettings.EarthquakeMaxMagnitude;

            var mainWindow = new MainWindow();
            mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
            this.MainWindow = mainWindow;
            this.MainWindow.Show();
		}

		#endregion 
	}
}