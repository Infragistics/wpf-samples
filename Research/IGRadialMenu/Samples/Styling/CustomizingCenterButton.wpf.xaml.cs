using Infragistics.Controls.Editors;
using Infragistics.Controls.Menus;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;
using System.Windows;

namespace IGRadialMenu.Samples.Styling
{
    public partial class CustomizingCenterButton : SampleContainer
    {
        public CustomizingCenterButton()
        {
            InitializeComponent();
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
