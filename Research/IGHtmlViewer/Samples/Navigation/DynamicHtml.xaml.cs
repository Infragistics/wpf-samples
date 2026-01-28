using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using IGHtmlViewer.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.DataProviders;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Models.TeamForce;

namespace IGHtmlViewer.Samples.Navigation
{
    public partial class DynamicHtml : SampleContainer
    {
        DispatcherTimer t;

        // Windowless constructor should be implemented here.
        public DynamicHtml()
        {
            InitializeComponent();

            t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, 50);
            t.Tick += new EventHandler(t_Tick);
            t.Start();

            this.Loaded += OnSampleLoaded;
        }

        private TeamForceViewModel viewModel;
        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.viewModel = new TeamForceViewModel();
            this.DataContext = this.viewModel;
            this.viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(viewModel_PropertyChanged);
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

        void viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.SetNodeExpandedState(this.customersTree.XamTreeItems, true);

            if (this.customersTree.XamTreeItems.First().XamTreeItems.Count() > 0)
            {
                this.customersTree.XamTreeItems.First().XamTreeItems.First().IsSelected = true;
            }
        }

        private void customersTree_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0 && e.AddedItems[0] is Customer)
            {
                Customer customer = e.AddedItems[0] as Customer;

                string str = HTMLViewerStrings.HTML_ContactCardBase;

                while (str.Contains("$AgentImage.png"))
                {
                    str = str.Replace("$AgentImage.png", customer.ImageResourcePath);
                }

                while (str.Contains("$Fname"))
                {
                    str = str.Replace("$Fname", customer.ContactName.Split(' ')[0]);
                }

                while (str.Contains("$Lname"))
                {
                    str = str.Replace("$Lname", customer.ContactName.Split(' ')[1]);
                }

                while (str.Contains("$Position"))
                {
                    str = str.Replace("$Position", customer.ContactTitle);
                }

                while (str.Contains("$E-mail"))
                {
                    str = str.Replace("$E-mail", customer.ContactEmail);
                }

                while (str.Contains("$Telephone"))
                {
                    str = str.Replace("$Telephone", customer.Phone);
                }

                while (str.Contains("$Fax"))
                {
                    str = str.Replace("$Fax", customer.Fax);
                }

                while (str.Contains("$Address"))
                {
                    str = str.Replace("$Address", customer.AddressOne + "</br>" + customer.AddressTwo);
                }

                this.htmlViewer.HtmlCode = str;
                (this.htmlViewer.Parent as Infragistics.Controls.Interactions.XamDialogWindow).WindowState =
                    Infragistics.Controls.Interactions.WindowState.Maximized;
            }
        }

        private bool isFirstTime = true;
        void OnTreeLoaded(object sender, RoutedEventArgs e)
        {
            if (this.isFirstTime)
            {
                Infragistics.Controls.Menus.XamTree tree = (Infragistics.Controls.Menus.XamTree)sender;
                this.SetNodeExpandedState(tree.XamTreeItems, true);

                if (tree.Items.Count > 0)
                {
                    Infragistics.Controls.Menus.XamTreeItem rootNode = tree.XamTreeItems[0];
                    rootNode.XamTreeItems[0].IsSelected = true;
                }
                this.isFirstTime = false;
            }
        }

        private void SetNodeExpandedState(Infragistics.Controls.Menus.XamTreeItemsCollection nodes, bool expandNode)
        {
            foreach (Infragistics.Controls.Menus.XamTreeItem item in nodes)
            {
                item.IsExpanded = expandNode;
                this.SetNodeExpandedState(item.XamTreeItems, expandNode);
            }
        }

        private void XamDialogWindow_WindowStateChanged(object sender, Infragistics.Controls.Interactions.WindowStateChangedEventArgs e)
        {
            if (e.NewWindowState == Infragistics.Controls.Interactions.WindowState.Hidden)
                this.launch.Visibility = Visibility.Visible;
            else
                this.launch.Visibility = Visibility.Collapsed;
        }

        private void launch_Click(object sender, RoutedEventArgs e)
        {
            (this.htmlViewer.Parent as Infragistics.Controls.Interactions.XamDialogWindow).WindowState =
                Infragistics.Controls.Interactions.WindowState.Maximized;
        }
    }
}