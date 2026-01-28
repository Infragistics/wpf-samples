using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using System.Windows.Browser;

namespace Infragistics.Samples.Shared.Controls
{
    public class MainPage : UserControl
    {
        Frame navigationFrame;

        public MainPage()
        {
            this.navigationFrame = new Frame();
            this.Content = navigationFrame;
            this.navigationFrame.Navigated += new System.Windows.Navigation.NavigatedEventHandler(navigationFrame_Navigated);
            this.navigationFrame.NavigationFailed += new NavigationFailedEventHandler(navigationFrame_NavigationFailed);
            this.navigationFrame.UriMapper = new CustomUriMapper();
        }

        void navigationFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {

        }

        void navigationFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            //try
            //{
            //    HtmlPage.Window.Invoke("onSampleLoaded", e.Uri);
            //}
            //catch (System.Exception)
            //{
            //}
        }
    }

    public class CustomUriMapper : UriMapperBase
    {
        // Regex uriRegex = new Regex("^/[^/]+(.*)$");
        public override Uri MapUri(Uri uri)
        {
            var samplePath = HtmlPage.Window.Invoke("getSLSamplePath", uri.ToString());
            if (samplePath == null)
            {
                // deprecated once proper js function is implemented 
                var fixedURI = uri.ToString();
                if (fixedURI.StartsWith("Samples."))
                {
                    fixedURI = "/" + fixedURI.Substring(8);
                    if (fixedURI.EndsWith(".sl"))
                    {
                        fixedURI = fixedURI.Substring(0, fixedURI.Length - 3).Replace('.', '/') + ".sl";
                    }
                    else
                        fixedURI = fixedURI.Replace('.', '/');
                }

                return new Uri("/Samples" + fixedURI + ".xaml", UriKind.Relative);
            }
            return new Uri("/Samples" + samplePath.ToString() + ".xaml", UriKind.Relative);
        }
    }
}

