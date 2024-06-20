using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using IGContextMenu.Resources;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;

namespace IGContextMenu.Samples.Style
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Theming_Loaded);
            this.Unloaded += new RoutedEventHandler(Theming_Unloaded);
        }

        void Theming_Loaded(object sender, RoutedEventArgs e)
        {
            List<Theme> themes = new List<Theme>
            {
                new Theme("IG", CommonStrings.XW_ThemeSupport_IGTheme,
                    new Uri("/Infragistics.Themes.IGTheme;component/IG.xamMenu.xaml", UriKind.Relative)),
                new Theme("Default", CommonStrings.XW_ThemeSupport_DefaultTheme, null),
                new Theme("Blue", CommonStrings.XW_ThemeSupport_Office2010BlueTheme,
                    new Uri("/Infragistics.Themes.Office2010BlueTheme;component/Office2010Blue.xamMenu.xaml", UriKind.Relative)),
                new Theme("Metro", CommonStrings.XW_ThemeSupport_Metro,
                    new Uri("/Infragistics.Themes.MetroTheme;component/Metro.xamMenu.xaml", UriKind.Relative)),
                new Theme("MetroDark", CommonStrings.XW_ThemeSupport_MetroDark,
                    new Uri("/Infragistics.Themes.MetroDarkTheme;component/MetroDark.xamMenu.xaml", UriKind.Relative)),
            };

            themeChangeCombo.ItemsSource = themes;
            themeChangeCombo.SelectedIndex = 0;
        }

        void Theming_Unloaded(object sender, RoutedEventArgs e)
        {
            this.ConMenuMan.ContextMenu.IsOpen = false;
            // restore the IG themes for the xamDockManager and xamMenu
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamMenu");
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary() { Source = new Uri("/Infragistics.Themes.IGTheme;component/IG.xamMenu.xaml", UriKind.Relative) });
        }

        private void themeChangeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Theme selectedTheme = (Theme)e.AddedItems[0];

                // remove any available theme for the specified control
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamMenu");

                if (selectedTheme.ThemeUri != null)
                {
                    // add the newly selected theme
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = selectedTheme.ThemeUri });
                }
            }
        }

        private void XamMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(MenuStrings.CM_MenuItem1Clicked);
        }

        private void XamContextMenu_ItemClicked(object sender, Infragistics.Controls.Menus.ItemClickedEventArgs e)
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
    }
}
