using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;

namespace IGSlider.Samples.Style
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
            List<Theme> themes = new List<Theme>
            {
                new Theme("IG", CommonStrings.XW_ThemeSupport_IGTheme,
                    new Uri("/Infragistics.Themes.IGTheme;component/IG.xamSlider.xaml", UriKind.Relative)),
                new Theme("Default", CommonStrings.XW_ThemeSupport_DefaultTheme, null),
                new Theme("Blue", CommonStrings.XW_ThemeSupport_Office2010BlueTheme,
                    new Uri("/Infragistics.Themes.Office2010BlueTheme;component/Office2010Blue.xamSlider.xaml", UriKind.Relative)),
                new Theme("Metro", CommonStrings.XW_ThemeSupport_Metro,
                    new Uri("/Infragistics.Themes.MetroTheme;component/Metro.xamSlider.xaml", UriKind.Relative)),
                new Theme("MetroDark", CommonStrings.XW_ThemeSupport_MetroDark,
                    new Uri("/Infragistics.Themes.MetroDarkTheme;component/MetroDark.xamSlider.xaml", UriKind.Relative)),
            };

            themeChangeCombo.ItemsSource = themes;
            themeChangeCombo.SelectedIndex = 0;

            calendarRight.DisplayDateStart = sliderRight.MinValue;
            calendarRight.DisplayDateEnd = sliderRight.MaxValue;
            calendarRight.DisplayDate = new DateTime(2013, 9, 15);
        }

        private void themeChangeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Theme selectedTheme = (Theme)e.AddedItems[0];

                // remove any available theme for the specified control
                ThemeResourceHelper.RemoveResourceDictionariesForControl(this.Resources, "xamSlider");

                if (selectedTheme.ThemeUri != null)
                {
                    // add the newly selected theme
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = selectedTheme.ThemeUri });
                }
            }
        }

        private void calendarRight_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            this.calendarRight.DisplayDate = this.calendarRight.SelectedDate.Value;
        }

        private void calendarRight_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            this.calendarRight.SelectedDate = this.calendarRight.DisplayDate;
        }
    }
}
