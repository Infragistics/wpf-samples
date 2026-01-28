using System;
using System.Windows;
using IGContextMenu.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using IGContextMenu.Controls;
using System.Windows.Media;

namespace IGContextMenu.Samples.Display
{
    public partial class CascadingMenus : SampleContainer
    {
        public CascadingMenus()
        {
            InitializeComponent();
            this.Unloaded += new RoutedEventHandler(CascadingMenus_Unloaded);
        }

        void CascadingMenus_Unloaded(object sender, RoutedEventArgs e)
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

        private void XamContextMenu_Opening(object sender, OpeningEventArgs e)
        {
            var elements = e.GetClickedElements<BlueRectangle>();
            if (elements.Count != 0)
                elements[0].TextForeground = new SolidColorBrush(Colors.LightGray);
        }

        private void XamContextMenu_Closing(object sender, Infragistics.CancellableEventArgs e)
        {
            rectangle.TextForeground = new SolidColorBrush(Colors.White);
        }
    }
}
