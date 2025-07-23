using Infragistics.Controls.Editors;
using Infragistics.Controls.Menus;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace IGRadialMenu.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        public Theming()
        {
            InitializeComponent();
            InitializeSample();
        }

        public void InitializeSample()
        {
            var themes = new List<Theme>
            {
                new Theme("IG", CommonStrings.XW_ThemeSupport_IGTheme,
                    new Uri("/Infragistics.Themes.IGTheme;component/IG.xamRadialMenu.xaml", UriKind.Relative)),
                new Theme("Default", CommonStrings.XW_ThemeSupport_DefaultTheme, null),
                new Theme("Blue", CommonStrings.XW_ThemeSupport_Office2010BlueTheme,
                    new Uri("/Infragistics.Themes.Office2010BlueTheme;component/Office2010Blue.xamRadialMenu.xaml", UriKind.Relative)),
                new Theme("Metro", CommonStrings.XW_ThemeSupport_Metro,
                    new Uri("/Infragistics.Themes.MetroTheme;component/Metro.xamRadialMenu.xaml", UriKind.Relative)),
                new Theme("MetroDark", CommonStrings.XW_ThemeSupport_MetroDark,
                    new Uri("/Infragistics.Themes.MetroDarkTheme;component/MetroDark.xamRadialMenu.xaml", UriKind.Relative)),
            };

            this.themeChangeCombo.ItemsSource = themes;
            this.themeChangeCombo.SelectedIndex = 0;
        }

        private BitmapImage GetImageFromUriString(string uriString)
        {
            Uri uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.UriSource = uri;
            return bmpImage;
        }

        private void themeChangeCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedTheme = (Theme)e.AddedItems[0];

                // remove any available theme for the specified control
                ThemeResourceHelper.RemoveResourceDictionariesForControl(this.Resources, "xamRadialMenu");

                if (selectedTheme.ThemeUri != null)
                {
                    // add the newly selected theme
                    this.Resources.MergedDictionaries.Add(new ResourceDictionary() { Source = selectedTheme.ThemeUri });
                }
            }
        }

        private void RadialMenuNumericItem_Click(object sender, System.EventArgs e)
        {
            if (this.xamRichTextEditor1.ActiveDocumentView == null) return;
            RadialMenuNumericItem ni = sender as RadialMenuNumericItem;
            if (ni != null)
            {
                this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyFontSize(ni.Value);
            }
        }

        private void RadialMenuNumericItem_ValueChanged(object sender, Infragistics.Controls.Menus.RadialMenuNumericValueChangedEventArgs e)
        {
            if (this.xamRichTextEditor1 == null || this.xamRichTextEditor1.ActiveDocumentView == null) return;
            this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyFontSize(e.NewValue);
        }

        private void fontSubMenu_SelectedValueChanged(object sender, Infragistics.Controls.Menus.RadialMenuListValueChangedEventArgs e)
        {
            if (this.xamRichTextEditor1.ActiveDocumentView == null) return;

            if (e.NewValue is string)
            {
                this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyFont(new RichTextFont((string)e.NewValue));
            }
        }

        private void xamRichTextEditor1_SelectionChanged(object sender, Infragistics.Controls.Editors.RichTextSelectionChangedEventArgs e)
        {
            Caret c = e.DocumentView.RichTextEditor.Caret;
            if (c != null)
            {
                Point p = c.PixelLocation;
                Thickness t = new Thickness(0);
                t.Left = p.X + 50;
                t.Top = p.Y + 50;
                if (t.Left + this.rMenu.Width > e.DocumentView.RichTextEditor.ActualWidth)
                {
                    t.Left = p.X - this.rMenu.Width - 50;
                }
                if (t.Top + this.rMenu.Height > e.DocumentView.RichTextEditor.ActualHeight)
                {
                    t.Top = e.DocumentView.RichTextEditor.ActualHeight - this.rMenu.Height;
                }
                this.rMenu.Margin = t;
            }
        }
    }
}
