using System;
using System.Windows.Browser;

namespace Infragistics.Samples.Framework.Utility
{
    /// <summary>
    /// SL: Provides useful methods for SL Sample Browser
    /// </summary>
    public static class BrowserHelper
    {
        public static string GetCurrentHostPath()
        {
            string rootUrl = HtmlPage.Document.DocumentUri.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
            rootUrl = rootUrl.ToLower();

            if (rootUrl.EndsWith("default.aspx"))
            {
                rootUrl = rootUrl.Replace("default.aspx", "");
            }
            else if (!rootUrl.EndsWith("/"))
            {
                rootUrl = rootUrl + @"/";
            }
            return rootUrl;
        }

        public static string GetCurrentHostUri()
        {
            return BrowserHelper.GetUriComponent(UriComponents.SchemeAndServer);
        }

        private static string GetUriComponent(UriComponents componentName)
        {
            return HtmlPage.Document.DocumentUri.GetComponents(componentName, UriFormat.UriEscaped);
        }

        public static string GetCurrentPageUri()
        {
            return HtmlPage.Document.DocumentUri.AbsoluteUri;
        }

        public static string GetCurrentPagePath()
        {
            return HtmlPage.Document.DocumentUri.AbsolutePath;
        }

        public static void BookmarkCurrentPage(string title)
        {
            BrowserHelper.BookmarkPage(title, BrowserHelper.GetCurrentPageUri());
        }

        public static void BookmarkPage(string title, string uri)
        {
            HtmlPage.Window.CreateInstance("AddBookmark", new string[] { title, uri });
        }

    }
}
