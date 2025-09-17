using Infragistics.Controls.Editors;
using Infragistics.Samples.Framework;
using System;
using System.Windows;

namespace IGRadialMenu.Samples.Display
{
    public partial class ButtonItems : SampleContainer
    {
        public ButtonItems()
        {
            InitializeComponent();
        }

        private void RadialMenu_Click(object sender, EventArgs e)
        {
            if (this.cbCloseOnClick.IsChecked.HasValue && this.cbCloseOnClick.IsChecked.Value && this.rMenu.IsOpen)
            {
                this.rMenu.IsOpen = false;
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
