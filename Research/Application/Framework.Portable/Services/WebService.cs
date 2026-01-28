using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infragistics.Framework.Services
{
    /// <summary>
    /// Represents a cross-platform service for downloading data from web
    /// </summary>
    public class WebService
    {
        #region Private
        private static void Iterate<TResult>(IEnumerable<Task> asyncIterator,
           TaskCompletionSource<TResult> tcs)
        {
            var enumerator = asyncIterator.GetEnumerator();
            Action<Task> recursiveBody = null;
            recursiveBody = completedTask =>
            {
                if (completedTask != null && completedTask.IsFaulted)
                {
                    tcs.TrySetException(completedTask.Exception.InnerExceptions);
                    enumerator.Dispose();
                }
                else if (enumerator.MoveNext())
                {
                    enumerator.Current.ContinueWith(recursiveBody, TaskContinuationOptions.ExecuteSynchronously);
                }
                else enumerator.Dispose();
            };
            recursiveBody(null);
        }
        
        private static WebServiceResponse Update(
            WebServiceResponse response, HttpWebResponse httpResponse)
        {
            Logs.Message("WebService -> Getting web response completed: " + httpResponse.StatusDescription);

            response.Uri = httpResponse.ResponseUri;
            response.Method = httpResponse.Method;
            response.StatusCode = httpResponse.StatusCode;
            response.StatusInfo = httpResponse.StatusDescription;
            response.ResultType = httpResponse.ContentType;
            response.ResultLength = httpResponse.ContentLength;

            Logs.Message("WebService -> Getting web response stream");
            response.ResultStream = httpResponse.GetResponseStream();

            return response;
        }

        private static WebServiceResponse Update(WebServiceResponse response, Exception ex)
        {
            Logs.Message("WebService -> Getting web response failed: " + ex.Message);

            response.StatusCode = HttpStatusCode.NotAcceptable;
            response.ErrorMessage = ex.Message;
            response.ErrorStack = ex.Message + "\n" + ex.StackTrace;
            response.ErrorOccured = true;
            return response;
        }

        #endregion
        public static Task<WebServiceResponse> RequestAsync(string url, object userState)
        {
            var tcs = new TaskCompletionSource<WebServiceResponse>();
            Iterate(RequestAsyncTask(url, userState, tcs), tcs);
            return tcs.Task;
        }

        private static IEnumerable<Task> RequestAsyncTask(string url, object userState,
            TaskCompletionSource<WebServiceResponse> tcs)
        {
            Logs.Message("WebService -> Creating web request " + url);
            var response = new WebServiceResponse();
            response.RequestUri = url;
            response.UserState = userState;
                       
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);

            Logs.Message("WebService -> Getting web response...");
            var httpTask = Task.Factory.FromAsync<WebResponse>
               (httpRequest.BeginGetResponse, httpRequest.EndGetResponse, null);
            yield return httpTask;

            try
            {
                // get web response  
                using (var httpResponse = (HttpWebResponse)httpTask.Result)
                {
                    response = Update(response, httpResponse);
                    tcs.TrySetResult(response);
                }
            }
            catch (Exception error)
            {
                response =  Update(response,error);
                tcs.TrySetResult(response);
            }

        }

        public static void Request(string url, object userState = null, 
            Action<WebServiceResponse> callback = null)
        {
            Logs.Message("WebService -> Creating web request " + url + "...");
           
            try
            {
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);

                Logs.Message("WebService -> Getting web response...");
                var result = (IAsyncResult)httpRequest.BeginGetResponse(
                    new AsyncCallback(delegate(IAsyncResult tempResult)
                {
                    try
                    {
                        var httpResponse = (HttpWebResponse)httpRequest.EndGetResponse(tempResult);

                        var response = new WebServiceResponse();
                        response.RequestUri = url;
                        response.UserState = userState;
                        response = Update(response, httpResponse);

                        if (callback != null)
                            callback(response);
                    }
                    catch (WebException e)
                    {
                        var response = new WebServiceResponse();
                        response.RequestUri = url;
                        response.UserState = userState;
                        response = Update(response, e);
                        if (callback != null)
                            callback(response);
                    }
                    
                }), null);
            }
            catch (Exception error)
            {
                var response = new WebServiceResponse();
                response.RequestUri = url;
                response.UserState = userState;
                response = Update(response, error);
                if (callback != null)
                    callback(response);
            }
        }
    }
}