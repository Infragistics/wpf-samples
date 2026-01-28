using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Resources;
using System.Windows.Threading;
using ResStrings = Infragistics.SamplesBrowser.Properties.Resources;

namespace Infragistics.SamplesBrowser
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Uncomment one of the following code lines to test against other cultures.
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr-FR");
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-ES");
            //Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ja-JP");

            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);

            if (Thread.CurrentThread.CurrentCulture.Name.StartsWith("ja"))
            {
                // Ensure the current culture passed into bindings is the OS culture.
                // By default, WPF uses en-US as the culture, regardless of the system settings.
                FrameworkElement.LanguageProperty.OverrideMetadata(
                  typeof(FrameworkElement),
                  new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
            }
        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Handle the exception when AdomdClient is not installed
            if (e.Exception.InnerException != null 
                && ((e.Exception).InnerException).Message.Contains("AdomdClient"))
            {
                var result = MessageBox.Show(ResStrings.PivotGrid_ADOMD_Error_Message, ResStrings.PivotGrid_ADOMD_Error_Header, MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start("http://www.microsoft.com/en-us/download/details.aspx?id=16978");
                }
                
                ((SamplesBrowserWindow)(App.Current.MainWindow)).frSample.Navigate(new Uri("Infragistics.SamplesBrowser;component/WelcomePages/PivotGridWelcome.xaml", UriKind.Relative));
                e.Handled = true;
                return;
            }

            // TODO: Uncomment the following lines after debug phase. (GH 9-21-2007)
            System.Diagnostics.Debug.WriteLine(e.Exception.ToString());
            HandleException(e.Exception);
            e.Handled = true;
        }

        /// <summary>
        /// Shows the a friendly exception message to the user.
        /// </summary>
        /// <param name="ex"></param>
        /// <remarks>In Debug mode, it will provide the exception details through its ToString implementation; in Release, it will only show the message.</remarks>
        public static void HandleException(Exception ex)
        {
            if (ex == null)
                return;
            var displayMessage = ResStrings.GeneralExceptionPrompt + System.Environment.NewLine + System.Environment.NewLine;
            while ((ex is System.Reflection.TargetInvocationException || ex is System.Windows.Markup.XamlParseException) && ex.InnerException != null)
                ex = ex.InnerException;
#if (DEBUG)
            displayMessage += ex.ToString();
#else
            displayMessage += ex.Message;
#endif
            MessageBox.Show(displayMessage, ResStrings.GeneralExceptionTitle, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Samples should call this method to extract an assembly resource.
        /// NOTE: Be sure to dispose of the Stream that gets returned.
        /// </summary>
        /// <param name="resourceFile">A pack URI pointing to a resource in the EXE.</param>
        public static Stream GetResourceStream(string resourceFile)
        {
            Uri uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);

            StreamResourceInfo info = Application.GetResourceStream(uri);
            if (info == null || info.Stream == null)
                throw new ApplicationException("Missing resource file: " + resourceFile);

            return info.Stream;
        }
    }
}
