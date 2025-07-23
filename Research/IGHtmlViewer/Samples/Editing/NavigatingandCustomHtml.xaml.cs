using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using IGHtmlViewer.Resources;
using Infragistics.Samples.Framework;

namespace IGHtmlViewer.Samples.Editing
{
    public partial class NavigatingandCustomHtml : SampleContainer
    {
        DispatcherTimer t;

        // Windowless constructor should be implemented here.
        public NavigatingandCustomHtml()
        {
            InitializeComponent();

            t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            this.snippet.Loaded += new RoutedEventHandler(snippet_Loaded);
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

        void snippet_Loaded(object sender, RoutedEventArgs e)
        {
            this.snippet.Text = HTMLViewerStrings.HTML_HelloWorld;
        }

        private void URLLaunched(object sender, RoutedEventArgs e)
        {
            this.htmlViewer.SourceUri = new Uri(((ComboBoxItem)(this.urlCombobox.SelectedItem)).Content as string);
            this.viewerWindow.WindowState = Infragistics.Controls.Interactions.WindowState.Maximized;
        }

        private void SnippetLaunched(object sender, RoutedEventArgs e)
        {
            this.htmlViewer.SourceUri = null;
            this.htmlViewer.HtmlCode = this.snippet.Text;
            this.viewerWindow.WindowState = Infragistics.Controls.Interactions.WindowState.Maximized;
        }
    }
}