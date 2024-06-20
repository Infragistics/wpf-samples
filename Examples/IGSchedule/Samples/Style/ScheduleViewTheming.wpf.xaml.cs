using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Schedules;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;

namespace IGSchedule.Samples.Style
{
    /// <summary>
    /// Interaction logic for ScheduleViewTheming.xaml
    /// </summary>
    public partial class ScheduleViewTheming : SampleContainer
    {
        ResourceDictionary dictionary;

        public ScheduleViewTheming()
        {
            InitializeComponent(); 
            this.SampleDisposed += DayViewTheming_SampleDisposed;
        }

        private void DayViewTheming_SampleDisposed(object sender, EventArgs e)
        {
            Application.Current.Resources.MergedDictionaries.Remove(dictionary);
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);

            if (theme.Resources != null)
            {
                this.NullColorScheme.IsChecked = true;
                ChangeRadioButtonsState(this.RBHolder, false);                 
            }
            else
            {
                ChangeRadioButtonsState(this.RBHolder, true);
            }

            if (theme.Resources != null)
            {
                dictionary = ThemeResourceHelper.ThemeOptimizer(dictionary, theme.Resources);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Remove(dictionary);
            }
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
