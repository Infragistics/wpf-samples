using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Windows.DockManager;
using Infragistics.Windows.DockManager.Events;

namespace IGDockManager.Samples.Style
{
    /// <summary>
    /// Interaction logic for SettingTheTheme.xaml
    /// </summary>
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            InitializeComponent();
        }

        #region Event Handlers

        // =====================================================================================================
        // The ContentPane.OptionsMenuOpening event is fired when a ContentPane's ContextMenu is about to open.
        // This collection of items on the ContextMenu is passed in the event args and you can add or remove
        // MenuItems from the collection.
        // =====================================================================================================
        private void ContentPane_OptionsMenuOpening(object sender, PaneOptionsMenuOpeningEventArgs e)
        {
            ContentPane contentPane = e.Source as ContentPane;
            if (contentPane == null)
                return;

            // Create MenuItems for selecting different themes and add them to the Items collection passed in the event args
            MenuItem menuItem = new MenuItem();
            menuItem.Header = "Aero";
            menuItem.Tag = "Aero";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "IG Theme";
            menuItem.Tag = "IGTheme";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Luna Normal";
            menuItem.Tag = "LunaNormal";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Luna Olive";
            menuItem.Tag = "LunaOlive";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Luna Silver";
            menuItem.Tag = "LunaSilver";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Metro";
            menuItem.Tag = "Metro";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "MetroDark";
            menuItem.Tag = "MetroDark";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2010 Blue";
            menuItem.Tag = "Office2010Blue";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2013";
            menuItem.Tag = "Office2013";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Black Theme";
            menuItem.Tag = "Office2k7Black";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Blue Theme";
            menuItem.Tag = "Office2k7Blue";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Office 2007 Silver Theme";
            menuItem.Tag = "Office2k7Silver";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Royal Dark";
            menuItem.Tag = "RoyalDark";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.Header = "Royal Light";
            menuItem.Tag = "RoyalLight";
            menuItem.Click += new RoutedEventHandler(menuItem_Click);
            menuItem.SetResourceReference(StyleProperty, XamDockManager.MenuItemStyleKey);
            e.Items.Add(menuItem);
        }

        private void menuItem_Click(object sender, RoutedEventArgs e)
        {
            // Assign the Theme we stored in the MenuItem's Tag property to the XamDockManager.
            MenuItem menuItem = e.Source as MenuItem;
            if (menuItem != null)
                this.dockManager.Theme = (string)menuItem.Tag;
        }

        #endregion Event Handlers
    }
}
