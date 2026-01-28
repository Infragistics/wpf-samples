using System;
using System.Windows;
using System.Windows.Input;
using IGContextMenu.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGContextMenu.Samples.Display
{
    public partial class OpeningBehavior : SampleContainer
    {
        public OpeningBehavior()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            this.Unloaded += new RoutedEventHandler(OpeningBehavior_Unloaded);
        }

        void OpeningBehavior_Unloaded(object sender, RoutedEventArgs e)
        {
            this.contextMenuManager.ContextMenu.IsOpen = false;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in cboModifierKeys.EnumValueHolders)
            {
                if (item.Name == "Windows")
                {
                    cboModifierKeys.EnumValueHolders.Remove(item);
                    break;
                }
            }
        }

        private void XamContextMenu_ItemClicked(object sender, ItemClickedEventArgs e)
        {
            if (System.Threading.Thread.CurrentThread.CurrentCulture.Name.ToLower() == "ja-jp")
            {
                MessageBox.Show(e.Item.Header.ToString() + MenuStrings.CM_YouHaveJustClicked);
            }
            else
            {
                MessageBox.Show(MenuStrings.CM_YouHaveJustClicked + e.Item.Header.ToString());
            }
        }

        private void XamMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MenuStrings.CM_MenuItem1Clicked);
        }

        private void cboModifierKeys_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (blueRect != null)
                ContextMenuService.GetManager(blueRect).ModifierKeys = (ModifierKeys)(cboModifierKeys.Value);
        }

        private void cboOpenMode_ValueChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (blueRect != null)
                ContextMenuService.GetManager(blueRect).OpenMode = (OpenMode)cboOpenMode.Value;
        }
    }
}
