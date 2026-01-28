using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;

namespace IGSchedule.Samples.Style
{
    public partial class DateNavigatorOutlookCalendarTheming : SampleContainer
    {
        public DateNavigatorOutlookCalendarTheming()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(DayViewTheming_Loaded);
            this.Unloaded += new RoutedEventHandler(DayViewTheming_Unloaded);
        }

        void DayViewTheming_Loaded(object sender, RoutedEventArgs e)
        {
            this.dataManager.ColorScheme = new IGColorScheme();

            List<Theme> themes = new List<Theme>
            {
                new Theme("Default", CommonStrings.XW_ThemeSupport_DefaultTheme, null),
                new Theme("Metro", CommonStrings.XW_ThemeSupport_Metro,
                    new Uri("/Infragistics.Themes.MetroTheme;component/Metro.xamSchedule.xaml", UriKind.Relative)),
                new Theme("MetroDark", CommonStrings.XW_ThemeSupport_MetroDark,
                    new Uri("/Infragistics.Themes.MetroDarkTheme;component/MetroDark.xamSchedule.xaml", UriKind.Relative)),
            };

            this.themeChangeCombo.ItemsSource = themes;
            this.themeChangeCombo.SelectedIndex = 0;
            this.DefaultSchemeRadio.IsChecked = true;
        }

        void DayViewTheming_Unloaded(object sender, RoutedEventArgs e)
        {
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamSchedule");
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamCalendar");
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamDialogWindow");
            ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamMenu");
        }

        private void ColorScheme_Changed(object sender, RoutedEventArgs e)
        {
            if (dataManager != null)
            {
                RadioButton rbtn = sender as RadioButton;
                if (rbtn != null)
                {
                    if (rbtn.Tag.ToString().StartsWith("IGTheme"))
                    {
                        dataManager.ColorScheme = new IGColorScheme();
                    }
                    else if (rbtn.Tag.ToString().StartsWith("Null"))
                    {
                        dataManager.ClearValue(XamScheduleDataManager.ColorSchemeProperty);
                    }
                    else
                    {
                        bool is2007Theme = rbtn.Tag.ToString().StartsWith("2007_");
                        if (is2007Theme)
                        {
                            var scheme2007 = new Office2007ColorScheme();
                            scheme2007.OfficeColorScheme = (OfficeColorScheme)int.Parse(rbtn.Tag.ToString().Substring(5));
                            dataManager.ColorScheme = scheme2007;
                        }
                        else
                        {
                            var scheme2010 = new Office2010ColorScheme();
                            scheme2010.OfficeColorScheme = (OfficeColorScheme)int.Parse(rbtn.Tag.ToString().Substring(5));
                            dataManager.ColorScheme = scheme2010;
                        }
                    }
                }
            }
        }

        private void themeChangeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Theme selectedTheme = (Theme)e.AddedItems[0];

                // remove any available theme for the specified control
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamSchedule");
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamCalendar");
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamDialogWindow");
                ThemeResourceHelper.RemoveResourceDictionariesForControl(Application.Current.Resources, "xamMenu");

                if (selectedTheme.ThemeUri != null)
                {
                    this.NullColorScheme.IsChecked = true;
                    ChangeRadioButtonsState(this.RBHolder, false);

                    // add the newly selected theme
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = selectedTheme.ThemeUri });
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri(selectedTheme.ThemeUri.OriginalString.Replace("xamSchedule", "xamCalendar"), UriKind.Relative)
                    });
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri(selectedTheme.ThemeUri.OriginalString.Replace("xamSchedule", "xamDialogWindow"), UriKind.Relative)
                    });
                    Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
                    {
                        Source = new Uri(selectedTheme.ThemeUri.OriginalString.Replace("xamSchedule", "xamMenu"), UriKind.Relative)
                    });
                }
                else
                {
                    ChangeRadioButtonsState(this.RBHolder, true);
                }
            }
        }

        private void ChangeRadioButtonsState(UIElement node, bool newState)
        {
            if (node is RadioButton)
            {
                ((RadioButton)node).IsEnabled = newState;
            }
            else if (node is Panel)
            {
                foreach (UIElement el in ((Panel)node).Children)
                {
                    ChangeRadioButtonsState(el, newState);
                }
            }
        }
    }
}
