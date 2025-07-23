using System;
using System.Windows;
using System.Windows.Threading;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGHtmlViewer.Samples.Editing
{
    public partial class BasicHTMLViewer : SampleContainer
    {
        DispatcherTimer t;

        public BasicHTMLViewer()
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
            this.htmlViewer.SourceUri = SamplePagesPathProvider.GetSamplePageUri("BasicHtmlViewerSampleLayout.aspx");
        }

        void t_Tick(object sender, EventArgs e)
        {
            if (htmlViewer != null)
            {
                htmlViewer.UpdateLayout();
                htmlViewer.InvalidateMeasure();
            }
        }

        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            t.Stop();
            this.LayoutRoot.Children.Remove(this.htmlViewer);
            base.OnNavigatingFrom(e);
        }
    }
}