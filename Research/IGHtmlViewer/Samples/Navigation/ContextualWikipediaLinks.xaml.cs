using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Infragistics.Samples.Framework;

namespace IGHtmlViewer.Samples.Navigation
{
    public partial class ContextualWikipediaLinks : SampleContainer
    {
        private string cultureURL;
        DispatcherTimer t;

        //Windowless constructor should be implemented here.
        public ContextualWikipediaLinks()
        {
            InitializeComponent();
            t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            this.Loaded += OnSampleLoaded;
        }


        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                cultureURL = "http://ja.wikipedia.org/wiki/";
                pnlEN.Visibility = Visibility.Collapsed;
                pnlJP.Visibility = Visibility.Visible;
            }
            else
            {
                cultureURL = "http://en.wikipedia.org/wiki/";
                pnlJP.Visibility = Visibility.Collapsed;
                pnlEN.Visibility = Visibility.Visible;
            }
            htmlViewer.SourceUri = new Uri(cultureURL + "Silverlight");
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            t.Stop();
            this.LayoutRoot.Children.Remove(this.htmlViewer);
            base.OnNavigatingFrom(e);
        }

        void t_Tick(object sender, EventArgs e)
        {
            if (htmlViewer != null)
            {
                htmlViewer.UpdateLayout();
                htmlViewer.InvalidateMeasure();
            }
        }

        private void text_MouseEnter(object sender, MouseEventArgs e)
        {
            string url = cultureURL + ((TextBlock)sender).Text;
            if (this.htmlViewer.SourceUri.OriginalString != url)
            {
                this.htmlViewer.SourceUri = new Uri(url);
            }
        }
    }
}