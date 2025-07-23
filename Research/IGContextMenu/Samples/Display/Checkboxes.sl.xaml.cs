using System;
using System.Windows;
using IGContextMenu.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGContextMenu.Samples.Display
{
    public partial class Checkboxes : SampleContainer
    {
        public Checkboxes()
        {
            InitializeComponent();
            this.Unloaded += new RoutedEventHandler(Checkboxes_Unloaded);
        }

        void Checkboxes_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ConMenuMan.ContextMenu.IsOpen = false;
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
    }
}
