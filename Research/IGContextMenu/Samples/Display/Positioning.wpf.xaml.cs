using System.Windows;
using System.Windows.Controls;
using IGContextMenu.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;

namespace IGContextMenu.Samples.Display
{
    public partial class Positioning : SampleContainer
    {
        public Positioning()
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

        private void PlacementTargetChanged(object sender, SelectionChangedEventArgs e)
        {
            Infragistics.Controls.Menus.ContextMenuManager contextMenu = null;
            if (this.rect != null)
                contextMenu = Infragistics.Controls.Menus.ContextMenuService.GetManager(this.rect);
            if (contextMenu != null)
            {
                var selectedItem = (ComboBoxItem)placementTargetComboBox.SelectedItem;
                if (selectedItem.Content.ToString() == MenuStrings.CM_Positioning_PlacementTarget_None)
                    contextMenu.ContextMenu.PlacementTarget = null;
                else if (selectedItem.Content.ToString() == MenuStrings.CM_Positioning_PlacementTarget_SmallRect)
                {
                    if (((PlacementMode)(this.placementComboBox.Value)) == PlacementMode.MouseClick)
                    {
                        this.placementComboBox.Value = PlacementMode.AlignedAbove;
                    }
                    contextMenu.ContextMenu.PlacementTarget = this.smallRect;
                }
            }
        }
    }
}
