using System.Windows;
using System.Windows.Markup;
using IGExtensions.Framework.Controls;

namespace IGShowcase.AirplaneSeatingChart
{
    public partial class App : NavigationApp //Application
    {
        public App()
        {
            this.Startup += this.OnAppStartup;
            //this.Exit += this.OnAppExit;
           
            InitializeComponent();
            //this.InitializeCulture(Strings.AppLanguage);
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
