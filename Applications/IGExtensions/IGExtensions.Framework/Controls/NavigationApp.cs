using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Threading;
using IGExtensions.Framework;
using IGExtensions.Framework.Controls;

namespace IGExtensions.Framework.Controls
{
    /// <summary>
    /// Represents common wrapper for <see cref="System.Windows.Application"/>
    /// </summary>
    public class NavigationApp : Application
    {

        public static ErrorShowStackTrace ErrorPolicy { get; set; }

        /// <summary>
        /// Gets or sets application language: <see cref="System.Globalization.CultureInfo.Name"/> 
        /// </summary>
        public string AppLanguage { get { return this.GetAppLanguage(); } }
        private static NavigationApp _current;

        public static string CurrentCultureName()
        {
            return Thread.CurrentThread.CurrentUICulture.Name;
        }
        ///<summary>
        /// Gets current application 
        ///</summary>
        public new static NavigationApp Current
        {
            get { return _current; }
        }
        //TODO add method InitializeMainVisual(type MainWindow/MainPage)
//#if SILVERLIGHT
//            var mainPage = new MainPage();
//            //mainPage.Language = XmlLanguage.GetLanguage(this.AppLanguage);
//            this.RootVisual = mainPage;

//#else  // WPF
//            var mainWindow = new MainWindow();
//            mainWindow.Language = XmlLanguage.GetLanguage(this.AppLanguage);
//            this.MainWindow = mainWindow;
//            this.MainWindow.Show();
//#endif

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static UIElement MainVisual()
        {
#if SILVERLIGHT
            return Current.RootVisual;
#else // if WPF
            return Current.MainWindow;
#endif
        }
        ///// <summary>
        ///// 
        ///// </summary>
        //public FrameworkElement RootElement { get; set; }
       
        #region Constructor
        /// <summary>
        /// Constructor for NavigationApp
        /// </summary>
        public NavigationApp()
        {
            _current = this;

            //ErrorPolicy = ErrorShowStackTrace.OnlyWhenDebugging;
            ErrorPolicy = ErrorShowStackTrace.Always;
            DebugManager.LoggingLevel = DebugLogLevel.All;

            DebugManager.LogTrace("App initializing...");
            // NOTE: comment out the following code when testing Japanese culture
            //this.InitializeCulture(SupportedCultures.ja);

#if SILVERLIGHT
            this.UnhandledException += this.OnAppUnhandledException;
#else // if WPF
            this.DispatcherUnhandledException += this.OnAppUnhandledException;
#endif
        } 
        #endregion

        #region Error Handling
        /////<summary>
        ///// gets or sets an UI element that supports error reporting, e.g. MainWindow, MainPage
        /////</summary>
        //public IElementSupportsErrorReporting ErrorReportingElement { get; set; }
        ///// <summary>
        ///// Occurs when an unhandled exception is thrown
        ///// </summary>
        //public event EventHandler<UnhandledExceptionReportedEventArgs> UnhandledExceptionReported;

        /// <summary>
        /// Reports an exception in error dialog window using the ErrorReportingElement 
        /// </summary>
        /// <param name="exception"></param>
        public void ReportError(Exception exception)
        {
            ErrorWindow.Report(exception, ErrorPolicy);
            //var index = 1;
            //var message = exception.Message;
            //var stack = string.Format("Error #{0}: {1}\n{2}", index++, message, exception.StackTrace);
            //var stackTrace = new StringBuilder(stack);
            //exception = exception.InnerException;
            //while (exception != null)
            //{
            //    message = exception.Message;
            //    stack = string.Format("\nError #{0}: {1}\n{2}", index++, message, exception.StackTrace);
            //    stackTrace.Append(stack);
            //    exception = exception.InnerException;
            //}

            //if (this.ErrorReportingElement != null)
            //{
            //    this.ErrorReportingElement.ShowErrorDialog(message, stackTrace.ToString());
            //}
            //SL
            //System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + message + stackTrace.ToString() + "\");");
        }

#if SILVERLIGHT
        private void OnAppUnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // a ChildWindow control.
            if (Debugger.IsAttached)
            {
                //System.Diagnostics.Debugger.Break();
            }
            // Allow the application to continue running after an exception has been thrown but not handled. 
            // For production applications this error handling should be replaced with something that will 
            // report the error to the website and stop the application.
            e.Handled = true;

            // report the exception to user for information purpose
            ReportError(e.ExceptionObject);

            // Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportException(e.ExceptionObject); });
        }
#else // if WPF
        private void OnAppUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Allow the application to continue running after an exception has been thrown but not handled. 
            // For production applications this error handling should be replaced with something that will 
            // report the error to the website and stop the application.
            e.Handled = true;
            // report the exception to user for information purpose
            ReportError(e.Exception);
            //OnUnhandledExceptionReported(e.Exception, e.Handled);

        }
#endif
        //private void OnUnhandledExceptionReported(Exception ex, bool handled)
        //{
        //    // If the app is running outside of the debugger then report the exception using
        //    // the browser's exception mechanism. On IE this will display it an alert icon
        //    // in the status bar and Firefox will display a script error.
        //    //if (!System.Diagnostics.Debugger.IsAttached)
        //    //{
        //    //   //System.Diagnostics.Debugger.Break();

        //    //    if (UnhandledExceptionReported != null)
        //    //    {
        //    //        UnhandledExceptionReported(this, new UnhandledExceptionReportedEventArgs(ex));
        //    //    }
        //    //}

        //    //if (UnhandledExceptionReported != null)
        //    //{
        //    //    UnhandledExceptionReported(this, new UnhandledExceptionReportedEventArgs(ex, handled));
        //    //}
        //}
        //private void OnUnhandledExceptionReported(Exception ex)
        //{
        //    OnUnhandledExceptionReported(ex, true);
        //} 
        #endregion

        
        ///// <summary>
        ///// This method checks if we have a localization for the current culture's language and if not falls back 
        ///// the CurrentCulture and CurrentUICulture to the ones defined in the default resources.
        ///// It also sets the language of the root visual. This affects all converters in data bindings.
        ///// </summary>
        ///// <returns></returns>
        //public void InitializeCulture(CultureInfo forcedCulture)
        //{

        //    this.InitializeCulture(forcedCulture);
        //    this.AppLanguage = forcedCulture.Name;

           
        //}

    }
   
    /////<summary>
    /////</summary>
    //public interface IElementSupportsErrorReporting
    //{
    //    ///<summary>
    //    ///</summary>
    //    ///<param name="errorMessage"></param>
    //    ///<param name="errorDetails"></param>
    //    void ShowErrorDialog(string errorMessage, string errorDetails);

    //}

    ///// <summary>
    ///// Provides data for UnhandledExceptionReported event
    ///// </summary>
    //public class UnhandledExceptionReportedEventArgs : EventArgs
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="ex"></param>
    //    public UnhandledExceptionReportedEventArgs(Exception ex) : this(ex, true)
    //    { }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="ex"></param>
    //    /// <param name="handled"></param>
    //    public UnhandledExceptionReportedEventArgs(Exception ex, bool handled)
    //    {
    //        this.ExceptionObject = ex;
    //        this.Handled = handled;
    //    }
    //    public Exception ExceptionObject { get; set; }
    //    public bool Handled { get; set; }
    //}

    /// <summary>
    /// 
    /// </summary>
    public enum SupportedCultures
    {
        /// <summary>
        /// English culture 
        /// </summary>
        English,
        /// <summary>
        /// Japanese culture
        /// </summary>
        Japanese
    }

}
namespace System.Windows
{
    /// <summary>
    /// Represents a class that provides extensions for  <see cref="System.Windows.Application"/>
    /// </summary>
    public static class ApplicationEx
    {
        #region Localization Extensions
        /// <summary>
        /// Checks if we have a localization for the specified language and if not falls back 
        /// the CurrentCulture and CurrentUICulture to the ones defined in the default resources.
        /// It also sets the language of the root visual. This affects all converters in data bindings.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="forcedLanguageName">Four Letter Language Name, e.g. "en-US", "en"</param>
        /// <remarks>This method should be called after App.InitializeComponent() </remarks>
        public static void InitializeCulture(this Application app, string forcedLanguageName)
        {
            if ((forcedLanguageName.Length == 5 && forcedLanguageName.Contains("-") ||
                 forcedLanguageName.Length == 2))
            {
                var forcedCultur = new CultureInfo(forcedLanguageName);
                app.InitializeCulture(forcedCultur);
            }
            else
            {
                DebugManager.LogWarning("ApplicationEx.InitializeCulture() called with invalid forcedLanguageName parameter: " + forcedLanguageName);
            }

        }
        /// <summary>
        /// Checks if we have a localization for the specified language and if not falls back 
        /// the CurrentCulture and CurrentUICulture to the ones defined in the default resources.
        /// It also sets the language of the root visual. This affects all converters in data bindings.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="forcedCulture">Two Letter ISO Language Name</param>
        /// <remarks>This method should be called after App.InitializeComponent() </remarks>
        public static void InitializeCulture(this Application app, SupportedCultures forcedCulture)
        {
            var forcedLanguageName = "en-US";
            if (forcedCulture == SupportedCultures.Japanese)
                forcedLanguageName = "ja-JP";

            app.InitializeCulture(forcedLanguageName);
        }
        /// <summary>
        /// Checks if we have a localization for the current culture's language and if not falls back 
        /// the CurrentCulture and CurrentUICulture to the ones defined in the default resources.
        /// It also sets the language of the root visual. This affects all converters in data bindings.
        /// </summary>
        /// <remarks>This method should be called after App.InitializeComponent() </remarks>
        public static void InitializeCulture(this Application app, CultureInfo forcedCulture)
        {
            DebugManager.LogTrace("App initializing culture...");
            var uiCulture = CultureInfo.CurrentUICulture;
            //var resourceCulture = new CultureInfo("ja");

            if (uiCulture.TwoLetterISOLanguageName != forcedCulture.TwoLetterISOLanguageName)
            {
                Thread.CurrentThread.CurrentUICulture = forcedCulture;
            }
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;
            DebugManager.LogTrace("App initializing culture completed for " + forcedCulture.TwoLetterISOLanguageName);
           
            var visualElement = app.GetMainVisual();
            if (visualElement == null)
            {
                DebugManager.LogWarning("ApplicationEx.InitializeCulture() called before initialized App.RootVisual or App.MainWindow");
            }
            else
            {
                visualElement.Language = XmlLanguage.GetLanguage(forcedCulture.Name);
            }
        }
        public static void TestJapaneseCulture(this Application app)
        {
            app.InitializeCulture(SupportedCultures.Japanese);
        }
        public static void TestEnglishCulture(this Application app)
        {
            app.InitializeCulture(SupportedCultures.English);
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

#if SILVERLIGHT
        /// <summary>
        /// Returns main visual element for the current application, e.g. MainPage (SL) or MainWindow (WPF)
        /// </summary>
        public static FrameworkElement GetMainVisual(this Application app)
        {
            return app.RootVisual as FrameworkElement;
        }
#else // if WPF
        public static FrameworkElement GetMainVisual(this Application app)
        {
            return app.MainWindow;
        }

#endif

        #endregion
        #region Desinger Methods
        /// <summary>
        /// Checks if current application is in design mode, XAML view open in MS Blend or VS Designer.
        /// </summary>
        /// <remarks>This is useful to prevent loading of some code when in MS Blend or VS Designer.</remarks>
        public static bool IsInDesingMode(this Application app)
        {
            return app.IsInDesignModeStatic();
        }
        private static bool? _isInDesignMode;
        private static bool IsInDesignModeStatic(this Application app)
        {
            if (!_isInDesignMode.HasValue)
            {
#if SILVERLIGHT
                _isInDesignMode = DesignerProperties.IsInDesignTool;
#else
            var prop = DesignerProperties.IsInDesignModeProperty;
            _isInDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
#endif
            }

            if (_isInDesignMode != null && _isInDesignMode.HasValue) return _isInDesignMode.Value;
            return false;
        } 
        #endregion

        public static void Close()
        {
            

        }
    }

}