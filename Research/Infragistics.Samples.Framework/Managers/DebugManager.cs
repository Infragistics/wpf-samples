using System;
using System.Diagnostics;

namespace Infragistics.Samples.Framework
{
    /// <summary>
    /// Represents a debug manager for managing and logging messages in debug window
    /// </summary>
    public static class DebugManager //: // System.Diagnostics.Debug 
    {
        /// <summary>
        /// Gets or sets debug logging level for writing messages in debug window
        /// </summary>
        public static DebugLogLevel LoggingLevel = DebugLogLevel.Error;
        public static string LoggingPrepend = "Samples.Framework -> ";
        public static bool IsUsingLogTime = false;

        public static void LogData(string message)
        {
            Log(message, DebugLogLevel.Data);
        }
        public static void LogError(Exception exception)
        {
            LogError(exception.ToString());
        }
        public static void LogError(string message)
        {
            if (message.ToUpper().StartsWith("<ERROR>"))
                message = "<ERROR> " + message;

            Log(message, DebugLogLevel.Error);
        }
        public static void LogWarning(string message)
        {
            if (message.ToUpper().StartsWith("<WARNING>"))
                message = "<WARNING> " + message;

            Log(message, DebugLogLevel.Warning);
        }
        public static void LogTrace(string message)
        {
            Log(message, DebugLogLevel.Trace);
        }
        public static void Log(Exception exception)
        {
            LogError(exception.ToString());
        }

        public static void Log(string message, DebugLogLevel loggingLevel = DebugLogLevel.Trace)
        {
            if (LoggingLevel == DebugLogLevel.None) return;

            //var messageKey = message.ToUpper();
            //if (messageKey.Contains("ERROR") || messageKey.Contains("ERROR"))
            //    loggingLevel = DebugLogLevel.Error;

            //else if (messageKey.Contains("WARNING"))
            //    loggingLevel = DebugLogLevel.Warning;

            if (loggingLevel > LoggingLevel) return;

            var logTime = IsUsingLogTime ? GetTime() + " " : "";
            Debug.WriteLine(logTime + LoggingPrepend + message);
      
           // Debug.WriteLine(LoggingPrepend + message);
        }
        public static string GetTime()
        {
            var time = DateTime.Now;
            var ms = string.Format("{0:000}", time.Millisecond);
            var result = time.Hour + ":" + time.Minute + ":" + time.Second + "." + ms;
            return result;
        }
    }
    /// <summary>
    /// Represents debugging log level
    /// </summary>
    public enum DebugLogLevel
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
      
        /// <summary>
        /// 
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 
        /// </summary>
        Error = 3,
        /// <summary>
        /// 
        /// </summary>
        Trace = 4,
        /// <summary>
        /// 
        /// </summary>
        Data = 5,
        /// <summary>
        /// 
        /// </summary>
        All = 10,
        
    }
}
