using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Infragistics.Samples.Framework;
using Infragistics.Themes;
using Infragistics.Samples.Shared.Models;
using System.Collections.Generic;
using System;

namespace IGThemeManager.Samples.Display
{
    public partial class ApplyThemes : SampleContainer
    {
        public ApplyThemes()
        {
            InitializeComponent();
            xamCombo.ItemsSource = ThemesData.GetThemesList();
            xamCombo.DisplayMemberPath = "ThemeName";
            xamCombo.SelectedItemChanged += XamCombo_SelectedItemChanged;
        }

        private void XamCombo_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var comboSelection = (ThemeItem)xamCombo.SelectedItem;
            // Apply a theme to a FrameworkElement using the ThemeManager SetTheme method

            SetTheme(this.LayoutRoot, comboSelection);
        }

        private void SetTheme(FrameworkElement element, ThemeItem theme)
        {
            // Apply a theme to a FrameworkElement using the ThemeManager SetTheme method
            ThemeManager.SetTheme(element, null);
            switch(theme.ThemeID)
            {
                case "IG":
                    ThemeManager.SetTheme(element, new IgTheme());
                    break;
                case "Metro":
                    ThemeManager.SetTheme(element, new MetroTheme());
                    break;
                case "MetroDark":
                    ThemeManager.SetTheme(element, new MetroDarkTheme());
                    break;
                case "Office2010":
                    ThemeManager.SetTheme(element, new Office2010BlueTheme());
                    break;
                 case "Office2013":
                    ThemeManager.SetTheme(element, new Office2013Theme());
                    break;
                 case "RoyalDark":
                    ThemeManager.SetTheme(element, new RoyalDarkTheme());
                    break;
                  case "RoyalLight":
                    ThemeManager.SetTheme(element, new RoyalLightTheme());
                    break;
                default:
                    break;

            }
           // ThemeManager.SetTheme(element, theme.Theme);
        }

        public static class ThemesData
        {
            public static IEnumerable<ThemeItem> ThemeItems { get; set; }

            public static IEnumerable<ThemeItem> GetThemesList()
            {
                return new List<ThemeItem>
                {
                     new ThemeItem("IG", "IG"),
                    new ThemeItem("Metro", "Metro"),
                    new ThemeItem("MetroDark", "MetroDark"),
                    new ThemeItem("Office2010", "Office2010"),
                    new ThemeItem("Office2013", "Office2013"),
                    new ThemeItem("RoyalDark", "RoyalDark"),
                    new ThemeItem("RoyalLight", "RoyalLight"),
                    //etc.
                };
            }
        }

        [Serializable]
        public class ThemeItem 
        {
            public string ThemeID { get; set; }
            public string ThemeName { get; set; }
         
            public ThemeItem(string themeID, string localizableThemeName)
            {
                ThemeID = themeID;
                ThemeName = localizableThemeName;
            }
        }
    }
}
