using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Browser;

namespace Infragistics.Samples.Framework.Data
{
    public static class RemotingFactory
    {
        private const string URL_PATTERN = "products/{0}/";
        private const string ACTUAL_URL = "samplesbrowser/";

        private static string _url_pattern = String.Empty;

        static RemotingFactory()
        {
            _url_pattern = String.Format(URL_PATTERN, ((InfragisticsApplication)Application.Current).ProductFamilyName).ToLower();
        }

        public static void WithClient<CLIENT, INTERFACE>(params Action<CLIENT>[] actions)
            where CLIENT : new()
            where INTERFACE : class
        {
            var client = new CLIENT();
            var clientBase = client as ClientBase<INTERFACE>;

            ConfigureEndpointAddress(clientBase);

            try
            {
                foreach (var action in actions)
                {
                    Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "RemotingFactory action ({0}) defined by ({1})", action.Method.Name, action.Method.DeclaringType));

                    action.Invoke(client);
                }
            }
            catch (TimeoutException)
            {
                clientBase.Abort();
                HtmlPage.Window.Alert("Timeout while trying to reach the server. Please restart the application and try again.");
            }
            catch (CommunicationException)
            {
                clientBase.Abort();
                HtmlPage.Window.Alert("Unable to reach the server. Please restart the application and try again.");
            }
            finally
            {
                var method = client.GetType().GetMethod("CloseAsync", new Type[] { });
                method.Invoke(client, new object[] { });
            }
        }

        private static void WithClientAsync_AsyncCallback<CLIENT>(IAsyncResult result)
             where CLIENT : new()
        {
            if (result.IsCompleted)
            {
                var obj = Cast(result.AsyncState, new { Clinet = new CLIENT(), Action = new Action<CLIENT>(delegate(CLIENT c) { }) });

                obj.Action.EndInvoke(result);

                var method = obj.Clinet.GetType().GetMethod("CloseAsync", new Type[] { });

                method.Invoke(obj.Clinet, new object[] { });
            }
        }

        private static T Cast<T>(object obj, T t)
        {
            return (T)obj;
        }


        public static void WithClient<CLIENT>(params Action<CLIENT>[] actions)
            where CLIENT : new()
        {
            var client = new CLIENT();

            try
            {
                foreach (var action in actions)
                {
                    Debug.WriteLine(string.Format(CultureInfo.InvariantCulture, "RemotingFactory action ({0}) defined by ({1})", action.Method.Name, action.Method.DeclaringType));

                    action.Invoke(client);
                }
            }
            catch (TimeoutException)
            {
                //clientBase.Abort();
                HtmlPage.Window.Alert("Timeout while trying to reach the server. Please restart the application and try again.");
            }
            catch (CommunicationException)
            {
                //clientBase.Abort();
                HtmlPage.Window.Alert("Unable to reach the server. Please restart the application and try again.");
            }
            finally
            {
                var method = client.GetType().GetMethod("CloseAsync", new Type[] { });
                method.Invoke(client, new object[] { });
            }
        }

        public static T WithClientEasy<CLIENT, INTERFACE, T>(Func<CLIENT, T> action)
            where CLIENT : new()
            where INTERFACE : class
        {
            var client = new CLIENT();
            var clientBase = client as ClientBase<INTERFACE>;

            ConfigureEndpointAddress(clientBase);

            try
            {
                return action.Invoke(client);
            }
            catch (TimeoutException)
            {
                if (clientBase != null) clientBase.Abort();
                HtmlPage.Window.Alert("Timeout while trying to reach the server. Please restart the application and try again.");
                throw;
            }
            catch (CommunicationException)
            {
                if (clientBase != null) clientBase.Abort();
                HtmlPage.Window.Alert("Unable to reach the server. Please restart the application and try again.");
                throw;
            }
            finally
            {
                var method = client.GetType().GetMethod("CloseAsync", new Type[] { });
                method.Invoke(client, new object[] { });
            }
        }

        public static EventHandler<T> WrapEvent<T>(Action<object, T> onSuccess) where T : AsyncCompletedEventArgs
        {
            return WrapEvent(onSuccess, null);
        }

        public static EventHandler<T> WrapEvent<T>(Action<object, T> onSuccess, Action<object, T, Exception> onFailure) where T : AsyncCompletedEventArgs
        {
            return ((sender, eventArgs) =>
            {
                //this only checks for WCF exceptions
                if (eventArgs.Error != null)
                    HandleError(onFailure, sender, eventArgs, eventArgs.Error);
                else onSuccess.Invoke(sender, eventArgs);
            });
        }

        private static void HandleError<T>(Action<object, T, Exception> onFailure, object sender, T eventArgs, Exception message) where T : AsyncCompletedEventArgs
        {
            //If there is no onFailure specified
            if (onFailure != null)
                onFailure.Invoke(sender, eventArgs, message);
            else
            {
                // default error handling
                var current = message;

                var builder = new StringBuilder();
                while ((current = current.InnerException) != null)
                {
                    builder.AppendLine(current.GetType().FullName);
                    builder.AppendLine(current.Message);
                    builder.AppendLine(current.StackTrace);
                }
            }
        }

        static readonly System.Text.RegularExpressions.Regex hostRegex = new System.Text.RegularExpressions.Regex(@"https?://([^/:]*)((:\d+)*)/");

        public static string GetHost(string url)
        {
            return hostRegex.Match(url).Value;
        }

        private static void ConfigureEndpointAddress<I>(ClientBase<I> clientBase) where I : class
        {
            // Returns something like /my/Service.svc           
            var absolutePath = clientBase.Endpoint.Address.Uri.AbsolutePath;
            var svcName = absolutePath.Substring(absolutePath.IndexOf(' ') + 1, absolutePath.Length - absolutePath.IndexOf(' ') - 1);

            //var uri = new Uri(Application.Current.Host.Source, svcName);
            string hostUrl = GetAbsoluteUrl(svcName.Remove(0, 1));

            var uri = new Uri(hostUrl);
            clientBase.Endpoint.Address = new EndpointAddress(uri);

        }

        public static string GetAbsoluteUrl(string strRelativePath)
        {
            if (string.IsNullOrEmpty(strRelativePath))
                return strRelativePath;

            string strFullUrl;
            if (strRelativePath.StartsWith("http:", StringComparison.OrdinalIgnoreCase)
              || strRelativePath.StartsWith("https:", StringComparison.OrdinalIgnoreCase)
              || strRelativePath.StartsWith("file:", StringComparison.OrdinalIgnoreCase)
              )
            {
                //already absolute
                strFullUrl = strRelativePath;
            }
            else
            {
                //relative, need to convert to absolute
                strFullUrl = System.Windows.Application.Current.Host.Source.AbsoluteUri;

                var host = GetHost(strFullUrl);

                strFullUrl = String.Format("{0}/{1}", host.TrimEnd('/'), strRelativePath.TrimStart('/')).ToLower();
            }

            if (strFullUrl.Contains(_url_pattern))
            {
                strFullUrl = strFullUrl.Replace(_url_pattern, ACTUAL_URL);

            }

            return strFullUrl;
        }
    }
}
