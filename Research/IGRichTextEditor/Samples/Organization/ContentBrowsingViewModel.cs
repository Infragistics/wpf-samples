using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using IGRichTextEditor.Resources;
using IGRichTextEditor.Samples.Helpers;
using Infragistics.Documents.RichText;

namespace IGRichTextEditor.Samples.Organization
{
    public class ContentBrowsingViewModel : INotifyPropertyChanged
    {
        public ContentBrowsingViewModel()
        {
        }

        private List<CustomNode> _nodes = new List<CustomNode>();
        public List<CustomNode> Nodes
        {
            get
            {
                return this._nodes;
            }
            
            set
            {
                this._nodes = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Nodes"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private RichTextDocument _document;
        public RichTextDocument Document
        {
            get
            {
                if (this._document == null)
                {
                    Stream fs = FilesProvider.GetStreamForFile(RichTextEditorStrings.fileDocxRichContent);
                    this._document = new RichTextDocument();
                    this._document.LoadFromWord(fs);
                    UpdateDocumentTree();
                    this._document.ContentChanged += _document_ContentChanged;
                }
                return this._document;
            }
        }

        void _document_ContentChanged(object sender, DocumentContentChangedEventArgs e)
        {
            UpdateDocumentTree();
        }

        private void UpdateDocumentTree()
        {
            CustomNode cn = CreateCustomNode(this._document.RootNode);
            List<CustomNode> l = new List<CustomNode>();
            l.Add(cn);
            this.Nodes = l;
        }

        private CustomNode CreateCustomNode(NodeBase nb)
        {
            CustomNode cn = new CustomNode();
            cn.DisplayName = nb.GetType().Name;
            cn.Content = RichTextEditorStrings.lblType + " " + nb.GetType().Name;

            if (nb is TextNode)
            {
                cn.Content += "\r\n" + RichTextEditorStrings.lblContent + " \"" + ((TextNode)nb).Text + "\"";
            }
            else if (nb is RunNode)
            {
                RunNode rn = (RunNode)nb;
                SerializeCharacterSettings(rn.Settings, cn);
                if (rn.StyleId != null)
                {
                    cn.Content += "\r\n[StyleId: " + rn.StyleId + "]";
                }
            }
            else if (nb is ParagraphNode)
            {
                ParagraphNode pn = (ParagraphNode)nb;
                SerializeParagraphSettings(pn.Settings, cn);
                if (pn.StyleId != null)
                {
                    cn.Content += "\r\n[StyleId: " + pn.StyleId + "]";
                }
            }
            else if (nb is DocumentBodyNode)
            {
                DocumentBodyNode dbn = (DocumentBodyNode)nb;
            }
            else if (nb is DocumentRootNode)
            {
                DocumentRootNode drn = (DocumentRootNode)nb;
                if (drn.Styles != null && drn.Styles.Count > 0)
                {
                    cn.Content += "\r\n[Styles: ";
                    int i = 0;
                    foreach (RichTextStyleBase style in drn.Styles)
                    {
                        cn.Content += style.Id;
                        if (i != drn.Styles.Count - 1)
                        {
                            cn.Content += ", ";
                        }
                        i++;
                    }
                    cn.Content += "]";
                }
            }
            else if (nb is ImageNode)
            {
                ImageNode imgNode = (ImageNode)nb;
                if (imgNode.Image != null)
                    cn.Content += "\r\n[" + RichTextEditorStrings.setFormat + "(" + imgNode.Image.Format + ")] ";
                if (imgNode.RenderSize != null && imgNode.RenderSize.HasValue)
                {
                    cn.Content += "[" + RichTextEditorStrings.setWidth + "(" + imgNode.RenderSize.Value.Width + ")]";
                    cn.Content += "[" + RichTextEditorStrings.setHeight + "(" + imgNode.RenderSize.Value.Height + ")]";
                }
            }
            else if (nb is HyperlinkNode)
            {
                HyperlinkNode hn = (HyperlinkNode)nb;
                if (hn.Uri != null)
                    cn.Content += "\r\n[" + RichTextEditorStrings.setUri + "(" + hn.Uri + ")] ";
                if (hn.Tooltip != null)
                    cn.Content += "[" + RichTextEditorStrings.setTooltip + "(" + hn.Tooltip + ")] ";
            }
            else if (nb is TableNode)
            {
                TableNode tn = (TableNode)nb;
                cn.Content += "\r\n[" + RichTextEditorStrings.setRows + "(" + tn.GetRowCount() + ")] ";
                cn.Content += "[" + RichTextEditorStrings.setColumns + "(" + tn.GetColumnCount() + ")] ";
                if (tn.StyleId != null)
                {
                    cn.Content += "\r\n[StyleId: " + tn.StyleId + "]";
                }
            }
            foreach (NodeBase childNB in nb.ChildNodes)
            {
                cn.ChildNodes.Add(CreateCustomNode(childNB));
            }
            return cn;
        }

        private void SerializeCharacterSettings(CharacterSettings cs, CustomNode cn)
        {
            if (cs == null) return;
            cn.Content += "\r\n" + RichTextEditorStrings.lblSettings;
            if (cs.Bold == true) cn.Content += "[" + RichTextEditorStrings.setBold + "] ";
            if (cs.Italics == true) cn.Content += "[" + RichTextEditorStrings.setItalics + "] ";
            if (cs.UnderlineType.HasValue && cs.UnderlineType.Value != UnderlineType.None)
                cn.Content += "[" + RichTextEditorStrings.setUnderline + "(" + cs.UnderlineType.Value + ")] ";
            if (cs.StrikeThrough == true)
                cn.Content += "[" + RichTextEditorStrings.setStrikeThrough + "] ";
            if (cs.DoubleStrikeThrough == true)
                cn.Content += "[" + RichTextEditorStrings.setDoubleStrikeThrough + "] ";
            if (cs.Color != null)
                cn.Content += "[" + RichTextEditorStrings.setColor + "(" + cs.Color.Color.ToString() + ")] ";
            if (cs.HighlightColor != null)
                cn.Content += "[" + RichTextEditorStrings.setHighlight + "(" + cs.HighlightColor.ToString() + ")] ";
            if (cs.FontSettings != null && cs.FontSettings.Ascii.HasValue)
                cn.Content += "[" + RichTextEditorStrings.setFont + "(" + cs.FontSettings.Ascii.Value.Name + ")] ";
            if (cs.FontSize != null && cs.FontSize.HasValue)
                cn.Content += "[" + RichTextEditorStrings.setFontSize + "(" + cs.FontSize.Value.ToString() + ")] ";
        }

        private void SerializeParagraphSettings(ParagraphSettings ps, CustomNode cn)
        {
            if (ps == null) return;
            cn.Content += "\r\n" + RichTextEditorStrings.lblSettings;
            if (ps.Indentation != null)
                cn.Content += "[" + RichTextEditorStrings.setIdentation + "(" + ps.Indentation.ToString() + ")] ";
            if (ps.ParagraphAlignment.HasValue)
                cn.Content += "[" + RichTextEditorStrings.setHAlignment + "(" + ps.ParagraphAlignment.Value + ")] ";
            if (ps.TextAlignment.HasValue)
                cn.Content += "[" + RichTextEditorStrings.setVAlignment + "(" + ps.TextAlignment.Value + ")] ";
            if (ps.Spacing != null)
                cn.Content += "[" + RichTextEditorStrings.setSpacing + "(" + 
                    RichTextEditorStrings.setBefore + ps.Spacing.BeforeParagraph + ", " +
                    RichTextEditorStrings.setAfter + ps.Spacing.AfterParagraph + ", " +
                    RichTextEditorStrings.setLine + ps.Spacing.LineSpacing + ")] ";
            if (ps.WordWrap.HasValue)
                cn.Content += "[" + RichTextEditorStrings.setWordWrap + "(" + ps.WordWrap + ")] ";
            if (ps.Frame != null && ps.Frame.DropCap.HasValue)
            {
                cn.Content += "[" + RichTextEditorStrings.setDropCap + "(" + ps.Frame.DropCap.Value;
                if (ps.Frame.DropCapLines.HasValue)
                    cn.Content += ", " + RichTextEditorStrings.setLines + ps.Frame.DropCapLines.Value;
                cn.Content += ")]";
            }
            if (ps.ListId != null)
            {
                cn.Content += "[" + RichTextEditorStrings.setListId + "(" + ps.ListId + ")] ";
                cn.Content += "[" + RichTextEditorStrings.setListLevel + "(" + ps.ListLevel + ")] ";
            }
        }
    }

    public class CustomNode
    {
        private string _displayName = string.Empty;
        private string _content = string.Empty;
        private List<CustomNode> _childNodes;

        public CustomNode()
        {
            this._childNodes = new List<CustomNode>();
        }

        public string DisplayName
        {
            get { return this._displayName; }
            set { this._displayName = value; }
        }

        public string Content
        {
            get { return this._content; }
            set { this._content = value; }
        }

        public List<CustomNode> ChildNodes
        {
            get
            {
                return this._childNodes;
            }
        }
    }
}
