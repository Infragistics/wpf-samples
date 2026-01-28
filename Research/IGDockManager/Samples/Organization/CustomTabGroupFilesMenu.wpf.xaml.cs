using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DockManager;

namespace IGDockManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CustomizingTabGroupFilesMenu.xaml
    /// </summary>
    public partial class CustomTabGroupFilesMenu : SampleContainer
    {
        public CustomTabGroupFilesMenu()
        {
            InitializeComponent();
        }

        private void dockManager_FilesMenuOpening(object sender, Infragistics.Windows.DockManager.Events.FilesMenuOpeningEventArgs e)
        {
            TabGroupPane groupPane = e.OriginalSource as TabGroupPane;
            if (groupPane == null)
                return;

            Separator sep = new Separator();
            sep.SetResourceReference(StyleProperty, Infragistics.Windows.DockManager.XamDockManager.MenuItemSeparatorStyleKey);
            e.Items.Add(sep);

            RoutedEventHandler clickHandler = new RoutedEventHandler(menuItem_Click);

            // Create MenuItems for selecting different themes and add them to the Items collection passed in the event args
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Black Theme";
            menuItem.Tag = "Office2k7Black";
            menuItem.Click += clickHandler;
            menuItem.SetResourceReference(StyleProperty, Infragistics.Windows.DockManager.XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Blue Theme";
            menuItem.Tag = "Office2k7Blue";
            menuItem.Click += clickHandler;
            menuItem.SetResourceReference(StyleProperty, Infragistics.Windows.DockManager.XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Silver Theme";
            menuItem.Tag = "Office2k7Silver";
            menuItem.Click += clickHandler;
            menuItem.SetResourceReference(StyleProperty, Infragistics.Windows.DockManager.XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            // Assign the Theme we stored in the MenuItem's Tag property to the XamDockManager.
            MenuItem menuItem = e.Source as MenuItem;
            if (menuItem != null)
                this.dockManager.Theme = (string)menuItem.Tag;
        }
    }
}
