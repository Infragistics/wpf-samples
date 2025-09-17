using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using System.Windows.Media;
using Infragistics.Themes;

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
            calendarRight.DisplayDateStart = sliderRight.MinValue;
            calendarRight.DisplayDateEnd = sliderRight.MaxValue;
            calendarRight.DisplayDate = new DateTime(2013, 9, 15);
        }
         
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
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
