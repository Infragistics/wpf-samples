using System;
using System.IO;
using System.Diagnostics; 
using System.Reflection;

namespace System
{
    //TODO-MT re-use in the new SB
    public static class LOG  
    {
        public static string Prefix = "";
        static TextWriterTraceListener Listner;  
        static LOG()
        {            
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            Listner = new TextWriterTraceListener(new StreamWriter("trace.log", false));
       
            Trace.Listeners.Add(Listner);
            Trace.AutoFlush = true;
             
            Info("Running...");
        } 
        static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {   
            var error = e.ExceptionObject as Exception;
            var message = GetSender() + " ERROR " + error.ToString();  
            Console.WriteLine(message); 
            Listner.WriteLine(message + "\n" + error.StackTrace);

            Close();
            Environment.Exit(-1);
        }
        static string GetAssemblyName()
        {
            return Assembly.GetExecutingAssembly().GetName().Name;
        }
        static string GetSender()
        {
            if (string.IsNullOrEmpty(Prefix))
                return GetAssemblyName();

            return GetAssemblyName() + " " + Prefix;
        }
        public static void Info(string message)
        {
            message = GetSender() + " " + message;
            Console.WriteLine(message); 
            Listner.WriteLine(message);  
        }
        public static void Warn(string message)
        {
            message = GetSender() + " WARNING " + message; 
            Console.WriteLine(message);  
            Listner.WriteLine(message);  
        }
        public static void Error(string message)
        {
            message = GetSender() + " ERROR " + message;  
            Console.WriteLine(message);  
            Listner.WriteLine(message); 
        }
        public static void Close()
        {
            var message = GetAssemblyName() + " Closing...";
            Console.WriteLine(message); 
            Listner.WriteLine(message);  
            Listner.Close(); 
        }
    }

}
