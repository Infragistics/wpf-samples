using System;
using IGMenu.Resources;
using Infragistics.Controls.Menus;
using Infragistics.Controls.Menus.Primitives;
using Infragistics.Samples.Framework;

namespace IGMenu.Samples.Editing
{
    public partial class AddRemoveDisable : SampleContainer
    {
        public AddRemoveDisable()
        {
            InitializeComponent();
            this.MainMenu.ItemClicked += new EventHandler<ItemClickedEventArgs>(MainMenu_ItemClicked);
        }

        void MainMenu_ItemClicked(object sender, ItemClickedEventArgs e)
        {
            XamHeaderedItemsControl item = e.Item as XamHeaderedItemsControl;
            XamMenuItem itemParent = item.Parent as XamMenuItem;

            if (itemParent != null)
            {
                if ((itemParent.Header.ToString() == MenuStrings.XWM_AddRemoveDisable_Add) && (item.Header.ToString() == MenuStrings.XWM_AddRemoveDisable_AddNewItem))
                {
                    itemParent.Items.Add(new XamMenuItem() { Header = MenuStrings.XWM_AddRemoveDisable_NewlyAddedItem });
                }
                else if (itemParent.Header.ToString() == MenuStrings.XWM_AddRemoveDisable_Remove)
                {
                    itemParent.Items.Remove(item);
                }
                else if (itemParent.Header.ToString() == MenuStrings.XWM_AddRemoveDisable_Disable)
                {
                    item.IsEnabled = false;
                }
            }
        }
    }
}
