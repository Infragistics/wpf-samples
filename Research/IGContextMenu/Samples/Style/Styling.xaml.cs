using System.Windows;
using IGContextMenu.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGContextMenu.Samples.Style
{
    public partial class Styling : SampleContainer
    {
        public Styling()
        {
            InitializeComponent();
            this.Unloaded += new RoutedEventHandler(Styling_Unloaded);
        }

        void Styling_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ConMenuMan.ContextMenu.IsOpen = false;
        }

        private void XamContextMenu_ItemClicked(object sender, ItemClickedEventArgs e)
        {
            MessageBox.Show(MenuStrings.CM_YouHaveJustClickedAMenuItem);
        }
    }
}
