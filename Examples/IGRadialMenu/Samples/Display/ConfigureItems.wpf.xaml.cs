using IGRadialMenu.Resources;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Menus;
using Infragistics.Samples.Framework;
using System;
using System.Windows;

namespace IGRadialMenu.Samples.Display
{
    public partial class ConfigureItems : SampleContainer
    {
        private RadialMenuItemBase radialMenuItemBase = null;

        public ConfigureItems()
        {
            InitializeComponent();
        }

        private void RadialMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is RadialMenuItemBase)
            {
                radialMenuItemBase = sender as RadialMenuItemBase;
                this.tbWedgeIndex.Text = "" + radialMenuItemBase.WedgeIndex;
                this.tbWedgeSpan.Text = "" + radialMenuItemBase.WedgeSpan;
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (radialMenuItemBase == null)
            {
                MessageBox.Show(RadialMenuStrings.errSelectMenuItem);
                return;
            }
            try
            {
                int value = int.Parse(this.tbWedgeIndex.Text);
                radialMenuItemBase.WedgeIndex = value;
            }
            catch (Exception)
            {
                MessageBox.Show(RadialMenuStrings.errIntegerValue);
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            if (radialMenuItemBase == null)
            {
                MessageBox.Show(RadialMenuStrings.errSelectMenuItem);
                return;
            }
            try
            {
                int value = int.Parse(this.tbWedgeSpan.Text);
                if (value < 1)
                {
                    MessageBox.Show(RadialMenuStrings.errIntegerValue1More);
                    return;
                }
                radialMenuItemBase.WedgeSpan = value;
            }
            catch (Exception)
            {
                MessageBox.Show(RadialMenuStrings.errIntegerValue1More);
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
