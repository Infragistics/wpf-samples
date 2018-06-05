using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace IGExtensions.Framework.Controls
{
    /// <summary>
    /// Represents a window for reporting exceptions with detailed information 
    /// </summary>
    public partial class ErrorWindow : NavigationWindow
    {
        #region Constructors
        //public ErrorWindow()
        //{
        //    InitializeComponent();
        //}
        internal ErrorWindow(string errorMessage, string errorDetails)
        {
            InitializeComponent();
#if WPF
            this.Topmost = true;
            this.ShowInTaskbar = false;
            this.WindowStyle = WindowStyle.ToolWindow;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.CanResize;
            this.Icon = null;
            this.Closing += (s, e) => Application.Current.Shutdown();
#endif
            this.ErrorMessage.Text = errorMessage;
            if (errorDetails != string.Empty)
            {
                this.ErrorDetailViewer.Visibility = Visibility.Visible;
                this.ErrorDetailViewer.ScrollToVerticalOffset(double.MaxValue);
            }
            else
            {
                this.ErrorDetailViewer.Visibility = Visibility.Collapsed;
            }
            this.ErrorDetails.Text = errorDetails;
        }

        #endregion
        private void OnOKButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

#if WPF
            Application.Current.Shutdown(); 
#endif
        }
        #region Methods

        /// <summary>
        /// Gets or sets policy for showing error stack trace in <see cref="ErrorWindow"/>
        /// </summary>
        public static ErrorShowStackTrace ShowErrorStackPolicy = ErrorShowStackTrace.OnlyWhenDebugging;
        /// <summary>
        /// Creates instance of <see cref="ErrorWindow"/> for all other factory Report methods will result in a call to this one
        /// </summary>
        /// <param name="message">Which message to display</param>
        /// <param name="stackTrace">The associated stack trace</param>
        /// <param name="policy">In which situations the stack trace should be appended to the message</param>
        private static void Report(string message, string stackTrace, ErrorShowStackTrace policy)
        {
            string errorDetails = string.Empty;

            if (NavigationApp.ErrorPolicy == ErrorShowStackTrace.Always ||
                policy == ErrorShowStackTrace.Always ||
                policy == ErrorShowStackTrace.OnlyWhenDebugging && Debugger.IsAttached)
            {
                errorDetails = stackTrace ?? string.Empty;
            }
            if (Debugger.IsAttached)
                DebugManager.LogError(message + Environment.NewLine + stackTrace);

            var ew = new ErrorWindow(message, errorDetails);
            ew.ShowDialog();
        }
        /// <summary>
        /// Creates a new Error Window given an error message.
        /// Current stack trace will be displayed if app is running under
        /// debug or on the local machine
        /// </summary>
        public static void Report(string message)
        {
            Report(message, ShowErrorStackPolicy);
        }
        /// <summary>
        /// Creates a new Error Window given an error message.
        /// </summary>
        public static void Report(string message, ErrorShowStackTrace policy)
        {
            Report(message, new StackTrace().ToString(), policy);
        }
        /// <summary>
        /// Creates a new Error Window given an exception.
        /// Current stack trace will be displayed if app is running under
        /// debug or on the local machine.
        /// </summary>
        public static void Report(Exception exception)
        {
            Report(exception, ShowErrorStackPolicy);
        }
        /// <summary>
        /// Creates a new Error Window given an exception.>
        /// </summary>
        public static void Report(Exception exception, ErrorShowStackTrace policy)
        {
            var error = new ErrorStackMessage(exception);

            Report(error.Message, error.Stack, policy);
        } 
        #endregion
    }
    /// <summary>
    /// Controls when a stack trace should be displayed on the Error Window
    /// Defaults to <see cref="OnlyWhenDebugging"/>
    /// </summary>
    public enum ErrorShowStackTrace
    {
        /// <summary>
        /// Stack trace is showed only when running with a debugger attached
        /// or when running the app on the local machine. Use this to get
        /// additional debug information you don't want the end users to see
        /// </summary>
        OnlyWhenDebugging,
        /// <summary>
        /// Always show the stack trace, even if debugging
        /// </summary>
        Always,
        /// <summary>
        /// Never show the stack trace, even when debugging
        /// </summary>
        Never
    }
    /// <summary>
    /// Represents error stack message
    /// </summary>
    public class ErrorStackMessage
    {
        public ErrorStackMessage()
        {
        }
        /// <summary>
        /// Creates Error Stack Message with nested exceptions included as stack
        /// </summary>
        public ErrorStackMessage(Exception exception)
        {
            var index = 1;
            var message = exception.Message;
            //var stack = string.Format("Error #{0}: {1}\n\n{2}", index++, message, exception.StackTrace);
            var stack = string.Format("Error #{0}: {1}\n{2}\n", index++, message, exception.StackTrace);
            var stackTrace = new StringBuilder(stack);
            exception = exception.InnerException;
            while (exception != null)
            {
                message = exception.Message;
                stack = string.Format("Error #{0}: {1}\n{2}\n", index++, message, exception.StackTrace);
              
                //stack = string.Format("Error #{0}: {1}\n", index++, 
                //    System.Environment.NewLine + message + 
                //    System.Environment.NewLine + exception.StackTrace);
                //stack = string.Format("\nError #{0}: {1}\n\n{2}", index++, message, exception.StackTrace);
                stackTrace.Append(stack);
                //stackTrace.Insert(0, stack);
                exception = exception.InnerException;
            }
            this.Message = message;
            this.Stack = stackTrace.ToString();
        }
        public string Message { get; set; }
        public string Stack { get; set; }
    }
}

namespace System
{
    public class AppException : Exception
    {
        public AppException() : base() { }
        public AppException(string message) : base(message) { }
        public AppException(string message, Exception inner) : base(message, inner) { }

        // A constructor is needed for serialization when an 
        // exception propagates from a remoting server to the client.  
        //protected AppException(System.Runtime.Serialization.SerializationInfo info,
        //    System.Runtime.Serialization.StreamingContext context) { }
    }

}

