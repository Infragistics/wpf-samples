using System.Windows;
using System.Windows.Controls;
using IGRichTextEditor.Resources;
using Infragistics.Documents.RichText;
using Infragistics.Samples.Framework;

namespace IGRichTextEditor.Samples.Editing
{
    public partial class TabStops : SampleContainer
    {
        ParagraphNode oldPN = null;
        
        public TabStops()
        {
            InitializeComponent();
            InitializeSample();
        }

        void InitializeSample()
        {
            TabStop tabStop220 = new TabStop(new Extent(220, ExtentUnitType.LogicalPixels));
            TabStop tabStop350 = new TabStop(new Extent(350, ExtentUnitType.LogicalPixels));
            TabStop tabStop500 = new TabStop(new Extent(510, ExtentUnitType.LogicalPixels));

            CreateParagraph(RichTextEditorStrings.txtTabStopLine1.Replace("\\t", "\t"), new TabStop[] { tabStop220, tabStop350, tabStop500 });
            CreateParagraph(RichTextEditorStrings.txtTabStopLine2.Replace("\\t", "\t"), new TabStop[] { tabStop220, tabStop350, tabStop500 });
            CreateParagraph(RichTextEditorStrings.txtTabStopLine3.Replace("\\t", "\t"), new TabStop[] { tabStop220, tabStop350, tabStop500 });
            CreateParagraph(RichTextEditorStrings.txtTabStopLine4.Replace("\\t", "\t"), new TabStop[] { tabStop220, tabStop350, tabStop500 });
            CreateParagraph(RichTextEditorStrings.txtTabStopLine5.Replace("\\t", "\t"), new TabStop[] { tabStop220, tabStop350, tabStop500 });
            CreateParagraph(RichTextEditorStrings.txtTabStopLine6.Replace("\\t", "\t"), new TabStop[] { tabStop220, tabStop350, tabStop500 });
        }

        private void CreateParagraph(string content, TabStop[] tabStops)
        {
            ParagraphNode pn = new ParagraphNode();
            pn.SetText(content);
            pn.Settings = new ParagraphSettings();
            foreach (TabStop ts in tabStops)
                pn.Settings.TabStops.AddTab(ts);
            this.xamRichTextEditor1.Document.RootNode.Body.ChildNodes.Add(pn);
        }

		void xamRichTextEditor1_SelectionChanged(object sender, Infragistics.Controls.Editors.RichTextSelectionChangedEventArgs e)
        {
            UpdateCurrentParagraphTabStops();
        }

        private void UpdateCurrentParagraphTabStops()
        {
            VersionedDocumentOffset vdo = this.xamRichTextEditor1.Caret.DocumentOffset;
            NodeBase nb = this.xamRichTextEditor1.Document.GetNodeAtDocumentOffset(vdo.Offset);
            while (nb != null && !(nb is ParagraphNode))
            {
                nb = nb.Parent;
            }
            ParagraphNode pn = nb as ParagraphNode;
            if (pn != null)
            {
                oldPN = pn;
                this.scrollContent.Children.Clear();

                ParagraphSettings ps = pn.Settings;
                if (ps != null)
                {
                    for (int i = 0; i < ps.TabStops.Count; i++)
                    {
                        StackPanel sp = new StackPanel();
                        sp.Orientation = Orientation.Horizontal;
                        
                        Button b = new Button();
                        b.Content = RichTextEditorStrings.btnDelete;
                        b.Tag = ps.TabStops[i];
                        b.Click += RemoveButton_Click;
                        b.Height = 20;
                        sp.Children.Add(b);

                        TextBlock tb = new TextBlock();
                        tb.Margin = new Thickness(5, 5, 0, 0);
                        tb.Text =
                            RichTextEditorStrings.lblTabStopDescr1 + ps.TabStops[i].Position +
                            RichTextEditorStrings.lblTabStopDescr2 + ps.TabStops[i].Alignment +
                            RichTextEditorStrings.lblTabStopDescr3 + ps.TabStops[i].Leader;
                        tb.Height = 20;
                        sp.Children.Add(tb);

                        this.scrollContent.Children.Add(sp);
                    }
                }
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (oldPN == null) return;
            Button b = sender as Button;
            if (b == null) return;
            if (b.Tag is TabStop)
            {
                TabStop ts = (TabStop)b.Tag;
                ParagraphSettings settings = oldPN.Settings;
                settings.TabStops.RemoveTab(ts.Position);
                UpdateCurrentParagraphTabStops();
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VersionedDocumentOffset vdo = this.xamRichTextEditor1.Caret.DocumentOffset;
            NodeBase nb = this.xamRichTextEditor1.Document.GetNodeAtDocumentOffset(vdo.Offset);
            while (nb != null && !(nb is ParagraphNode))
            {
                nb = nb.Parent;
            }
            ParagraphNode pn = nb as ParagraphNode;
            if (pn != null)
            {
                int TabStopExtent;
                try
                {
                    TabStopExtent = int.Parse(this.tabStopExtent.Text);
                    if (TabStopExtent < 0)
                    {
                        MessageBox.Show(RichTextEditorStrings.errValidNumbers);
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show(RichTextEditorStrings.errValidNumbers);
                    return;
                }
                ParagraphSettings settings = pn.Settings;
                if (settings == null)
                {
                    settings = new ParagraphSettings();
                    pn.Settings = settings;
                }
                settings.TabStops.AddTab(
                    new Extent(TabStopExtent, ExtentUnitType.LogicalPixels),
                    (TabStopAlignment)this.tabStopAlignmentCombo.Value,
                    (TabStopLeader)this.tabStopTabStopLeaderCombo.Value);
                oldPN = null;
                UpdateCurrentParagraphTabStops();
            }
        }

    }
}
