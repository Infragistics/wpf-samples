using Infragistics.Controls.Editors;
using Infragistics.Controls.Menus;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Models;
using Infragistics.Samples.Shared.Resources;
using Infragistics.Samples.Shared.Tools;
using Infragistics.Themes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace IGRadialMenu.Samples.Styling
{
    public partial class Theming : SampleContainer
    {
        bool iconsLight = false;
        private Image imgBold;
        private Image imgItalic;
        private Image imgFontSize;
        private Image imgFont;
        private Image imgBoldLight;
        private Image imgItalicLight;
        private Image imgFontSizeLight;
        private Image imgFontLight;

        public Theming()
        {
            InitializeComponent();
            InitializeSample();
        }

        public void InitializeSample()
        { 
            imgBold = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Bold.png") };
            imgItalic = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Italic.png") };
            imgFontSize = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Size.png") };
            imgFont = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/Font.png") };

            imgBoldLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/BoldLight.png") };
            imgItalicLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/ItalicLight.png") };
            imgFontSizeLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/SizeLight.png") };
            imgFontLight = new Image() { Width = 16, Height = 16, Source = GetImageFromUriString("/IGRadialMenu;component/Images/FontLight.png") };
        }

        private BitmapImage GetImageFromUriString(string uriString)
        {
            Uri uri = new Uri(uriString, UriKind.RelativeOrAbsolute);
            BitmapImage bmpImage = new BitmapImage();
            bmpImage.BeginInit();
            bmpImage.UriSource = uri;
            bmpImage.EndInit();
            return bmpImage;
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            var theme = this.ThemeSelector.SelectedItem as ThemeInfo;           
            if (theme == null) return;

            // apply selected theme 
            ThemeManager.SetTheme(this.LayoutRoot, theme.Resources);

            UpdateRadialMenuIcons(!theme.ID.Contains("Dark"));
            
        }
         
        private void UpdateRadialMenuIcons(bool light)
        {
            if (light && !iconsLight)
            {
                this.btnBold.Icon = imgBoldLight;
                this.btnItalic.Icon = imgItalicLight;
                this.fontNumericItem.Icon = imgFontSizeLight;
                this.fontSubMenu.Icon = imgFontLight;
                iconsLight = true;
            }
            else if (!light && iconsLight)
            {
                this.btnBold.Icon = imgBold;
                this.btnItalic.Icon = imgItalic;
                this.fontNumericItem.Icon = imgFontSize;
                this.fontSubMenu.Icon = imgFont;
                iconsLight = false;
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
            if (this.xamRichTextEditor1.ActiveDocumentView == null) return;
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
