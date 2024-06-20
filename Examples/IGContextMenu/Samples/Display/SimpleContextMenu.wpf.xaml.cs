using System;
using System.Windows;
using System.Windows.Media;
using IGContextMenu.Controls;
using IGContextMenu.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGContextMenu.Samples.Display
{
    public partial class SimpleContextMenu : SampleContainer
    {
        public SimpleContextMenu()
        {
            InitializeComponent();
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
            // In this event handler you can expect the elements that were clicked:
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
