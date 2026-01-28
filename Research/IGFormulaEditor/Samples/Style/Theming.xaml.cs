using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Controls.Interactions;
using Infragistics.Themes;

namespace IGFormulaEditor.Samples.Style
{
    /// <summary>
    /// Interaction logic for Theming.xaml
    /// </summary>
    public partial class Theming : SampleContainer
    {
        private ThemeInfo theme;

        public Theming()
        {
            this.UseDefaultTheme = true;
            InitializeComponent();
            this.formulaEditor.FormulaEditorDialogDisplaying += OnFormulaEditorDialogDisplaying;
            
            this.Loaded += OnSampleLoaded;
        }

        private void OnSampleLoaded(object sender, RoutedEventArgs e)
        {
            theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme  
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);
        }

        // Apply the theme to the FormulaEditorDialog
        // This is needed because the Page resources do not apply for the 
        // window in which the FormulaEditorDialog is displayed
        void OnFormulaEditorDialogDisplaying(object sender, FormulaEditorDialogDisplayingEventArgs e)
        {
            if (theme == null) return;
            if (theme.Resources == null) return;
            
            ThemeManager.SetTheme(e.Dialog, theme.Resources);
        }
    }
}
