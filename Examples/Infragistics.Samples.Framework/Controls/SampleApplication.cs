using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Markup;

namespace Infragistics.Samples.Framework
{
    /// <summary>
    /// Represents common wrapper for <see cref="System.Windows.Application"/> with extended localization features provided by the <see cref="Infragistics.Samples.Framework.ApplicationEx"/> extension.
    /// </summary>
    public class SampleApplication : Application
    {

        #region Localization
        /// <summary>
        /// Gets application language: <see cref="System.Globalization.CultureInfo.Name"/> 
        /// </summary>
        public string AppLanguage { get; private set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor for NavigationApp
        /// </summary>
        public SampleApplication()
        {
            // NOTE: comment out the following code when testing Japanese culture
            //this.ForceCulture(SupportedCultures.ja);

            this.DispatcherUnhandledException += this.OnAppUnhandledException;
        } 
        #endregion

        #region Event Handlers
        private void OnAppUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            //TODO: implement standardized error handling and reporting for WPF
            //e.Handled = true;
            //OnUnhandledExceptionReported(e.Exception, e.Handled);
            //ReportError(e.Exception);
        }
	#endregion
    }
    public enum SupportedCultures
    {
        /// <summary>
        /// English 
        /// </summary>
        en,
        /// <summary>
        /// Japanese
        /// </summary>
        ja
    }

    public static class ApplicationEx
    {
        #region Localization
        /// <summary>
        /// Checks if we have a localization for the current culture's language and if not falls back 
        /// the CurrentCulture and CurrentUICulture to the ones defined in the default resources.
        /// It also sets the language of the root visual. This affects all converters in data bindings.
        /// </summary>
        /// <param name="forcedLanguageName">Four Letter Language Name, e.g. "en-US"</param>
        public static void ForceCulture(this Application app, string forcedLanguageName)
        {
            if (forcedLanguageName.Length == 5 && forcedLanguageName.Contains("-"))
            {
                var forcedCultur = new CultureInfo(forcedLanguageName);
                app.ForceCulture(forcedCultur);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Warning: SampleApplication.ForceLocalization() called with invalid forcedLanguageName parameter: " + forcedLanguageName);
            }

        }
        /// <summary>
        /// Checks if we have a localization for the current culture's language and if not falls back 
        /// the CurrentCulture and CurrentUICulture to the ones defined in the default resources.
        /// It also sets the language of the root visual. This affects all converters in data bindings.
        /// </summary>
        /// <param name="forcedCulture">Two Letter ISO Language Name</param>
        public static void ForceCulture(this Application app, SupportedCultures forcedCulture)
        {
            var forcedLanguageName = "en-US";
            if (forcedCulture == SupportedCultures.ja)
                forcedLanguageName = "ja-JP";

            app.ForceCulture(forcedLanguageName);
        }

        /// <summary>
        /// Checks if we have a localization for the current culture's language and if not falls back 
        /// the CurrentCulture and CurrentUICulture to the ones defined in the default resources.
        /// It also sets the language of the root visual. This affects all converters in data bindings.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="forcedCulture"></param>
        public static void ForceCulture(this Application app, CultureInfo forcedCulture)
        {
            var uiCulture = CultureInfo.CurrentUICulture;
            //var resourceCulture = new CultureInfo("ja");

            if (uiCulture.TwoLetterISOLanguageName != forcedCulture.TwoLetterISOLanguageName)
            {
                Thread.CurrentThread.CurrentUICulture = forcedCulture;
            }
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

            var visualElement = app.GetMainVisualElement();
            if (visualElement == null)
            {
                System.Diagnostics.Debug.WriteLine("Warning: SampleApplication.ForceLocalization() called before initialized App.RootVisual or App.MainWindow");
            }
            else
            {
                visualElement.Language = XmlLanguage.GetLanguage(forcedCulture.Name);
            }
        }
        public static string GetAppLanguage(this Application app)
        {
            return Thread.CurrentThread.CurrentUICulture.Name;
        }
        /// <summary>
        /// Get Current UI Culture Information 
        /// </summary>
        /// <returns></returns>
        public static CultureInfo GetCurrentUiCultureInfo()
        {
            CultureInfo uiCulture = CultureInfo.CurrentUICulture;
            //#if WPF 
            //            // Let's get the startup parameters to see if we need to "force" a locale.  We can look at the query parameters"
            //            // for click-once deployments, and command line parameters for a normal execution.  
            //            // We skip the first command line parameter as it is always the path to the running exe.
            //            System.Collections.Specialized.NameValueCollection cmdlineParams = HttpQueryProvider.QueryStringParameters() ??
            //                                System.Environment.GetCommandLineArgs().Skip(1).ToArray().ToNameValueCollection();
            //            string lc = cmdlineParams["lc"];

            //            if (lc != null)
            //            {
            //                uiCulture = new CultureInfo(lc);
            //                System.Threading.Thread.CurrentThread.CurrentUICulture = uiCulture;
            //            }
            //#endif
            return uiCulture;
        }
        #endregion

        #region Helper Methods
		
        public static FrameworkElement GetMainVisualElement(this Application app)
        {
            return app.MainWindow;
        }

	    #endregion

    }
}