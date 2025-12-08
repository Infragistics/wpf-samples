using IGDataChart.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Samples.Framework;
using Markdig;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace IGDataChart.Samples.Layers
{
    /// <summary>
    /// Interaction logic for UserAnnotationLayer.xaml
    /// </summary>
    public partial class UserAnnotationLayer : SampleContainer
    {
        public UserAnnotationLayer()
        {
            InitializeComponent();
            UseDefaultTheme = true;
        }

        private void Chart_UserAnnotationInformationRequested(object sender, Infragistics.Controls.Charts.UserAnnotationInformationEventArgs args)
        {
            var loc = new Point(args.AnnotationInfo.DialogSuggestedXLocation, args.AnnotationInfo.DialogSuggestedYLocation);
            var dialog = new AnnotationDialog()
            {             
                WindowStartupLocation = WindowStartupLocation.Manual,
                Width = 400,
                Height = 400,
                Left = loc.X,
                Top = loc.Y,
                AnnotationInformation = args.AnnotationInfo
            };
            dialog.Closed += Dialog_Closed;

            dialog.Show();
        }

        private void Dialog_Closed(object sender, EventArgs e)
        {
            var d = (AnnotationDialog)sender;
            if (d.ShouldCommit)
            {
                Chart.FinishAnnotationFlow(d.AnnotationInformation);
            }
            else
            {
                Chart.CancelAnnotationFlow(d.AnnotationInformation.AnnotationId);
            }
        }

        private async void Wv_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            var wv = (WebView2)sender;
            var tc = "white";
            await wv.CoreWebView2.ExecuteScriptAsync("document.body.style.color = '" + tc + "'");
        }

        private async void Chart_UserAnnotationToolTipContentUpdating(object sender, Infragistics.Controls.Charts.UserAnnotationToolTipContentUpdatingEventArgs args)
        {
            var cc = (ContentControl)args.Content;
            Grid g = null;
            WebView2 wv = null;

            if (cc.Content == null)
            {
                g = new Grid();
                g.Width = 150;
                g.Height = 200;
                cc.Content = g;

                wv = new WebView2();
                wv.NavigationCompleted += Wv_NavigationCompleted;
                g.Children.Add(wv);

            }
            else
            {
                g = (Grid)cc.Content;
                wv = (WebView2)g.Children[0];
            }

            await wv.EnsureCoreWebView2Async();
            if (args.AnnotationInfo.AnnotationData != null)
            {
                var html = Markdown.ToHtml(args.AnnotationInfo.AnnotationData);

                var info = args.AnnotationInfo;
                var bg = info.MainColor;



                html = Regex.Replace(html, "<a ", "<a target=\"_blank\" ");

                if (!html.Equals(wv.Tag))
                {
                    wv.Tag = html;
                    wv.NavigateToString(html);
                }
            }
        }
    }
}
