using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using IGHtmlViewer.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;

namespace IGHtmlViewer.Samples.Organization
{
    public partial class RuntimeConfiguration : SampleContainer
    {
        double left = 0;
        double right = 0;
        double up = 0;
        double down = 0;

        double incrementalStep = 5;

        DispatcherTimer t;

        public RuntimeConfiguration()
        {
            InitializeComponent();

            t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            this.Loaded += OnSampleLoaded;

            neWidth.Value = 300d;
            neHeight.Value = 200d;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.htmlViewer.SourceUri = SamplePagesPathProvider.GetSamplePageUri("ControlDimensionsSampleLayout.aspx");
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

        private void MoveWindow(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content as string;

            if (content == HTMLViewerStrings.HTML_Nav_Up)
            {
                up -= incrementalStep;
            }
            if (content == HTMLViewerStrings.HTML_Nav_Down)
            {
                up += incrementalStep;
            }
            if (content == HTMLViewerStrings.HTML_Nav_Left)
            {
                left -= incrementalStep;
            }
            if (content == HTMLViewerStrings.HTML_Nav_Right)
            {
                left += incrementalStep;
            }

            this.htmlViewer.Margin = new Thickness(left, up, right, down);
        }

        private void checkTree_Click(object sender, RoutedEventArgs e)
        {
            object obj = VisualTreeHelper.GetParent(this.htmlViewer);

            if (obj == null)
            {
                MessageBox.Show(HTMLViewerStrings.HTML_Nav_NotFound);
            }
            else
            {
                MessageBox.Show(HTMLViewerStrings.HTML_Nav_Found);
            }
        }

        private void addHTMLViewer_Click(object sender, RoutedEventArgs e)
        {
            if (VisualTreeHelper.GetParent(this.htmlViewer) != null)
            {
                MessageBox.Show(HTMLViewerStrings.HTML_Nav_IsInTree);
            }
            else
            {
                this.LayoutRoot.Children.Add(this.htmlViewer);
            }
        }

        private void removeHTMLViewer_Click(object sender, RoutedEventArgs e)
        {
            this.LayoutRoot.Children.Remove(this.htmlViewer);
        }

        private void VisibilityChanged(object sender, RoutedEventArgs e)
        {
            if (this.htmlViewer != null)
            {
                if (this.htmlViewer.Visibility == Visibility.Visible)
                    this.htmlViewer.Visibility = Visibility.Collapsed;
                else
                    this.htmlViewer.Visibility = Visibility.Visible;
            }
        }
    }
}