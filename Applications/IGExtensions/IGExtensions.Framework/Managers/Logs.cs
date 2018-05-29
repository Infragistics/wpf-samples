﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Infragistics.Framework
{
    /// <summary> Specifies logging level of debug messages   </summary>
    [Flags]
    public enum LogLevel
    {
        All         = 1 << 7,
        Message     = 1 << 5,
        Trace       = 1 << 4,
        Task        = 1 << 3,
        Warning     = 1 << 2,
        Error       = 1 << 1,
        None        = 1,
    }
    
    /// <summary> Represents logging utility with options to filter log messages </summary>
    public static class Logs
    {
        static Logs()
        {
            Tasks = new Dictionary<string, DateTime>();
#if DEBUG
            LogLevel = LogLevel.All;
            UseLogTime = true;
#else
            LogLevel = LogLevel.None;
            UseLogTime = false;
#endif
            LogStart = DateTime.Now;
        }
        #region Properties
        public static bool UseLogTime { get; set; }
        public static LogLevel LogLevel { get; set; }

        internal static Dictionary<string, DateTime> Tasks;
        internal static DateTime LogStart { get; set; }

        #endregion
        #region Methods Public
        public static void Message(string message)
        {
            Report(message, LogLevel.Message);
        }
        public static void Message(object sender, string message)
        {
            Report(sender, message, LogLevel.Message);
        }
        public static void Warning(string message)
        {
            Report(message, LogLevel.Warning);
        }
        public static void Warning(object sender, string message)
        {
            Report(sender, message, LogLevel.Warning);
        }
        public static void Error(string message)
        {
            Report(message, LogLevel.Error);
        }
        public static void Error(object sender, string message)
        {
            Report(sender, message, LogLevel.Error);
        }
        public static void Trace(string message)
        {
            Report(message, LogLevel.Trace);
        }
        public static void Trace(object sender, string message)
        {
            Report(sender, message, LogLevel.Trace);
        }
        internal static void Task(object sender, string message)
        {
            message = sender.GetType().Name + " " + message;
            Task(message);
        }
        public static void Task(string task)
        {
            var message = task + "...";
            if (Tasks.ContainsKey(task))
            {
                var duration = GetDuration(Tasks[task], DateTime.Now);
                message += " completed in ";
                //message = message.PadRight(150); 
                message += duration.Format() + " seconds";

                if (duration.TotalSeconds > 3)
                {
                    message += " <<< WARNING - LONG TASK! ";
                }
                Tasks[task] = DateTime.Now;
            }
            else
            {
                Tasks.Add(task, DateTime.Now);
            }

            Report(message, LogLevel.Task);
        } 
        #endregion
        #region Methods Internal
        internal static TimeSpan GetDuration(DateTime timeStart, DateTime timeStop)
        {
            return timeStop.Subtract(timeStart);
        }
        internal static string Format(this TimeSpan duration)
        {
            return duration.ToString(@"ss\.fff");
        }
        internal static void Report(object sender, string message, LogLevel logLevel)
        {
            message = sender.GetType().Name + " " + message;
            Report(message, logLevel);
        }
        internal static void Report(string message, LogLevel logLevel)
        {
            if (!Debugger.IsAttached) return;

            if (LogLevel < logLevel) return;

            var output = string.Empty;
            if (UseLogTime)
            {
                var duration = GetDuration(LogStart, DateTime.Now);
                LogStart = DateTime.Now;
                output += duration.Format() + " ";
            }

            if (logLevel == LogLevel.Warning)
            {
                output += ">>> WARNING " + message;
            }
            else if (logLevel == LogLevel.Error)
            {
                output += ">>> ERROR " + message;
            } 
            else
            {
                output += ">>> " + message;
            }
            OnLogsChanged(new LogEventArgs(output));

            Debug.WriteLine(output);
        }

        #endregion
        public static event EventHandler<LogEventArgs> LogsChanged;

        private static void OnLogsChanged(LogEventArgs e)
        {
            var handler = LogsChanged;
            if (handler != null)
            {
                handler(null, e);
            }
        }
       
    }

    public class LogEventArgs : EventArgs
    {
        /// <summary> Gets or sets Message </summary>
        public string Message { get; set; }

        public LogEventArgs(string message)
        {
            Message = message;
        }

    }
}