using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DockManager;

namespace IGDockManager.Samples.Organization
{
    /// <summary>
    /// Interaction logic for CustomizingContentPaneContextMenu.xaml
    /// </summary>
    public partial class CustomizingContentPaneContextMenu : SampleContainer
    {
        public CustomizingContentPaneContextMenu()
        {
            InitializeComponent();
        }

        // =====================================================================================================
        // The ContentPane.OptionsMenuOpening event is fired when a ContentPane's ContextMenu is about to open.
        // This collection of items on the ContextMenu is passed in the event args and you can add or remove
        // MenuItems from the collection.
        // =====================================================================================================
        void ContentPane_OptionsMenuOpening(object sender, Infragistics.Windows.DockManager.Events.PaneOptionsMenuOpeningEventArgs e)
        {
            ContentPane contentPane = e.Source as ContentPane;
            if (contentPane == null)
                return;


            // Create MenuItems for selecting different themes and add them to the Items collection passed in the event args
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Black Theme";
            menuItem.Tag = "Office2k7Black";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, Infragistics.Windows.DockManager.XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Blue Theme";
            menuItem.Tag = "Office2k7Blue";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, Infragistics.Windows.DockManager.XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Silver Theme";
            menuItem.Tag = "Office2k7Silver";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, Infragistics.Windows.DockManager.XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);
        }

        void menuItem_Click(object sender, RoutedEventArgs e)
        {
            // Assign the Theme we stored in the MenuItem's Tag property to the XamDockManager.
            MenuItem menuItem = e.Source as MenuItem;
            if (menuItem != null)
                this.dockManager.Theme = (string)menuItem.Tag;
        }
    }
}
