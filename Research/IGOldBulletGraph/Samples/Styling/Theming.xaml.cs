using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;

namespace IGBulletGraph.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            InitializeComponent();
            this.Loaded += OnSampleLoaded;
        }

        void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= OnSampleLoaded;
            // set locations for xamGauge themes
            Uri uriIgTheme = new Uri("/Infragistics.Themes.IGTheme;component/IG.xamGauge.xaml", UriKind.RelativeOrAbsolute);
            Uri uriOfficeBlueTheme = new Uri("/Infragistics.Themes.Office2010BlueTheme;component/Office2010Blue.xamGauge.xaml", UriKind.RelativeOrAbsolute);
            Uri uriMetroTheme = new Uri("/Infragistics.Themes.MetroTheme;component/Metro.xamGauge.xaml", UriKind.RelativeOrAbsolute);
            Uri uriMetroDarkTheme = new Uri("/Infragistics.Themes.MetroDarkTheme;component/MetroDark.xamGauge.xaml", UriKind.RelativeOrAbsolute);
            Uri uriRoyalDarkTheme = new Uri("/Infragistics.Themes.RoyalDarkTheme;component/RoyalDark.xamGauge.xaml", UriKind.RelativeOrAbsolute);

            var themes = new List<Theme>            
            {                
                new Theme {ThemeName = CommonStrings.XW_ThemeSupport_IGTheme, ThemeUri = uriIgTheme},
                new Theme {ThemeName = CommonStrings.XW_ThemeSupport_DefaultTheme, ThemeUri = null},
                new Theme {ThemeName = CommonStrings.XW_ThemeSupport_Office2010BlueTheme, ThemeUri = uriOfficeBlueTheme},
                new Theme {ThemeName = CommonStrings.XW_ThemeSupport_Metro, ThemeUri = uriMetroTheme},
                new Theme {ThemeName = CommonStrings.XW_ThemeSupport_MetroDark, ThemeUri = uriMetroDarkTheme},
                new Theme {ThemeName = CommonStrings.XW_ThemeSupport_RoyalDark, ThemeUri = uriRoyalDarkTheme},
            };

            this.themeComboBox.DisplayMemberPath = "ThemeName";
            this.themeComboBox.ItemsSource = themes;
            this.themeComboBox.SelectionChanged += OnSelectionChanged;
            this.themeComboBox.SelectedIndex = 0;


            this.btnNext.Click += OnNextItemButtonClick;
            this.btnPrevious.Click += OnPreviousItemButtonClick;
        }

        private void OnPreviousItemButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.themeComboBox.SelectedIndex == 0)
            {
                this.themeComboBox.SelectedIndex = this.themeComboBox.Items.Count - 1;
            }
            else
            {
                this.themeComboBox.SelectedIndex -= 1;
            }
        }

        private void OnNextItemButtonClick(object sender, RoutedEventArgs e)
        {
            this.themeComboBox.SelectedIndex = (this.themeComboBox.SelectedIndex + 1) % this.themeComboBox.Items.Count;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ThemeResourceHelper.RemoveResourceDictionariesForControl(this.Resources, "xamGauge");

            Theme theme = (Theme)this.themeComboBox.SelectedItem;
            if (theme.IsDefaultTheme) return; // no need to apply default theme 

            // apply selected theme to the xamBulletGraph control
            this.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = theme.ThemeUri });
        }
    }
}
