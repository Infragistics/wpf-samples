using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Threading;

namespace IGExtensions.Framework 
{
    /// <summary>
    /// Represents a debug manager for managing and logging messages in debug window
    /// </summary>
    public static class DebugManager  
    {
        static DebugManager()
        {
              InitializeLog();
        }
      
        internal static Dictionary<string, DateTime> ProcessTracking = new Dictionary<string, DateTime>();
        public static TimeSpan ProcessTimeWarning = TimeSpan.FromSeconds(5);
       
        public static TimeSpan Time(string processName, bool updateProcess = false)
        {
            var time = TimeSpan.FromSeconds(0);
            if (Debugger.IsAttached)
            {
                if (ProcessTracking.ContainsKey(processName))
                {
                    var message = "";
                    var start = ProcessTracking[processName];
                    time = DateTime.Now.Subtract(start);

                    if (time >= ProcessTimeWarning)
                        message += " <WARNING> ";

                    Log(message + processName + " completed in " + time.TotalSeconds + " seconds");

                    if (updateProcess)
                        ProcessTracking[processName] = DateTime.Now;
                    else
                        ProcessTracking.Remove(processName);
                }
                else
                {
                    ProcessTracking.Add(processName, DateTime.Now);
                }
            }
            return time;
        }
        //public static void TimeLog(string processName)
        //{
        //    var time = Time(processName);
        //    Log("Process -> " + processName + " completed in " + time.TotalSeconds + " seconds");
        //}
           
        /// <summary>
        /// Gets or sets debug logging level for writing messages in debug window
        /// </summary>
        public static DebugLogLevel LoggingLevel { get; set; }
        public static string LogFileName { get; set; }

        //public static DebugLogLevel LoggingLevel = DebugLogLevel.Error;
        public static string LoggingPrefix = "IGExtensions -> ";
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
            if (!message.ToUpper().StartsWith("<ERROR>"))
                 message = "<ERROR> " + message;

            Log(message, DebugLogLevel.Error);
        }
        public static void LogWarning(string message)
        {
            if (!message.ToUpper().StartsWith("<WARNING>"))
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

            var logTime = IsUsingLogTime ? LogTime() + " " : "";
            Debug.WriteLine(logTime + LoggingPrefix + message);

            LogToFile(LogTime() + " " + message);
    
           // Debug.WriteLine(LoggingPrepend + message);
        }

        internal static DispatcherTimer LogTimer = new DispatcherTimer();
        internal static List<string> LogStack = new List<string>();
        internal static List<string> LogHistory = new List<string>();

        public static void InitializeLog()
        {
            LoggingLevel = DebugLogLevel.Error;

            LogHistory = new List<string>(); 
            LogStack = new List<string>();
            LogTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(3)};
            LogTimer.Tick += OnLogTimerTick;
#if WPF
            LogFileName = "AppLog.txt";
            LogFilePath = GetLogPath() + LogFileName;
            ClearLog(); 
#endif
            LogTimer.Start();
        }

        static void OnLogTimerTick(object sender, EventArgs e)
        {
            if (LogStack.Count == 0) return;
            WriteToLogFile(LogStack); 
        
            LogHistory.AddRange(LogStack);
            LogStack = new List<string>();
        }
        public static void ClearLog()
        {
            LogHistory = new List<string>();
            LogStack = new List<string>();
#if WPF
            if (System.IO.File.Exists(LogFilePath))
                System.IO.File.Delete(LogFilePath);

            LogToFile(LogTime() + " Logging started..."); 
#endif
        }
        
        private static string LogFilePath { get; set; }
        
        public static void LogToFile(string strMessage)
        {
            LogStack.Add(strMessage);
        }

        public static void WriteToLogFile(List<string> messages)
        {
#if WPF
            // log string to a local file
            using (var writer = new StreamWriter(new FileStream(LogFilePath, FileMode.Append)))
            {
                foreach (var message in messages)
                {
                    writer.WriteLine(message);
                }
            }
#endif
        }

        private static string GetLogPath()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            var ind = path.LastIndexOf("\\", StringComparison.Ordinal);
            path = path.Substring(0, ind);
            path = path + @"\";
            return path;
        }

        public static string LogTime()
        {
            var time = DateTime.Now;
            var ms = string.Format("{0:000}", time.Millisecond);
            var hhmmss = string.Format("{0:00}:{1:00}:{2:00}", time.Hour, time.Minute, time.Second);
            var result = hhmmss + "." + ms;
            return result;
        }
    }
    /// <summary>
    /// Represents debugging log level, None -> Warning -> Error -> Trace -> Data -> All
    /// </summary>
    public enum DebugLogLevel
    {
        /// <summary>
        /// Logging Level 0
        /// </summary>
        None = 0,
        /// <summary>
        /// Logging Level 1
        /// </summary>
        Error = 1,
        /// <summary>
        /// Logging Level 2
        /// </summary>
        Warning = 2,
        /// <summary>
        /// Logging Level 3
        /// </summary>
        Trace = 3,
        /// <summary>
        /// Logging Level 4
        /// </summary>
        Data = 4,
        /// <summary>
        /// Logging Level 5
        /// </summary>
        All = 5,
        
    }
}