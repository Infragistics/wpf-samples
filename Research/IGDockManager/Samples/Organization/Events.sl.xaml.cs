using System;
using System.Windows.Controls;
using IGDockManager.Resources;
using Infragistics.Controls.Layouts;
using Infragistics.Samples.Framework;

namespace IGDockManager.Samples.Organization
{
    public partial class Events : SampleContainer
    {
        private bool IsInitialized = false;

        public Events()
        {
            InitializeComponent();
            IsInitialized = true;
        }

        private void xamDockManager_ActivePaneChanging(object sender, ActivePaneChangingEventArgs e)
        {
            if (IsInitialized)
            {
                PrintLog("ActivePaneChanging: " + DockManagerStrings.Docking_PreviousActivePane + e.PreviousPane.Header + " " + DockManagerStrings.Docking_NewActivePane + e.NewPane.Header);
            }
        }

        private void xamDockManager_ActivePaneChanged(object sender, ActivePaneChangedEventArgs e)
        {
            if (IsInitialized)
            {
                PrintLog("ActivePaneChanged: " + e.NewPane.Header);
            }
        }

        private void xamDockManager_PaneClosing(object sender, CancellablePaneEventArgs e)
        {
            if (e.Pane.Name == "OutputPane")
            {
                e.Cancel = true;
                PrintLog(DockManagerStrings.Docking_PaneClosingCancelled + " " + e.Pane.Name);
            }
            else
            {
                PrintLog("PaneClosing: " + e.Pane.Name);
            }
        }

        private void xamDockManager_PaneClosed(object sender, PaneEventArgs e)
        {
            PrintLog("PaneClosed: " + e.Pane.Name);
        }

        private void xamDockManager_PaneFloating(object sender, CancellablePaneEventArgs e)
        {
            PrintLog("PaneFloating: " + DockManagerStrings.Docking_FloatTop + e.Pane.FloatTop + DockManagerStrings.Docking_FloatLeft + e.Pane.FloatLeft);
        }

        private void xamDockManager_PaneFloated(object sender, PaneEventArgs e)
        {
            PrintLog("PaneFloated: " + DockManagerStrings.Docking_FloatTop + e.Pane.FloatTop + " " + DockManagerStrings.Docking_FloatLeft + e.Pane.FloatLeft);
        }

        private void xamDockManager_PaneUnpinning(object sender, CancellablePaneEventArgs e)
        {
            if (e.Pane.Name == "OutputPane")
            {
                e.Cancel = true;
                PrintLog(DockManagerStrings.Docking_PaneUnpinningCancelled + e.Pane.Name);
            }
            else
            {
                PrintLog("PaneUnpinning: " + e.Pane.Name);
            }
        }

        private void xamDockManager_PaneUnpinned(object sender, PaneEventArgs e)
        {
            PrintLog("PaneUnpinned: " + e.Pane.Name);
        }

        private void PrintLog(string msg)
        {
            ContentPane cp = (ContentPane)xamDockManager.FindPane("OutputPane");
            if (cp != null)
            {
                ScrollViewer scrollViewer = cp.Content as ScrollViewer;

                ListBox listBox = (ListBox)scrollViewer.Content;

                string logMsg = "[" + DateTime.Now.ToString("HH:mm") + "] " + msg + "\n";

                ListBoxItem lstBoxItem = new ListBoxItem
                {
                    Content = logMsg,
                    FontSize = 10,
                    Height = 20
                };

                listBox.Items.Insert(0, lstBoxItem);
            }
        }

        private void HyperlinkButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ListBox listBox = this.FindName("ListBox_Output") as ListBox;
            if (listBox != null && listBox.Items.Count > 0)
            {
                listBox.Items.Clear();
            }
        }
    }
}
