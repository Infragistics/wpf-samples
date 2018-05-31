using System.Windows;
using System.Windows.Markup;
using IGExtensions.Framework.Controls;
using Infragistics.Themes; // provides ThemeManager

namespace IGShowcase.AirplaneSeatingChart
{
    public partial class App : NavigationApp  
    {  
        public App()
        {
            ThemeManager.ApplicationTheme = new MetroTheme();
            // merge app resources that use colors and styles defined in the Metro Theme
            this.Resources.MergeDictionary("/IGExtensions.Common;component/Assets/Styles/CommonStyles.xaml");
            this.Resources.MergeDictionary("/IGShowcase.AirplaneSeatingChart;component/Assets/Styles/AppBrushes.xaml");
            this.Resources.MergeDictionary("/IGShowcase.AirplaneSeatingChart;component/Assets/Styles/AppStyles.xaml"); 
            this.Resources.MergeDictionary("/IGShowcase.AirplaneSeatingChart;component/Assets/Styles/AmentitiesShapes.xaml");
            this.Resources.MergeDictionary("/IGShowcase.AirplaneSeatingChart;component/Assets/Styles/XamDataGridStyles.xaml");
        
            this.Startup += this.OnAppStartup;  
        }

        private void OnAppStartup(object sender, StartupEventArgs e)
        { 

#if SILVERLIGHT 
            var mainPage = new MainPage();
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
