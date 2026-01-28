using System.Windows;
using System.Windows.Controls;
using Infragistics.Windows.DockManager;

namespace IGDockManager.CustomControls
{
    /// <summary>
    /// Interaction logic for XdmDockingBehaviorOptionsControl.xaml
    /// </summary>
    public partial class XdmDockingBehaviorOptionsControl : UserControl
    {
        public XdmDockingBehaviorOptionsControl()
        {
            InitializeComponent();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            ContentPane parent = (ContentPane)this.Parent;
            switch (cbx.Name.ToString())
            {
                case "AllowClose":
                    if (cbx.IsChecked.Value == true)
                    {
                        parent.AllowClose = true;
                    }
                    else
                    {
                        parent.AllowClose = false;
                    }
                    break;
                case "AllowPinning":
                    if (cbx.IsChecked.Value == true)
                    {
                        parent.AllowPinning = true;
                    }
                    else
                    {
                        parent.AllowPinning = false;
                    }
                    break;
                case "AllowDocking":
                    if (cbx.IsChecked.Value == true)
                    {
                        parent.AllowDocking = true;
                        AllowDockingInTabGroup.IsEnabled = true;
                        AllowFloatingOnly.IsEnabled = true;
                    }
                    else
                    {
                        parent.AllowDocking = false;
                        AllowDockingInTabGroup.IsEnabled = false;
                        AllowFloatingOnly.IsEnabled = false;
                    }
                    break;
                case "AllowDockingInTabGroup":
                    if (cbx.IsChecked.Value == true)
                    {
                        parent.AllowDockingInTabGroup = true;
                    }
                    else
                    {
                        parent.AllowDockingInTabGroup = false;
                    }
                    break;
                case "AllowFloatingOnly":
                    if (cbx.IsChecked.Value == true)
                    {
                        parent.AllowFloatingOnly = true;
                    }
                    else
                    {
                        parent.AllowFloatingOnly = false;
                    }
                    break;
                case "AllowInDocumentHost":
                    if (cbx.IsChecked.Value == true)
                    {
                        parent.AllowInDocumentHost = true;
                    }
                    else
                    {
                        parent.AllowInDocumentHost = false;
                    }
                    break;
            }
        }

        private void SyncCheckBoxes_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPane parent = (ContentPane)this.Parent;

            if (parent.AllowClose) { AllowClose.IsChecked = true; }
            if (parent.AllowDocking) { AllowDocking.IsChecked = true; }
            if (parent.AllowDockingInTabGroup) { AllowDockingInTabGroup.IsChecked = true; }
            if (parent.AllowInDocumentHost) { AllowInDocumentHost.IsChecked = true; }
            if (parent.AllowFloatingOnly) { AllowFloatingOnly.IsChecked = true; }
            if (parent.AllowPinning) { AllowPinning.IsChecked = true; }
        }
    }
}
