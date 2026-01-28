using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;

namespace IGGantt.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
            this.Unloaded += OnSampleUnloaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            List<Theme> themes = new List<Theme>
            {
                new Theme("IG", CommonStrings.XW_ThemeSupport_IGTheme,
                    new Uri("/Infragistics.Themes.IGTheme;component/IG.xamGantt.xaml", UriKind.Relative)),
                new Theme("Default", CommonStrings.XW_ThemeSupport_DefaultTheme, null),
                new Theme("Blue", CommonStrings.XW_ThemeSupport_Office2010BlueTheme,
                    new Uri("/Infragistics.Themes.Office2010BlueTheme;component/Office2010Blue.xamGantt.xaml", UriKind.Relative)),
                new Theme("Metro", CommonStrings.XW_ThemeSupport_Metro,
                    new Uri("/Infragistics.Themes.MetroTheme;component/Metro.xamGantt.xaml", UriKind.Relative)),
                new Theme("MetroDark", CommonStrings.XW_ThemeSupport_MetroDark,
                    new Uri("/Infragistics.Themes.MetroDarkTheme;component/MetroDark.xamGantt.xaml", UriKind.Relative)),
            };

            themeChangeCombo.ItemsSource = themes;
            themeChangeCombo.SelectedIndex = 0;
        }

        private void themeChangeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Theme selectedTheme = (Theme)e.AddedItems[0];

                // remove any available theme for the specified control
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamGantt");
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamMenu");
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamCalendar");

                if (selectedTheme.ThemeUri != null)
                {
                    var menuThemePath = selectedTheme.ThemeUri.OriginalString.Replace("xamGantt", "xamMenu");
                    Uri menuThemeUri = new Uri(menuThemePath, UriKind.Relative);

                    var calendarThemePath = selectedTheme.ThemeUri.OriginalString.Replace("xamGantt", "xamCalendar");
                    Uri calendarThemeUri = new Uri(calendarThemePath, UriKind.Relative);

                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = selectedTheme.ThemeUri });
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = menuThemeUri });
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = calendarThemeUri });
                }
            }
        }

        private void OnSampleUnloaded(object sender, RoutedEventArgs e)
        {
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamGantt");
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamMenu");
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamCalendar");
        }
    }
}
