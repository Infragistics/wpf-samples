using Infragistics.Controls.Editors;
using Infragistics.Controls.Menus;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;
using System.Windows;
using System.Windows.Media;

namespace IGRadialMenu.Samples.Display
{
    public partial class ColorItems : SampleContainer
    {
        public ColorItems()
        {
            InitializeComponent();
        }

        private void RMColorItemForeground_Click(object sender, System.EventArgs e)
        {
            if (this.xamRichTextEditor1.ActiveDocumentView == null) return;
            RadialMenuColorItem ci = sender as RadialMenuColorItem;
            if (ci != null)
            {
                this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyTextForecolor(ci.Color);
            }
        }

        private void RMColorItemForeground_Change(object sender, RadialMenuColorChangedEventArgs e)
        {
            if (this.xamRichTextEditor1.ActiveDocumentView == null) return;
            Color c = e.NewValue;
            this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyTextForecolor(new ColorInfo(c));
        }

        private void RMColorItemBackground_Click(object sender, System.EventArgs e)
        {
            if (this.xamRichTextEditor1.ActiveDocumentView == null) return;
            RadialMenuColorItem ci = sender as RadialMenuColorItem;
            if (ci != null)
            {
                this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyTextHighlightColor(ColorToHighlightColor(ci.Color));
            }
        }

        private void RMColorItemBackground_Change(object sender, RadialMenuColorChangedEventArgs e)
        {
            if (this.xamRichTextEditor1.ActiveDocumentView == null) return;
            this.xamRichTextEditor1.ActiveDocumentView.Selection.ApplyTextHighlightColor(ColorToHighlightColor(e.NewValue));
        }

        private HighlightColor ColorToHighlightColor(Color c)
        {
            if (c.Equals(Colors.White))
                return HighlightColor.White;
            else if (c.Equals(Colors.Black))
                return HighlightColor.Black;
            else if (c.Equals(Colors.Blue))
                return HighlightColor.Blue;
            else if (c.Equals(Colors.Cyan))
                return HighlightColor.Cyan;
            else if (c.Equals(Colors.Green))
                return HighlightColor.Green;
            else if (c.Equals(Colors.Magenta))
                return HighlightColor.Magenta;
            else if (c.Equals(Colors.Red))
                return HighlightColor.Red;
            else if (c.Equals(Colors.Yellow))
                return HighlightColor.Yellow;
            return HighlightColor.None;
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
