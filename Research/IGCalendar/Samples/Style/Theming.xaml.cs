using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;

namespace IGCalendar.Samples.Style
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent(); 
        }
          
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources); 
        }

        void ColorScheme_Changed(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                var result = (CalendarResourceSet)Enum.Parse(typeof(CalendarResourceSet), rb.Tag as string, true);
                ChangeCalendarResourceSet(result);
            }
        }

        private void ChangeCalendarResourceSet(CalendarResourceSet newCalendarResourceSet)
        {
            xamCalendar.ClearValue(XamCalendar.ResourceProviderProperty);
            xamCalendar.SetValue(
                XamCalendar.ResourceProviderProperty,
                new CalendarResourceProvider() { ResourceSet = newCalendarResourceSet });
        }       
         
    }
}